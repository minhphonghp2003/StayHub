using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactorcrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Contract_ContractId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ContractId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FeeCategoryId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "VatTypeId",
                table: "Service",
                newName: "UnitTypeId");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "Service",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "Job",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Customer",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRepresentative",
                table: "Customer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Customer",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ContractService",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Asset",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Service_UnitTypeId",
                table: "Service",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OwnerId",
                table: "Notification",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ContractId",
                table: "Customer",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UnitId",
                table: "Customer",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractService_ServiceId",
                table: "ContractService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAsset_AssetId",
                table: "ContractAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_TypeId",
                table: "Asset",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_CategoryItem_TypeId",
                table: "Asset",
                column: "TypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractAsset_Asset_AssetId",
                table: "ContractAsset",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractService_Service_ServiceId",
                table: "ContractService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Contract_ContractId",
                table: "Customer",
                column: "ContractId",
                principalTable: "Contract",
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
                name: "FK_Notification_User_OwnerId",
                table: "Notification",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_CategoryItem_UnitTypeId",
                table: "Service",
                column: "UnitTypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_CategoryItem_TypeId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractAsset_Asset_AssetId",
                table: "ContractAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractService_Service_ServiceId",
                table: "ContractService");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Contract_ContractId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Unit_UnitId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_User_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_OwnerId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_CategoryItem_UnitTypeId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_UnitTypeId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Notification_OwnerId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ContractId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UnitId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_ContractService_ServiceId",
                table: "ContractService");

            migrationBuilder.DropIndex(
                name: "IX_ContractAsset_AssetId",
                table: "ContractAsset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_TypeId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IsRepresentative",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "UnitTypeId",
                table: "Service",
                newName: "VatTypeId");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeeCategoryId",
                table: "Service",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Service",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "Job",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "ContractService",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Asset",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ContractId",
                table: "User",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Contract_ContractId",
                table: "User",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id");
        }
    }
}
