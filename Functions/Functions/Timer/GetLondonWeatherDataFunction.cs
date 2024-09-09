using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Services.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Functions.Functions.Timer
{
    public class GetLondonWeatherDataFunction
    {
        private readonly IGetLondonWeatherDataService _getLondonWeatherDataService;

        public GetLondonWeatherDataFunction(IGetLondonWeatherDataService getLondonWeatherDataService)
        {
            _getLondonWeatherDataService = getLondonWeatherDataService;
        }

        [FunctionName("GetLondonWeatherDataFunction")]
        public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo timerInfo, ILogger log, CancellationToken ct)
        {
            log.LogInformation("GetLondonWeatherDataFunction start");
            await _getLondonWeatherDataService.Execute(DateTime.UtcNow, ct);
            log.LogInformation("GetLondonWeatherDataFunction end");
        }
    }
}
