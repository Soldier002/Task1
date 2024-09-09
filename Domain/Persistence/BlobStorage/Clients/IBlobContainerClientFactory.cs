using Azure.Storage.Blobs;

namespace Domain.Persistence.BlobStorage.Clients
{
    public interface IBlobContainerClientFactory
    {
        Task<BlobContainerClient> Create();
    }
}