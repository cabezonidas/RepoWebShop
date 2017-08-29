using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class ihcorderdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoMail",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPhoneNumber",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MercadoPagoMail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MercadoPagoName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MercadoPhoneNumber",
                table: "Orders");
        }
    }
}
