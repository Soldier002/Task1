using Domain.Persistence.TableStorage.Models.Entities;

namespace Domain.Services.Services
{
    public interface IGetLogsForPeriodService
    {
        Task<IList<WeatherApiCallLog>> Execute(DateTime from, DateTime to, CancellationToken ct);
    }
}
