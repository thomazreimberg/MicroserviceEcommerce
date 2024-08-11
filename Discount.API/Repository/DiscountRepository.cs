using Dapper;
using Discount.API.Entities;
using Discount.API.Repository.Interfaces;
using Npgsql;

namespace Discount.API.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetByProductName(string productName)
        {
            using (NpgsqlConnection connection = GetConnectionString())
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                    "SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName }
                );

                return coupon ?? new Coupon();
            }
        }

        public async Task<bool> Create(Coupon coupon)
        {
            using (NpgsqlConnection connection = GetConnectionString())
            {
                var affected = await connection.ExecuteAsync(
                    "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { coupon.ProductName, coupon.Description, coupon.Amount }
                );

                return affected != 0;
            }
        }

        public async Task<bool> Update(Coupon coupon)
        {
            using (NpgsqlConnection connection = GetConnectionString())
            {
                var affected = await connection.ExecuteAsync(
                    "UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                    new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id }
                );

                return affected != 0;
            }
        }

        public async Task<bool> Delete(string productName)
        {
            using (NpgsqlConnection connection = GetConnectionString())
            {
                var affected = await connection.ExecuteAsync(
                    "DELETE FROM Coupon WHERE ProductName = @ProductName",
                    new { ProductName = productName }
                );

                return affected != 0;
            }
        }

        private NpgsqlConnection GetConnectionString()
        {
            return new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
    }
}
