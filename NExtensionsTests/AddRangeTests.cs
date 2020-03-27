using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class AddRangeTests
    {
        [TestMethod]
        public void Test01_NullListReference()
        {
            List<decimal> decimalList = default;

            Assert.ThrowsException<ArgumentNullException>(() => decimalList.AddRange(1, 2, 3));
        }

        [TestMethod]
        public void Test02_NullStringCollectionReference()
        {
            StringCollection collectionTest = default;
            
            StringCollection collection2 = new StringCollection();
            collection2.Add("init");

            Assert.ThrowsException<ArgumentNullException>(() => collectionTest.AddRange(collection2));
        }

        [TestMethod]
        public void Test03_NullStringCollectionReference2()
        {
            StringCollection collectionTest = default;

            Assert.ThrowsException<ArgumentNullException>(() => collectionTest.AddRange("some", "string", "values"));
        }

        [TestMethod]
        public void Test04_NullDecimalArray()
        {
            List<decimal> decimalList = new List<decimal>();

            Assert.ThrowsException<ArgumentNullException>(() => decimalList.AddRange(items: null));
        }

        [TestMethod]
        public void Test05_NullStringCollection()
        {
            StringCollection collectionTest = new StringCollection();

            StringCollection collection2 = default;

            Assert.ThrowsException<ArgumentNullException>(() => collectionTest.AddRange(collection2));
        }

        [TestMethod]
        public void Test06_NullStringArray()
        {
            StringCollection collectionTest = new StringCollection();

            collectionTest.Add("collection");
            collectionTest.Add("with");
            collectionTest.Add("strings");

            Assert.ThrowsException<ArgumentNullException>(() => collectionTest.AddRange(strings: default(string[])));
        }

        [TestMethod]
        public void Test07_Decimals()
        {
            List<decimal> decimalList = new List<decimal>();

            decimalList.AddRange(1);
            decimalList.AddRange(10);
            decimalList.AddRange(90);

            int countBefore = decimalList.Count;

            decimal[] decimalsAdded = new decimal[] { 2, 20, 80, 40 };
            int quantityAdded = decimalsAdded.Length;

            decimalList.AddRange(items: decimalsAdded);

            int countAfter = decimalList.Count;

            Assert.AreEqual(countBefore + quantityAdded, countAfter);
        }

        [TestMethod]
        public void Test08_StringCollection()
        {
            StringCollection collectionTest = new StringCollection();

            collectionTest.Add("original");
            collectionTest.Add("string");
            collectionTest.Add("collection");

            StringCollection collection2 = new StringCollection();

            collection2.Add("added");

            collectionTest.AddRange(collection2);

            Assert.AreEqual(collectionTest[collectionTest.Count - 1], collection2[0]);
        }

        [TestMethod]
        public void Test09_StringCollectionWithStrings()
        {
            StringCollection collectionTest = new StringCollection();

            collectionTest.Add("one");
            collectionTest.Add("final");
            collectionTest.Add("test");

            var addedStrings = new string[] { "to", "this" };

            collectionTest.AddRange(strings: addedStrings);

            Assert.AreEqual(collectionTest[3], addedStrings[0]);
            Assert.AreEqual(collectionTest[4], addedStrings[1]);
        }
    }
}