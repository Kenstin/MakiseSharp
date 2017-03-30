using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MakiseSharp.Utility
{
    public class StringStream
    {
        private const int LengthBytes = 4;
        private readonly Stream ioStream;
        private readonly Encoding streamEncoding;

        public StringStream(Stream stream)
        {
            ioStream = stream;
            streamEncoding = new UnicodeEncoding();
        }

        public async Task<string> ReadMessage()
        {
            var length = new byte[4];
            if (ioStream.Read(length, 0, LengthBytes) != 4)
            {
                return string.Empty;
            }

            var len = BitConverter.ToInt32(length, 0);
            var message = new byte[len];
            int read = 0;
            do
            {
                read += await ioStream.ReadAsync(message, read, len - read);
            }
            while (read < len);

            return streamEncoding.GetString(message);
        }

        public async Task<int> WriteMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return 0;
            }

            var buffer = streamEncoding.GetBytes(message);
            int len = buffer.Length; //overflow

            var length = BitConverter.GetBytes(len);
            await ioStream.WriteAsync(length, 0, LengthBytes);
            await ioStream.WriteAsync(buffer, 0, len);
            await ioStream.FlushAsync();

            return LengthBytes + len;
        }
    }
}
