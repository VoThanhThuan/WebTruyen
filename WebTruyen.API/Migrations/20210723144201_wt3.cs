using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTruyen.API.Migrations
{
    public partial class wt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLink",
                table: "Page",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLink",
                table: "Page");
        }
    }
}
