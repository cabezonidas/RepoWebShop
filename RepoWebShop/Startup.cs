using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RepoWebShop.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using RepoWebShop.Interfaces;
using RepoWebShop.Repositories;
using React.AspNet;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace RepoWebShop
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true)
                .Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = _configurationRoot["FacebookAppId"];
                facebookOptions.AppSecret = _configurationRoot["FacebookAppSecret"];
                //facebookOptions.Scope.Add("user_birthday");
            });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = _configurationRoot["GoogleAppId"];
                googleOptions.ClientSecret = _configurationRoot["GoogleAppSecret"];
                //facebookOptions.Scope.Add("user_birthday");
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 4;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddTransient<IDeliveryRepository, DeliveryRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IPieRepository, PieRepository>();
            services.AddTransient<IPieDetailRepository, PieDetailRepository>();
            services.AddTransient<IPaymentNoticeRepository, PaymentNoticeRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IGalleryRepository, GalleryRepository>();
            services.AddTransient<ISmsRepository, SmsRepository>();
            services.AddTransient<ICalendarRepository, CalendarRepository>();
            services.AddSingleton<IFlickrRepository, FlickrRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMercadoPago, MercadoPago>();
            services.AddSingleton<IConfiguration>(_configurationRoot);
            services.AddScoped(sp => ShoppingCart.GetCart(sp));

            services.AddReact();

            services.AddMvc();

            services.AddAutoMapper();
            services.AddMemoryCache();
            services.AddSession();
            
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/AppException");
            }

            app.UseReact(config => { });

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "categoryfilter",
                    template: "Pie/{action}/{category?}",
                    defaults: new { Controller = "Pie", action = "List" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
