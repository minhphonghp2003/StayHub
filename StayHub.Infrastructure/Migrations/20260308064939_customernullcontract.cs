using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class customernullcontract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_User_SaleId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CategoryItem_GenderId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Contract_ContractId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Province_ProvinceId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Unit_UnitId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_User_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Ward_WardId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UnitId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Customer");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_User_SaleId",
                table: "Contract",
                column: "SaleId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CategoryItem_GenderId",
                table: "Customer",
                column: "GenderId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Contract_ContractId",
                table: "Customer",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Province_ProvinceId",
                table: "Customer",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_User_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Ward_WardId",
                table: "Customer",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_User_SaleId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CategoryItem_GenderId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Contract_ContractId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Province_ProvinceId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_User_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Ward_WardId",
                table: "Customer");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Customer",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UnitId",
                table: "Customer",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_User_SaleId",
                table: "Contract",
                column: "SaleId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CategoryItem_GenderId",
                table: "Customer",
                column: "GenderId",
                principalTable: "CategoryItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Contract_ContractId",
                table: "Customer",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Province_ProvinceId",
                table: "Customer",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Unit_UnitId",
                table: "Customer",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_User_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Ward_WardId",
                table: "Customer",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id");
        }
    }
}
