using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoWebShop.Migrations
{
    public partial class alot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "PieDetails",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PieDetails",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "LongDescription",
                table: "PieDetails",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "PieDetails",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImageThumbnailUrl",
                table: "PieDetails",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "SizeDescription",
                table: "Pies",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pies",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "PieDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PieDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LongDescription",
                table: "PieDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "PieDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageThumbnailUrl",
                table: "PieDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SizeDescription",
                table: "Pies",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pies",
                nullable: true);
        }
    }
}
