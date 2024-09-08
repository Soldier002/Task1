using Interfaces.Persistence.TableStorage.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Persistence.TableStorage.Repositories
{
    public interface ITableStorageRepository
    {
        Task Save(WeatherApiCallLog weatherApiCallLog);
    }
}
