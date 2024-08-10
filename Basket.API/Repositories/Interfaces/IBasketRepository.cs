using Basket.API.Entities;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository : IBaseRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetBasketByUserName(string userName);
        Task DeleteByUserName(string userName);
    }
}
