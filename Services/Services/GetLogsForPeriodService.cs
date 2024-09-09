using Ardalis.GuardClauses;
using Domain.Common.Parsers;
using Domain.Persistence.TableStorage.Repositories;
using Domain.Services.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Services.Services
{
    public class GetLogsForPeriodService : IGetLogsForPeriodService
    {
        private readonly ITableStorageRepository _tableStorageRepository;
        private readonly IDateTimeParser _dateTimeParser;

        public GetLogsForPeriodService(ITableStorageRepository tableStorageRepository, IDateTimeParser dateTimeParser)
        {
            _tableStorageRepository = tableStorageRepository;
            _dateTimeParser = dateTimeParser;
        }

        public async Task<string> Execute(HttpRequest httpRequest)
        {
            Guard.Against.Null(httpRequest);
            Guard.Against.Null(httpRequest.Query);

            var from = _dateTimeParser.Parse(httpRequest.Query["from"].ToString());
            var to = _dateTimeParser.Parse(httpRequest.Query["to"].ToString());

            var entities = await _tableStorageRepository.GetAll(from, to);
            var data = JsonConvert.SerializeObject(entities);

            return data;
        }
    }
}
