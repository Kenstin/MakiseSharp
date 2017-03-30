﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MakiseSharp.Models;
using MakiseSharp.Modules;
using Newtonsoft.Json;

namespace MakiseSharp
{
    internal class Bot
    {
        private const string Token = "Mjg5NTEyNjE2NTU5Mzc4NDMy.C6NdJg.K_NfXCH1oCHVdINApOIt9jz1ebU";
        private readonly DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            LogLevel = LogSeverity.Info
        });

        private readonly IDependencyMap dependencyMap = new DependencyMap();
        private readonly CommandHandler commandHandler = new CommandHandler();

        public async Task TravisNotification(string json)
        {
            if (this.client == null || string.IsNullOrEmpty(json))
            {
                Console.WriteLine("Travis not notified cuz of null client or empty string");
                return;
            }

            var data = JsonConvert.DeserializeObject<TravisWebhookModel>(json);
            if (data != null)
            {
                await TravisModule.ProcessWebhook(data, this.client).ConfigureAwait(false);
            }
        }

        public async Task WriteToGeneral(string data)
        {
            if (this.client == null || string.IsNullOrEmpty(data))
            {
                Console.WriteLine("client or data is null");
                return;
            }

            await ((Task)this.client?.GetGuild(242641834298441729)?.GetTextChannel(242641834298441729)?.SendMessageAsync(data) ?? Task.FromResult(0));
        }

        public async Task Login()
        {
            await this.Configure();

            // Configure the client to use a Bot token, and use our token
            await this.client.LoginAsync(TokenType.Bot, Token);
            // Connect the client to Discord's gateway
            await this.client.StartAsync();
        }

        private async Task Configure()
        {
            this.dependencyMap.Add(this.client);
            await this.commandHandler.Install(this.client);

            this.client.Log += message =>
            {
                Console.WriteLine($"{message.ToString()}");
                return Task.CompletedTask;
            };
        }
    }
}
