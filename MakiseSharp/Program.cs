using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                var pClient = new NamedPipeClientStream(".", "MakiseeSharp", PipeDirection.InOut);
                await pClient.ConnectAsync();
                pClient.ReadMode = PipeTransmissionMode.Message;
                for (;;)
                {
                    var buffer = new byte[2048];
                    var sb = new StringBuilder();
                    sb.EnsureCapacity(2048);
                    int read;
                    do
                    {
                        read = await pClient.ReadAsync(buffer, 0, buffer.Length);
                        sb.Append(Encoding.ASCII.GetString(buffer, 0, read));
                    }
                    while (read > 0 && !pClient.IsMessageComplete);

                    if (read == 0)
                    {
                        pClient.Dispose();
                        break;
                    }

                    await bot.TravisNotification(sb.ToString());
                }
            }
        }
    }
}