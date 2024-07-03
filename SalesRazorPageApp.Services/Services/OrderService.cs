using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.Enums;
using SalesRazorPageApp.Shared.Exceptions;

namespace SalesRazorPageApp.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddToCart(int memberId, int productId, int quantity)
        {
            var product = await _unitOfWork.ProductRepository.FindOneAsync(p => p.ProductId == productId);

            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }

            var order = await _unitOfWork.OrderRepository.FindOneAsync(o => o.MemberId == memberId && o.Status
            == OrderStatus.InCart.ToString());

            if (order is null)
            {
                order = new Order
                {
                    MemberId = memberId,
                    Freight = 0,
                    Status = OrderStatus.InCart.ToString(),
                };

                await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();
            }

            var orderDetail = await _unitOfWork.OrderDetailRepository.FindOneAsync(
                od => od.OrderId == order.OrderId && od.ProductId == productId);

            if (orderDetail is null)
            {
                orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.UnitPrice * quantity,
                };

                await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
            }
            else
            {
                orderDetail.Quantity += quantity;
                orderDetail.UnitPrice += product.UnitPrice * orderDetail.Quantity;
            }

            order.Freight += orderDetail.UnitPrice;
                    
            await _unitOfWork.SaveChangesAsync();
            
        }

        public async Task ConfirmOrder(int orderId, int memberId)
        {
            var order = await _unitOfWork.OrderRepository.FindOneAsync(o => o.OrderId == orderId && o.MemberId == memberId);

            if (order is null)
            {
                throw new NotFoundException("Order not found");                
            }

            order.Status = OrderStatus.Pending.ToString();
            order.OrderDate = DateTime.Now;
            order.ShippedDate = DateTime.Now.AddDays(7);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Order> GetCartInfo(int memberId)
        {
            var order = await _unitOfWork.OrderRepository.GetCartInfo(memberId);

            if (order is null)
            {
                throw new NotFoundException("Cart not found");
            }

            return order;

        }
    }
}