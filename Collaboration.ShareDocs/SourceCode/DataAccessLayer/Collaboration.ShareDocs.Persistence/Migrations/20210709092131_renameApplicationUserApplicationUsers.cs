using Microsoft.EntityFrameworkCore.Migrations;

namespace Collaboration.ShareDocs.Persistence.Migrations
{
    public partial class renameApplicationUserApplicationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowingsId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowsId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserApplicationUser",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserApplicationUser_FollowsId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.RenameColumn(
                name: "FollowsId",
                table: "ApplicationUserApplicationUser",
                newName: "FollowersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserApplicationUser",
                table: "ApplicationUserApplicationUser",
                columns: new[] { "FollowersId", "FollowingsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplicationUser_FollowingsId",
                table: "ApplicationUserApplicationUser",
                column: "FollowingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowersId",
                table: "ApplicationUserApplicationUser",
                column: "FollowersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowingsId",
                table: "ApplicationUserApplicationUser",
                column: "FollowingsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowersId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowingsId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserApplicationUser",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserApplicationUser_FollowingsId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.RenameColumn(
                name: "FollowersId",
                table: "ApplicationUserApplicationUser",
                newName: "FollowsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserApplicationUser",
                table: "ApplicationUserApplicationUser",
                columns: new[] { "FollowingsId", "FollowsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplicationUser_FollowsId",
                table: "ApplicationUserApplicationUser",
                column: "FollowsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowingsId",
                table: "ApplicationUserApplicationUser",
                column: "FollowingsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserApplicationUser_Users_FollowsId",
                table: "ApplicationUserApplicationUser",
                column: "FollowsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
