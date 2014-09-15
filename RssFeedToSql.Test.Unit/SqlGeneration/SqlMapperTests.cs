using System;
using System.Collections.Generic;
using NUnit.Framework;
using RssFeedToSql.Model;
using RssFeedToSql.ProcessingPipelines.SqlDump;

namespace RssFeedToSql.Test.Unit.SqlGeneration
{
    [TestFixture]
    public class SqlMapperTests
    {
        private string _sqlLines;

        [SetUp]
        public void SetUp()
        {
            var entries = new List<Entry>
            {
                new Entry
                {
                    Id = 1,
                    PublicationId = 1,
                    Body = "Body 1",
                    Writer = new Writer
                    {
                        Email = "email1@tempuri.org",
                        Name = "Writer1",

                    },
                    Uri = "http://uri1.com",
                    Timestamp = new DateTime(2014, 1, 2, 2, 4, 00),
                    Title = "Title 1"
                },
                new Entry
                {
                    Id = 2,
                    PublicationId = 1,
                    Body = "Body 2",
                    Writer = new Writer
                    {
                        Email = "email2@tempuri.org",
                        Name = "Writer2",
                    },
                    Uri = "http://uri2.com",
                    Timestamp = new DateTime(2012, 1, 2, 2, 4, 00),
                    Title = "Title 2"
                },
            };

            var sqlMapper = new SqlMapper();
            _sqlLines += sqlMapper.GenerateSqlFor(entries[0]);
            _sqlLines += sqlMapper.GenerateSqlFor(entries[0]);
        }

        [Test]
        public void ContainsAuthorSql()
        {
            Assert.That(_sqlLines, Is.StringContaining("INSERT INTO articles (id, title, content, source, datetime, publicationId, writerId) VALUES ('1','Title 1','Body 1','http://uri1.com','02/01/2014 2:04:00 AM','1','0');"));
            Assert.That(_sqlLines, Is.StringContaining("INSERT INTO articles (id, title, content, source, datetime, publicationId, writerId) VALUES ('1','Title 1','Body 1','http://uri1.com','02/01/2014 2:04:00 AM','1','0');"));
        }
    }
}