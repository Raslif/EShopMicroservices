namespace Basket.API.Models
{
    public class BasketDocumentDTO
    {
        //[BsonId]
        //public ObjectId Id { get; set; }
        public int UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = [];
    }
}
