using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Users",
                newName: "CPFOrCNPJ");

            migrationBuilder.AddColumn<int>(
                name: "TypeClient",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropColumn(
                name: "TypeClient",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CPFOrCNPJ",
                table: "Users",
                newName: "CPF");
        }
    }
}
