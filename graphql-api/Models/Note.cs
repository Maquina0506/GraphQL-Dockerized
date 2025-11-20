namespace GraphQL_Api.Models;
{
    public class Note
    {
        public string? Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Content { get; set; }
        public string OwnerEmail { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
