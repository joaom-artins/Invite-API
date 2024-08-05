using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateColumnPaidInHallsEventsBuffets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Invoices",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                table: "Invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "BuffetId",
                table: "InvoiceItemizeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "InvoiceItemizeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HallId",
                table: "InvoiceItemizeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Halls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Buffets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemizeds_BuffetId",
                table: "InvoiceItemizeds",
                column: "BuffetId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemizeds_EventId",
                table: "InvoiceItemizeds",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemizeds_HallId",
                table: "InvoiceItemizeds",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemizeds_Buffets_BuffetId",
                table: "InvoiceItemizeds",
                column: "BuffetId",
                principalTable: "Buffets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemizeds_Events_EventId",
                table: "InvoiceItemizeds",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemizeds_Halls_HallId",
                table: "InvoiceItemizeds",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemizeds_Buffets_BuffetId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemizeds_Events_EventId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemizeds_Halls_HallId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemizeds_BuffetId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemizeds_EventId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemizeds_HallId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropColumn(
                name: "BuffetId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "InvoiceItemizeds");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Buffets");

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Invoices",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
