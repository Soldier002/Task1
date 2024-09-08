using Azure.Data.Tables;

namespace Interfaces.Persistence.TableStorage.Clients
{
    public interface ITableClientFactory
    {
        Task<TableClient> Create();
    }
}