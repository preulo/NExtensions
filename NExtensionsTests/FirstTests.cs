using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class FirstTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            StringCollection collection = default;

            Assert.ThrowsException<ArgumentNullException>(() => collection.First());
        }

        [TestMethod]
        public void Test02_EmptyCollection()
        {
            StringCollection collection = new StringCollection();

            Assert.ThrowsException<InvalidOperationException>(() => collection.First());
        }

        [TestMethod]
        public void Test03_NoCondition()
        {
            StringCollection collection = new StringCollection();

            collection.Add("Buzz");
            collection.Add("Light");
            collection.Add("Year");

            Assert.AreEqual("Buzz", collection.First());
        }

        [TestMethod]
        public void Test04_WithCondition()
        {
            StringCollection collection = new StringCollection();

            collection.Add("Buzz");
            collection.Add("Light");
            collection.Add("Year");
            collection.Add("Is");
            collection.Add("Yearning");

            Assert.AreEqual("Year", collection.First(s => s.StartsWith("Y")));
        }

        [TestMethod]
        public void Test04_WithConditionNotMatched()
        {
            StringCollection collection = new StringCollection();

            collection.Add("Despicable");
            collection.Add("Me");
            collection.Add("Minion");
            collection.Add("BANANA");

            Assert.ThrowsException<InvalidOperationException>(() => collection.First(s => s.StartsWith("Ba")));
        }
    }
}