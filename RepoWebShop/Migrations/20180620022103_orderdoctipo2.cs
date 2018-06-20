using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class orderdoctipo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderNumber",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderType",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerIdNumber",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerIdType",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerName",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderType",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerIdNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerIdType",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "CardHolderNumber",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "CardHolderType",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "PayerIdNumber",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "PayerIdType",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "PayerName",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardHolderNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardHolderType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PayerIdNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PayerIdType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PayerName",
                table: "Orders");
        }
    }
}
