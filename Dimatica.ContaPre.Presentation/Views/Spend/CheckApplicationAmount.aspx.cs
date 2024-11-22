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

    public partial class CheckApplicationAmount : BasePage
    {
        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        private IBudgetApplicationsService budgetApplicationsService = DependencyFactory.GetInstance<IBudgetApplicationsService>();

        private IDocumentTypesService documentTypesService = DependencyFactory.GetInstance<IDocumentTypesService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "CheckApplicationAmount";

                var years = this.accountingRecordsService.GetYears("G");
                var year = years.FirstOrDefault().Value;
                years.Insert(0, new Year
                {
                    Value = "< Seleccione >"
                });
                this.RcBudgetYears.DataSource = years;
                this.RcBudgetYears.DataBind();

                this.RcBudgetYears.SelectedValue = year;

                var selected = this.RcBudgetYears.SelectedValue;

                if (!selected.Equals("< Seleccione >"))
                {
                    this.FillApplications(selected);
                }

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

                var documents = this.documentTypesService.GetDocumentTypesToCombo("G");
                documents.Insert(
                                 0,
                                 new PRE_TIPO_DOCUMENTO
                                 {
                                     TIPD_CODIGO = -1
                                 });

                this.RcDocumentTypes.DataSource = documents;
                this.RcDocumentTypes.DataBind();
            }
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

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillRecords(true);
        }

        private void FillRecords(bool manual)
        {
            var records = new List<PRE_EXPEDIENTE_CONTABLE>();

            var year = this.RcBudgetYears.SelectedValue.Equals("< Seleccione >") ? string.Empty : this.RcBudgetYears.SelectedValue;

            var realYear = string.IsNullOrEmpty(year) ? (int?)null : Convert.ToInt32(year);

            var application = this.RcApplications.SelectedValue;
            int? chapterCode = null;
            int? articleCode = null;
            int? conceptCode = null;
            int? subConceptCode = null;

            if (!string.IsNullOrWhiteSpace(application))
            {
                if (!application.Equals("-1"))
                {
                    var splits = application.Split('/');

                    if (splits.Length >= 1)
                    {
                        if (!string.IsNullOrWhiteSpace(splits[0]))
                        {
                            chapterCode = Convert.ToInt32(splits[0]);
                        }
                    }

                    if (splits.Length >= 2)
                    {
                        if (!string.IsNullOrWhiteSpace(splits[1]))
                        {
                            articleCode = Convert.ToInt32(splits[1]);
                        }
                    }

                    if (splits.Length >= 3)
                    {
                        if (!string.IsNullOrWhiteSpace(splits[2]))
                        {
                            conceptCode = Convert.ToInt32(splits[2]);
                        }
                    }

                    if (splits.Length >= 4)
                    {
                        if (!string.IsNullOrWhiteSpace(splits[3]))
                        {
                            subConceptCode = Convert.ToInt32(splits[3]);
                        }
                    }
                }
            }

            var sinceAmount = this.RntSince.Value == null ? (decimal?)null : Convert.ToDecimal(this.RntSince.Value);
            var untilAmount = this.RntUntil.Value == null ? (decimal?)null : Convert.ToDecimal(this.RntUntil.Value);

            var bound = string.IsNullOrWhiteSpace(this.RcBound.SelectedValue) ? (bool?)null : this.RcBound.SelectedValue.Equals("1");
            var square = string.IsNullOrWhiteSpace(this.RcSquare.SelectedValue) ? (bool?)null : this.RcSquare.SelectedValue.Equals("1");

            var providerId = this.RcProviders.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcProviders.SelectedValue);
            var nifFirst = string.IsNullOrWhiteSpace(this.RtbNifFirst.Text) ? string.Empty : this.RtbNifFirst.Text;
            var nifNumber = string.IsNullOrWhiteSpace(this.RtbNifNumber.Text) ? string.Empty : this.RtbNifNumber.Text;
            var nifLast = string.IsNullOrWhiteSpace(this.RtbNifLast.Text) ? string.Empty : this.RtbNifLast.Text;
            var nifAux = $"{nifFirst}-{nifNumber}-{nifLast}";
            var providerNif = nifAux.Equals("--") ? string.Empty : nifAux;

            var docType = this.RcDocumentTypes.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcDocumentTypes.SelectedValue);
            var docNumber = this.RntNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntNumber.Value);

            if (realYear != null || chapterCode != null || articleCode != null || conceptCode != null || subConceptCode != null || sinceAmount != null || untilAmount != null || bound != null || square != null
                || providerId != null || !string.IsNullOrWhiteSpace(providerNif) || docType != null || docNumber != null)
            {
                var recordsResult = this.accountingRecordsService.GetSpendsWithAppAmount(realYear, chapterCode, articleCode, conceptCode, subConceptCode, sinceAmount, untilAmount, bound, square, providerId, providerNif, docType, docNumber, null, null);

                foreach (var record in recordsResult)
                {
                    records.Add(record);
                }
            }

            this.RgRecords.DataSource = records;

            if (manual)
            {
                this.RgRecords.DataBind();
            }

            var total = records.Sum(r => r.IMPORTE);
            this.txtTotal.Text = total.ToString("N");

            this.RpbFilter.CollapseAllItems();
        }

        protected void RcYears_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var year = e.Value.Equals("< Seleccione >") ? string.Empty : e.Value;
            this.FillApplications(year);
        }

        private void FillApplications(string year)
        {
            var applications = new List<BudgetApplication>();

            if (!string.IsNullOrWhiteSpace(year))
            {
                var realYear = Convert.ToInt32(year);

                applications = this.budgetApplicationsService.GetChaptersNumbersByTypeByYear("G", realYear);
            }

            applications.Insert(
                                0,
                                new BudgetApplication
                                {
                                    CacsCode = "-1",
                                    Description = "< Seleccione >"
                                });

            this.RcApplications.DataSource = applications;
            this.RcApplications.DataBind();
        }

        #endregion
    }
}