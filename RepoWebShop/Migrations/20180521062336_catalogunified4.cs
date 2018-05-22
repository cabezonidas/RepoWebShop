using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class catalogunified4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieDetails_Categories_CategoryId",
                table: "PieDetails");

            migrationBuilder.DropIndex(
                name: "IX_PieDetails_CategoryId",
                table: "PieDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PieDetails_CategoryId",
                table: "PieDetails",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PieDetails_Categories_CategoryId",
                table: "PieDetails",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
