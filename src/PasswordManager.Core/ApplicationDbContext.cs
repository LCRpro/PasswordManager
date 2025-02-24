using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Models;

namespace PasswordManager.Core;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    // Ajoute ce constructeur pour permettre la création de migrations
    public ApplicationDbContext() {}

    public DbSet<PasswordEntry> Passwords { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=passwordmanager.db");
        }
    }
}
