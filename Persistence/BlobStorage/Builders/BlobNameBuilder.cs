using Domain.Common.Strings;
using Domain.Persistence.BlobStorage.Builders;
using Domain.Persistence.TableStorage.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BlobStorage.Builders
{
    public class BlobNameBuilder : IBlobNameBuilder
    {
        public string Build(DateTime dateTime) => 
            $"{dateTime.ToString($"{DateTimeFormats.PartitionKeyDateFormat}_{DateTimeFormats.RowKeyTimeFormat}")}_weather";

        public string Build(Keys keys) => $"{keys.PartitionKey}_{keys.RowKey}_weather";
    }
}
