using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travelista.Data.Migrations
{
    public partial class modifedhotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessibleRoom_Number",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConnectingRoom_Number",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeluxeRoom_Number",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilyRoom_Number",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StandardRoom_Number",
                table: "Hotels",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessibleRoom_Number",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ConnectingRoom_Number",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "DeluxeRoom_Number",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "FamilyRoom_Number",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "StandardRoom_Number",
                table: "Hotels");
        }
    }
}
