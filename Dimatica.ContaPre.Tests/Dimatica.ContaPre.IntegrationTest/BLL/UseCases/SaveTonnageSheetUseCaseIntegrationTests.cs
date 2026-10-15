namespace Dimatica.ContaPre.IntegrationTest.BLL.UseCases
{
    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.BLL.Services;
    using Dimatica.ContaPre.BLL.UseCases;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.IntegrationTest.Support;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [TestCategory("Integration")]
    [DoNotParallelize]
    public class SaveTonnageSheetUseCaseIntegrationTests
    {
        private const string TestWebConfigEnvironmentVariable = "CONTAPRE_TONNAGE_TEST_WEB_CONFIG_PATH";
        private const string TestUserIdEnvironmentVariable = "CONTAPRE_TONNAGE_TEST_USER_ID";

        [TestMethod]
        public void SaveTonnageSheet_CreatesAndUpdatesSheetAndDetails_InIsolatedDatabase()
        {
            int userId;
            if (!int.TryParse(Environment.GetEnvironmentVariable(TestUserIdEnvironmentVariable), out userId) || userId <= 0)
            {
                Assert.Inconclusive("Define " + TestUserIdEnvironmentVariable + " con el ID de un usuario válido de la base de pruebas.");
            }

            IntegrationDatabase.ConfigureFromExplicitWebConfig(TonnageSheetsDataContext.Instance, TestWebConfigEnvironmentVariable);

            var service = new TonnageSheetsService();
            var useCase = new SaveTonnageSheetUseCase(service);
            var exerciseYear = Convert.ToInt16(DateTime.Today.Year);
            var sheetNumber = FindUnusedSheetNumber(service, exerciseYear);
            var sheetId = 0;

            try
            {
                var insert = useCase.SaveTonnageSheet(
                    new PRE_HOJA_ARQUEO
                    {
                        HOJ_ANO = exerciseYear,
                        HOJ_NUMERO = sheetNumber,
                        HOJ_ARQUEO50 = false,
                        HOJ_FECHA = DateTime.Today
                    },
                    new List<PRE_DETALLE_HOJA_ARQUEO> { CreateDetail(exerciseYear) },
                    userId);

                Assert.AreEqual(ResponseCode.Ok, insert.ResponseCode);
                sheetId = (int)insert.ResponseMethod;
                Assert.IsNotNull(service.GetById(sheetId));

                var persistedDetails = service.GetDetailsById(sheetId);
                Assert.IsNotNull(persistedDetails);
                Assert.IsTrue(persistedDetails.Count > 0, "El caso de uso debe insertar el detalle de la hoja.");

                var persistedDetail = persistedDetails[0];
                var update = useCase.SaveTonnageSheet(
                    new PRE_HOJA_ARQUEO
                    {
                        HOJ_CODIGO = sheetId,
                        HOJ_FECHA = DateTime.Today.AddDays(-1)
                    },
                    new List<PRE_DETALLE_HOJA_ARQUEO>
                    {
                        new PRE_DETALLE_HOJA_ARQUEO
                        {
                            DET_NUMERO_EXPEDIENTE = persistedDetail.DET_NUMERO_EXPEDIENTE,
                            LIN_NUMERO = persistedDetail.LIN_NUMERO,
                            DET_FECHA_APUNTE = persistedDetail.DET_FECHA_APUNTE,
                            DET_IMPORTE = persistedDetail.DET_IMPORTE,
                            EXP_ANO_PRESUPUESTO = exerciseYear,
                            MARCADO = true
                        }
                    },
                    userId);

                Assert.AreEqual(ResponseCode.Ok, update.ResponseCode);
                Assert.IsNotNull(service.GetById(sheetId));
            }
            finally
            {
                if (sheetId == 0)
                {
                    var persistedSheetId = service.GetTonnageSheetCode(exerciseYear, sheetNumber, false);
                    if (persistedSheetId > 0)
                    {
                        sheetId = persistedSheetId;
                    }
                }

                if (sheetId > 0)
                {
                    foreach (var detail in service.GetDetailsById(sheetId))
                    {
                        service.DeleteDetail(detail.DET_CODIGO, userId);
                    }

                    service.DeleteTonnageSheet(sheetId, userId);
                }
            }
        }

        private static int FindUnusedSheetNumber(ITonnageSheetsService service, short exerciseYear)
        {
            var candidate = 900000 + (int)(DateTime.UtcNow.Ticks % 90000);
            for (var attempt = 0; attempt < 100; attempt++)
            {
                if (service.GetTonnageSheetCode(exerciseYear, candidate, false) == -1)
                {
                    return candidate;
                }

                candidate = candidate == 989999 ? 900000 : candidate + 1;
            }

            Assert.Inconclusive("No se encontró un número de hoja libre en la base de pruebas.");
            return 0;
        }

        private static PRE_DETALLE_HOJA_ARQUEO CreateDetail(short exerciseYear)
        {
            return new PRE_DETALLE_HOJA_ARQUEO
            {
                DET_FECHA_APUNTE = DateTime.Today,
                DET_IMPORTE = 1.23m,
                EXP_ANO_PRESUPUESTO = exerciseYear,
                MARCADO = false
            };
        }
    }
}
