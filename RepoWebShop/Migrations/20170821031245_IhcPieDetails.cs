using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RepoWebShop.Migrations
{
    public partial class IhcPieDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pies_Categories_CategoryId",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "AllergyInformation",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrl",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "IsPieOfTheWeek",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Pies");

            migrationBuilder.CreateTable(
                name: "PieDetails",
                columns: table => new
                {
                    PieDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllergyInformation = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    ImageThumbnailUrl = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    InStock = table.Column<bool>(nullable: false),
                    IsPieOfTheWeek = table.Column<bool>(nullable: false),
                    LongDescription = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PreparationTime = table.Column<int>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieDetails", x => x.PieDetailId);
                    table.ForeignKey(
                        name: "FK_PieDetails_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(
                name: "PieDetailId",
                table: "Pies",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Pies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pies_PieDetailId",
                table: "Pies",
                column: "PieDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PieDetails_CategoryId",
                table: "PieDetails",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pies_Categories_CategoryId",
                table: "Pies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pies_PieDetails_PieDetailId",
                table: "Pies",
                column: "PieDetailId",
                principalTable: "PieDetails",
                principalColumn: "PieDetailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pies_Categories_CategoryId",
                table: "Pies");

            migrationBuilder.DropForeignKey(
                name: "FK_Pies_PieDetails_PieDetailId",
                table: "Pies");

            migrationBuilder.DropIndex(
                name: "IX_Pies_PieDetailId",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "PieDetailId",
                table: "Pies");

            migrationBuilder.DropTable(
                name: "PieDetails");

            migrationBuilder.AddColumn<string>(
                name: "AllergyInformation",
                table: "Pies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnailUrl",
                table: "Pies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pies",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Pies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPieOfTheWeek",
                table: "Pies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Pies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreparationTime",
                table: "Pies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Pies",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Pies",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Pies_Categories_CategoryId",
                table: "Pies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
