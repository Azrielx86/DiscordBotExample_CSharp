using System.Text;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DiscordBot.Services;
using DiscordBot.Models;

namespace DiscordBot.Commands
{
    public class BotCommandsModule : BaseCommandModule
    {
        public INekoService? Neko { private get; set; }
        public IAnimeChanService? AnimeChan { private get; set; }

        [Command("greet"), Description("Greets a member")]
        public async Task GreetCommant(CommandContext ctx, DiscordMember member)
        {
            await ctx.RespondAsync($"Hello {member.Mention}!");
        }

        [Command("random"), Description("Returns a random number.")]
        public async Task RandomCommand(CommandContext ctx, int min, int max)
        {
            var rnd = new Random();
            await ctx.RespondAsync($"Your number is: {rnd.Next(min, max)}");
        }

        [Command("neko"), Description("Gets an cute anime neko girl")]
        public async Task NekoCommand(CommandContext ctx)
        {
            NekoModel? neko = await Neko!.Get();
            if (neko is null || neko.results is null)
            {
                await ctx.RespondAsync("I can't search for nekos right now :(");
                return;
            }

            var embedMessage = new DiscordEmbedBuilder()
            {
                ImageUrl = neko.results[0].url,
                Title = neko.results[0].artist_name,
                Url = neko.results[0].source_url,
                Description = "There is a cute neko for you!"
            };

            await ctx.RespondAsync(embedMessage.Build());
        }

        [Command("quote"), Description("Search for an random anime quote")]
        public async Task QuoteCommand(CommandContext ctx)
        {
            AnimeChanModel result = await AnimeChan!.Get();

            if (result is null)
            {
                await ctx.RespondAsync("I can't search for quotes right now.");
                return;
            }

            var sb = new StringBuilder();
            sb
            .AppendLine($"\"{result.quote}\"")
            .AppendLine($"- {result.character} ({result.anime})");
            await ctx.RespondAsync(sb.ToString());
        }

        [Command("quote"), Description("Search for an anime quote")]
        public async Task QuoteCommand(CommandContext ctx, [Description("The anime title you want to search.")] params string[] name)
        {
            AnimeChanModel result = await AnimeChan!.Get(string.Join(' ', name));

            if (result is null)
            {
                await ctx.RespondAsync("I couldn't find any quote :(");
                return;
            }

            var sb = new StringBuilder();
            sb
            .AppendLine($"\"{result.quote}\"")
            .AppendLine($"- {result.character} ({result.anime})");
            await ctx.RespondAsync(sb.ToString());
        }

        [Command("current"), Description("Gets the current channel")]
        public async Task CurrentChannelCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"Current channel: {ctx.Channel.Name}");
        }
    }
}