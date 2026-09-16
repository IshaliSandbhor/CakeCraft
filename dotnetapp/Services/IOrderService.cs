using dotnetapp.Models;

namespace dotnetapp.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<IEnumerable<Order>> GetOrdersByCustomer(string customerId);
    Task<Order?> GetOrderById(int orderId);
    Task<bool> CreateOrder(Order order);
    Task<bool> UpdateOrderStatus(int orderId, string status);
}
