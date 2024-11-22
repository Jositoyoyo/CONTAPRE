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

    public partial class Programs : BasePage
    {
        #region Fields
        
        IProgramsService programsService = DependencyFactory.GetInstance<IProgramsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "Programs";
            }
        }

        protected void RgPrograms_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var programs = this.programsService.GetPrograms();

            this.RgPrograms.DataSource = programs;
        }

        protected void RgPrograms_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var txtNumber = (RadTextBox)item.FindControl("txtNumber");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var checkDefault = (RadCheckBox)item.FindControl("checkDefault");

            var program = new PRE_PROGRAMA
                          {
                                  PRO_NUMERO = txtNumber.Text,
                                  PRO_DESCRIPCION = txtDescription.Text,
                                  PRO_POR_DEFECTO = checkDefault.Checked,
                                  USU_CODIGO = LoginUser.USU_CODIGO
                          };

            try
            {
                var insert = this.programsService.InsertProgram(program);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Programa.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Programa con ese Número.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Programa", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Programa.");
                strBuilder.Append("Ha ocurrido un error insertando el Programa en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Programa", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPrograms_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (byte)item.GetDataKeyValue("PRO_CODIGO");
            var txtNumber = (RadTextBox)item.FindControl("txtNumber");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var checkDefault = (RadCheckBox)item.FindControl("checkDefault");

            var program = new PRE_PROGRAMA
                          {
                                  PRO_CODIGO = id,
                                  PRO_NUMERO = txtNumber.Text,
                                  PRO_DESCRIPCION = txtDescription.Text,
                                  PRO_POR_DEFECTO = checkDefault.Checked,
                                  USU_CODIGO = LoginUser.USU_CODIGO
                          };

            try
            {
                var update = this.programsService.UpdateProgram(program);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Programa.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Programa en cuestión.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Programa con ese Número.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Programa", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Programa.");
                strBuilder.Append("Ha ocurrido un error modificando el Programa en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Programa", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPrograms_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var programId = (byte)item.GetDataKeyValue("PRO_CODIGO");

            try
            {
                var delete = this.programsService.DeleteProgram(programId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Programa.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse el Programa porque tiene asociado algún presupuesto o expediente.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Programa en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Programa", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Programa.");
                strBuilder.Append("Ha ocurrido un error eliminando el Programa en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Programa", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPrograms_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgPrograms.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgPrograms.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgPrograms.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgPrograms_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;
                var isDefault = (bool)dataBoundItem.GetDataKeyValue("PRO_POR_DEFECTO");

                if (isDefault)
                {
                    dataBoundItem["DeleteColumn"].Style.Add("Display", "none !important");
                }

                dataBoundItem["EditColumn"].ToolTip = "Editar Programa";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Programa";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgPrograms.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var item = (GridEditableItem)e.Item;
                    var isDefault = (bool)item.GetDataKeyValue("PRO_POR_DEFECTO");
                    var checkDefault = (RadCheckBox)item.FindControl("checkDefault");
                    checkDefault.Checked = isDefault;
                }
            }
        }

        #endregion
    }
}