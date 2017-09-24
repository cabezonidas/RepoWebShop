using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class shoppingCartRepository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "First_Name",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "Last_Name",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "Order_Code",
                table: "PaymentNotices");

            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoMail",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoName",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoTransaction",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoUsername",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPhoneNumber",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotal",
                table: "PaymentNotices",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "MercadoPagoMail",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "MercadoPagoName",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "MercadoPagoTransaction",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "MercadoPagoUsername",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "MercadoPhoneNumber",
                table: "PaymentNotices");

            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "PaymentNotices");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "First_Name",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Last_Name",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "PaymentNotices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Order_Code",
                table: "PaymentNotices",
                nullable: true);
        }
    }
}
