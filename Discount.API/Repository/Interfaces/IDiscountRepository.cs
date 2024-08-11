using Discount.API.Entities;

namespace Discount.API.Repository.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetByProductName(string productName);
        Task<bool> Create(Coupon coupon);
        Task<bool> Update(Coupon coupon);
        Task<bool> Delete(string productName);
    }
}
