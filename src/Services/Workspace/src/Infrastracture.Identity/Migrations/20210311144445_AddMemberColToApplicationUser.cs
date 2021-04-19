using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastracture.Identity.Migrations
{
    public partial class AddMemberColToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MemberID",
                schema: "Identity",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Member",
                schema: "Identity",
                columns: table => new
                {
                    MemberID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_MemberID",
                schema: "Identity",
                table: "User",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Member_MemberID",
                schema: "Identity",
                table: "User",
                column: "MemberID",
                principalSchema: "Identity",
                principalTable: "Member",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Member_MemberID",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropTable(
                name: "Member",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_User_MemberID",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MemberID",
                schema: "Identity",
                table: "User");
        }
    }
}
