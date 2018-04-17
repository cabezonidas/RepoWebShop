using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class discountsfixDiscountShoppingCart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingCartDiscount",
                columns: table => new
                {
                    ShoppingCartDiscountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiscountId = table.Column<int>(nullable: true),
                    ShoppingCartId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartDiscount", x => x.ShoppingCartDiscountId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartDiscount_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountId",
                table: "Orders",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartDiscount_DiscountId",
                table: "ShoppingCartDiscount",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ShoppingCartDiscount");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DiscountId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Orders");
        }
    }
}
