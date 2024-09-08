using Interfaces.Persistence.TableStorage.Models.Entities;
using Interfaces.Persistence.TableStorage.Repositories;
using Interfaces.Services.Services;
using Persistence.TableStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class GetLogsForPeriodService : IGetLogsForPeriodService
    {
        private readonly ITableStorageRepository _tableStorageRepository;

        public GetLogsForPeriodService(ITableStorageRepository tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        public async Task<IList<WeatherApiCallLog>> Execute(DateTime from, DateTime to)
        {
            return await _tableStorageRepository.GetAll(from, to);
        }
    }
}
