using BuildingBlocks.CQRS;
using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models;
using Catalog.API.Models.DocumentModels;
using Mapster;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(List<Product> Products);

    public class GetProductByCategoryQueryHandler(IProductDocumentRepo productDocumentRepo) 
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var listOfProductDoc = await productDocumentRepo.GetProductsByCategory(request.Category);

            if(listOfProductDoc == null || !listOfProductDoc.Any())
                return new(new List<Product>());

            TypeAdapterConfig<ProductDocument, Product>.NewConfig().Map(dest => dest.Id, src => src.Id.ToString());

            return new(listOfProductDoc.Adapt<List<Product>>());
        }
    }
}
