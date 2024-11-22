namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SpendRecords : BasePage
    {
        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        private IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "SpendRecords";

                var years = this.accountingRecordsService.GetYears("G");
                var year = years.FirstOrDefault().Value;
                years.Insert(0, new Year
                                        {
                                                Value = "< Seleccione >"
                                        });
                this.RcBudgetYears.DataSource = years;
                this.RcBudgetYears.DataBind();

                this.RcBudgetYears.SelectedValue = year;

                var provenances = this.provenancesService.GetProvenances("G");
                provenances.Insert(
                                   0,
                                   new PRE_PROCEDENCIA
                                           {
                                                   PROC_CODIGO = -1,
                                                   PROC_DESCRIPCION = "< Seleccione >"
                                           });

                this.RcProvenances.DataSource = provenances;
                this.RcProvenances.DataBind();

                var providers = this.providersService.GetProvidersToCombo();
                providers.Insert(
                                 0,
                                 new PRE_PROVEEDOR
                                         {
                                                 PROV_CODIGO = -1,
                                                 PROV_NOMBRE = "< Seleccione >"
                                         });
                this.RcProviders.DataSource = providers;
                this.RcProviders.DataBind();
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillRecords(true);
        }

        protected void RgRecords_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillRecords(false);
        }

        protected void RgRecords_OnPreRender(object sender, EventArgs e) { }

        protected void RgRecords_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateSpendRecord")
            {
                var item = e.Item as GridDataItem;
                var recordId = (int)item.GetDataKeyValue("EXP_CODIGO");

                this.Session["_currentSource"] = this.Request.Url.AbsoluteUri;
                this.Response.Redirect($"~/Views/Spend/ManageSpendRecord.aspx?id={recordId}");
            }
        }

        private void FillRecords(bool manual)
        {
            var budgetYear = this.RcBudgetYears.SelectedValue.Equals("< Seleccione >") ? (int?)null : Convert.ToInt32(this.RcBudgetYears.SelectedValue);
            var recordNumber = this.RntNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntNumber.Value);
            var provenanceId = this.RcProvenances.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.RcProvenances.SelectedValue);
            var providerId = this.RcProviders.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.RcProviders.SelectedValue);
            var nifFirst = string.IsNullOrWhiteSpace(this.RtbNifFirst.Text) ? string.Empty : this.RtbNifFirst.Text;
            var nifNumber = string.IsNullOrWhiteSpace(this.RtbNifNumber.Text) ? string.Empty : this.RtbNifNumber.Text;
            var nifLast = string.IsNullOrWhiteSpace(this.RtbNifLast.Text) ? string.Empty : this.RtbNifLast.Text;
            var nifAux = $"{nifFirst}-{nifNumber}-{nifLast}";
            var providerNif = nifAux.Equals("--") ? string.Empty : nifAux;
            var multiYear = string.IsNullOrWhiteSpace(this.RcMultiYear.SelectedValue) ? (bool?)null : this.RcMultiYear.SelectedValue.Equals("1");

            var records = new List<PRE_EXPEDIENTE_CONTABLE>();

            if (budgetYear != null || recordNumber != null || provenanceId != null || providerId != null || !string.IsNullOrWhiteSpace(providerNif) || multiYear != null)
            {
                records = this.accountingRecordsService.GetSpends(budgetYear, recordNumber, provenanceId, providerId, providerNif, multiYear, string.Empty, string.Empty);
            }

            this.RgRecords.DataSource = records;

            if (manual)
            {
                this.RgRecords.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }

        #endregion
    }
}