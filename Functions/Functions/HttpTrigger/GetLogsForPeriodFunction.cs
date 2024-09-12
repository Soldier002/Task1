using System;
using System.Threading;
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest request, ILogger logger, CancellationToken ct)
        {
            Guard.Against.Null(request);
            Guard.Against.Null(request.Query);
            Guard.Against.Null(logger);

            using var ctSource = CancellationTokenSource.CreateLinkedTokenSource(ct, request.HttpContext.RequestAborted);
            var validationResult = _dateTimeRangeValidator.Validate(request.Query["from"], request.Query["to"], "from", "to");

            if (!validationResult.Success)
            {
                return new BadRequestObjectResult(new { error = validationResult.ValidationMessages });
            }

            try
            {
                var data = await _getLogsForPeriodService.Execute(validationResult.Value.From, validationResult.Value.To, ctSource.Token);
                return new OkObjectResult(data);
            }
            catch (OperationCanceledException)
            {
                if (request.HttpContext.RequestAborted.IsCancellationRequested)
                {
                    logger.LogInformation("GetLogsForPeriodFunction canceled by caller.");
                }
                else if (ct.IsCancellationRequested)
                {
                    logger.LogInformation("GetLogsForPeriodFunction canceled by host.");
                }

                throw;
            }
        }
    }
}
