using Combat.Domain.Entities;
using Combat.Domain.Repositories;
using Combat.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Combat.Infrastructure.Persistence.Repositories
{
    public class CombatRepository(CombatDbContext dbContext) : ICombatRepository
    {
        private readonly CombatDbContext _dbContext = dbContext;

        public async Task<CombatEncounter?> GetByIdAsync(CombatId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Combats
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<CombatEncounter>> GetAllByHeroIdAsync(Guid heroId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Combats
                .Where(c => c.Hero.EntityId == heroId)
                .OrderByDescending(c => c.StartedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(CombatEncounter combat, CancellationToken cancellationToken = default)
        {
            await _dbContext.Combats.AddAsync(combat, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CombatEncounter combat, CancellationToken cancellationToken = default)
        {
            _dbContext.Combats.Update(combat);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}