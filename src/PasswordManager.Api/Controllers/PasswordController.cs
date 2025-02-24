using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Core;
using PasswordManager.Core.Models;

namespace PasswordManager.Api.Controllers;

using Microsoft.AspNetCore.Authorization;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PasswordEntry>>> GetPasswords()
    {
        return await _context.Passwords.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<PasswordEntry>> AddPassword(PasswordEntry password)
    {
        _context.Passwords.Add(password);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPasswords), new { id = password.Id }, password);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePassword(int id)
    {
        var password = await _context.Passwords.FindAsync(id);
        if (password == null) return NotFound();

        _context.Passwords.Remove(password);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
