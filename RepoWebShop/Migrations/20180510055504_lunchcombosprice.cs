using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class lunchcombosprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ComboPrice",
                table: "Lunch",
                defaultValue: 0m,
                oldDefaultValue: 0m,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ComboPrice",
                table: "Lunch",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
