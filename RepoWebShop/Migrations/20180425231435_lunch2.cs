using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class lunch2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLunchItems_ShoppingCartLunch_ShoppingCartLunchId",
                table: "ShoppingCartLunchItems");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                newName: "LunchShoppingCartLunchId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartLunchItems_ShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                newName: "IX_ShoppingCartLunchItems_LunchShoppingCartLunchId");

            migrationBuilder.CreateTable(
                name: "CustomLunch",
                columns: table => new
                {
                    CustomLunchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    MiscellaneousDescription = table.Column<string>(nullable: true),
                    MiscellaneousPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomLunch", x => x.CustomLunchId);
                });

            migrationBuilder.CreateTable(
                name: "CustomLunchItems",
                columns: table => new
                {
                    CustomLunchItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LunchCustomLunchId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomLunchItems", x => x.CustomLunchItemId);
                    table.ForeignKey(
                        name: "FK_CustomLunchItems_CustomLunch_LunchCustomLunchId",
                        column: x => x.LunchCustomLunchId,
                        principalTable: "CustomLunch",
                        principalColumn: "CustomLunchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomLunchItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomLunchItems_LunchCustomLunchId",
                table: "CustomLunchItems",
                column: "LunchCustomLunchId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomLunchItems_ProductId",
                table: "CustomLunchItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLunchItems_ShoppingCartLunch_LunchShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                column: "LunchShoppingCartLunchId",
                principalTable: "ShoppingCartLunch",
                principalColumn: "ShoppingCartLunchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLunchItems_ShoppingCartLunch_LunchShoppingCartLunchId",
                table: "ShoppingCartLunchItems");

            migrationBuilder.DropTable(
                name: "CustomLunchItems");

            migrationBuilder.DropTable(
                name: "CustomLunch");

            migrationBuilder.RenameColumn(
                name: "LunchShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                newName: "ShoppingCartLunchId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartLunchItems_LunchShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                newName: "IX_ShoppingCartLunchItems_ShoppingCartLunchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLunchItems_ShoppingCartLunch_ShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                column: "ShoppingCartLunchId",
                principalTable: "ShoppingCartLunch",
                principalColumn: "ShoppingCartLunchId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
