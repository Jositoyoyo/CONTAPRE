namespace Dimatica.ContaPre.UnitTest.BLL
{
    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.UseCases.Treasury;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DeleteTreasuryUseCaseTests
    {
        [TestMethod]
        public void DeleteTreasuryResult_PreservesWebMethodNumericCodes()
        {
            Assert.AreEqual(0, (int)DeleteTreasuryResult.Invalid);
            Assert.AreEqual(1, (int)DeleteTreasuryResult.Ok);
            Assert.AreEqual(2, (int)DeleteTreasuryResult.NotFound);
            Assert.AreEqual(3, (int)DeleteTreasuryResult.RecognizedRights);
            Assert.AreEqual(4, (int)DeleteTreasuryResult.OracleDeleteFailed);
        }

        [TestMethod]
        public void DeleteTreasury_ZeroId_ReturnsNotFoundWithoutCallingServices()
        {
            var treasuries = new TreasuryServiceDouble();
            var accountingDocuments = new AccountingDocumentsServiceDouble();
            var useCase = new DeleteTreasuryUseCase(treasuries, accountingDocuments);

            var result = useCase.DeleteTreasury(0, 22, true);

            Assert.AreEqual(DeleteTreasuryResult.NotFound, result);
            Assert.AreEqual(0, treasuries.Calls.Count);
            Assert.AreEqual(0, accountingDocuments.Calls.Count);
        }

        [TestMethod]
        public void DeleteTreasury_RecognizedRights_ReturnsThreeWithoutDeleting()
        {
            var treasuries = new TreasuryServiceDouble
            {
                FoundInOracle = true,
                RecognizedRights = "derechos reconocidos"
            };
            var accountingDocuments = new AccountingDocumentsServiceDouble();
            var useCase = new DeleteTreasuryUseCase(treasuries, accountingDocuments);

            var result = useCase.DeleteTreasury(15, 22, true);

            Assert.AreEqual(DeleteTreasuryResult.RecognizedRights, result);
            CollectionAssert.AreEqual(new[] { "FoundTreasuryOracle", "GetRecognizedRightsOracle" }, treasuries.Calls);
            Assert.AreEqual(0, accountingDocuments.Calls.Count);
        }

        [TestMethod]
        public void DeleteTreasury_OracleDeleteFailure_ReturnsFourAndSkipsLocalDelete()
        {
            var treasuries = new TreasuryServiceDouble
            {
                FoundInOracle = true,
                OracleDeleteResponse = new Response { ResponseCode = ResponseCode.Invalid }
            };
            var accountingDocuments = new AccountingDocumentsServiceDouble();
            var useCase = new DeleteTreasuryUseCase(treasuries, accountingDocuments);

            var result = useCase.DeleteTreasury(15, 22, true);

            Assert.AreEqual(DeleteTreasuryResult.OracleDeleteFailed, result);
            CollectionAssert.AreEqual(
                new[] { "FoundTreasuryOracle", "GetRecognizedRightsOracle", "GetDocuments", "DeleteTreasuryOracle" },
                treasuries.Calls);
            Assert.IsFalse(treasuries.Calls.Contains("DeleteTreasury"));
        }

        [DataTestMethod]
        [DataRow(ResponseCode.Invalid, DeleteTreasuryResult.Invalid)]
        [DataRow(ResponseCode.Ok, DeleteTreasuryResult.Ok)]
        [DataRow(ResponseCode.NotFound, DeleteTreasuryResult.NotFound)]
        public void DeleteTreasury_MapsLocalDeleteResponse(ResponseCode responseCode, DeleteTreasuryResult expectedResult)
        {
            var treasuries = new TreasuryServiceDouble
            {
                LocalDeleteResponse = new Response { ResponseCode = responseCode }
            };
            var accountingDocuments = new AccountingDocumentsServiceDouble();
            var useCase = new DeleteTreasuryUseCase(treasuries, accountingDocuments);

            var result = useCase.DeleteTreasury(15, 22, false);

            Assert.AreEqual(expectedResult, result);
            Assert.IsTrue(treasuries.Calls.Contains("GetDocuments"));
            Assert.IsTrue(treasuries.Calls.Contains("DeleteTreasury"));
            Assert.AreEqual(15, treasuries.DeletedTreasuryId);
            Assert.AreEqual(22, treasuries.DeletedByUserId);
            Assert.IsFalse(treasuries.Calls.Contains("FoundTreasuryOracle"));
        }

        [TestMethod]
        public void DeleteTreasury_OracleException_PropagatesToCaller()
        {
            var treasuries = new TreasuryServiceDouble { OracleException = new InvalidOperationException("oracle failure") };
            var useCase = new DeleteTreasuryUseCase(treasuries, new AccountingDocumentsServiceDouble());

            Assert.ThrowsException<InvalidOperationException>(() => useCase.DeleteTreasury(15, 22, true));
        }

        [TestMethod]
        public void DeleteTreasury_UpdatesInvoiceReferencesBeforeDeletingOracleAndLocalTreasury()
        {
            var treasuries = new TreasuryServiceDouble
            {
                FoundInOracle = true,
                OracleDeleteResponse = new Response { ResponseCode = ResponseCode.Ok },
                LocalDeleteResponse = new Response { ResponseCode = ResponseCode.Ok },
                Documents = new List<PRE_TESORERIA> { new PRE_TESORERIA { DOC_CODIGO = 123 } }
            };
            var accountingDocuments = new AccountingDocumentsServiceDouble
            {
                Purchases = new List<PRE_FACTURA_COMPRA>
                {
                    new PRE_FACTURA_COMPRA { CODFACTURAGEI = 456, NCERTIFICADO = "certificado" }
                }
            };
            var useCase = new DeleteTreasuryUseCase(treasuries, accountingDocuments);

            var result = useCase.DeleteTreasury(15, 22, true);

            Assert.AreEqual(DeleteTreasuryResult.Ok, result);
            CollectionAssert.AreEqual(
                new[] { "FoundTreasuryOracle", "GetRecognizedRightsOracle", "GetDocuments", "DeleteTreasuryOracle", "DeleteTreasury" },
                treasuries.Calls);
            CollectionAssert.AreEqual(new[] { "GetPurchases", "UpdatePayBankOracle", "UpdateHistoryOracle" }, accountingDocuments.Calls);
        }

        private sealed class TreasuryServiceDouble : TreasuryServiceDoubleBase
        {
            public List<string> Calls { get; } = new List<string>();

            public bool FoundInOracle { get; set; }

            public string RecognizedRights { get; set; } = string.Empty;

            public Response OracleDeleteResponse { get; set; } = new Response { ResponseCode = ResponseCode.Ok };

            public Response LocalDeleteResponse { get; set; } = new Response { ResponseCode = ResponseCode.Ok };

            public List<PRE_TESORERIA> Documents { get; set; } = new List<PRE_TESORERIA>();

            public int DeletedTreasuryId { get; private set; }

            public int DeletedByUserId { get; private set; }

            public Exception OracleException { get; set; }

            public override bool FoundTreasuryOracle(int treasuryId)
            {
                this.Calls.Add("FoundTreasuryOracle");
                if (this.OracleException != null) throw this.OracleException;
                return this.FoundInOracle;
            }

            public override string GetRecognizedRightsOracle(int treasuryId)
            {
                this.Calls.Add("GetRecognizedRightsOracle");
                return this.RecognizedRights;
            }

            public override List<PRE_TESORERIA> GetDocuments(int? documentId, int? extraBudgetaryId, int? treasuryId)
            {
                this.Calls.Add("GetDocuments");
                return this.Documents;
            }

            public override Response DeleteTreasuryOracle(int treasuryId)
            {
                this.Calls.Add("DeleteTreasuryOracle");
                return this.OracleDeleteResponse;
            }

            public override Response DeleteTreasury(int treasuryId, int userId)
            {
                this.Calls.Add("DeleteTreasury");
                this.DeletedTreasuryId = treasuryId;
                this.DeletedByUserId = userId;
                return this.LocalDeleteResponse;
            }
        }

        private sealed class AccountingDocumentsServiceDouble : AccountingDocumentsServiceDoubleBase
        {
            public List<string> Calls { get; } = new List<string>();

            public List<PRE_FACTURA_COMPRA> Purchases { get; set; } = new List<PRE_FACTURA_COMPRA>();

            public override List<PRE_FACTURA_COMPRA> GetPurchases(int accountingDocumentId)
            {
                this.Calls.Add("GetPurchases");
                return this.Purchases;
            }

            public override Response UpdatePayBankOracle(DateTime? payBankDate, int billCode)
            {
                this.Calls.Add("UpdatePayBankOracle");
                return new Response { ResponseCode = ResponseCode.Ok };
            }

            public override Response UpdateHistoryOracle(string certificate, string description)
            {
                this.Calls.Add("UpdateHistoryOracle");
                return new Response { ResponseCode = ResponseCode.Ok };
            }
        }
    }
}
