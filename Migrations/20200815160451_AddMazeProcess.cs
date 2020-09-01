using Microsoft.EntityFrameworkCore.Migrations;

namespace GZTimeServer.Migrations
{
    public partial class AddMazeProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LevelProcesses",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnimeProcesses",
                newName: "ID");

            migrationBuilder.CreateTable(
                name: "MazeProcesses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    x = table.Column<int>(type: "int", nullable: false),
                    y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MazeProcesses", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MazeProcesses");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "LevelProcesses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AnimeProcesses",
                newName: "Id");
        }
    }
}
