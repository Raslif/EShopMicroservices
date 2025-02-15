using Basket.API.Basket.DeleteBasket;
using FluentValidation;

namespace Basket.API.Validations
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required")
                                  .GreaterThan(0).WithMessage("User Id should be valid");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required");
        }
    }
}
