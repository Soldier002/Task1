using Azure.Storage.Blobs;

namespace Interfaces.Persistence.BlobStorage.Clients
{
    public interface IBlobContainerClientFactory
    {
        Task<BlobContainerClient> Create();
    }
}