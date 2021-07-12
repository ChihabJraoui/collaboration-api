using Microsoft.EntityFrameworkCore.Migrations;

namespace Collaboration.ShareDocs.Persistence.Migrations
{
    public partial class AddFollowingModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Users_FollowerId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Users_FollowingId",
                table: "Follow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follow",
                table: "Follow");

            migrationBuilder.DropIndex(
                name: "IX_Follow_FollowingId",
                table: "Follow");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Follow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follow",
                table: "Follow",
                columns: new[] { "FollowingId", "FollowerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FollowerId",
                table: "Follow",
                column: "FollowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Users_FollowerId",
                table: "Follow",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Users_FollowingId",
                table: "Follow",
                column: "FollowingId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Users_FollowerId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Users_FollowingId",
                table: "Follow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follow",
                table: "Follow");

            migrationBuilder.DropIndex(
                name: "IX_Follow_FollowerId",
                table: "Follow");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Follow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follow",
                table: "Follow",
                columns: new[] { "FollowerId", "FollowingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FollowingId",
                table: "Follow",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Users_FollowerId",
                table: "Follow",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Users_FollowingId",
                table: "Follow",
                column: "FollowingId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
