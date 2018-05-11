using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class shoppingcartcaterings7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCatering_Lunch_LunchId",
                table: "OrderCatering");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCatering_Orders_OrderId",
                table: "OrderCatering");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCatering",
                table: "OrderCatering");

            migrationBuilder.RenameTable(
                name: "OrderCatering",
                newName: "OrderCaterings");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCatering_OrderId",
                table: "OrderCaterings",
                newName: "IX_OrderCaterings_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCatering_LunchId",
                table: "OrderCaterings",
                newName: "IX_OrderCaterings_LunchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCaterings",
                table: "OrderCaterings",
                column: "OrderCateringId");

            migrationBuilder.CreateTable(
                name: "ShoppingCartCaterings",
                columns: table => new
                {
                    ShoppingCartComboCateringId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    BookingId = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LunchId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartCaterings", x => x.ShoppingCartComboCateringId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartCaterings_Lunch_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunch",
                        principalColumn: "LunchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartCaterings_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartCaterings_LunchId",
                table: "ShoppingCartCaterings",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartCaterings_OrderId",
                table: "ShoppingCartCaterings",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCaterings_Lunch_LunchId",
                table: "OrderCaterings",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCaterings_Orders_OrderId",
                table: "OrderCaterings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCaterings_Lunch_LunchId",
                table: "OrderCaterings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCaterings_Orders_OrderId",
                table: "OrderCaterings");

            migrationBuilder.DropTable(
                name: "ShoppingCartCaterings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCaterings",
                table: "OrderCaterings");

            migrationBuilder.RenameTable(
                name: "OrderCaterings",
                newName: "OrderCatering");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCaterings_OrderId",
                table: "OrderCatering",
                newName: "IX_OrderCatering_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCaterings_LunchId",
                table: "OrderCatering",
                newName: "IX_OrderCatering_LunchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCatering",
                table: "OrderCatering",
                column: "OrderCateringId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCatering_Lunch_LunchId",
                table: "OrderCatering",
                column: "LunchId",
                principalTable: "Lunch",
                principalColumn: "LunchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCatering_Orders_OrderId",
                table: "OrderCatering",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
