using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MakiseSharp.Common;
using MakiseSharp.Models;
using MakiseSharp.Utility;

namespace MakiseSharp.Modules
{
    public class TravisModule : ModuleBase<SocketCommandContext>
    {
        public static async Task ProcessWebhook(TravisWebhookModel data, DiscordSocketClient client, Configuration config)
        {
            var guildID = config.GuildID;
            var channelID = config.ChannelID;
            var eAuthor = new EmbedAuthorBuilder { Name = data.author_name, Url = data.compare_url.ToString() }; // Url = githubowy profil? iconurl ikonka z profilu
            var eFooter = new EmbedFooterBuilder
            {
                IconUrl = "https://travis-ci.com/images/logos/TravisCI-Mascot-1.png",
                Text = "Travis CI"
            };
            var color = data.Failed ? Colors.Red : Colors.Green;
            var e = new EmbedBuilder
            {
                Author = eAuthor,
                Footer = eFooter,
                Color = color,
                Title = data.pull_request ? $"Pull Request #{data.pull_request_number} {data.StatusMessage}"
                : $"Build #{data.Number} {data.StatusMessage}",
                Url = data.build_url.ToString(),
            };
            var field = new EmbedFieldBuilder
            {
                Name = $"[{data.repository.name}:{data.branch}] {data.pull_request_title}",
                Value = $"`{data.commit_message}`",
                IsInline = false
            };
            e.AddField(field);

            await ((Task)client?.GetGuild(guildID)
                ?.GetTextChannel(channelID)
                ?.SendMessageAsync(string.Empty, embed: e.Build()) ?? Task.FromResult(0));
        }
    }
}
