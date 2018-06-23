using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class afipdata4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuitDetails",
                columns: table => new
                {
                    CuitDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CuitId = table.Column<int>(nullable: true),
                    Property = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuitDetails", x => x.CuitDetailId);
                    table.ForeignKey(
                        name: "FK_CuitDetails_Cuits_CuitId",
                        column: x => x.CuitId,
                        principalTable: "Cuits",
                        principalColumn: "CuitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuitDetails_CuitId",
                table: "CuitDetails",
                column: "CuitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuitDetails");
        }
    }
}
