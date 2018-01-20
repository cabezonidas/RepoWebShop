using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class statusTrack4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayedInStore",
                table: "Orders",
                newName: "PaymentReceived");

            migrationBuilder.AddColumn<bool>(
                name: "PaymentReceived",
                table: "PaymentNotices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentReceived",
                table: "PaymentNotices");

            migrationBuilder.RenameColumn(
                name: "PaymentReceived",
                table: "Orders",
                newName: "PayedInStore");
        }
    }
}
