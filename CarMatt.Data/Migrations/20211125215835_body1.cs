using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMatt.Data.Migrations
{
    public partial class body1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "BodyTypeId",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyTypeId",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
