using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagementSystem.Migrations
{
    public partial class UserAndWorkersRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_User_userid",
                table: "Worker");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Worker");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Worker",
                newName: "roleid");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_userid",
                table: "Worker",
                newName: "IX_Worker_roleid");

            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Role_roleid",
                table: "Worker",
                column: "roleid",
                principalTable: "Role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Role_roleid",
                table: "Worker");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropColumn(
                name: "login",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Worker");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "Worker",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_roleid",
                table: "Worker",
                newName: "IX_Worker_userid");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "Worker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    workerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_User_userid",
                table: "Worker",
                column: "userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
