using Microsoft.EntityFrameworkCore.Migrations;

namespace Timeify.Infrastructure.Migrations
{
    public partial class AddTAsks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_JobTasks_Jobs_JobEntityId",
                "JobTasks");

            migrationBuilder.AlterColumn<int>(
                "JobEntityId",
                "JobTasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_JobTasks_Jobs_JobEntityId",
                "JobTasks",
                "JobEntityId",
                "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_JobTasks_Jobs_JobEntityId",
                "JobTasks");

            migrationBuilder.AlterColumn<int>(
                "JobEntityId",
                "JobTasks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                "FK_JobTasks_Jobs_JobEntityId",
                "JobTasks",
                "JobEntityId",
                "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}