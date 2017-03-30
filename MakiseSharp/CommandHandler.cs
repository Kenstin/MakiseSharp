using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace MakiseSharp
{
    class CommandHandler
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IDependencyMap map;

        public async Task Install(DiscordSocketClient c)
        {
            this.client = c;                                                 // Save an instance of the discord client.
            this.commands = new CommandService();                                // Create a new instance of the commandservice.

            await this.commands.AddModulesAsync(Assembly.GetEntryAssembly());    // Load all modules from the assembly.

            this.client.MessageReceived += this.HandleCommand;                    // Register the messagereceived event to handle commands.
        }

        public Task ConfigureServices(IDependencyMap map)
        {
            this.map = map;
            map.Add(this.commands);
            return Task.CompletedTask;
        }

        private async Task HandleCommand(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) // Check if the received message is from a user.
            {
                return;
            }

            var context = new SocketCommandContext(this.client, msg);     // Create a new command context.

            int argPos = 0; // Check if the message has either a string or mention prefix.
            if (msg.HasStringPrefix("m!", ref argPos) || //TODO add configuration thingy
                msg.HasMentionPrefix(this.client.CurrentUser, ref argPos))
            { // Try and execute a command with the given context.
                var result = await this.commands.ExecuteAsync(context, argPos);

                if (!result.IsSuccess) // If execution failed, reply with the error message.
                {
                    await context.Channel.SendMessageAsync(result.ToString());
                }
            }
        }
    }
}
