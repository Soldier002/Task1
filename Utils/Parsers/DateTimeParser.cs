using Ardalis.GuardClauses;
using Interfaces.Utils.Parsers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Parsers
{
    public class DateTimeParser : IDateTimeParser
    {
        public DateTime Parse(string str)
        {
            Guard.Against.NullOrWhiteSpace(str);
            var result = DateTime.ParseExact(str, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);

            return result;
        }
    }
}
