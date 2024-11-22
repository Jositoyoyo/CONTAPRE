namespace Dimatica.ContaPre.Presentation.Views.Systems
{
    #region NameSpaces

    using System;
    using System.Linq;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.BLL.Services;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class Parameters : BasePage
    {
        #region Fields

        IParametersService parametersService = DependencyFactory.GetInstance<IParametersService>();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RgParameters_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var parameters = this.parametersService.GetParameters();
            this.RgParameters.DataSource = parameters.OrderBy(t => t.PAR_DESCRIPCION).ToList();
        }

        protected void RgParameters_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            try
            {
                var item = (GridEditFormItem)e.Item;
                var id = (int)item.GetDataKeyValue("PAR_CODIGO");
                var txtValue = (RadTextBox)item.FindControl("txtValue");

                var parameters = new PRE_PARAMETROS
                {
                    PAR_CODIGO = id,
                    PAR_VALOR = txtValue.Text,
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var update = parametersService.UpdateParameter(parameters);
               
                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Parámetro.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Parámetro en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Parámetro", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Parámetro.");
                strBuilder.Append("Ha ocurrido un error modificando el Parámetro en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Parámetro", strBuilder, MessageType.Deny);
            }
        }
        
        protected void RgParameters_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;
                dataBoundItem["EditColumn"].ToolTip = "Editar Parámetro";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgParameters.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
            }
        }

        protected void RgParameters_PreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgParameters.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgParameters.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }
    }
}