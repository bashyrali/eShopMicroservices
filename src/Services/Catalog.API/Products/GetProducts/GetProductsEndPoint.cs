using System.Reflection.Metadata;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParametersAttribute] GetProductRequest request,ISender sender) =>
            {
                var query = request.Adapt<GetProductQuery>();
                var result = await sender.Send(query);
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