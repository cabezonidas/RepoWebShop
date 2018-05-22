using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class catalogunified6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flavour",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MultipleAmount",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SizeDescription",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flavour",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MultipleAmount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SizeDescription",
                table: "Products");
        }
    }
}
