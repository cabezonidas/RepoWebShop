using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class UserCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LunchItems_Lunch_LunchId",
                table: "LunchItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LunchItems_Products_ProductId",
                table: "LunchItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LunchMiscellanea_Lunch_LunchId",
                table: "LunchMiscellanea");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartCatalogProducts_Products_ProductId",
                table: "ShoppingCartCatalogProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartCustomLunch_Lunch_LunchId",
                table: "ShoppingCartCustomLunch");

            migrationBuilder.AlterColumn<int>(
                name: "LunchId",
                table: "ShoppingCartCustomLunch",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShoppingCartCatalogProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LunchId",
                table: "LunchMiscellanea",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "LunchItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LunchId",
                table: "LunchItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_LunchItems_Lunch_LunchId",
                table: "LunchItems",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LunchItems_Products_ProductId",
                table: "LunchItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LunchMiscellanea_Lunch_LunchId",
                table: "LunchMiscellanea",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartCatalogProducts_Products_ProductId",
                table: "ShoppingCartCatalogProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartCustomLunch_Lunch_LunchId",
                table: "ShoppingCartCustomLunch",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LunchItems_Lunch_LunchId",
                table: "LunchItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LunchItems_Products_ProductId",
                table: "LunchItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LunchMiscellanea_Lunch_LunchId",
                table: "LunchMiscellanea");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartCatalogProducts_Products_ProductId",
                table: "ShoppingCartCatalogProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartCustomLunch_Lunch_LunchId",
                table: "ShoppingCartCustomLunch");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "LunchId",
                table: "ShoppingCartCustomLunch",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShoppingCartCatalogProducts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LunchId",
                table: "LunchMiscellanea",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "LunchItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LunchId",
                table: "LunchItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LunchItems_Lunch_LunchId",
                table: "LunchItems",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LunchItems_Products_ProductId",
                table: "LunchItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LunchMiscellanea_Lunch_LunchId",
                table: "LunchMiscellanea",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartCatalogProducts_Products_ProductId",
                table: "ShoppingCartCatalogProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartCustomLunch_Lunch_LunchId",
                table: "ShoppingCartCustomLunch",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
