namespace Dimatica.ContaPre.Presentation.Views.Treasury
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeeTonnageSheets : BasePage
    {
        #region Fields

        private IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        private ITonnageSheetsService tonnageSheetsService = DependencyFactory.GetInstance<ITonnageSheetsService>();

        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Private Methods

        private void FillSeeTonnageSheets(bool manual)
        {
            var exerciseYear = this.RmyExerciseYear.SelectedDate?.Year;
            var accountingCode = this.RcAccounts.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcAccounts.SelectedValue);
            var sheetSinceNumber = this.RntSinceSheetNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntSinceSheetNumber.Value);
            var sheetUntilNumber = this.RntUntilSheetNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntUntilSheetNumber.Value);
            var sheetDateSince = this.RdpSinceDate.SelectedDate?.ToString("yyyy-MM-dd");
            var sheetDateUntil = this.RdpUntilDate.SelectedDate?.ToString("yyyy-MM-dd");

            var sheets = this.tonnageSheetsService.GetByFilters(exerciseYear, accountingCode, sheetSinceNumber, sheetUntilNumber, sheetDateSince, sheetDateUntil, null, null);

            this.RgTonnageSheet.DataSource = sheets;

            if (manual)
            {
                this.RgTonnageSheet.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "SeeTonnageSheets";

                this.RmyExerciseYear.SelectedDate = DateTime.Now;

                var accounts = this.accountRestrictedService.GetAccountsRestricted("I");
                accounts.Insert(
                                0,
                                new PRE_CUENTA_RESTRINGIDA
                                {
                                    CUE_CODIGO = -1
                                });
                this.RcAccounts.DataSource = accounts;
                this.RcAccounts.DataBind();
            }
        }

        protected void RgTonnageSheet_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillSeeTonnageSheets(false);
        }

        protected void RgTonnageSheet_InsertCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_UpdateCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_DeleteCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_ItemDataBound(object sender, GridItemEventArgs e) { }

        protected void RgTonnageSheet_PreRender(object sender, EventArgs e) { }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.FillSeeTonnageSheets(true);
        }

        protected void RgTonnageSheet_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateTonnageSheet")
            {
                var item = e.Item as GridDataItem;
                var tonnageId = (int)item.GetDataKeyValue("HOJ_CODIGO");

                this.Response.Redirect($"~/Views/Treasury/ManageTonnageSheet.aspx?id={tonnageId}");
            }
        }

        #endregion
    }
}