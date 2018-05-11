using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class shoppingcartcaterings5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCatalogItems_Orders_OrderId",
                table: "OrderCatalogItems");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderCatalogItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCatalogItems_Orders_OrderId",
                table: "OrderCatalogItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCatalogItems_Orders_OrderId",
                table: "OrderCatalogItems");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderCatalogItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCatalogItems_Orders_OrderId",
                table: "OrderCatalogItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
