﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using RssFeedToSql.Model;
using RssFeedToSql.Parsing;
using RssFeedToSql.SqlGeneration;

namespace RssFeedToSql
{
    public class DirectoryIndexer
    {
        private readonly InMemoryDatabaseOf<Writer> _writers;
        private readonly InMemoryDatabaseOf<Publication> _publications;

        private readonly Parser _parser;
        private readonly SqlMapper _sqlGen;
        private int _currentArticleId;

        public DirectoryIndexer()
        {
            _parser = new Parser();
            _sqlGen = new SqlMapper();
            
            _writers = new InMemoryDatabaseOf<Writer>();
            _publications = new InMemoryDatabaseOf<Publication>();

            _currentArticleId = 1;
        }

        public void Index(string rootDirectory, TextWriter openStream)
        {
            Console.WriteLine("Parsing files in " + rootDirectory);

            var publicationId = _publications.CreateOrRetrieveId(new Publication
            {
                Name = rootDirectory.Split('\\').Last().Trim()
            });

            var files = Directory.GetFiles(rootDirectory);

            foreach (var file in files)
            {
                _currentArticleId = _currentArticleId + 1;
                var text = File.ReadAllText(file, Encoding.Unicode);
                
                var entry = _parser.Parse(text);

                entry.Writer.Id = _writers.CreateOrRetrieveId(entry.Writer);
                entry.Id = _currentArticleId;
                entry.PublicationId = publicationId;

                var entrySql = _sqlGen.GenerateSqlFor(entry);
                openStream.WriteLine(entrySql);

                Console.Write(".");
            }

        }
    }
}