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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string from = req.Query["from"];
            string to = req.Query["to"];

            var fromDt = DateTime.Parse(from);
            var toDt = DateTime.Parse(to);

            var entities = await _getLogsForPeriodService.Execute(fromDt, toDt);

            var data = JsonConvert.SerializeObject(entities);

            return new OkObjectResult(data);
        }
    }
}
