using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class uitgroupfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "UnitGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "UnitGroup",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
