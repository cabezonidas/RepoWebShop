using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class errorhandling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "PageVisits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StuckTrace",
                table: "Exceptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "PageVisits");

            migrationBuilder.DropColumn(
                name: "StuckTrace",
                table: "Exceptions");
        }
    }
}
