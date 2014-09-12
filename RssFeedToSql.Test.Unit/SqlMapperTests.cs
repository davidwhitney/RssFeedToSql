using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace RssFeedToSql.Test.Unit
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
                    Body = "Body 1",
                    FromEmail = "email1@tempuri.org",
                    FromName = "Writer1",
                    Uri = "http://uri1.com",
                    Timestamp = new DateTime(2014, 1, 2, 2, 4, 00),
                    Title = "Title 1"
                },
                new Entry
                {
                    Body = "Body 2",
                    FromEmail = "email2@tempuri.org",
                    FromName = "Writer2",
                    Uri = "http://uri2.com",
                    Timestamp = new DateTime(2012, 1, 2, 2, 4, 00),
                    Title = "Title 2"
                },
            };

            var sqlMapper = new SqlMapper();
            _sqlLines = sqlMapper.Map(entries);
        }

        [Test]
        public void ContainsAuthorSql()
        {
            Assert.That(_sqlLines, Is.StringContaining("INSERT INTO writers ('name', 'email') VALUES ('Writer1','email1@tempuri.org')"));
            Assert.That(_sqlLines, Is.StringContaining("INSERT INTO writers ('name', 'email') VALUES ('Writer2','email2@tempuri.org')"));
        }
    }
}