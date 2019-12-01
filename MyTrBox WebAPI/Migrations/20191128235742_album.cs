using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrBoxWebAPI.Migrations
{
    public partial class album : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AlbumId",
                table: "Video",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VideoAlbumId",
                table: "Video",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AlbumId",
                table: "Song",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SongAlbum",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArtistId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongAlbum_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "VideoAlbum",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArtistId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoAlbum_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_AlbumId",
                table: "Video",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_VideoAlbumId",
                table: "Video",
                column: "VideoAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumId",
                table: "Song",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_SongAlbum_ArtistId",
                table: "SongAlbum",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoAlbum_ArtistId",
                table: "VideoAlbum",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_SongAlbum_AlbumId",
                table: "Song",
                column: "AlbumId",
                principalTable: "SongAlbum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_SongAlbum_AlbumId",
                table: "Video",
                column: "AlbumId",
                principalTable: "SongAlbum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_VideoAlbum_VideoAlbumId",
                table: "Video",
                column: "VideoAlbumId",
                principalTable: "VideoAlbum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_SongAlbum_AlbumId",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_SongAlbum_AlbumId",
                table: "Video");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_VideoAlbum_VideoAlbumId",
                table: "Video");

            migrationBuilder.DropTable(
                name: "SongAlbum");

            migrationBuilder.DropTable(
                name: "VideoAlbum");

            migrationBuilder.DropIndex(
                name: "IX_Video_AlbumId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_VideoAlbumId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Song_AlbumId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "VideoAlbumId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Song");
        }
    }
}
