using RgApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Services
{
    public interface IProduct
    {
        // Products
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        // Collections
        Task<ProductCollection> GetProductCollectionByIdAsync(int id);
        Task<List<ProductCollection>> GetAllProductCollectionsAsync();
        Task CreateProductCollectionAsync(ProductCollection collection);
        Task DeleteProductCollectionAsync(int id);
    }
}
