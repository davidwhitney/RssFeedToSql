using System;
using RssFeedToSql.Model;

namespace RssFeedToSql
{
    public interface IImportFromADataSource
    {
        Action<Publication> OnPublicationParsed { get; set; }
        Action<Entry, Publication> OnEntryParsed { get; set; } 
        void Import();
    }
}