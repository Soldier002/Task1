using Ardalis.GuardClauses;
using Domain.Persistence.TableStorage.Models.Entities;
using Domain.Persistence.TableStorage.Repositories;
using Domain.Services.Services;
using Newtonsoft.Json;

namespace Services.Services
{
    public class GetLogsForPeriodService : IGetLogsForPeriodService
    {
        private readonly ITableStorageRepository _tableStorageRepository;

        public GetLogsForPeriodService(ITableStorageRepository tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        public async Task<IList<WeatherApiCallLog>> Execute(DateTime from, DateTime to, CancellationToken ct)
        {
            var entities = await _tableStorageRepository.GetAll(from, to, ct);

            return entities;
        }
    }
}
