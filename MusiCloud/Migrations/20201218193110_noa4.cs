using Microsoft.EntityFrameworkCore.Migrations;

namespace MusiCloud.Migrations
{
    public partial class noa4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressName",
                table: "Concert",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Concert",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Concert",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Long",
                table: "Concert",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressName",
                table: "Concert");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Concert");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Concert");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Concert");
        }
    }
}
