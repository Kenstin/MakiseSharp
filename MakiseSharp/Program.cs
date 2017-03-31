using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MakiseSharp.Utility;
[assembly: InternalsVisibleTo("MakiseSharp.Tests")]

namespace MakiseSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run().GetAwaiter().GetResult();
        }

        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1002:SemicolonsMustBeSpacedCorrectly", Justification = "Reviewed.")]
        private async Task Run()
        {
            var bot = new Bot();

            await bot.Login();
            for (;;)
            {
                var pClient = new NamedPipeClientStream(".", "MakiseSharp", PipeDirection.InOut);
                await pClient.ConnectAsync();
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