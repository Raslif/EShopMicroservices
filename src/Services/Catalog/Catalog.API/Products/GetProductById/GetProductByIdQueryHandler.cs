using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models;
using Catalog.API.Models.DocumentModels;
using Mapster;
using MongoDB.Bson;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(string Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    public class GetProductByIdQueryHandler(IProductDocumentRepo productDocumentRepo) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productDocument = await productDocumentRepo.GetProductById(new ObjectId(request.Id), cancellationToken) 
                ?? throw new NotFoundException($"ProductId: {request.Id} not found");
            
            TypeAdapterConfig<ProductDocument, Product>.NewConfig().Map(dest => dest.Id, src => src.Id.ToString());

            return new GetProductByIdResult(productDocument.Adapt<Product>());
        }
    }
}
