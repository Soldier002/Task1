using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Domain.Functions.Validators;
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
        private readonly IDateTimeRangeValidator _dateTimeRangeValidator;

        public GetLogsForPeriodFunction(IGetLogsForPeriodService getLogsForPeriodService, IDateTimeRangeValidator dateTimeRangeValidator)
        {
            _getLogsForPeriodService = getLogsForPeriodService;
            _dateTimeRangeValidator = dateTimeRangeValidator;
        }

        [FunctionName("GetLogsForPeriodFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest request, ILogger logger)
        {
            Guard.Against.Null(request);
            Guard.Against.Null(request.Query);
            var validationResult = _dateTimeRangeValidator.Validate(request.Query["from"], request.Query["to"], "from", "to");

            if (!validationResult.Success)
            {
                return new BadRequestObjectResult(new { error = validationResult.ValidationMessages });
            }

            var data = await _getLogsForPeriodService.Execute(validationResult.From, validationResult.To);

            return new OkObjectResult(data);
        }
    }
}
