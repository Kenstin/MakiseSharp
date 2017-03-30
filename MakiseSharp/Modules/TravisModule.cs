using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MakiseSharp.Models;
using MakiseSharp.Utility;

namespace MakiseSharp.Modules
{
    public class TravisModule : ModuleBase<SocketCommandContext>
    {
        private const ulong ChID = 272089398445735936;
        private const ulong GuildID = 242641834298441729;

        public static async Task ProcessWebhook(TravisWebhookModel data, DiscordSocketClient client)
        {
            var eAuthor = new EmbedAuthorBuilder { Name = data.author_name, Url = data.compare_url.ToString() }; // Url = githubowy profil? iconurl ikonka z profilu
            var eFooter = new EmbedFooterBuilder
            {
                IconUrl = "https://enterprise.travis-ci.com/img/mascot.png",
                Text = "Travis CI"
            };
            //TODO: pull requesty
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
            //TODO CRON API
            var field = new EmbedFieldBuilder
            {
                Name = $"[{data.repository.name}:{data.branch}] {data.pull_request_title}",
                Value = $"`{data.commit_message}`",
                IsInline = false
            };
            e.AddField(field);

            await ((Task)client?.GetGuild(GuildID)
                ?.GetTextChannel(ChID)
                ?.SendMessageAsync(string.Empty, embed: e.Build()) ?? Task.FromResult(0));
        }

        [Command("say")]
        public async Task Say([Remainder] string msg)
        {
            await ReplyAsync(msg);
        }
    }
}
