using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class discountbottom3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loop",
                table: "Discounts");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Discounts",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "Weekly",
                table: "Discounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weekly",
                table: "Discounts");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Discounts",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Loop",
                table: "Discounts",
                nullable: true);
        }
    }
}
