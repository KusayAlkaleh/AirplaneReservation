using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    public partial class UpdatingAirportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IATACode",
                table: "Airports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IATACode",
                table: "Airports");
        }
    }
}
