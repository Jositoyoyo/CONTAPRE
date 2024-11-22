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

    public partial class Provenances : BasePage
    {
        #region Fields

        IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "Provenances";

                if (!string.IsNullOrWhiteSpace(LoginUser?.USU_I_G))
                {
                    this.RcTypes.SelectedValue = LoginUser.USU_I_G;
                }
            }
        }

        protected void RgProvenances_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypes.SelectedValue) ? string.Empty : this.RcTypes.SelectedValue;
            this.FillAccounts(type, false);
        }

        protected void RgProvenances_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var radType = (RadComboBox)item.FindControl("radDropType");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var provenance = new PRE_PROCEDENCIA
                             {
                                     PROC_I_G = radType.SelectedValue,
                                     PROC_DESCRIPCION = txtDescription.Text
                             };

            try
            {
                var insert = this.provenancesService.InsertProvenance(provenance);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Procedencia.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe una Procedencia con esa Descripción.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando la Procedencia", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la Procedencia.");
                strBuilder.Append("Ha ocurrido un error insertando la Procedencia en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando la Procedencia", strBuilder, MessageType.Deny);
            }
        }

        protected void RgProvenances_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("PROC_CODIGO");
            var radType = (RadComboBox)item.FindControl("radDropType");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");

            var provenance = new PRE_PROCEDENCIA
                             {
                                     PROC_CODIGO = id,
                                     PROC_I_G = radType.SelectedValue,
                                     PROC_DESCRIPCION = txtDescription.Text
                             };

            try
            {
                var update = this.provenancesService.UpdateProvenance(provenance);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Procedencia.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Procedencia en cuestión.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe una Procedencia con esa Descripción.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Procedencia", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Procedencia.");
                strBuilder.Append("Ha ocurrido un error modificando la Procedencia en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Procedencia", strBuilder, MessageType.Deny);
            }
        }

        protected void RgProvenances_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var provenanceId = (int)item.GetDataKeyValue("PROC_CODIGO");

            try
            {
                var delete = this.provenancesService.DeleteProvenance(provenanceId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Procedencia.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse la Procedencia porque tiene asociado algún expediente, hoja de arqueo o apunte de tesorería.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Procedencia en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Procedencia", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando la Procedencia.");
                strBuilder.Append("Ha ocurrido un error eliminando la Procedencia en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Procedencia", strBuilder, MessageType.Deny);
            }
        }

        protected void RgProvenances_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgProvenances.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgProvenances.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgProvenances.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgProvenances_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Procedencia";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Procedencia";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radType = (RadComboBox)item.FindControl("radDropType");
                radType.SelectedValue = this.RcTypes.SelectedValue;

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgProvenances.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
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
            var provenances = new List<PRE_PROCEDENCIA>();

            if (!string.IsNullOrEmpty(type))
            {
                var provenancesResult = this.provenancesService.GetProvenances(type);

                foreach (var account in provenancesResult)
                {
                    provenances.Add(account);
                }
            }

            this.RgProvenances.DataSource = provenances;

            if (manual)
            {
                this.RgProvenances.DataBind();
            }
        }

        #endregion
    }
}