using demo_minimal_api.Interfaces;
using demo_minimal_api.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_minimal_api.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MinimalContextDb _minimalContextDb;
        public ProductRepository(MinimalContextDb minimalContextDb)
        {
            _minimalContextDb = minimalContextDb;
        }

        public async Task<List<Products>> GetAll()
        {
            return await _minimalContextDb.Products.ToListAsync();
        }

        public async Task<Products> GetById(Guid id)
        {
            return await _minimalContextDb.Products.FindAsync(id);
        }

        public async Task<int> SaveNew(Products product)
        {
            var newProduct = await _minimalContextDb.Products.AddAsync(product);

            return await _minimalContextDb.SaveChangesAsync();
        }

        public async Task<bool> Update(Products product)
        {
            var productResult = await GetById(product.Id);

            if (productResult == null) return false;

            productResult.Code = product.Code;
            productResult.Description = product.Description;
            productResult.Price = product.Price;
            productResult.Active = product.Active;

            _minimalContextDb.Products.Update(productResult);

            await _minimalContextDb.SaveChangesAsync();

            return true;
        }

        public async Task<int> Delete(Guid id)
        {
            var productResult = await GetById(id);

            if (productResult == null) return 0;

            _minimalContextDb.Products.Remove(productResult);

            return await _minimalContextDb.SaveChangesAsync();
        }
    }
}
