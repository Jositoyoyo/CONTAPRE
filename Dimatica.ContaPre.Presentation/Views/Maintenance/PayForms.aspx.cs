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

    public partial class PayForms : BasePage
    {
        #region Fields

        IPayFormsService payFormsService = DependencyFactory.GetInstance<IPayFormsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "PayForms";
            }
        }

        protected void RgPayForms_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var payForms = this.payFormsService.GetPayForms();

            this.RgPayForms.DataSource = payForms;
        }

        protected void RgPayForms_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var payForm = new PRE_FORMA_PAGO
                          {
                                  FOR_DESCRIPCION = txtDescription.Text
                          };

            try
            {
                var insert = this.payFormsService.InsertPayForm(payForm);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Forma de Pago.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe una Forma de Pago con esa Descripción.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando la Forma de Pago", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la Forma de Pago.");
                strBuilder.Append("Ha ocurrido un error insertando la Forma de Pago en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando la Forma de Pago", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPayForms_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (byte)item.GetDataKeyValue("FOR_CODIGO");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var payForm = new PRE_FORMA_PAGO
                          {
                                  FOR_CODIGO = id,
                                  FOR_DESCRIPCION = txtDescription.Text,
                          };

            try
            {
                var update = this.payFormsService.UpdatePayForm(payForm);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Forma de Pago.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Forma de Pago en cuestión.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe una Forma de Pago con esa Descripción.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Forma de Pago", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Forma de Pago.");
                strBuilder.Append("Ha ocurrido un error modificando la Forma de Pago en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Forma de Pago", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPayForms_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var payFormId = (byte)item.GetDataKeyValue("FOR_CODIGO");

            try
            {
                var delete = this.payFormsService.DeletePayForm(payFormId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Forma de Pago.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse la Forma de Pago porque tiene asociado algún expediente o documento.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Forma de Pago en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Forma de Pago", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando la Forma de Pago.");
                strBuilder.Append("Ha ocurrido un error eliminando la Forma de Pago en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Forma de Pago", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPayForms_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgPayForms.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgPayForms.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgPayForms.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgPayForms_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Forma de Pago";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Forma de Pago";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgPayForms.Items)
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