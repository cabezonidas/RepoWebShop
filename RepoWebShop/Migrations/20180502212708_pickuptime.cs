using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class pickuptime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCartPickUpDates",
                columns: table => new
                {
                    ShoppingCartPickUpDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    To = table.Column<TimeSpan>(nullable: false),
                    UserSubmitted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartPickUpDates", x => x.ShoppingCartPickUpDateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartPickUpDates");
        }
    }
}
