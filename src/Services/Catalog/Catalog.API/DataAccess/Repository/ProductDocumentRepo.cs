using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models.DocumentModels;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Repository
{
    public class ProductDocumentRepo : MongoDBService, IProductDocumentRepo
    {
        private readonly IMongoCollection<ProductDocument> _productCollection;
        public ProductDocumentRepo(IConfiguration config) 
            : base(config) 
        {
            _productCollection = _database.GetCollection<ProductDocument>("Catalog");
        }
        public async Task<string> SaveProduct(ProductDocument productDocument)
        {
            await _productCollection.InsertOneAsync(productDocument);
            return $"{productDocument.Id}";
        }
    }
}
