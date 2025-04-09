using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.GetProducts;
using Marten;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid productId) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductById {@query}", request);
        var product = await session.LoadAsync<Product>(request.productId, cancellationToken);
        if (product is null)
        {
            throw new Exception("Not Found");
        }

        return new GetProductByIdResult(product);
    }
}