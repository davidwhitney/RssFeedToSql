using System;

namespace RssFeedToSql.Model
{
    public class Entry : IHaveAnId
    {
        public int Id { get; set; }
     
        public Writer Writer { get; set; }

        public DateTime Timestamp { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public string Body { get; set; }
        public int PublicationId { get; set; }
    }
}