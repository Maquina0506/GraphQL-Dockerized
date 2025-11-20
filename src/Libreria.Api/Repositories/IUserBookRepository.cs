using Libreria.Api.Models;

public interface IUserBookRepository
{
    Task<UserBook?> GetAsync(int userId, int bookId);
    Task<IEnumerable<UserBook>> GetByUserAsync(int userId);
    Task AddAsync(UserBook ub);
    Task UpdateAsync(UserBook ub);
    Task DeleteAsync(int userId, int bookId);
}