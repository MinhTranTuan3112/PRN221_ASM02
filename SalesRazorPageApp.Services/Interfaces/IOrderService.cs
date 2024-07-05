using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Shared.RequestModels.Order;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddToCart(int memberId, int productId, int quantity);

        Task ConfirmOrder(int orderId, int memberId);

        Task UpdateCart(int orderId, int productId, int quantity);

        Task<Order> GetCartInfo(int memberId);

        Task<PagedResultResponse<Order>> GetPagedOrders(QueryPagedOrderRequest request);

        Task<Order> GetOrderDetailsInfoById(int orderId);

        Task UpdateOrder(Order order);

        Task DeleteOrder(int orderId);
    }
}