using Ardalis.GuardClauses;
using Azure.Core;
using Interfaces.Persistence.TableStorage.Models.Entities;
using Interfaces.Persistence.TableStorage.Repositories;
using Interfaces.Services.Services;
using Interfaces.Utils.Parsers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Persistence.TableStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
