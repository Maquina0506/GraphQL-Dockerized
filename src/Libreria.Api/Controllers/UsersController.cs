using Libreria.Api.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _users;
    public UsersController(IUserRepository users) => _users = users;

    [HttpPost] // registro
    public async Task<ActionResult<User>> Register([FromBody] User user)
    {
        if (await _users.ExistsByEmailAsync(user.Email))
            return Conflict("Email ya registrado.");
        var created = await _users.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAll() => await _users.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _users.GetByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] User dto)
    {
        var existing = await _users.GetByIdAsync(id);
        if (existing is null) return NotFound();
        existing.Nombre = dto.Nombre;
        existing.Apellido = dto.Apellido;
        existing.Email = dto.Email;
        if (!string.IsNullOrWhiteSpace(dto.PasswordHash))
            existing.PasswordHash = dto.PasswordHash;
        await _users.UpdateAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _users.DeleteAsync(id);
        return NoContent();
    }
}