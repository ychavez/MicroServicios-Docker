namespace Basket.Api.Entities;

public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public string? Color { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
}

