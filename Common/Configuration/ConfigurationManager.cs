using Domain.Common.Configuration;
using System.Runtime.CompilerServices;

namespace Common.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        // pretend it is in key vault
        public string AzureWebJobsStorage => GetConfig();

        public string TableClientName => GetConfig();

        public string BlobContainerName => GetConfig();

        // pretend it is in key vault
        public string WeatherApiKey => GetConfig();

        private string GetConfig([CallerMemberName] string callerMemberName = "")
        {
            if (string.IsNullOrEmpty(callerMemberName))
            {
                throw new ArgumentException($"{nameof(callerMemberName)} argument null or empty");
            }

            var value = Environment.GetEnvironmentVariable(callerMemberName);
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Configuration for key \"{callerMemberName}\" missing");
            }

            return value;
        }
    }
}
