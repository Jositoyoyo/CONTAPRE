namespace Dimatica.ContaPre.Presentation.Helpers
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.Web;
    using Dimatica.ContaPre.OL.Business;

    #endregion

    public static class SiteDataHelper
    {
        #region Public Static Methods

        public static List<SiteDataItem> GetSiteDataItems()
        {
            var siteDataItems = new List<SiteDataItem>();

            siteDataItems.Add(new SiteDataItem(1, 0, "Presupuestos"));
            siteDataItems.Add(new SiteDataItem(2, 1, "Ingresos", "~/Views/Budget/Incomes.aspx"));
            siteDataItems.Add(new SiteDataItem(3, 1, "Gastos", "~/Views/Budget/Spends.aspx"));
            siteDataItems.Add(new SiteDataItem(4, 1, "Modificaciones Crédito", "~/Views/Budget/CreditModifications.aspx"));

            siteDataItems.Add(new SiteDataItem(10, 0, "Ingresos"));
            siteDataItems.Add(new SiteDataItem(11, 10, "Expedientes", "~/Views/Income/Files.aspx"));
            siteDataItems.Add(new SiteDataItem(12, 10, "Agrupar DR", "~/Views/Income/GroupDr.aspx"));
            siteDataItems.Add(new SiteDataItem(13, 10, "Estado Cuentas Restringidas", "~/Views/Income/StatementAccounts.aspx"));

            siteDataItems.Add(new SiteDataItem(20, 0, "Gastos"));
            siteDataItems.Add(new SiteDataItem(21, 20, "Exped. Administrativos", "~/Views/Spend/AdministrativeRecords.aspx"));
            siteDataItems.Add(new SiteDataItem(22, 20, "Exped. Gastos", "~/Views/Spend/SpendRecords.aspx"));
            siteDataItems.Add(new SiteDataItem(23, 20, "Consulta Aplic/Importe", "~/Views/Spend/CheckApplicationAmount.aspx"));
            siteDataItems.Add(new SiteDataItem(24, 20, "Comprobar Expedientes", "~/Views/Spend/CheckRecords.aspx"));
            siteDataItems.Add(new SiteDataItem(25, 20, "Consultar Facturas Compras", "~/Views/Spend/CheckPurchases.aspx"));

            siteDataItems.Add(new SiteDataItem(30, 0, "Listados"));
            siteDataItems.Add(new SiteDataItem(31, 30, "Listados Gastos", "~/Views/List/SpendsList.aspx"));
            siteDataItems.Add(new SiteDataItem(32, 30, "Listados Ingresos", "~/Views/List/IncomesList.aspx"));
            siteDataItems.Add(new SiteDataItem(33, 30, "Listados Extrapresup.", "~/Views/List/ExtrabudgetaryList.aspx"));
            siteDataItems.Add(new SiteDataItem(34, 30, "Listados Tesorería", "~/Views/List/TreasuriesList.aspx"));

            siteDataItems.Add(new SiteDataItem(40, 0, "Extrapresupuestarias", "~/Views/ExtraBudgetary/ExtraBudgetaries.aspx"));

            siteDataItems.Add(new SiteDataItem(50, 0, "Tesorería"));
            siteDataItems.Add(new SiteDataItem(51, 50, "Asignar Hoja Arqueo", "~/Views/Treasury/AssignTonnageSheets.aspx"));
            siteDataItems.Add(new SiteDataItem(52, 50, "Ver Hoja Arqueo", "~/Views/Treasury/SeeTonnageSheets.aspx"));
            siteDataItems.Add(new SiteDataItem(53, 50, "Apuntes Tesorería", "~/Views/Treasury/NotesTreasuries.aspx"));
            siteDataItems.Add(new SiteDataItem(54, 50, "Alta Registro Pagos", "~/Views/Treasury/PaymentRegister.aspx"));

            siteDataItems.Add(new SiteDataItem(60, 0, "Rectificaciones", "~/Views/Rectification/Rectifications.aspx"));

            siteDataItems.Add(new SiteDataItem(70, 0, "Señalamientos", "~/Views/Pointing/Sings.aspx"));

            siteDataItems.Add(new SiteDataItem(80, 0, "Sistema"));
            siteDataItems.Add(new SiteDataItem(82, 80, "Parámetros", "~/Views/Systems/Parameters.aspx"));
            siteDataItems.Add(new SiteDataItem(83, 80, "Cambiar Password", "~/Views/Systems/ChangePassword.aspx"));
            siteDataItems.Add(new SiteDataItem(84, 80, "Gestion Usuarios", "~/Views/Systems/Users/Users.aspx"));

            siteDataItems.Add(new SiteDataItem(90, 0, "Tablas"));
            siteDataItems.Add(new SiteDataItem(91, 90, "Aplicaciones", "~/Views/Maintenance/Applications.aspx"));
            siteDataItems.Add(new SiteDataItem(92, 90, "Cuentas Restringidas", "~/Views/Maintenance/AccountsRestricted.aspx"));
            siteDataItems.Add(new SiteDataItem(93, 90, "Aplic. Extrapresup.", "~/Views/Maintenance/ExtraBudgetaryApplications.aspx"));
            siteDataItems.Add(new SiteDataItem(94, 90, "Formas de Pago", "~/Views/Maintenance/PayForms.aspx"));
            siteDataItems.Add(new SiteDataItem(95, 90, "Líneas de Tesorería", "~/Views/Maintenance/TreasuryLines.aspx"));
            siteDataItems.Add(new SiteDataItem(96, 90, "Programas", "~/Views/Maintenance/Programs.aspx"));
            siteDataItems.Add(new SiteDataItem(97, 90, "Procedencias", "~/Views/Maintenance/Provenances.aspx"));
            siteDataItems.Add(new SiteDataItem(98, 90, "Proveedores", "~/Views/Maintenance/Providers.aspx"));
            siteDataItems.Add(new SiteDataItem(99, 90, "Tipos Documentos", "~/Views/Maintenance/DocumentTypes.aspx"));
            siteDataItems.Add(new SiteDataItem(100, 90, "Tipos Modificación de Crédito", "~/Views/Maintenance/CreditModificationTypes.aspx"));
            siteDataItems.Add(new SiteDataItem(101, 90, "Tipos de Pago", "~/Views/Maintenance/PayTypes.aspx"));

            // Verificación de localhost
            if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
            {
                siteDataItems.Add(new SiteDataItem(200, 0, "Development"));
                siteDataItems.Add(new SiteDataItem(201, 200, "Ajax Form", "~/Views/Development/AjaxForm.aspx"));
                siteDataItems.Add(new SiteDataItem(202, 200, "Alert, Confirm and Prompt", "~/Views/Development/AlertConfirmPrompt.aspx"));
                siteDataItems.Add(new SiteDataItem(203, 200, "Home Test", "~/Views/Development/AjaxForm.aspx"));
                siteDataItems.Add(new SiteDataItem(204, 200, "RadWindow", "~/Views/Development/RadWindow.aspx"));
                siteDataItems.Add(new SiteDataItem(205, 200, "Send Email", "~/Views/Development/SendEmail.aspx"));
            }


            siteDataItems.Add(new SiteDataItem(900, 0, "Cerrar Sesión", "~/Views/Account/Login.aspx?SignOut=1"));

            return siteDataItems;
        }

        #endregion
    }
}
