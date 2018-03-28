using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using MakiseSharp.Common;
using MakiseSharp.Models;
using MakiseSharp.Modules;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MakiseSharp
{
    internal class Bot
    {
        private readonly DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            LogLevel = LogSeverity.Info
        });

        private readonly CommandHandler commandHandler = new CommandHandler();

        private IServiceProvider services;

        public async Task TravisNotification(string json)
        {
            if (client == null || string.IsNullOrEmpty(json))
            {
                Console.WriteLine("Travis not notified cuz of null client or empty string");
                return;
            }

            TravisWebhookModel data;
            try
            {
                data = JsonConvert.DeserializeObject<TravisWebhookModel>(json);
            }
            catch (Exception)
            {
                return;
            }

            if (data != null)
            {
                await TravisModule.ProcessWebhook(data, client).ConfigureAwait(false);
            }
        }

        public async Task WriteToGeneral(string data)
        {
            if (client == null || string.IsNullOrEmpty(data))
            {
                Console.WriteLine("client or data is null");
                return;
            }

            await ((Task)client?.GetGuild(242641834298441729)?.GetTextChannel(242641834298441729)?.SendMessageAsync(data) ?? Task.FromResult(0));
        }

        public async Task Start()
        {
            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(Configuration.Get())
                .BuildServiceProvider();
            await Configure();

            // Configure the client to use a Bot token, and use our token
            await client.LoginAsync(TokenType.Bot, services.GetService<Configuration>().Token);
            // Connect the client to Discord's gateway
            await client.StartAsync();
        }

        private async Task Configure()
        {
            await commandHandler.Install(client, services);

            client.Log += message =>
            {
                Console.WriteLine($"{message.ToString()}");
                return Task.CompletedTask;
            };
        }
    }
}
