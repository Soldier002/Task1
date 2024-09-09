using System.Threading.Tasks;
using Domain.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions.Functions.HttpTrigger
{
    public class GetLogsForPeriodFunction
    {
        private readonly IGetLogsForPeriodService _getLogsForPeriodService;

        public GetLogsForPeriodFunction(IGetLogsForPeriodService getLogsForPeriodService)
        {
            _getLogsForPeriodService = getLogsForPeriodService;
        }

        [FunctionName("GetLogsForPeriodFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest request, ILogger logger)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            var data = await _getLogsForPeriodService.Execute(request);

            return new OkObjectResult(data);
        }
    }
}
