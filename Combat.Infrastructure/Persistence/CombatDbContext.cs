using Combat.Domain.Entities;
using Combat.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Combat.Infrastructure.Persistence
{
    public class CombatDbContext(DbContextOptions<CombatDbContext> options) : DbContext(options)
    {
        public DbSet<CombatEncounter> Combats => Set<CombatEncounter>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CombatEncounterConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}