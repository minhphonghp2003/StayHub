using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletetier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Tier_TierId",
                table: "Property");

            migrationBuilder.AlterColumn<int>(
                name: "TierId",
                table: "Property",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Tier_TierId",
                table: "Property",
                column: "TierId",
                principalTable: "Tier",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Tier_TierId",
                table: "Property");

            migrationBuilder.AlterColumn<int>(
                name: "TierId",
                table: "Property",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Tier_TierId",
                table: "Property",
                column: "TierId",
                principalTable: "Tier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
