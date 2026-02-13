using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StayHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    BillingCycle = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ward",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ProvinceId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ward_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionTier",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "integer", nullable: false),
                    TiersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTier", x => new { x.ActionsId, x.TiersId });
                    table.ForeignKey(
                        name: "FK_ActionTier_Action_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "Action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionTier_Tier_TiersId",
                        column: x => x.TiersId,
                        principalTable: "Tier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    StartSubscriptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndSubscriptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubscriptionStatus = table.Column<int>(type: "integer", nullable: false),
                    LastPaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TierId = table.Column<int>(type: "integer", nullable: false),
                    WardId = table.Column<int>(type: "integer", nullable: true),
                    ProvinceId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Property_Tier_TierId",
                        column: x => x.TierId,
                        principalTable: "Tier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_Ward_WardId",
                        column: x => x.WardId,
                        principalTable: "Ward",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionTier_TiersId",
                table: "ActionTier",
                column: "TiersId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProvinceId",
                table: "Property",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_TierId",
                table: "Property",
                column: "TierId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_WardId",
                table: "Property",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Ward_ProvinceId",
                table: "Ward",
                column: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionTier");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Tier");

            migrationBuilder.DropTable(
                name: "Ward");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
