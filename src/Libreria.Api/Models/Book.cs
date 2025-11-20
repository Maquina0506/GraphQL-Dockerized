namespace Libreria.Api.Models;

public class Book
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Autor { get; set; } = null!;
    public int AnioPublicacion { get; set; }
    public string? ImagenPortadaUrl { get; set; }

    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}