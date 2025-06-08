using MongoDB.Driver;

namespace Basket.API.Data
{
    public class MongoDBService(IConfiguration config)
    {
        protected readonly IMongoDatabase _database = new MongoClient(config.GetValue<string>("BasketDB:ConnectionString"))
                                                                     .GetDatabase(config.GetValue<string>("BasketDB:DatabaseName"));
    }
}
