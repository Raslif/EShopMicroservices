using MongoDB.Driver;

namespace Catalog.API.DataAccess.Repository
{
    public class MongoDBService(IConfiguration config)
    {
        protected readonly IMongoDatabase _database = new MongoClient(config.GetValue<string>("MongoDB:ConnectionString"))
                                                                     .GetDatabase(config.GetValue<string>("MongoDB:DatabaseName"));
    }
}
