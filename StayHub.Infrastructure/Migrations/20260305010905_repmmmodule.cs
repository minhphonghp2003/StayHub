using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class repmmmodule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_CategoryItem_TypeId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Property_PropertyId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Unit_UnitId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Property_PropertyId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_CategoryItem_FeeCategoryId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_CategoryItem_TypeId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_CategoryItem_VatTypeId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Property_PropertyId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_FeeCategoryId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_PropertyId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TypeId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_VatTypeId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Job_PropertyId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Asset_PropertyId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_TypeId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_UnitId",
                table: "Asset");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Service_FeeCategoryId",
                table: "Service",
                column: "FeeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_PropertyId",
                table: "Service",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TypeId",
                table: "Service",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_VatTypeId",
                table: "Service",
                column: "VatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PropertyId",
                table: "Job",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_PropertyId",
                table: "Asset",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_TypeId",
                table: "Asset",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_UnitId",
                table: "Asset",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_CategoryItem_TypeId",
                table: "Asset",
                column: "TypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Property_PropertyId",
                table: "Asset",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Unit_UnitId",
                table: "Asset",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Property_PropertyId",
                table: "Job",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_CategoryItem_FeeCategoryId",
                table: "Service",
                column: "FeeCategoryId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_CategoryItem_TypeId",
                table: "Service",
                column: "TypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_CategoryItem_VatTypeId",
                table: "Service",
                column: "VatTypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Property_PropertyId",
                table: "Service",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
