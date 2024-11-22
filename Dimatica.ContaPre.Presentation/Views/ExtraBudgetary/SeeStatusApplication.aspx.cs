namespace Dimatica.ContaPre.Presentation.Views.ExtraBudgetary
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeeStatusApplication : BasePage
    {
        #region Fields

        private IExtraBudgetaryApplicationsService extraBudgetaryApplicationsService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();

        #endregion

        #region Public Properties

        public int Year
        {
            get
            {
                var o = this.Session["_year"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_year"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_year"] = value;
            }
        }

        public int ExtraBudgetaryApplication
        {
            get
            {
                var o = this.Session["_extraBudgetaryApplication"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_extraBudgetaryApplication"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_extraBudgetaryApplication"] = value;
            }
        }

        public string ExtraBudgetaryApplicationText
        {
            get
            {
                var o = this.Session["_extraBudgetaryApplicationText"];

                if (o == null)
                {
                    o = string.Empty;
                    this.Session["_extraBudgetaryApplicationText"] = o;
                }

                return o.ToString();
            }

            set
            {
                this.Session["_extraBudgetaryApplicationText"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var year = this.Request.QueryString["year"];
                var application = this.Request.QueryString["application"];
                var applicationText = this.Request.QueryString["applicationText"];

                if (string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(applicationText))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseWindows", $"CloseWindows(1);", true);
                }
                else
                {
                    this.Year = Convert.ToInt32(year);
                    this.ExtraBudgetaryApplication = Convert.ToInt32(application);
                    this.ExtraBudgetaryApplicationText = applicationText;
                }
            }
        }

        protected void RgResume_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var resume = new List<StatusRecord>();

            if (this.ExtraBudgetaryApplication != 0 && this.Year != 0)
            {
                var status = this.extraBudgetaryApplicationsService.GetStatus(this.ExtraBudgetaryApplication, this.Year);

                var income = status.FirstOrDefault(s => s.EXTRAPRE_DESCRIPCION.Equals("ingresos"));
                decimal incomeAmount;

                if (income == null)
                {
                    incomeAmount = 0;
                }
                else
                {
                    incomeAmount = (decimal)income.EXP_EXTRAP_IMPORTE;
                }

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "Ingresos:",
                                           Accumulated = incomeAmount
                                   });

                var spend = status.FirstOrDefault(s => s.EXTRAPRE_DESCRIPCION.Equals("gastos"));
                decimal spendAmount;

                if (spend == null)
                {
                    spendAmount = 0;
                }
                else
                {
                    spendAmount = (decimal)spend.EXP_EXTRAP_IMPORTE;
                }

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "Gatos:",
                                           Accumulated = spendAmount
                                   });

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "SALDO:",
                                           Accumulated = incomeAmount - spendAmount
                                   });
            }

            this.RgResume.DataSource = resume;
        }

        protected void RgResume_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["Column"].ForeColor = Color.White;
                dataBoundItem["Column"].BackColor = ColorTranslator.FromHtml("#3d3e47");
            }
        }

        protected void RgResume_OnPreRender(object sender, EventArgs e)
        {
            var headerItem = this.RgResume.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

            headerItem["AccumulatedLabel"].Text = this.ExtraBudgetaryApplicationText;
        }

        #endregion
    }
}