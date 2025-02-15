using BuildingBlocks.CQRS;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(int UserId, Guid ProductId) : ICommand<DeleteBasketCommandResponse>;
    public record DeleteBasketCommandResponse(bool IsDeleted);

    public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResponse>
    {
        public async Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            return new DeleteBasketCommandResponse(IsDeleted: true);
        }
    }
}
