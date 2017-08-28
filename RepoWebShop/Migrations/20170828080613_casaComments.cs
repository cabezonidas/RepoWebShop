using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class casaComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CustomerComments",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagementComments",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerComments",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ManagementComments",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Orders",
                maxLength: 250,
                nullable: true);
        }
    }
}
