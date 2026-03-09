using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class servicercontract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Service",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_ContractId",
                table: "Service",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Contract_ContractId",
                table: "Service",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Contract_ContractId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_ContractId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Service");
        }
    }
}
