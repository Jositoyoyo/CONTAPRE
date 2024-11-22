namespace Dimatica.ContaPre.Presentation.Views.Budget
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

    public partial class ManageCreditModification : BasePage
    {
        #region Static Fields and Constants

        private static ICreditModificationsService creditModificationsService = DependencyFactory.GetInstance<ICreditModificationsService>();

        #endregion

        #region Fields

        IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        IBudgetApplicationsService budgetApplicationsService = DependencyFactory.GetInstance<IBudgetApplicationsService>();

        ICostPlacesService costPlacesService = DependencyFactory.GetInstance<ICostPlacesService>();

        ICreditModificationBudgetsService creditModificationBudgetsService = DependencyFactory.GetInstance<ICreditModificationBudgetsService>();

        ICreditModificationTypesService creditModificationTypesService = DependencyFactory.GetInstance<ICreditModificationTypesService>();

        IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        #endregion

        #region Private Properties

        private PRE_MODIFICACION_CREDITO CreditModification
        {
            get
            {
                var creditModification = this.Session["_creditModification"] as PRE_MODIFICACION_CREDITO;

                if (creditModification == null)
                {
                    creditModification = new PRE_MODIFICACION_CREDITO();

                    this.Session["_creditModification"] = creditModification;
                }

                return creditModification;
            }

            set
            {
                this.Session["_creditModification"] = value;
            }
        }

        #endregion


        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var creditModificationId = this.Request.QueryString["id"];

            if (creditModificationId == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideGrids", $"hideGrids();", true);
            }

            if (!this.IsPostBack)
            {
                this.CreditModification = null;

                var incomesTypes = this.creditModificationTypesService.GetCreditModificationTypes("I");
                this.rcModificationTypeIncomes.DataSource = incomesTypes;
                this.rcModificationTypeIncomes.DataBind();

                var spendsTypes = this.creditModificationTypesService.GetCreditModificationTypes("G");
                this.rcModificationTypeSpends.DataSource = spendsTypes;
                this.rcModificationTypeSpends.DataBind();

                if (creditModificationId == null)
                {
                    this.titleHeader.InnerText = "Nueva Modificación";
                    this.dateCreated.SelectedDate = DateTime.Now;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideGrids", $"hideGrids();", true);
                }
                else
                {
                    this.titleHeader.InnerText = "Editar Modificación";
                    var creditModification = creditModificationsService.GetById(Convert.ToInt32(creditModificationId));

                    if (creditModification == null)
                    {
                        this.Response.Redirect("~/Views/Budget/ManageCreditModification.aspx");
                    }
                    else
                    {
                        this.CreditModification = creditModification;
                        this.rmyYear.SelectedDate = new DateTime(Convert.ToInt32(this.CreditModification.MOD_ANO_PRESUPUESTO), 1, 1);
                        this.txtOrder.Value = Convert.ToDouble(this.CreditModification.MOD_NUMERO_ORDEN);
                        this.dateProposal.SelectedDate = this.CreditModification.MOD_FECHA_PROPUESTA;
                        this.dateEfective.SelectedDate = this.CreditModification.MOD_FECHA_ASIENTO_DIARIO;
                        this.rcModificationTypeIncomes.SelectedValue = this.CreditModification.TIPM_CODIGO_I.ToString();
                        this.rcModificationTypeSpends.SelectedValue = this.CreditModification.TIPM_CODIGO_G.ToString();
                        this.txtDescription.Text = this.CreditModification.MOD_DESCRIPCION;
                        this.dateCreated.SelectedDate = this.CreditModification.MOD_FECHA_MODIFICACION;

                        this.rmyYear.Enabled = false;

                        if ((bool)this.CreditModification.MOD_EJECUTADA)
                        {
                            this.dateProposal.Enabled = false;
                            this.dateEfective.Enabled = false;
                            this.rcModificationTypeIncomes.Enabled = false;
                            this.rcModificationTypeSpends.Enabled = false;
                            this.txtDescription.Enabled = false;
                            this.dateCreated.Enabled = false;

                            this.btnSave.Enabled = false;
                            this.btnGenerateFile.Enabled = false;
                            this.btnExecute.Enabled = false;

                            this.txtStatus.InnerText = "Ejecutada";
                            this.txtStatus.Style.Add("color", "red");
                        }
                    }
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            if (this.CreditModification.MOD_CODIGO == 0)
            {
                var creditModification = new PRE_MODIFICACION_CREDITO
                {
                    MOD_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.rmyYear.SelectedDate).Year),
                    MOD_NUMERO_ORDEN = Convert.ToInt32(txtOrder.Value),
                    MOD_FECHA_PROPUESTA = dateProposal.SelectedDate,
                    MOD_FECHA_ASIENTO_DIARIO = dateEfective.SelectedDate,
                    MOD_DESCRIPCION = txtDescription.Text,
                    TIPM_CODIGO_I = Convert.ToInt32(rcModificationTypeIncomes.SelectedValue),
                    TIPM_CODIGO_G = Convert.ToInt32(rcModificationTypeSpends.SelectedValue),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                try
                {
                    var insert = creditModificationsService.InsertCreditModification(creditModification);

                    switch (insert.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                            break;
                        case ResponseCode.Found:
                            strBuilder.Append("Ya existe una Modificación de Crédito con ese Orden en ese Año.");

                            break;
                    }

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error insertando la Modificación de Crédito", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        var id = (int)insert.ResponseMethod;
                        this.Response.Redirect($"~/Views/Budget/ManageCreditModification.aspx?id={id}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error insertando la Modificación de Crédito.");
                    strBuilder.Append("Ha ocurrido un error insertando la Modificación de Crédito en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error insertando la Modificación de Crédito", strBuilder, MessageType.Deny);
                }
            }
            else
            {
                var creditModification = new PRE_MODIFICACION_CREDITO
                {
                    MOD_CODIGO = this.CreditModification.MOD_CODIGO,
                    MOD_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.rmyYear.SelectedDate).Year),
                    MOD_NUMERO_ORDEN = Convert.ToInt32(txtOrder.Value),
                    MOD_FECHA_PROPUESTA = dateProposal.SelectedDate,
                    MOD_FECHA_ASIENTO_DIARIO = dateEfective.SelectedDate,
                    MOD_DESCRIPCION = txtDescription.Text,
                    TIPM_CODIGO_I = Convert.ToInt32(rcModificationTypeIncomes.SelectedValue),
                    TIPM_CODIGO_G = Convert.ToInt32(rcModificationTypeSpends.SelectedValue),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                try
                {
                    var update = creditModificationsService.UpdateCreditModification(creditModification);

                    switch (update.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                            break;
                        case ResponseCode.NotFound:
                            strBuilder.Append("No se ha encontrado la Modificación de Crédito en cuestión.");

                            break;
                        case ResponseCode.Found:
                            strBuilder.Append("Ya existe una Modificación de Crédito con ese Orden en ese Año.");

                            break;
                    }

                    if (update.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error modificando la Modificación de Crédito", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        this.Response.Redirect($"~/Views/Budget/ManageCreditModification.aspx?id={this.CreditModification.MOD_CODIGO}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error modificando el Tipo de Modificación de Crédito.");
                    strBuilder.Append("Ha ocurrido un error modificando la Modificación de Crédito en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error modificando el Tipo de Modificación de Crédito", strBuilder, MessageType.Deny);
                }
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Budget/CreditModifications.aspx");
        }

        protected void RmyYear_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (e.NewDate == null)
            {
                this.txtOrder.Value = null;
            }
            else
            {
                var year = e.NewDate?.Year;

                if (this.CreditModification.MOD_CODIGO != 0)
                {
                    if (this.CreditModification.MOD_ANO_PRESUPUESTO == Convert.ToInt16(year))
                    {
                        this.txtOrder.Value = Convert.ToDouble(this.CreditModification.MOD_NUMERO_ORDEN);

                        return;
                    }
                }

                var order = creditModificationsService.GetNextOrderByYear((int)year);

                this.txtOrder.Value = Convert.ToDouble(order);
            }
        }

        protected void RgIncomeApplications_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var creditModifications = new List<PRE_MODIF_CREDITO_PRESUPUESTO>();

            if (this.CreditModification.MOD_CODIGO != 0)
            {
                creditModifications = this.creditModificationBudgetsService.GetByCreditModification(this.CreditModification.MOD_CODIGO, "I");
            }

            this.RgIncomeApplications.DataSource = creditModifications;

            decimal total = 0;

            foreach (var cm in creditModifications)
            {
                var value = (decimal)cm.MODP_IMPORTE;

                if (cm.MODP_POSITIVO == false)
                {
                    value = value * -1;
                }

                total += value;
            }

            this.txtTotalIncomes.Text = total.ToString("N");
        }

        protected void RgIncomeApplications_OnPreRender(object sender, EventArgs e)
        {
            if (this.CreditModification.MOD_CODIGO != 0)
            {
                if ((bool)this.CreditModification.MOD_EJECUTADA || (bool)this.CreditModification.MOD_CREADO_EXPEDIENTE)
                {
                    this.RgIncomeApplications.MasterTableView.GetColumn("EditColumn_Income").Visible = false;
                    this.RgIncomeApplications.MasterTableView.GetColumn("DeleteColumn_Income").Visible = false;
                }
            }
        }

        protected void RgIncomeApplications_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radApplications = (RadComboBox)item.FindControl("radDropIncomeAplication");

                var year = ((DateTime)this.rmyYear.SelectedDate).Year;

                var incomeApplications = this.budgetApplicationsService.GetNumbersByTypeByYear("I", year);
                radApplications.DataSource = incomeApplications.ToList();
                radApplications.DataBind();

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgIncomeApplications.MasterTableView.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var budgetId = item.GetDataKeyValue("CACS_CODIGO");
                    radApplications.SelectedValue = budgetId.ToString();

                    var txtAmount = (RadNumericTextBox)item.FindControl("txtIncomeAmount");
                    var amount = item.GetDataKeyValue("MODP_IMPORTE").ToString();
                    txtAmount.Value = Convert.ToDouble(amount);

                    var radDropSing = (RadComboBox)item.FindControl("radDropIncomeSing");
                    var sing = item.GetDataKeyValue("SingLabel").ToString();
                    radDropSing.SelectedValue = sing;
                }
            }
        }

        protected void RgIncomeApplications_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;
                var radApplications = (RadComboBox)item.FindControl("radDropIncomeAplication");
                var cacsCode = radApplications.SelectedValue;

                var error = false;

                if (cacsCode.EndsWith("/"))
                {
                    var count = radApplications.Items.Count(i => i.Value.Contains(cacsCode));

                    if (count > 1)
                    {
                        strBuilder.Append("La Aplicación tiene subconceptos por lo que no se puede seleccionar. Si lo desea, puede seleccionar uno de dichos subconceptos. ");
                        error = true;
                    }
                }

                foreach (GridDataItem itemVerify in this.RgIncomeApplications.MasterTableView.Items)
                {
                    var code = itemVerify.GetDataKeyValue("CACS_CODIGO").ToString();

                    if (!code.Equals(cacsCode))
                    {
                        continue;
                    }

                    strBuilder.Append("La Aplicación seleccionada ya se encuentra en la Modificación de Credito. ");
                    error = true;

                    break;
                }

                var txtIncomeAmount = (RadNumericTextBox)item.FindControl("txtIncomeAmount");

                if (txtIncomeAmount.Value == null || txtIncomeAmount.Value == 0)
                {
                    strBuilder.Append("El importe de la Aplicación tiene que tener un valor mayor que 0. ");
                    error = true;
                }

                if (error)
                {
                    this.ShowMessage(this.RadNotification, "Imposible agregar Aplicación", strBuilder, MessageType.Warning);
                    e.Canceled = true;

                    return;
                }

                var year = ((DateTime)this.rmyYear.SelectedDate).Year;
                var radSing = (RadComboBox)item.FindControl("radDropIncomeSing");

                var credictModificationBudget = new PRE_MODIF_CREDITO_PRESUPUESTO
                {
                    CACS_CODIGO = cacsCode,
                    MODP_I_G = "I",
                    MOD_CODIGO = this.CreditModification.MOD_CODIGO,
                    MODP_IMPORTE = Convert.ToDecimal(txtIncomeAmount.Value),
                    MODP_POSITIVO = radSing.SelectedValue.Equals("+"),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var insert = this.creditModificationBudgetsService.InsertCreditModificationBudget(credictModificationBudget, year);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("La Aplicación seleccionada ya se encuentra en la Modificación de Credito.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando la Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error insertando la Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando la Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgIncomeApplications_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;

                var error = false;

                var txtIncomeAmount = (RadNumericTextBox)item.FindControl("txtIncomeAmount");

                if (txtIncomeAmount.Value == null || txtIncomeAmount.Value == 0)
                {
                    strBuilder.Append("El importe de la Aplicación tiene que tener un valor mayor que 0. ");
                    error = true;
                }

                if (error)
                {
                    this.ShowMessage(this.RadNotification, "Imposible aditar Aplicación", strBuilder, MessageType.Warning);
                    e.Canceled = true;

                    return;
                }

                var year = ((DateTime)this.rmyYear.SelectedDate).Year;
                var radSing = (RadComboBox)item.FindControl("radDropIncomeSing");

                var budgetId = (int)item.GetDataKeyValue("PRE_CODIGO");

                var credictModificationBudget = new PRE_MODIF_CREDITO_PRESUPUESTO
                {
                    PRE_CODIGO = budgetId,
                    MODP_I_G = "I",
                    MOD_CODIGO = this.CreditModification.MOD_CODIGO,
                    MODP_IMPORTE = Convert.ToDecimal(txtIncomeAmount.Value),
                    MODP_POSITIVO = radSing.SelectedValue.Equals("+"),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var insert = this.creditModificationBudgetsService.UpdateCreditModificationBudget(credictModificationBudget, year);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Modificación de Credito para poder editarla.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error modificando la Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgIncomeApplications_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var budgetId = (int)item.GetDataKeyValue("PRE_CODIGO");
            var creditModificationId = (int)item.GetDataKeyValue("MOD_CODIGO");

            try
            {
                var delete = this.creditModificationBudgetsService.DeleteCreditModificationBudget(budgetId, creditModificationId, "I", LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Modificación de Crédito en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Imposible eliminar Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error eliminando la Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgSpendApplications_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var creditModifications = new List<PRE_MODIF_CREDITO_PRESUPUESTO>();

            if (this.CreditModification.MOD_CODIGO != 0)
            {
                creditModifications = this.creditModificationBudgetsService.GetByCreditModification(this.CreditModification.MOD_CODIGO, "G");
            }

            this.RgSpendApplications.DataSource = creditModifications;

            decimal total = 0;

            foreach (var cm in creditModifications)
            {
                var value = (decimal)cm.MODP_IMPORTE;

                if (cm.MODP_POSITIVO == false)
                {
                    value = value * -1;
                }

                total += value;
            }

            this.txtTotalSpends.Text = total.ToString("N");
        }

        protected void RgSpendApplications_OnPreRender(object sender, EventArgs e)
        {
            if (this.CreditModification.MOD_CODIGO != 0)
            {
                if ((bool)this.CreditModification.MOD_EJECUTADA || (bool)this.CreditModification.MOD_CREADO_EXPEDIENTE)
                {
                    this.RgSpendApplications.MasterTableView.GetColumn("EditColumn_Spend").Visible = false;
                    this.RgSpendApplications.MasterTableView.GetColumn("DeleteColumn_Spend").Visible = false;
                }
            }
        }

        protected void RgSpendApplications_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radApplications = (RadComboBox)item.FindControl("radDropSpendAplication");

                var year = ((DateTime)this.rmyYear.SelectedDate).Year;

                var spendApplications = this.budgetApplicationsService.GetNumbersByTypeByYear("G", year);
                radApplications.DataSource = spendApplications.ToList();
                radApplications.DataBind();

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgSpendApplications.MasterTableView.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var budgetId = item.GetDataKeyValue("CACS_CODIGO");
                    radApplications.SelectedValue = budgetId.ToString();

                    var txtAmount = (RadNumericTextBox)item.FindControl("txtSpendAmount");
                    var amount = item.GetDataKeyValue("MODP_IMPORTE").ToString();
                    txtAmount.Value = Convert.ToDouble(amount);

                    var radDropSing = (RadComboBox)item.FindControl("radDropSpendSing");
                    var sing = item.GetDataKeyValue("SingLabel").ToString();
                    radDropSing.SelectedValue = sing;
                }
            }
        }

        protected void RgSpendApplications_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;
                var radApplications = (RadComboBox)item.FindControl("radDropSpendAplication");
                var cacsCode = radApplications.SelectedValue;

                var error = false;

                if (cacsCode.EndsWith("/"))
                {
                    var count = radApplications.Items.Count(i => i.Value.Contains(cacsCode));

                    if (count > 1)
                    {
                        strBuilder.Append("La Aplicación tiene subconceptos por lo que no se puede seleccionar. Si lo desea, puede seleccionar uno de dichos subconceptos. ");
                        error = true;
                    }
                }

                foreach (GridDataItem itemVerify in this.RgSpendApplications.MasterTableView.Items)
                {
                    var code = itemVerify.GetDataKeyValue("CACS_CODIGO").ToString();

                    if (!code.Equals(cacsCode))
                    {
                        continue;
                    }

                    strBuilder.Append("La Aplicación seleccionada ya se encuentra en la Modificación de Credito. ");
                    error = true;

                    break;
                }

                var txtSpendAmount = (RadNumericTextBox)item.FindControl("txtSpendAmount");

                if (txtSpendAmount.Value == null || txtSpendAmount.Value == 0)
                {
                    strBuilder.Append("El importe de la Aplicación tiene que tener un valor mayor que 0. ");
                    error = true;
                }

                if (error)
                {
                    this.ShowMessage(this.RadNotification, "Imposible agregar Aplicación", strBuilder, MessageType.Warning);
                    e.Canceled = true;

                    return;
                }

                var year = ((DateTime)this.rmyYear.SelectedDate).Year;
                var radSing = (RadComboBox)item.FindControl("radDropSpendSing");

                var credictModificationBudget = new PRE_MODIF_CREDITO_PRESUPUESTO
                {
                    CACS_CODIGO = cacsCode,
                    MODP_I_G = "G",
                    MOD_CODIGO = this.CreditModification.MOD_CODIGO,
                    MODP_IMPORTE = Convert.ToDecimal(txtSpendAmount.Value),
                    MODP_POSITIVO = radSing.SelectedValue.Equals("+"),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var insert = this.creditModificationBudgetsService.InsertCreditModificationBudget(credictModificationBudget, year);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("La Aplicación seleccionada ya se encuentra en la Modificación de Credito.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando la Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error insertando la Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando la Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgSpendApplications_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridEditFormItem)e.Item;

                var error = false;

                var txtSpendAmount = (RadNumericTextBox)item.FindControl("txtSpendAmount");

                if (txtSpendAmount.Value == null || txtSpendAmount.Value == 0)
                {
                    strBuilder.Append("El importe de la Aplicación tiene que tener un valor mayor que 0. ");
                    error = true;
                }

                if (error)
                {
                    this.ShowMessage(this.RadNotification, "Imposible aditar Aplicación", strBuilder, MessageType.Warning);
                    e.Canceled = true;

                    return;
                }

                var year = ((DateTime)this.rmyYear.SelectedDate).Year;
                var radSing = (RadComboBox)item.FindControl("radDropSpendSing");

                var budgetId = (int)item.GetDataKeyValue("PRE_CODIGO");

                var credictModificationBudget = new PRE_MODIF_CREDITO_PRESUPUESTO
                {
                    PRE_CODIGO = budgetId,
                    MODP_I_G = "G",
                    MOD_CODIGO = this.CreditModification.MOD_CODIGO,
                    MODP_IMPORTE = Convert.ToDecimal(txtSpendAmount.Value),
                    MODP_POSITIVO = radSing.SelectedValue.Equals("+"),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var insert = this.creditModificationBudgetsService.UpdateCreditModificationBudget(credictModificationBudget, year);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Modificación de Credito para poder editarla.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error modificando la Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgSpendApplications_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var budgetId = (int)item.GetDataKeyValue("PRE_CODIGO");
            var creditModificationId = (int)item.GetDataKeyValue("MOD_CODIGO");

            try
            {
                var delete = this.creditModificationBudgetsService.DeleteCreditModificationBudget(budgetId, creditModificationId, "G", LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Modificación de Crédito.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Modificación de Crédito en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Imposible eliminar Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error eliminando la Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.CreditModification.MOD_CODIGO != 0)
            {
                if (!(bool)this.CreditModification.MOD_EJECUTADA)
                {
                    if (this.CreditModification.MOD_CREADO_EXPEDIENTE == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "moveNewButtons", "moveNewButtons();", true);
                    }

                    this.ValidateButtons();
                }
                else
                {
                    this.btnGenerateFile.Enabled = false;
                    this.btnReport.Enabled = true;
                    this.btnExecute.Enabled = false;
                }
            }
            else
            {
                this.btnGenerateFile.Enabled = false;
                this.btnReport.Enabled = false;
                this.btnExecute.Enabled = false;
            }
        }

        private void ValidateButtons()
        {
            if (this.RgIncomeApplications.MasterTableView.Items.Count == 0 && this.RgSpendApplications.MasterTableView.Items.Count == 0)
            {
                this.btnGenerateFile.Enabled = false;
                this.btnReport.Enabled = false;
                this.btnExecute.Enabled = false;
            }
            else
            {
                if (this.txtTotalIncomes.Text.Equals(this.txtTotalSpends.Text))
                {
                    if (this.CreditModification.MOD_CREADO_EXPEDIENTE == false)
                    {
                        this.btnGenerateFile.Enabled = true;
                        this.btnReport.Enabled = false;
                        this.btnExecute.Enabled = false;
                    }
                    else
                    {
                        this.btnGenerateFile.Enabled = false;
                        this.btnReport.Enabled = true;
                        this.btnExecute.Enabled = true;
                    }
                }
                else
                {
                    this.btnGenerateFile.Enabled = false;
                    this.btnReport.Enabled = false;
                    this.btnExecute.Enabled = false;
                }
            }
        }

        protected void btnGenerateFile_Click(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var creditModification = new PRE_MODIFICACION_CREDITO
                {
                    MOD_CODIGO = this.CreditModification.MOD_CODIGO,
                    MOD_ANO_PRESUPUESTO = this.CreditModification.MOD_ANO_PRESUPUESTO,
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var generate = creditModificationsService.GenerateRecordsCreditModification(creditModification);

                if (generate.ResponseCode != ResponseCode.Ok)
                {
                    strBuilder.Append($"No se pueden generar los expedientes porque {generate.ResponseMethod.ToString()}");
                    this.ShowMessage(this.RadNotification, "Error generando expedientes", strBuilder, MessageType.Warning);
                }
                else
                {
                    this.Response.Redirect($"~/Views/Budget/ManageCreditModification.aspx?id={this.CreditModification.MOD_CODIGO}");
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error generando expedientes.");
                strBuilder.Append("Ha ocurrido un error generando los expedientes de la Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Error generando expedientes", strBuilder, MessageType.Deny);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            var id = this.CreditModification.MOD_CODIGO;
            var description = this.CreditModification.MOD_DESCRIPCION;
            var year = this.CreditModification.MOD_ANO_PRESUPUESTO;
            var order = this.CreditModification.MOD_NUMERO_ORDEN;
            var proposalDate = ((DateTime)this.CreditModification.MOD_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var incomeType = this.rcModificationTypeIncomes.SelectedItem.Text.Split('-')[0].TrimStart();
            var incomeDescription = this.rcModificationTypeIncomes.SelectedItem.Text.Split('-')[1].TrimStart();
            var spendType = this.rcModificationTypeSpends.SelectedItem.Text.Split('-')[0].TrimStart();
            var spendDescription = this.rcModificationTypeSpends.SelectedItem.Text.Split('-')[1].TrimStart();

            var script = $"printCreditModification('{id}', '{description}', '{year}', '{order}', '{proposalDate}', '{incomeType}', '{incomeDescription}', '{spendType}', '{spendDescription}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "printCreditModification", script, true);
        }

        protected void btnExecute_OnClick(object sender, EventArgs e)
        {
            var page = this.Page;
            var type = this.Page.GetType();
            ScriptManager.RegisterStartupScript(page, type, "confirmExecute", $"confirmExecute();", true);
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int ExecuteCreditModification()
        {
            try
            {
                var id = ((PRE_MODIFICACION_CREDITO)HttpContext.Current.Session["_creditModification"]).MOD_CODIGO;

                var execute = creditModificationsService.ExecuteCreditModification(id, LoginUser.USU_CODIGO);

                var result = (int)execute.ResponseMethod;

                return result;
            }
            catch (Exception ex)
            {
                LogError(ex, "Error ejecutando la modificación de crédito.");
                return 0;
            }
        }

        #endregion


    }
}