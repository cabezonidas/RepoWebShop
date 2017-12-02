using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ShoppingCartValidationNumber
    {
        public int ShoppingCartValidationNumberId { get; set; }
        public string ValidationNumber { get; set; }
        public string ShoppingCartId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Validated { get; set; }
    }
}
