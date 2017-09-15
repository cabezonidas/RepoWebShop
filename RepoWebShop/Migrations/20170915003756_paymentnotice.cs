using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RepoWebShop.Migrations
{
    public partial class paymentnotice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentNotifications");

            migrationBuilder.CreateTable(
                name: "PaymentNotices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Area_Code = table.Column<string>(nullable: true),
                    Concept_Amount = table.Column<decimal>(nullable: false),
                    Currency_Id = table.Column<string>(nullable: true),
                    Date_Approved = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    First_Name = table.Column<string>(nullable: true),
                    Installment_Amount = table.Column<decimal>(nullable: true),
                    Installments = table.Column<int>(nullable: false),
                    Last_Name = table.Column<string>(nullable: true),
                    Merchant_Order_Id = table.Column<string>(nullable: true),
                    Money_Release_Date = table.Column<DateTime>(nullable: true),
                    Net_Received_Amount = table.Column<decimal>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Operation_Type = table.Column<string>(nullable: true),
                    Order_Code = table.Column<string>(nullable: true),
                    Order_Id = table.Column<string>(nullable: true),
                    Payment_Id = table.Column<string>(nullable: true),
                    Payment_Type = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Site_Id = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Status_Detail = table.Column<string>(nullable: true),
                    Total_Paid_Amount = table.Column<decimal>(nullable: false),
                    Transaction_Amount = table.Column<decimal>(nullable: false),
                    Transaction_Order_Id = table.Column<int>(nullable: false),
                    User_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentNotices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentNotices");

            migrationBuilder.CreateTable(
                name: "PaymentNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(nullable: true),
                    Api_Version = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: false),
                    Live_Mode = table.Column<bool>(nullable: false),
                    MercadoPagoId = table.Column<int>(nullable: false),
                    PaymentId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    User_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentNotifications", x => x.Id);
                });
        }
    }
}
