using Libreria.Api.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _books;
    public BooksController(IBookRepository books) => _books = books;

    [HttpPost]
    public async Task<ActionResult<Book>> Create([FromBody] Book book)
    {
        var created = await _books.AddAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<IEnumerable<Book>> GetAll() => await _books.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _books.GetByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Book dto)
    {
        var b = await _books.GetByIdAsync(id);
        if (b is null) return NotFound();
        b.Titulo = dto.Titulo;
        b.Autor = dto.Autor;
        b.AnioPublicacion = dto.AnioPublicacion;
        b.ImagenPortadaUrl = dto.ImagenPortadaUrl;
        await _books.UpdateAsync(b);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _books.DeleteAsync(id);
        return NoContent();
    }
}