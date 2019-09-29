using RgApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Interfaces
{
    public interface IProductCollection
    {
        Task<ProductCollection> GetProductCollectionByIdAsync(int id);
        Task<List<ProductCollection>> GetAllProductCollectionsForCustomersAsync();
        Task<List<ProductCollection>> GetAllProductCollectionsForSalonsAsync();
        Task AddProductCollectionAsync(ProductCollection collection);
        Task DeleteProductCollectionAsync(int id);
    }
}
