namespace Basket.Models;

public class ShopingCart
{
    public string UserName { get; set; } = default!;
    public List<ShopingCartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}
