using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class removedemailid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Emails_EmailId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "EmailId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Emails_EmailId",
                table: "Orders",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "EmailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Emails_EmailId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "EmailId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Emails_EmailId",
                table: "Orders",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "EmailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
