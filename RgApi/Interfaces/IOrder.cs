using RgApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Interfaces
{
    public interface IOrder
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetAllOrdersForUserAsync(string userId);
        Task<List<OrderDetail>> GetOrderDetailsForOrder(int orderId);
        Task<bool> AddOrderAsync(Order order, IEnumerable<OrderDetail> items);

        Task<decimal> GetCartTotalCostWithShippingAsync(string userId);
    }
}
