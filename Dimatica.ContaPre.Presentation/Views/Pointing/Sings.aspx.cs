namespace Dimatica.ContaPre.Presentation.Views.Pointing
{
    #region NameSpaces

    using System;
    using System.Globalization;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class Sings : BasePage
    {
        #region Fields

        private ISingsService singsService = DependencyFactory.GetInstance<ISingsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "Sings";

                this.RmyYear.SelectedDate = DateTime.Now;
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.FillSings(true);
        }

        protected void RgSings_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillSings(false);
        }

        protected void RgSings_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdatePointing")
            {
                var item = e.Item as GridDataItem;
                var pointingId = (int)item.GetDataKeyValue("SEN_CODIGO");

                this.Response.Redirect($"~/Views/Pointing/ManagePointing.aspx?id={pointingId}");
            }
        }

        protected void RgSings_PreRender(object sender, EventArgs e) { }

        private void FillSings(bool manual)
        {
            var year = ((DateTime)this.RmyYear.SelectedDate).Year;
            var pointingNumber = this.RntPointingNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntPointingNumber.Value);
            var date = this.RdpDate.SelectedDate == null ? string.Empty : ((DateTime)this.RdpDate.SelectedDate).ToString("d", new CultureInfo("es-ES"));
            var amount = this.RntAmount.Value == null ? (decimal?)null : Convert.ToDecimal(this.RntAmount.Value);
            var fileNumber = this.RntFileNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntFileNumber.Value);
            var type = this.RcType.SelectedValue.Equals("-1") ? string.Empty : this.RcType.SelectedValue;

            var sings = this.singsService.GetByFilters(year, pointingNumber, date, amount, fileNumber, type);

            this.RgSings.DataSource = sings;

            if (manual)
            {
                this.RgSings.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }

        #endregion
    }
}