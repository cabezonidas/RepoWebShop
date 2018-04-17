using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class discounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DurationDays = table.Column<int>(nullable: false),
                    InstancesLeft = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Loop = table.Column<int>(nullable: true),
                    Percentage = table.Column<int>(nullable: false),
                    Phrase = table.Column<string>(maxLength: 20, nullable: false),
                    Roof = table.Column<decimal>(nullable: false),
                    ValidFrom = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");
        }
    }
}
