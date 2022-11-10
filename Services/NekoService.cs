using System.Text.Json;
using DiscordBot.Models;

namespace DiscordBot.Services
{
    public class NekoService : INekoService
    {
        private const string _url = "https://nekos.best/api/v2/neko";

        public async Task<NekoModel?> Get()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_url);
            var content = await response.Content.ReadAsStringAsync();
            var nekoResult = JsonSerializer.Deserialize<NekoModel>(content);
            return nekoResult;
        }
    }
}