using Microsoft.EntityFrameworkCore;
using Vtope.Domain;

namespace Vtope.Models;

public class VtopeDbContext : DbContext
{
    public VtopeDbContext(DbContextOptions<VtopeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstaAccount>().HasIndex(x => x.Username).IsUnique();
        modelBuilder.Entity<InstaAccount>().Property(x => x.Username).IsRequired().HasMaxLength(30);
        
        modelBuilder.Entity<InstaAccount>().Property(x => x.Password).IsRequired().HasMaxLength(30);
    }

    public DbSet<InstaAccount> InstaAccounts { get; set; }
}