using System.Text.Json;
using DiscordBot.Models;

namespace DiscordBot.Services
{
    public class AnimeChanService : IAnimeChanService
    {
        private readonly string _base_url = "https://animechan.vercel.app/api/random";
        private HttpClient _httpClient = new HttpClient();

        public async Task<AnimeChanModel> Get()
        {
            var response = await _httpClient.GetAsync(_base_url);
            var content = await response.Content.ReadAsStringAsync();
            var quoteResult = JsonSerializer.Deserialize<AnimeChanModel>(content);
            return quoteResult!;
        }

        public async Task<AnimeChanModel> Get(string name)
        {
            var response = await _httpClient.GetAsync(String.Concat(_base_url, $"/anime?title={name}"));
            var content = await response.Content.ReadAsStringAsync();
            var quoteResult = JsonSerializer.Deserialize<AnimeChanModel>(content);
            return quoteResult!;
        }
    }
}