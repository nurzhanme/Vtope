using Microsoft.EntityFrameworkCore;
using Vtope.Domain;

namespace Vtope.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
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