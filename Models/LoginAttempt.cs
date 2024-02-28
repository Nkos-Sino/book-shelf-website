using System;
using System.Collections.Generic;

namespace BookShelfHaven5.Models;

public partial class LoginAttempt
{
    public int AttemptId { get; set; }

    public string? Username { get; set; }

    public DateTime AttemptDateTime { get; set; }

    public bool Success { get; set; }

    public virtual Customer? UsernameNavigation { get; set; }
}
