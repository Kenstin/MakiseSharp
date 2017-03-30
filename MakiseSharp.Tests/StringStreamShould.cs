using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MakiseSharp.Utility;

namespace MakiseSharp.Tests
{
    public class StringStreamShould
    { 
        [Fact]
        public async Task WriteAndReadTheSameThing()
        {
            var stream = new MemoryStream(); //moze cos w konstruktorze
            var stringStream = new StringStream(stream);
            await stringStream.WriteMessage("Hejka");
            await stringStream.WriteMessage("1");
            stream.Position = 0;

            Assert.Equal("Hejka", await stringStream.ReadMessage());
            Assert.Equal("1", await stringStream.ReadMessage());
        }
    }
}
