using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Mapster;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(int UserId, bool IsSaveSucces);
    public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var basketDocument = command.Cart.Adapt<BasketDocumentDTO>();
            var isSaved = await basketRepository.StoreBasket(basketDocument, cancellationToken);
            return new StoreBasketResult(command.Cart.UserId, isSaved);
        }
    }
}
// Hello