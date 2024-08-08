using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(x => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetMyProducts());
            }
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return
            [
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Candy",
                    Category = "Food",
                    Description = "Candy, alternatively called sweets or lollies, is a confection that features sugar as a principal ingredient",
                    Image = "",
                    Price = 2.0m
                }
            ];
        }
    }
}
