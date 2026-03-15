using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class invoiceservcie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "InvoiceService");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "InvoiceService",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InvoiceService");

            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "InvoiceService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
