using Microsoft.EntityFrameworkCore.Migrations;

namespace MusiCloud.Migrations
{
    public partial class ModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AristLink",
                table: "Artist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlbumLink",
                table: "Album",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AristLink",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "AlbumLink",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "User");
        }
    }
}
