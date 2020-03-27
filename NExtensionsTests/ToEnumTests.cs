using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Text;

using NExtensions.NMethods;

namespace NExtensionsTests
{
    [TestClass]
    public class ToEnumTests
    {
        [TestMethod]
        public void Test01_NullReference()
        {
            string nullName = default;

            Assert.ThrowsException<ArgumentNullException>(() => nullName.ToEnum<GCCollectionMode>());
        }

        [TestMethod]
        public void Test02_InvalidName()
        {
            string invalidEnum = "an invalid name to an enum value";

            Assert.ThrowsException<ArgumentException>(() => invalidEnum.ToEnum<GCNotificationStatus>());
        }

        [TestMethod]
        public void Test03_ValueNotFound()
        {
            string nonexistentEnum = "IgnoreHeight";

            Assert.ThrowsException<ArgumentException>(() => nonexistentEnum.ToEnum<CompareOptions>());
        }

        [TestMethod]
        public void Test04_Parsed()
        {
            string enumString = "NoCurrentDateDefault";

            Assert.AreEqual(DateTimeStyles.NoCurrentDateDefault, enumString.ToEnum<DateTimeStyles>());
        }

        [TestMethod]
        public void Test05_CaseInsensitive()
        {
            string nonexistentEnum = "fORmkC";

            Assert.AreEqual(NormalizationForm.FormKC, nonexistentEnum.ToEnum<NormalizationForm>(true));
        }

        [TestMethod]
        public void Test06_InvalidType()
        {
            string enumValue = "Now";

            Assert.ThrowsException<InvalidOperationException>(() => enumValue.ToEnum<DateTime>());
        }
    }
}
