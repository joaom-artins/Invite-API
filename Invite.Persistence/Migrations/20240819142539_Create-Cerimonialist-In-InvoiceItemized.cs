using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateCerimonialistInInvoiceItemized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CerimonialisId",
                table: "InvoiceItemizeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CerimonialistId",
                table: "InvoiceItemizeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemizeds_CerimonialistId",
                table: "InvoiceItemizeds",
                column: "CerimonialistId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemizeds_Cerimonialists_CerimonialistId",
                table: "InvoiceItemizeds",
                column: "CerimonialistId",
                principalTable: "Cerimonialists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemizeds_Cerimonialists_CerimonialistId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemizeds_CerimonialistId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropColumn(
                name: "CerimonialisId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropColumn(
                name: "CerimonialistId",
                table: "InvoiceItemizeds");
        }
    }
}
