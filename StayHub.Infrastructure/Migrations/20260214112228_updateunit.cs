using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateunit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionStatus",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Property",
                newName: "TypeId");

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "Unit",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Unit",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Unit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Unit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionStatusId",
                table: "Property",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PropertyUser",
                columns: table => new
                {
                    PropertiesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyUser", x => new { x.PropertiesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PropertyUser_Property_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Unit_PropertyId",
                table: "Unit",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_StatusId",
                table: "Unit",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_SubscriptionStatusId",
                table: "Property",
                column: "SubscriptionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_TypeId",
                table: "Property",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUser_UsersId",
                table: "PropertyUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_CategoryItem_SubscriptionStatusId",
                table: "Property",
                column: "SubscriptionStatusId",
                principalTable: "CategoryItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_CategoryItem_TypeId",
                table: "Property",
                column: "TypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_CategoryItem_StatusId",
                table: "Unit",
                column: "StatusId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Property_PropertyId",
                table: "Unit",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_CategoryItem_SubscriptionStatusId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_CategoryItem_TypeId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_CategoryItem_StatusId",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Property_PropertyId",
                table: "Unit");

            migrationBuilder.DropTable(
                name: "PropertyUser");

            migrationBuilder.DropIndex(
                name: "IX_Unit_PropertyId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_StatusId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Property_SubscriptionStatusId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_TypeId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "SubscriptionStatusId",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Property",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionStatus",
                table: "Property",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
