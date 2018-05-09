using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class catalogitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderCatalogItems",
                columns: table => new
                {
                    OrderCatalogItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCatalogItems", x => x.OrderCatalogItemId);
                    table.ForeignKey(
                        name: "FK_OrderCatalogItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderCatalogItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartCatalogProducts",
                columns: table => new
                {
                    ShoppingCartCatalogItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: true),
                    ShoppingCartId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartCatalogProducts", x => x.ShoppingCartCatalogItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartCatalogProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderCatalogItems_OrderId",
                table: "OrderCatalogItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCatalogItems_ProductId",
                table: "OrderCatalogItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartCatalogProducts_ProductId",
                table: "ShoppingCartCatalogProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderCatalogItems");

            migrationBuilder.DropTable(
                name: "ShoppingCartCatalogProducts");
        }
    }
}
