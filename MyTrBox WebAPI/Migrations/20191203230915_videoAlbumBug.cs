using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrBoxWebAPI.Migrations
{
    public partial class videoAlbumBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_SongAlbum_AlbumId",
                table: "Video");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_VideoAlbum_VideoAlbumId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_AlbumId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Video");

            migrationBuilder.AlterColumn<Guid>(
                name: "VideoAlbumId",
                table: "Video",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_VideoAlbum_VideoAlbumId",
                table: "Video",
                column: "VideoAlbumId",
                principalTable: "VideoAlbum",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_VideoAlbum_VideoAlbumId",
                table: "Video");

            migrationBuilder.AlterColumn<Guid>(
                name: "VideoAlbumId",
                table: "Video",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "AlbumId",
                table: "Video",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Video_AlbumId",
                table: "Video",
                column: "AlbumId");

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
    }
}
