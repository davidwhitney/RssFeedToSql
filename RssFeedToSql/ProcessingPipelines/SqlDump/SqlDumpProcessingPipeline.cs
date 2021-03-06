using System;
using System.IO;
using RssFeedToSql.Model;

namespace RssFeedToSql.ProcessingPipelines.SqlDump
{
    public class SqlDumpProcessingPipeline : IProcessArticles, IProcessPublications, IDisposable
    {
        private int _currentArticleId;
        private readonly SqlMapper _sqlGen;
        private readonly StreamWriter _writer;

        private readonly InMemoryDatabaseOf<Writer> _writers;
        private readonly InMemoryDatabaseOf<Publication> _publications;

        public SqlDumpProcessingPipeline(string outputFile)
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
            
            _writer.WriteLine();
            _writer.WriteLine(entrySql);
        }

        public void ProcessSingleItem(Publication publication)
        {
            var id = _publications.CreateOrRetrieveId(publication);
            publication.Id = id;

            // Delayed write for nice formatting in output file - see dispose.
        }

        private void WritePublicationAndWriterData(TextWriter openStream)
        {
            foreach (var entry in _publications.Items)
            {
                openStream.WriteLine();
                openStream.WriteLine(_sqlGen.GenerateSqlFor(entry));
            }

            foreach (var entry in _writers.Items)
            {
                openStream.WriteLine();
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