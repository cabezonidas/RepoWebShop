using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class shoppingcartlunchrename : Migration
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
