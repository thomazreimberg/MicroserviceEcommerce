using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        private const string AllCartsKey = "all_shopping_cart_keys";

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<IEnumerable<ShoppingCart>> GetAll()
        {
            var keys = await _redisCache.GetStringAsync(AllCartsKey);

            if (string.IsNullOrEmpty(keys))
            {
                return Enumerable.Empty<ShoppingCart>();
            }

            var cartKeys = keys.Split(',');

            var shoppingCarts = new List<ShoppingCart>();

            foreach (var key in cartKeys)
            {
                var cartData = await _redisCache.GetStringAsync(key);
                if (!string.IsNullOrEmpty(cartData))
                {
                    var cart = JsonSerializer.Deserialize<ShoppingCart>(cartData);
                    if (cart != null)
                    {
                        shoppingCarts.Add(cart);
                    }
                }
            }

            return shoppingCarts;
        }

        public async Task<ShoppingCart> GetBasketByUserName(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket)) return null!;

            return JsonSerializer.Deserialize<ShoppingCart>(basket)!;
        }

        public async Task<ShoppingCart> Update(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));

            return await GetBasketByUserName(basket.UserName);
        }

        public async Task DeleteByUserName(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
