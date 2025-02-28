using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Core;
using PasswordManager.Core.Models;
using System.Security.Claims;

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

    private int? GetUserId()
    {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    return userIdClaim != null ? int.Parse(userIdClaim.Value) : null;
    }


[HttpGet]
public async Task<ActionResult<IEnumerable<PasswordEntry>>> GetPasswords()
{
    int? userId = GetUserId();
    if (userId == null) return Unauthorized(); 

    var passwords = await _context.Passwords
        .Where(p => p.UserId == userId.Value) 
        .ToListAsync();

    return Ok(passwords);
}

[HttpPost]
public async Task<ActionResult<PasswordEntry>> AddPassword([FromBody] PasswordEntry password)
{
    int? userId = GetUserId();
    if (userId == null) return Unauthorized();

    password.UserId = userId.Value;

    _context.Passwords.Add(password);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetPasswords), new { id = password.Id }, password);
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdatePassword(int id, [FromBody] PasswordEntry updatedPassword)
{
    int? userId = GetUserId();
    if (userId == null) return Unauthorized();

    var existingPassword = await _context.Passwords.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId.Value);
    if (existingPassword == null) return NotFound();

    existingPassword.Title = updatedPassword.Title;
    existingPassword.Username = updatedPassword.Username;
    existingPassword.EncryptedPassword = updatedPassword.EncryptedPassword;
    existingPassword.Category = updatedPassword.Category;
    await _context.SaveChangesAsync();

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeletePassword(int id)
{
    int? userId = GetUserId();
    if (userId == null) return Unauthorized();

    var password = await _context.Passwords.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId.Value);
    if (password == null) return NotFound();

    _context.Passwords.Remove(password);
    await _context.SaveChangesAsync();

    return NoContent();
}



}
