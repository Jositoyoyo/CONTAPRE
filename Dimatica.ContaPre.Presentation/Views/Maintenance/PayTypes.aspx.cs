namespace Dimatica.ContaPre.Presentation.Views.Maintenance
{
    #region NameSpaces

    using System;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class PayTypes : BasePage
    {
        #region Fields

        IPayTypesService payTypesService = DependencyFactory.GetInstance<IPayTypesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "PayTypes";
            }
        }

        protected void RgPayTypes_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var payTypes = this.payTypesService.GetPayTypes();

            this.RgPayTypes.DataSource = payTypes;
        }

        protected void RgPayTypes_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var payType = new PRE_TIPO_PAGO
                          {
                                  TIPP_DESCRIPCION = txtDescription.Text
                          };

            try
            {
                var insert = this.payTypesService.InsertPayType(payType);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Pago.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Tipo de Pago con esa Descripción.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Tipo de Pago", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Tipo de Pago.");
                strBuilder.Append("Ha ocurrido un error insertando el Tipo de Pago en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Tipo de Pago", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPayTypes_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (byte)item.GetDataKeyValue("TIPP_CODIGO");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var payType = new PRE_TIPO_PAGO
                          {
                                  TIPP_CODIGO = id,
                                  TIPP_DESCRIPCION = txtDescription.Text,
                          };

            try
            {
                var update = this.payTypesService.UpdatePayType(payType);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Pago.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Tipo de Pago en cuestión.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Tipo de Pago con esa Descripción.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Tipo de Pago", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Tipo de Pago.");
                strBuilder.Append("Ha ocurrido un error modificando el Tipo de Pago en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Tipo de Pago", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPayTypes_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var payTypeId = (byte)item.GetDataKeyValue("TIPP_CODIGO");

            try
            {
                var delete = this.payTypesService.DeletePayType(payTypeId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Pago.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse el Tipo de Pago porque tiene asociado algún expediente o documento.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Tipo de Pago en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Tipo de Pago", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Tipo de Pago.");
                strBuilder.Append("Ha ocurrido un error eliminando el Tipo de Pago en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Tipo de Pago", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPayTypes_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgPayTypes.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgPayTypes.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgPayTypes.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgPayTypes_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Tipo de Pago";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Tipo de Pago";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgPayTypes.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
            }
        }

        #endregion
    }
}