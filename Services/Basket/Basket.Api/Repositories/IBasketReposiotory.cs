using Basket.Api.Entities;

namespace Basket.Api.Repositories
{
    public interface IBasketReposiotory
    {
        Task DeleteBasekt(string userName);
        Task<ShopingCart?> GetBasket(string userName);
        Task<ShopingCart?> UpdateBasket(ShopingCart shopingCart);
    }
}