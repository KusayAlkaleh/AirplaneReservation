using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    public partial class UpdateFlightsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_AirportId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "AirportId",
                table: "Flights",
                newName: "AirportID");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_AirportId",
                table: "Flights",
                newName: "IX_Flights_AirportID");

            migrationBuilder.AlterColumn<int>(
                name: "StartingPoint",
                table: "Flights",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ArrivingPoint",
                table: "Flights",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AirportID",
                table: "Flights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_AirportID",
                table: "Flights",
                column: "AirportID",
                principalTable: "Airports",
                principalColumn: "AirportID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_AirportID",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "AirportID",
                table: "Flights",
                newName: "AirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_AirportID",
                table: "Flights",
                newName: "IX_Flights_AirportId");

            migrationBuilder.AlterColumn<string>(
                name: "StartingPoint",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ArrivingPoint",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AirportId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_AirportId",
                table: "Flights",
                column: "AirportId",
                principalTable: "Airports",
                principalColumn: "AirportID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
