using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.Enums;
using SalesRazorPageApp.Shared.Exceptions;
using SalesRazorPageApp.Shared.RequestModels.Order;
using SalesRazorPageApp.Shared.ResponseModels;
using SalesRazorPageApp.Shared.ResponseModels.Query;

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
            var order = await _unitOfWork.OrderRepository.FindOneAsync(o => o.OrderId == orderId && o.MemberId == memberId
            && o.Status == OrderStatus.InCart.ToString());

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            order.Status = OrderStatus.Pending.ToString();
            order.OrderDate = DateTime.Now;
            order.ShippedDate = DateTime.Now.AddDays(7);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrder(int orderId)
        {
            if (!await _unitOfWork.OrderRepository.AnyAsync(o => o.OrderId == orderId))
            {
                throw new NotFoundException("Order not found");
            }
            
            await _unitOfWork.OrderDetailRepository.ExecuteDeleteAsync(od => od.OrderId == orderId);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.OrderRepository.ExecuteDeleteAsync(o => o.OrderId == orderId);
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

        public async Task<Order> GetOrderDetailsInfoById(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderDetailsInfoById(orderId);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            return order;
        }

        public async Task<PagedResultResponse<Order>> GetPagedOrders(QueryPagedOrderRequest request)
        {
            return await _unitOfWork.OrderRepository.GetPagedOrders(request);
        }

        public async Task<List<StatResponse>> GetStats(int? year = null)
        {
            return await _unitOfWork.OrderRepository.GetStats(year);
        }

        public async Task UpdateCart(int orderId, int productId, int quantity)
        {
            var order = await _unitOfWork.OrderRepository.FindOneAsync(o => o.OrderId == orderId && o.Status == OrderStatus.InCart.ToString());
            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            var orderDetail = await _unitOfWork.OrderDetailRepository.GetOrderDetailWithProduct(orderId, productId);

            if (orderDetail is not null)
            {
                if (quantity == 0)
                {
                    await _unitOfWork.OrderDetailRepository.ExecuteDeleteAsync(od => od.OrderId == orderId && od.ProductId == productId);
                }
                else
                {
                    orderDetail.Quantity = quantity;
                    orderDetail.UnitPrice = orderDetail.Product.UnitPrice * quantity;
                }

                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.OrderRepository.UpdateOrderFreight(orderId);
        }

        public async Task UpdateOrder(Order order)
        {
            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}