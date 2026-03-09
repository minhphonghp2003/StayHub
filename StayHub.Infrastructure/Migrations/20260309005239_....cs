using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ContractService");

     
            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractService",
                table: "ContractService",
                columns: new[] { "ContractsId", "ServicesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContractService_Contract_ContractsId",
                table: "ContractService",
                column: "ContractsId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractService_Service_ServicesId",
                table: "ContractService",
                column: "ServicesId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractService_Contract_ContractsId",
                table: "ContractService");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractService_Service_ServicesId",
                table: "ContractService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractService",
                table: "ContractService");

            migrationBuilder.RenameColumn(
                name: "ServicesId",
                table: "ContractService",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "ContractsId",
                table: "ContractService",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_ContractService_ServicesId",
                table: "ContractService",
                newName: "IX_ContractService_ServiceId");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Service",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ContractService",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "ContractService",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractService",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ContractService",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractService",
                table: "ContractService",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ContractId",
                table: "Service",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractService_ContractId",
                table: "ContractService",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractService_Contract_ContractId",
                table: "ContractService",
                column: "ContractId",
                principalTable: "Contract",
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
                name: "FK_Service_Contract_ContractId",
                table: "Service",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id");
        }
    }
}
