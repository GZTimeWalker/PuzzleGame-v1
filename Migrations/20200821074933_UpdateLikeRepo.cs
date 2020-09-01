using Microsoft.EntityFrameworkCore.Migrations;

namespace GZTimeServer.Migrations
{
    public partial class UpdateLikeRepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "LiveLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "LiveLikes");
        }
    }
}
