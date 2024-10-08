﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Persistence.BlobStorage.Repositories
{
    public interface IBlobStorageRepository
    {
        Task SaveWeatherData(Stream weatherData, DateTime now, CancellationToken ct);

        Task<Stream> GetWeatherData(string blobName, CancellationToken ct);
    }
}
