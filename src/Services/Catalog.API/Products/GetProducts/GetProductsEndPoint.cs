using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery());
                var response = result.Adapt<GetProductsResult>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}