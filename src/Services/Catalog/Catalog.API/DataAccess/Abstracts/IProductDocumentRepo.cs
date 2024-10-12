using Catalog.API.Models.DocumentModels;

namespace Catalog.API.DataAccess.Abstracts
{
    public interface IProductDocumentRepo
    {
        Task<string> SaveProduct(ProductDocument productDocument);
    }
}
