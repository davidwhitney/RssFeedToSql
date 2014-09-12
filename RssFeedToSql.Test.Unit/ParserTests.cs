using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RssFeedToSql.Test.Unit
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Parse_SentCorrectlyIdentified()
        {
            var parser = new Parser();

            var entry = parser.Parse(_textSample);

            Assert.That(entry.Sent, Is.EqualTo(new DateTime(2014, 6, 23, 12, 03, 00)));
        }

        private string _textSample = @"From:	""no-reply@giantbomb.com (Patrick Klepek)""
Sent:	6/23/2014 12:03:00 PM
To:	
Subject:	A Conversation With Wolfenstein: The New Order's Jens Matthies

Some body goes here
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
