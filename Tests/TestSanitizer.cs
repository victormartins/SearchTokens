using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StringHelpers;

namespace Tests
{
    [TestFixture]
    public class TestSanitizer
    {
        Sanitizer sanitizer;

        [SetUp]
        public void Setup() {
            sanitizer = new Sanitizer();
        }

        [Test]
        public void EscapeNonAlfanumericChars() {
            string words = "abcç!'&";
            string result;
            result = sanitizer.EscapeNonAlphanumeric(words);
            Assert.AreEqual(@"abcç\!\'\&", result);
        }
    }
}
