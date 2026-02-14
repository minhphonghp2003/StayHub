using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class unitname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_CategoryItem_StatusId",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Property_PropertyId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_PropertyId",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Unit",
                newName: "UnitGroupId");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Unit",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_StatusId",
                table: "Unit",
                newName: "IX_Unit_UnitGroupId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Unit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UnitGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitGroup_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroup_PropertyId",
                table: "UnitGroup",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_UnitGroup_UnitGroupId",
                table: "Unit",
                column: "UnitGroupId",
                principalTable: "UnitGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_UnitGroup_UnitGroupId",
                table: "Unit");

            migrationBuilder.DropTable(
                name: "UnitGroup");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "UnitGroupId",
                table: "Unit",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Unit",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_UnitGroupId",
                table: "Unit",
                newName: "IX_Unit_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_PropertyId",
                table: "Unit",
                column: "PropertyId");

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
    }
}
