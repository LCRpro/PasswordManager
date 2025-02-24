using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Models;

namespace PasswordManager.Core;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    public DbSet<PasswordEntry> Passwords { get; set; }
}
