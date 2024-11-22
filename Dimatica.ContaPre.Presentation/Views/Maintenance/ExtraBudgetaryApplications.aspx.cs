namespace Dimatica.ContaPre.Presentation.Views.Maintenance
{
    #region NameSpaces

    using System;
    using System.Linq;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class ExtraBudgetaryApplications : BasePage
    {
        #region Fields

        IAccountPgcpService accountService = DependencyFactory.GetInstance<IAccountPgcpService>();

        IExtraBudgetaryApplicationsService extraBudgetaryService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "ExtraBudgetaryApplications";
            }
        }

        protected void RgExtraBudgetaryApplications_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var extraBudgetary = this.extraBudgetaryService.GetExtraBudgetaryApplications();

            this.RgExtraBudgetaryApplications.DataSource = extraBudgetary;
        }

        protected void RgExtraBudgetaryApplications_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var radDropAccount = (RadComboBox)item.FindControl("radDropAccount");

            var application = new PRE_EXTRAPRESUPUESTARIA
                              {
                                      EXTRAPRE_NUMERO = Convert.ToInt32(txtNumber.Value),
                                      EXTRAPRE_DESCRIPCION = txtDescription.Text,
                                      CUEP_CODIGO = radDropAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(radDropAccount.SelectedValue)
                              };

            try
            {
                var insert = this.extraBudgetaryService.InsertExtraBudgetaryApplication(application);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Aplicación Extrapresupuestaria.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando la Aplicación Extrapresupuestaria", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la Aplicación Extrapresupuestaria.");
                strBuilder.Append("Ha ocurrido un error insertando la Aplicación Extrapresupuestaria en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando la Aplicación Extrapresupuestaria", strBuilder, MessageType.Deny);
            }
        }

        protected void RgExtraBudgetaryApplications_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("EXTRAPRE_CODIGO");
            var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var radDropAccount = (RadComboBox)item.FindControl("radDropAccount");

            var application = new PRE_EXTRAPRESUPUESTARIA
                              {
                                      EXTRAPRE_CODIGO = id,
                                      EXTRAPRE_NUMERO = Convert.ToInt32(txtNumber.Value),
                                      EXTRAPRE_DESCRIPCION = txtDescription.Text,
                                      CUEP_CODIGO = radDropAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(radDropAccount.SelectedValue)
                              };

            try
            {
                var update = this.extraBudgetaryService.UpdateExtraBudgetaryApplication(application);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Aplicación Extrapresupuestaria.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Aplicación Extrapresupuestaria en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Aplicación Extrapresupuestaria", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Aplicación Extrapresupuestaria.");
                strBuilder.Append("Ha ocurrido un error modificando la Aplicación Extrapresupuestaria en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Aplicación Extrapresupuestaria", strBuilder, MessageType.Deny);
            }
        }

        protected void RgExtraBudgetaryApplications_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var applicationId = (int)item.GetDataKeyValue("EXTRAPRE_CODIGO");

            try
            {
                var delete = this.extraBudgetaryService.DeleteExtraBudgetaryApplication(applicationId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Aplicación Extrapresupuestaria.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse la Aplicación Extrapresupuestaria porque tiene asociado algún expediente, hoja de arqueo o apunte de tesorería.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Aplicación Extrapresupuestaria en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar aplicación extrapresupuestaria", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Imposible eliminar aplicación extrapresupuestaria.");
                strBuilder.Append("Ha ocurrido un error eliminando la Aplicación Extrapresupuestaria en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar aplicación extrapresupuestaria", strBuilder, MessageType.Deny);
            }
        }

        protected void RgExtraBudgetaryApplications_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgExtraBudgetaryApplications.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgExtraBudgetaryApplications.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgExtraBudgetaryApplications.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgExtraBudgetaryApplications_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Aplicación Extrapresupuestaria";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Aplicación Extrapresupuestaria";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radAccount = (RadComboBox)item.FindControl("radDropAccount");

                var accounts = this.accountService.GetAccountsToCombo();
                accounts.Insert(
                                0, new PRE_CUENTA_PGCP
                                   {
                                           CUEP_CODIGO = -1,
                                           CUEP_NUMERO = "< Seleccione >"
                                   });
                radAccount.DataSource = accounts.ToList();
                radAccount.DataBind();

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgExtraBudgetaryApplications.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var txtNumber = (RadNumericTextBox)item.FindControl("txtNumber");
                    var number = (int)item.GetDataKeyValue("EXTRAPRE_NUMERO");
                    txtNumber.Value = Convert.ToDouble(number);

                    var accountId = item.GetDataKeyValue("CUEP_CODIGO");

                    if (accountId != null)
                    {
                        radAccount.SelectedValue = accountId.ToString();
                    }
                }
            }
        }

        #endregion
    }
}