using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateHall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Events",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Events",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HallId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Events",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Events",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Events",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseHallRegistred",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 11, nullable: false),
                    OwnerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    State = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    PriceInWeek = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    PriceInWeekend = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_HallId",
                table: "Events",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_UserId",
                table: "Halls",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Halls_HallId",
                table: "Events",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Halls_HallId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Events_HallId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UseHallRegistred",
                table: "Events");
        }
    }
}
