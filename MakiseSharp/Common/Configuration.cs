using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MakiseSharp.Common
{
    class Configuration
    {
        [JsonIgnore]
        public static string FileName { get; } = "config/configuration.json";

        /// <summary> Gets or sets ids of users who will have owner access to the bot. </summary>
        public ulong[] Owners { get; set; } = { };

        /// <summary> Gets or sets your bot's command prefix. </summary>
        public string Prefix { get; set; } = "!";

        /// <summary> Gets or sets your bot's login token. </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary> Load the configuration from the path specified in FileName. </summary>
        /// <returns> Deserialized configuration </returns>
        public static Configuration Get()
        {
            EnsureExists();
            string file = Path.Combine(AppContext.BaseDirectory, FileName);
            var config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(file));
            Console.WriteLine("Configuration loaded.");
            Console.WriteLine($"Using token: {config.Token}");
            Console.WriteLine($"Using prefix: {config.Prefix}");
            Console.WriteLine($"Bot owners' IDs': {string.Join(", ", config.Owners)}");
            return config;
        }

        private static void EnsureExists()
        {
            string file = Path.Combine(AppContext.BaseDirectory, FileName);
            if (!File.Exists(file)) // Check if the configuration file exists.
            {
                string path = Path.GetDirectoryName(file); // Create config directory if doesn't exist.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var config = new Configuration(); // Create a new configuration object.

                Console.WriteLine("Please enter your token: ");
                var token = Console.ReadLine(); // Read the bot token from console.

                config.Token = token;
                config.SaveJson(); // Save the new configuration object to file.
            }
        }

        /// <summary> Save the configuration to the path specified in FileName. </summary>
        private void SaveJson()
        {
            string file = Path.Combine(AppContext.BaseDirectory, FileName);
            File.WriteAllText(file, ToJson());
        }

        /// <summary> Convert the configuration to a json string. </summary>
        /// <returns> Serialized configuration json string</returns>
        private string ToJson()
            => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
