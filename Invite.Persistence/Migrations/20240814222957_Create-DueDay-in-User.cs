using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateDueDayinUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DueDay",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDay",
                table: "Users");
        }
    }
}
