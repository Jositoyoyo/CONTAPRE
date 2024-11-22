using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dimatica.ContaPre.BLL.Configs;
using Dimatica.ContaPre.BLL.Interfaces;
using Dimatica.ContaPre.OL.Procedures;
using Dimatica.ContaPre.Presentation.Views.Shared;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace Dimatica.ContaPre.Presentation.Views.Rectification
{
    using System.Text;

    public partial class Rectifications : BasePage
    {
        #region Static Fields and Constants

        private IRectificationsService rectificationsService = DependencyFactory.GetInstance<IRectificationsService>();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "Rectifications";

                if (!string.IsNullOrWhiteSpace(LoginUser?.USU_I_G))
                {
                    this.RmyDatesNegative.SelectedDate = DateTime.Now;
                    this.RmyDatesPositive.SelectedDate = DateTime.Now;
                }
            }
        }

        protected void btnReport_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            if (this.RgRectificationsNegatives.SelectedItems.Count == 0 || this.RgRectificationsPositives.SelectedItems.Count == 0)
            {
                strBuilder.Append("Debe seleccionar al menos una rectificación negativa y una positiva para ver el reporte.");
                this.ShowMessage(this.RadNotification, "Imposible ver rectificaciones", strBuilder, MessageType.Warning);

                return;
            }

            var iCodes = string.Empty;
            var eCodes = string.Empty;

            foreach (GridDataItem item in this.RgRectificationsNegatives.SelectedItems)
            {
                var origin = item.GetDataKeyValue("Origin").ToString();
                var code = item.GetDataKeyValue("Code").ToString();

                switch (origin)
                {
                    case "I":
                        iCodes = string.IsNullOrWhiteSpace(iCodes) ? code : $"{iCodes},{code}";
                        break;
                    case "E":
                        eCodes = string.IsNullOrWhiteSpace(eCodes) ? code : $"{eCodes},{code}";
                        break;
                }
            }

            foreach (GridDataItem item in this.RgRectificationsPositives.SelectedItems)
            {
                var origin = item.GetDataKeyValue("Origin").ToString();
                var code = item.GetDataKeyValue("Code").ToString();

                switch (origin)
                {
                    case "I":
                        iCodes = string.IsNullOrWhiteSpace(iCodes) ? code : $"{iCodes},{code}";
                        break;
                    case "E":
                        eCodes = string.IsNullOrWhiteSpace(eCodes) ? code : $"{eCodes},{code}";
                        break;
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSelectedDate", $"showSelectedDate('{iCodes}','{eCodes}');", true);
        }

        protected void RcTypesNegative_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            var year = this.RmyDatesNegative.SelectedDate;

            this.FillNegativesRectifications(type, year, true);
        }

        protected void RmyDatesNegative_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypesNegative.SelectedValue) ? string.Empty : this.RcTypesNegative.SelectedValue;
            var year = e.NewDate;

            this.FillNegativesRectifications(type, year, true);
        }

        private void FillNegativesRectifications(string type, DateTime? year, bool manual)
        {
            var rectifications = new List<Dimatica.ContaPre.OL.Procedures.Rectification>();
            if (year != null)
            {
                var realYear = ((DateTime)year).Year;
                var rectificationsResult = this.rectificationsService.GetRectifications("-", type, realYear);
                foreach (var r in rectificationsResult)
                {
                    rectifications.Add(r);
                }
            }

            this.RgRectificationsNegatives.DataSource = rectifications;
            if (manual)
            {
                this.RgRectificationsNegatives.DataBind();
            }
        }

        protected void RgRectificationsNegatives_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypesNegative.SelectedValue) ? string.Empty : this.RcTypesNegative.SelectedValue;
            var year = this.RmyDatesNegative.SelectedDate;

            this.FillNegativesRectifications(type, year, false);
        }

        protected void RcTypesPositive_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            var year = this.RmyDatesPositive.SelectedDate;

            this.FillPositivesRectifications(type, year, true);
        }

        protected void RmyDatesPositive_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypesPositive.SelectedValue) ? string.Empty : this.RcTypesPositive.SelectedValue;
            var year = e.NewDate;

            this.FillPositivesRectifications(type, year, true);
        }

        private void FillPositivesRectifications(string type, DateTime? year, bool manual)
        {
            var rectifications = new List<Dimatica.ContaPre.OL.Procedures.Rectification>();
            if (year != null)
            {
                var realYear = ((DateTime)year).Year;
                var rectificationsResult = this.rectificationsService.GetRectifications("+", type, realYear);
                foreach (var r in rectificationsResult)
                {
                    rectifications.Add(r);
                }
            }

            this.RgRectificationsPositives.DataSource = rectifications;
            if (manual)
            {
                this.RgRectificationsPositives.DataBind();
            }
        }

        protected void RgRectificationsPositives_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypesPositive.SelectedValue) ? string.Empty : this.RcTypesPositive.SelectedValue;
            var year = this.RmyDatesPositive.SelectedDate;

            this.FillPositivesRectifications(type, year, false);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showBtnReport", "showBtnReport();", true);
        }
    }
}