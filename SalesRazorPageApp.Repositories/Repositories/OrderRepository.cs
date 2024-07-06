using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Extensions;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Shared.Enums;
using SalesRazorPageApp.Shared.RequestModels.Order;
using SalesRazorPageApp.Shared.ResponseModels;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Repositories.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly SalesManagementDbContext _context;

        public OrderRepository(SalesManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order?> GetCartInfo(int memberId)
        {
            return await _context.Orders
                                    .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .FirstOrDefaultAsync(o => o.MemberId == memberId && OrderStatus.InCart.ToString() == o.Status);
        }

        public async Task<Order?> GetOrderDetailsInfoById(int orderId)
        {
            return await _context.Orders
                                    .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .SingleOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<PagedResultResponse<Order>> GetPagedOrders(QueryPagedOrderRequest request)
        {
            var query = _context.Orders
                            .Include(o => o.Member)
                            .AsQueryable();

            if (request.MemberId.HasValue)
            {
                query = query.Where(o => o.MemberId == request.MemberId.Value);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= request.StartDate);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= request.EndDate);
            }


            return await query.ToPagedResponseAsync(request.PageNumber, request.PageSize);
        }

        public async Task<List<StatResponse>> GetStats(int? year = default)
        {
            year ??= DateTime.Now.Year;
            
            var monthlyStatistics = await _context.Orders
                                                .AsNoTracking()
                                                .Where(o => o.OrderDate.Year == year && o.Status != OrderStatus.InCart.ToString())
                                                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                                                .Select(g => new StatResponse
                                                {
                                                    Year = g.Key.Year,
                                                    Month = g.Key.Month,
                                                    TotalSales = g.Sum(o => o.Freight ?? 0)
                                                })
                                                .ToListAsync();

            return monthlyStatistics;
        }

        public async Task UpdateOrderFreight(int orderId)
        {
            decimal newFreight = await _context.OrderDetails.Where(od => od.OrderId == orderId)
                                                            .SumAsync(od => od.UnitPrice * od.Quantity);
            await _context.Orders.Where(o => o.OrderId == orderId)
                                .ExecuteUpdateAsync(setter => setter.SetProperty(o => o.Freight, newFreight));

            await _context.SaveChangesAsync();
        }
    }
}