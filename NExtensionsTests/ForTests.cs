using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class ForTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            List<decimal> testList = default;

            Assert.ThrowsException<ArgumentNullException>(() => testList.For((i,d) => testList[i] = d + 1));
        }

        [TestMethod]
        public void Test02_NullAction()
        {
            List<DateTime> testList = new List<DateTime>();

            Assert.ThrowsException<ArgumentNullException>(() => testList.For(default));
        }

        [TestMethod]
        public void Test03_ForWithString()
        {
            List<string> strings = new List<string>();

            strings.Add("this");
            strings.Add("is");
            strings.Add("a");
            strings.Add("simple");
            strings.Add("test");
            strings.Add("method");

            strings.For((i, text) => {
                if (i == 0)
                {
                    strings[i] = "This";
                }

                strings[i] = string.Concat(strings[i], " ");

                if (i == strings.Count - 1)
                {
                    strings[i] = string.Concat(strings[i].Trim(), "!");
                }
            });

            Assert.AreEqual(strings[0], "This ");
            Assert.AreEqual(strings[5], "method!");
        }

        [TestMethod]
        public void Test04_ForWithInt()
        {
            List<int> integers = new List<int>();

            integers.Add(1);
            integers.Add(2);
            integers.Add(3);
            integers.Add(5);
            integers.Add(8);

            integers.For((i, integer) => integers[i] += i);

            Assert.AreEqual(integers.First(), 1);
            Assert.AreEqual(integers.Last(), 12);
        }
    }
}