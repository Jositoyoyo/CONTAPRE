using System.Globalization;
using System.Threading;
using Dimatica.ContaPre.OL.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.UnitTest.OL.Business
{
    [TestClass]
    public class StatusRecordTests
    {
        private CultureInfo originalCulture;

        [TestInitialize]
        public void SetInvariantCulture()
        {
            this.originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [TestCleanup]
        public void RestoreCulture()
        {
            Thread.CurrentThread.CurrentCulture = this.originalCulture;
        }

        [TestMethod]
        public void AccumulatedLabel_FormatsAmountUsingNumberFormat()
        {
            var record = new StatusRecord { Column = "Ingresos", Accumulated = 1234.5m };

            Assert.AreEqual("1,234.50", record.AccumulatedLabel);
        }

        [TestMethod]
        public void AccumulatedLabel_IsEmptyForDiscounts()
        {
            var record = new StatusRecord { Column = "Descuentos", Accumulated = 1234.5m };

            Assert.AreEqual(string.Empty, record.AccumulatedLabel);
        }

        [TestMethod]
        public void AccumulatedLabel_IsEmptyForReceiptPaymentColumn()
        {
            var record = new StatusRecord { Column = "RC - P", Accumulated = 1234.5m };

            Assert.AreEqual(string.Empty, record.AccumulatedLabel);
        }

        [TestMethod]
        public void PendingLabel_IsEmptyForHyphenatedColumnsExceptReceiptPayment()
        {
            var record = new StatusRecord { Column = "RC - I", Pending = 1234.5m };

            Assert.AreEqual(string.Empty, record.PendingLabel);
        }

        [TestMethod]
        public void PendingLabel_FormatsReceiptPaymentColumn()
        {
            var record = new StatusRecord { Column = "RC - P", Pending = 1234.5m };

            Assert.AreEqual("1,234.50", record.PendingLabel);
        }

        [TestMethod]
        public void PendingLabel_FormatsColumnWithoutHyphen()
        {
            var record = new StatusRecord { Column = "Ingresos", Pending = 1234.5m };

            Assert.AreEqual("1,234.50", record.PendingLabel);
        }
    }
}
