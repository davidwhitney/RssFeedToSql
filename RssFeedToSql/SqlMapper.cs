using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssFeedToSql
{
    public class SqlMapper
    {
        public string Map(List<Entry> entries)
        {
            var articleInsert = @"INSERT INTO articles (title, content, source, datetime) VALUES ('{0}','{1}','{2}','{3}')";
            var authorInsert = @"INSERT INTO writers (name, email) VALUES ('{0}','{1}')";
            
            var sb = new StringBuilder();

            var groupedByAuthors = entries.GroupBy(x => x.FromName);
            foreach (var group in groupedByAuthors)
            {
                var first = group.First();
                sb.AppendLine(string.Format(authorInsert, first.FromName, first.FromEmail));
            }

            foreach (var group in groupedByAuthors)
            foreach (var item in group)
            {
                sb.AppendLine(string.Format(articleInsert, item.Title, item.Body, item.Uri, item.Timestamp));
            }

            return sb.ToString();
        }
    }
}