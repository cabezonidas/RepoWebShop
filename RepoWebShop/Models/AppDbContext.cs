using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RepoWebShop.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();

            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Order>()
        //        .HasIndex(p => new { p.BookingId })
        //        .IsUnique(true);
        //}


        public DbSet<ShoppingCartLunchItem> ShoppingCartLunchItems  { get; set; }
        public DbSet<ShoppingCartLunch> ShoppingCartLunch { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProcessingHours> ProcessingHours { get; set; }
        public DbSet<OpenHours> OpenHours { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<GalleryFlickrAlbum> GalleryFlickrAlbums { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<PieDetail> PieDetails { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCartComment> ShoppingCartComments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PaymentNotice> PaymentNotices { get; set; }
        public DbSet<ApplicationUser> Registrations { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<PublicHoliday> Holidays { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ShoppingCartData> ShoppingCartData { get; set; }
        public DbSet<ShoppingCartDiscount> ShoppingCartDiscount { get; set; }
    }
}
