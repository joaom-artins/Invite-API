using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buffets_Users_UserId",
                table: "Buffets");

            migrationBuilder.DropForeignKey(
                name: "FK_Cerimonialists_Users_UserId",
                table: "Cerimonialists");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Users_UserId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_UserId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Buffets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Halls",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Events",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_UserId",
                table: "Events",
                newName: "IX_Events_ServiceId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cerimonialists",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Cerimonialists_UserId",
                table: "Cerimonialists",
                newName: "IX_Cerimonialists_ServiceId");

            migrationBuilder.AddColumn<Guid>(
                name: "ServoceId",
                table: "Halls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Buffets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Halls_ServoceId",
                table: "Halls",
                column: "ServoceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buffets_Services_UserId",
                table: "Buffets",
                column: "UserId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cerimonialists_Services_ServiceId",
                table: "Cerimonialists",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Services_ServiceId",
                table: "Events",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Services_ServoceId",
                table: "Halls",
                column: "ServoceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buffets_Services_UserId",
                table: "Buffets");

            migrationBuilder.DropForeignKey(
                name: "FK_Cerimonialists_Services_ServiceId",
                table: "Cerimonialists");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Services_ServiceId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Services_ServoceId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_ServoceId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "ServoceId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Buffets");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Halls",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Events",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ServiceId",
                table: "Events",
                newName: "IX_Events_UserId");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Cerimonialists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cerimonialists_ServiceId",
                table: "Cerimonialists",
                newName: "IX_Cerimonialists_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Halls",
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
                name: "IX_Halls_UserId",
                table: "Halls",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buffets_Users_UserId",
                table: "Buffets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cerimonialists_Users_UserId",
                table: "Cerimonialists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Users_UserId",
                table: "Halls",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
