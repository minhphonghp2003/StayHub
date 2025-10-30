using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class menuactionrole1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionMenu");

            migrationBuilder.DropTable(
                name: "ActionRole");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "MenuAction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuAction_MenuId",
                table: "MenuAction",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuAction_Menu_MenuId",
                table: "MenuAction",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuAction_Menu_MenuId",
                table: "MenuAction");

            migrationBuilder.DropIndex(
                name: "IX_MenuAction_MenuId",
                table: "MenuAction");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MenuAction");

            migrationBuilder.CreateTable(
                name: "ActionMenu",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "integer", nullable: false),
                    MenusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionMenu", x => new { x.ActionsId, x.MenusId });
                    table.ForeignKey(
                        name: "FK_ActionMenu_Action_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "Action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionMenu_Menu_MenusId",
                        column: x => x.MenusId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionRole",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "integer", nullable: false),
                    RolesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRole", x => new { x.ActionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ActionRole_Action_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "Action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionMenu_MenusId",
                table: "ActionMenu",
                column: "MenusId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRole_RolesId",
                table: "ActionRole",
                column: "RolesId");
        }
    }
}
