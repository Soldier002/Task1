using Azure.Data.Tables;
using Domain.Persistence.TableStorage.Clients;

namespace Persistence.TableStorage.Clients
{
    public class TableClientFactory : ITableClientFactory
    {
        private readonly TableClient _tableClient;

        private bool _initialized;

        public TableClientFactory(TableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public async Task<TableClient> Create()
        {
            if (!_initialized)
            {
                await _tableClient.CreateIfNotExistsAsync();
                _initialized = true;
            }

            return _tableClient;
        }
    }
}
