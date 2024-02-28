using System;
using System.Collections.Generic;

namespace BookShelfHaven5.Models;

public partial class Customer
{
    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int FailedLoginAttempts { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();

    public virtual ICollection<LoginAttempt> LoginAttempts { get; set; } = new List<LoginAttempt>();
}
