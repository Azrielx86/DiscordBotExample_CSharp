using DiscordBot.Models;

namespace DiscordBot.Services
{
    public interface IAnimeChanService
    {
        public Task<AnimeChanModel> Get();
        public Task<AnimeChanModel> Get(string name);
    }
}