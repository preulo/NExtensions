using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class RemoveManyTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            string nullReference = default;

            Assert.ThrowsException<ArgumentNullException>(() => nullReference.RemoveMany("", ""));
        }

        [TestMethod]
        public void Test02_NullList()
        {
            string testString = "sufficient for the test";

            Assert.ThrowsException<ArgumentNullException>(() => testString.RemoveMany(null));
        }

        [TestMethod]
        public void Test03_ListContainingNull()
        {
            string testString = "we can not fully understand";

            Assert.ThrowsException<ArgumentException>(() => testString.RemoveMany("perfect", null, "words"));
        }

        [TestMethod]
        public void Test04_ListContainingEmpty()
        {
            string testString = "this is the method we use";

            Assert.ThrowsException<ArgumentException>(() => testString.RemoveMany("choose", "", "disagree"));
        }

        [TestMethod]
        public void Test05_Removed()
        {
            string testString = "we want to know everything and misinterpret nothing";

            Assert.AreEqual("e ant to kno every and misinterpret no", testString.RemoveMany("thing", "w"));
        }
    }
}