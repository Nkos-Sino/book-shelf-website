using System;
using System.Collections.Generic;

namespace BookShelfHaven5.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public string? Username { get; set; }

    public string? UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string ProductNames { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Customer? UsernameNavigation { get; set; }
}
