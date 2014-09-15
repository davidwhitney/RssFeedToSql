using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using RssFeedToSql.Model;

namespace RssFeedToSql.DataSources.DirectoryAndTextFile
{
    public class TextFileDataParser
    {
        public static Entry Empty = new Entry();

        public Entry Parse(string textSample)
        {
            var entry = new Entry();
            textSample = textSample.Trim();
            var lines = textSample.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToList();

            if (lines.Count < 4)
            {
                return Empty;
            }
            
            MapFrom(lines, entry);
            MapSent(lines, entry);
            MapTitle(lines, entry);
            MapLink(lines, entry);

            lines.RemoveRange(0, 4);
            lines.RemoveRange(lines.Count - 1, 1);
            
            entry.Body = string.Join("\n\n", lines);
            
            entry.Body = entry.Body.Replace("\n\n[embedded content]\n\n", "\n\n");
            
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            entry.Body = regex.Replace(entry.Body, @" ");

            return entry;
        }

        private void MapLink(List<string> lines, Entry entry)
        {
            var articleSlugLine = lines.Last();
            var cutDown = articleSlugLine.Replace("View article... <", "").Replace(">", "");
            entry.Uri = cutDown.Trim();
        }

        private void MapTitle(List<string> lines, Entry entry)
        {
            entry.Title = TrimTag("Subject", lines[3]);

            if (entry.Title.Trim() == "Subject:")
            {
                entry.Title = "";
            }
        }

        private void MapSent(List<string> lines, Entry entry)
        {
            entry.Timestamp = DateTime.Parse(TrimTag("Sent", lines[1]), new DateTimeFormatInfo(), DateTimeStyles.None);
        }

        private void MapFrom(List<string> lines, Entry entry)
        {
            var trimmed = TrimTag("From", lines[0]);
            var trimmedParts = trimmed.Split(new[] { '(' });

            if (trimmedParts.Length <= 1)
            {
                entry.Writer = new Writer {Name = trimmedParts.First().Trim(), Email = string.Empty};
                return;
            }
            
            entry.Writer = new Writer
            {
                Email = trimmedParts.First().Trim(),
                Name = trimmedParts.Skip(1).First().Trim().Replace(")", "")
            };
        }

        private string TrimTag(string tag, string text)
        {
            return text.Replace(tag + ":\t", "").Replace("\"", "").Trim();
        }
    }
}
