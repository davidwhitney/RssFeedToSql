using System;
using System.IO;
using System.Linq;
using System.Text;
using RssFeedToSql.Model;

namespace RssFeedToSql.Parsing
{
    public class DirectoryAndFlatFileImporter : IImportFromADataSource
    {
        public Action<Publication> OnPublicationParsed { get; set; }
        public Action<Entry, Publication> OnEntryParsed { get; set; }

        private readonly string _rootDirectory;

        private readonly Parser _parser;

        public DirectoryAndFlatFileImporter(string rootDirectory)
        {
            OnPublicationParsed = p => { };
            OnEntryParsed = (e,p) => { };

            _rootDirectory = rootDirectory;
            _parser = new Parser();
        }
        
        public void Import()
        {
            var dirs = Directory.GetDirectories(_rootDirectory);
            Console.WriteLine(dirs.Length + " publications found.");

            foreach (var directory in dirs)
            {
                IndexSingleDirectory(directory);
            }
        }

        public void IndexSingleDirectory(string rootDirectory)
        {
            Console.WriteLine("Parsing files in " + rootDirectory);

            var publication = new Publication { Name = rootDirectory.Split('\\').Last().Trim() };

            OnPublicationParsed(publication);
            
            var files = Directory.GetFiles(rootDirectory);

            Console.WriteLine(files.Length + " files found.");
            for (var index = 0; index < files.Length; index++)
            {
                var file = files[index];
                var text = File.ReadAllText(file, Encoding.Unicode);
                var entry = _parser.Parse(text);

                OnEntryParsed(entry, publication);

                Console.Write(index + 1 + " ");
            }
        }
    }
}