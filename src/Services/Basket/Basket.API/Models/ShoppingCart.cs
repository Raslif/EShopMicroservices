namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public int UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal TotalPrice => Items != null && Items.Any() ? Items.Sum(x => x.Price * x.Quantity) : 0;

        public ShoppingCart(int userId)
        {
            UserId = userId;
        }

        //Required for Mapping
        public ShoppingCart()
        {
        }
    }
}
