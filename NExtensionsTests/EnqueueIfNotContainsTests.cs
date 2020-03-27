using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class EnqueueIfNotContainsTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            Queue<int> testQueue = default;

            Assert.ThrowsException<ArgumentNullException>(() => testQueue.EnqueueIfNotContains(1));
        }

        [TestMethod]
        public void Test02_ContainsValueType()
        {
            Queue<int> testQueue = new Queue<int>();

            testQueue.Enqueue(2);
            testQueue.Enqueue(3);
            testQueue.Enqueue(10);

            int quantityBefore = testQueue.Count;

            testQueue.EnqueueIfNotContains(10);

            int quantityAfter = testQueue.Count;

            Assert.AreEqual(quantityBefore, quantityAfter);
        }

        [TestMethod]
        public void Test03_DoesNotContainValueType()
        {
            Queue<int> testQueue = new Queue<int>();

            testQueue.Enqueue(2);
            testQueue.Enqueue(3);
            testQueue.Enqueue(10);

            int quantityBefore = testQueue.Count;

            testQueue.EnqueueIfNotContains(20);

            int quantityAfter = testQueue.Count;

            Assert.AreEqual(quantityBefore + 1, quantityAfter);
        }

        [TestMethod]
        public void Test04_ContainsReferenceType()
        {
            Queue<string> testQueue = new Queue<string>();

            testQueue.Enqueue("simple");
            testQueue.Enqueue("super");
            testQueue.Enqueue("test");

            int quantityBefore = testQueue.Count;

            testQueue.EnqueueIfNotContains("super");

            int quantityAfter = testQueue.Count;

            Assert.AreEqual(quantityBefore, quantityAfter);
        }

        [TestMethod]
        public void Test05_DoesNotContainReferenceType()
        {
            Queue<string> testQueue = new Queue<string>();

            testQueue.Enqueue("simple");
            testQueue.Enqueue("super");
            testQueue.Enqueue("test");

            int quantityBefore = testQueue.Count;

            testQueue.EnqueueIfNotContains("now");

            int quantityAfter = testQueue.Count;

            Assert.AreEqual(quantityBefore + 1, quantityAfter);
        }

        [TestMethod]
        public void Test06_ContainsUserDefinedType()
        {
            Queue<EnqueueIfNotContainsTestClass1> testQueue = new Queue<EnqueueIfNotContainsTestClass1>();

            var obj1 = new EnqueueIfNotContainsTestClass1(239632,    new DateTime(2010, 1, 1, 3, 4, 12), "a", "d", "a");
            var obj2 = new EnqueueIfNotContainsTestClass1(12324,     new DateTime(2000, 4, 7, 4, 3, 21), "b", "e", "z");
            var obj3 = new EnqueueIfNotContainsTestClass1(683264815, new DateTime(1990, 5, 2, 7, 6, 51), "i", "d", "k", "f", "a");

            testQueue.Enqueue(obj1);
            testQueue.Enqueue(obj2);
            testQueue.Enqueue(obj3);

            int quantityBefore = testQueue.Count;

            testQueue.EnqueueIfNotContains(obj2);

            int quantityAfter = testQueue.Count;

            Assert.AreEqual(quantityBefore, quantityAfter);
        }

        [TestMethod]
        public void Test06_ContainsUserDefinedTypeWithEquals()
        {
            Queue<EnqueueIfNotContainsTestClass2> testQueue = new Queue<EnqueueIfNotContainsTestClass2>();

            var obj1 = new EnqueueIfNotContainsTestClass2(239632,    new DateTime(2010, 1, 1, 3, 4, 12), "odo");
            var obj2 = new EnqueueIfNotContainsTestClass2(12324,     new DateTime(2000, 4, 7, 4, 3, 21), "pen");
            var obj3 = new EnqueueIfNotContainsTestClass2(683264815, new DateTime(1990, 5, 2, 7, 6, 51), "iddqd");

            testQueue.Enqueue(obj1);
            testQueue.Enqueue(obj2);
            testQueue.Enqueue(obj3);

            int quantityBefore = testQueue.Count;

            var obj4 = new EnqueueIfNotContainsTestClass2(632321, new DateTime(2030, 5, 2, 7, 6, 51), "iddqd");

            testQueue.EnqueueIfNotContains(obj4);

            int quantityAfter = testQueue.Count;

            Assert.AreEqual(quantityBefore, quantityAfter);
        }
    }

    internal class EnqueueIfNotContainsTestClass1
    {
        public int      Identifier { get; set; }
        public DateTime Updated    { get; set; }
        public string[] Data       { get; set; }

        public EnqueueIfNotContainsTestClass1(int identifier, DateTime updated, params string[] data)
        {
            Identifier = identifier;
            Updated    = updated;
            Data       = data;
        }
    }

    internal class EnqueueIfNotContainsTestClass2 : IEquatable<EnqueueIfNotContainsTestClass2>
    {
        public int      Identifier { get; set; }
        public DateTime Updated    { get; set; }
        public byte[]   Data       { get; set; }

        public EnqueueIfNotContainsTestClass2(int identifier, DateTime updated, string data)
        {
            Identifier = identifier;
            Updated    = updated;
            Data       = Encoding.UTF8.GetBytes(data);
        }

        public bool Equals([AllowNull] EnqueueIfNotContainsTestClass2 other)
        {
            if (other == null)
            {
                return false;
            }

            return Encoding.UTF8.GetString(Data) == Encoding.UTF8.GetString(other.Data);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EnqueueIfNotContainsTestClass2))
            {
                return false;
            }

            var objTest = obj as EnqueueIfNotContainsTestClass2;

            return Equals(objTest);
        }

        public override int GetHashCode()
        {
            return Encoding.UTF8.GetString(Data).GetHashCode();
        }
    }
}