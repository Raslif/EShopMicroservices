using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsDeleted);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/delete/{productId}", async (string productId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(productId));

                return Results.Ok(result.Adapt<DeleteProductResponse>());
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
