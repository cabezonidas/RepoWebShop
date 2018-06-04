using RepoWebShop.Interfaces;

namespace RepoWebShop.Models
{
    public class LunchItem
    {
        public int LunchItemId { get; set; }
        public Lunch Lunch { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal SubTotal
        {
            get => ItemCount * Product.Price;
        }

        public decimal SubTotalInStore
        {
            get => ItemCount * Product.PriceInStore;
        }

        public int ItemCount
        {
            get => Quantity > 0 ? Product.MinOrderAmount + (Product.MultipleAmount * (Quantity - 1)) : 0;
        }
    }
}