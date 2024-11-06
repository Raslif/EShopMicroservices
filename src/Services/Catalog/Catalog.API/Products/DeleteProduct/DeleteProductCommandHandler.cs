using BuildingBlocks.CQRS;
using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Exceptions;
using MongoDB.Bson;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(string ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);

    public class DeleteProductCommandHandler(IProductDocumentRepo productDocumentRepo)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            ObjectId id = new(command.ProductId);
            if (!await productDocumentRepo.CheckProductExists(id, cancellationToken))
                throw new ProductNotFoundException($"ProductId: {id} not found");

            var result = await productDocumentRepo.DeleteProductById(id, cancellationToken);

            return new(result.IsAcknowledged && result.DeletedCount > 0);
        }
    }
}
