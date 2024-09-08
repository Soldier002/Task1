using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Persistence.BlobStorage
{
    public interface IBlobStorageRepository
    {
        Task<string> SaveWeatherData(Stream weatherData, DateTime now);
    }
}
