using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pintea_Razvan_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publishers_PublisherID",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherID",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AuthorsID",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorsID",
                table: "Book",
                column: "AuthorsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_AuthorsID",
                table: "Book",
                column: "AuthorsID",
                principalTable: "Authors",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publishers_PublisherID",
                table: "Book",
                column: "PublisherID",
                principalTable: "Publishers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_AuthorsID",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publishers_PublisherID",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorsID",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorsID",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherID",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publishers_PublisherID",
                table: "Book",
                column: "PublisherID",
                principalTable: "Publishers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
