using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Core;
using PasswordManager.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace PasswordManager.Api.Controllers;

[Authorize]
[Route("api/passwords")]
[ApiController]
public class PasswordController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PasswordController(ApplicationDbContext context)
    {
        _context = context;
    }

[HttpPut("{id}")]
public async Task<IActionResult> UpdatePassword(int id, [FromBody] PasswordEntry updatedPassword)
{

    var existingPassword = await _context.Passwords.FindAsync(id);
    if (existingPassword == null)
    {
        return NotFound();
    }

    existingPassword.Title = updatedPassword.Title;
    existingPassword.Username = updatedPassword.Username;
    existingPassword.EncryptedPassword = updatedPassword.EncryptedPassword;
    existingPassword.Category = updatedPassword.Category;
    existingPassword.CreatedAt = updatedPassword.CreatedAt; 

    await _context.SaveChangesAsync();

    return NoContent();
}


    [HttpGet]
    public async Task<ActionResult<IEnumerable<PasswordEntry>>> GetPasswords()
    {
        var passwords = await _context.Passwords.ToListAsync();

        if (passwords == null || passwords.Count == 0)
        {
        }
        else
        {
        }

        return passwords;
    }

[HttpPost]
public async Task<ActionResult<PasswordEntry>> AddPassword([FromBody] PasswordEntry password)
{
 

    if (string.IsNullOrEmpty(password.Title) || string.IsNullOrEmpty(password.Username) || string.IsNullOrEmpty(password.EncryptedPassword))
    {
        return BadRequest("Tous les champs sont requis.");
    }

    _context.Passwords.Add(password);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetPasswords), new { id = password.Id }, password);
}


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePassword(int id)
    {

        var password = await _context.Passwords.FindAsync(id);
        if (password == null)
        {
            return NotFound();
        }

        _context.Passwords.Remove(password);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
