using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagementSystem.Migrations
{
    public partial class RefactorDTOs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "workerId",
                table: "Task",
                newName: "workerid");

            migrationBuilder.CreateIndex(
                name: "IX_Task_workerid",
                table: "Task",
                column: "workerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Worker_workerid",
                table: "Task",
                column: "workerid",
                principalTable: "Worker",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Worker_workerid",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_workerid",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "workerid",
                table: "Task",
                newName: "workerId");
        }
    }
}
