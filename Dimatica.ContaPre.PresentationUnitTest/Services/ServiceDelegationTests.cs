namespace Dimatica.ContaPre.PresentationUnitTest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Dimatica.ContaPre.BLL.Services;
    using Dimatica.ContaPre.DAL.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ServiceDelegationTests
    {
        [TestMethod]
        public void AccountingDocumentsService_DelegatesAllMethods()
        {
            VerifyService(typeof(AccountingDocumentsService), typeof(IAccountingDocumentsDataContext));
        }

        [TestMethod]
        public void AccountingRecordsService_DelegatesAllMethods()
        {
            VerifyService(typeof(AccountingRecordsService), typeof(IAccountingRecordsDataContext));
        }

        [TestMethod]
        public void AccountPgcpService_DelegatesAllMethods()
        {
            VerifyService(typeof(AccountPgcpService), typeof(IAccountPgcpDataContext));
        }

        [TestMethod]
        public void AccountRestrictedService_DelegatesAllMethods()
        {
            VerifyService(typeof(AccountRestrictedService), typeof(IAccountRestrictedDataContext));
        }

        [TestMethod]
        public void AdministrativeRecordsService_DelegatesAllMethods()
        {
            VerifyService(typeof(AdministrativeRecordsService), typeof(IAdministrativeRecordsDataContext));
        }

        [TestMethod]
        public void ApplicationService_DelegatesAllMethods()
        {
            VerifyService(typeof(ApplicationService), typeof(IApplicationDataContext));
        }

        [TestMethod]
        public void BillPurchasesService_DelegatesAllMethods()
        {
            VerifyService(typeof(BillPurchasesService), typeof(IBillPurchasesDataContext));
        }

        [TestMethod]
        public void BudgetApplicationsService_DelegatesAllMethods()
        {
            VerifyService(typeof(BudgetApplicationsService), typeof(IBudgetApplicationsDataContext));
        }

        [TestMethod]
        public void BudgetsService_DelegatesAllMethods()
        {
            VerifyService(typeof(BudgetsService), typeof(IBudgetsDataContext));
        }

        [TestMethod]
        public void ContractTypesService_DelegatesAllMethods()
        {
            VerifyService(typeof(ContractTypesService), typeof(IContractTypesDataContext));
        }

        [TestMethod]
        public void CostPlacesService_DelegatesAllMethods()
        {
            VerifyService(typeof(CostPlacesService), typeof(ICostPlacesDataContext));
        }

        [TestMethod]
        public void CreditModificationBudgetsService_DelegatesAllMethods()
        {
            VerifyService(typeof(CreditModificationBudgetsService), typeof(ICreditModificationBudgetsDataContext));
        }

        [TestMethod]
        public void CreditModificationsService_DelegatesAllMethods()
        {
            VerifyService(typeof(CreditModificationsService), typeof(ICreditModificationsDataContext));
        }

        [TestMethod]
        public void CreditModificationTypesService_DelegatesAllMethods()
        {
            VerifyService(typeof(CreditModificationTypesService), typeof(ICreditModificationTypesDataContext));
        }

        [TestMethod]
        public void DocumentTypesService_DelegatesAllMethods()
        {
            VerifyService(typeof(DocumentTypesService), typeof(IDocumentTypesDataContext));
        }

        [TestMethod]
        public void ExtraBudgetaryApplicationsService_DelegatesAllMethods()
        {
            VerifyService(typeof(ExtraBudgetaryApplicationsService), typeof(IExtraBudgetaryApplicationsDataContext));
        }

        [TestMethod]
        public void ExtraBudgetaryRecordsService_DelegatesAllMethods()
        {
            VerifyService(typeof(ExtraBudgetaryRecordsService), typeof(IExtraBudgetaryRecordsDataContext));
        }

        [TestMethod]
        public void OriginsService_DelegatesAllMethods()
        {
            VerifyService(typeof(OriginsService), typeof(IOriginsDataContext));
        }

        [TestMethod]
        public void ParametersService_DelegatesAllMethods()
        {
            VerifyService(typeof(ParametersService), typeof(IParametersDataContext));
        }

        [TestMethod]
        public void PayFormsService_DelegatesAllMethods()
        {
            VerifyService(typeof(PayFormsService), typeof(IPayFormsDataContext));
        }

        [TestMethod]
        public void PayTypesService_DelegatesAllMethods()
        {
            VerifyService(typeof(PayTypesService), typeof(IPayTypesDataContext));
        }

        [TestMethod]
        public void ProgramsService_DelegatesAllMethods()
        {
            VerifyService(typeof(ProgramsService), typeof(IProgramsDataContext));
        }

        [TestMethod]
        public void ProvenancesService_DelegatesAllMethods()
        {
            VerifyService(typeof(ProvenancesService), typeof(IProvenancesDataContext));
        }

        [TestMethod]
        public void ProvidersService_DelegatesAllMethods()
        {
            VerifyService(typeof(ProvidersService), typeof(IProvidersDataContext));
        }

        [TestMethod]
        public void ProvincesService_DelegatesAllMethods()
        {
            VerifyService(typeof(ProvincesService), typeof(IProvincesDataContext));
        }

        [TestMethod]
        public void RecordTypesService_DelegatesAllMethods()
        {
            VerifyService(typeof(RecordTypesService), typeof(IRecordTypesDataContext));
        }

        [TestMethod]
        public void RectificationsService_DelegatesAllMethods()
        {
            VerifyService(typeof(RectificationsService), typeof(IRectificationsDataContext));
        }

        [TestMethod]
        public void SingsService_DelegatesAllMethods()
        {
            VerifyService(typeof(SingsService), typeof(ISingsDataContext));
        }

        [TestMethod]
        public void TonnageSheetsService_DelegatesAllMethods()
        {
            VerifyService(typeof(TonnageSheetsService), typeof(ITonnageSheetsDataContext));
        }

        [TestMethod]
        public void TreasuriesService_DelegatesAllMethods()
        {
            VerifyService(typeof(TreasuriesService), typeof(ITreasuriesDataContext));
        }

        [TestMethod]
        public void TreasuryLinesService_DelegatesAllMethods()
        {
            VerifyService(typeof(TreasuryLinesService), typeof(ITreasuryLinesDataContext));
        }

        [TestMethod]
        public void UserService_DelegatesAllMethods()
        {
            VerifyService(typeof(UserService), typeof(IUserDataContext));
        }

        [TestMethod]
        public void AllServices_RejectNullDataContext()
        {
            AssertNullDependency(typeof(AccountingDocumentsService), typeof(IAccountingDocumentsDataContext));
            AssertNullDependency(typeof(AccountingRecordsService), typeof(IAccountingRecordsDataContext));
            AssertNullDependency(typeof(AccountPgcpService), typeof(IAccountPgcpDataContext));
            AssertNullDependency(typeof(AccountRestrictedService), typeof(IAccountRestrictedDataContext));
            AssertNullDependency(typeof(AdministrativeRecordsService), typeof(IAdministrativeRecordsDataContext));
            AssertNullDependency(typeof(ApplicationService), typeof(IApplicationDataContext));
            AssertNullDependency(typeof(BillPurchasesService), typeof(IBillPurchasesDataContext));
            AssertNullDependency(typeof(BudgetApplicationsService), typeof(IBudgetApplicationsDataContext));
            AssertNullDependency(typeof(BudgetsService), typeof(IBudgetsDataContext));
            AssertNullDependency(typeof(ContractTypesService), typeof(IContractTypesDataContext));
            AssertNullDependency(typeof(CostPlacesService), typeof(ICostPlacesDataContext));
            AssertNullDependency(typeof(CreditModificationBudgetsService), typeof(ICreditModificationBudgetsDataContext));
            AssertNullDependency(typeof(CreditModificationsService), typeof(ICreditModificationsDataContext));
            AssertNullDependency(typeof(CreditModificationTypesService), typeof(ICreditModificationTypesDataContext));
            AssertNullDependency(typeof(DocumentTypesService), typeof(IDocumentTypesDataContext));
            AssertNullDependency(typeof(ExtraBudgetaryApplicationsService), typeof(IExtraBudgetaryApplicationsDataContext));
            AssertNullDependency(typeof(ExtraBudgetaryRecordsService), typeof(IExtraBudgetaryRecordsDataContext));
            AssertNullDependency(typeof(OriginsService), typeof(IOriginsDataContext));
            AssertNullDependency(typeof(ParametersService), typeof(IParametersDataContext));
            AssertNullDependency(typeof(PayFormsService), typeof(IPayFormsDataContext));
            AssertNullDependency(typeof(PayTypesService), typeof(IPayTypesDataContext));
            AssertNullDependency(typeof(ProgramsService), typeof(IProgramsDataContext));
            AssertNullDependency(typeof(ProvenancesService), typeof(IProvenancesDataContext));
            AssertNullDependency(typeof(ProvidersService), typeof(IProvidersDataContext));
            AssertNullDependency(typeof(ProvincesService), typeof(IProvincesDataContext));
            AssertNullDependency(typeof(RecordTypesService), typeof(IRecordTypesDataContext));
            AssertNullDependency(typeof(RectificationsService), typeof(IRectificationsDataContext));
            AssertNullDependency(typeof(SingsService), typeof(ISingsDataContext));
            AssertNullDependency(typeof(TonnageSheetsService), typeof(ITonnageSheetsDataContext));
            AssertNullDependency(typeof(TreasuriesService), typeof(ITreasuriesDataContext));
            AssertNullDependency(typeof(TreasuryLinesService), typeof(ITreasuryLinesDataContext));
            AssertNullDependency(typeof(UserService), typeof(IUserDataContext));
        }

        private static void AssertNullDependency(Type serviceType, Type contextType)
        {
            var constructor = serviceType.GetConstructor(new[] { contextType });
            Assert.IsNotNull(constructor, serviceType.FullName);
            var exception = Assert.ThrowsException<TargetInvocationException>(() => constructor.Invoke(new object[] { null }));
            Assert.IsInstanceOfType(exception.InnerException, typeof(ArgumentNullException));
        }

        private static void VerifyService(Type serviceType, Type contextType)
        {
            var fake = new RecordingDataContext();
            var service = Activator.CreateInstance(serviceType, new object[] { fake });

            foreach (var contextMethod in contextType.GetMethods())
            {
                var parameterInfos = contextMethod.GetParameters();
                var arguments = new object[parameterInfos.Length];
                for (var index = 0; index < parameterInfos.Length; index++)
                {
                    arguments[index] = TestValueFactory.CreateArgument(parameterInfos[index].ParameterType, index + 1);
                }

                var expected = TestValueFactory.CreateReturn(contextMethod.ReturnType, contextMethod.Name);
                fake.ReturnValue = expected;
                fake.Reset();

                var serviceMethod = serviceType.GetMethod(
                    contextMethod.Name,
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    parameterInfos.Select(parameter => parameter.ParameterType).ToArray(),
                    null);

                Assert.IsNotNull(serviceMethod, serviceType.FullName + "." + contextMethod.Name);
                var actual = serviceMethod.Invoke(service, arguments);

                Assert.AreEqual(contextMethod.Name, fake.LastMethodName.Substring(fake.LastMethodName.IndexOf('.') + 1));
                Assert.AreEqual(arguments.Length, fake.LastArguments.Length);
                for (var index = 0; index < arguments.Length; index++)
                {
                    Assert.AreEqual(arguments[index], fake.LastArguments[index]);
                }

                if (contextMethod.ReturnType.IsValueType)
                {
                    Assert.AreEqual(expected, actual);
                }
                else
                {
                    Assert.AreSame(expected, actual);
                }
            }
        }
    }

    internal static class TestValueFactory
    {
        public static object CreateArgument(Type type, int seed)
        {
            if (type == typeof(string)) return "argument-" + seed;
            if (type == typeof(int)) return seed + 100;
            if (type == typeof(bool)) return true;
            if (type == typeof(decimal)) return seed + 0.5m;
            if (type == typeof(DateTime)) return new DateTime(2020, 1, 1).AddDays(seed);
            if (type == typeof(byte)) return (byte)(seed + 1);
            if (type == typeof(short)) return (short)(seed + 1);
            if (type == typeof(long)) return (long)(seed + 1);
            if (type.IsEnum) return Enum.GetValues(type).GetValue(0);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Activator.CreateInstance(type, new[] { CreateArgument(type.GetGenericArguments()[0], seed) });
            }
            if (type.IsArray) return Array.CreateInstance(type.GetElementType(), 0);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return Activator.CreateInstance(type);
            }

            return FormatterServices.GetUninitializedObject(type);
        }

        public static object CreateReturn(Type type, string methodName)
        {
            if (type == typeof(string)) return "return-" + methodName;
            if (type == typeof(int)) return 9001;
            if (type == typeof(bool)) return true;
            if (type == typeof(decimal)) return 9001.5m;
            if (type == typeof(DateTime)) return new DateTime(2021, 2, 3);
            if (type == typeof(byte)) return (byte)200;
            if (type == typeof(short)) return (short)200;
            if (type == typeof(long)) return (long)200;
            if (type.IsEnum) return Enum.GetValues(type).GetValue(0);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Activator.CreateInstance(type, new[] { CreateReturn(type.GetGenericArguments()[0], methodName) });
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return Activator.CreateInstance(type);
            }

            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
