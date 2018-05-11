using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class shoppingcartcaterings6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCatering_Orders_OrderId",
                table: "OrderCatering");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderCatering",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCatering_Orders_OrderId",
                table: "OrderCatering",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCatering_Orders_OrderId",
                table: "OrderCatering");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderCatering",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCatering_Orders_OrderId",
                table: "OrderCatering",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
