using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            AppDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();

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
                    new Pie { Name = "Frutal Chica", Price = 240M, PieDetail = Piedetails["Frutal"] },
                    new Pie { Name = "Frutal Grande", Price = 310M, PieDetail = Piedetails["Frutal"] },
                    new Pie { Name = "Frutal Super Grande", Price = 420M, PieDetail = Piedetails["Frutal"] },
                    new Pie { Name = "Crumble", Price = 220M, PieDetail = Piedetails["Crumble"] },
                    new Pie { Name = "Africa Chica", Price = 200M, PieDetail = Piedetails["Africa"] },
                    new Pie { Name = "Africa Grande", Price = 250M, PieDetail = Piedetails["Africa"] },
                    new Pie { Name = "Africa Super Grande", Price = 360M, PieDetail = Piedetails["Africa"] },
                    new Pie { Name = "Linzer", Price = 200M, PieDetail = Piedetails["Linzer"] },
                    new Pie { Name = "Italiana", Price = 250M, PieDetail = Piedetails["Italiana"] },
                    new Pie { Name = "Pasta Frola", Price = 220M, PieDetail = Piedetails["Pasta frola"] },
                    new Pie { Name = "Masas", Price = 120M, PieDetail = Piedetails["Masas"] },
                    new Pie { Name = "Desayuno", Price = 580M, PieDetail = Piedetails["Desayuno"] }
                );
            }


            context.SaveChanges();
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
                        new PieDetail { PreparationTime = 24, Name = "Pasta frola", ShortDescription = "A Christmas favorite", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Secos"], ImageUrl = "https://farm9.staticflickr.com/8299/7923219632_83939f152e_b.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8299/7923219632_83939f152e_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 24, Name = "Masas", ShortDescription = "Sweet as peach", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Secos"], ImageUrl = "https://farm9.staticflickr.com/8298/7923222124_78aa26b8fe_b.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://farm9.staticflickr.com/8298/7923222124_78aa26b8fe_n.jpg", AllergyInformation = "" },
                        new PieDetail { PreparationTime = 24, Name = "Desayuno", ShortDescription = "Our Halloween favorite", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Especiales"], ImageUrl = "https://farm9.staticflickr.com/8457/7923069204_63f9e03039_b.jpg", InStock = true, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://farm9.staticflickr.com/8457/7923069204_63f9e03039_n.jpg", AllergyInformation = "" },
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
