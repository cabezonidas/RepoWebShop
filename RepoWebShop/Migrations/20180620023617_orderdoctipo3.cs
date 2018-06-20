using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class orderdoctipo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayerName",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "PayerName",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayerName",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerName",
                table: "Orders",
                nullable: true);
        }
    }
}
