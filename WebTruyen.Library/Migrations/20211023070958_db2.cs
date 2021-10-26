using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTruyen.Library.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_CommentReplyId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "CommentReplyId",
                table: "Comment",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CommentReplyId",
                table: "Comment",
                newName: "IX_Comment_CommentId");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comment",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_CommentId",
                table: "Comment",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_CommentId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comment",
                newName: "CommentReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CommentId",
                table: "Comment",
                newName: "IX_Comment_CommentReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_CommentReplyId",
                table: "Comment",
                column: "CommentReplyId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
