using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bgservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Invoice",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DownloadedContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadedContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InOutCome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    Payer = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContractId = table.Column<int>(type: "integer", nullable: true),
                    IsRepeatMonthly = table.Column<bool>(type: "boolean", nullable: false),
                    IsOutCome = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InOutCome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InOutCome_CategoryItem_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "CategoryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InOutCome_CategoryItem_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CategoryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InOutCome_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InOutCome_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InOutCome_ContractId",
                table: "InOutCome",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_InOutCome_PaymentMethodId",
                table: "InOutCome",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_InOutCome_PropertyId",
                table: "InOutCome",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_InOutCome_TypeId",
                table: "InOutCome",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadedContent");

            migrationBuilder.DropTable(
                name: "InOutCome");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invoice");
        }
    }
}
