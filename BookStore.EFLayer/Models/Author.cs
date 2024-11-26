using System;
using System.Collections.Generic;

namespace BookStore.EFLayer.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Bio { get; set; }

    public DateOnly? DateOfBirth { get; set; }
}
