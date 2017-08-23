using Microsoft.AspNetCore.Razor.TagHelpers;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.TagHelpers
{
    public class PiePriceCarouselTagHelper : TagHelper
    {
        public int PieDetailId { get; set; }

        public IEnumerable<Pie> Pies { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var carouselId = "PieDetail-" + PieDetailId;
            output.TagName = "div";
            output.Attributes.SetAttribute("id", carouselId);
            output.Attributes.SetAttribute("class", "carousel slide");
            output.Attributes.SetAttribute("style", "height:150px; margin:10px;");
            output.Content.SetContent(GetItemsHtml(Pies, carouselId));
        }

        private string GetItemsHtml(IEnumerable<Pie> pies, string carouselId)
        {
            string items = string.Empty;
            int itemsCount = 0;
            foreach (var pie in Pies)
            {
                var classItem = itemsCount == 0 ? "active item" : "item";
                items +=
                    $"<div class='{classItem}'>" +
                        "<div class='addToCart' style='text-align:center;'>" +
                            $"<h2><strong>${pie.Price}</strong></h2>" +
                            "<p class='button'>" +
                                $"<a class='btn btn-primary' asp-controller='ShoppingCart' asp-action='AddToShoppingCart' asp-route-pieId='{pie.PieId}'>" +
                                    "Comprar <span class='glyphicon glyphicon-shopping-cart'></span>" +
                                "</a>" +
                            "</p>" +
                            $"<h4>{pie.Name}</h4>" +
                        "</div>" +
                    "</div>";
                itemsCount++;
            }
            if (itemsCount > 1)
            {
                items +=
                    $"<a class='carousel-control left' href='#{carouselId}' data-slide='prev' style='background-image:none;'>" +
                        "<span class='glyphicon glyphicon-chevron-left' aria-hidden='true' style='color:darkseagreen'></span>" +
                    "</a>" +
                    $"<a class='carousel-control right' href='#{carouselId}' data-slide='next' style='background-image:none;'>" +
                        "<span class='glyphicon glyphicon-chevron-right' aria-hidden='true' style='color:darkseagreen'></span>" +
                    "</a>";
            }
            return items;
        }
    }
}