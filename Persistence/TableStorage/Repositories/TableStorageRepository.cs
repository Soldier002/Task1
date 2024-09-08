using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Interfaces.Persistence.TableStorage.Mappers;
using Interfaces.Persistence.TableStorage.Models.Entities;
using Interfaces.Persistence.TableStorage.Repositories;
using Interfaces.Utils.Configuration;

namespace Persistence.TableStorage.Repositories
{
    public class TableStorageRepository : ITableStorageRepository
    {
        private readonly IConfigurationManager _configurationManager;
        private readonly IKeysMapper _keysMapper;

        public TableStorageRepository(IConfigurationManager configurationManager, IKeysMapper keysMapper)
        {
            _configurationManager = configurationManager;
            _keysMapper = keysMapper;
        }

        public async Task Save(WeatherApiCallLog weatherApiCallLog)
        {
            var connectionString = _configurationManager.AzureWebJobsStorage;
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient("weatherData");

            await tableClient.CreateIfNotExistsAsync();
            await tableClient.AddEntityAsync(weatherApiCallLog);
        }

        public async Task<IList<WeatherApiCallLog>> GetAll(DateTime from, DateTime to)
        {
            var fromKeys = _keysMapper.Map(from);
            var toKeys = _keysMapper.Map(to);

            var connectionString = _configurationManager.AzureWebJobsStorage;
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient("weatherData");
            await tableClient.CreateIfNotExistsAsync();

            Expression<Func<WeatherApiCallLog, bool>> oneDayQuery = x =>
                x.PartitionKey == fromKeys.PartitionKey
                && x.RowKey.CompareTo(fromKeys.RowKey) >= 0
                && x.RowKey.CompareTo(toKeys.RowKey) <= 0;

            Expression<Func<WeatherApiCallLog, bool>> manyDaysQuery = x =>
                    (x.PartitionKey.CompareTo(fromKeys.PartitionKey) > 0 && x.PartitionKey.CompareTo(toKeys.PartitionKey) < 0)
                    || (x.PartitionKey.CompareTo(fromKeys.PartitionKey) == 0 && x.RowKey.CompareTo(fromKeys.RowKey) >= 0)
                    || (x.PartitionKey.CompareTo(toKeys.PartitionKey) == 0 && x.RowKey.CompareTo(toKeys.RowKey) <= 0);

            var query = fromKeys.PartitionKey == toKeys.PartitionKey ? oneDayQuery : manyDaysQuery;

            var weatherApiCallLogs = new List<WeatherApiCallLog>();
            await foreach (var page in tableClient.QueryAsync(query).AsPages())
            {
                weatherApiCallLogs.AddRange(page.Values);
            }

            return weatherApiCallLogs;
        }
    }
}
