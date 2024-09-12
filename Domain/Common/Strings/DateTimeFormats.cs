using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Strings
{
    public static class DateTimeFormats
    {
        public static string PartitionKeyDateFormat => "yyyyMMdd";

        public static string RowKeyTimeFormat => "HHmmss";

        public static string Iso8601Format => "yyyy-MM-ddTHH:mm:ss";
    }
}
