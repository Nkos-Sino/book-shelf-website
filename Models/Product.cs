using System;
using System.Collections.Generic;

namespace BookShelfHaven5.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductNames { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
}
