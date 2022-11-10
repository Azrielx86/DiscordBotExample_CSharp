using System.Text;
using DSharpPlus;
using DSharpPlus.Interactivity;
using DiscordBot.Commands;
using DiscordBot.Services;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.DependencyInjection;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity.Enums;
using DiscordBot.Interactivity;
using Newtonsoft.Json;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var json = "";
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();
            if (json is null) throw new Exception("Json invalid.");
            var cfgjson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = cfgjson.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged,
#if DEBUG
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
#endif
            });

            discord.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(30),
            });

            var services = new ServiceCollection()
            .AddSingleton<INekoService, NekoService>()
            .AddSingleton<IAnimeChanService, AnimeChanService>()
            .BuildServiceProvider();

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { cfgjson.CommandPrefix },
                Services = services
            });

            commands.RegisterCommands<BotCommandsModule>();
            commands.RegisterCommands<BotInteractivityModule>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }

    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string CommandPrefix { get; private set; }
    }
}