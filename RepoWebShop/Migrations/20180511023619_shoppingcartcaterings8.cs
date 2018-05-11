using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class shoppingcartcaterings8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartCaterings_Orders_OrderId",
                table: "ShoppingCartCaterings");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartCaterings_OrderId",
                table: "ShoppingCartCaterings");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShoppingCartCaterings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ShoppingCartCaterings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartCaterings_OrderId",
                table: "ShoppingCartCaterings",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartCaterings_Orders_OrderId",
                table: "ShoppingCartCaterings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
