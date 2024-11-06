using BuildingBlocks.CQRS;
using Catalog.API.DataAccess.Abstracts;
using Catalog.API.Models.DocumentModels;
using FluentValidation;
using Mapster;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
                                        : ICommand<CreateProductResult>;
    public record CreateProductResult(string Id);

    public class CreateProductCommandHandler(IProductDocumentRepo productDocumentRepo, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validations = await validator.ValidateAsync(command, cancellationToken);
            var listOfErrors = validations.Errors.Select(x => x.ErrorMessage).ToList();
            if (listOfErrors.Any())
            {
                throw new ValidationException(listOfErrors.FirstOrDefault());
            }

            var productDocument = command.Adapt<ProductDocument>();
            var result = await productDocumentRepo.SaveProduct(productDocument, cancellationToken);
            return new CreateProductResult(result);
        }
    }
}
