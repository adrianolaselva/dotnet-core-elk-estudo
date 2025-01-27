using System.IO;
using Microsoft.Extensions.Configuration;

namespace analyst_challenge_test.Helpers
{
    public class ConfigurationSingleton
    {
        private static IConfigurationRoot configuration;

        private ConfigurationSingleton()
        {
        }

        public static IConfigurationRoot GetConfiguration()
        {
            if (configuration is null)
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Path.GetFullPath("../../../")))
                    .AddJsonFile("appsettings.dev.json")
                    .AddEnvironmentVariables()
                    .Build();

            return configuration;
        }
    }
}
