using RepoWebShop.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<PieDetail> PieDetails { get; set; }

        [Display(Name = "Nombre Genérico")]
        public int PieDetailId { get; set; }
        
        public int ProductId { get; set; }

        public PieDetail PieDetail { get; set; }

        [Display(Name = "Venta Online")]
        public bool IsOnSale { get; set; } = true;

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Elaboración (hs)")]
        [Required]
        public int PreparationTime { get; set; } = 4;

        [Display(Name = "Bocados / Porciones")]
        [Required]
        public int Portions { get; set; } = 8;

        [Display(Name = "Temperatura")]
        public string Temperature { get; set; } = "Ambiente";

        [Display(Name = "Ingredientes")]
        [Required]
        public string Ingredients { get; set; }

        [Display(Name = "Tamaño")]
        public string SizeDescription { get; set; }

        [Display(Name = "Variedad")]
        public string Flavour { get; set; }

        [Display(Name = "Cantidad Mínima")]
        [Required]
        public int MinOrderAmount { get; set; } = 1;

        [Display(Name = "Incrementos")]
        [Required]
        public int MultipleAmount { get; set; } = 1;

        [Display(Name = "Categoría")]
        [Required]
        public string Category { get; set; } = "Postre";

        [Display(Name = "Precio en Sucursal")]
        [Required]
        public decimal PriceInStore { get; set; } = 100;

        [Display(Name = "Precio Online")]
        [Required]
        public decimal Price { get; set; } = 100;

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }
    }
}
