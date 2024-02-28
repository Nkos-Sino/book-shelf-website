using System;
using System.Collections.Generic;

namespace BookShelfHaven5.Models;

public partial class Checkout
{
    public int CheckoutId { get; set; }

    public string? Username { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CheckoutDateTime { get; set; }

    public string? PaymentInformation { get; set; }

    public string? ShippingInformation { get; set; }

    public string Status { get; set; } = null!;

    public string? AdditionalInformation { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Customer? UsernameNavigation { get; set; }
}
