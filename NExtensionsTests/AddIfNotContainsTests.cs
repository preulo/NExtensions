using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class AddIfNotContainsTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            Dictionary<int, string> testDictionary = default;

            Assert.ThrowsException<ArgumentNullException>(() => testDictionary.AddIfNotContains(0, "test01"));
        }

        [TestMethod]
        public void Test02_NullKey()
        {
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            Assert.ThrowsException<ArgumentNullException>(() => testDictionary.AddIfNotContains(null, "test zero two"));
        }

        [TestMethod]
        public void Test03_ItemAdded()
        {
            Dictionary<bool, int> testDictionary = new Dictionary<bool, int>();

            testDictionary.Add(true, 1);

            Assert.AreEqual(true, testDictionary.AddIfNotContains(false, 0));
            Assert.AreEqual(2, testDictionary.Count);
        }

        [TestMethod]
        public void Test04_ItemNotAdded()
        {
            Dictionary<decimal, string> testDictionary = new Dictionary<decimal, string>();

            testDictionary.Add(0m, "zero");
            testDictionary.Add(10m, "dollars");
            testDictionary.Add(3m, "beers");

            Assert.AreEqual(false, testDictionary.AddIfNotContains(10, "money"));
            Assert.AreEqual(3, testDictionary.Count);
        }

        [TestMethod]
        public void Test05_SortedDictionary()
        {
            SortedDictionary<DateTime, decimal> testSortedDict = new SortedDictionary<DateTime, decimal>();

            DateTime key1 = DateTime.Today;
            DateTime key2 = DateTime.Today.AddMonths(-7);
            DateTime key3 = new DateTime(2020, 1, 1, 13, 2, 3);

            testSortedDict.Add(key1, 1000);
            testSortedDict.Add(key2, 767);
            testSortedDict.Add(key3, -6143);

            Assert.AreEqual(false, testSortedDict.AddIfNotContains(key3, 121));
            Assert.AreEqual(3, testSortedDict.Count);
        }
    }
}