using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrBoxWebAPI.Migrations
{
    public partial class songUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Song",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Media",
                table: "Song",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "Media",
                table: "Song");
        }
    }
}
