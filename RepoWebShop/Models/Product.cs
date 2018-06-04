using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name = "Nombre")] 
        public string Name { get; set; } //To be deprecated

        [Display(Name = "Descripción")]
        public string Description { get; set; } //To be deprecated

        public decimal? OldPrice { get; set; }
        public decimal? OldPriceInStore { get; set; }

        [Display(Name = "Precio Online")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; } = 100;

        [Display(Name = "Incrementos")]
        [Required]
        public int MultipleAmount { get; set; } = 1;

        [Display(Name = "Tamaño")]
        public string SizeDescription { get; set; }

        [Display(Name = "Variedad")]
        public string Flavour { get; set; }

        [Display(Name = "Precio en Tienda")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PriceInStore { get; set; } = 100;

        [Display(Name = "Categoría")]
        [Required]
        public string Category { get; set; }

        [Display(Name = "Temperatura")]
        public string Temperature { get; set; }

        [Display(Name = "Cantidad mínima")]
        [Required]
        [Range(1, int.MaxValue)]
        public int MinOrderAmount { get; set; } = 1;

        [Display(Name = "Bocados por unidad")]
        [Range(1, int.MaxValue)]
        [Required]
        public int Portions { get; set; } = 1;

        [Display(Name = "Tiempo de preparación")]
        [Range(0, int.MaxValue)]
        [Required]
        public int PreparationTime { get; set; } = 6;


        public bool IsActive { get; set; } = true;

        [Display(Name = "¿Se vende por internet?")]
        [Required]
        public bool IsOnSale { get; set; }

        [Display(Name = "¿Está destacado con fotos?")]
        public PieDetail PieDetail { get; set; }

        [Display(Name = "¿Está destacado con fotos?")]
        public int? PieDetailId { get; set; }

        [BindNever]
        public string DisplayName
        {
            get
            {
                if (PieDetail == null)
                    return Name;
                else
                {
                    var displayName = PieDetail.Name.TrimStart();
                    if (!String.IsNullOrEmpty(SizeDescription))
                        displayName += $" {SizeDescription}";
                    if (!String.IsNullOrEmpty(Flavour))
                        displayName += $" ({Flavour})";
                    return displayName;
                }
            }
        }

        [BindNever]
        public string DisplayDescription
        {
            get
            {
                if (PieDetail == null)
                    return Description;
                else
                {
                    return PieDetail.Ingredients;
                }
            }
        }
    }
}
