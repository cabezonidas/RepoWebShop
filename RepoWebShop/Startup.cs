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
using RepoWebShop.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;

namespace RepoWebShop
{
	public class Startup
	{
		private IConfigurationRoot _config;
		private IHostingEnvironment _env;

		public Startup(IHostingEnvironment hostingEnvironment)
		{
			_env = hostingEnvironment;
			_config = new ConfigurationBuilder()
			.SetBasePath(hostingEnvironment.ContentRootPath)
			.AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true)
			.Build();
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
			services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
			services.AddAuthentication().AddCookie(opts =>
			{
				opts.SlidingExpiration = true;
				opts.ExpireTimeSpan = TimeSpan.FromDays(90);
				opts.Cookie.Name = "auth";
			});
			services.AddAuthentication().AddFacebook(options => { options.AppId = _config["FacebookAppId"]; options.AppSecret = _config["FacebookAppSecret"]; });
			services.AddAuthentication().AddGoogle(options => { options.ClientId = _config["GoogleAppId"]; options.ClientSecret = _config["GoogleAppSecret"]; });

			#region Account
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 4;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;
				options.User.RequireUniqueEmail = true;
			});
			#endregion
			#region Dependency Injection
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
			services.AddSingleton<IProductsCacheRepository, ProductsCacheRepository>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IMercadoPago, MercadoPago>();
			services.AddSingleton<IConfiguration>(_config);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped(sp => ShoppingCart.GetCart(sp));
			#endregion

			services.AddMvc(o => { o.Filters.Add<GlobalExceptionFilter>(); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddSpaStaticFiles(configuration => { configuration.RootPath = "wwwroot/dist"; });
			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "De las Artes API", Version = "v1" }); c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")); });

			services.AddAutoMapper();
			services.AddMemoryCache();
			services.AddDistributedMemoryCache();
			services.AddDistributedSqlServerCache(o => { o.ConnectionString = _config.GetConnectionString("DefaultConnection"); o.SchemaName = "dbo"; o.TableName = "ServerCache"; });
			services.AddSession();
			services.AddCors();
			return services.BuildServiceProvider();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
				app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
			}
			else
				app.UseExceptionHandler("/AppException");

			app.UseSession();
			app.UseAuthentication();

			app.UseStaticFiles();
			app.UseSpaStaticFiles();
			app.UseDefaultFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "categoryfilter", template: "Pie/{action}/{category?}", defaults: new { Controller = "Pie", action = "List" });
				routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "De las Artes API V1");
				c.RoutePrefix = "api";
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "wwwroot/dist";
				if (env.IsDevelopment())
					spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
			});
		}
	}
}
