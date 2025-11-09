namespace Combat.API.Controllers
{
    public sealed record InitiateCombatRequest(Guid HeroId, Guid EnemyId);
}