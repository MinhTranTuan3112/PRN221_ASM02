using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Shared.RequestModels.Order;
using SalesRazorPageApp.Shared.ResponseModels;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetCartInfo(int memberId);

        Task UpdateOrderFreight(int orderId);

        Task<PagedResultResponse<Order>> GetPagedOrders(QueryPagedOrderRequest request);

        Task<Order?> GetOrderDetailsInfoById(int orderId);

        Task<List<StatResponse>> GetStats(int? year = default);
    }
}