using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMatt.Data.Migrations
{
    public partial class feedbacks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "FeedBacks");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "FeedBacks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "FeedBacks");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FeedBacks",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "FeedBacks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
