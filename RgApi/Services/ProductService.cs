using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RgApi.Models;

namespace RgApi.Services
{
    public class ProductService : IProduct
    {
        private readonly AppDbContext _repo;

        public ProductService(AppDbContext context)
        {
            _repo = context;
        }

        #region Product Methods
        
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repo.Products
                              .Include(p => p.CollectionProducts)
                              .Include(p => p.CustomerPrices)
                              .Include(p => p.SalonPrices)
                              .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _repo.Products
                              .Include(p => p.CollectionProducts)
                              .Include(p => p.CustomerPrices)
                              .Include(p => p.SalonPrices)
                              .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Collection Methods

        public async Task<List<ProductCollection>> GetAllProductCollectionsForCustomersAsync()
        {
            return await _repo.ProductCollections
                              .Include(p => p.CollectionProducts)
                                .ThenInclude(p => p.Product)
                                    .ThenInclude(p => p.CustomerPrices)
                              .ToListAsync();
        }

        public async Task<List<ProductCollection>> GetAllProductCollectionsForSalonsAsync()
        {
            return await _repo.ProductCollections
                              .Include(p => p.CollectionProducts)
                                .ThenInclude(p => p.Product)
                                    .ThenInclude(p => p.SalonPrices)
                              .ToListAsync();
        }

        public async Task <ProductCollection> GetProductCollectionByIdAsync(int id)
        {
            return await _repo.ProductCollections
                              .Include(p => p.CollectionProducts)
                                .ThenInclude(p => p.Product)
                              .FirstOrDefaultAsync(p => p.ProductCollectionId == id);
        }

        public async Task CreateProductCollectionAsync(ProductCollection collection)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductCollectionAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
