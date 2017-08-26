using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ShoppingCartComment
    {
        public int ShoppingCartCommentId { get; set; }
        public string Comments { get; set; }
        public string ShoppingCartId { get; set; }
        public DateTime Created { get; set; }
    }
}
