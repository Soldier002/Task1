using Domain.Persistence.BlobStorage.Builders;
using Domain.Persistence.BlobStorage.Repositories;
using Domain.Persistence.TableStorage.Models.Dtos;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class GetBlobForLogEntryService : IGetBlobForLogEntryService
    {
        private readonly IBlobNameBuilder _blobNameBuilder;
        private readonly IBlobStorageRepository _blobStorageRepository;

        public GetBlobForLogEntryService(IBlobNameBuilder blobNameBuilder, IBlobStorageRepository blobStorageRepository)
        {
            _blobNameBuilder = blobNameBuilder;
            _blobStorageRepository = blobStorageRepository;
        }

        public async Task<Stream> Execute(string partitionKey, string rowKey)
        {
            var keys = new Keys
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,
            };

            var blobName = _blobNameBuilder.Build(keys);
            var blobStream = await _blobStorageRepository.GetWeatherData(blobName);

            return blobStream;
        }
    }
}
