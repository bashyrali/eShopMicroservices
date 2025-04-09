namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid Id) : base($"Entity \"Product\" ({Id}) was not found.")
    {
    }
}