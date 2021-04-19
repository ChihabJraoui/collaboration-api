using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fixApplicationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsMembers_Members_MemberId",
                table: "ProjectsMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsMembers_projects_ProjectId",
                table: "ProjectsMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsMembers_Members_MemberId",
                table: "ProjectsMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsMembers_projects_ProjectId",
                table: "ProjectsMembers",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsMembers_Members_MemberId",
                table: "ProjectsMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsMembers_projects_ProjectId",
                table: "ProjectsMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsMembers_Members_MemberId",
                table: "ProjectsMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsMembers_projects_ProjectId",
                table: "ProjectsMembers",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
