namespace Combat.Application.Common.Interfaces
{
    public interface IHeroServiceClient
    {
        Task<HeroData?> GetHeroAsync(Guid heroId, CancellationToken cancellationToken = default);
    }

    public record HeroData(
        Guid Id,
        string Name,
        string Class,
        int Level,
        HeroStats Stats
    );

    public record HeroStats(
        int Health,
        int Attack,
        int Defense,
        int Speed
    );
}