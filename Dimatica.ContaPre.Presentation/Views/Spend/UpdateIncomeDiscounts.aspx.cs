namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class UpdateIncomeDiscounts : BasePage
    {
        #region Static Fields and Constants

        private static IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        #endregion

        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        IBudgetApplicationsService budgetApplicationsService = DependencyFactory.GetInstance<IBudgetApplicationsService>();

        private ICostPlacesService costPlacesService = DependencyFactory.GetInstance<ICostPlacesService>();

        private IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

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

        public DateTime ProposalDate
        {
            get
            {
                var o = this.Session["_proposalDate"];

                if (o == null)
                {
                    o = DateTime.Now;
                    this.Session["_proposalDate"] = o;
                }

                return Convert.ToDateTime(o);
            }

            set
            {
                this.Session["_proposalDate"] = value;
            }
        }

        #endregion

        #region Private Properties

        private PRE_EXPEDIENTE_CONTABLE AccountingRecord
        {
            get
            {
                var file = this.Session["_accountingRecordDiscounts"] as PRE_EXPEDIENTE_CONTABLE;

                if (file == null)
                {
                    file = new PRE_EXPEDIENTE_CONTABLE();

                    this.Session["_accountingRecordDiscounts"] = file;
                }

                return file;
            }

            set
            {
                this.Session["_accountingRecordDiscounts"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteAccountingRecord()
        {
            try
            {
                var id = ((PRE_EXPEDIENTE_CONTABLE)HttpContext.Current.Session["_accountingRecordDiscounts"]).EXP_CODIGO;

                if (id == 0)
                {
                    return 3;
                }

                var delete = accountingRecordsService.DeleteAccountingRecord(id, LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        return 0;
                    case ResponseCode.Ok:
                        return 1;
                    case ResponseCode.NotFound:
                        return 2;
                }

                return 0;
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Expediente Contable.");
                return 0;
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
                var year = this.Request.QueryString["year"];
                var fileId = this.Request.QueryString["fileId"];
                var proposal = this.Request.QueryString["proposal"];

                if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(fileId) || string.IsNullOrWhiteSpace(proposal))
                {
                    this.Response.Redirect("~//Views//Spend//SpendRecords.aspx");
                }
                else
                {
                    this.ParentId = Convert.ToInt32(documentId);
                    this.Year = Convert.ToInt32(year);
                    this.FileId = Convert.ToInt32(fileId);
                    this.ProposalDate = Convert.ToDateTime(proposal, new CultureInfo("es-ES"));

                    var areas = this.costPlacesService.GetCostPlacesOriginToCombo();
                    this.RcAreas.DataSource = areas;
                    this.RcAreas.DataBind();

                    var provenances = this.provenancesService.GetProvenances("I");
                    this.RcProvenances.DataSource = provenances;
                    this.RcProvenances.DataBind();

                    var providers = this.providersService.GetProvidersToCombo();
                    this.RcProviders.DataSource = providers;
                    this.RcProviders.DataBind();

                    this.UpdateInfo();
                }
            }
        }

        private void UpdateInfo()
        {
            var incomeDiscounts = this.accountingDocumentsService.GetIncomeDiscountsCount(this.ParentId);

            if (incomeDiscounts != 0)
            {
                var accountingRecord = this.accountingDocumentsService.GetAccountingRecord(this.ParentId);

                if (accountingRecord == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseWindows", $"CloseWindows(0);", true);
                }
                else
                {
                    this.AccountingRecord = accountingRecord;

                    var accountingRecords = this.accountingDocumentsService.GetByIdAndType("I", this.AccountingRecord.EXP_CODIGO);

                    if (accountingRecords.Any())
                    {
                        this.AccountingRecord.DOC_CODIGO = accountingRecords.FirstOrDefault().DOC_CODIGO;
                    }

                    switch ((int)this.AccountingRecord.TIPD_CLAVE)
                    {
                        case 10:
                            this.RrbLine.SelectedValue = "42";

                            break;
                        case 13:
                            this.RrbLine.SelectedValue = "41";

                            break;
                    }

                    this.RmyBudgetYear.SelectedDate = new DateTime((int)this.AccountingRecord.EXP_ANO_PRESUPUESTO, 1, 1);

                    this.RcAreas.SelectedValue = this.AccountingRecord.CEN_CODIGO.ToString();
                    this.RcProvenances.SelectedValue = this.AccountingRecord.PROC_CODIGO.ToString();
                    this.RcProviders.SelectedValue = this.AccountingRecord.PROV_CODIGO.ToString();
                }
            }
            else
            {
                this.AccountingRecord = null;
                this.RmyBudgetYear.SelectedDate = this.ProposalDate;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideApplications", "hideApplications();", true);
            }
        }

        protected void RgUpdateApplications_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var applications = new List<PRE_DOCUMENTO_APLICACION>();

            if (this.AccountingRecord.DOC_CODIGO != 0)
            {
                var applicationsResult = this.accountingDocumentsService.GetApplicationsByDocumentId(this.AccountingRecord.DOC_CODIGO);

                foreach (var application in applicationsResult)
                {
                    applications.Add(application);
                }
            }

            this.RgUpdateApplications.DataSource = applications;

            var total = applications.Sum(a => a.DOCA_IMPORTE);
            this.txtTotal.Text = ((decimal)total).ToString("N");
        }

        protected void RgUpdateApplications_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridEditFormItem)e.Item;
            var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
            var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");

            var application = new PRE_DOCUMENTO_APLICACION
                                      {
                                              CACS_CODIGO = radDropApplication.SelectedValue,
                                              DOCA_IMPORTE = Convert.ToDecimal(txtAmount.Value),
                                              DOCA_ANO_PRESUPUESTO = Convert.ToInt16(this.Year),
                                              DOCA_I_G = "I",
                                              DOC_CODIGO = this.AccountingRecord.DOC_CODIGO,
                                              USU_CODIGO = LoginUser.USU_CODIGO
                                      };

            try
            {
                var insert = this.accountingDocumentsService.InsertApplication(application);

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(7);", true);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Expediente Contable.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(6);", true);
            }
        }

        protected void RgUpdateApplications_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridEditFormItem)e.Item;
            var applicationId = (int)item.GetDataKeyValue("DOCA_CODIGO");
            var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
            var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");

            var application = new PRE_DOCUMENTO_APLICACION
                                      {
                                              DOCA_CODIGO = applicationId,
                                              CACS_CODIGO = radDropApplication.SelectedValue,
                                              DOCA_IMPORTE = Convert.ToDecimal(txtAmount.Value),
                                              DOCA_ANO_PRESUPUESTO = Convert.ToInt16(this.Year),
                                              DOCA_I_G = "I",
                                              DOC_CODIGO = this.AccountingRecord.DOC_CODIGO,
                                              USU_CODIGO = LoginUser.USU_CODIGO
                                      };

            try
            {
                var update = this.accountingDocumentsService.UpdateApplication(application);

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    switch (update.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(9);", true);

                            break;
                        case ResponseCode.NotFound:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(10);", true);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Expediente Contable.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(8);", true);
            }
        }

        protected void RgUpdateApplications_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            var applicationId = (int)item.GetDataKeyValue("DOCA_CODIGO");

            try
            {
                var delete = this.accountingDocumentsService.DeleteApplication(applicationId, LoginUser.USU_CODIGO);

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.NotFound:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(12);", true);

                            break;
                        case ResponseCode.Invalid:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(11);", true);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Expediente Contable.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(11);", true);
            }
        }

        protected void RgUpdateApplications_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;
                var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");

                var applications = this.budgetApplicationsService.GetNumbersByTypeByYear("I", this.Year);

                radDropApplication.DataSource = applications.ToList();
                radDropApplication.DataBind();

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgUpdateApplications.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var applicationId = item.GetDataKeyValue("CACS_CODIGO");
                    radDropApplication.SelectedValue = applicationId.ToString();

                    var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");
                    var amount = item.GetDataKeyValue("DOCA_IMPORTE");

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

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (this.AccountingRecord.EXP_CODIGO == 0)
            {
                var accountingFile = new PRE_EXPEDIENTE_CONTABLE
                                             {
                                                     EXP_I_G = "I",
                                                     EXP_DESCRIPCION = $"DTO. ING.(EXP: {this.FileId}/{this.Year})",
                                                     EXP_PLURIANUAL = false,
                                                     EXP_CUADRADO = false,
                                                     EXP_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyBudgetYear.SelectedDate).Year),
                                                     PROV_CODIGO = Convert.ToInt32(this.RcProviders.SelectedValue),
                                                     PROC_CODIGO = Convert.ToInt32(this.RcProvenances.SelectedValue),
                                                     CEN_CODIGO = Convert.ToInt32(this.RcAreas.SelectedValue),
                                                     CUE_CODIGO = 10,
                                                     DOC_CODIGO_G_DESCUENTO_I = this.ParentId,
                                                     USU_CODIGO = LoginUser.USU_CODIGO
                                             };

                try
                {
                    var insert = accountingRecordsService.InsertAccountingRecord(accountingFile);

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(0);", true);
                    }
                    else
                    {
                        var accountingFileId = (int)insert.ResponseMethod;

                        var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                                                         {
                                                                 DOC_I_G = "I",
                                                                 DOC_FECHA_PROPUESTA = this.ProposalDate.AddDays(1),
                                                                 DOC_FECHA_ASIENTO_DIARIO = this.ProposalDate.AddDays(2),
                                                                 DOC_DESCRIPCION = this.RcProviders.SelectedItem.Text,
                                                                 DOC_NUMERO_MOVIMIENTO_I = 0,
                                                                 DOC_FECHA_MOVIMIENTO_I = this.ProposalDate,
                                                                 EXP_CODIGO = accountingFileId,
                                                                 TIPD_CODIGO = Convert.ToInt32(this.RrbLine.SelectedValue),
                                                                 CUE_CODIGO = 10,
                                                                 DOC_ENLAZADO_TESORERIA = false,
                                                                 DOC_CODIGO_G_DESCUENTO_I = this.ParentId,
                                                                 USU_CODIGO = LoginUser.USU_CODIGO
                                                         };

                        var insertDocument = this.accountingDocumentsService.InsertAccountingDocument(accountingDocument);

                        if (insertDocument.ResponseCode != ResponseCode.Ok)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(2);", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error insertando el Expediente Contable.");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(1);", true);
                }
            }
            else
            {
                var accountingFile = new PRE_EXPEDIENTE_CONTABLE
                                             {
                                                     EXP_CODIGO = this.AccountingRecord.EXP_CODIGO,
                                                     EXP_I_G = "I",
                                                     EXP_DESCRIPCION = $"DTO. ING.(EXP: {this.FileId}/{this.Year})",
                                                     EXP_PLURIANUAL = false,
                                                     EXP_CUADRADO = false,
                                                     EXP_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyBudgetYear.SelectedDate).Year),
                                                     PROV_CODIGO = Convert.ToInt32(this.RcProviders.SelectedValue),
                                                     PROC_CODIGO = Convert.ToInt32(this.RcProvenances.SelectedValue),
                                                     CEN_CODIGO = Convert.ToInt32(this.RcAreas.SelectedValue),
                                                     CUE_CODIGO = 10,
                                                     DOC_CODIGO_G_DESCUENTO_I = this.ParentId,
                                                     USU_CODIGO = LoginUser.USU_CODIGO
                                             };

                try
                {
                    var update = accountingRecordsService.UpdateAccountingRecord(accountingFile);

                    if (update.ResponseCode != ResponseCode.Ok)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(3);", true);
                    }
                    else
                    {
                        var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                                                         {
                                                                 DOC_CODIGO = this.AccountingRecord.DOC_CODIGO,
                                                                 DOC_I_G = "I",
                                                                 DOC_FECHA_PROPUESTA = this.ProposalDate.AddDays(1),
                                                                 DOC_FECHA_ASIENTO_DIARIO = this.ProposalDate.AddDays(2),
                                                                 DOC_DESCRIPCION = this.RcProviders.SelectedItem.Text,
                                                                 DOC_NUMERO_MOVIMIENTO_I = 0,
                                                                 DOC_FECHA_MOVIMIENTO_I = this.ProposalDate,
                                                                 EXP_CODIGO = this.AccountingRecord.EXP_CODIGO,
                                                                 TIPD_CODIGO = Convert.ToInt32(this.RrbLine.SelectedValue),
                                                                 CUE_CODIGO = 10,
                                                                 DOC_ENLAZADO_TESORERIA = false,
                                                                 DOC_CODIGO_G_DESCUENTO_I = this.ParentId,
                                                                 USU_CODIGO = LoginUser.USU_CODIGO
                                                         };

                        var updateDocument = this.accountingDocumentsService.InsertAccountingDocument(accountingDocument);

                        if (updateDocument.ResponseCode != ResponseCode.Ok)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(5);", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error modificando el Expediente Contable.");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(4);", true);
                }
            }

            this.UpdateInfo();
        }

        #endregion
    }
}