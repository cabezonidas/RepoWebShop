using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class deliveryAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    DeliveryAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    DeliveryCost = table.Column<decimal>(nullable: false),
                    DeliveryInstructions = table.Column<string>(maxLength: 256, nullable: true),
                    Distance = table.Column<int>(nullable: false),
                    ShoppingCartId = table.Column<string>(nullable: true),
                    State = table.Column<string>(maxLength: 50, nullable: true),
                    StreetName = table.Column<string>(maxLength: 100, nullable: false),
                    StreetNumber = table.Column<string>(maxLength: 100, nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.DeliveryAddressId);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_UserId",
                table: "DeliveryAddresses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAddresses");
        }
    }
}
