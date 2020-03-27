using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class RemoveLastTests
    {
        [TestMethod]
        public void Test01_NullStringReference()
        {
            string testString = default;

            Assert.ThrowsException<ArgumentNullException>(() => testString.RemoveLast());
        }

        [TestMethod]
        public void Test02_ShortString()
        {
            string testString = string.Empty;

            Assert.ThrowsException<InvalidOperationException>(() => testString.RemoveLast());
        }

        [TestMethod]
        public void Test03_RemoveLastString()
        {
            string expected = "Unimaginable";
            string testStringPlusOneChar = string.Concat(expected, "!");

            string actual = testStringPlusOneChar.RemoveLast();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test04_RemoveLastNString()
        {
            string expected = "Multiple chars";
            string toAdd    = " have been removed";

            string testStringAdded = string.Concat(expected, toAdd);

            string actual = testStringAdded.RemoveLast(toAdd.Length);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Test05_NullStringBuilderReference()
        {
            StringBuilder testStringBuilder = default;

            Assert.ThrowsException<ArgumentNullException>(() => testStringBuilder.RemoveLast());
        }

        [TestMethod]
        public void Test06_ShortStringBuilder()
        {
            StringBuilder testStringBuilder = new StringBuilder();

            Assert.ThrowsException<InvalidOperationException>(() => testStringBuilder.RemoveLast(3));
        }

        [TestMethod]
        public void Test07_RemoveLastNStringBuilder()
        {
            StringBuilder expected = new StringBuilder("Building with");
            StringBuilder testBuilderPlusChars = new StringBuilder(expected.ToString());

            string toAdd = " multiple characters";
            testBuilderPlusChars.Append(toAdd);

            testBuilderPlusChars.RemoveLast(toAdd.Length);

            Assert.AreEqual(expected.ToString(), testBuilderPlusChars.ToString());
        }
    }
}