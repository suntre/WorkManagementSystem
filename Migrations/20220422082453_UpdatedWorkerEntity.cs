using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagementSystem.Migrations
{
    public partial class UpdatedWorkerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "Worker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_userid",
                table: "Worker",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_User_userid",
                table: "Worker",
                column: "userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_User_userid",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_userid",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "Worker");
        }
    }
}
