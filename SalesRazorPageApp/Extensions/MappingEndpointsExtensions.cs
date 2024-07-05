using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.RequestModels;

namespace SalesRazorPageApp.Extensions
{
    public static class MappingEndpointsExtensions
    {
        public static WebApplication MapCustomEndpoints(this WebApplication app)
        {
            app.MapGet("/api/orders/cart-info/{memberId}", async (IOrderService _orderService, int memberId) =>
            {
                var order = await _orderService.GetCartInfo(memberId);

                return order;
            });

            app.MapPost("/api/orders/add-to-cart", async (IOrderService _orderService,
            [FromBody] AddToCartRequest request) =>
            {
                await _orderService.AddToCart(request.MemberId, request.ProductId, request.Quantity);

                return Results.Ok();
            });

            app.MapPost("/api/orders/confirm-order/{orderId}/members/{memberId}", async (IOrderService _orderService,
            [FromRoute] int orderId, [FromRoute] int memberId) =>
            {
                await _orderService.ConfirmOrder(orderId, memberId);
                
                return Results.Ok();
            });

            app.MapPut("api/orders/update-cart", async (IOrderService _orderService,
            [FromBody] UpdateCartRequest request) =>
            {
                await _orderService.UpdateCart(request.OrderId, request.ProductId, request.Quantity);

                return Results.NoContent();
            });

            return app;
        }
    }
}