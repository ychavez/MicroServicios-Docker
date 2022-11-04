using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Repositories
{
    public class BasketReposiotory : IBasketReposiotory
    {
        private readonly IDistributedCache distributedCache;

        public BasketReposiotory(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<ShopingCart?> GetBasket(string userName)
        {
            // obtener el valor dirtectamente de Redis
            var basket = await distributedCache.GetStringAsync(userName);

            if (basket is null)
                return null;

            return JsonSerializer.Deserialize<ShopingCart>(basket);

        }

        public async Task<ShopingCart?> UpdateBasket(ShopingCart shopingCart)
        {
            await distributedCache.SetStringAsync(shopingCart.UserName, JsonSerializer.Serialize(shopingCart));

            return await GetBasket(shopingCart.UserName);

        }

        public async Task DeleteBasekt(string userName)
            => await distributedCache.RemoveAsync(userName);
    }
}
