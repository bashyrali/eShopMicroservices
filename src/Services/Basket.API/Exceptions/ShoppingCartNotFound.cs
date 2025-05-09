namespace Basket.API.Exceptions;

public class ShoppingCartNotFound : NotFoundException
{
    public ShoppingCartNotFound(string userName) : base("Basket", userName)
    {
    }
}