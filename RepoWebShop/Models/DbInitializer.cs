﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            //AppDbContext context = serviceProvider.GetService<AppDbContext>();
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (!context.Holidays.Any())
                {
                    context.Holidays.Add(new PublicHoliday()
                    {
                        Date = new DateTime(2017, 10, 12),
                        OpenHours = new OpenHours() { DayId = 8, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(6, 0, 0) },
                        ProcessingHours = new ProcessingHours() { DayId = 8, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(6, 0, 0) }
                    });
                }

                if (!context.Vacations.Any())
                {
                    context.Vacations.Add(new Vacation()
                    {
                         StartDate = new DateTime(2018, 1, 10),
                         EndDate = new DateTime(2018, 1, 10)
                    });
                }


                if (!context.OpenHours.Any())
                {
                    context.OpenHours.Add(new OpenHours() { DayId = 2, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 2, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 3, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 3, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 4, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 4, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 5, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 5, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 6, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) });
                    context.OpenHours.Add(new OpenHours() { DayId = 0, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 30, 0) });
                }
                if (!context.ProcessingHours.Any())
                {
                    context.ProcessingHours.Add(new ProcessingHours() { DayId = 2, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) });
                    context.ProcessingHours.Add(new ProcessingHours() { DayId = 3, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) });
                    context.ProcessingHours.Add(new ProcessingHours() { DayId = 4, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) });
                    context.ProcessingHours.Add(new ProcessingHours() { DayId = 5, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) });
                    context.ProcessingHours.Add(new ProcessingHours() { DayId = 6, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) });
                    context.ProcessingHours.Add(new ProcessingHours() { DayId = 0, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 30, 0) });
                }

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(Categories.Select(c => c.Value));
                }

                if (!context.PieDetails.Any())
                {
                    context.PieDetails.AddRange(Piedetails.Select(c => c.Value));
                }

                if (!context.Pies.Any())
                {
                    context.AddRange
                    (
                        new Pie { Name = "Chica", Price = 240M, PieDetail = Piedetails["Frutal"], SizeDescription = "8 porciones" },
                        new Pie { Name = "Grande", Price = 310M, PieDetail = Piedetails["Frutal"], SizeDescription = "12 porciones" },
                        new Pie { Name = "Super Grande", Price = 420M, PieDetail = Piedetails["Frutal"], SizeDescription = "16 porciones" },
                        new Pie { Name = "Estandar", Price = 220M, PieDetail = Piedetails["Crumble"], SizeDescription = "8 porciones" },
                        new Pie { Name = "Chica", Price = 200M, PieDetail = Piedetails["Africa"], SizeDescription = "8 porciones" },
                        new Pie { Name = "Grande", Price = 250M, PieDetail = Piedetails["Africa"], SizeDescription = "12 porciones" },
                        new Pie { Name = "Super Grande", Price = 360M, PieDetail = Piedetails["Africa"], SizeDescription = "16 porciones" },
                        new Pie { Name = "Estandar", Price = 200M, PieDetail = Piedetails["Linzer"], SizeDescription = "8 porciones" },
                        new Pie { Name = "Estandar", Price = 250M, PieDetail = Piedetails["Italiana"], SizeDescription = "8 porciones" },
                        new Pie { Name = "Chica", Price = 220M, PieDetail = Piedetails["Pasta frola"], SizeDescription = "8 porciones" },
                        new Pie { Name = "250gr", Price = 120M, PieDetail = Piedetails["Masas"], SizeDescription = "4 personas" },
                        new Pie { Name = "Estandar", Price = 580M, PieDetail = Piedetails["Desayuno"], SizeDescription = "Individual" },
                        new Pie { Name = "Doble", Price = 580M, PieDetail = Piedetails["Desayuno"], SizeDescription = "2 personas" }
                    );
                }
                context.SaveChanges();
            }
            CreateAdminRoles(serviceProvider);
        }

        private static void CreateAdminRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Task<IdentityResult> roleResult;
            string username = "admin";

            //Check that there is an Administrator role and create if not
            Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
                roleResult.Wait();
            }

            //Check if the admin user exists and create it if not
            //Add to the Administrator role

            Task<ApplicationUser> admin = userManager.FindByNameAsync(username);
            admin.Wait();

            var password = "Lgaga132!";

            if (admin.Result == null)
            {
                ApplicationUser administrator = new ApplicationUser()
                {
                    UserName = username,
                    Email = "sebastian.scd@gmail.com",
                    PhoneNumber = "02102790126",
                    State="Wellington",
                    ZipCode="6022",
                    AddressLine1="7B What st",
                    LastName="Cabeza",
                    FirstName="Sebastian",
                    Password = password
                };
                Task<IdentityResult> newUser = userManager.CreateAsync(administrator, password);
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
                    newUserRole.Wait();
                }
            }
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var categoriesList = new Category[]
                    {
                        new Category { CategoryName = "Frutales" },
                        new Category { CategoryName = "Frios" },
                        new Category { CategoryName = "Secos" },
                        new Category { CategoryName = "Especiales" },
                        new Category { CategoryName = "De estacion" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category category in categoriesList)
                    {
                        categories.Add(category.CategoryName, category);
                    }
                }

                return categories;
            }
        }
        private static Dictionary<string, PieDetail> piedetails;
        public static Dictionary<string, PieDetail> Piedetails
        {
            get
            {
                if (piedetails == null)
                {
                    var piedetailsList = new PieDetail[]
                    {
                        new PieDetail { PreparationTime = 48, Name = "Frutal", ShortDescription = "Our famous apple pies!", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Frutales"], ImageUrl = "https://farm9.staticflickr.com/8303/7923305570_44190ba80c_b.jpg", InStock = true, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://farm9.staticflickr.com/8303/7923305570_44190ba80c_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 24, Name = "Crumble", ShortDescription = "You'll love it!", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Frutales"], ImageUrl = "https://farm9.staticflickr.com/8307/7923309964_4f49aa07d4_b.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8307/7923309964_4f49aa07d4_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 24, Name = "Africa", ShortDescription = "Plain cheese cake. Plain pleasure.", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Frios"], ImageUrl = "https://farm9.staticflickr.com/8321/7923319318_5359625e7d_b.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8321/7923319318_5359625e7d_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 24, Name = "Linzer", ShortDescription = "A summer classic!", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["De estacion"], ImageUrl = "https://farm9.staticflickr.com/8302/7923213448_f61e20d258_b.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8302/7923213448_f61e20d258_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 24, Name = "Italiana", ShortDescription = "Happy holidays with this pie!", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["De estacion"], ImageUrl = "https://farm9.staticflickr.com/8315/7923215680_dfa089680e_b.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8315/7923215680_dfa089680e_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 6, Name = "Pasta frola", ShortDescription = "A Christmas favorite", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Secos"], ImageUrl = "https://farm9.staticflickr.com/8299/7923219632_83939f152e_b.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8299/7923219632_83939f152e_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 4, Name = "Masas", ShortDescription = "Sweet as peach", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Secos"], ImageUrl = "https://farm9.staticflickr.com/8298/7923222124_78aa26b8fe_b.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8298/7923222124_78aa26b8fe_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 72, Name = "Desayuno", ShortDescription = "Our Halloween favorite", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Especiales"], ImageUrl = "https://farm9.staticflickr.com/8457/7923069204_63f9e03039_b.jpg", InStock = true, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://farm9.staticflickr.com/8457/7923069204_63f9e03039_n.jpg", AllergyInformation = "" },
                    };

                    piedetails = new Dictionary<string, PieDetail>();

                    foreach (PieDetail piedetail in piedetailsList)
                    {
                        piedetails.Add(piedetail.Name, piedetail);
                    }
                }

                return piedetails;
            }
        }
    }
}
