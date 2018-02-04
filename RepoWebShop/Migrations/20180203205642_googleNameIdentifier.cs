using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class googleNameIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameIdentifier",
                table: "AspNetUsers",
                newName: "GoogleNameIdentifier");

            migrationBuilder.AddColumn<string>(
                name: "FacebookNameIdentifier",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookNameIdentifier",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "GoogleNameIdentifier",
                table: "AspNetUsers",
                newName: "NameIdentifier");
        }
    }
}
