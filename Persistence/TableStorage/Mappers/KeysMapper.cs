using Domain.Common.Strings;
using Domain.Persistence.TableStorage.Mappers;
using Domain.Persistence.TableStorage.Models.Dtos;

namespace Persistence.TableStorage.Mappers
{
    public class KeysMapper : IKeysMapper
    {
        public Keys Map(DateTime dateTime)
        {
            return new Keys
            {
                PartitionKey = dateTime.ToString(DateTimeFormats.PartitionKeyDateFormat),
                RowKey = dateTime.ToString(DateTimeFormats.RowKeyTimeFormat),
            };
        }
    }
}
