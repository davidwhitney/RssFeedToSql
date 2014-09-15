using RssFeedToSql.Model;

namespace RssFeedToSql
{
    public interface IProcessArticles
    {
        void ProcessSingleItem(Entry entry, Publication publication);
    }
}