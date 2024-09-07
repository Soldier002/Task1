using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Interfaces.Services.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Functions.Functions
{
    public class GetLondonWeatherDataTimerFunction
    {
        private readonly IGetLondonWeatherDataService _getLondonWeatherDataService;

        public GetLondonWeatherDataTimerFunction(IGetLondonWeatherDataService getLondonWeatherDataService)
        {
            _getLondonWeatherDataService = getLondonWeatherDataService;
        }

        [FunctionName("GetLondonWeatherDataTimerFunction")]
        public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("GetLondonWeatherDataTimerFunction start");
            await _getLondonWeatherDataService.Execute();
            log.LogInformation("GetLondonWeatherDataTimerFunction end");
        }
    }
}
