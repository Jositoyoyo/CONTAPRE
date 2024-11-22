namespace Dimatica.ContaPre.Presentation.Views.Systems.Users
{
    #region NameSpaces

    using System;
    using System.Linq;
    using System.Text;
    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.Presentation.Views.Shared;
    using Telerik.Web.UI;

    #endregion

    public partial class Users : BasePage
    {
        #region Fields

        IUserService userService = DependencyFactory.GetInstance<IUserService>();

        #endregion

        #region Private Methods

        /**
         * Metodo por defecto que se invoca en cada llamada                              
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializePage("");
            }
            if (LoginUser.USU_NIVEL.ToString() != "100")
            {
                newUserDiv.Visible = false;
            }
        }


        protected void RgUsers_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var users = this.userService.GetUsers();
            this.RgUsers.DataSource = users.OrderBy(t => t.Obsolete).ThenBy(t => t.USU_NOMBRE).ToList();
        }

        protected void RgUsers_OnStatusCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var item = (GridDataItem)e.Item;

                var id = (int)item.GetDataKeyValue("USU_CODIGO");
                var obsolete = (bool)item.GetDataKeyValue("Obsolete");

                var currentObsolete = !obsolete;

                var update = this.userService.UpdateStatus(id, currentObsolete);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de el Usuario.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Usuario en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el estado de el Usuario", strBuilder, MessageType.Warning);
                }

            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Usuario.");
                strBuilder.Append("Ha ocurrido un error modificando el estado de el Usuario en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Usuario", strBuilder, MessageType.Deny);
            }
        }

        protected void RgUsers_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() != "100")
            {
                this.RgUsers.MasterTableView.GetColumn("EditColumn").Visible   = false;
                this.RgUsers.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgUsers.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }
        protected void RgUsers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem                      = e.Item as GridDataItem;
                var obsolete                           = (bool)dataBoundItem.GetDataKeyValue("Obsolete");
                dataBoundItem["DeleteColumn"].CssClass = obsolete ? "fas fa-trash-restore-alt" : "fas fa-hidden-eye";
                dataBoundItem["EditColumn"].ToolTip    = "Editar Usuario";
                dataBoundItem["DeleteColumn"].ToolTip  = obsolete ? "Recuperar Usuario" : "Desactivar Usuario";
            }

        }

        protected void RgUsers_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateUser")
            {
                var item   = e.Item as GridDataItem;
                var UserId = (int) item.GetDataKeyValue("USU_CODIGO");

                this.Response.Redirect($"~/Views/Systems/Users/ManageUser.aspx?id={UserId}");
            }
        }


        #endregion
    }
}