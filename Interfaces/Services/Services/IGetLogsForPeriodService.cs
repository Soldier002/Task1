using Interfaces.Persistence.TableStorage.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services.Services
{
    public interface IGetLogsForPeriodService
    {
        Task<IList<WeatherApiCallLog>> Execute(DateTime from, DateTime to);
    }
}
