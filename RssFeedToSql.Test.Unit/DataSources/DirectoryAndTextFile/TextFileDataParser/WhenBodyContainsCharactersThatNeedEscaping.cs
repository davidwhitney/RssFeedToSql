using NUnit.Framework;
using RssFeedToSql.Model;

namespace RssFeedToSql.Test.Unit.DataSources.DirectoryAndTextFile.TextFileDataParser
{
    [TestFixture]
    public class WhenBodyContainsCharactersThatNeedEscaping
    {
        private Entry _entry;

        [SetUp]
        public void SetUp()
        {
            var parser = new RssFeedToSql.DataSources.DirectoryAndTextFile.TextFileDataParser();
            _entry = parser.Parse(TextSample); 
        }
     
        [Test]
        public void BodyIsFound()
        {
            Assert.That(_entry.Body, Is.StringContaining("\\ ’ ` “ ” '"));
        }

        private const string TextSample = @"From:	""no-reply@giantbomb.com (Patrick Klepek)""
Sent:	6/23/2014 12:03:00 PM
To:	
Subject:	A Conversation With Wolfenstein: The New Order's Jens Matthies

Start
\ ’ ` “ ” '
End is here.

View article... <http://www.giantbomb.com/articles/a-conversation-with-wolfenstein-the-new-orders-jens-matthies/1100-4943/>";
    }
}
