namespace Dimatica.ContaPre.Presentation.Views.Maintenance
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class CreditModificationTypes : BasePage
    {
        #region Fields

        ICreditModificationTypesService creditModificationTypesService = DependencyFactory.GetInstance<ICreditModificationTypesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "CreditModificationTypes";

                if (!string.IsNullOrWhiteSpace(LoginUser?.USU_I_G))
                {
                    this.RcTypes.SelectedValue = LoginUser.USU_I_G;
                }
            }
        }

        protected void RgCreditModificationTypes_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypes.SelectedValue) ? string.Empty : this.RcTypes.SelectedValue;
            this.FillAccounts(type, false);
        }

        protected void RgCreditModificationTypes_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var radType = (RadComboBox)item.FindControl("radDropType");
            var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var creditModificationType = new PRE_TIPO_MODIFICACION_CREDITO
                                         {
                                                 TIPM_I_G = radType.SelectedValue,
                                                 TIPM_NUMERO = Convert.ToInt32(txtNumber.Value),
                                                 TIPM_DESCRIPCION = txtDescription.Text
                                         };

            try
            {
                var insert = this.creditModificationTypesService.InsertCreditModificationType(creditModificationType);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Modificación de Crédito.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Tipo de Modificación de Crédito con esa Descripción.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Tipo de Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Tipo de Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error insertando el Tipo de Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Tipo de Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgCreditModificationTypes_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("TIPM_CODIGO");
            var radType = (RadComboBox)item.FindControl("radDropType");
            var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var creditModificationType = new PRE_TIPO_MODIFICACION_CREDITO
                                         {
                                                 TIPM_CODIGO = id,
                                                 TIPM_I_G = radType.SelectedValue,
                                                 TIPM_NUMERO = Convert.ToInt32(txtNumber.Value),
                                                 TIPM_DESCRIPCION = txtDescription.Text
                                         };

            try
            {
                var update = this.creditModificationTypesService.UpdateCreditModificationType(creditModificationType);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Modificación de Crédito.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Tipo de Modificación de Crédito en cuestión.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Tipo de Modificación de Crédito con esa Descripción.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Tipo de Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Tipo de Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error modificando el Tipo de Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Tipo de Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgCreditModificationTypes_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var creditModificationTypeId = (int)item.GetDataKeyValue("TIPM_CODIGO");

            try
            {
                var delete = this.creditModificationTypesService.DeleteCreditModificationType(creditModificationTypeId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Modificación de Crédito.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse el Tipo de Modificación de Crédito porque tiene asociado algún expediente de modificacón.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Tipo de Modificación de Crédito en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Tipo de Modificación de Crédito", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Imposible eliminar Tipo de Modificación de Crédito.");
                strBuilder.Append("Ha ocurrido un error eliminando el Tipo de Modificación de Crédito en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Tipo de Modificación de Crédito", strBuilder, MessageType.Deny);
            }
        }

        protected void RgCreditModificationTypes_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgCreditModificationTypes.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgCreditModificationTypes.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgCreditModificationTypes.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgCreditModificationTypes_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Tipo Modificación de Crédito";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Tipo Modificación de Crédito";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radType = (RadComboBox)item.FindControl("radDropType");
                radType.SelectedValue = this.RcTypes.SelectedValue;

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgCreditModificationTypes.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
                    var number = (int)item.GetDataKeyValue("TIPM_NUMERO");
                    txtNumber.Value = Convert.ToDouble(number);
                }
            }
        }

        protected void RcTypes_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            this.FillAccounts(type, true);
        }

        private void FillAccounts(string type, bool manual)
        {
            var creditModificationTypes = new List<PRE_TIPO_MODIFICACION_CREDITO>();

            if (!string.IsNullOrEmpty(type))
            {
                var creditModificationTypesResult = this.creditModificationTypesService.GetCreditModificationTypes(type);

                foreach (var account in creditModificationTypesResult)
                {
                    creditModificationTypes.Add(account);
                }
            }

            this.RgCreditModificationTypes.DataSource = creditModificationTypes;

            if (manual)
            {
                this.RgCreditModificationTypes.DataBind();
            }
        }

        #endregion
    }
}