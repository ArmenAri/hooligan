using Hooligan.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hooligan.Infrastructure.Context;

public class HooliganDbContext(DbContextOptions<HooliganDbContext> options) : DbContext(options)
{
    public required DbSet<Association> Associations { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Association>()
            .HasKey(a => new { a.First, a.Second });
    }
}