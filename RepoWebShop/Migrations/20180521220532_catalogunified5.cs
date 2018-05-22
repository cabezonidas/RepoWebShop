using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class catalogunified5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PieDetails");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "PieDetails");

            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "PieDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PieDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "PieDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PreparationTime",
                table: "PieDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}
