using Catalog.API.Models.DocumentModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Abstracts
{
    public interface IProductDocumentRepo
    {
        Task<bool> CheckProductExists(ObjectId productId);
        Task<string> SaveProduct(ProductDocument productDocument);
        Task<List<ProductDocument>> GetAllProducts();
        Task<ProductDocument> GetProductById(ObjectId productId);
        Task<List<ProductDocument>> GetProductsByCategory(string category);
        Task<ReplaceOneResult> UpdateProductById(ObjectId productId, ProductDocument productDocument);
        Task<DeleteResult> DeleteProductById(ObjectId productId);
    }
}
