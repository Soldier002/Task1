namespace Domain.Common.Parsers
{
    public interface IDateTimeParser
    {
        DateTime Parse(string str);
    }
}