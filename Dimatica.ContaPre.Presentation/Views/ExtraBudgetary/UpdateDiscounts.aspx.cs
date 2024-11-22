namespace Dimatica.ContaPre.Presentation.Views.ExtraBudgetary
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class UpdateDiscounts : BasePage
    {
        #region Fields

        private IExtraBudgetaryApplicationsService extraBudgetaryApplicationsService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();

        private IExtraBudgetaryRecordsService extraBudgetaryRecordsService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        #endregion

        #region Public Properties

        public int Id
        {
            get
            {
                var o = this.Session["_extraBudgetaryId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_extraBudgetaryId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_extraBudgetaryId"] = value;
            }
        }

        public int ProviderId
        {
            get
            {
                var o = this.Session["_providerId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_providerId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_providerId"] = value;
            }
        }

        public decimal ExtraBudgetaryAmount
        {
            get
            {
                var o = this.Session["_extraBudgetaryAmount"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_extraBudgetaryAmount"] = o;
                }

                return Convert.ToDecimal(o);
            }

            set
            {
                this.Session["_extraBudgetaryAmount"] = value;
            }
        }

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

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var id = this.Request.QueryString["id"];

                if (string.IsNullOrWhiteSpace(id))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseWindows", $"CloseWindows(1);", true);
                }
                else
                {
                    this.Id = Convert.ToInt32(id);

                    var extraBudgetary = this.extraBudgetaryRecordsService.GetById(this.Id);

                    if (extraBudgetary == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseWindows", $"CloseWindows(1);", true);
                    }
                    else
                    {
                        // this.txtFileNumber.InnerText = extraBudgetary.EXP_EXTRAP_NUMERO.ToString();
                        // this.txtYear.InnerText = extraBudgetary.EXP_EXTRAP_ANO_PRESUPUESTO.ToString();
                        // this.txtDescription.InnerText = extraBudgetary.EXP_EXTRAP_TEXTO;
                        this.Year = Convert.ToInt32(extraBudgetary.EXP_EXTRAP_ANO_PRESUPUESTO);
                        this.ProviderId = extraBudgetary.PROV_CODIGO_PROVEEDOR ?? 0;
                        this.ExtraBudgetaryAmount = extraBudgetary.EXP_EXTRAP_IMPORTE ?? 0;
                    }
                }
            }
        }

        protected void RgUpdateDiscounts_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var discounts = new List<PRE_EXP_EXTRAPRE>();

            if (this.Id != 0)
            {
                var discountsResult = this.extraBudgetaryRecordsService.GetByBoundId(this.Id);

                foreach (var application in discountsResult)
                {
                    discounts.Add(application);
                }
            }

            this.RgUpdateDiscounts.DataSource = discounts;

            decimal amount = 0;

            foreach (var discount in discounts)
            {
                if (discount.EXP_EXTRAP_IMPORTE == null)
                {
                    continue;
                }

                amount += (decimal)discount.EXP_EXTRAP_IMPORTE;
            }

            this.txtTotal.Text = amount.ToString("N");
            this.txtTotalLiquid.Text = (this.ExtraBudgetaryAmount - amount).ToString("N");
        }

        protected void RgUpdateDiscounts_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridEditFormItem)e.Item;
            var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
            var dateOperation = (RadDatePicker)item.FindControl("dateOperation");
            var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");

            var extraBudgetary = new PRE_EXP_EXTRAPRE
                                         {
                                                 EXP_CODIGO = null,
                                                 EXTRAPRE_CODIGO = Convert.ToInt32(radDropApplication.SelectedValue),
                                                 EXP_EXTRAP_ANO_PRESUPUESTO = Convert.ToInt16(this.Year),
                                                 TIP_EXTRAP_CODIGO = 1,
                                                 DOC_CODIGO = null,
                                                 EXP_EXTRAP_FECHA = dateOperation.SelectedDate,
                                                 EXP_EXTRAP_IMPORTE = Convert.ToDecimal(txtAmount.Value),
                                                 PROV_CODIGO_PROVEEDOR = this.ProviderId == 0 ? (int?)null : this.ProviderId,
                                                 EXP_EXTRAP_NUMERO = 0,
                                                 EXP_ENLAZADO_TESORERIA = false,
                                                 EXP_NUM_EXP_CONTABLE_ANUAL = 0,
                                                 EXP_EXTRAP_CODIGO_ENLAZADO = this.Id,
                                                 USU_CODIGO = LoginUser.USU_CODIGO
                                         };

            try
            {
                var insert = this.extraBudgetaryRecordsService.InsertExtraBudgetary(extraBudgetary);

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(0);", true);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Expediente Extrapresupuestario.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(1);", true);
            }
        }

        protected void RgUpdateDiscounts_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridEditFormItem)e.Item;
            var extraBudgetaryId = (int)item.GetDataKeyValue("EXP_EXTRAP_CODIGO");
            var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
            var dateOperation = (RadDatePicker)item.FindControl("dateOperation");
            var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");

            var extraBudgetary = new PRE_EXP_EXTRAPRE
                                         {
                                                 EXP_EXTRAP_CODIGO = extraBudgetaryId,
                                                 EXTRAPRE_CODIGO = Convert.ToInt32(radDropApplication.SelectedValue),
                                                 EXP_EXTRAP_FECHA = dateOperation.SelectedDate,
                                                 EXP_EXTRAP_IMPORTE = Convert.ToDecimal(txtAmount.Value),
                                                 USU_CODIGO = LoginUser.USU_CODIGO
                                         };

            try
            {
                var update = this.extraBudgetaryRecordsService.UpdateExtraBudgetary(extraBudgetary);

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    switch (update.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(2);", true);

                            break;
                        case ResponseCode.NotFound:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(3);", true);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Expediente Extrapresupuestario.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(4);", true);
            }
        }

        protected void RgUpdateDiscounts_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            var extraBudgetaryId = (int)item.GetDataKeyValue("EXP_EXTRAP_CODIGO");

            try
            {
                var delete = this.extraBudgetaryRecordsService.DeleteExtraBudgetary(extraBudgetaryId, LoginUser.USU_CODIGO);

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(5);", true);

                            break;
                        case ResponseCode.NotFound:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(6);", true);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Expediente Extrapresupuestario.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(5);", true);
            }
        }

        protected void RgUpdateDiscounts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;
                var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
                var dateOperation = (RadDatePicker)item.FindControl("dateOperation");

                var applications = this.extraBudgetaryApplicationsService.GetExtraBudgetaryApplications();

                radDropApplication.DataSource = applications.ToList();
                radDropApplication.DataBind();

                dateOperation.SelectedDate = DateTime.Now;

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgUpdateDiscounts.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var applicationId = item.GetDataKeyValue("EXTRAPRE_CODIGO");
                    radDropApplication.SelectedValue = applicationId.ToString();

                    var date = item.GetDataKeyValue("EXP_EXTRAP_FECHA");

                    if (date != null)
                    {
                        dateOperation.SelectedDate = (DateTime)date;
                    }

                    var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");
                    var amount = item.GetDataKeyValue("EXP_EXTRAP_IMPORTE");

                    if (amount != null)
                    {
                        txtAmount.Value = Convert.ToDouble(amount);
                    }
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "moveNewButtons", "moveNewButtons();", true);
        }

        #endregion
    }
}