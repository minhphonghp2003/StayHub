using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class customerproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Customer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PropertyId",
                table: "Customer",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Property_PropertyId",
                table: "Customer",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Property_PropertyId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_PropertyId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Customer");
        }
    }
}
