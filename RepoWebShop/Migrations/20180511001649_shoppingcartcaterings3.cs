using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class shoppingcartcaterings3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLunch_Lunch_LunchId",
                table: "ShoppingCartLunch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartLunch",
                table: "ShoppingCartLunch");

            migrationBuilder.RenameTable(
                name: "ShoppingCartLunch",
                newName: "ShoppingCartCustomLunch");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartLunch_LunchId",
                table: "ShoppingCartCustomLunch",
                newName: "IX_ShoppingCartCustomLunch_LunchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartCustomLunch",
                table: "ShoppingCartCustomLunch",
                column: "ShoppingCartLunchId");

            migrationBuilder.CreateTable(
                name: "OrderCatering",
                columns: table => new
                {
                    OrderCateringId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    BookingId = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LunchId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCatering", x => x.OrderCateringId);
                    table.ForeignKey(
                        name: "FK_OrderCatering_Lunch_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunch",
                        principalColumn: "LunchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCatering_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderCatering_LunchId",
                table: "OrderCatering",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCatering_OrderId",
                table: "OrderCatering",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartCustomLunch_Lunch_LunchId",
                table: "ShoppingCartCustomLunch",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartCustomLunch_Lunch_LunchId",
                table: "ShoppingCartCustomLunch");

            migrationBuilder.DropTable(
                name: "OrderCatering");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartCustomLunch",
                table: "ShoppingCartCustomLunch");

            migrationBuilder.RenameTable(
                name: "ShoppingCartCustomLunch",
                newName: "ShoppingCartLunch");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartCustomLunch_LunchId",
                table: "ShoppingCartLunch",
                newName: "IX_ShoppingCartLunch_LunchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartLunch",
                table: "ShoppingCartLunch",
                column: "ShoppingCartLunchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLunch_Lunch_LunchId",
                table: "ShoppingCartLunch",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
