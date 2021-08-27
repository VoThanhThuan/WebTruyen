using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTruyen.Library.Migrations
{
    public partial class wt8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "NewComicAnnouncement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NewComicAnnouncement_IdUser",
                table: "NewComicAnnouncement",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement");

            migrationBuilder.DropIndex(
                name: "IX_NewComicAnnouncement_IdUser",
                table: "NewComicAnnouncement");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "NewComicAnnouncement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewComicAnnouncement",
                table: "NewComicAnnouncement",
                columns: new[] { "IdUser", "IdChapter" });
        }
    }
}
