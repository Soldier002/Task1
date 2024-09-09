using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Azure.Core;
using Domain.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Services.Services;

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
        public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo timerInfo, ILogger logger, CancellationToken ct)
        {
            Guard.Against.Null(logger);

            try
            {
                await _getLondonWeatherDataService.Execute(DateTime.UtcNow, ct);
            }
            catch (OperationCanceledException)
            {
                if (ct.IsCancellationRequested)
                {
                    logger.LogInformation("GetLondonWeatherDataFunction canceled by host.");
                }

                throw;
            }
        }
    }
}
