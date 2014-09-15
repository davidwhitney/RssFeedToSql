using System;
using RssFeedToSql.Parsing;
using RssFeedToSql.SqlGeneration;

namespace RssFeedToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ");
                Console.WriteLine("RssFeedToSql.exe \"c:\\some\\directory\\path\"");
                return;
            }

            TryImportData(args);
        }

        private static void ImportData(string[] args)
        {
            using (var sqlFileProcessingPipeline = new SqlOutputFileProcessor("import.sql"))
            using (var indexer = new DirectoryAndFlatFileImporter(args[0]))
            {
                indexer.OnPublicationParsed = sqlFileProcessingPipeline.ProcessSingleItem;
                indexer.OnEntryParsed = sqlFileProcessingPipeline.ProcessSingleItem;
                indexer.Import();
            }

            Console.WriteLine("Written all text to import.sql");
        }

        private static void TryImportData(string[] args)
        {
            try
            {
                ImportData(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something bad happened.");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
