using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTruyen.Library.Migrations
{
    public partial class wt9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement");

            migrationBuilder.DropIndex(
                name: "IX_NewComicAnnouncement_IdChapter",
                table: "NewComicAnnouncement");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NewComicAnnouncement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement",
                columns: new[] { "IdChapter", "IdUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "NewComicAnnouncement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NewComicAnnouncement_IdChapter",
                table: "NewComicAnnouncement",
                column: "IdChapter");
        }
    }
}
