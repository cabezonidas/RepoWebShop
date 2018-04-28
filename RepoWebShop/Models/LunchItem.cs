using RepoWebShop.Interfaces;

namespace RepoWebShop.Models
{
    public class LunchItem
    {
        public int LunchItemId { get; set; }
        public Lunch Lunch { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}