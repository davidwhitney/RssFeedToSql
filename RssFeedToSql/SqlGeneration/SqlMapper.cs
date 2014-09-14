using RssFeedToSql.Model;

namespace RssFeedToSql.SqlGeneration
{
    public class SqlMapper
    {
        private const string ArticleInsert = @"INSERT INTO articles (id, title, content, source, datetime, publicationId, writerId) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');";
        private const string AuthorInsert = @"INSERT INTO writers (id, name, email) VALUES ('{0}','{1}','{2}');";

        public string GenerateSqlFor(Entry item)
        {
            return string.Format(ArticleInsert,
                EncodeMySqlString(item.Id.ToString()),
                EncodeMySqlString(item.Title),
                EncodeMySqlString(item.Body.TrimStart()),
                EncodeMySqlString(item.Uri),
                EncodeMySqlString(item.Timestamp.ToString()),
                EncodeMySqlString(item.PublicationId.ToString()),
                EncodeMySqlString(item.Writer.Id.ToString()));
        }

        public string GenerateSqlFor(Writer item)
        {
            return string.Format(AuthorInsert,
                EncodeMySqlString(item.Id.ToString()),
                EncodeMySqlString(item.Name.TrimStart()),
                EncodeMySqlString(item.Email));
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