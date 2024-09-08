using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Interfaces.Persistence.BlobStorage.Clients;
using Interfaces.Persistence.TableStorage.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
