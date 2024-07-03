using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesRazorPageApp.Repositories.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    [Required]
    public int CategoryId { get; set; }
    
    [Required]
    [MaxLength(50, ErrorMessage = "Name is too long")]
    public string ProductName { get; set; } = null!;
    
    public string Weight { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
    public decimal UnitPrice { get; set; } 

    [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid stock quantity")]
    public int UnitsInStock { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
