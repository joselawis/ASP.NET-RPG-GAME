using Heroes.Domain.Entities;
using Heroes.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Infrastructure.Persistence
{
    public class HeroesDbContext(DbContextOptions<HeroesDbContext> options) : DbContext(options)
    {
        public DbSet<Hero> Heroes => Set<Hero>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HeroConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}