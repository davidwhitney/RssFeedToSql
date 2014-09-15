using System.Collections.Generic;
using System.Linq;

namespace RssFeedToSql.ProcessingPipelines.SqlDump
{
    public static class StringEncodingExtensions
    {
        private static readonly Dictionary<string, string> CharactersToEncode = new Dictionary<string, string>
        {
            {@"\", @"\\"},
            {"’", @"'"},
            {"`", @"`"},
            {"“", @""""},
            {"”", @""""},
            {"'", @"''"}
        };

        public static string ToEscapedString(this object str)
        {
            return CharactersToEncode.Aggregate(str.ToString(), (current, pair) => current.Replace(pair.Key, pair.Value));
        }
    }
}