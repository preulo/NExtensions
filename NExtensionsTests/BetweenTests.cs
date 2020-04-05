using Microsoft.VisualStudio.TestTools.UnitTesting;
using NExtensions.NMethods;
using System;
using System.Collections.Generic;
using System.Text;

namespace NExtensionsTests
{
    [TestClass]
    public class BetweenTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            string reference = null;

            Assert.ThrowsException<ArgumentNullException>(() => reference.Between("from", "to"));
        }

        [TestMethod]
        public void Test02_NullFrom()
        {
            string reference = "test zero two";

            Assert.ThrowsException<ArgumentNullException>(() => reference.Between(null, "text"));
        }

        [TestMethod]
        public void Test03_NullTo()
        {
            string reference = "to null";

            Assert.ThrowsException<ArgumentNullException>(() => reference.Between("from", null));
        }

        [TestMethod]
        public void Test04_IsBetween()
        {
            int value = 51;

            Assert.AreEqual(true, value.Between(10, 1000));
        }

        [TestMethod]
        public void Test05_IsBetweenInverted()
        {
            int value = 51;

            Assert.AreEqual(true, value.Between(1000, 1));
        }

        [TestMethod]
        public void Test06_IsNotBetween()
        {
            decimal value = -7;

            Assert.AreEqual(false, value.Between(1, 2));
        }

        [TestMethod]
        public void Test07_IsNotBetweenInverted()
        {
            decimal value = 7;

            Assert.AreEqual(false, value.Between(2, 1));
        }
    }
}