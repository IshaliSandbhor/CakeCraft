using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services;

public class OrderService(ApplicationDbContext context) : IOrderService
{
    public async Task<IEnumerable<Order>> GetAllOrders() =>
        await context.Orders.Include(o => o.Cake).OrderByDescending(o => o.CreatedAt).ToListAsync();

    public async Task<IEnumerable<Order>> GetOrdersByCustomer(string customerId) =>
        await context.Orders.Include(o => o.Cake)
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<Order?> GetOrderById(int orderId) =>
        await context.Orders.Include(o => o.Cake).FirstOrDefaultAsync(o => o.OrderId == orderId);

    public async Task<bool> CreateOrder(Order order)
    {
        var cake = await context.Cakes.FindAsync(order.CakeId);
        if (cake == null) return false;

        if (order.Quantity <= 0) return false;

        order.Cake = cake;
        order.TotalPrice = cake.Price * order.Quantity;
        order.Status = "Pending";
        order.CreatedAt = DateTime.UtcNow;

        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateOrderStatus(int orderId, string status)
    {
        var order = await context.Orders.FindAsync(orderId);
        if (order == null) return false;

        order.Status = status;
        await context.SaveChangesAsync();
        return true;
    }
}
