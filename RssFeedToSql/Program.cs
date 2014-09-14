using System;
using System.IO;

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

            try
            {
                var indexer = new DirectoryIndexer();
                indexer.IndexAllDirectoriesUnder(args[0]);

                Console.WriteLine("Written all text to import.sql");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something bad happened.");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
