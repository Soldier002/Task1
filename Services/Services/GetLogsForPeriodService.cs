using Ardalis.GuardClauses;
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

        public async Task<string> Execute(DateTime from, DateTime to)
        {
            Guard.Against.Default(from);
            Guard.Against.Default(to);

            var entities = await _tableStorageRepository.GetAll(from, to);
            var data = JsonConvert.SerializeObject(entities);

            return data;
        }
    }
}
