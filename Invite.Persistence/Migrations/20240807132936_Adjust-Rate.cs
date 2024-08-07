using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "Halls",
                type: "float(1)",
                precision: 1,
                scale: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "Buffets",
                type: "float(1)",
                precision: 1,
                scale: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Halls",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(1)",
                oldPrecision: 1,
                oldScale: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Buffets",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(1)",
                oldPrecision: 1,
                oldScale: 1);
        }
    }
}
