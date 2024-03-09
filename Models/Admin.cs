using System;
using System.Collections.Generic;

namespace BookShelfHaven5.Models;

public partial class Admin
{
    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int FailedLoginAttempts { get; set; }
}
