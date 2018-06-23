using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RepoWebShop.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Order>()
        //        .HasIndex(p => new { p.BookingId })
        //        .IsUnique(true);
        //}

        public DbSet<CuitDetail> CuitDetails { get; set; }
        public DbSet<Cuit> Cuits { get; set; }
        public DbSet<Cae> Caes { get; set; }
        public DbSet<InvoiceData> InvoiceData { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<ShoppingCartCuit> ShoppingCartCuits { get; set; }

        public DbSet<PrintQueue> PrintQueue { get; set; }
        public DbSet<SmsHistory> SmsHistory { get; set; }
        public DbSet<EmailMarketingTemplate> EmailMarketingTemplates { get; set; }
        public DbSet<EmailMarketingHistory> EmailMarketingHistory { get; set; }

        public DbSet<Unsubscribe> Unsubscribed { get; set; }

        public DbSet<Session> ServerCache { get; set; }

        public DbSet<OrderCatering> OrderCaterings { get; set; }
        public DbSet<ShoppingCartComboCatering> ShoppingCartCaterings { get; set; }
        public DbSet<ShoppingCartLunch> ShoppingCartCustomLunch { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<ShoppingCartComment> ShoppingCartComments { get; set; }
        public DbSet<ShoppingCartDiscount> ShoppingCartDiscount { get; set; }

        public DbSet<OrderCatalogItem> OrderCatalogItems { get; set; }
        public DbSet<ShoppingCartCatalogItem> ShoppingCartCatalogProducts { get; set; }

        public DbSet<ShoppingCartPickUpDate> ShoppingCartPickUpDates { get; set; }
        public DbSet<ShoppingCartData> ShoppingCartData { get; set; }
        public DbSet<ShoppingCartByIp> ShoppingCartByIp { get; set; }
        public DbSet<BookingRecord> BookingRecords { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AdminNotification> AdminNotifications { get; set; }
        public DbSet<SiteException> Exceptions { get; set; }
        public DbSet<PageVisit> PageVisits { get; set; }
        public DbSet<LunchMiscellaneous> LunchMiscellanea { get; set; }
        public DbSet<LunchItem> LunchItems  { get; set; }
        public DbSet<Lunch> Lunch { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProcessingHours> ProcessingHours { get; set; }
        public DbSet<OpenHours> OpenHours { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<GalleryFlickrAlbum> GalleryFlickrAlbums { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<PieDetail> PieDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PaymentNotice> PaymentNotices { get; set; }
        public DbSet<ApplicationUser> Registrations { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<PublicHoliday> Holidays { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
