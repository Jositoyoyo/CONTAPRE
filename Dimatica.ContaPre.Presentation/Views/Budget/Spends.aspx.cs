namespace Dimatica.ContaPre.Presentation.Views.Budget
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web.Services;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class Spends : BasePage
    {
        #region Static Fields and Constants

        private static IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        #endregion

        #region Fields

        IApplicationService applicationService = DependencyFactory.GetInstance<IApplicationService>();

        #endregion

        #region Public Properties

        public bool IsClose
        {
            get
            {
                var p = (bool)this.Session["_isClose"];

                return p;
            }

            set
            {
                this.Session["_isClose"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteBudget(string yearValue)
        {
            try
            {
                var year = Convert.ToInt32(yearValue);

                var delete = budgetsService.DeleteBudget(year, "G", LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Ok:
                        return 1;
                    case ResponseCode.NotFound:
                        return 2;
                    case ResponseCode.Found:
                        return 0;
                    case ResponseCode.IsClose:
                        return 3;
                    case ResponseCode.Invalid:
                        return 4;
                }

                return 0;
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el presupuesto de gastos.");
                return 0;
            }
        }

        [WebMethod]
        public static int CloseBudget(string yearValue)
        {
            try
            {
                var year = Convert.ToInt32(yearValue);

                var delete = budgetsService.CloseBudget(year, "G", LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Ok:
                        return 1;
                    case ResponseCode.NotFound:
                        return 2;
                    case ResponseCode.Invalid:
                        return 3;
                }

                return 0;
            }
            catch (Exception ex)
            {
                LogError(ex, "Error cerrando el presupuesto de gastos.");
                return 0;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "Spends";

                var years = budgetsService.GetYearsByType("G");

                this.RcYears.DataSource = years;
                this.RcYears.DataBind();
            }
        }

        protected void RgSpends_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var year = string.IsNullOrWhiteSpace(this.RcYears.SelectedValue) ? string.Empty : this.RcYears.SelectedValue;
            this.FillBudgets(year, false);
        }

        protected void RgSpends_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                var level = dataBoundItem.GetDataKeyValue("Level").ToString();

                switch (level)
                {
                    case "CAP":
                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#e8e8e8");

                        break;
                    case "ART":
                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#f5f5f5");

                        break;
                    case "CON":
                        break;
                    case "SUB":
                        break;
                }
            }
        }

        protected void RgSpends_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgSpends.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgSpendsClose_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var year = string.IsNullOrWhiteSpace(this.RcYears.SelectedValue) ? string.Empty : this.RcYears.SelectedValue;
            this.FillBudgets(year, false);
        }

        protected void RgSpendsClose_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                var level = dataBoundItem.GetDataKeyValue("Level").ToString();

                switch (level)
                {
                    case "CAP":
                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#e8e8e8");

                        break;
                    case "ART":
                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#f5f5f5");

                        break;
                    case "CON":
                        break;
                    case "SUB":
                        break;
                }

                var description = dataBoundItem.GetDataKeyValue("Description").ToString();
                dataBoundItem["DescriptionColumn"].ToolTip = description;
            }
        }

        protected void RgSpendsClose_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgSpends.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RcYears_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var year = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            this.FillBudgets(year, true);
        }

        private void FillBudgets(string year, bool manual)
        {
            var budgets = new List<Budget>();
            this.RgSpends.DataSource = budgets;
            this.RgSpendsClose.DataSource = budgets;

            if (manual)
            {
                this.RgSpends.DataBind();
                this.RgSpendsClose.DataBind();
            }

            decimal total = 0;
            decimal updateTotal = 0;
            decimal finalTotal = 0;
            this.IsClose = false;

            if (!string.IsNullOrEmpty(year))
            {
                var realYear = Convert.ToInt32(year);
                var budgetsResult = budgetsService.GetBudgetsByType(realYear, "G");

                foreach (var budget in budgetsResult)
                {
                    this.IsClose = budget.IsClose;
                    budgets.Add(budget);

                    if (budget.Level.Equals("CAP"))
                    {
                        total += budget.Amount;
                        updateTotal += budget.UpdateAmount;
                        finalTotal += budget.FinalAmount;
                    }
                }
            }

            if (this.IsClose)
            {
                this.RgSpendsClose.DataSource = budgets;

                if (manual)
                {
                    this.RgSpendsClose.DataBind();
                }
            }
            else
            {
                this.RgSpends.DataSource = budgets;

                if (manual)
                {
                    this.RgSpends.DataBind();
                }
            }

            this.txtTotal.Text = finalTotal.ToString("N");
            this.txtCurrentTotal.Text = total.ToString("N");
            this.txtUpdateTotal.Text = updateTotal.ToString("N");
            this.txtFinalTotal.Text = finalTotal.ToString("N");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideShowGrids", $"hideShowGrids('{this.IsClose}');", true);
        }

        #endregion
    }
}