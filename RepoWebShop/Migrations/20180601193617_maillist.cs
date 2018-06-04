using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Migrations
{
    public partial class maillist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailMarketingTemplates",
                columns: table => new
                {
                    EmailMarketingTemplateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    EmailBody = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMarketingTemplates", x => x.EmailMarketingTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Unsubscribed",
                columns: table => new
                {
                    UnsubscribeId = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Unsubscribed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unsubscribed", x => x.UnsubscribeId);
                });

            migrationBuilder.CreateTable(
                name: "EmailMarketingHistory",
                columns: table => new
                {
                    EmailMarketingHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    EmailTemplateEmailMarketingTemplateId = table.Column<int>(nullable: true),
                    Sent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMarketingHistory", x => x.EmailMarketingHistoryId);
                    table.ForeignKey(
                        name: "FK_EmailMarketingHistory_EmailMarketingTemplates_EmailTemplateEmailMarketingTemplateId",
                        column: x => x.EmailTemplateEmailMarketingTemplateId,
                        principalTable: "EmailMarketingTemplates",
                        principalColumn: "EmailMarketingTemplateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailMarketingHistory_EmailTemplateEmailMarketingTemplateId",
                table: "EmailMarketingHistory",
                column: "EmailTemplateEmailMarketingTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailMarketingHistory");

            migrationBuilder.DropTable(
                name: "Unsubscribed");

            migrationBuilder.DropTable(
                name: "EmailMarketingTemplates");
        }
    }
}
