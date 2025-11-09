namespace Heroes.API.Controllers
{
    public sealed record CreateHeroRequest(string Name, string Class);
    public sealed record GainExperienceRequest(int ExperienceAmount);
}