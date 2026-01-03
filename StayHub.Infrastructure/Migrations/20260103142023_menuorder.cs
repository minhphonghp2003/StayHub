using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class menuorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Menu",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Menu");
        }
    }
}
