using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Core.Models;

public class PasswordEntry
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string EncryptedPassword { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // 🔥 Associe chaque mot de passe à un utilisateur
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
}
