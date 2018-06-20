using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class orderdoctipo4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComprobanteTipo",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DocTipo",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DocNro",
                table: "Orders",
                newName: "Cuit");

            migrationBuilder.CreateTable(
                name: "ShoppingCartCuits",
                columns: table => new
                {
                    ShoppingCartCuitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cuit = table.Column<long>(nullable: false),
                    ShoppingCartId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartCuits", x => x.ShoppingCartCuitId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartCuits");

            migrationBuilder.RenameColumn(
                name: "Cuit",
                table: "Orders",
                newName: "DocNro");

            migrationBuilder.AddColumn<int>(
                name: "ComprobanteTipo",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocTipo",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }
    }
}
