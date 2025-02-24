namespace PasswordManager.Core.Models;

public class PasswordEntry
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; 
    public string Username { get; set; } = string.Empty; 
    public string EncryptedPassword { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
