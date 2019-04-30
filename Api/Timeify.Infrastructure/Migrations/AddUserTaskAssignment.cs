using Microsoft.EntityFrameworkCore.Migrations;

namespace Timeify.Infrastructure.Migrations
{
    public partial class AddUserTaskAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "UserName",
                "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                "AK_Users_UserName",
                "Users",
                "UserName");

            migrationBuilder.CreateTable(
                "JobTaskUserLink",
                table => new
                {
                    JobTaskId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTaskUserLink", x => new {x.JobTaskId, x.UserName});
                    table.ForeignKey(
                        "FK_JobTaskUserLink_JobTasks_JobTaskId",
                        x => x.JobTaskId,
                        "JobTasks",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_JobTaskUserLink_Users_UserName",
                        x => x.UserName,
                        "Users",
                        "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_JobTaskUserLink_UserName",
                "JobTaskUserLink",
                "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "JobTaskUserLink");

            migrationBuilder.DropUniqueConstraint(
                "AK_Users_UserName",
                "Users");

            migrationBuilder.AlterColumn<string>(
                "UserName",
                "Users",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}