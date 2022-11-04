namespace Basket.Api.Entities
{
    public class ShopingCart
    {
        public string UserName { get; set; } = null!;

        public List<ShoppingCartItem> shoppingCartItems { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in shoppingCartItems)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;

            }
        }
        public ShopingCart()
        {
        }

        public ShopingCart(string userName)
        {
            UserName = userName;
        }

    }
}
