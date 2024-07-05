using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Shared.Enums;

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