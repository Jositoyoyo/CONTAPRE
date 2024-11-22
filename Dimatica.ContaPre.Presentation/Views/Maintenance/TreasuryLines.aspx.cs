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

    public partial class TreasuryLines : BasePage
    {
        #region Fields

        ITreasuryLinesService treasuryLinesService = DependencyFactory.GetInstance<ITreasuryLinesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "TreasuryLines";
            }
        }

        protected void RgTreasuryLines_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var treasuryLines = this.treasuryLinesService.GetTreasuryLines();

            this.RgTreasuryLines.DataSource = treasuryLines;
        }

        protected void RgTreasuryLines_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var txtId = (RadNumericTextBox)item.FindControl("txtId");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var txtOrigin = (RadTextBox)item.FindControl("txtOrigin");
            var txtOriginCode = (RadNumericTextBox)item.FindControl("txtOriginCode");


            var treasuryLine = new PRE_LINEA_TESORERIA
            {
                LIN_NUMERO = Convert.ToInt32(txtId.Value),
                LIN_DESCRIPCION = txtDescription.Text,
                LIN_ORIGEN_DESCRIPCION = txtOrigin.Text,
                LIN_ORIGEN_CODIGO = Convert.ToInt32(txtOriginCode.Value),
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            try
            {
                var insert = this.treasuryLinesService.InsertTreasuryLine(treasuryLine);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Línea de Tesorería.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe una Línea de Tesorería con esa Clave o Descripción.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando la Línea de Tesorería", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la Línea de Tesorería.");
                strBuilder.Append("Ha ocurrido un error insertando la Línea de Tesorería en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando la Línea de Tesorería", strBuilder, MessageType.Deny);
            }
        }

        protected void RgTreasuryLines_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("LIN_NUMERO");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var txtOrigin = (RadTextBox)item.FindControl("txtOrigin");
            var txtOriginCode = (RadNumericTextBox)item.FindControl("txtOriginCode");


            var treasuryLine = new PRE_LINEA_TESORERIA
                               {
                                       LIN_NUMERO = id,
                                       LIN_DESCRIPCION = txtDescription.Text,
                                       LIN_ORIGEN_DESCRIPCION = txtOrigin.Text,
                                       LIN_ORIGEN_CODIGO = Convert.ToInt32(txtOriginCode.Value),
                                       USU_CODIGO = LoginUser.USU_CODIGO
            };
            
            try
            {
                var update = this.treasuryLinesService.UpdateTreasuryLine(treasuryLine);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Línea de Tesorería.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Línea de Tesorería en cuestión.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe una Línea de Tesorería con esa Descripción.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Línea de Tesorería", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Línea de Tesorería.");
                strBuilder.Append("Ha ocurrido un error modificando la Línea de Tesorería en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Línea de Tesorería", strBuilder, MessageType.Deny);
            }
        }

        protected void RgTreasuryLines_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var treasuryLineId = (int)item.GetDataKeyValue("LIN_NUMERO");

            try
            {
                var delete = this.treasuryLinesService.DeleteTreasuryLine(treasuryLineId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Línea de Tesorería.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse la Línea de Tesorería porque tiene asociado algún expediente o documento.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Línea de Tesorería en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Línea de Tesorería", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando la Línea de Tesorería.");
                strBuilder.Append("Ha ocurrido un error eliminando la Línea de Tesorería en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Línea de Tesorería", strBuilder, MessageType.Deny);
            }
        }

        protected void RgTreasuryLines_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgTreasuryLines.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgTreasuryLines.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgTreasuryLines.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgTreasuryLines_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Línea de Tesorería";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Línea de Tesorería";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgTreasuryLines.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var item = (GridEditableItem)e.Item;
                    var txtId = (RadNumericTextBox)item.FindControl("txtId");
                    var txtOriginCode = (RadNumericTextBox)item.FindControl("txtOriginCode");
                    var id = (int)item.GetDataKeyValue("LIN_NUMERO");
                    txtId.Value = Convert.ToDouble(id);
                    var originCode = (int)item.GetDataKeyValue("LIN_ORIGEN_CODIGO");
                    txtOriginCode.Value = Convert.ToDouble(originCode);
                }
            }
        }

        #endregion
    }
}