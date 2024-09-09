using System.Linq.Expressions;
using Domain.Persistence.TableStorage.Clients;
using Domain.Persistence.TableStorage.Mappers;
using Domain.Persistence.TableStorage.Models.Entities;
using Domain.Persistence.TableStorage.Repositories;

namespace Persistence.TableStorage.Repositories
{
    public class TableStorageRepository : ITableStorageRepository
    {
        private readonly IKeysMapper _keysMapper;
        private readonly ITableClientFactory _tableClientFactory;

        public TableStorageRepository(IKeysMapper keysMapper, ITableClientFactory tableClientFactory)
        {
            _keysMapper = keysMapper;
            _tableClientFactory = tableClientFactory;
        }

        public async Task Save(WeatherApiCallLog weatherApiCallLog)
        {
            var tableClient = await _tableClientFactory.Create();
            await tableClient.AddEntityAsync(weatherApiCallLog);
        }

        public async Task<IList<WeatherApiCallLog>> GetAll(DateTime from, DateTime to, CancellationToken ct)
        {
            var fromKeys = _keysMapper.Map(from);
            var toKeys = _keysMapper.Map(to);

            Expression<Func<WeatherApiCallLog, bool>> oneDayQuery = x =>
                x.PartitionKey == fromKeys.PartitionKey
                && x.RowKey.CompareTo(fromKeys.RowKey) >= 0
                && x.RowKey.CompareTo(toKeys.RowKey) <= 0;

            Expression<Func<WeatherApiCallLog, bool>> manyDaysQuery = x =>
                    (x.PartitionKey.CompareTo(fromKeys.PartitionKey) > 0 && x.PartitionKey.CompareTo(toKeys.PartitionKey) < 0)
                    || (x.PartitionKey.CompareTo(fromKeys.PartitionKey) == 0 && x.RowKey.CompareTo(fromKeys.RowKey) >= 0)
                    || (x.PartitionKey.CompareTo(toKeys.PartitionKey) == 0 && x.RowKey.CompareTo(toKeys.RowKey) <= 0);

            var query = fromKeys.PartitionKey == toKeys.PartitionKey ? oneDayQuery : manyDaysQuery;
            var tableClient = await _tableClientFactory.Create();
            var weatherApiCallLogs = new List<WeatherApiCallLog>();

            ct.ThrowIfCancellationRequested();
            await foreach (var page in tableClient.QueryAsync(query, cancellationToken: ct).AsPages())
            {
                ct.ThrowIfCancellationRequested();
                weatherApiCallLogs.AddRange(page.Values);
            }

            return weatherApiCallLogs;
        }
    }
}
