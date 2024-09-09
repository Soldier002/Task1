using Azure.Data.Tables;

namespace Domain.Persistence.TableStorage.Clients
{
    public interface ITableClientFactory
    {
        Task<TableClient> Create();
    }
}