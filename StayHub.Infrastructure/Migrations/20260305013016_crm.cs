using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class crm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CCCD = table.Column<string>(type: "text", nullable: true),
                    GenderId = table.Column<int>(type: "integer", nullable: true),
                    ProvinceId = table.Column<int>(type: "integer", nullable: true),
                    WardId = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<int>(type: "integer", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Job = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_PropertyId",
                table: "Service",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PropertyId",
                table: "Job",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_PropertyId",
                table: "Asset",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Property_PropertyId",
                table: "Asset",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Property_PropertyId",
                table: "Job",
                column: "PropertyId",
                principalTable: "Property",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Property_PropertyId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Property_PropertyId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Property_PropertyId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Service_PropertyId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Job_PropertyId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Asset_PropertyId",
                table: "Asset");
        }
    }
}
