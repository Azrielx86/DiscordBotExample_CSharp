using DiscordBot.Models;

namespace DiscordBot.Services
{
    public interface INekoService
    {
        public Task<NekoModel?> Get();
    }
}