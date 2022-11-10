using System.Text;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace DiscordBot.Interactivity
{
    public class BotInteractivityModule : BaseCommandModule
    {
        [Command("react"), Description("Run a poll of reactions")]
        public async Task SelectReactionCommand(CommandContext ctx, DiscordMember member)
        {
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ok_hand:");
            var message = await ctx.RespondAsync($"{member.Mention} react with {emoji}");

            var result = message.WaitForReactionAsync(member, emoji).Result;

            if (!result.TimedOut)
            {
                await ctx.RespondAsync("Thank you!");
            }
        }

        [Command("collect"), Description("Collects reactions")]
        public async Task CollectReactionsCommand(CommandContext ctx)
        {
            var message = await ctx.RespondAsync("React here!");
            var reactions = await message.CollectReactionsAsync();

            var sb = new StringBuilder();
            foreach(var reaction in reactions)
            {
                sb.AppendLine($"{reaction.Emoji}: {reaction.Total}");
            }

            await ctx.RespondAsync(sb.ToString());
        }
    }
}