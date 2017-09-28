using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class fieldsrenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessingHours",
                table: "ProcessingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenHours",
                table: "OpenHours");

            migrationBuilder.DropColumn(
                name: "ProcessingHoursId",
                table: "ProcessingHours");

            migrationBuilder.DropColumn(
                name: "OpenHoursId",
                table: "OpenHours");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProcessingHours",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OpenHours",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessingHours",
                table: "ProcessingHours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenHours",
                table: "OpenHours",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessingHours",
                table: "ProcessingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenHours",
                table: "OpenHours");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProcessingHours");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OpenHours");

            migrationBuilder.AddColumn<int>(
                name: "ProcessingHoursId",
                table: "ProcessingHours",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "OpenHoursId",
                table: "OpenHours",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessingHours",
                table: "ProcessingHours",
                column: "ProcessingHoursId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenHours",
                table: "OpenHours",
                column: "OpenHoursId");
        }
    }
}
