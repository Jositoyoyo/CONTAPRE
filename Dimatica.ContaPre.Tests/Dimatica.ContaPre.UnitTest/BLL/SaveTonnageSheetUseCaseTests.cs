namespace Dimatica.ContaPre.UnitTest.BLL
{
    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.BLL.UseCases;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SaveTonnageSheetUseCaseTests
    {
        [TestMethod]
        public void SaveTonnageSheet_InsertFailure_ReturnsHeaderResponseWithoutInsertingDetails()
        {
            var service = new TonnageSheetsServiceDouble
            {
                InsertSheetResponse = new Response { ResponseCode = ResponseCode.Invalid }
            };
            var useCase = new SaveTonnageSheetUseCase(service);
            var details = new List<PRE_DETALLE_HOJA_ARQUEO> { CreateDetail() };

            var result = useCase.SaveTonnageSheet(CreateSheet(0), details, 17);

            Assert.AreEqual(ResponseCode.Invalid, result.ResponseCode);
            CollectionAssert.AreEqual(new[] { "InsertTonnageSheet" }, service.Calls);
            Assert.AreEqual(0, service.InsertedDetails.Count);
        }

        [TestMethod]
        public void SaveTonnageSheet_InsertSuccess_AssignsSheetAndUserToDetails()
        {
            var service = new TonnageSheetsServiceDouble
            {
                InsertSheetResponse = new Response { ResponseCode = ResponseCode.Ok, ResponseMethod = 41 }
            };
            var useCase = new SaveTonnageSheetUseCase(service);
            var sheet = CreateSheet(0);
            var detail = CreateDetail();

            var result = useCase.SaveTonnageSheet(sheet, new List<PRE_DETALLE_HOJA_ARQUEO> { detail }, 17);

            Assert.AreEqual(ResponseCode.Ok, result.ResponseCode);
            Assert.AreEqual(17, service.InsertedSheet.USU_CODIGO);
            Assert.AreEqual(41, detail.HOJ_CODIGO);
            Assert.AreEqual(17, detail.USU_CODIGO);
            CollectionAssert.AreEqual(new[] { "InsertTonnageSheet", "InsertTonnageSheetDetail" }, service.Calls);
        }

        [DataTestMethod]
        [DataRow(ResponseCode.Invalid)]
        [DataRow(ResponseCode.NotFound)]
        public void SaveTonnageSheet_UpdateFailure_SkipsDetailOperations(ResponseCode responseCode)
        {
            var service = new TonnageSheetsServiceDouble
            {
                UpdateSheetResponse = new Response { ResponseCode = responseCode }
            };
            var useCase = new SaveTonnageSheetUseCase(service);

            var result = useCase.SaveTonnageSheet(CreateSheet(8), new List<PRE_DETALLE_HOJA_ARQUEO> { CreateDetail() }, 17);

            Assert.AreEqual(responseCode, result.ResponseCode);
            CollectionAssert.AreEqual(new[] { "UpdateTonnageSheet" }, service.Calls);
        }

        [TestMethod]
        public void SaveTonnageSheet_UpdateSuccess_UpdatesExistingDetailCheck()
        {
            var service = new TonnageSheetsServiceDouble
            {
                ExistingDetail = new PRE_DETALLE_HOJA_ARQUEO { DET_CODIGO = 27 },
                DetailUpdateResponse = new Response { ResponseCode = ResponseCode.Invalid }
            };
            var useCase = new SaveTonnageSheetUseCase(service);
            var detail = CreateDetail();
            detail.MARCADO = true;

            var result = useCase.SaveTonnageSheet(CreateSheet(8), new List<PRE_DETALLE_HOJA_ARQUEO> { detail }, 17);

            Assert.AreEqual(ResponseCode.Ok, result.ResponseCode);
            CollectionAssert.AreEqual(new[] { "UpdateTonnageSheet", "GetDetailByFilters", "UpdateTonnageSheetDetail" }, service.Calls);
            Assert.AreEqual(27, service.UpdatedDetailId);
            Assert.IsTrue(service.UpdatedDetailCheck);
            Assert.AreEqual(17, service.UpdatedDetailUserId);
            Assert.AreEqual(8, service.LastDetailLookupSheetId);
        }

        [TestMethod]
        public void SaveTonnageSheet_UpdateSuccess_InsertsMissingDetail()
        {
            var service = new TonnageSheetsServiceDouble();
            var useCase = new SaveTonnageSheetUseCase(service);
            var detail = CreateDetail();

            var result = useCase.SaveTonnageSheet(CreateSheet(8), new List<PRE_DETALLE_HOJA_ARQUEO> { detail }, 17);

            Assert.AreEqual(ResponseCode.Ok, result.ResponseCode);
            CollectionAssert.AreEqual(new[] { "UpdateTonnageSheet", "GetDetailByFilters", "InsertTonnageSheetDetail" }, service.Calls);
            Assert.AreEqual(8, detail.HOJ_CODIGO);
            Assert.AreEqual(17, detail.USU_CODIGO);
        }

        [TestMethod]
        public void SaveTonnageSheet_DetailFailure_IsIgnoredAndHeaderResponseIsReturned()
        {
            var service = new TonnageSheetsServiceDouble
            {
                InsertSheetResponse = new Response { ResponseCode = ResponseCode.Ok, ResponseMethod = 41 },
                DetailInsertResponse = new Response { ResponseCode = ResponseCode.Invalid }
            };
            var useCase = new SaveTonnageSheetUseCase(service);

            var result = useCase.SaveTonnageSheet(
                CreateSheet(0),
                new List<PRE_DETALLE_HOJA_ARQUEO> { CreateDetail(), CreateDetail() },
                17);

            Assert.AreEqual(ResponseCode.Ok, result.ResponseCode);
            CollectionAssert.AreEqual(
                new[] { "InsertTonnageSheet", "InsertTonnageSheetDetail", "InsertTonnageSheetDetail" },
                service.Calls);
        }

        [TestMethod]
        public void SaveTonnageSheet_ServiceException_PropagatesToCaller()
        {
            var service = new TonnageSheetsServiceDouble
            {
                ExceptionToThrow = new InvalidOperationException("service failure")
            };
            var useCase = new SaveTonnageSheetUseCase(service);

            Assert.ThrowsException<InvalidOperationException>(() =>
                useCase.SaveTonnageSheet(CreateSheet(8), new List<PRE_DETALLE_HOJA_ARQUEO> { CreateDetail() }, 17));
        }

        [TestMethod]
        public void SaveTonnageSheet_DetailException_PropagatesToCaller()
        {
            var service = new TonnageSheetsServiceDouble
            {
                InsertSheetResponse = new Response { ResponseCode = ResponseCode.Ok, ResponseMethod = 41 },
                DetailInsertException = new InvalidOperationException("detail failure")
            };
            var useCase = new SaveTonnageSheetUseCase(service);

            Assert.ThrowsException<InvalidOperationException>(() =>
                useCase.SaveTonnageSheet(CreateSheet(0), new List<PRE_DETALLE_HOJA_ARQUEO> { CreateDetail() }, 17));
        }

        private static PRE_HOJA_ARQUEO CreateSheet(int id)
        {
            return new PRE_HOJA_ARQUEO
            {
                HOJ_CODIGO = id,
                HOJ_ANO = 2026,
                HOJ_NUMERO = 12,
                HOJ_FECHA = new DateTime(2026, 10, 15)
            };
        }

        private static PRE_DETALLE_HOJA_ARQUEO CreateDetail()
        {
            return new PRE_DETALLE_HOJA_ARQUEO
            {
                DET_NUMERO_EXPEDIENTE = 55,
                LIN_NUMERO = 4,
                DET_FECHA_APUNTE = new DateTime(2026, 10, 15),
                DET_IMPORTE = 100m,
                EXP_ANO_PRESUPUESTO = 2026,
                MARCADO = false
            };
        }

        private sealed class TonnageSheetsServiceDouble : ITonnageSheetsService
        {
            public List<string> Calls { get; } = new List<string>();

            public List<PRE_DETALLE_HOJA_ARQUEO> InsertedDetails { get; } = new List<PRE_DETALLE_HOJA_ARQUEO>();

            public Response InsertSheetResponse { get; set; } = new Response { ResponseCode = ResponseCode.Ok, ResponseMethod = 41 };

            public Response UpdateSheetResponse { get; set; } = new Response { ResponseCode = ResponseCode.Ok };

            public Response DetailInsertResponse { get; set; } = new Response { ResponseCode = ResponseCode.Ok };

            public Response DetailUpdateResponse { get; set; } = new Response { ResponseCode = ResponseCode.Ok };

            public PRE_DETALLE_HOJA_ARQUEO ExistingDetail { get; set; }

            public PRE_HOJA_ARQUEO InsertedSheet { get; private set; }

            public int UpdatedDetailId { get; private set; }

            public bool UpdatedDetailCheck { get; private set; }

            public int UpdatedDetailUserId { get; private set; }

            public int LastDetailLookupSheetId { get; private set; }

            public Exception ExceptionToThrow { get; set; }

            public Exception DetailInsertException { get; set; }

            public PRE_DETALLE_HOJA_ARQUEO GetDetailByFilters(int tonnageSheetCode, int? fileNumber, int? lineNumber)
            {
                this.ThrowIfConfigured();
                this.Calls.Add("GetDetailByFilters");
                this.LastDetailLookupSheetId = tonnageSheetCode;
                return this.ExistingDetail;
            }

            public Response InsertTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
            {
                this.ThrowIfConfigured();
                this.Calls.Add("InsertTonnageSheet");
                this.InsertedSheet = tonnageSheet;
                return this.InsertSheetResponse;
            }

            public Response UpdateTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
            {
                this.ThrowIfConfigured();
                this.Calls.Add("UpdateTonnageSheet");
                return this.UpdateSheetResponse;
            }

            public Response InsertTonnageSheetDetail(PRE_DETALLE_HOJA_ARQUEO tonnageSheetDetail)
            {
                this.ThrowIfConfigured();
                this.Calls.Add("InsertTonnageSheetDetail");
                if (this.DetailInsertException != null)
                {
                    throw this.DetailInsertException;
                }

                this.InsertedDetails.Add(tonnageSheetDetail);
                return this.DetailInsertResponse;
            }

            public Response UpdateTonnageSheetDetail(int tonnageSheetDetailCode, bool check, int userId)
            {
                this.ThrowIfConfigured();
                this.Calls.Add("UpdateTonnageSheetDetail");
                this.UpdatedDetailId = tonnageSheetDetailCode;
                this.UpdatedDetailCheck = check;
                this.UpdatedDetailUserId = userId;
                return this.DetailUpdateResponse;
            }

            public PRE_HOJA_ARQUEO GetById(int id) { return default(PRE_HOJA_ARQUEO); }
            public List<PRE_HOJA_ARQUEO> GetRestrictedAccountStatement(int? exerciseYear, int? budgetYear, int accountId, string sinceDate, string untilDate) { return default(List<PRE_HOJA_ARQUEO>); }
            public int GetTonnageSheetCode(int exerciseYear, int number, bool isFifty) { return default(int); }
            public List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetails(int exerciseYear, int number, bool isFifty) { return default(List<PRE_HOJA_ARQUEO>); }
            public List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetailsWithoutSheet(int exerciseYear, int number, bool isFifty) { return default(List<PRE_HOJA_ARQUEO>); }
            public List<PRE_HOJA_ARQUEO> GetByFilters(int? exerciseYear, int? accountingCode, int? sheetSinceNumber, int? sheetUntilNumber, string sheetDateSince, string sheetDateUntil, string order, string sense) { return default(List<PRE_HOJA_ARQUEO>); }
            public List<PRE_DETALLE_HOJA_ARQUEO> GetDetailsById(int tonnageSheetId) { return default(List<PRE_DETALLE_HOJA_ARQUEO>); }
            public Response DeleteDetail(int detailId, int userId) { return default(Response); }
            public Response DeleteTonnageSheet(int id, int userId) { return default(Response); }

            private void ThrowIfConfigured()
            {
                if (this.ExceptionToThrow != null)
                {
                    throw this.ExceptionToThrow;
                }
            }
        }
    }
}
