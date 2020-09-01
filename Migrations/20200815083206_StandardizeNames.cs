using Microsoft.EntityFrameworkCore.Migrations;

namespace GZTimeServer.Migrations
{
    public partial class StandardizeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_codeKeys",
                table: "codeKeys");

            migrationBuilder.RenameTable(
                name: "codeKeys",
                newName: "CodeKeys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodeKeys",
                table: "CodeKeys",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CodeKeys",
                table: "CodeKeys");

            migrationBuilder.RenameTable(
                name: "CodeKeys",
                newName: "codeKeys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codeKeys",
                table: "codeKeys",
                column: "ID");
        }
    }
}
