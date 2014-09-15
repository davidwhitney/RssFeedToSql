using RssFeedToSql.Model;

namespace RssFeedToSql
{
    public interface IProcessPublications
    {
        void ProcessSingleItem(Publication publication);
    }
}