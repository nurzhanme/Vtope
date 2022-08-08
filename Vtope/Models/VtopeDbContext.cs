using Microsoft.EntityFrameworkCore;

namespace Vtope.Models;

public class VtopeDbContext : DbContext
{
    public VtopeDbContext(DbContextOptions<VtopeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<InstaAccount> InstaAccounts { get; set; }
}