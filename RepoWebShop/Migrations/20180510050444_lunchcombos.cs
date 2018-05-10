using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class lunchcombos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attendants",
                table: "Lunch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ComboPrice",
                table: "Lunch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lunch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventDuration",
                table: "Lunch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCombo",
                table: "Lunch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PreparationTime",
                table: "Lunch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Lunch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attendants",
                table: "Lunch");

            migrationBuilder.DropColumn(
                name: "ComboPrice",
                table: "Lunch");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lunch");

            migrationBuilder.DropColumn(
                name: "EventDuration",
                table: "Lunch");

            migrationBuilder.DropColumn(
                name: "IsCombo",
                table: "Lunch");

            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "Lunch");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Lunch");
        }
    }
}
