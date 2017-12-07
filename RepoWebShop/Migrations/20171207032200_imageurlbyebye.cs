using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class imageurlbyebye : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrl",
                table: "PieDetails");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PieDetails");

            migrationBuilder.AddColumn<long>(
                name: "FlickrAlbumId",
                table: "PieDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlickrAlbumId",
                table: "PieDetails");

            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnailUrl",
                table: "PieDetails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PieDetails",
                nullable: false,
                defaultValue: "");
        }
    }
}
