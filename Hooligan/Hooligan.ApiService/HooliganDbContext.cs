using Hooligan.ApiService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hooligan.ApiService
{
    public class HooliganDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public HooliganDbContext(DbContextOptions<HooliganDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    }
}
