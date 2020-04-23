using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Migrations
{
    public partial class stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId1",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_ApplicationUserId1",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Book",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Book_ApplicationUserId",
                table: "Book",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId",
                table: "Book",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_ApplicationUserId",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Book",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Book",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_ApplicationUserId1",
                table: "Book",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId1",
                table: "Book",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
