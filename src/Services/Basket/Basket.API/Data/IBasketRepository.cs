using Basket.API.Models;

namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<BasketDocumentDTO> GetBasket(int userId, CancellationToken cancellationToken = default);
        Task<bool> StoreBasket(BasketDocumentDTO basket, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasket(int userId, CancellationToken cancellationToken = default);
    }
}
