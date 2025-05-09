namespace Basket.API.Basket.GetBasket;

public record StoreBasketCommand(ShoppingCart Cart)
    : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Корзина не может быть null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Имя пользователя не может быть пустым");
    }
}

public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await repository.StoreBasket(request.Cart, cancellationToken);
        return new StoreBasketResult(basket.UserName);
    }
}