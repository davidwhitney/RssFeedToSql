using System;
using NUnit.Framework;
using RssFeedToSql.Model;
using RssFeedToSql.Parsing;

namespace RssFeedToSql.Test.Unit
{
    [TestFixture]
    public class ParserTests
    {
        private Entry _entry;

        [SetUp]
        public void SetUp()
        {
            var parser = new Parser();
            _entry = parser.Parse(_textSample); 
        }

        [Test]
        public void SentCorrectlyIdentified()
        {
            Assert.That(_entry.Timestamp, Is.EqualTo(new DateTime(2014, 6, 23, 12, 03, 00)));
        }

        [Test]
        public void FromCorrectlyIdentified()
        {
            Assert.That(_entry.Writer.Email, Is.EqualTo(""));
            Assert.That(_entry.Writer.Name, Is.EqualTo("Patrick Klepek"));
        }

        [Test]
        public void TitleIdentified()
        {
            Assert.That(_entry.Title, Is.EqualTo(""));
        }

        [Test]
        public void LinkIdentified()
        {
            Assert.That(_entry.Uri, Is.EqualTo("http://www.giantbomb.com/articles/a-conversation-with-wolfenstein-the-new-orders-jens-matthies/1100-4943/"));
        }

        [Test]
        public void BodyIsFound()
        {
            Assert.That(_entry.Body, Is.StringContaining("Some body goes here"));
        }

        [Test]
        public void NoWindowsLineEndings()
        {
            Assert.That(_entry.Body, Is.Not.StringContaining("\r\n"));
        }

        private string _textSample = @"From:	""Patrick Klepek""
Sent:	6/23/2014 12:03:00 PM
To:	
Subject:

Some body goes here
Another line is here
Matthies: Oh, yeah. That would be wonderful.
Patrick Klepek <https://plus.google.com/116204666196772968478?rel=author>  on Google+ 
	Filed under:
	Wolfenstein: The New Order <http://www.giantbomb.com/wolfenstein-the-new-order/3030-42581/> 
	id Software <http://www.giantbomb.com/id-software/3010-347/> 
	Jens Matthies <http://www.giantbomb.com/jens-matthies/3040-61021/> 
	MachineGames <http://www.giantbomb.com/machinegames/3010-7331/> 

View article... <http://www.giantbomb.com/articles/a-conversation-with-wolfenstein-the-new-orders-jens-matthies/1100-4943/>";
    }
}
