namespace Dimatica.ContaPre.Presentation.Views.Income
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

    public partial class UpdateApplications : BasePage
    {
        #region Fields

        IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        IBudgetApplicationsService budgetApplicationsService = DependencyFactory.GetInstance<IBudgetApplicationsService>();

        #endregion

        #region Public Properties

        public int Id
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

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var id = this.Request.QueryString["id"];
                var year = this.Request.QueryString["year"];
                var fileId = this.Request.QueryString["fileId"];
                var documentTitle = this.Request.QueryString["documentTitle"];

                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(fileId))
                {
                    this.Response.Redirect("~//Views//Income//Files.aspx");
                }
                else
                {
                    this.Id = Convert.ToInt32(id);
                    this.Year = Convert.ToInt32(year);
                    this.FileId = Convert.ToInt32(fileId);
                    this.lblTitle.InnerText = string.IsNullOrWhiteSpace(documentTitle) ? string.Empty : documentTitle.Replace(":", "+");
                }
            }
        }

        protected void RgUpdateApplications_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var applications = new List<PRE_DOCUMENTO_APLICACION>();

            if (this.Id != 0)
            {
                var applicationsResult = this.accountingDocumentsService.GetApplicationsByDocumentId(this.Id);

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
                DOC_CODIGO = this.Id,
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            try
            {
                var insert = this.accountingDocumentsService.InsertApplication(application);

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(0);", true);
                }
                else
                {
                    var drAmount = this.accountingRecordsService.GetDrAmount(this.FileId);
                    var miAmount = this.accountingRecordsService.GetMiAmount(this.FileId);

                    if (drAmount == miAmount)
                    {
                        var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, true, LoginUser.USU_CODIGO);
                    }
                    else
                    {
                        var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la aplicación en el documento.");
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(1);", true);
            }
        }

        protected void RgUpdateApplications_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("DOCA_CODIGO");
            var radDropApplication = (RadComboBox)item.FindControl("radDropApplication");
            var txtAmount = (RadNumericTextBox)item.FindControl("txtAmount");

            var application = new PRE_DOCUMENTO_APLICACION
            {
                DOCA_CODIGO = id,
                CACS_CODIGO = radDropApplication.SelectedValue,
                DOCA_IMPORTE = Convert.ToDecimal(txtAmount.Value),
                DOCA_ANO_PRESUPUESTO = Convert.ToInt16(this.Year),
                DOCA_I_G = "I",
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            try
            {
                var update = this.accountingDocumentsService.UpdateApplication(application);

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    switch (update.ResponseCode)
                    {
                        case ResponseCode.NotFound:
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(2);", true);
                            break;
                        case ResponseCode.Invalid:
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(3);", true);
                            break;
                    }
                }
                else
                {
                    var drAmount = this.accountingRecordsService.GetDrAmount(this.FileId);
                    var miAmount = this.accountingRecordsService.GetMiAmount(this.FileId);

                    if (drAmount == miAmount)
                    {
                        var updateR = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, true, LoginUser.USU_CODIGO);
                    }
                    else
                    {
                        var updateR = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la aplicación en el documento.");
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(4);", true);
            }
        }

        protected void RgUpdateApplications_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;

            var id = (int)item.GetDataKeyValue("DOCA_CODIGO");

            try
            {
                var delete = this.accountingDocumentsService.DeleteApplication(id, LoginUser.USU_CODIGO);

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.NotFound:
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(5);", true);
                            break;
                        case ResponseCode.Invalid:
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(6);", true);
                            break;
                    }
                }
                else
                {
                    var drAmount = this.accountingRecordsService.GetDrAmount(this.FileId);
                    var miAmount = this.accountingRecordsService.GetMiAmount(this.FileId);

                    if (drAmount == miAmount)
                    {
                        var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, true, LoginUser.USU_CODIGO);
                    }
                    else
                    {
                        var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando la aplicación en el documento.");
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(6);", true);
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

        protected void RgUpdateApplications_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCommand")
            {
                var item = e.Item as GridDataItem;
                var number = (string)item.GetDataKeyValue("CACS_NUMERO");

                var script = $"deleteApplication('{item.ItemIndex}', '{number}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "deleteApplication", script, true);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "moveNewButtons", "moveNewButtons();", true);
        }

        #endregion
    }
}