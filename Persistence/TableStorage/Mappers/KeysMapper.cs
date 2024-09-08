using Interfaces.Persistence.TableStorage.Mappers;
using Interfaces.Persistence.TableStorage.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.TableStorage.Mappers
{
    public class KeysMapper : IKeysMapper
    {
        public Keys Map(DateTime dateTime)
        {
            return new Keys
            {
                PartitionKey = dateTime.ToString("yyyyMMdd"),
                RowKey = dateTime.ToString("HHmmss"),
            };
        }
    }
}
