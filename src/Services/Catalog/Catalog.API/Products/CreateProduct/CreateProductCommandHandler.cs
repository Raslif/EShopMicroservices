using BuildingBlocks.CQRS;
using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models.DocumentModels;
using Mapster;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
                                        : ICommand<CreateProductResult>;
    public record CreateProductResult(string Id);

    public class CreateProductCommandHandler(IProductDocumentRepo productDocumentRepo)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var productDocument = command.Adapt<ProductDocument>();
            var result = await productDocumentRepo.SaveProduct(productDocument, cancellationToken);
            return new CreateProductResult(result);
        }
    }
}
