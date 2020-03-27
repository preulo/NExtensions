using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Text;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class ToStringTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            object testObject = default;

            Assert.ThrowsException<ArgumentNullException>(() => testObject.ToString(objFormat: string.Empty));
        }

        [TestMethod]
        public void Test02_InvalidFormat()
        {
            object testObject = new object();

            Assert.ThrowsException<ArgumentNullException>(() => testObject.ToString(objFormat: null));
        }

        [TestMethod]
        public void Test03_GenericObject()
        {
            object testObject = new object();

            string expected = testObject.ToString();
            string actual   = testObject.ToString(objFormat: string.Empty);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test04_Int()
        {
            int testValue = 0;

            string expected = testValue.ToString();
            string actual   = testValue.ToString(objFormat: string.Empty);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test05_DateTime()
        {
            DateTime testValue = new DateTime(2020, 1, 1);

            string expected = testValue.ToString(string.Empty);
            string actual   = testValue.ToString(objFormat: string.Empty);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test06_TimeSpan()
        {
            TimeSpan testValue = TimeSpan.FromHours(25.5);
            string   format    = @"dd\:hh\:mm\:ss\.fffffff";

            string expected = testValue.ToString(format);
            string actual   = testValue.ToString(objFormat: format);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test07_DateTimeWithInvariantCulture()
        {
            DateTime testValue = new DateTime(2020, 1, 1);

            string expected = testValue.ToString(string.Empty, CultureInfo.InvariantCulture);
            string actual   = testValue.ToString(objFormat: string.Empty, formatProvider: CultureInfo.InvariantCulture);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test08_DateTimeWithFormat()
        {
            DateTime testValue = new DateTime(2020, 1, 1);

            string expected = testValue.ToString("yyyy", CultureInfo.InvariantCulture);
            string actual   = testValue.ToString(objFormat: "yyyy", formatProvider: CultureInfo.InvariantCulture);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test09_AnonymousType()
        {
            var anonymousTypeValue = new { p1 = "string", p2 = 0m, p3 = DateTime.Today };

            string expected = anonymousTypeValue.ToString();
            string actual   = anonymousTypeValue.ToString(objFormat: string.Empty);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test10_UserDefinedReferenceType()
        {
            var test = new Test10Anciliary();

            test.Id   = 1323;
            test.Desc = "OK";
            test.When = DateTime.Now;

            string expected = test.ToString();
            string actual   = test.ToString(objFormat: string.Empty);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test11_CultureInfo()
        {
            Test11Anciliary test = new Test11Anciliary();

            test.Id   = 1323;
            test.Desc = "OK";
            test.When = DateTime.Now;

            string expected = test.ToString(string.Empty, CultureInfo.GetCultureInfo("zh-Hans-HK"));
            string actual   = test.ToString(objFormat: string.Empty, formatProvider: CultureInfo.GetCultureInfo("zh-Hans-HK"));

            Assert.AreEqual(expected, actual);
        }
    }

    internal class Test10Anciliary
    {
        public int      Id   { get; set; }
        public string   Desc { get; set; }
        public DateTime When { get; set; }

        public override string ToString()
        {
            return $"[Id={Id};Desc={Desc};When={When:yyyy/MM/dd}]";
        }
    }

    internal class Test11Anciliary : IFormattable
    {
        public int      Id   { get; set; }
        public string   Desc { get; set; }
        public DateTime When { get; set; }

        public override string ToString()
        {
            return $"[Id={Id};Desc={Desc};When={When:yyyy/MM/dd}]";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder sb1 = new StringBuilder();

            sb1.Append("[Id=");
            sb1.Append(Id.ToString(format, formatProvider));
            sb1.Append(";Desc=");
            sb1.Append(Desc.ToString(format, formatProvider));
            sb1.Append(";When=");
            sb1.Append(When.ToString(format, formatProvider));
            sb1.Append("]");

            return sb1.ToString();
        }
    }
}