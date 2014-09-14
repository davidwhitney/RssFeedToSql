using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RssFeedToSql.Model;

namespace RssFeedToSql.Parsing
{
    public class Parser
    {
        public static Entry Empty = new Entry();

        public Entry Parse(string textSample)
        {
            var entry = new Entry();
            textSample = textSample.Trim();
            var lines = textSample.Split(new[] {Environment.NewLine}, StringSplitOptions.None).ToList();

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

            return entry;
        }

        private void MapLink(List<string> lines, Entry entry)
        {
            var articleSlugLine = lines.Last();
            var cutDown = articleSlugLine.Replace("View article... <", "").Replace(">", "");
            entry.Uri = cutDown;
        }

        private void MapTitle(List<string> lines, Entry entry)
        {
            entry.Title = TrimTag("Subject", lines[3]);
        }

        private void MapSent(List<string> lines, Entry entry)
        {
            entry.Timestamp = DateTime.Parse(TrimTag("Sent", lines[1]), new DateTimeFormatInfo(), DateTimeStyles.None);
        }

        private void MapFrom(List<string> lines, Entry entry)
        {
            var fromParts = TrimTag("From", lines[0]).Split(new[] {'('});
            entry.Writer = new Writer
            {
                Email = fromParts.First().Trim(),
                Name = fromParts.Skip(1).First().Trim().Replace(")", "")
            };
        }

        private string TrimTag(string tag, string text)
        {
            return text.Replace(tag + ":\t", "").Replace("\"", "").Trim();
        }
    }
}
