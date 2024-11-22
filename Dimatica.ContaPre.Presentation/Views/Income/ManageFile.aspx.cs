namespace Dimatica.ContaPre.Presentation.Views.Income
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

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;
    using Telerik.Web.UI.Calendar;

    #endregion

    public partial class ManageFile : BasePage
    {
        #region Static Fields and Constants

        private static IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        #endregion

        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        private ICostPlacesService costPlacesService = DependencyFactory.GetInstance<ICostPlacesService>();

        private IDocumentTypesService documentTypesService = DependencyFactory.GetInstance<IDocumentTypesService>();

        private IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Properties

        private PRE_EXPEDIENTE_CONTABLE AccountingFile
        {
            get
            {
                var file = this.Session["_accountingFile"] as PRE_EXPEDIENTE_CONTABLE;

                if (file == null)
                {
                    file = new PRE_EXPEDIENTE_CONTABLE();

                    this.Session["_accountingFile"] = file;
                }

                return file;
            }

            set
            {
                this.Session["_accountingFile"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var fileId = this.Request.QueryString["id"];

            if (fileId == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideGrids", $"hideGrids();", true);
            }


            if (!this.IsPostBack)
            {
                BindProviders();

                this.AccountingFile = null;

                var provenances = this.provenancesService.GetProvenances("I");

                this.RcProvenances.DataSource = provenances;
                this.RcProvenances.DataBind();

                var areas = this.costPlacesService.GetCostPlacesOriginToCombo();

                this.RcAreas.DataSource = areas;
                this.RcAreas.DataBind();

                if (fileId == null)
                {
                    this.titleHeader.InnerText = "Nuevo Expediente";
                    this.RmyYear.SelectedDate = DateTime.Now;

                    this.LblSquare.Text = "No";
                    this.LblDrAmount.Text = 0.ToString("N", new CultureInfo("es-ES"));
                    this.LblMiAmount.Text = 0.ToString("N", new CultureInfo("es-ES"));
                    this.LblDifference.Text = 0.ToString("N", new CultureInfo("es-ES"));

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideGrids", "hideGrids();", true);
                }
                else
                {

                    this.titleHeader.InnerText = "Editar Expediente";

                    var accountingFile = accountingRecordsService.GetById(Convert.ToInt32(fileId));

                    if (accountingFile == null)
                    {
                        this.Response.Redirect("~/Views/Income/ManageFile.aspx");
                    }
                    else
                    {
                        this.AccountingFile = accountingFile;
                        this.RmyYear.SelectedDate = new DateTime(Convert.ToInt32(this.AccountingFile.EXP_ANO_PRESUPUESTO), 1, 1);
                        this.RmyYear.Enabled = false;

                        if (this.AccountingFile.PROV_CODIGO != null)
                        {
                            this.RcProviders.SelectedValue = this.AccountingFile.PROV_CODIGO.ToString();
                        }

                        if (this.AccountingFile.PROC_CODIGO != null)
                        {
                            this.RcProvenances.SelectedValue = this.AccountingFile.PROC_CODIGO.ToString();
                        }

                        if (this.AccountingFile.CEN_CODIGO != null)
                        {
                            this.RcAreas.SelectedValue = this.AccountingFile.CEN_CODIGO.ToString();
                        }

                        this.RtbDescription.Text = this.AccountingFile.EXP_DESCRIPCION;
                        this.LblSquare.Text = (bool)this.AccountingFile.EXP_CUADRADO ? "Si" : "No";

                        var drAmount = accountingRecordsService.GetDrAmount(this.AccountingFile.EXP_CODIGO);
                        this.LblDrAmount.Text = drAmount.ToString("N", new CultureInfo("es-ES"));

                        var miAmount = accountingRecordsService.GetMiAmount(this.AccountingFile.EXP_CODIGO);
                        this.LblMiAmount.Text = miAmount.ToString("N", new CultureInfo("es-ES"));

                        this.LblDifference.Text = (drAmount - miAmount).ToString("N", new CultureInfo("es-ES"));

                        if (miAmount > drAmount)
                        {
                            var strBuilder = new StringBuilder();
                            strBuilder.Append("En este expediente la suma de importes de MI es superior a la de DR.</br>Revise los documentos.");
                            this.ShowMessage(this.RadNotification, "Aviso", strBuilder, MessageType.Warning);
                        }
                    }
                }
              
            } 
        }

        private void BindProviders()
        {
            var providers = this.providersService.GetProvidersToCombo();
            RcProviders.DataSource = providers;
            RcProviders.DataTextField = "PROV_NOMBRE";
            RcProviders.DataValueField = "PROV_CODIGO";
            RcProviders.DataBind();
            RcProviders.Items.Insert(0, new RadComboBoxItem("Seleccione un proveedor", "-1"));
        }

        protected void RcProviders_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var selectedProviderCode = RcProviders.SelectedValue;
            BindProviders();
        }


        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            if (this.AccountingFile.EXP_CODIGO == 0)
            {
                int? providerCode = null;

                if (!string.IsNullOrEmpty(RcProviders.SelectedValue) && RcProviders.SelectedValue != "-1")
                {
                    providerCode = Convert.ToInt32(RcProviders.SelectedValue);
                }

                var accountingFile = new PRE_EXPEDIENTE_CONTABLE
                {
                    EXP_I_G = "I",
                    EXP_DESCRIPCION = string.IsNullOrWhiteSpace(this.RtbDescription.Text) ? string.Empty : this.RtbDescription.Text,
                    EXP_PLURIANUAL = false,
                    EXP_CUADRADO = false,
                    EXP_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyYear.SelectedDate).Year),
                    PROV_CODIGO = providerCode,
                    PROC_CODIGO = Convert.ToInt32(this.RcProvenances.SelectedValue),
                    CEN_CODIGO = Convert.ToInt32(this.RcAreas.SelectedValue),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                try
                {
                    var insert = accountingRecordsService.InsertAccountingRecord(accountingFile);

                    switch (insert.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos del Expediente Contable.");

                            break;
                    }

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error insertando el Expediente Contable", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        var id = (int)insert.ResponseMethod;
                        this.Response.Redirect($"~/Views/Income/ManageFile.aspx?id={id}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error insertando el Expediente Contable.");
                    strBuilder.Append("Ha ocurrido un error insertando el Expediente Contable en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error insertando el Expediente Contable", strBuilder, MessageType.Deny);
                }
            }
            else
            {

                int? providerCode = null;

                if (!string.IsNullOrEmpty(RcProviders.SelectedValue) && RcProviders.SelectedValue != "-1")
                {
                    providerCode = Convert.ToInt32(RcProviders.SelectedValue);
                }

                var accountingFile = new PRE_EXPEDIENTE_CONTABLE
                {
                    EXP_CODIGO = this.AccountingFile.EXP_CODIGO,
                    EXP_I_G = "I",
                    EXP_DESCRIPCION = string.IsNullOrWhiteSpace(this.RtbDescription.Text) ? string.Empty : this.RtbDescription.Text,
                    EXP_PLURIANUAL = false,
                    EXP_CUADRADO = this.AccountingFile.EXP_CUADRADO,
                    EXP_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyYear.SelectedDate).Year),
                    PROV_CODIGO = providerCode,
                    PROC_CODIGO = Convert.ToInt32(this.RcProvenances.SelectedValue),
                    CEN_CODIGO = Convert.ToInt32(this.RcAreas.SelectedValue),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                try
                {
                    var update = accountingRecordsService.UpdateAccountingRecord(accountingFile);

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
                        this.Response.Redirect($"~/Views/Income/ManageFile.aspx?id={this.AccountingFile.EXP_CODIGO}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error modificando el Expediente Contable.");
                    strBuilder.Append("Ha ocurrido un error modificando el Expediente Contable en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error modificando el Expediente Contable", strBuilder, MessageType.Deny);
                }
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            this.rwmManageFile.RadConfirm($"¿ Desea realmente eliminar el expediente de ingresos {this.AccountingFile.PROC_DESCRIPCION} del año {this.AccountingFile.EXP_ANO_PRESUPUESTO} ?<br/><br/>Se eliminarán todos los datos asociados a dicho documento:<br/>- Documentos de ingresos.<br/>- Aplicaciones asociadas y sus importes.", "confirmDeleteCallBackFn", 330, 140, null, "Confirmación");
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Income/Files.aspx");
        }

        protected void RgDocuments_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var accountingDocuments = new System.Collections.Generic.List<PRE_DOCUMENTO_CONTABLE>();

            if (this.AccountingFile.EXP_CODIGO != 0)
            {
                accountingDocuments = this.accountingDocumentsService.GetByIdAndType("I", this.AccountingFile.EXP_CODIGO);
            }

            this.RgDocuments.DataSource = accountingDocuments;
        }

        protected void RgDocuments_OnPreRender(object sender, EventArgs e) { }

        protected void RgDocuments_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            // Manejo para filas de datos (GridDataItem)
            if (e.Item is GridDataItem dataBoundItem)
            {
                // Obtener el valor de TIPO_DOC
                var docType = dataBoundItem.GetDataKeyValue("TIPO_DOC").ToString();

                // Ocultar columna de impresión para ciertos tipos de documento
                if (docType.Equals("400 PMP") || docType.Contains("MI") || docType.Contains("R-") || docType.Contains("R+"))
                {
                    return;
                }

                // Ocultar columna de impresión
                dataBoundItem["PrintColumn"].Style.Add("display", "none !important");
            }

            // Manejo para elementos editables
            if (e.Item is GridEditableItem item && e.Item.IsInEditMode)
            {

                // Configuración para controles en el formulario de edición
                var radTypes = (RadComboBox)item.FindControl("radDropDocumentType");
                var radAccounts = (RadComboBox)item.FindControl("radDropRestrictedAccount");

                // Llenar combobox de tipos de documento
                var documentTypes = this.documentTypesService.GetDocumentTypesToCombo("I");
                radTypes.DataSource = documentTypes.ToList();
                radTypes.DataBind();

                // Llenar combobox de cuentas restringidas
                var restrictedAccount = this.accountRestrictedService.GetAccountsRestricted("I");
                radAccounts.DataSource = restrictedAccount.ToList();
                radAccounts.DataBind();
                radAccounts.SelectedValue = "10"; // Valor predeterminado

                // Inicializar controles de fechas
                var rdOperation = (RadDatePicker)item.FindControl("rdOperation");
                var rdProposal = (RadDatePicker)item.FindControl("rdProposal");
                var rdEffective = (RadDatePicker)item.FindControl("rdEffective");

                // Manejo para inserción de un nuevo registro
                if (e.Item is GridEditFormInsertItem)
                {
                    // Cerrar otras filas en modo edición
                    foreach (GridDataItem i in this.RgDocuments.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }

                    // Asignar fechas predeterminadas
                    var now = DateTime.Now;
                    rdOperation.SelectedDate = now;
                    rdProposal.SelectedDate = now.AddDays(1);
                    rdEffective.SelectedDate = now.AddDays(2);
                }
                else // Manejo para edición de un registro existente
                {                    

                    // Obtener valores de las claves de datos
                    var documentId = item.GetDataKeyValue("TIPD_CODIGO");
                    radTypes.SelectedValue = documentId.ToString();

                    // Configurar número de documento
                    var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
                    var number = item.GetDataKeyValue("DOC_NUMERO_MOVIMIENTO_I");
                    txtNumber.Value = number == null ? (double?)null : Convert.ToDouble((int)number);

                    // Configurar fechas
                    rdOperation.SelectedDate = (DateTime)item.GetDataKeyValue("DOC_FECHA_MOVIMIENTO_I");
                    rdProposal.SelectedDate = (DateTime)item.GetDataKeyValue("DOC_FECHA_PROPUESTA");
                    rdEffective.SelectedDate = (DateTime)item.GetDataKeyValue("DOC_FECHA_ASIENTO_DIARIO");

                    // Configurar descripción
                    var txtDescription = (RadTextBox)item.FindControl("txtDescription");
                    txtDescription.Text = (string)item.GetDataKeyValue("DOC_DESCRIPCION");

                    // Configurar cuenta restringida
                    var account = item.GetDataKeyValue("CUE_CODIGO");
                    radAccounts.SelectedValue = account.ToString();

                    // Configurar enlace de tesorería
                    var checkBinding = (RadCheckBox)item.FindControl("checkBinding");
                    checkBinding.Checked = (bool)item.GetDataKeyValue("DOC_ENLAZADO_TESORERIA");

                    // Configurar hoja de arqueo y años
                    if (item.GetDataKeyValue("HOJ_NUMERO") is int tonnageSheet)
                    {
                        var txtTonnageSheet = (RadNumericTextBox)item.FindControl("txtTonnageSheet");
                        txtTonnageSheet.Value = tonnageSheet;
                    }

                    var tonnageSheetYear = item.GetDataKeyValue("ANO_HOJA");

                    // Validar si el valor no es nulo
                    if (tonnageSheetYear != null && int.TryParse(tonnageSheetYear.ToString(), out int year))
                    {
                        var rmyTonnageSheetYear = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                        rmyTonnageSheetYear.SelectedDate = new DateTime(year, 1, 1);
                    }
                    else
                    { 
                        var rmyTonnageSheetYear = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                        rmyTonnageSheetYear.SelectedDate = null; // o DateTime.Now si es preferible.
                    }

                    // Configurar hoja de arqueo 50
                    if (item.GetDataKeyValue("HOJ_NUMERO50") is int tonnageSheet50)
                    {
                        var txtTonnageSheet50 = (RadNumericTextBox)item.FindControl("txtTonnageSheet50");
                        txtTonnageSheet50.Value = tonnageSheet50;
                    }

                    var tonnageSheetYear50 = item.GetDataKeyValue("ANO_HOJA50");

                    // Validar si el valor no es nulo
                    if (tonnageSheetYear50 != null && int.TryParse(tonnageSheetYear50.ToString(), out int year50))
                    {
                        var rmyTonnageSheetYear50 = (RadMonthYearPicker)item.FindControl("rmyTonnageSheet50Year");
                        rmyTonnageSheetYear50.SelectedDate = new DateTime(year50, 1, 1);
                    }
                    else
                    {
                        var rmyTonnageSheetYear = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                        rmyTonnageSheetYear.SelectedDate = null; // o DateTime.Now si es preferible.
                    }

                    // Configurar número de cheque
                    var txtCheck = (RadTextBox)item.FindControl("txtCheck");
                    txtCheck.Text = (string)item.GetDataKeyValue("DOC_NUMERO_CHEQUE");
                }
            }
        }

        protected void RgDocuments_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;
                var radDocumentType = (RadComboBox)item.FindControl("radDropDocumentType");
                var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
                var rdOperation = (RadDatePicker)item.FindControl("rdOperation");
                var rdProposal = (RadDatePicker)item.FindControl("rdProposal");
                var rdEffective = (RadDatePicker)item.FindControl("rdEffective");
                var txtDescription = (RadTextBox)item.FindControl("txtDescription");
                var radRestrictedAccount = (RadComboBox)item.FindControl("radDropRestrictedAccount");
                var checkBinding = (RadCheckBox)item.FindControl("checkBinding");
                var txtTonnageSheet       = (RadNumericTextBox)item.FindControl("txtTonnageSheet");
                var rmyTonnageSheetYear   = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                var txtTonnageSheet50     = (RadNumericTextBox)item.FindControl("txtTonnageSheet50");
                var rmyTonnageSheet50Year = (RadMonthYearPicker)item.FindControl("rmyTonnageSheet50Year");
                var txtCheck              = (RadTextBox)item.FindControl("txtCheck");

                var year = ((DateTime)this.RmyYear.SelectedDate).Year;
                var documentType = Convert.ToInt32(radDocumentType.SelectedValue);
                //var numberValue = txtNumber.Value == null ? 0 : Convert.ToInt32(txtNumber.Value);
                var operationValue = (DateTime)rdOperation.SelectedDate;
                var proposalValue = rdProposal.SelectedDate ?? operationValue.AddDays(1);
                var effectiveValue = rdEffective.SelectedDate ?? operationValue.AddDays(2);
                var descriptionValue = string.IsNullOrWhiteSpace(txtDescription.Text) ? string.Empty : txtDescription.Text;
                var restrictedAccount = Convert.ToInt32(radRestrictedAccount.SelectedValue);
                var tonnageSheetValue = txtTonnageSheet.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet.Value);
                var tonnageSheetYearValue = rmyTonnageSheetYear.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheetYear.SelectedDate).Year);
                var tonnageSheet50Value = txtTonnageSheet50.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet50.Value);
                var tonnageSheet50YearValue = rmyTonnageSheet50Year.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheet50Year.SelectedDate).Year);
                var checkValue = string.IsNullOrWhiteSpace(txtCheck.Text) ? string.Empty : txtCheck.Text;

                int numberValue;

                if (documentType == 3 || documentType == 4 || documentType == 5)
                {
                    numberValue = this.accountingDocumentsService.GetLastMiNumber(year);
                }
                else
                {
                    numberValue = txtNumber.Value == null ? 0 : Convert.ToInt32(txtNumber.Value);
                }

                //if (numberValue == 0 && (documentType == 3 || documentType == 4 || documentType == 5))
                //{
                //    numberValue = this.accountingDocumentsService.GetLastMiNumber(year);
                //}

                //if (numberValue == 0 && documentType == 5)
                //{
                //    numberValue = accountingRecordsService.GetLastYearNumber(year, "G");
                //}

                if (numberValue != 0)
                {
                    var number = this.accountingDocumentsService.CheckMiNumber(year, numberValue, documentType, 0);

                    if (number > 0)
                    {
                        strBuilder.Append($"El número de ingreso {numberValue} para el año de ejercicio {year} ya existe.</br>Verifique que el año de ejercicio y el número de ingreso son correctos o deje el número de ingreso en blanco para que el sistema le asigne uno de forma automática.");
                        this.ShowMessage(this.RadNotification, "Atención", strBuilder, MessageType.Warning);

                        return;
                    }
                }

                var countTonnageSheet = 0;
                if (txtTonnageSheet.Value != null && rmyTonnageSheetYear.SelectedDate != null)
                {
                    countTonnageSheet = this.accountingDocumentsService.CheckTonnageSheet(((DateTime)rmyTonnageSheetYear.SelectedDate).Year, Convert.ToInt32(txtTonnageSheet.Value), true);
                }

                var countTonnageSheet50 = 0;

                if (txtTonnageSheet50.Value != null && rmyTonnageSheet50Year.SelectedDate != null)
                {
                    countTonnageSheet50 = this.accountingDocumentsService.CheckTonnageSheet(((DateTime)rmyTonnageSheet50Year.SelectedDate).Year, Convert.ToInt32(txtTonnageSheet50.Value), false);
                }

                if (countTonnageSheet > 0 || countTonnageSheet50 > 0)
                {
                    strBuilder.Append("El número de hoja de arqueo introducido ya existe. Cambie el número de hoja de arqueo.");
                    this.ShowMessage(this.RadNotification, "Atención", strBuilder, MessageType.Warning);

                    return;
                }

                var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                {
                    DOC_I_G = "I",
                    DOC_FECHA_PROPUESTA = proposalValue,
                    DOC_FECHA_ASIENTO_DIARIO = effectiveValue,
                    DOC_NUMERO_CHEQUE = checkValue,
                    DOC_DESCRIPCION = descriptionValue,
                    DOC_NUMERO_MOVIMIENTO_I = numberValue,
                    DOC_FECHA_MOVIMIENTO_I = operationValue,
                    EXP_CODIGO = this.AccountingFile.EXP_CODIGO,
                    TIPD_CODIGO = documentType,
                    CUE_CODIGO = restrictedAccount,
                    DOC_ENLAZADO_TESORERIA = (bool)checkBinding.Checked,
                    HOJ_NUMERO = tonnageSheetValue,
                    HOJ_NUMERO50 = tonnageSheet50Value,
                    ANO_HOJA = tonnageSheetYearValue,
                    ANO_HOJA50 = tonnageSheet50YearValue,
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

                // var isnsert = this.accountingDocumentsService.InsertAccountingDocument("I", proposalValue, effectiveValue, checkValue, descriptionValue, numberValue, operationValue, this.AccountingFile.EXP_CODIGO, documentType, restrictedAccount, (bool)checkBinding.Checked, tonnageSheetValue, tonnageSheet50Value, rmyTonnageSheetYear.SelectedDate, rmyTonnageSheet50Year.SelectedDate, LoginUser.USU_CODIGO);

                // var error = false;

                // if (cacsCode.EndsWith("/"))
                // {
                // var count = radDocumentType.Items.Count(i => i.Value.Contains(cacsCode));

                // if (count > 1)
                // {
                // strBuilder.Append("La Aplicación tiene subconceptos por lo que no se puede seleccionar. Si lo desea, puede seleccionar uno de dichos subconceptos. ");
                // error = true;
                // }
                // }

                // foreach (GridDataItem itemVerify in this.RgIncomeApplications.MasterTableView.Items)
                // {
                // var code = itemVerify.GetDataKeyValue("CACS_CODIGO").ToString();

                // if (!code.Equals(cacsCode))
                // {
                // continue;
                // }

                // strBuilder.Append("La Aplicación seleccionada ya se encuentra en la Modificación de Credito. ");
                // error = true;

                // break;
                // }

                // var txtIncomeAmount = (RadNumericTextBox)item.FindControl("txtIncomeAmount");

                // if (txtIncomeAmount.Value == null || txtIncomeAmount.Value == 0)
                // {
                // strBuilder.Append("El importe de la Aplicación tiene que tener un valor mayor que 0. ");
                // error = true;
                // }

                // if (error)
                // {
                // this.ShowMessage(this.RadNotification, "Imposible agregar Aplicación", strBuilder, MessageType.Warning);
                // e.Canceled = true;

                // return;
                // }

                // var radSing = (RadComboBox)item.FindControl("radDropIncomeSing");

                // var credictModificationBudget = new PRE_MODIF_CREDITO_PRESUPUESTO
                // {
                // CACS_CODIGO = cacsCode,
                // MODP_I_G = "I",
                // MOD_CODIGO = this.CreditModification.MOD_CODIGO,
                // MODP_IMPORTE = Convert.ToDecimal(txtIncomeAmount.Value),
                // MODP_POSITIVO = radSing.SelectedValue.Equals("+"),
                // USU_CODIGO = LoginUser.USU_CODIGO
                // };

                // var insert = this.creditModificationBudgetsService.InsertCreditModificationBudget(credictModificationBudget, year);

                // switch (insert.ResponseCode)
                // {
                // case ResponseCode.Invalid:
                // strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                // break;
                // case ResponseCode.Found:
                // strBuilder.Append("La Aplicación seleccionada ya se encuentra en la Modificación de Credito.");

                // break;
                // }

                // if (insert.ResponseCode != ResponseCode.Ok)
                // {
                // this.ShowMessage(this.RadNotification, "Error insertando la Modificación de Crédito", strBuilder, MessageType.Warning);
                // }
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
                var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
                var rdOperation = (RadDatePicker)item.FindControl("rdOperation");
                var rdProposal = (RadDatePicker)item.FindControl("rdProposal");
                var rdEffective = (RadDatePicker)item.FindControl("rdEffective");
                var txtDescription = (RadTextBox)item.FindControl("txtDescription");
                var radRestrictedAccount = (RadComboBox)item.FindControl("radDropRestrictedAccount");
                var checkBinding = (RadCheckBox)item.FindControl("checkBinding");
                var txtTonnageSheet = (RadNumericTextBox)item.FindControl("txtTonnageSheet");
                var rmyTonnageSheetYear = (RadMonthYearPicker)item.FindControl("rmyTonnageSheetYear");
                var txtTonnageSheet50 = (RadNumericTextBox)item.FindControl("txtTonnageSheet50");
                var rmyTonnageSheet50Year = (RadMonthYearPicker)item.FindControl("rmyTonnageSheet50Year");
                var txtCheck = (RadTextBox)item.FindControl("txtCheck");

                var year = ((DateTime)this.RmyYear.SelectedDate).Year;
                var documentType = Convert.ToInt32(radDocumentType.SelectedValue);
                //var numberValue = txtNumber.Value == null ? 0 : Convert.ToInt32(txtNumber.Value);
                var operationValue = (DateTime)rdOperation.SelectedDate;
                var proposalValue = rdProposal.SelectedDate ?? operationValue.AddDays(1);
                var effectiveValue = rdEffective.SelectedDate ?? operationValue.AddDays(2);
                var descriptionValue = string.IsNullOrWhiteSpace(txtDescription.Text) ? string.Empty : txtDescription.Text;
                var restrictedAccount = Convert.ToInt32(radRestrictedAccount.SelectedValue);
                var tonnageSheetValue = txtTonnageSheet.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet.Value);
                var tonnageSheetYearValue = rmyTonnageSheetYear.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheetYear.SelectedDate).Year);
                var tonnageSheet50Value = txtTonnageSheet50.Value == null ? (int?)null : Convert.ToInt32(txtTonnageSheet50.Value);
                var tonnageSheet50YearValue = rmyTonnageSheet50Year.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)rmyTonnageSheet50Year.SelectedDate).Year);
                var checkValue = string.IsNullOrWhiteSpace(txtCheck.Text) ? string.Empty : txtCheck.Text;

                int numberValue;

                if (txtNumber.Value != null)
                {
                    numberValue = Convert.ToInt32(txtNumber.Value);
                }
                else
                {
                    if (documentType == 3 || documentType == 4 || documentType == 5)
                    {
                        numberValue = this.accountingDocumentsService.GetLastMiNumber(year);
                    }
                    else
                    {
                        numberValue = 0;
                    }
                }

                //if (numberValue == 0 && (documentType == 3 || documentType == 4))
                //{
                //    numberValue = this.accountingDocumentsService.GetLastMiNumber(year);
                //}

                //if (numberValue == 0 && documentType == 5)
                //{
                //    numberValue = accountingRecordsService.GetLastYearNumber(year, "G");
                //}

                if (numberValue != 0)
                {
                    var number = this.accountingDocumentsService.CheckMiNumber(year, numberValue, documentType, id);

                    if (number > 0)
                    {
                        strBuilder.Append($"El número de ingreso {numberValue} para el año de ejercicio {year} ya existe.</br>Verifique que el año de ejercicio y el número de ingreso son correctos o deje el número de ingreso en blanco para que el sistema le asigne uno de forma automática.");
                        this.ShowMessage(this.RadNotification, "Atención", strBuilder, MessageType.Warning);

                        return;
                    }
                }

                var countTonnageSheet = 0;

                if (txtTonnageSheet.Value != null && rmyTonnageSheetYear.SelectedDate != null)
                {
                    countTonnageSheet = this.accountingDocumentsService.CheckTonnageSheet(((DateTime)rmyTonnageSheetYear.SelectedDate).Year, Convert.ToInt32(txtTonnageSheet.Value), false);
                }

                var countTonnageSheet50 = 0;

                if (txtTonnageSheet50.Value != null && rmyTonnageSheet50Year.SelectedDate != null)
                {
                    countTonnageSheet50 = this.accountingDocumentsService.CheckTonnageSheet(((DateTime)rmyTonnageSheet50Year.SelectedDate).Year, Convert.ToInt32(txtTonnageSheet50.Value), true);
                }

                if (countTonnageSheet > 0 || countTonnageSheet50 > 0)
                {
                    strBuilder.Append("El número de hoja de arqueo introducido ya existe. Cambie el número de hoja de arqueo.");
                    this.ShowMessage(this.RadNotification, "Atención", strBuilder, MessageType.Warning);

                    return;
                }

                var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                {
                    DOC_CODIGO = id,
                    DOC_I_G = "I",
                    DOC_FECHA_PROPUESTA = proposalValue,
                    DOC_FECHA_ASIENTO_DIARIO = effectiveValue,
                    DOC_NUMERO_CHEQUE = checkValue,
                    DOC_DESCRIPCION = descriptionValue,
                    DOC_NUMERO_MOVIMIENTO_I = numberValue,
                    DOC_FECHA_MOVIMIENTO_I = operationValue,
                    EXP_CODIGO = this.AccountingFile.EXP_CODIGO,
                    TIPD_CODIGO = documentType,
                    CUE_CODIGO = restrictedAccount,
                    DOC_ENLAZADO_TESORERIA = (bool)checkBinding.Checked,
                    HOJ_NUMERO = tonnageSheetValue,
                    HOJ_NUMERO50 = tonnageSheet50Value,
                    ANO_HOJA = tonnageSheetYearValue,
                    ANO_HOJA50 = tonnageSheet50YearValue,
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
                    if ((tonnageSheetValue != null && tonnageSheetYearValue != null) || (tonnageSheet50Value != null && tonnageSheetYearValue != null))
                    {
                        var updateTonnageSheet = this.accountingDocumentsService.UpdateTonnageSheet(tonnageSheetValue, tonnageSheetYearValue, tonnageSheet50Value, tonnageSheetYearValue, restrictedAccount, LoginUser.USU_CODIGO);
                    }
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
            var item = e.Item as GridDataItem;

            var id = (int)item.GetDataKeyValue("DOC_CODIGO");

            try
            {
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
                LogError(ex, "Imposible eliminar documento contable.");
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
                
                var documentType = (string)item.GetDataKeyValue("TIPO_DOC");

                var script = $"deleteIncomeRecord('{item.ItemIndex}', '{documentType}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "deleteIncomeRecord", script, true);
            }

            if (e.CommandName == "UpdateApplications")
            {
                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
                var documentType = (string)item.GetDataKeyValue("TIPO_DOC");
                var documentText = (string)item.GetDataKeyValue("DOC_DESCRIPCION");
                var documentTitle = string.IsNullOrWhiteSpace(documentText) ? documentType.Replace("+", ":") : $"{documentType.Replace("+", ":")} | {documentText}";
                var year = ((DateTime)this.RmyYear.SelectedDate).Year;

                var script = $"updateApplications('{documentId}', '{year}', '{this.AccountingFile.EXP_CODIGO}', '{documentTitle}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "updateApplications", script, true);
            }

            if (e.CommandName == "PrintApplications")
            {
                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
                var documentType = (string)item.GetDataKeyValue("TIPO_DOC");

                var report = string.Empty;

                if (documentType.Contains("MI") || documentType.Contains("R-") || documentType.Contains("R+"))
                {
                    report = "incomeMi";
                }

                if (documentType == "400 PMP")
                {
                    report = "incomePmp";
                }

                if (string.IsNullOrWhiteSpace(report))
                {
                    return;
                }

                var script = $"printApplications('{documentId}', '{report}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "printApplications", script, true);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "moveNewButtons", "moveNewButtons();", true);

            // if (this.CreditModification.MOD_CODIGO != 0)
            // {
            // if (!(bool)this.CreditModification.MOD_EJECUTADA)
            // {
            // if (this.CreditModification.MOD_CREADO_EXPEDIENTE == false)
            // {
            // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "moveNewButtons", "moveNewButtons();", true);
            // }

            // this.ValidateButtons();
            // }
            // else
            // {
            // this.btnGenerateFile.Enabled = false;
            // this.btnExecute.Enabled = false;
            // }
            // }
            // else
            // {
            // this.btnGenerateFile.Enabled = false;
            // this.btnExecute.Enabled = false;
            // }
        }

        protected void TxtTonnageSheet_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            try
            {
                // Obtén el año seleccionado del RadMonthYearPicker
                RadMonthYearPicker picker = sender as RadMonthYearPicker;
                if (picker == null || !picker.SelectedDate.HasValue)
                {
                    return; // Si no hay fecha seleccionada, salir
                }

                int selectedYear = picker.SelectedDate.Value.Year;

                // Llama a un servicio o método para obtener el último número de arqueo del año
                int? lastTonnageSheetNumber = this.accountingDocumentsService.GetNextTonnageSheetNumber(selectedYear);

                // Función anónima para buscar el control recursivamente
                Func<Control, string, Control> findControlRecursive = null;
                findControlRecursive = (root, id) =>
                {
                    if (root.ID == id)
                    {
                        return root;
                    }

                    foreach (Control child in root.Controls)
                    {
                        Control found = findControlRecursive(child, id);
                        if (found != null)
                        {
                            return found;
                        }
                    }

                    return null;
                };

                // Busca el control txtTonnageSheet
                RadNumericTextBox txtTonnageSheet = findControlRecursive(this, "txtTonnageSheet") as RadNumericTextBox;
                if (txtTonnageSheet != null && lastTonnageSheetNumber.HasValue)
                {
                    txtTonnageSheet.Value = lastTonnageSheetNumber.Value;
                }
            }
            catch (Exception ex)
            {
                // Maneja errores y registra si es necesario
                System.Diagnostics.Debug.WriteLine("Error en RadMonthYearPicker1_SelectedDateChanged: " + ex.Message);
            }
        }


        protected void TxtTonnageSheet50_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            try
            {
                // Obtén el año seleccionado del RadMonthYearPicker
                RadMonthYearPicker picker = sender as RadMonthYearPicker;
                if (picker == null || !picker.SelectedDate.HasValue)
                {
                    return; // Si no hay fecha seleccionada, salir
                }

                int selectedYear = picker.SelectedDate.Value.Year;

                // Llama a un servicio o método para obtener el último número de arqueo del año
                int? lastTonnageSheetNumber = this.accountingDocumentsService.GetNextTonnageSheetNumber50(selectedYear);

                // Función anónima para buscar el control recursivamente
                Func<Control, string, Control> findControlRecursive = null;
                findControlRecursive = (root, id) =>
                {
                    if (root.ID == id)
                    {
                        return root;
                    }

                    foreach (Control child in root.Controls)
                    {
                        Control found = findControlRecursive(child, id);
                        if (found != null)
                        {
                            return found;
                        }
                    }

                    return null;
                };

                // Busca el control txtTonnageSheet50
                RadNumericTextBox txtTonnageSheet50 = findControlRecursive(this, "txtTonnageSheet50") as RadNumericTextBox;
                if (txtTonnageSheet50 != null && lastTonnageSheetNumber.HasValue)
                {
                    txtTonnageSheet50.Value = lastTonnageSheetNumber.Value;
                }
            }
            catch (Exception ex)
            {
                // Maneja errores y registra si es necesario
                System.Diagnostics.Debug.WriteLine("Error en TxtTonnageSheet50_SelectedDateChanged: " + ex.Message);
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteAccountingRecord()
        {
            try
            {
                var id = ((PRE_EXPEDIENTE_CONTABLE)HttpContext.Current.Session["_accountingFile"]).EXP_CODIGO;

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
                LogError(ex, "Error eliminando el expediente contable.");
                return 0;
            }
        }


        #endregion


    }
}