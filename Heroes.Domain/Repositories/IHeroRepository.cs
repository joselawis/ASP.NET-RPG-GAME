using Heroes.Domain.ValueObjects;

namespace Heroes.Domain.Repositories
{
    public interface IHeroRepository
    {
        Task<Hero?> GetByIdAsync(HeroId id, CancellationToken cancellationToken = default);

        Task<List<Hero>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Hero hero, CancellationToken cancellationToken = default);

        Task UpdateAsync(Hero hero, CancellationToken cancellationToken = default);

        Task DeleteAsync(Hero hero, CancellationToken cancellationToken = default);
    }
}