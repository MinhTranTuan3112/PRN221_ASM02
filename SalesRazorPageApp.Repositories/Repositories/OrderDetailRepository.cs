using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;

namespace SalesRazorPageApp.Repositories.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly SalesManagementDbContext _context;
        public OrderDetailRepository(SalesManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderDetail?> GetOrderDetailWithProduct(int orderId, int productId)
        {
            return await _context.OrderDetails.Include(od => od.Product)
                                            .SingleOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);
        }
    }
}