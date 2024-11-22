using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dimatica.ContaPre.Presentation.Views.Maintenance
{
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    public partial class AccountsRestricted : BasePage
    {
        #region Fields
        
        IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        ICostPlacesService costPlacesService = DependencyFactory.GetInstance<ICostPlacesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "AccountsRestricted";

                if (!string.IsNullOrWhiteSpace(LoginUser?.USU_I_G))
                {
                    this.RcTypes.SelectedValue = LoginUser.USU_I_G;
                }
            }
        }

        protected void RgAccountsRestricted_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypes.SelectedValue) ? string.Empty : this.RcTypes.SelectedValue;
            this.FillAccounts(type, false);
        }

        protected void RgAccountsRestricted_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var radType = (RadComboBox)item.FindControl("radDropType");
            var txtOrder = (RadNumericTextBox)item.FindControl("txtOrder");
            var radPlace = (RadComboBox)item.FindControl("radDropPlace");
            var txtName = (RadTextBox)item.FindControl("txtName");
            var txtBank = (RadTextBox)item.FindControl("txtBank");
            var txtAccount = (RadTextBox)item.FindControl("txtAccount");
            var txtAddress = (RadTextBox)item.FindControl("txtAddress");

            var account = new PRE_CUENTA_RESTRINGIDA
            {
                CUE_I_G = radType.SelectedValue,
                CUE_ORDINAL_PERCEPTOR = Convert.ToInt32(txtOrder.Value),
                CUE_DESCRIPCION = txtName.Text,
                CUE_ENTIDAD = txtBank.Text,
                CUE_CC = txtAccount.Text,
                CUE_DIRECCION = txtAddress.Text,
                CEN_CODIGO = Convert.ToInt32(radPlace.SelectedValue),
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            try
            {
                var insert = this.accountRestrictedService.InsertAccount(account);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Cuenta Restringida.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando la Cuenta Restringida", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la Cuenta Restringida.");
                strBuilder.Append("Ha ocurrido un error insertando la Cuenta Restringida en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando la Cuenta Restringida", strBuilder, MessageType.Deny);
            }
        }

        protected void RgAccountsRestricted_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("CUE_CODIGO");
            var radType = (RadComboBox)item.FindControl("radDropType");
            var txtOrder = (RadNumericTextBox)item.FindControl("txtOrder");
            var radPlace = (RadComboBox)item.FindControl("radDropPlace");
            var txtName = (RadTextBox)item.FindControl("txtName");
            var txtBank = (RadTextBox)item.FindControl("txtBank");
            var txtAccount = (RadTextBox)item.FindControl("txtAccount");
            var txtAddress = (RadTextBox)item.FindControl("txtAddress");

            var account = new PRE_CUENTA_RESTRINGIDA
                          {
                                  CUE_CODIGO = id,
                                  CUE_I_G = radType.SelectedValue,
                                  CUE_ORDINAL_PERCEPTOR = Convert.ToInt32(txtOrder.Value),
                                  CUE_DESCRIPCION = txtName.Text,
                                  CUE_ENTIDAD = txtBank.Text,
                                  CUE_CC = txtAccount.Text,
                                  CUE_DIRECCION = txtAddress.Text,
                                  CEN_CODIGO = Convert.ToInt32(radPlace.SelectedValue),
                                  USU_CODIGO = LoginUser.USU_CODIGO
                          };

            try
            {
                var update = this.accountRestrictedService.UpdateAccount(account);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Cuenta Restringida.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Cuenta Restringida en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Cuenta Restringida", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Cuenta Restringida.");
                strBuilder.Append("Ha ocurrido un error modificando la Cuenta Restringida en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Cuenta Restringida", strBuilder, MessageType.Deny);
            }
        }

        protected void RgAccountsRestricted_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var accountId = (int)item.GetDataKeyValue("CUE_CODIGO");

            try
            {
                var delete = this.accountRestrictedService.DeleteAccount(accountId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Cuenta Restringida.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse la Cuenta Restringida porque tiene asociado algún expediente, hoja de arqueo o apunte de tesorería.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Cuenta Restringida en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Cuenta Restringida", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Imposible eliminar cuenta restringida.");
                strBuilder.Append("Ha ocurrido un error eliminando la Cuenta Restringida en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar cuenta restringida", strBuilder, MessageType.Deny);
            }
        }

        protected void RgAccountsRestricted_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgAccountsRestricted.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgAccountsRestricted.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgAccountsRestricted.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgAccountsRestricted_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Cuenta Restringida";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Cuenta Restringida";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radType = (RadComboBox)item.FindControl("radDropType");
                radType.SelectedValue = this.RcTypes.SelectedValue;

                var radPlace = (RadComboBox)item.FindControl("radDropPlace");

                var costPlaces = this.costPlacesService.GetCostPlacesToCombo();
                radPlace.DataSource = costPlaces.ToList();
                radPlace.DataBind();

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgAccountsRestricted.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var txtOrder = (RadNumericTextBox)item.FindControl("txtOrder");
                    var order = (int)item.GetDataKeyValue("CUE_ORDINAL_PERCEPTOR");
                    txtOrder.Value = Convert.ToDouble(order);

                    var placeId = item.GetDataKeyValue("CEN_CODIGO");

                    if (placeId != null)
                    {
                        radPlace.SelectedValue = placeId.ToString();
                    }
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
            var accounts = new List<PRE_CUENTA_RESTRINGIDA>();

            if (!string.IsNullOrEmpty(type))
            {
                var accountsResult = this.accountRestrictedService.GetAccountsRestricted(type);

                foreach (var account in accountsResult)
                {
                    accounts.Add(account);
                }
            }

            this.RgAccountsRestricted.DataSource = accounts;

            if (manual)
            {
                this.RgAccountsRestricted.DataBind();
            }
        }

        #endregion
    }
}