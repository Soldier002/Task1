using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Persistence.TableStorage.Models.Dtos
{
    public class Keys
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}
