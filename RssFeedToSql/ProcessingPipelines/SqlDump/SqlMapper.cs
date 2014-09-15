using RssFeedToSql.Model;

namespace RssFeedToSql.ProcessingPipelines.SqlDump
{
    public class SqlMapper
    {
        private const string ArticleInsert = @"INSERT INTO articles (id, title, content, source, datetime, publicationid, writerid) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');";
        private const string AuthorInsert = @"INSERT INTO writers (id, name, email) VALUES ('{0}', '{1}', '{2}');";
        private const string PublicationInsert = @"INSERT INTO publications (id, name, feed) VALUES ('{0}', '{1}', '');";
        
        public string GenerateSqlFor(Entry item)
        {
            return string.Format(ArticleInsert,
                item.Id.ToEscapedString(),
                item.Title.ToEscapedString(),
                item.Body.TrimStart().ToEscapedString(),
                item.Uri.ToEscapedString(),
                item.Timestamp.ToEscapedString(),
                item.PublicationId.ToEscapedString(),
                item.Writer.Id.ToEscapedString());
        }

        public string GenerateSqlFor(Writer item)
        {
            return string.Format(AuthorInsert, 
                item.Id.ToEscapedString(),
                item.Name.TrimStart().ToEscapedString(),
                item.Email.ToEscapedString());
        }

        public string GenerateSqlFor(Publication entry)
        {
            return string.Format(PublicationInsert, 
                entry.Id.ToEscapedString(),
                entry.Name.TrimStart().ToEscapedString());
        }
    }
}
