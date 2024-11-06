using Catalog.API.Models.DocumentModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Abstracts
{
    public interface IProductDocumentRepo
    {
        Task<bool> CheckProductExists(ObjectId productId, CancellationToken cancellationToken);
        Task<string> SaveProduct(ProductDocument productDocument, CancellationToken cancellationToken);
        Task<List<ProductDocument>> GetAllProducts(CancellationToken cancellationToken);
        Task<ProductDocument> GetProductById(ObjectId productId, CancellationToken cancellationToken);
        Task<List<ProductDocument>> GetProductsByCategory(string category, CancellationToken cancellationToken);
        Task<ReplaceOneResult> UpdateProductById(ObjectId productId, ProductDocument productDocument, CancellationToken cancellationToken);
        Task<DeleteResult> DeleteProductById(ObjectId productId, CancellationToken cancellationToken);
    }
}
