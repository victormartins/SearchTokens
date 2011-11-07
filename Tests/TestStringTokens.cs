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
        public void CleanAfter()
        {
            List<string> words = st.ForSearch("foo bar");
            Assert.AreEqual(2, words.Count);
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);

            words = st.ForSearch("foo bar");
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

        [Test]
        public void StartedWithQuoteButNeverEnded()
        {
            List<string> words = st.ForSearch("\"foo bar blah");
            Assert.AreEqual(1, words.Count);
            Assert.AreEqual("foo bar blah", words[0]);            
        }

        [Test]
        public void WordsQuotesWordsQuotesWord()
        {
            List<string> words = st.ForSearch("Simon Templar \"Maximus Decimus\" foo bar \"Regina Spektor\" blah");
            Assert.AreEqual(7, words.Count);
            Assert.AreEqual("Simon", words[0]);
            Assert.AreEqual("Templar", words[1]);
            Assert.AreEqual("Maximus Decimus", words[2]);
            Assert.AreEqual("foo", words[3]);
            Assert.AreEqual("bar", words[4]);
            Assert.AreEqual("Regina Spektor", words[5]);
            Assert.AreEqual("blah", words[6]);
        }

        [Test]
        public void WordsQuotesWordsQuotesWord_PLICAS()
        {
            List<string> words = st.ForSearch("Simon Templar 'Maximus Decimus\" foo bar 'Regina Spektor' blah");
            Assert.AreEqual(7, words.Count);
            Assert.AreEqual("Simon", words[0]);
            Assert.AreEqual("Templar", words[1]);
            Assert.AreEqual("Maximus Decimus", words[2]);
            Assert.AreEqual("foo", words[3]);
            Assert.AreEqual("bar", words[4]);
            Assert.AreEqual("Regina Spektor", words[5]);
            Assert.AreEqual("blah", words[6]);
        }

        [Test]
        public void KeepWhiteSpace()
        {
            SearchTokensOption options = new SearchTokensOption();
            options.TrimWhiteSpace = false;
            StringTokens stKeep = new StringTokens(options);
            List<string> words = stKeep.ForSearch(" ' foo ' 'bar ' ' simon'");
            Assert.AreEqual(3, words.Count);            
            Assert.AreEqual(" foo ", words[0]);
            Assert.AreEqual("bar ", words[1]);
            Assert.AreEqual(" simon", words[2]);
        }

        [Test]
        public void TrimWhiteSpace()
        {
            List<string> words = st.ForSearch(" ' foo ' 'bar ' ' simon' 'Victor Daniel Martins'");
            Assert.AreEqual(4, words.Count);            
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);
            Assert.AreEqual("simon", words[2]);
            Assert.AreEqual("Victor Daniel Martins", words[3]);
        }

        [Test]
        public void IgnoreEmptyStrings()
        {
            List<string> words = st.ForSearch(" ' foo ' 'bar ' ' simon'");
            Assert.AreEqual(3, words.Count);
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);
            Assert.AreEqual("simon", words[2]);
        }

        [Test]
        public void BreakAfterQuoteEvenIfNoSpace()
        {
            List<string> words = st.ForSearch("'foo'bar");
            Assert.AreEqual(2, words.Count);
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);            
        }

        [Test]
        public void CutWhenAQuoteIsFoundIfThereAreLettersInCache()
        {
            List<string> words = st.ForSearch("\"\"\"foo\"\"\"bar\"bug\"");
            Assert.AreEqual(3, words.Count);
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);
            Assert.AreEqual("bug", words[2]);
        }

        [Test]
        public void CheckPercentages()
        {
            List<string> words = st.ForSearch("asd% asd%'rere %>> <<'");
            Assert.AreEqual(3, words.Count);
            Assert.AreEqual("asd%", words[0]);
            Assert.AreEqual("asd%", words[1]);
            Assert.AreEqual("rere %>> <<", words[2]);
        }


        [Test]
        public void ChangeWordGatheringChars()
        {
            SearchTokensOption options = new SearchTokensOption();
            options.WordGatheringChars = "|%";
            StringTokens stKeep = new StringTokens(options);
            List<string> words = stKeep.ForSearch("|foo||bar| one two %three four");
            Assert.AreEqual(5, words.Count);
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);
            Assert.AreEqual("one", words[2]);
            Assert.AreEqual("two", words[3]);
            Assert.AreEqual("three four", words[4]); 

        }

    }
}
