using Ardalis.GuardClauses;
using Domain.Common.Parsers;
using System.Globalization;

namespace Common.Parsers
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
