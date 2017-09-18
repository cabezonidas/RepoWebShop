using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class AddPieDetailIdToPie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pies_PieDetails_PieDetailId",
                table: "Pies");

            migrationBuilder.DropIndex(
                name: "IX_Pies_PieDetailId",
                table: "Pies");

            migrationBuilder.AlterColumn<int>(
                name: "PieDetailId",
                table: "Pies",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Pies_PieDetailId",
                table: "Pies",
                column: "PieDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pies_PieDetails_PieDetailId",
                table: "Pies",
                column: "PieDetailId",
                principalTable: "PieDetails",
                principalColumn: "PieDetailId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pies_PieDetails_PieDetailId",
                table: "Pies");

            migrationBuilder.AlterColumn<int>(
                name: "PieDetailId",
                table: "Pies",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pies_PieDetails_PieDetailId",
                table: "Pies",
                column: "PieDetailId",
                principalTable: "PieDetails",
                principalColumn: "PieDetailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
