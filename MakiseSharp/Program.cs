using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MakiseSharp.Utility;
[assembly: InternalsVisibleTo("MakiseSharp.Tests")]

namespace MakiseSharp
{
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1002:SemicolonsMustBeSpacedCorrectly", Justification = "Reviewed.")]
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
                try
                {
                    new Program().Run().GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Disconnected: {e.Message}");
                }
            }
        }

        private async Task Run()
        {
            var bot = new Bot();
            await bot.Start();

            for (;;)
            {
                var pClient = new NamedPipeClientStream(".", "makisesharp", PipeDirection.InOut);
                var connected = false;
                while (!connected)
                {
                    try
                    {
                        await pClient.ConnectAsync(100);
                        connected = true;
                    }
                    catch (Exception)
                    {
                        await Task.Delay(20000);
                    }
                }

                for (;;)
                {
                    var streamString = new StringStream(pClient);
                    var message = await streamString.ReadMessage();

                    if (string.IsNullOrEmpty(message))
                    {
                        pClient.Dispose();
                        break;
                    }

                    await bot.TravisNotification(message);
                }
            }
        }
    }
}