using Libreria.Api.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users/{userId:int}/collection")]
public class UserBooksController : ControllerBase
{
    private readonly IUserBookRepository _userBooks;
    private readonly IUserRepository _users;
    private readonly IBookRepository _books;

    public UserBooksController(IUserBookRepository userBooks, IUserRepository users, IBookRepository books)
    {
        _userBooks = userBooks; _users = users; _books = books;
    }

    [HttpPost("{bookId:int}")]
    public async Task<IActionResult> AddBook(int userId, int bookId)
    {
        var u = await _users.GetByIdAsync(userId);
        var b = await _books.GetByIdAsync(bookId);
        if (u is null || b is null) return NotFound();
        var existing = await _userBooks.GetAsync(userId, bookId);
        if (existing is null)
            await _userBooks.AddAsync(new UserBook { UserId = userId, BookId = bookId });
        return NoContent();
    }

    public record RateReviewDto(int Rating, string? Review);

    [HttpPut("{bookId:int}/rate")]
    public async Task<IActionResult> RateReview(int userId, int bookId, [FromBody] RateReviewDto dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5) return BadRequest("Rating 1..5");
        var ub = await _userBooks.GetAsync(userId, bookId);
        if (ub is null) return NotFound();
        ub.Rating = dto.Rating;
        ub.Review = dto.Review;
        await _userBooks.UpdateAsync(ub);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBook>>> List(int userId)
        => Ok(await _userBooks.GetByUserAsync(userId));

    [HttpDelete("{bookId:int}")]
    public async Task<IActionResult> Remove(int userId, int bookId)
    {
        await _userBooks.DeleteAsync(userId, bookId);
        return NoContent();
    }
}