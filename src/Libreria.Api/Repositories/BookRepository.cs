using Libreria.Api.Models;
using Libreria.Api.Persistence;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _ctx;
    public BookRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Book?> GetByIdAsync(int id) =>
        await _ctx.Books.FindAsync(id);

    public async Task<IEnumerable<Book>> GetAllAsync() =>
        await _ctx.Books.AsNoTracking().ToListAsync();

    public async Task<Book> AddAsync(Book book)
    {
        _ctx.Books.Add(book);
        await _ctx.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _ctx.Books.Update(book);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var b = await _ctx.Books.FindAsync(id);
        if (b is null) return;
        _ctx.Books.Remove(b);
        await _ctx.SaveChangesAsync();
    }
}