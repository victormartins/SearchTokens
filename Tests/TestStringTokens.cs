using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SearchTokens;

namespace Tests
{
    [TestFixture]
    public class TestStringTokens
    {
        public StringTokens st;
        [SetUp]
        public void Setup() {
            st = new StringTokens();
        }

        [TearDown]
        public void TearDown() { 
        
        }

        [Test]
        public void TwoWords() {         
            List<string> words = st.ForSearch("foo bar");
            Assert.AreEqual(2, words.Count);
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);
        }

        [Test]
        public void TwoWordsWithQuotes()
        {
            List<string> words = st.ForSearch("\"foo bar\"");
            Assert.AreEqual(1, words.Count);
            Assert.AreEqual("foo bar", words[0]);            
        }

        [Test]
        public void ThreeWordsButTwoToguetherByQuotes()
        {            
            List<string> words = st.ForSearch("\"foo bar\" blah");
            Assert.AreEqual(2, words.Count);
            Assert.AreEqual("foo bar", words[0]);
            Assert.AreEqual("blah", words[1]);
        }

    }
}
