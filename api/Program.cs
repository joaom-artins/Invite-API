using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using api.Utils;
using FluentValidation;
using Invite.Business.Utils;
using Invite.Commons;
using Invite.Commons.Middlewares;
using Invite.Commons.Utils;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Context;
using Invite.Persistence.Utils;
using Invite.Services.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

var appSettingsSection = builder.Configuration.GetRequiredSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "INVITE API",
        Description = "API para envio de convites",
        Version = "v1"
    });
});

builder.Services.AddControllers();

builder.Services
.AddControllers(options =>
{
    options.ModelValidatorProviders.Clear();
    options.Filters.Add(new ConsumesAttribute("application/json"));
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<NotificationFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequest>();

builder.Services.AddHttpContextAccessor();

RegisterPersistence.Register(builder);
RegisterBusiness.Register(builder);
RegisterCommons.Register(builder);
RegisterService.Register(builder);

builder.Services.AddIdentity<UserModel, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(auth =>
{
    auth.RequireHttpsMetadata = true;
    auth.SaveToken = true;
    auth.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings!.Jwt.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseMiddleware(typeof(ExceptionMiddleware));
app.UseMiddleware(typeof(AuthorizationMiddleware));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DocExpansion(DocExpansion.None));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
    dbContext!.Database.Migrate();
}

app.Run();
