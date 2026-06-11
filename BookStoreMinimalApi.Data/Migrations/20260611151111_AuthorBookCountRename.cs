using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreMinimalApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorBookCountRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedBooks",
                table: "Authors",
                newName: "PublishedBooksCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedBooksCount",
                table: "Authors",
                newName: "PublishedBooks");
        }
    }
}
