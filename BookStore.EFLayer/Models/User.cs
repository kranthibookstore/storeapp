using System;
using System.Collections.Generic;

namespace BookStore.EFLayer.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? UserId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }
}
