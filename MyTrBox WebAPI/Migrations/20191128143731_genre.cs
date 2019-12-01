using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrBoxWebAPI.Migrations
{
    public partial class genre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Song",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "Song",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: false),
                    ArtistId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Media = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Video_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_GenreId",
                table: "Song",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_ArtistId",
                table: "Video",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_GenreId",
                table: "Video",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Genre_GenreId",
                table: "Song",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Genre_GenreId",
                table: "Song");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Song_GenreId",
                table: "Song");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Song",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Song",
                newName: "SongId");
        }
    }
}
