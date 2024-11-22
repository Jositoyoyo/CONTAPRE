namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class ManageSpendRecord : BasePage
    {
        #region Static Fields and Constants

        private static IBillPurchasesService billPurchasesService = DependencyFactory.GetInstance<IBillPurchasesService>();

        private static IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        #endregion

        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        private IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        private ICostPlacesService costPlacesService = DependencyFactory.GetInstance<ICostPlacesService>();

        private IDocumentTypesService documentTypesService = DependencyFactory.GetInstance<IDocumentTypesService>();

        private IPayFormsService payFormsService = DependencyFactory.GetInstance<IPayFormsService>();

        private IPayTypesService payTypesService = DependencyFactory.GetInstance<IPayTypesService>();

        private IProgramsService programsService = DependencyFactory.GetInstance<IProgramsService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Properties

        private PRE_EXPEDIENTE_CONTABLE AccountingRecord
        {
            get
            {
                var accounting = this.Session["_accountingRecord"] as PRE_EXPEDIENTE_CONTABLE;

                if (accounting == null)
                {
                    accounting = new PRE_EXPEDIENTE_CONTABLE();

                    this.Session["_accountingRecord"] = accounting;
                }

                return accounting;
            }

            set
            {
                this.Session["_accountingRecord"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteSpendRecord()
        {
            try
            {
                var id = ((PRE_EXPEDIENTE_CONTABLE)HttpContext.Current.Session["_accountingRecord"]).EXP_CODIGO;

                if (id == 0)
                {
                    return 3;
                }

                var administrativeCode = ((PRE_EXPEDIENTE_CONTABLE)HttpContext.Current.Session["_accountingRecord"]).EA_CODIGO;

                // var accountingRecords = billPurchasesService.GetByAccountingRecord(id);

                // foreach (var accountingRecord in accountingRecords)
                // {
                // // TODO: aqui se llamaba a oracle
                // //expediente.actualizaImportadaContabl("N", dt.Rows(i).Item("codFacturaGEI"), bOK)
                // }

                // if (se elimina de Oracle)
                // {
                var deleteBills = billPurchasesService.DeleteByAccountingRecord(id);
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

                // }
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
            var spendRecordId = this.Request.QueryString["id"];

            if (spendRecordId == null)
            {
                this.Response.Redirect("~/Views/Spend/SpendRecords.aspx");

                return;
            }

            if (!this.IsPostBack)
            {
                this.AccountingRecord = null;

                var accountingRecord = accountingRecordsService.GetById(Convert.ToInt32(spendRecordId));

                if (accountingRecord == null)
                {
                    this.Response.Redirect("~/Views/Spend/SpendRecords.aspx");

                    return;
                }

                var areas = this.costPlacesService.GetCostPlacesOriginToCombo();
                areas.Insert(
                             0,
                             new PRE_CENTRO_COSTE
                             {
                                 CEN_CODIGO = -1
                             });
                this.RcAreas.DataSource = areas;
                this.RcAreas.DataBind();

                var providers = this.providersService.GetProvidersToCombo();
                providers.Insert(
                                 0,
                                 new PRE_PROVEEDOR
                                 {
                                     PROV_CODIGO = -1,
                                     PROV_NOMBRE = "< Seleccione >"
                                 });
                this.RcProviders.DataSource = providers;
                this.RcProviders.DataBind();

                var restrictedAccount = this.accountRestrictedService.GetAccountsRestricted("G");
                this.RcRestrictedAccount.DataSource = restrictedAccount;
                this.RcRestrictedAccount.DataBind();

                var programs = this.programsService.GetPrograms();
                programs.Insert(
                                0,
                                new PRE_PROGRAMA
                                {
                                    PRO_CODIGO_AUX = -1,
                                    PRO_NUMERO = "< Seleccione >"
                                });
                this.RcPrograms.DataSource = programs;
                this.RcPrograms.DataBind();

                this.AccountingRecord = accountingRecord;

                this.TxtExerciseYear.InnerText = this.AccountingRecord.EA_ANO_EJERCICIO == null ? string.Empty : this.AccountingRecord.EA_ANO_EJERCICIO.ToString();
                this.TxtRecordNumber.InnerText = this.AccountingRecord.EA_NUMERO == null ? string.Empty : this.AccountingRecord.EA_NUMERO.ToString();
                this.TxtProvenance.InnerText = this.AccountingRecord.PROC_DESCRIPCION;
                this.TxtBudgetYear.InnerText = this.AccountingRecord.EXP_ANO_PRESUPUESTO == null ? string.Empty : this.AccountingRecord.EXP_ANO_PRESUPUESTO.ToString();
                this.TxtAccountingNumber.InnerText = this.AccountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : this.AccountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
                this.TxtDescription.InnerText = this.AccountingRecord.EXP_DESCRIPCION;

                if (this.AccountingRecord.CEN_CODIGO != null)
                {
                    this.RcAreas.SelectedValue = this.AccountingRecord.CEN_CODIGO.ToString();
                }

                if (this.AccountingRecord.PROV_CODIGO != null)
                {
                    this.RcProviders.SelectedValue = this.AccountingRecord.PROV_CODIGO.ToString();
                }

                this.btnProviders.Enabled = this.RcProviders.SelectedValue.Equals("112");

                this.RcRestrictedAccount.SelectedValue = this.AccountingRecord.CUE_CODIGO == null ? "23" : this.AccountingRecord.CUE_CODIGO.ToString();

                // this.TxtOrgClassification.Text = string.Empty;
                if (this.AccountingRecord.PRO_CODIGO != null)
                {
                    this.RcPrograms.SelectedValue = this.AccountingRecord.PRO_CODIGO.ToString();
                }

                this.RcbSquare.Checked = this.AccountingRecord.EXP_CUADRADO;
                this.RcbMultiYear.Checked = this.AccountingRecord.EXP_PLURIANUAL;

                if ((bool)!this.AccountingRecord.EXP_PLURIANUAL)
                {
                    this.RcbSquare.Enabled = false;
                    this.RcbMultiYear.Enabled = false;
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var program = this.RcPrograms.SelectedValue;
                var programId = program == "-1" ? this.budgetsService.GetProgramCodeByYear((int)this.AccountingRecord.EXP_ANO_PRESUPUESTO) : Convert.ToInt32(program);

                var accountingRecord = new PRE_EXPEDIENTE_CONTABLE
                {
                    EXP_CODIGO = this.AccountingRecord.EXP_CODIGO,
                    EXP_I_G = "G",
                    EA_ANO_EJERCICIO = this.AccountingRecord.EA_ANO_EJERCICIO,
                    EA_NUMERO = this.AccountingRecord.EA_NUMERO,
                    EXP_DESCRIPCION = this.AccountingRecord.EXP_DESCRIPCION,
                    EXP_ANO_PRESUPUESTO = this.AccountingRecord.EXP_ANO_PRESUPUESTO,
                    EXP_NUM_EXP_CONTABLE_ANUAL = this.AccountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL,
                    PROC_CODIGO = this.AccountingRecord.PROC_CODIGO,
                    EXP_CUADRADO = (bool)this.RcbSquare.Checked,
                    EXP_PLURIANUAL = (bool)this.RcbMultiYear.Checked,
                    CUE_CODIGO = Convert.ToInt32(this.RcRestrictedAccount.SelectedValue),
                    CEN_CODIGO = this.RcAreas.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.RcAreas.SelectedValue),
                    PROV_CODIGO = this.RcProviders.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.RcProviders.SelectedValue),
                    PRO_CODIGO = Convert.ToByte(programId),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var update = accountingRecordsService.UpdateAccountingRecord(accountingRecord);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Expediente Contable.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Expediente Contable en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Expediente Contable", strBuilder, MessageType.Warning);
                }
                else
                {
                    this.Response.Redirect($"~/Views/Spend/ManageSpendRecord.aspx?id={this.AccountingRecord.EXP_CODIGO}");
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Expediente Contable.");
                strBuilder.Append("Ha ocurrido un error modificando el Expediente Contable en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Expediente Contable", strBuilder, MessageType.Deny);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "moveNewButtons", "moveNewButtons();", true);

            if ((bool)this.RcbMultiYear.Checked)
            {
                this.btnSee.Enabled = true;
            }
            else
            {
                this.btnSee.Enabled = false;
            }
        }

        protected void RgDocuments_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var accountingDocuments = new List<PRE_DOCUMENTO_CONTABLE>();

            if (this.AccountingRecord != null && this.AccountingRecord.EXP_CODIGO != 0)
            {
                accountingDocuments = this.accountingDocumentsService.GetByIdAndType("G", this.AccountingRecord.EXP_CODIGO);
            }

            this.RgDocuments.DataSource = accountingDocuments;
        }

        protected void RgDocuments_OnPreRender(object sender, EventArgs e) { }

        protected void RgDocuments_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radTypes = (RadComboBox)item.FindControl("radDropDocumentType");

                var documentTypes = this.documentTypesService.GetDocumentTypesToCombo("G");
                radTypes.DataSource = documentTypes;
                radTypes.DataBind();

                var radAccounts = (RadComboBox)item.FindControl("radDropRestrictedAccount");

                var restrictedAccount = this.accountRestrictedService.GetAccountsRestricted("G");
                radAccounts.DataSource = restrictedAccount;
                radAccounts.DataBind();

                radAccounts.SelectedValue = "23";

                var radPayType = (RadComboBox)item.FindControl("radDropPayType");

                var payTypes = this.payTypesService.GetPayTypes();
                payTypes.Insert(
                                0,
                                new PRE_TIPO_PAGO
                                {
                                    TIPP_CODIGO_AUX = -1
                                });
                radPayType.DataSource = payTypes;
                radPayType.DataBind();

                var radPayForm = (RadComboBox)item.FindControl("radDropPayForm");

                var payForms = this.payFormsService.GetPayForms();
                payForms.Insert(
                                0,
                                new PRE_FORMA_PAGO
                                {
                                    FOR_CODIGO_AUX = -1
                                });
                radPayForm.DataSource = payForms;
                radPayForm.DataBind();

                var rdProposal = (RadDatePicker)item.FindControl("rdProposal");
                var rdEffective = (RadDatePicker)item.FindControl("rdEffective");

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgDocuments.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }

                    var now = DateTime.Now;

                    rdProposal.SelectedDate = now.AddDays(1);
                    rdEffective.SelectedDate = now.AddDays(2);
                }
                else
                {
                    var documentId = item.GetDataKeyValue("TIPD_CODIGO");
                    radTypes.SelectedValue = documentId.ToString();

                    var proposal = (DateTime)item.GetDataKeyValue("DOC_FECHA_PROPUESTA");
                    rdProposal.SelectedDate = proposal;

                    var effective = (DateTime)item.GetDataKeyValue("DOC_FECHA_ASIENTO_DIARIO");
                    rdEffective.SelectedDate = effective;

                    var txtDescription = (RadTextBox)item.FindControl("txtDescription");
                    var description = (string)item.GetDataKeyValue("DOC_DESCRIPCION");
                    txtDescription.Text = description;

                    var account = item.GetDataKeyValue("CUE_CODIGO");
                    radAccounts.SelectedValue = account.ToString();

                    var checkBinding = (RadCheckBox)item.FindControl("checkBinding");
                    var check = (bool)item.GetDataKeyValue("DOC_ENLAZADO_TESORERIA");
                    checkBinding.Checked = check;

                    var tonnageSheet = item.GetDataKeyValue("HOJ_NUMERO");

                    if (tonnageSheet != null)
                    {
                        var txtTonnageSheet = (RadNumericTextBox)item.FindControl("txtTonnageSheet");
                        txtTonnageSheet.Value = Convert.ToDouble(tonnageSheet);
                    }

                    var tonnageSheetYear = item.GetDataKeyValue("ANO_HOJA");

                    if (tonnageSheetYear != null)
                    {
                        var rmyTonnageSheetYear = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                        rmyTonnageSheetYear.SelectedDate = new DateTime(Convert.ToInt32(tonnageSheetYear), 1, 1);
                    }

                    var tonnageSheet50 = item.GetDataKeyValue("HOJ_NUMERO50");

                    if (tonnageSheet50 != null)
                    {
                        var txtTonnageSheet50 = (RadNumericTextBox)item.FindControl("txtTonnageSheet50");
                        txtTonnageSheet50.Value = Convert.ToDouble(tonnageSheet50);
                    }

                    var tonnageSheet50Year = item.GetDataKeyValue("ANO_HOJA50");

                    if (tonnageSheet50Year != null)
                    {
                        var rmyTonnageSheet50Year = (RadMonthYearPicker)item.FindControl("rmyTonnageSheet50Year");
                        rmyTonnageSheet50Year.SelectedDate = new DateTime(Convert.ToInt32(tonnageSheet50Year), 1, 1);
                    }

                    var txtCheck = (RadTextBox)item.FindControl("txtCheck");
                    var checkValue = (string)item.GetDataKeyValue("DOC_NUMERO_CHEQUE");
                    txtCheck.Text = checkValue;

                    var txtSing = (RadTextBox)item.FindControl("txtSing");
                    var singValue = item.GetDataKeyValue("SEN_NUMERO");
                    txtSing.Text = singValue == null ? string.Empty : singValue.ToString();

                    var payType = item.GetDataKeyValue("TIPP_CODIGO");

                    if (payType != null)
                    {
                        radPayType.SelectedValue = payType.ToString();
                    }

                    var payForm = item.GetDataKeyValue("FOR_CODIGO");

                    if (payForm != null)
                    {
                        radPayForm.SelectedValue = payForm.ToString();
                    }
                }
            }

            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Documento";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Documento";
                dataBoundItem["ApplicationsColumn"].ToolTip = "Gestionar Aplicaciones";
            }
        }

        protected void RgDocuments_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;
                var radDocumentType = (RadComboBox)item.FindControl("radDropDocumentType");
                var rdProposal = (RadDatePicker)item.FindControl("rdProposal");
                var rdEffective = (RadDatePicker)item.FindControl("rdEffective");
                var txtDescription = (RadTextBox)item.FindControl("txtDescription");
                var radRestrictedAccount = (RadComboBox)item.FindControl("radDropRestrictedAccount");
                var txtCheck = (RadTextBox)item.FindControl("txtCheck");
                var checkBinding = (RadCheckBox)item.FindControl("checkBinding");
                var txtTonnageSheet = (RadNumericTextBox)item.FindControl("txtTonnageSheet");
                var rmyTonnageSheetYear = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                var txtTonnageSheet50 = (RadNumericTextBox)item.FindControl("txtTonnageSheet50");
                var rmyTonnageSheet50Year = (RadMonthYearPicker)item.FindControl("rmyTonnageSheet50Year");
                var radPayForm = (RadComboBox)item.FindControl("radDropPayForm");
                var radPayType = (RadComboBox)item.FindControl("radDropPayType");
                var txtSing = (RadTextBox)item.FindControl("txtSing");

                var documentType = Convert.ToInt32(radDocumentType.SelectedValue);
                var operationValue = DateTime.Now;
                var proposalValue = rdProposal.SelectedDate ?? operationValue.AddDays(1);
                var effectiveValue = rdEffective.SelectedDate ?? operationValue.AddDays(2);
                var descriptionValue = string.IsNullOrWhiteSpace(txtDescription.Text) ? string.Empty : txtDescription.Text;
                var restrictedAccount = Convert.ToInt32(radRestrictedAccount.SelectedValue);
                var tonnageSheetValue = txtTonnageSheet.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet.Value);
                var tonnageSheetYearValue = rmyTonnageSheetYear.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheetYear.SelectedDate).Year);
                var tonnageSheet50Value = txtTonnageSheet50.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet50.Value);
                var tonnageSheet50YearValue = rmyTonnageSheet50Year.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheet50Year.SelectedDate).Year);
                var checkValue = string.IsNullOrWhiteSpace(txtCheck.Text) ? string.Empty : txtCheck.Text;
                var payFormValue = radPayForm.SelectedValue == "-1" ? (byte?)null : Convert.ToByte(radPayForm.SelectedValue);
                var payTypeValue = radPayType.SelectedValue == "-1" ? (byte?)null : Convert.ToByte(radPayType.SelectedValue);
                var singValue = string.IsNullOrWhiteSpace(txtSing.Text) ? string.Empty : txtSing.Text;

                var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                {
                    DOC_I_G = "G",
                    DOC_FECHA_PROPUESTA = proposalValue,
                    DOC_FECHA_ASIENTO_DIARIO = effectiveValue,
                    DOC_NUMERO_CHEQUE = checkValue,
                    DOC_DESCRIPCION = descriptionValue,
                    EXP_CODIGO = this.AccountingRecord.EXP_CODIGO,
                    TIPD_CODIGO = documentType,
                    FOR_CODIGO = payFormValue,
                    CUE_CODIGO = restrictedAccount,
                    TIPP_CODIGO = payTypeValue,
                    DOC_ENLAZADO_TESORERIA = (bool)checkBinding.Checked,
                    HOJ_NUMERO = tonnageSheetValue,
                    HOJ_NUMERO50 = tonnageSheet50Value,
                    ANO_HOJA = tonnageSheetYearValue,
                    ANO_HOJA50 = tonnageSheet50YearValue,
                    DOC_FACTURA = singValue,
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var insert = this.accountingDocumentsService.InsertAccountingDocument(accountingDocument);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Documento Contable.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Documento Contable", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Documento Contable.");
                strBuilder.Append("Ha ocurrido un error insertando el Documento Contable en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Documento Contable", strBuilder, MessageType.Deny);
            }
        }

        protected void RgDocuments_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;
                var id = (int)item.GetDataKeyValue("DOC_CODIGO");
                var radDocumentType = (RadComboBox)item.FindControl("radDropDocumentType");
                var rdProposal = (RadDatePicker)item.FindControl("rdProposal");
                var rdEffective = (RadDatePicker)item.FindControl("rdEffective");
                var txtDescription = (RadTextBox)item.FindControl("txtDescription");
                var radRestrictedAccount = (RadComboBox)item.FindControl("radDropRestrictedAccount");
                var txtCheck = (RadTextBox)item.FindControl("txtCheck");
                var checkBinding = (RadCheckBox)item.FindControl("checkBinding");
                var txtTonnageSheet = (RadNumericTextBox)item.FindControl("txtTonnageSheet");
                var rmyTonnageSheetYear = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                var txtTonnageSheet50 = (RadNumericTextBox)item.FindControl("txtTonnageSheet50");
                var rmyTonnageSheet50Year = (RadMonthYearPicker)item.FindControl("rmyTonnageSheet50Year");
                var radPayForm = (RadComboBox)item.FindControl("radDropPayForm");
                var radPayType = (RadComboBox)item.FindControl("radDropPayType");
                var txtSing = (RadTextBox)item.FindControl("txtSing");

                var documentType = Convert.ToInt32(radDocumentType.SelectedValue);
                var operationValue = DateTime.Now;
                var proposalValue = rdProposal.SelectedDate ?? operationValue.AddDays(1);
                var effectiveValue = rdEffective.SelectedDate ?? operationValue.AddDays(2);
                var descriptionValue = string.IsNullOrWhiteSpace(txtDescription.Text) ? string.Empty : txtDescription.Text;
                var restrictedAccount = Convert.ToInt32(radRestrictedAccount.SelectedValue);
                var tonnageSheetValue = txtTonnageSheet.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet.Value);
                var tonnageSheetYearValue = rmyTonnageSheetYear.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheetYear.SelectedDate).Year);
                var tonnageSheet50Value = txtTonnageSheet50.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet50.Value);
                var tonnageSheet50YearValue = rmyTonnageSheet50Year.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheet50Year.SelectedDate).Year);
                var checkValue = string.IsNullOrWhiteSpace(txtCheck.Text) ? string.Empty : txtCheck.Text;
                var payFormValue = radPayForm.SelectedValue == "-1" ? (byte?)null : Convert.ToByte(radPayForm.SelectedValue);
                var payTypeValue = radPayType.SelectedValue == "-1" ? (byte?)null : Convert.ToByte(radPayType.SelectedValue);
                var singValue = string.IsNullOrWhiteSpace(txtSing.Text) ? string.Empty : txtSing.Text;

                var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                {
                    DOC_CODIGO = id,
                    DOC_I_G = "G",
                    DOC_FECHA_PROPUESTA = proposalValue,
                    DOC_FECHA_ASIENTO_DIARIO = effectiveValue,
                    DOC_NUMERO_CHEQUE = checkValue,
                    DOC_DESCRIPCION = descriptionValue,
                    EXP_CODIGO = this.AccountingRecord.EXP_CODIGO,
                    TIPD_CODIGO = documentType,
                    FOR_CODIGO = payFormValue,
                    CUE_CODIGO = restrictedAccount,
                    TIPP_CODIGO = payTypeValue,
                    DOC_ENLAZADO_TESORERIA = (bool)checkBinding.Checked,
                    HOJ_NUMERO = tonnageSheetValue,
                    HOJ_NUMERO50 = tonnageSheet50Value,
                    ANO_HOJA = tonnageSheetYearValue,
                    ANO_HOJA50 = tonnageSheet50YearValue,
                    DOC_FACTURA = singValue,
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var update = this.accountingDocumentsService.UpdateAccountingDocument(accountingDocument);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Documento Contable.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Documento Contable en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Documento Contable", strBuilder, MessageType.Warning);
                }
                else
                {
                    var document = new PRE_DOCUMENTO_CONTABLE
                    {
                        DOC_CODIGO = id,
                        DOC_FECHA_MOVIMIENTO_I = proposalValue,
                        DOC_FECHA_PROPUESTA = proposalValue.AddDays(1),
                        DOC_FECHA_ASIENTO_DIARIO = proposalValue.AddDays(2),
                        USU_CODIGO = LoginUser.USU_CODIGO
                    };

                    var updateDates = this.accountingDocumentsService.UpdateIncomeDiscounts(document);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Documento Contable.");
                strBuilder.Append("Ha ocurrido un error modificando el Documento Contable en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Documento Contable", strBuilder, MessageType.Deny);
            }
        }

        protected void RgDocuments_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = e.Item as GridDataItem;

                var id = (int)item.GetDataKeyValue("DOC_CODIGO");

                var delete = this.accountingDocumentsService.DeleteAccountingDocument(id, LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Ha Ocurrido un error elimando el Documento Contable en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Documento Contable.");
                strBuilder.Append("Ha ocurrido un error eliminando el Documento Contable en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Deny);
            }
        }

        protected void RgDocuments_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            if (e.CommandName == "DeleteCommand")
            {
                var item = e.Item as GridDataItem;

                var treasuryId = item.GetDataKeyValue("TES_CODIGO");

                if (treasuryId != null)
                {
                    strBuilder.Append("No se puede eliminar el documento contable por que tiene apuntes de tesoreria asociados.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Warning);

                    return;
                }

                var binding = (bool)item.GetDataKeyValue("DOC_ENLAZADO_TESORERIA");

                if (binding)
                {
                    strBuilder.Append("No se puede eliminar el documento contable por que está enlazado con tesorería,  si quiere usted eliminarlo, borre el apunte de tesorería y quite el enlace de este documento.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Warning);

                    return;
                }

                var tonnageSheet = item.GetDataKeyValue("HOJ_NUMERO");

                if (tonnageSheet != null)
                {
                    strBuilder.Append("No se puede eliminar el documento contable porque está enlazado con una hoja de arqueo, si quiere usted eliminarlo, borre la hoja de arqueo y quite el N⁰ del Hoja de arqueo de este documento.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Warning);

                    return;
                }

                var tonnageSheet50 = item.GetDataKeyValue("HOJ_NUMERO50");

                if (tonnageSheet50 != null)
                {
                    strBuilder.Append("No se puede eliminar el documento contable porque está enlazado con Hoja de arqueo 50, si quiere usted eliminarlo, borre la hoja de arqueo y quite el N⁰ del Hoja de arqueo 50 de este documento.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Warning);

                    return;
                }

                var singId = item.GetDataKeyValue("SEN_CODIGO");

                if (singId != null)
                {
                    strBuilder.Append("No se puede eliminar el documento contable porque está contenido en un señalamiento, si quiere usted eliminarlo, elimine este documento del señalamiento.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Warning);

                    return;
                }

                var id = (int)item.GetDataKeyValue("DOC_CODIGO");
                var documentType = (string)item.GetDataKeyValue("TIPO_DOC");

                var purchasesCount = this.accountingDocumentsService.GetPurchasesCount(id);

                if (purchasesCount != 0)
                {
                    strBuilder.Append("No se puede eliminar el documento contable porque tiene facturas vinculadas, si quiere usted eliminarlo, desvincule primero las facturas del documento.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar documento contable", strBuilder, MessageType.Warning);

                    return;
                }

                var script = $"deleteSpendRecord('{item.ItemIndex}', '{documentType}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "deleteSpendRecord", script, true);
            }

            if (e.CommandName == "UpdateApplications")
            {
                if (this.AccountingRecord.EXP_ANO_PRESUPUESTO == null)
                {
                    strBuilder.Append("No pueden gestionarse las apliciones debido a que el año del presupuesto no está con valor.");
                    this.ShowMessage(this.RadNotification, "Imposible gestinar aplicaciones", strBuilder, MessageType.Warning);

                    return;
                }

                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
                var documentType = (string)item.GetDataKeyValue("TIPO_DOC");
                var documentText = (string)item.GetDataKeyValue("DOC_DESCRIPCION");
                var documentTitle = string.IsNullOrWhiteSpace(documentText) ? documentType.Replace("+", ":") : $"{documentType.Replace("+", ":")} | {documentText}";
                var year = Convert.ToInt32(this.AccountingRecord.EXP_ANO_PRESUPUESTO);

                var script = $"updateApplications('{documentId}', '{year}', '{this.AccountingRecord.EXP_CODIGO}', '{documentTitle}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "updateApplications", script, true);
            }

            if (e.CommandName == "PrintApplications")
            {
                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");

                var documentCounts = this.accountingDocumentsService.GetDocumentsCount(documentId);

                if (documentCounts >= 1)
                {
                    var page1 = string.Empty;
                    var page2 = string.Empty;

                    var haveRc = this.accountingDocumentsService.HaveRcPhase(documentId);
                    var haveAd = this.accountingDocumentsService.HaveAdPhase(documentId);
                    var haveO = this.accountingDocumentsService.HaveOPhase(documentId);
                    var haveP = this.accountingDocumentsService.HavePPhase(documentId);

                    var haveExtraBudgetaries = this.accountingDocumentsService.HaveExtraBudgetaries(documentId);

                    var haveIncomeDiscounts = this.accountingDocumentsService.HaveIncomeDiscounts(documentId);

                    if (haveRc && !haveAd && !haveO && !haveP)
                    {
                        page1 = "spendRC";

                        if (documentCounts > 1)
                        {
                            page2 = "spendAnnexRc";
                        }
                    }
                    else
                    {
                        if ((!haveRc && haveAd && !haveO && !haveP) || (haveRc && haveAd && !haveO && !haveP))
                        {
                            page1 = "spendAD";
                        }
                        else
                        {
                            if ((!haveRc && !haveAd && haveO && !haveP) || (haveRc && haveAd && haveO && !haveP) || (!haveRc && haveAd && haveO && !haveP))
                            {
                                if (!haveExtraBudgetaries)
                                {
                                    if (documentCounts == 1 && haveIncomeDiscounts)
                                    {
                                        page1 = "spendIncomeDiscounts";
                                    }
                                    else
                                    {
                                        page1 = "spendO";
                                    }
                                }
                                else
                                {
                                    if (haveIncomeDiscounts)
                                    {
                                        page1 = "spendPRecordDiscounts";
                                    }
                                    else
                                    {
                                        page1 = "spendORecord";
                                    }
                                }
                            }
                            else
                            {
                                if ((!haveRc && haveAd && haveO && haveP) || (!haveRc && !haveAd && !haveO && !haveP) || (!haveRc && !haveAd && haveO && haveP) || (haveRc && haveAd && haveO && haveP))
                                {
                                    if (!haveExtraBudgetaries)
                                    {
                                        if (documentCounts == 1 && haveIncomeDiscounts)
                                        {
                                            page1 = "spendIncomeDiscounts";
                                        }
                                        else
                                        {
                                            page1 = "spendP";

                                            if (documentCounts > 1)
                                            {
                                                page2 = "spendAnnexP";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (haveIncomeDiscounts)
                                        {
                                            page1 = "spendPRecordDiscounts";
                                        }
                                        else
                                        {
                                            page1 = "spendPRecord";
                                        }

                                        if (documentCounts > 1)
                                        {
                                            page2 = "spendAnnexP";
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showReport", $"showReport('{page1}', '{page2}', '{documentId}')", true);
                }
                else
                {
                    strBuilder.Append("No se puede imprimir el documento contable porque no presenta documentos asociados.");
                    this.ShowMessage(this.RadNotification, "Imposible imprimir documento", strBuilder, MessageType.Warning);

                    return;
                }
            }

            if (e.CommandName == "PrintCertificate")
            {
                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");

                var haveRc = this.accountingDocumentsService.HaveRcPhase(documentId);
                var haveAd = this.accountingDocumentsService.HaveAdPhase(documentId);

                if (haveRc || haveAd)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showCertificateReport", $"showCertificateReport('{documentId}')", true);
                }
                else
                {
                    strBuilder.Append("No se puede imprimir el certificado debido a que el documento en cuestión no cumple con los requisitos.");
                    this.ShowMessage(this.RadNotification, "Imposible imprimir documento", strBuilder, MessageType.Warning);
                }
            }
        }

        protected void btnIncomeDiscounts_OnClick(object sender, EventArgs e)
        {
            if (this.RgDocuments.SelectedItems.Count == 0)
            {
                var strBuilder = new StringBuilder();
                strBuilder.Append("No pueden gestionarse los descuentos de ingresos debido a que no hay ningún documento seleccionado.");
                this.ShowMessage(this.RadNotification, "Imposible gestinar descuentos", strBuilder, MessageType.Warning);

                return;
            }

            var item = (GridDataItem)this.RgDocuments.SelectedItems[0];
            var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
            var year = Convert.ToInt32(this.AccountingRecord.EXP_ANO_PRESUPUESTO);
            var proposal = (DateTime)item.GetDataKeyValue("DOC_FECHA_PROPUESTA");

            var script = $"updateIncomeDiscounts('{documentId}', '{year}', '{this.AccountingRecord.EXP_CODIGO}', '{proposal.ToString(new CultureInfo("es-ES"))}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "updateIncomeDiscounts", script, true);
        }

        protected void btnExtraBudgetaryDiscounts_OnClick(object sender, EventArgs e)
        {
            if (this.RgDocuments.SelectedItems.Count == 0)
            {
                var strBuilder = new StringBuilder();
                strBuilder.Append("No pueden gestionarse los descuentos extrapresupuestarios debido a que no hay ningún documento seleccionado.");
                this.ShowMessage(this.RadNotification, "Imposible gestinar descuentos", strBuilder, MessageType.Warning);

                return;
            }

            var item = (GridDataItem)this.RgDocuments.SelectedItems[0];
            var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
            var year = Convert.ToInt32(this.AccountingRecord.EXP_ANO_PRESUPUESTO);
            var annualFileId = this.AccountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL ?? -1;

            var script = $"updateExtraBudgetaryDiscounts('{documentId}', '{this.AccountingRecord.EXP_CODIGO}', '{year}', '{annualFileId}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "updateExtraBudgetaryDiscounts", script, true);
        }

        protected void btnSeeStatusRecord_OnClick(object sender, EventArgs e)
        {
            var script = $"seeStatusRecord('{this.AccountingRecord.EXP_CODIGO}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeStatusRecord", script, true);
        }

        protected void btnSeeNotesTreasuries_OnClick(object sender, EventArgs e)
        {
            if (this.RgDocuments.SelectedItems.Count == 0)
            {
                var strBuilder = new StringBuilder();
                strBuilder.Append("No se pueden ver los apuntes de tesorería debido a que no hay ningún documento seleccionado.");
                this.ShowMessage(this.RadNotification, "Imposible ver apuntes de tesorería", strBuilder, MessageType.Warning);

                return;
            }

            var item = (GridDataItem)this.RgDocuments.SelectedItems[0];
            var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
            var treasuryId = item.GetDataKeyValue("TES_CODIGO") == null ? -1 : (int)item.GetDataKeyValue("TES_CODIGO");

            // if (treasuryId == null)
            // {
            // var strBuilder = new StringBuilder();
            // strBuilder.Append("No se pueden ver los apuntes de tesorería debido a que el documento seleccionado no tiene apuntes de tesoría.");
            // this.ShowMessage(this.RadNotification, "Imposible ver apuntes de tesorería", strBuilder, MessageType.Warning);

            // return;
            // }
            var script = $"seeNotesTreasuries('{documentId}', '{(int)treasuryId}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeNotesTreasuries", script, true);
        }

        protected void btnPurchaseBills_OnClick(object sender, EventArgs e)
        {
            if (this.RgDocuments.SelectedItems.Count == 0)
            {
                var strBuilder = new StringBuilder();
                strBuilder.Append("No se pueden ver las facturas de compras debido a que no hay ningún documento seleccionado.");
                this.ShowMessage(this.RadNotification, "Imposible ver facturas de compras", strBuilder, MessageType.Warning);

                return;
            }

            var item = (GridDataItem)this.RgDocuments.SelectedItems[0];
            var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
            var administrativeId = item.GetDataKeyValue("EA_CODIGO") == null ? -1 : (int)item.GetDataKeyValue("EA_CODIGO");
            var yearNumber = item.GetDataKeyValue("EXP_NUM_EXP_CONTABLE_ANUAL") == null ? -1 : (int)item.GetDataKeyValue("EXP_NUM_EXP_CONTABLE_ANUAL");

            var script = $"seePurchaseBills('{documentId}', '{this.AccountingRecord.EXP_CODIGO}', '{administrativeId}', '{this.AccountingRecord.EXP_DESCRIPCION}', '{this.AccountingRecord.EXP_ANO_PRESUPUESTO}', '{yearNumber}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seePurchaseBills", script, true);
        }

        protected void btnSee_OnClick(object sender, EventArgs e)
        {
            var script = $"seeMultiYears('{this.AccountingRecord.EXP_CODIGO}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeMultiYears", script, true);
        }

        protected void btnProviders_OnClick(object sender, EventArgs e)
        {
            var script = $"seeProviders('{this.AccountingRecord.EXP_CODIGO}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeProviders", script, true);
        }

        protected void RcProviders_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var providerId = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;

            if (providerId.Equals("112"))
            {
                this.btnProviders.Enabled = true;
            }
            else
            {
                if (this.btnProviders.Enabled)
                {
                    var providers = accountingRecordsService.GetProviders(this.AccountingRecord.EXP_CODIGO);

                    if (providers.Any())
                    {
                        var strBuilder = new StringBuilder();

                        strBuilder.Append("Si el expediente ya no está asociado a varios proveedores, debe primero eliminarlos.");
                        this.ShowMessage(this.RadNotification, "Imposible cambiar proveedor", strBuilder, MessageType.Warning);

                        this.RcProviders.SelectedValue = "112";

                        return;
                    }
                }

                this.btnProviders.Enabled = false;
            }
        }

        #endregion
    }
}