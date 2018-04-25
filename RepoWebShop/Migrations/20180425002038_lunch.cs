using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class lunch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCartLunch",
                columns: table => new
                {
                    ShoppingCartLunchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    MiscellaneousDescription = table.Column<string>(nullable: true),
                    MiscellaneousPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartLunch", x => x.ShoppingCartLunchId);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartLunchItems",
                columns: table => new
                {
                    ShoppingCartLunchItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ShoppingCartLunchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartLunchItems", x => x.ShoppingCartLunchItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartLunchItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartLunchItems_ShoppingCartLunch_ShoppingCartLunchId",
                        column: x => x.ShoppingCartLunchId,
                        principalTable: "ShoppingCartLunch",
                        principalColumn: "ShoppingCartLunchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLunchItems_ProductId",
                table: "ShoppingCartLunchItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLunchItems_ShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                column: "ShoppingCartLunchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartLunchItems");

            migrationBuilder.DropTable(
                name: "ShoppingCartLunch");
        }
    }
}
