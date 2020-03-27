using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class LastTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            StringCollection collection = default;

            Assert.ThrowsException<ArgumentNullException>(() => collection.Last());
        }

        [TestMethod]
        public void Test02_EmptyCollection()
        {
            StringCollection collection = new StringCollection();

            Assert.ThrowsException<InvalidOperationException>(() => collection.Last());
        }

        [TestMethod]
        public void Test03_NoCondition()
        {
            StringCollection collection = new StringCollection();

            collection.Add("Buzz");
            collection.Add("Year");
            collection.Add("Light");

            Assert.AreEqual("Light", collection.Last());
        }

        [TestMethod]
        public void Test04_WithCondition()
        {
            StringCollection collection = new StringCollection();

            collection.Add("Buzz");
            collection.Add("Yearning");
            collection.Add("Light");
            collection.Add("Year");
            collection.Add("Is");

            Assert.AreEqual("Year", collection.Last(s => s.StartsWith("Y")));
        }

        [TestMethod]
        public void Test04_WithConditionNotMatched()
        {
            StringCollection collection = new StringCollection();

            collection.Add("Despicable");
            collection.Add("Me");
            collection.Add("Minion");
            collection.Add("BANANA");

            Assert.ThrowsException<InvalidOperationException>(() => collection.Last(s => s.StartsWith("Gru")));
        }
    }
}