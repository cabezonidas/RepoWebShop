using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RepoWebShop.Migrations
{
    public partial class workinghours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MercadoPhoneNumber",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OpenHours",
                columns: table => new
                {
                    OpenHoursId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayId = table.Column<int>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    StartingAt = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenHours", x => x.OpenHoursId);
                });

            migrationBuilder.CreateTable(
                name: "ProcessingHours",
                columns: table => new
                {
                    ProcessingHoursId = table.Column<string>(nullable: false),
                    DayId = table.Column<int>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    StartingAt = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessingHours", x => x.ProcessingHoursId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenHours");

            migrationBuilder.DropTable(
                name: "ProcessingHours");

            migrationBuilder.AddColumn<string>(
                name: "MercadoPhoneNumber",
                table: "Orders",
                nullable: true);
        }
    }
}
