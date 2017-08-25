using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class reservas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Orders",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RegistrationId",
                table: "Orders",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_RegistrationId",
                table: "Orders",
                column: "RegistrationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_RegistrationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RegistrationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "Orders");
        }
    }
}
