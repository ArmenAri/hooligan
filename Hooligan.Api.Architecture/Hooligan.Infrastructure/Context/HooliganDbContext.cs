using Hooligan.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hooligan.Infrastructure.Context;

public class HooliganDbContext(DbContextOptions<HooliganDbContext> options) : DbContext(options)
{
    public DbSet<Association>? Associations { get; init; }
}