namespace Dimatica.ContaPre.Presentation.Views.Income
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class Files : BasePage
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
                this.Session["_currentPage"] = "Files";

                var years = this.accountingRecordsService.GetYears("I");

                var selected = years.FirstOrDefault().Value;

                years.Insert(
                             0,
                             new Year
                                     {
                                             Value = "< Seleccione >"
                                     });

                this.RcYears.DataSource = years;
                this.RcYears.DataBind();

                this.RcYears.SelectedValue = selected;

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

                var documents = this.documentTypesService.GetDocumentTypesToCombo("I");
                documents.Insert(
                                 0,
                                 new PRE_TIPO_DOCUMENTO
                                         {
                                                 TIPD_DESCRIPCION = "-1",
                                                 TIPD_NOMBRE_CORTO = "< Seleccione >"
                                         });

                this.RcDocumentTypes.DataSource = documents;
                this.RcDocumentTypes.DataBind();
            }
        }

        protected void RcYears_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var year = e.Value.Equals("< Seleccione >") ? string.Empty : e.Value;
            this.FillApplications(year);
        }

        protected void RgFiles_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillFiles(false);
        }

        protected void RgFiles_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgFiles.MasterTableView.GetColumn("EditColumn").Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgFiles.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgFiles_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAccountingRecord")
            {
                var item = e.Item as GridDataItem;
                var accountingRecordId = (int)item.GetDataKeyValue("EXP_CODIGO");

                this.Response.Redirect($"~/Views/Income/ManageFile.aspx?id={accountingRecordId}");
            }
        }

        protected void RgFiles_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Expediente";
            }
        }

        private void FillFiles(bool manual)
        {
            var records = new List<PRE_EXPEDIENTE_CONTABLE>();

            var year = this.RcYears.SelectedValue.Equals("< Seleccione >") ? (int?)null : Convert.ToInt32(this.RcYears.SelectedValue);

            var square = string.IsNullOrWhiteSpace(this.RcSquare.SelectedValue) ? (bool?)null : this.RcSquare.SelectedValue.Equals("1");
            var bound = string.IsNullOrWhiteSpace(this.RcBound.SelectedValue) ? (bool?)null : this.RcBound.SelectedValue.Equals("1");
            var sinceDate = this.RdSince.SelectedDate?.ToString("yyyy-MM-dd");
            var untilDate = this.RdUntil.SelectedDate?.ToString("yyyy-MM-dd");
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

            var operationYear = this.RntOperationYear.Value == null ? (int?)null : Convert.ToInt32(this.RntOperationYear.Value);

            decimal? sinceAmount = null;
            decimal? untilAmount = null;

            if (this.RntSince.Value != null)
            {
                sinceAmount = Convert.ToDecimal(this.RntSince.Value);
            }

            if (this.RntUntil.Value != null)
            {
                untilAmount = Convert.ToDecimal(this.RntUntil.Value);
            }

            int? providerCode = null;

            if (!string.IsNullOrWhiteSpace(this.RcProviders.SelectedValue))
            {
                if (!this.RcProviders.SelectedValue.Equals("-1"))
                {
                    providerCode = Convert.ToInt32(this.RcProviders.SelectedValue);
                }
            }

            string docType = null;

            if (!string.IsNullOrWhiteSpace(this.RcDocumentTypes.SelectedValue))
            {
                if (!this.RcDocumentTypes.SelectedValue.Equals("-1"))
                {
                    docType = this.RcDocumentTypes.SelectedValue;
                }
            }

            int? docNumber = null;

            if (this.RntNumber.Value != null)
            {
                docNumber = Convert.ToInt32(this.RntNumber.Value);
            }

            var recordsResult = this.accountingRecordsService.GetIncomesWithAppAmount(year, this.RtbDescription.Text, square, providerCode, sinceDate, untilDate, sinceAmount, untilAmount, chapterCode, articleCode, conceptCode, subConceptCode, bound, operationYear, docType, docNumber, null, null);

            foreach (var record in recordsResult)
            {
                records.Add(record);
            }

            this.RgFiles.DataSource = records;

            if (manual)
            {
                this.RgFiles.DataBind();
            }

            var total = records.Sum(r => r.IMPORTE);
            this.txtTotal.Text = total.ToString("N");

            this.RpbFilter.CollapseAllItems();
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillFiles(true);
        }

        private void FillApplications(string year)
        {
            var applications = new List<BudgetApplication>();

            if (!string.IsNullOrWhiteSpace(year))
            {
                var realYear = Convert.ToInt32(year);

                applications = this.budgetApplicationsService.GetChaptersNumbersByTypeByYear("I", realYear);
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