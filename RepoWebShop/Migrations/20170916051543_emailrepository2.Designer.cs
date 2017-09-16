using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RepoWebShop.Models;

namespace RepoWebShop.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170916051543_emailrepository2")]
    partial class emailrepository2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RepoWebShop.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<string>("Description");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RepoWebShop.Models.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bcc");

                    b.Property<string>("Body");

                    b.Property<string>("Cc");

                    b.Property<string>("Status");

                    b.Property<string>("Subject");

                    b.Property<string>("To");

                    b.HasKey("EmailId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("RepoWebShop.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookingId");

                    b.Property<string>("CustomerComments");

                    b.Property<string>("ManagementComments");

                    b.Property<string>("MercadoPagoMail");

                    b.Property<string>("MercadoPagoName");

                    b.Property<string>("MercadoPagoUsername");

                    b.Property<string>("MercadoPhoneNumber");

                    b.Property<DateTime>("OrderPlaced");

                    b.Property<decimal>("OrderTotal");

                    b.Property<DateTime?>("Payout");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime?>("PickUp");

                    b.Property<bool>("PickedUp");

                    b.Property<int>("PreparationTime");

                    b.Property<string>("RegistrationId");

                    b.Property<string>("Status");

                    b.HasKey("OrderId");

                    b.HasIndex("RegistrationId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RepoWebShop.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("OrderId");

                    b.Property<int>("PieId");

                    b.Property<decimal>("Price");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PieId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("RepoWebShop.Models.PaymentNotice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Area_Code");

                    b.Property<decimal>("Concept_Amount");

                    b.Property<string>("Currency_Id");

                    b.Property<DateTime?>("Date_Approved");

                    b.Property<DateTime?>("Date_Created");

                    b.Property<string>("Email");

                    b.Property<string>("Extension");

                    b.Property<string>("First_Name");

                    b.Property<decimal?>("Installment_Amount");

                    b.Property<int>("Installments");

                    b.Property<string>("Last_Name");

                    b.Property<string>("Merchant_Order_Id");

                    b.Property<DateTime?>("Money_Release_Date");

                    b.Property<decimal>("Net_Received_Amount");

                    b.Property<string>("Nickname");

                    b.Property<string>("Number");

                    b.Property<string>("Operation_Type");

                    b.Property<string>("Order_Code");

                    b.Property<string>("Order_Id");

                    b.Property<string>("Payment_Id");

                    b.Property<string>("Payment_Type");

                    b.Property<string>("Reason");

                    b.Property<string>("Site_Id");

                    b.Property<string>("Status");

                    b.Property<string>("Status_Detail");

                    b.Property<decimal>("Total_Paid_Amount");

                    b.Property<decimal>("Transaction_Amount");

                    b.Property<int>("Transaction_Order_Id");

                    b.Property<string>("User_Id");

                    b.HasKey("Id");

                    b.ToTable("PaymentNotices");
                });

            modelBuilder.Entity("RepoWebShop.Models.Pie", b =>
                {
                    b.Property<int>("PieId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Name");

                    b.Property<int?>("PieDetailId");

                    b.Property<decimal>("Price");

                    b.Property<string>("SizeDescription");

                    b.HasKey("PieId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PieDetailId");

                    b.ToTable("Pies");
                });

            modelBuilder.Entity("RepoWebShop.Models.PieDetail", b =>
                {
                    b.Property<int>("PieDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AllergyInformation");

                    b.Property<int>("CategoryId");

                    b.Property<string>("ImageThumbnailUrl");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("InStock");

                    b.Property<bool>("IsPieOfTheWeek");

                    b.Property<string>("LongDescription");

                    b.Property<string>("Name");

                    b.Property<int>("PreparationTime");

                    b.Property<string>("ShortDescription");

                    b.HasKey("PieDetailId");

                    b.HasIndex("CategoryId");

                    b.ToTable("PieDetails");
                });

            modelBuilder.Entity("RepoWebShop.Models.ShoppingCartComment", b =>
                {
                    b.Property<int>("ShoppingCartCommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("Created");

                    b.Property<string>("ShoppingCartId");

                    b.HasKey("ShoppingCartCommentId");

                    b.ToTable("ShoppingCartComments");
                });

            modelBuilder.Entity("RepoWebShop.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int?>("PieId");

                    b.Property<string>("ShoppingCartId");

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("PieId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("RepoWebShop.Models.Registration", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("AddressLine2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("State")
                        .HasMaxLength(10);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.ToTable("Registration");

                    b.HasDiscriminator().HasValue("Registration");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RepoWebShop.Models.Order", b =>
                {
                    b.HasOne("RepoWebShop.Models.Registration", "Registration")
                        .WithMany()
                        .HasForeignKey("RegistrationId");
                });

            modelBuilder.Entity("RepoWebShop.Models.OrderDetail", b =>
                {
                    b.HasOne("RepoWebShop.Models.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RepoWebShop.Models.Pie", "Pie")
                        .WithMany()
                        .HasForeignKey("PieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RepoWebShop.Models.Pie", b =>
                {
                    b.HasOne("RepoWebShop.Models.Category")
                        .WithMany("Pies")
                        .HasForeignKey("CategoryId");

                    b.HasOne("RepoWebShop.Models.PieDetail", "PieDetail")
                        .WithMany()
                        .HasForeignKey("PieDetailId");
                });

            modelBuilder.Entity("RepoWebShop.Models.PieDetail", b =>
                {
                    b.HasOne("RepoWebShop.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RepoWebShop.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("RepoWebShop.Models.Pie", "Pie")
                        .WithMany()
                        .HasForeignKey("PieId");
                });
        }
    }
}
