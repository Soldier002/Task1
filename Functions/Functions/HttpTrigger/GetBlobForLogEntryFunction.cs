using System;
using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Domain.Common.Exceptions;
using Domain.Functions.Validators;
using Domain.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using NotFoundException = Domain.Common.Exceptions.NotFoundException;

namespace Functions.Functions.HttpTrigger
{
    public class GetBlobForLogEntryFunction
    {
        private readonly IGetBlobForLogEntryService _getBlobForLogEntryService;
        private readonly IRowKeyValidator _rowKeyValidator;
        private readonly IPartitionKeyValidator _partitionKeyValidator;

        public GetBlobForLogEntryFunction(IGetBlobForLogEntryService getBlobForLogEntryService, IRowKeyValidator rowKeyValidator, IPartitionKeyValidator partitionKeyValidator)
        {
            _getBlobForLogEntryService = getBlobForLogEntryService;
            _rowKeyValidator = rowKeyValidator;
            _partitionKeyValidator = partitionKeyValidator;
        }

        [FunctionName("GetBlobForLogEntryFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest request, ILogger logger, CancellationToken ct)
        {
            Guard.Against.Null(request);
            Guard.Against.Null(request.Query);
            Guard.Against.Null(logger);

            using var ctSource = CancellationTokenSource.CreateLinkedTokenSource(ct, request.HttpContext.RequestAborted);
            var partitionKeyValidationResult = _partitionKeyValidator.Validate(request.Query["partitionKey"], "partitionKey");
            var rowKeyValidationResult = _rowKeyValidator.Validate(request.Query["rowKey"], "rowKey");

            if (!(partitionKeyValidationResult.Success && rowKeyValidationResult.Success))
            {
                return new BadRequestObjectResult(new { error = partitionKeyValidationResult.ValidationMessages + rowKeyValidationResult.ValidationMessages });
            }

            try
            {
                using var blobStream = await _getBlobForLogEntryService.Execute(partitionKeyValidationResult.Value, rowKeyValidationResult.Value, ctSource.Token);
                return new FileStreamResult(blobStream, MediaTypeNames.Application.Octet);
            }
            catch (OperationCanceledException)
            {
                if (request.HttpContext.RequestAborted.IsCancellationRequested)
                {
                    logger.LogInformation("GetBlobForLogEntryFunction canceled by caller.");
                }
                else if (ct.IsCancellationRequested)
                {
                    logger.LogInformation("GetBlobForLogEntryFunction canceled by host.");
                }

                throw;
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
