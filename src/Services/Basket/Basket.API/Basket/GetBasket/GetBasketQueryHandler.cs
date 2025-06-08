using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Mapster;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(int UserId) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var result = await basketRepository.GetBasket(request.UserId, cancellationToken);
            var response = result.Adapt<ShoppingCart>();
            return new GetBasketResult(response); 
        }
    }
}
