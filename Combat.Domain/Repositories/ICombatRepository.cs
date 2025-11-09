using Combat.Domain.Entities;
using Combat.Domain.ValueObjects;

namespace Combat.Domain.Repositories
{
    public interface ICombatRepository
    {
        Task<CombatEncounter?> GetByIdAsync(CombatId id, CancellationToken cancellationToken = default);

        Task<List<CombatEncounter>> GetAllByHeroIdAsync(Guid heroId, CancellationToken cancellationToken = default);

        Task AddAsync(CombatEncounter combat, CancellationToken cancellationToken = default);

        Task UpdateAsync(CombatEncounter combat, CancellationToken cancellationToken = default);
    }
}