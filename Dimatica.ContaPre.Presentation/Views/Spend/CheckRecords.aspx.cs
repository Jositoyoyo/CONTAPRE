namespace Dimatica.ContaPre.Presentation.Views.Spend
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

    public partial class CheckRecords : BasePage
    {
        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "CheckRecords";

                var years = this.accountingRecordsService.GetYears("G");

                this.RcYears.DataSource = years;
                this.RcYears.DataBind();
            }
        }

        protected void RcYears_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var year = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            this.FillRecords(year, true);
        }

        protected void RgRecords_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var year = string.IsNullOrWhiteSpace(this.RcYears.SelectedValue) ? string.Empty : this.RcYears.SelectedValue;
            this.FillRecords(year, false);
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

        private void FillRecords(string year, bool manual)
        {
            var records = new List<PRE_EXPEDIENTE_CONTABLE>();

            if (!string.IsNullOrEmpty(year))
            {
                var realYear = Convert.ToInt32(year);

                var recordsResult = this.accountingRecordsService.GetSpends(realYear, null, null, null, null, null, null, null);

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
        }

        #endregion
    }
}