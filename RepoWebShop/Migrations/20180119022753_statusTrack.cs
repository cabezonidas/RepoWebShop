using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class statusTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PickUp",
                table: "Orders",
                newName: "PickUpTime");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PayedInStore",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Refunded",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PayedInStore",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Refunded",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PickUpTime",
                table: "Orders",
                newName: "PickUp");
        }
    }
}
