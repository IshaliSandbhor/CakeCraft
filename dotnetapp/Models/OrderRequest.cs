using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models;

public class OrderRequest
{
    [Required]
    public int CakeId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;
}
