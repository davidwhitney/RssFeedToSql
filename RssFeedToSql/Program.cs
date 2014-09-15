using System;
using RssFeedToSql.DataSources.DirectoryAndTextFile;
using RssFeedToSql.ProcessingPipelines.SqlDump;

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
            using (var outputPipeline = new SqlDumpProcessingPipeline("import.sql"))
            using (var dataSource = new DirectoryAndFlatFileDataSource(args[0]))
            {
                dataSource.OnPublicationParsed = outputPipeline.ProcessSingleItem;
                dataSource.OnEntryParsed = outputPipeline.ProcessSingleItem;
                dataSource.Import();
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
