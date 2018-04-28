using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class lunch3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomLunchItems");

            migrationBuilder.DropTable(
                name: "ShoppingCartLunchItems");

            migrationBuilder.DropTable(
                name: "CustomLunch");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "ShoppingCartLunch");

            migrationBuilder.DropColumn(
                name: "MiscellaneousDescription",
                table: "ShoppingCartLunch");

            migrationBuilder.DropColumn(
                name: "MiscellaneousPrice",
                table: "ShoppingCartLunch");

            migrationBuilder.AddColumn<int>(
                name: "LunchId",
                table: "ShoppingCartLunch",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lunch",
                columns: table => new
                {
                    LunchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lunch", x => x.LunchId);
                });

            migrationBuilder.CreateTable(
                name: "LunchItems",
                columns: table => new
                {
                    LunchItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LunchId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchItems", x => x.LunchItemId);
                    table.ForeignKey(
                        name: "FK_LunchItems_Lunch_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunch",
                        principalColumn: "LunchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LunchItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LunchMiscellanea",
                columns: table => new
                {
                    LunchMiscellaneousId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    LunchId = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchMiscellanea", x => x.LunchMiscellaneousId);
                    table.ForeignKey(
                        name: "FK_LunchMiscellanea_Lunch_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunch",
                        principalColumn: "LunchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLunch_LunchId",
                table: "ShoppingCartLunch",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchItems_LunchId",
                table: "LunchItems",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchItems_ProductId",
                table: "LunchItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchMiscellanea_LunchId",
                table: "LunchMiscellanea",
                column: "LunchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLunch_Lunch_LunchId",
                table: "ShoppingCartLunch",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLunch_Lunch_LunchId",
                table: "ShoppingCartLunch");

            migrationBuilder.DropTable(
                name: "LunchItems");

            migrationBuilder.DropTable(
                name: "LunchMiscellanea");

            migrationBuilder.DropTable(
                name: "Lunch");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartLunch_LunchId",
                table: "ShoppingCartLunch");

            migrationBuilder.DropColumn(
                name: "LunchId",
                table: "ShoppingCartLunch");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "ShoppingCartLunch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiscellaneousDescription",
                table: "ShoppingCartLunch",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MiscellaneousPrice",
                table: "ShoppingCartLunch",
                nullable: false,
                defaultValue: 0m);

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
                name: "ShoppingCartLunchItems",
                columns: table => new
                {
                    ShoppingCartLunchItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LunchShoppingCartLunchId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartLunchItems", x => x.ShoppingCartLunchItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartLunchItems_ShoppingCartLunch_LunchShoppingCartLunchId",
                        column: x => x.LunchShoppingCartLunchId,
                        principalTable: "ShoppingCartLunch",
                        principalColumn: "ShoppingCartLunchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartLunchItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLunchItems_LunchShoppingCartLunchId",
                table: "ShoppingCartLunchItems",
                column: "LunchShoppingCartLunchId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLunchItems_ProductId",
                table: "ShoppingCartLunchItems",
                column: "ProductId");
        }
    }
}
