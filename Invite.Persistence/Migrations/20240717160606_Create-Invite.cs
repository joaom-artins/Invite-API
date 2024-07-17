using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateInvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responsibles_Events_EventId",
                table: "Responsibles");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Responsibles",
                newName: "InviteId");

            migrationBuilder.RenameIndex(
                name: "IX_Responsibles_EventId",
                table: "Responsibles",
                newName: "IX_Responsibles_InviteId");

            migrationBuilder.CreateTable(
                name: "Invites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Acepted = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    LimitDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invites_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invites_EventId",
                table: "Invites",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibles_Invites_InviteId",
                table: "Responsibles",
                column: "InviteId",
                principalTable: "Invites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responsibles_Invites_InviteId",
                table: "Responsibles");

            migrationBuilder.DropTable(
                name: "Invites");

            migrationBuilder.RenameColumn(
                name: "InviteId",
                table: "Responsibles",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Responsibles_InviteId",
                table: "Responsibles",
                newName: "IX_Responsibles_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibles_Events_EventId",
                table: "Responsibles",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
