using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Text;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class IsEmptyTests
    {
        [TestMethod]
        public void Test01_NullStringBuilder()
        {
            StringBuilder testBuilder = default;

            Assert.ThrowsException<ArgumentNullException>(() => testBuilder.IsEmpty());
        }

        [TestMethod]
        public void Test02_NullArray()
        {
            int[] testArray = default;

            Assert.ThrowsException<ArgumentNullException>(() => testArray.IsEmpty());
        }

        [TestMethod]
        public void Test03_NullCollection()
        {
            ICollection testCollection = default;

            Assert.ThrowsException<ArgumentNullException>(() => testCollection.IsEmpty());
        }

        [TestMethod]
        public void Test04_EmptyStringBuilder()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Assert.IsTrue(stringBuilder.IsEmpty());
        }

        [TestMethod]
        public void Test05_EmptyArray()
        {
            int[] array = new int[0];

            Assert.IsTrue(array.IsEmpty());
        }

        [TestMethod]
        public void Test06_EmptyCollection()
        {
            ICollection collection = new ArrayList(3);

            Assert.IsTrue(collection.IsEmpty());
        }

        [TestMethod]
        public void Test07_NotEmptyStringBuilder()
        {
            StringBuilder stringBuilder = new StringBuilder("content");

            Assert.IsFalse(stringBuilder.IsEmpty());
        }

        [TestMethod]
        public void Test08_NotEmptyArray()
        {
            int[] array = new int[2];

            Assert.IsFalse(array.IsEmpty());
        }

        [TestMethod]
        public void Test09_NotEmptyCollection()
        {
            ICollection collection = new ArrayList(new object[] { "a", 1, TimeSpan.Zero });

            Assert.IsFalse(collection.IsEmpty());
        }
    }
}