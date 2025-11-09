using Combat.Application.Common.Interfaces;
using System.Net.Http.Json;

namespace Combat.Infrastructure.Services
{
    public class HeroServiceClient(HttpClient httpClient) : IHeroServiceClient
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<HeroData?> GetHeroAsync(Guid heroId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/heroes/{heroId}", cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var heroDto = await response.Content.ReadFromJsonAsync<HeroDto>(cancellationToken: cancellationToken);

                if (heroDto is null)
                {
                    return null;
                }

                return new HeroData(
                    heroDto.Id,
                    heroDto.Name,
                    heroDto.Class,
                    heroDto.Level,
                    new HeroStats(
                        heroDto.Stats.Health,
                        heroDto.Stats.Attack,
                        heroDto.Stats.Defense,
                        heroDto.Stats.Speed));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private record HeroDto(
          Guid Id,
          string Name,
          string Class,
          int Level,
          StatsDto Stats);

        private record StatsDto(
            int Health,
            int Attack,
            int Defense,
            int Speed);
    }
}