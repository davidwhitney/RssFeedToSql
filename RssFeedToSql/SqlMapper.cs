using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssFeedToSql
{
    public class SqlMapper
    {
        public string Map(List<Entry> entries)
        {
            var articleInsert = @"INSERT INTO articles ('title', 'content', 'source', 'datetime') VALUES ('{0}','{1}','{2}','{3}')";
            var authorInsert = @"INSERT INTO writers ('name', 'email') VALUES ('{0}','{1}')";
            
            var sb = new StringBuilder();

            var groupedByAuthors = entries.GroupBy(x => x.FromName);
            foreach (var group in groupedByAuthors)
            {
                var first = group.First();
                sb.AppendLine(
                    string.Format(authorInsert,
                        EncodeMySqlString(first.FromName), 
                        EncodeMySqlString(first.FromEmail))
                        );
            }

            foreach (var group in groupedByAuthors)
            foreach (var item in group)
            {
                sb.AppendLine(
                    string.Format(articleInsert,
                        EncodeMySqlString(item.Title),
                        EncodeMySqlString(item.Body.TrimStart()),
                        EncodeMySqlString(item.Uri), 
                        EncodeMySqlString(item.Timestamp.ToString()))
                        );
            }

            return sb.ToString();
        }

        string EncodeMySqlString(string value)
        {
            return value.Replace(@"\", @"\\")
                        .Replace("’", @"")
                        .Replace("`", @"")
                        .Replace("'", @"");
        }
    }
}