using Azure.Data.Tables;
using Interfaces.Persistence.TableStorage.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
