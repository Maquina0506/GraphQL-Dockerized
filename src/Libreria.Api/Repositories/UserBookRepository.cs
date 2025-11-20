using Libreria.Api.Models;
using Libreria.Api.Persistence;
using Microsoft.EntityFrameworkCore;

public class UserBookRepository : IUserBookRepository
{
    private readonly AppDbContext _ctx;
    public UserBookRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<UserBook?> GetAsync(int userId, int bookId) =>
        await _ctx.UserBooks.Include(x => x.Book).FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == bookId);

    public async Task<IEnumerable<UserBook>> GetByUserAsync(int userId) =>
        await _ctx.UserBooks.Include(x => x.Book).Where(x => x.UserId == userId).ToListAsync();

    public async Task AddAsync(UserBook ub)
    {
        _ctx.UserBooks.Add(ub);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserBook ub)
    {
        _ctx.UserBooks.Update(ub);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int bookId)
    {
        var ub = await _ctx.UserBooks.FindAsync(userId, bookId);
        if (ub is null) return;
        _ctx.UserBooks.Remove(ub);
        await _ctx.SaveChangesAsync();
    }
}