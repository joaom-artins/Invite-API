using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using api.Utils;
using FluentValidation;
using Invite.Business.Utils;
using Invite.Commons.Middlewares;
using Invite.Entities.Models;
using Invite.Entities.Validators;
using Invite.Persistence.Context;
using Invite.Persistence.Utils;
using Invite.Services.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

builder.Services.AddIdentity<UserModel, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configurar autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
builder.Services.AddValidatorsFromAssemblyContaining<ResponsibleCreateRequestValidator>();

builder.Services.AddHttpContextAccessor();

RegisterPersistence.Register(builder);
RegisterBusiness.Register(builder);
RegisterService.Register(builder);


var app = builder.Build();

app.UseMiddleware(typeof(ExceptionMiddleware));

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DocExpansion(DocExpansion.None));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
