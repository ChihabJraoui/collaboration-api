using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Collaboration.ShareDocs.Persistence.Migrations
{
    public partial class AddIndividualFixChatModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndivudialChats");

            migrationBuilder.CreateTable(
                name: "IndividualChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualChats_Users_FromId",
                        column: x => x.FromId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualChats_FromId",
                table: "IndividualChats",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualChats_UserId",
                table: "IndividualChats",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualChats");

            migrationBuilder.CreateTable(
                name: "IndivudialChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndivudialChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndivudialChats_Users_FromId",
                        column: x => x.FromId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndivudialChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndivudialChats_FromId",
                table: "IndivudialChats",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_IndivudialChats_UserId",
                table: "IndivudialChats",
                column: "UserId");
        }
    }
}
