using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

                var parser = new Parser();
                var mapper = new SqlMapper();

                var files = Directory.GetFiles(args[0]);
                var items = new List<Entry>();

                Console.WriteLine("Parsing files");
                foreach (var file in files)
                {
                    var text = File.ReadAllText(file, Encoding.Unicode);
                    var entry = parser.Parse(text);
                    items.Add(entry);

                    Console.Write(".");
                }

                Console.WriteLine("Generating SQL");
                var sqlDump = mapper.Map(items);

                File.WriteAllText("import.sql", sqlDump);
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
