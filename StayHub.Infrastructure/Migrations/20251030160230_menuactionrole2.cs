using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class menuactionrole2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuAction_Role_RoleId",
                table: "MenuAction");

            migrationBuilder.DropIndex(
                name: "IX_MenuAction_RoleId",
                table: "MenuAction");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "MenuAction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "MenuAction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuAction_RoleId",
                table: "MenuAction",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuAction_Role_RoleId",
                table: "MenuAction",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
