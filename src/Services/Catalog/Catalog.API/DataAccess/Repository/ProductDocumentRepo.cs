using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models.DocumentModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;

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

        public async Task<bool> CheckProductExists(ObjectId productId, CancellationToken cancellationToken)
        {
            var filter = Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId);
            return await _productCollection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0;
        }

        public async Task<string> SaveProduct(ProductDocument productDocument, CancellationToken cancellationToken)
        {
            await _productCollection.InsertOneAsync(productDocument, cancellationToken: cancellationToken);
            return $"{productDocument.Id}";
        }

        public async Task<List<ProductDocument>> GetAllProducts(CancellationToken cancellationToken) 
            => await _productCollection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        public async Task<ProductDocument> GetProductById(ObjectId productId, CancellationToken cancellationToken)
        {
            var filter = Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId);
            return await _productCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<ProductDocument>> GetProductsByCategory(string category, CancellationToken cancellationToken)
        {
            var filter = Builders<ProductDocument>.Filter.AnyEq(doc => doc.Category, category);
            return await _productCollection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<ReplaceOneResult> UpdateProductById(ObjectId productId, ProductDocument productDocument, CancellationToken cancellationToken)
        {
            var filter = Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId);
            return await _productCollection.ReplaceOneAsync(filter, productDocument, cancellationToken: cancellationToken);
        }

        public async Task<DeleteResult> DeleteProductById(ObjectId productId, CancellationToken cancellationToken)
        {
            return await _productCollection.DeleteOneAsync(Builders<ProductDocument>.Filter.Eq(doc => doc.Id, productId), cancellationToken: cancellationToken);
        }
    }
}
