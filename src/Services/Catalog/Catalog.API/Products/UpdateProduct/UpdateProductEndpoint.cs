using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsUpdated);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/product/update",
            async (UpdateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
