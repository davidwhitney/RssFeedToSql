using System;
using System.IO;
using RssFeedToSql.Model;

namespace RssFeedToSql.SqlGeneration
{
    public class SqlOutputFileProcessor : IProcessArticles, IProcessPublications, IDisposable
    {
        private int _currentArticleId;
        private readonly SqlMapper _sqlGen;
        private readonly StreamWriter _writer;

        private readonly InMemoryDatabaseOf<Writer> _writers;
        private readonly InMemoryDatabaseOf<Publication> _publications;

        public SqlOutputFileProcessor(string outputFile)
        {
            _sqlGen = new SqlMapper();
            _writer = new StreamWriter(outputFile, false);
            _writers = new InMemoryDatabaseOf<Writer>();
            _publications = new InMemoryDatabaseOf<Publication>();
        }

        public void ProcessSingleItem(Entry entry, Publication publication)
        {
            _currentArticleId = _currentArticleId + 1;
            entry.Writer.Id = _writers.CreateOrRetrieveId(entry.Writer);
            entry.Id = _currentArticleId;
            entry.PublicationId = publication.Id;

            var entrySql = _sqlGen.GenerateSqlFor(entry);
            _writer.WriteLine(entrySql);
        }

        public void ProcessSingleItem(Publication publication)
        {
            _publications.CreateOrRetrieveId(publication);
        }

        private void WritePublicationAndWriterData(TextWriter openStream)
        {
            foreach (var entry in _publications.Items)
            {
                openStream.WriteLine(_sqlGen.GenerateSqlFor(entry));
            }

            foreach (var entry in _writers.Items)
            {
                openStream.WriteLine(_sqlGen.GenerateSqlFor(entry));
            }
        }

        public void Dispose()
        {
            WritePublicationAndWriterData(_writer);

            _writer.Flush();
            _writer.Close();
            _writer.Dispose();
        }

    }
}