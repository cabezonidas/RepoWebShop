using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class catalogunified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PieDetailId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PieDetailId",
                table: "Products",
                column: "PieDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PieDetails_PieDetailId",
                table: "Products",
                column: "PieDetailId",
                principalTable: "PieDetails",
                principalColumn: "PieDetailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PieDetails_PieDetailId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PieDetailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PieDetailId",
                table: "Products");
        }
    }
}
