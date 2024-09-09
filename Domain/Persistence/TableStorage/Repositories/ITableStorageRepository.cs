using Domain.Persistence.TableStorage.Models.Entities;

namespace Domain.Persistence.TableStorage.Repositories
{
    public interface ITableStorageRepository
    {
        Task Save(WeatherApiCallLog weatherApiCallLog, CancellationToken ct);

        Task<IList<WeatherApiCallLog>> GetAll(DateTime from, DateTime to, CancellationToken ct);
    }
}
