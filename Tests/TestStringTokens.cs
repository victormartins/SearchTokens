using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StringHelpers;

namespace Tests
{
    [TestFixture]
    public class TestSearchTokens
    {
        public SearchTokens st;
        [SetUp]
        public void Setup() {
            st = new SearchTokens();
        }

        [TearDown]
        public void TearDown() { 
        
        }
        
        [Test]
        public void TwoWords() {         
            List<string> words = st.ForSearch("foo bar");
            Assert.AreEqual(2, words.Count);
            Assert.Contains("foo", words);
            Assert.Contains("bar", words);
        }

        [Test]
        public void CleanAfter()
        {
            List<string> words = st.ForSearch("foo bar");
            Assert.AreEqual(2, words.Count);
            Assert.Contains("foo", words);
            Assert.Contains("bar", words);

            words = st.ForSearch("foo bar");
            Assert.AreEqual(2, words.Count);
            Assert.Contains("foo", words);
            Assert.Contains("bar", words);
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
            Assert.Contains("Simon", words);
            Assert.Contains("Templar", words);
            Assert.Contains("Maximus Decimus", words);
            Assert.Contains("foo", words);
            Assert.Contains("bar", words);
            Assert.Contains("Regina Spektor", words);
            Assert.Contains("blah", words);
        }

        [Test]
        public void WordsQuotesWordsQuotesWord_PLICAS()
        {
            List<string> words = st.ForSearch("Simon Templar 'Maximus Decimus\" foo bar 'Regina Spektor' blah");
            Assert.AreEqual(7, words.Count);
            Assert.Contains("Simon", words);
            Assert.Contains("Templar", words);
            Assert.Contains("Maximus Decimus", words);
            Assert.Contains("foo", words);
            Assert.Contains("bar", words);
            Assert.Contains("Regina Spektor", words);
            Assert.Contains("blah", words);
        }

        [Test]
        public void KeepWhiteSpace()
        {
            
            List<string> words = st.ForSearch(" ' foo ' 'bar ' ' simon'");
            Assert.AreEqual(3, words.Count);            
            Assert.AreEqual(" foo ", words[0]);
            Assert.AreEqual("bar ", words[1]);
            Assert.AreEqual(" simon", words[2]);
        }

        [Test]
        public void TrimWhiteSpace()
        {
            SearchTokensOption options = new SearchTokensOption();
            options.TrimWhiteSpace = true;
            SearchTokens stTrim = new SearchTokens(options);
            List<string> words = stTrim.ForSearch(" ' foo ' 'bar ' ' simon' 'Victor Daniel Martins'");
            Assert.AreEqual(4, words.Count);            
            Assert.AreEqual("foo", words[0]);
            Assert.AreEqual("bar", words[1]);
            Assert.AreEqual("simon", words[2]);
            Assert.AreEqual("Victor Daniel Martins", words[3]);
        }

        [Test]
        public void IgnoreEmptyStrings()
        {
            List<string> words = st.ForSearch(" '' 'foo' 'bar' 'simon' ''");
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
            Assert.Contains("asd%", words);
            Assert.Contains("asd%", words);
            Assert.Contains("rere %>> <<", words);
        }

        [Test]
        public void ChangeWordGatheringChars()
        {
            SearchTokensOption options = new SearchTokensOption();
            options.WordGatheringChars = "|%";
            SearchTokens stKeep = new SearchTokens(options);
            List<string> words = stKeep.ForSearch("|foo ||bar%   one  two %three four ");
            Assert.AreEqual(5, words.Count);
            Assert.Contains("foo ", words);
            Assert.Contains("bar", words);
            Assert.Contains("one", words);
            Assert.Contains("two", words);
            Assert.Contains("three four ", words);


        }

        [Test]
        public void TrimAndChangeWordGatheringChars()
        {
            SearchTokensOption options = new SearchTokensOption();
            options.WordGatheringChars = "|%";            
            SearchTokens stKeep = new SearchTokens(options);
            List<string> words = stKeep.ForSearch("|foo ||bar%   one  two %three four       ");
            Assert.AreEqual(5, words.Count);
            Assert.Contains("foo ", words);
            Assert.Contains("bar", words);
            Assert.Contains("one", words);
            Assert.Contains("two", words);
            Assert.Contains("three four       ", words);

        }

        [Test]
        public void TokensDescriminatedByType() {
            SearchTokens st = new SearchTokens();
            TokenLists result = st.ForSearchDescriminated("this \"is a\" test \"embrace your selfs\" the winter is comming!");
            Assert.AreEqual(2, result.AgregatedWords.Count);
            Assert.Contains("is a", result.AgregatedWords);
            Assert.Contains("embrace your selfs", result.AgregatedWords);

            Assert.AreEqual(6, result.SingularWords.Count);
        }
    }
}
