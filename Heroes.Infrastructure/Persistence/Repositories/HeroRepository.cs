using Heroes.Domain.Entities;
using Heroes.Domain.Repositories;
using Heroes.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Infrastructure.Persistence.Repositories
{
    public class HeroRepository(HeroesDbContext dbContext) : IHeroRepository
    {
        private readonly HeroesDbContext _dbContext = dbContext;

        public async Task AddAsync(Hero hero, CancellationToken cancellationToken = default)
        {
            await _dbContext.Heroes.AddAsync(hero, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Hero hero, CancellationToken cancellationToken = default)
        {
            _dbContext.Heroes.Remove(hero);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Hero>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Heroes
                 .OrderByDescending(h => h.Level)
                 .ThenBy(h => h.Name)
                 .ToListAsync(cancellationToken);
        }

        public async Task<Hero?> GetByIdAsync(HeroId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Heroes.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Hero hero, CancellationToken cancellationToken = default)
        {
            _dbContext.Heroes.Update(hero);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}