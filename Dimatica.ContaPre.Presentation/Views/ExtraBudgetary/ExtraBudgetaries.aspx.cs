namespace Dimatica.ContaPre.Presentation.Views.ExtraBudgetary
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class ExtraBudgetaries : BasePage
    {
        #region Fields

        private IExtraBudgetaryApplicationsService extraBudgetaryApplicationsService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();
        
        private IExtraBudgetaryRecordsService extraBudgetaryRecordsService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "ExtraBudgetaries";

                this.RmyYear.SelectedDate = DateTime.Now;

                var extraBudgetaryTypes = this.extraBudgetaryApplicationsService.GetExtraBudgetaryTypes();

                extraBudgetaryTypes.Insert(
                                           0,
                                           new PRE_TIPO_EXTRAP
                                           {
                                               TIP_EXTRAP_CODIGO_AUX = -2,
                                               TIP_EXTRAP_DESCRIPCION = "Todos menos Descuentos"
                                           });
                extraBudgetaryTypes.Insert(
                                           0,
                                           new PRE_TIPO_EXTRAP
                                           {
                                               TIP_EXTRAP_CODIGO_AUX = -1,
                                               TIP_EXTRAP_DESCRIPCION = "< Selecccione >"
                                           });

                this.RcExtraBudgetaryTypes.DataSource = extraBudgetaryTypes;
                this.RcExtraBudgetaryTypes.DataBind();

                this.RcExtraBudgetaryTypes.SelectedValue = "-2";

                var extraBudgetaryApplications = this.extraBudgetaryApplicationsService.GetExtraBudgetaryApplicationsToCombo();

                extraBudgetaryApplications.Insert(
                                                  0,
                                                  new PRE_EXTRAPRESUPUESTARIA
                                                  {
                                                      EXTRAPRE_CODIGO = -1,
                                                      EXTRAPRE_DESCRIPCION = "< Seleccione >"
                                                  });

                this.RcExtraBudgetaryApplications.DataSource = extraBudgetaryApplications;
                this.RcExtraBudgetaryApplications.DataBind();

                //var providers = this.providersService.GetProvidersToCombo();
                //providers.Insert(
                //                 0,
                //                 new PRE_PROVEEDOR
                //                 {
                //                     PROV_CODIGO = -1,
                //                     PROV_NOMBRE = "< Seleccione >"
                //                 });

                //this.RcProviders.DataSource = providers;
                //this.RcProviders.DataBind();
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillFiles(true);
        }

        #endregion

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
            if (e.CommandName == "UpdateExtraBudgetary")
            {
                var item = e.Item as GridDataItem;
                var extraBudgetaryId = (int)item.GetDataKeyValue("EXP_EXTRAP_CODIGO");

                this.Response.Redirect($"~/Views/ExtraBudgetary/ManageExtraBudgetary.aspx?id={extraBudgetaryId}");
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
            var year = this.RmyYear.SelectedDate?.Year;
            var extraBudgetaryType = this.RcExtraBudgetaryTypes.SelectedValue.Equals("-1") ? (int?)null : this.RcExtraBudgetaryTypes.SelectedValue.Equals("-2") ? 0 : Convert.ToInt32(this.RcExtraBudgetaryTypes.SelectedValue);
            var extraBudgetaryApplication = this.RcExtraBudgetaryApplications.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcExtraBudgetaryApplications.SelectedValue);
            var isBound = string.IsNullOrWhiteSpace(this.RcBound.SelectedValue) ? (bool?)null : this.RcBound.SelectedValue.Equals("1");
            var sinceDate = this.RdSince.SelectedDate?.ToString("yyyy-MM-dd");
            var untilDate = this.RdUntil.SelectedDate?.ToString("yyyy-MM-dd");
            var fileNumberSince = this.RntSince.Value == null ? (int?)null : Convert.ToInt32(this.RntSince.Value);
            var fileNumberUntil = this.RntUntil.Value == null ? (int?)null : Convert.ToInt32(this.RntUntil.Value);
            //var providerCode = this.RcProviders.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcProviders.SelectedValue);
            //var isOperationType = string.IsNullOrWhiteSpace(this.RcOperationType.SelectedValue) ? string.Empty : this.RcOperationType.SelectedValue;

            var recordsResult = this.extraBudgetaryRecordsService.GetByFilters(year, extraBudgetaryType, extraBudgetaryApplication, isBound, sinceDate, untilDate, fileNumberSince, fileNumberUntil, null);

            this.RgFiles.DataSource = recordsResult;

            if (manual)
            {
                this.RgFiles.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }
    }
}