namespace BookStore.EFLib.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public decimal Price { get; set; }

    public DateOnly? PublishedDate { get; set; }

    public int Stock { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
