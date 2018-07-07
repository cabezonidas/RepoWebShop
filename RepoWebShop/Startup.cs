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
using RepoWebShop.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;

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

            services.AddAuthentication().Services.ConfigureApplicationCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(90);
            });

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

            services.AddTransient<IPrinterRepository, PrinterRepository>();
            services.AddTransient<IElectronicBillingRepository, ElectronicBillingRepository>();
            services.AddTransient<IMarketingRepository, MarketingRepository>();
            services.AddTransient<ILunchRepository, LunchRepository>();
            services.AddTransient<IDeliveryRepository, DeliveryRepository>();
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));

            services.AddMvc(o => { o.Filters.Add<GlobalExceptionFilter>(); })
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;

            //services.AddNodeServices();

            services.AddReact();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/dist";
            });

            services.AddAutoMapper();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddDistributedSqlServerCache(o =>
            {
                o.ConnectionString = _configurationRoot.GetConnectionString("DefaultConnection");
                o.SchemaName = "dbo";
                o.TableName = "ServerCache";
            });

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


            app.UseReact(config => {
                config
                    .AddScript("~/Scripts/LunchEstimate.jsx")
                    .AddScript("~/Scripts/LunchItem.jsx");
            });

            app.UseSession();
            app.UseAuthentication();

            //app.UseDefaultFiles();
            app.UseStaticFiles();

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

            if(!env.IsProduction())
            {
                  app.UseSpaStaticFiles();
                  app.UseSpa(spa =>
                  {
                      spa.Options.SourcePath = "wwwroot/dist";

                      //spa.UseSpaPrerendering(options =>
                      //{
                        //options.BootModulePath = $"wwwroot/dist/server.main.js";
                        //options.BootModuleBuilder = env.IsDevelopment()
                        //  ? new AngularCliBuilder(npmScript: "build-1:ssr")
                        //  : null;
                        //options.ExcludeUrls = new[] { "/sockjs-node" };
                      //});

                      if (env.IsDevelopment())
                      {
                          spa.UseAngularCliServer(npmScript: "start");
                          //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                      }
                  });
            }
        }
    }
}
