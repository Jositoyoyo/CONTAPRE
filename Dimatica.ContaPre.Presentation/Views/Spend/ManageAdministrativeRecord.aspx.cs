namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
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

    public partial class ManageAdministrativeRecord : BasePage
    {
        #region Static Fields and Constants

        private static IAdministrativeRecordsService administrativeRecordsService = DependencyFactory.GetInstance<IAdministrativeRecordsService>();

        #endregion

        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        private IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        private IContractTypesService contractTypesService = DependencyFactory.GetInstance<IContractTypesService>();

        private IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Properties

        private PRE_EXPEDIENTE_ADMINISTRATIVO AdministrativeRecord
        {
            get
            {
                var administrativeRecord = this.Session["_administrativeRecord"] as PRE_EXPEDIENTE_ADMINISTRATIVO;

                if (administrativeRecord == null)
                {
                    administrativeRecord = new PRE_EXPEDIENTE_ADMINISTRATIVO();

                    this.Session["_administrativeRecord"] = administrativeRecord;
                }

                return administrativeRecord;
            }

            set
            {
                this.Session["_administrativeRecord"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteAdministrativeRecord()
        {
            try
            {
                var id = ((PRE_EXPEDIENTE_ADMINISTRATIVO)HttpContext.Current.Session["_administrativeRecord"]).EA_CODIGO;

                if (id == 0)
                {
                    return 3;
                }

                var delete = administrativeRecordsService.DeleteAdministrativeRecord(id, LoginUser.USU_CODIGO);

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
                LogError(ex, "Error eliminando el expediente administrativo.");
                return 0;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var administrativeRecordId = this.Request.QueryString["id"];

            if (administrativeRecordId == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideGrids", $"hideGrids();", true);
            }

            if (!this.IsPostBack)
            {
                this.AdministrativeRecord = null;

                var provenances = this.provenancesService.GetProvenances("G");
                provenances.Insert(
                                   0,
                                   new PRE_PROCEDENCIA
                                           {
                                                   PROC_CODIGO = -1,
                                                   PROC_DESCRIPCION = "< Seleccione >"
                                           });

                this.rcProvenances.DataSource = provenances;
                this.rcProvenances.DataBind();

                var types = this.contractTypesService.GetContractTypes();
                types.Insert(
                             0,
                             new PRE_TIPO_CONTRATO
                                     {
                                             TIPC_CODIGO_AUX = -1,
                                             TIPC_DESCRIPCION = "< Seleccione >"
                                     });

                this.rcType.DataSource = types;
                this.rcType.DataBind();

                if (administrativeRecordId == null)
                {
                    this.titleHeader.InnerText = "Nuevo Expediente Administrativo";
                    this.rmyYear.SelectedDate = DateTime.Now;
                    this.btnDelete.Enabled = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideGrids", $"hideGrids();", true);
                }
                else
                {
                    this.titleHeader.InnerText = "Editar Expediente Administrativo";
                    var administrativeRecord = administrativeRecordsService.GetById(Convert.ToInt32(administrativeRecordId));

                    if (administrativeRecord == null)
                    {
                        this.Response.Redirect("~/Views/Spend/ManageAdministrativeRecord.aspx");
                    }
                    else
                    {
                        this.txtOrder.Enabled = false;
                        this.rmyYear.Enabled = false;
                        this.rcProvenances.Enabled = false;

                        this.AdministrativeRecord = administrativeRecord;

                        if (this.AdministrativeRecord.EA_NUMERO != null)
                        {
                            this.txtOrder.Value = Convert.ToDouble(this.AdministrativeRecord.EA_NUMERO);
                        }

                        this.rmyYear.SelectedDate = new DateTime(Convert.ToInt32(this.AdministrativeRecord.EA_ANO_EJERCICIO), 1, 1);

                        if (this.AdministrativeRecord.PROC_CODIGO != null)
                        {
                            this.rcProvenances.SelectedValue = this.AdministrativeRecord.PROC_CODIGO.ToString();
                        }

                        if (this.AdministrativeRecord.TIPC_CODIGO != null)
                        {
                            this.rcType.SelectedValue = this.AdministrativeRecord.TIPC_CODIGO.ToString();
                        }

                        this.txtDescription.Text = this.AdministrativeRecord.EA_DESCRIPCION;
                    }
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            if (this.AdministrativeRecord.EA_CODIGO == 0)
            {
                var administrativeRecord = new PRE_EXPEDIENTE_ADMINISTRATIVO
                                                   {
                                                           EA_NUMERO = this.txtOrder.Value == null ? (int?)null : Convert.ToInt32(this.txtOrder.Value),
                                                           EA_ANO_EJERCICIO = Convert.ToInt16(((DateTime)this.rmyYear.SelectedDate).Year),
                                                           PROC_CODIGO = this.rcProvenances.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.rcProvenances.SelectedValue),
                                                           TIPC_CODIGO = this.rcType.SelectedValue == "-1" ? (byte?)null : Convert.ToByte(this.rcType.SelectedValue),
                                                           EA_DESCRIPCION = string.IsNullOrWhiteSpace(this.txtDescription.Text) ? string.Empty : this.txtDescription.Text,
                                                           USU_CODIGO = LoginUser.USU_CODIGO
                                                   };

                try
                {
                    var insert = administrativeRecordsService.InsertAdministrativeRecord(administrativeRecord);

                    switch (insert.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos del Expediente Administrativo.");

                            break;
                    }

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error insertando el Expediente Administrativo", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        var id = (int)insert.ResponseMethod;
                        this.Response.Redirect($"~/Views/Spend/ManageAdministrativeRecord.aspx?id={id}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error insertando el Expediente Administrativo.");
                    strBuilder.Append("Ha ocurrido un error insertando el Expediente Administrativo en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error insertando el Expediente Administrativo", strBuilder, MessageType.Deny);
                }
            }
            else
            {
                var administrativeRecord = new PRE_EXPEDIENTE_ADMINISTRATIVO
                                                   {
                                                           EA_CODIGO = this.AdministrativeRecord.EA_CODIGO,
                                                           EA_NUMERO = this.txtOrder.Value == null ? (int?)null : Convert.ToInt32(this.txtOrder.Value),
                                                           EA_ANO_EJERCICIO = Convert.ToInt16(((DateTime)this.rmyYear.SelectedDate).Year),
                                                           PROC_CODIGO = this.rcProvenances.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.rcProvenances.SelectedValue),
                                                           TIPC_CODIGO = this.rcType.SelectedValue == "-1" ? (byte?)null : Convert.ToByte(this.rcType.SelectedValue),
                                                           EA_DESCRIPCION = string.IsNullOrWhiteSpace(this.txtDescription.Text) ? string.Empty : this.txtDescription.Text,
                                                           USU_CODIGO = LoginUser.USU_CODIGO
                                                   };

                try
                {
                    var update = administrativeRecordsService.UpdateAdministrativeRecord(administrativeRecord);

                    switch (update.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos de el Expediente Administrativo.");

                            break;
                        case ResponseCode.NotFound:
                            strBuilder.Append("No se ha encontrado el Expediente Administrativo en cuestión.");

                            break;
                    }

                    if (update.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error modificando el Expediente Administrativo", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        this.Response.Redirect($"~/Views/Spend/ManageAdministrativeRecord.aspx?id={this.AdministrativeRecord.EA_CODIGO}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error modificando el Expediente Administrativo.");
                    strBuilder.Append("Ha ocurrido un error modificando el Expediente Administrativo en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error modificando el Expediente Administrativo", strBuilder, MessageType.Deny);
                }
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var message = $"¿ Desea realmente eliminar el expediente administrativo {this.AdministrativeRecord.EA_NUMERO}";

            if (this.rcProvenances.SelectedValue != "-1")
            {
                message = $"{message} de {this.rcProvenances.SelectedItem.Text}";
            }

            message = $"{message} del año {this.AdministrativeRecord.EA_ANO_EJERCICIO} ?<br/><br/>Se eliminarán todos los datos asociados a dicho expediente:<br/>- Expedientes contables asociados.<br/>- Documentos contables y toda su información asociada.";

            this.rwmManageAdministrativeRecord.RadConfirm(message, "confirmDeleteCallBackFn", 330, 140, null, "Confirmación");
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Spend/AdministrativeRecords.aspx");
        }

        protected void RgAdministrativeRecords_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var accountingRecords = new List<PRE_EXPEDIENTE_CONTABLE>();

            if (this.AdministrativeRecord.EA_CODIGO != 0)
            {
                accountingRecords = this.accountingRecordsService.GetByAdministrativeRecord(this.AdministrativeRecord.EA_CODIGO);
            }

            this.RgAdministrativeRecords.DataSource = accountingRecords;
        }

        protected void RgAdministrativeRecords_OnPreRender(object sender, EventArgs e) { }

        protected void RgAdministrativeRecords_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var rcYears = (RadComboBox)item.FindControl("rcYears"); 
                var years = this.budgetsService.GetYearsByType("G");
                rcYears.DataSource = years;
                rcYears.DataBind();

                var radDropProviders = (RadComboBox)item.FindControl("radDropProviders");
                var providers = this.providersService.GetProvidersToCombo();
                providers.Insert(
                                 0,
                                 new PRE_PROVEEDOR
                                         {
                                                 PROV_CODIGO = -1,
                                                 PROV_NOMBRE = "< Seleccione >"
                                         });
                radDropProviders.DataSource = providers;
                radDropProviders.DataBind();

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgAdministrativeRecords.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
            }
        }

        protected void RgAdministrativeRecords_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;
                var rcYears = (RadComboBox)item.FindControl("rcYears");
                var radDropProviders = (RadComboBox)item.FindControl("radDropProviders");

                var administrativeYear = this.AdministrativeRecord.EA_ANO_EJERCICIO;
                var year = Convert.ToInt16(rcYears.SelectedValue);
                var accountingNumber = this.accountingRecordsService.GetLastYearNumber(year, "G");
                var programCode = this.budgetsService.GetProgramCodeByYear(year);

                if (programCode == 0)
                {
                    strBuilder.Append($"No se puede crear el Expediente Contable debido a que no existe presupuesto de gastos en el {year}.");
                    this.ShowMessage(this.RadNotification, "Imposible crear Expediente Contable", strBuilder, MessageType.Warning);
                    return;
                }

                var accountingRecord = new PRE_EXPEDIENTE_CONTABLE
                                               {
                                                       EA_CODIGO = this.AdministrativeRecord.EA_CODIGO,
                                                       EXP_I_G = "G",
                                                       PRO_CODIGO = programCode != 0 ? (byte?)null : Convert.ToByte(programCode),
                                                       EXP_DESCRIPCION = this.AdministrativeRecord.EA_DESCRIPCION,
                                                       EXP_PLURIANUAL = administrativeYear != year,
                                                       EXP_CUADRADO = false,
                                                       EXP_ANO_PRESUPUESTO = year,
                                                       EXP_NUM_EXP_CONTABLE_ANUAL = accountingNumber,
                                                       PROV_CODIGO = radDropProviders.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(radDropProviders.SelectedValue),
                                                       PROC_CODIGO = this.AdministrativeRecord.PROC_CODIGO,
                                                       USU_CODIGO = LoginUser.USU_CODIGO
                                               };

                var insert = this.accountingRecordsService.InsertAccountingRecord(accountingRecord);

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
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Expediente Contable.");
                strBuilder.Append("Ha ocurrido un error insertando el Expediente Contable en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Expediente Contable", strBuilder, MessageType.Deny);
            }
        }

        protected void RgAdministrativeRecords_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateSpendRecord")
            {
                var item = e.Item as GridDataItem;
                var recordId = (int)item.GetDataKeyValue("EXP_CODIGO");

                this.Session["_currentSource"] = this.Request.Url.AbsoluteUri;
                this.Response.Redirect($"~/Views/Spend/ManageSpendRecord.aspx?id={recordId}");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.AdministrativeRecord.EA_CODIGO != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "moveNewButtons", "moveNewButtons();", true);
            }
        }

        protected void rmyYear_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (e.NewDate == null)
            {
                this.txtOrder.Value = null;
            }
            else
            {
                var year = e.NewDate?.Year;
                var provenance = this.rcProvenances.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.rcProvenances.SelectedValue);
                this.FillOrder(year, provenance);
            }
        }

        protected void rcProvenances_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var provenance = e.Value.Equals("-1") ? (int?)null : Convert.ToInt32(e.Value);

            if (provenance == null)
            {
                this.txtOrder.Value = null;
            }
            else
            {
                var year = this.rmyYear.SelectedDate?.Year;
                this.FillOrder(year, provenance);
            }
        }

        private void FillOrder(int? year, int? provenance)
        {
            if (year == null || provenance == null)
            {
                this.txtOrder.Value = null;
            }
            else
            {
                var order = administrativeRecordsService.GetNextOrder((int)year, (int)provenance);
                this.txtOrder.Value = Convert.ToDouble(order);
            }
        }

        #endregion
    }
}