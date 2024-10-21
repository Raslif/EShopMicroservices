using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models.DocumentModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Repository
{
    // If the Document is strongly typed classes, then we can use  LINQ expressions
    //  otherwise, use this approach of MongoDB.Driver
    public class ProductDocumentRepo : MongoDBService, IProductDocumentRepo
    {
        private readonly IMongoCollection<ProductDocument> _productCollection;
        public ProductDocumentRepo(IConfiguration config) 
            : base(config) 
        {
            _productCollection = _database.GetCollection<ProductDocument>("Catalog");
        }

        public async Task<bool> CheckProductExists(ObjectId productId)
        {
            var filter = Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId);
            return await _productCollection.CountDocumentsAsync(filter) > 0;
        }

        public async Task<string> SaveProduct(ProductDocument productDocument)
        {
            await _productCollection.InsertOneAsync(productDocument);
            return $"{productDocument.Id}";
        }

        public async Task<List<ProductDocument>> GetAllProducts() => await _productCollection.Find(new BsonDocument()).ToListAsync();

        public async Task<ProductDocument> GetProductById(ObjectId productId)
        {
            var filter = Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId);
            return await _productCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<ProductDocument>> GetProductsByCategory(string category)
        {
            var filter = Builders<ProductDocument>.Filter.AnyEq(doc => doc.Category, category);
            return await _productCollection.Find(filter).ToListAsync();
        }

        public async Task<ReplaceOneResult> UpdateProductById(ObjectId productId, ProductDocument productDocument)
        {
            var filter = Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId);
            return await _productCollection.ReplaceOneAsync(filter, productDocument);
        }

        public async Task<DeleteResult> DeleteProductById(ObjectId productId)
        {
            return await _productCollection.DeleteOneAsync(Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId));
        }
    }
}
