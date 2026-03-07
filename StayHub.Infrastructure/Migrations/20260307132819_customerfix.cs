using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class customerfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customer_GenderId",
                table: "Customer",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ProvinceId",
                table: "Customer",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_WardId",
                table: "Customer",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CategoryItem_GenderId",
                table: "Customer",
                column: "GenderId",
                principalTable: "CategoryItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Province_ProvinceId",
                table: "Customer",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Ward_WardId",
                table: "Customer",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CategoryItem_GenderId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Province_ProvinceId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Ward_WardId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_GenderId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ProvinceId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_WardId",
                table: "Customer");
        }
    }
}
