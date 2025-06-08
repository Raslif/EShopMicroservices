using Basket.API.Models;
using MongoDB.Driver;

namespace Basket.API.Data
{
    public class BasketRepository : MongoDBService, IBasketRepository
    {
        private readonly IMongoCollection<BasketDocumentDTO> _basketCollection;

        public BasketRepository(IConfiguration config) : base(config)
        {
            _basketCollection = _database.GetCollection<BasketDocumentDTO>("Basket");
        }

        public async Task<BasketDocumentDTO> GetBasket(int userId, CancellationToken cancellationToken = default)
        {
            return await _basketCollection.Find(Builders<BasketDocumentDTO>.Filter.Eq(doc => doc.UserId, userId))
                                          .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<bool> StoreBasket(BasketDocumentDTO basket, CancellationToken cancellationToken = default)
        {
            var filter = Builders<BasketDocumentDTO>.Filter.Eq(doc => doc.UserId, basket.UserId);
            var options = new ReplaceOptions { IsUpsert = true }; // Insert if not exists, update if exists
            var result = await _basketCollection.ReplaceOneAsync(filter, basket, options, cancellationToken: cancellationToken);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteBasket(int userId, CancellationToken cancellationToken = default)
        {
            var result = await _basketCollection.DeleteOneAsync(Builders<BasketDocumentDTO>.Filter.Eq(doc => doc.UserId, userId), cancellationToken: cancellationToken);
            return result.IsAcknowledged;
        }
    }
}
