using Microsoft.EntityFrameworkCore.Migrations;

namespace Polar.Migrations
{
    public partial class Markersentityupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Markers",
                type: "nvarchar(25)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Markers");
        }
    }
}
