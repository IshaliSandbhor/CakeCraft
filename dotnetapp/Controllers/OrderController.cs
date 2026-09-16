using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dotnetapp.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(IOrderService service) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = UserRoles.Baker)]
    public async Task<ActionResult<IEnumerable<Order>>> GetAll() => Ok(await service.GetAllOrders());

    [HttpGet("my-orders")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Order>>> MyOrders()
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(customerId)) return Unauthorized();
        return Ok(await service.GetOrdersByCustomer(customerId));
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<IActionResult> Create([FromBody] OrderRequest request)
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(customerId)) return Unauthorized();

        var order = new Order
        {
            CakeId = request.CakeId,
            Quantity = request.Quantity,
            CustomerId = customerId,
            CustomerName = User.FindFirstValue(ClaimTypes.Name) ?? "Customer"
        };

        var created = await service.CreateOrder(order);
        return created ? Ok(new { message = "Order placed successfully" }) : BadRequest(new { message = "Invalid cake or quantity" });
    }

    [HttpPut("{orderId:int}/status")]
    [Authorize(Roles = UserRoles.Baker)]
    public async Task<IActionResult> UpdateStatus(int orderId, [FromBody] OrderStatusUpdate model)
    {
        var updated = await service.UpdateOrderStatus(orderId, model.Status);
        return updated ? Ok(new { message = "Order status updated" }) : NotFound(new { message = "Order not found" });
    }
}

public class OrderStatusUpdate
{
    public string Status { get; set; } = "Pending";
}
