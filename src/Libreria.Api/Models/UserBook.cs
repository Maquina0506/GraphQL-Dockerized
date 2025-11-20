namespace Libreria.Api.Models;

public class UserBook
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int? Rating { get; set; } // 1..5
    public string? Review { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}