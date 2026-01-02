using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class autoincrease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Menu_MenuId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Menu");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Menu_ParentId",
                table: "Menu",
                column: "ParentId",
                principalTable: "Menu",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Menu_ParentId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_ParentId",
                table: "Menu");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Menu",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuId",
                table: "Menu",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Menu_MenuId",
                table: "Menu",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");
        }
    }
}
