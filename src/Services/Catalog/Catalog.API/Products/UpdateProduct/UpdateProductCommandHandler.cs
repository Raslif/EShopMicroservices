using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.DataAccess.Abstracts;
using MongoDB.Bson;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);
    public class UpdateProductCommandHandler(IProductDocumentRepo productDocumentRepo) 
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            ObjectId docId = new(command.Id);
            var productDocument = await productDocumentRepo.GetProductById(docId, cancellationToken) 
                ?? throw new NotFoundException($"ProductId: {docId} not found");

            productDocument.Name = command.Name;
            productDocument.Category = command.Category;
            productDocument.Description = command.Description;
            productDocument.ImageFile = command.ImageFile;
            productDocument.Price = command.Price;

            var result = await productDocumentRepo.UpdateProductById(docId, productDocument, cancellationToken);

            return new(result.IsAcknowledged && result.ModifiedCount > 0);
        }
    }
}
