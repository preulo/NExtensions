using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class InTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            object testObject = default;

            Assert.ThrowsException<ArgumentNullException>(() => testObject.In(1, "abc", 'd'));
        }

        [TestMethod]
        public void Test02_NullArray()
        {
            string testString = "zero";
            
            Assert.ThrowsException<ArgumentNullException>(() => testString.In(null));
        }

        [TestMethod]
        public void Test03_EmptyArray()
        {
            string testString = "";

            Assert.IsFalse(testString.In());
        }

        [TestMethod]
        public void Test04_StringParameterNotFound()
        {
            string testString = "ciao";

            Assert.IsFalse(testString.In("", null, "hi"));
        }

        [TestMethod]
        public void Test05_StringParameterFound()
        {
            string testString = "hi";

            Assert.IsTrue(testString.In("ciao", null, "hi"));
        }

        [TestMethod]
        public void Test06_IntNotFound()
        {
            int number = 4;

            Assert.IsFalse(number.In(1,2,3,5));
        }

        [TestMethod]
        public void Test07_IntFound()
        {
            int number = 12128321;

            Assert.IsTrue(number.In(1, 2, 3, 12128321));
        }

        [TestMethod]
        public void Test08_DateTimeFound()
        {
            DateTime dateTime = DateTime.Today;

            Assert.IsTrue(dateTime.In(new DateTime(2020, 1, 1), new DateTime(2006, 6, 6), DateTime.Today, DateTime.Now));
        }

        [TestMethod]
        public void Test09_UserDefinedReferenceTypeFound()
        {
            var i1 = new InTestClass1(1, "description", 3);
            var i2 = new InTestClass1(2, string.Empty, 6);
            var i3 = new InTestClass1(4, "testing in op", 0);
            var i4 = new InTestClass1(5, "tested in operator", null);

            Assert.IsTrue(i1.In(i1, i2, i3, i4));
        }

        [TestMethod]
        public void Test10_UserDefinedReferenceTypeNotFound()
        {
            var i1 = new InTestClass1(1, "description", 3);
            var i2 = new InTestClass1(2, string.Empty, 6);
            var i3 = new InTestClass1(4, "testing in op", 0);
            var i4 = new InTestClass1(1, "description", 3);

            Assert.IsFalse(i1.In(i2, i3, i4));
        }

        [TestMethod]
        public void Test11_UserDefinedReferenceTypeEquatableFound()
        {
            var i1 = new InTestClass2(1, "description", 3);
            var i2 = new InTestClass2(2, string.Empty, 6);
            var i3 = new InTestClass2(4, "testing in op", 0);
            var i4 = new InTestClass2(1, "tested in operator", null);

            Assert.IsTrue(i1.In(i2, i3, i4));
        }

        [TestMethod]
        public void InTest12()
        {
            var i1 = new InTestClass2(1, "description", 3);
            var i2 = new InTestClass2(2, string.Empty, 6);
            var i3 = new InTestClass2(4, "testing in op", 0);

            Assert.IsFalse(i1.In(i2, i3));
        }

        [TestMethod]
        public void InTest13()
        {
            var i1 = new InTestStruct1(1, "description", 3);
            var i2 = new InTestStruct1(2, string.Empty, 6);
            var i3 = new InTestStruct1(4, "testing in op", 0);
            var i4 = new InTestStruct1(5, "tested in operator", null);

            Assert.IsTrue(i1.In(i1, i2, i3, i4));
        }

        [TestMethod]
        public void InTest14()
        {
            var i1 = new InTestStruct1(1, "description", 3);
            var i2 = new InTestStruct1(2, string.Empty, 6);
            var i3 = new InTestStruct1(4, "testing in op", 0);

            Assert.IsFalse(i1.In(i2, i3));
        }

        [TestMethod]
        public void InTest15()
        {
            var i1 = new InTestStruct1(1, "description", 3);
            var i2 = new InTestStruct1(2, string.Empty, 6);
            var i3 = new InTestStruct1(4, "testing in op", 0);
            var i4 = new InTestStruct1(1, "description", 3);

            Assert.IsTrue(i1.In(i2, i3, i4));
        }

        [TestMethod]
        public void InTest16()
        {
            var i1 = new InTestStruct2(1, "description", 3);
            var i2 = new InTestStruct2(2, string.Empty, 6);
            var i3 = new InTestStruct2(4, "testing in op", 0);
            var i4 = new InTestStruct2(5, "tested in operator", null);

            Assert.IsTrue(i1.In(i1, i2, i3, i4));
        }

        [TestMethod]
        public void InTest17()
        {
            var i1 = new InTestStruct2(1, "description", 3);
            var i2 = new InTestStruct2(2, string.Empty, 6);
            var i3 = new InTestStruct2(4, "testing in op", 0);

            Assert.IsFalse(i1.In(i2, i3));
        }

        [TestMethod]
        public void InTest18()
        {
            var i1 = new InTestStruct2(1, "description", 3);
            var i2 = new InTestStruct2(2, string.Empty, 6);
            var i3 = new InTestStruct2(4, "testing in op", 0);
            var i4 = new InTestStruct2(1, "duplicate", 120);

            Assert.IsTrue(i1.In(i2, i3, i4));
        }
    }

    internal class InTestClass1
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal? Amount { get; set; }

        public InTestClass1(int id, string desc, decimal? amount = 0)
        {
            Id = id;
            Desc = desc;
            Amount = amount;
        }
    }

    internal class InTestClass2 : IEquatable<InTestClass2>
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal? Amount { get; set; }

        public InTestClass2(int id, string desc, decimal? amount = 0)
        {
            Id = id;
            Desc = desc;
            Amount = amount;
        }

        public bool Equals([AllowNull] InTestClass2 other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is InTestClass2))
            {
                return false;
            }

            var objTest = obj as InTestClass2;

            return Equals(objTest);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    internal struct InTestStruct1
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal? Amount { get; set; }

        public InTestStruct1(int id, string desc, decimal? amount = 0)
        {
            Id = id;
            Desc = desc;
            Amount = amount;
        }
    }

    internal struct InTestStruct2 : IEquatable<InTestStruct2>
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal? Amount { get; set; }

        public InTestStruct2(int id, string desc, decimal? amount = 0)
        {
            Id = id;
            Desc = desc;
            Amount = amount;
        }

        public bool Equals([AllowNull] InTestStruct2 other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is InTestStruct2))
            {
                return false;
            }

            var objTest = (InTestStruct2)obj;

            return Equals(objTest);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}