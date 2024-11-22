namespace Dimatica.ContaPre.Presentation.Views.Spend
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

    public partial class UpdateExtraBudgetaryDiscounts : BasePage
    {
        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IExtraBudgetaryApplicationsService extraBudgetaryApplicationsService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();

        private IExtraBudgetaryRecordsService extraBudgetaryRecordsService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        #endregion

        #region Public Properties

        public int ParentId
        {
            get
            {
                var o = this.Session["_documentId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_documentId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_documentId"] = value;
            }
        }

        public int FileId
        {
            get
            {
                var o = this.Session["_fileId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_fileId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_fileId"] = value;
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

        public int AnnualFileId
        {
            get
            {
                var o = this.Session["_annualFileId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_annualFileId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_annualFileId"] = value;
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
                var documentId = this.Request.QueryString["documentId"];
                var fileId = this.Request.QueryString["fileId"];
                var year = this.Request.QueryString["year"];
                var annualFileId = this.Request.QueryString["annualFileId"];

                if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(fileId) || string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(annualFileId))
                {
                    this.Response.Redirect("~//Views//Spend//SpendRecords.aspx");
                }
                else
                {
                    this.ParentId = Convert.ToInt32(documentId);
                    this.FileId = Convert.ToInt32(fileId);
                    this.Year = Convert.ToInt32(year);
                    this.AnnualFileId = Convert.ToInt32(annualFileId);
                    this.ProviderId = this.accountingDocumentsService.GetProviderDc(this.ParentId);
                }
            }
        }

        protected void RgExtraBudgetary_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var extraBudgetary = new List<PRE_EXP_EXTRAPRE>();

            if (this.ParentId != 0)
            {
                var extraBudgetaryResult = this.extraBudgetaryRecordsService.GetByDocumentId(this.ParentId, 1);

                foreach (var application in extraBudgetaryResult)
                {
                    extraBudgetary.Add(application);
                }
            }

            this.RgExtraBudgetary.DataSource = extraBudgetary;

            var total = extraBudgetary.Sum(a => a.EXP_EXTRAP_IMPORTE);
            this.txtTotal.Text = ((decimal)total).ToString("N");
        }

        protected void RgExtraBudgetary_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridEditFormItem)e.Item;
            var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
            var dateOperation = (RadDatePicker)item.FindControl("dateOperation");
            var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");

            var extraBudgetary = new PRE_EXP_EXTRAPRE
            {
                EXP_CODIGO = this.FileId,
                EXTRAPRE_CODIGO = Convert.ToInt32(radDropApplication.SelectedValue),
                EXP_EXTRAP_ANO_PRESUPUESTO = Convert.ToInt16(this.Year),
                TIP_EXTRAP_CODIGO = 1,
                DOC_CODIGO = this.ParentId,
                EXP_EXTRAP_FECHA = dateOperation.SelectedDate,
                EXP_EXTRAP_IMPORTE = Convert.ToDecimal(txtAmount.Value),
                PROV_CODIGO_PROVEEDOR = this.ProviderId == 0 ? (int?)null : this.ProviderId,
                EXP_EXTRAP_NUMERO = 0,
                EXP_ENLAZADO_TESORERIA = false,
                EXP_NUM_EXP_CONTABLE_ANUAL = this.AnnualFileId == -1 ? (int?)null : this.AnnualFileId,
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
                LogError(ex, "Error eliminando el Expediente Extrapresupuestario.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(1);", true);
            }
        }

        protected void RgExtraBudgetary_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridEditFormItem)e.Item;
            var extraBudgetaryId = (int)item.GetDataKeyValue("EXP_EXTRAP_CODIGO");
            var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
            var dateOperation = (RadDatePicker)item.FindControl("dateOperation");
            var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");

            var extraBudgetary = new PRE_EXP_EXTRAPRE
            {
                EXP_EXTRAP_CODIGO = extraBudgetaryId,
                EXP_CODIGO = this.FileId,
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

        protected void RgExtraBudgetary_OnDeleteCommand(object sender, GridCommandEventArgs e)
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

        protected void RgExtraBudgetary_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;
                var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");

                var applications = this.extraBudgetaryApplicationsService.GetExtraBudgetaryApplicationsToCombo();

                radDropApplication.DataSource = applications.ToList();
                radDropApplication.DataBind();

                var dateOperation = (RadDatePicker)item.FindControl("dateOperation");
                dateOperation.SelectedDate = DateTime.Now;

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgExtraBudgetary.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var applicationId = item.GetDataKeyValue("EXTRAPRE_CODIGO");
                    radDropApplication.SelectedValue = applicationId.ToString();

                    var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");
                    var amount = item.GetDataKeyValue("EXP_EXTRAP_IMPORTE");

                    if (amount != null)
                    {
                        txtAmount.Value = Convert.ToDouble(amount);
                    }

                    var date = item.GetDataKeyValue("EXP_EXTRAP_FECHA");

                    if (date != null)
                    {
                        dateOperation.SelectedDate = Convert.ToDateTime(date);
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