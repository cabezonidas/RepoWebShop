using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class pagevisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PageVisits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageVisits_UserId",
                table: "PageVisits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_AspNetUsers_UserId",
                table: "PageVisits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_AspNetUsers_UserId",
                table: "PageVisits");

            migrationBuilder.DropIndex(
                name: "IX_PageVisits_UserId",
                table: "PageVisits");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PageVisits");
        }
    }
}
