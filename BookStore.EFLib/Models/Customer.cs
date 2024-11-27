using System;
using System.Collections.Generic;

namespace BookStore.EFLib.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? PasswordHash { get; set; }
    public DateTime RegisterAt { get; set; }
}
