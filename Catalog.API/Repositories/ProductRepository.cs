﻿using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _catalogContext.Products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _catalogContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Regex(x => x.Category, new MongoDB.Bson.BsonRegularExpression(category, "i"));

            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(x => x.Name, name);

            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task Create(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        public async Task<bool> Update(Product product)
        {
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter: x => x.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
