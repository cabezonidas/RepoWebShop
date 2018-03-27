using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class storePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StorePrice",
                table: "Pies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorePrice",
                table: "Pies");
        }
    }
}
