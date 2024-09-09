using Azure.Storage.Blobs;
using Domain.Persistence.BlobStorage.Clients;

namespace Persistence.BlobStorage.Clients
{
    public class BlobContainerClientFactory : IBlobContainerClientFactory
    {
        private readonly BlobContainerClient _blobContainerClient;

        private bool _initialized;

        public BlobContainerClientFactory(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }

        public async Task<BlobContainerClient> Create()
        {
            if (!_initialized)
            {
                await _blobContainerClient.CreateIfNotExistsAsync();
                _initialized = true;
            }

            return _blobContainerClient;
        }
    }
}
