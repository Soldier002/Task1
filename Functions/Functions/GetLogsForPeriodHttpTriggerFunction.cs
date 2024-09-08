using System;
using System.IO;
using System.Threading.Tasks;
using Interfaces.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.Functions
{
    public class GetLogsForPeriodHttpTriggerFunction
    {
        private readonly IGetLogsForPeriodService _getLogsForPeriodService;

        public GetLogsForPeriodHttpTriggerFunction(IGetLogsForPeriodService getLogsForPeriodService)
        {
            _getLogsForPeriodService = getLogsForPeriodService;
        }

        [FunctionName("GetLogsForPeriodHttpTriggerFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest request, ILogger logger)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            var data = await _getLogsForPeriodService.Execute(request);

            return new OkObjectResult(data);
        }
    }
}
