using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(int UserId, bool IsSaveSucces);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/save", async (StoreBasketRequest saveCommand, ISender sender) =>
            {
                var command = saveCommand.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserId}", response);
            })
         .WithName("StoreProductToBasket")
         .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Store Product To Basket")
         .WithDescription("Store Product To Basket");
        }
    }
}

// Test Commit in feature branch