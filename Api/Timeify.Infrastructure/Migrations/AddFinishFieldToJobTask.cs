using Microsoft.EntityFrameworkCore.Migrations;

namespace Timeify.Infrastructure.Migrations
{
    public partial class AddFinishFieldToJobTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "Finished",
                "JobTasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Finished",
                "JobTasks");
        }
    }
}