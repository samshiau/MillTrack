using Microsoft.EntityFrameworkCore;
using MillBackend.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define your database tables (DbSets)
    public DbSet<User> Users { get; set; }
}
