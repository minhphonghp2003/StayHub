using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletenull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_Category_CategoryId",
                table: "CategoryItem");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_Category_CategoryId",
                table: "CategoryItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_Category_CategoryId",
                table: "CategoryItem");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_Category_CategoryId",
                table: "CategoryItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
