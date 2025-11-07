using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class menugroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuGroupId",
                table: "Menu",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuGroupId",
                table: "Menu",
                column: "MenuGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_CategoryItem_MenuGroupId",
                table: "Menu",
                column: "MenuGroupId",
                principalTable: "CategoryItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_CategoryItem_MenuGroupId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuGroupId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuGroupId",
                table: "Menu");
        }
    }
}
