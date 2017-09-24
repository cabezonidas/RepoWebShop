using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class shoppingCartRepository2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MercadoPhoneNumber",
                table: "PaymentNotices");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PaymentNotices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PaymentNotices");

            migrationBuilder.AddColumn<string>(
                name: "MercadoPhoneNumber",
                table: "PaymentNotices",
                nullable: true);
        }
    }
}
