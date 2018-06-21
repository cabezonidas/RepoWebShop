using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class afipdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceData",
                columns: table => new
                {
                    InvoiceDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CantReg = table.Column<int>(nullable: false),
                    CbteTipo = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Cuit = table.Column<long>(nullable: false),
                    FchProceso = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    PtoVta = table.Column<int>(nullable: false),
                    Reproceso = table.Column<string>(nullable: true),
                    Resultado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceData", x => x.InvoiceDataId);
                    table.ForeignKey(
                        name: "FK_InvoiceData_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Caes",
                columns: table => new
                {
                    CaeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CAE = table.Column<string>(nullable: true),
                    CAEFchVto = table.Column<string>(nullable: true),
                    CbteDesde = table.Column<long>(nullable: false),
                    CbteFch = table.Column<string>(nullable: true),
                    CbteHasta = table.Column<long>(nullable: false),
                    Concepto = table.Column<int>(nullable: false),
                    DocNro = table.Column<long>(nullable: false),
                    DocTipo = table.Column<int>(nullable: false),
                    InvoiceDataId = table.Column<int>(nullable: true),
                    Resultado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caes", x => x.CaeId);
                    table.ForeignKey(
                        name: "FK_Caes_InvoiceData_InvoiceDataId",
                        column: x => x.InvoiceDataId,
                        principalTable: "InvoiceData",
                        principalColumn: "InvoiceDataId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    InvoiceDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    InvoiceDataId = table.Column<int>(nullable: false),
                    Msg = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceDetailId);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_InvoiceData_InvoiceDataId",
                        column: x => x.InvoiceDataId,
                        principalTable: "InvoiceData",
                        principalColumn: "InvoiceDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caes_InvoiceDataId",
                table: "Caes",
                column: "InvoiceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceData_OrderId",
                table: "InvoiceData",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceDataId",
                table: "InvoiceDetails",
                column: "InvoiceDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caes");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "InvoiceData");
        }
    }
}
