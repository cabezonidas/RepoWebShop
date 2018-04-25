namespace RepoWebShop.Models
{
    public class ShoppingCartLunchItem
    {
        public int ShoppingCartLunchItemId { get; set; }
        public ShoppingCartLunch ShoppingCartLunch { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}