using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class servicequantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ContractService");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Unit",
                newName: "IsActive");

            migrationBuilder.AddColumn<int>(
                name: "MaximumCustomer",
                table: "Unit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ContractService",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Job_UnitId",
                table: "Job",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Unit_UnitId",
                table: "Job",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Unit_UnitId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_UnitId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "MaximumCustomer",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ContractService");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Unit",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "Service",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ContractService",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
