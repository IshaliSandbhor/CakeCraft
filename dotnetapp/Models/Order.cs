using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotnetapp.Models;

public class Order
{
    public int OrderId { get; set; }

    [Required]
    public int CakeId { get; set; }

    public Cake? Cake { get; set; }

    [BindNever]
    public string CustomerId { get; set; } = "";

    [BindNever]
    public string CustomerName { get; set; } = "";

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;

    [BindNever]
    public decimal TotalPrice { get; set; }

    [BindNever]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BindNever]
    public string Status { get; set; } = "Pending";
}
