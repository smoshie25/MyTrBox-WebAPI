using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrBoxWebAPI.Migrations
{
    public partial class Songs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Genre_GenreId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artists_ArtistId",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artists",
                table: "Artists");

            migrationBuilder.RenameTable(
                name: "Artists",
                newName: "Artist");

            migrationBuilder.RenameIndex(
                name: "IX_Artists_GenreId",
                table: "Artist",
                newName: "IX_Artist_GenreId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenreId",
                table: "Artist",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_Genre_GenreId",
                table: "Artist",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artist_Genre_GenreId",
                table: "Artist");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.RenameTable(
                name: "Artist",
                newName: "Artists");

            migrationBuilder.RenameIndex(
                name: "IX_Artist_GenreId",
                table: "Artists",
                newName: "IX_Artists_GenreId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenreId",
                table: "Artists",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artists",
                table: "Artists",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Genre_GenreId",
                table: "Artists",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artists_ArtistId",
                table: "Song",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
