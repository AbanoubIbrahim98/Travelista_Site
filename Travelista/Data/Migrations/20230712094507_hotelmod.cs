using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travelista.Data.Migrations
{
    public partial class hotelmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessibleRoom_Number",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ConnectingRoom_Number",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "StandardRoom_Number",
                table: "Hotels",
                newName: "TripleRoom_Number");

            migrationBuilder.RenameColumn(
                name: "FamilyRoom_Number",
                table: "Hotels",
                newName: "SingleRoom_Number");

            migrationBuilder.RenameColumn(
                name: "DeluxeRoom_Number",
                table: "Hotels",
                newName: "DoubleRoom_Number");

            migrationBuilder.AlterColumn<int>(
                name: "Occupancy",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Bad_Type",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TripleRoom_Number",
                table: "Hotels",
                newName: "StandardRoom_Number");

            migrationBuilder.RenameColumn(
                name: "SingleRoom_Number",
                table: "Hotels",
                newName: "FamilyRoom_Number");

            migrationBuilder.RenameColumn(
                name: "DoubleRoom_Number",
                table: "Hotels",
                newName: "DeluxeRoom_Number");

            migrationBuilder.AlterColumn<int>(
                name: "Occupancy",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bad_Type",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
