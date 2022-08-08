using Microsoft.EntityFrameworkCore;

namespace Vtope.Models;

public class VtopeDbContext : DbContext
{
    public VtopeDbContext(DbContextOptions<VtopeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstaAccount>().HasIndex(x => x.Username).IsUnique();
        modelBuilder.Entity<InstaAccount>().Property(x => x.Username).IsRequired();
    }

    public DbSet<InstaAccount> InstaAccounts { get; set; }
}