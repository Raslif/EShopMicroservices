using BuildingBlocks.CQRS;
using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models;
using Catalog.API.Models.DocumentModels;
using Mapster;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(List<Product> Products);

    public class GetProductsQueryHandler(IProductDocumentRepo productDocumentRepo)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var listOfProductDoc = await productDocumentRepo.GetAllProducts(cancellationToken);

            if (!listOfProductDoc.Any())
                return new(new List<Product>());

            TypeAdapterConfig<ProductDocument, Product>.NewConfig().Map(dest => dest.Id, src => src.Id.ToString());

            return new(listOfProductDoc.Adapt<List<Product>>());
        }
    }
}
