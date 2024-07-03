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
    }
}