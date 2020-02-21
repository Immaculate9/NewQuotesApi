using Microsoft.EntityFrameworkCore.Migrations;

namespace NewQuotesApi.Migrations
{
    public partial class QuotesUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Quotes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Quotes",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Quotes",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Quotes",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Quotes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quotes");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
