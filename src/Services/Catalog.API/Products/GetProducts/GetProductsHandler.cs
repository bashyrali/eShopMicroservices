using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsHandler(ILogger<GetProductsHandler> logger, IDocumentSession documentSession)
    : IQueryHandler<GetProductQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GET PRODUCTS QUERY. Handle calledwith {@Query}", query);
        var products = await documentSession.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}