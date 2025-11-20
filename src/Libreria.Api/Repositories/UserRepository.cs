using Libreria.Api.Models;
using Libreria.Api.Persistence;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;
    public UserRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<User?> GetByIdAsync(int id) =>
        await _ctx.Users.Include(u => u.UserBooks).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _ctx.Users.AsNoTracking().ToListAsync();

    public async Task<User> AddAsync(User user)
    {
        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _ctx.Users.Update(user);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var u = await _ctx.Users.FindAsync(id);
        if (u is null) return;
        _ctx.Users.Remove(u);
        await _ctx.SaveChangesAsync();
    }

    public Task<bool> ExistsByEmailAsync(string email) =>
        _ctx.Users.AnyAsync(x => x.Email == email);
}