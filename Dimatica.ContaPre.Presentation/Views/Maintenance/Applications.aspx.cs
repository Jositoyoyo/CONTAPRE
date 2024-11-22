namespace Dimatica.ContaPre.Presentation.Views.Maintenance
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class Applications : BasePage
    {
        #region Fields

        IAccountPgcpService accountService = DependencyFactory.GetInstance<IAccountPgcpService>();

        IApplicationService applicationService = DependencyFactory.GetInstance<IApplicationService>();

        IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        #endregion

        public string Year
        {
            get
            {
                var o = this.Session["_year"];
                if (o == null)
                {
                    o = string.Empty;
                    this.Session["_year"] = o;
                }

                return o.ToString();
            }
            set
            {
                this.Session["_year"] = value;
            }
        }

        public string Type
        {
            get
            {
                var o = this.Session["_type"];
                if (o == null)
                {
                    o = string.Empty;
                    this.Session["_type"] = o;
                }

                return o.ToString();
            }
            set
            {
                this.Session["_type"] = value;
            }
        }

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "Applications";

                if (!string.IsNullOrWhiteSpace(LoginUser?.USU_I_G))
                {
                    this.RcTypes.SelectedValue = LoginUser.USU_I_G;
                }

                var year = this.Request.QueryString["year"];
                this.Year = year;
                var type = this.Request.QueryString["type"];
                this.Type = type;
            }
        }

        protected void RgApplications_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypes.SelectedValue) ? string.Empty : this.RcTypes.SelectedValue;
            this.FillApplications(type, false);
        }

        protected void RgApplications_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("ApplicationId");
            var level = item.GetDataKeyValue("Level").ToString();
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var radAccount = (RadComboBox)item.FindControl("radDropAccount");

            var application = new Application
            {
                ApplicationId = id,
                Level = level,
                Description = txtDescription.Text,
                AccountId = radAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(radAccount.SelectedValue)
            };

            try
            {
                var update = this.applicationService.UpdateApplication(application, LoginUser.USU_CODIGO);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Aplicación.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Aplicación en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Aplicación", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Aplicación.");
                strBuilder.Append("Ha ocurrido un error modificando la Aplicación en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Aplicación", strBuilder, MessageType.Deny);
            }
        }

        protected void RgApplications_OnStatusCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = ((GridEditableItem)e.Item);
            var id = (int)item.GetDataKeyValue("ApplicationId");
            var status = (bool)item.GetDataKeyValue("Active");

            var application = new Application
            {
                ApplicationId = id,
                Active = !status
            };

            try
            {
                var updateStatus = this.applicationService.UpdateStatus(application, LoginUser.USU_CODIGO);

                switch (updateStatus.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Aplicación.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Aplicación en cuestión.");

                        break;
                }

                if (updateStatus.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible cambiar estado", strBuilder, MessageType.Warning);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(this.Year) && !string.IsNullOrWhiteSpace(this.Type))
                    {
                        if (status)
                        {
                            var delete = this.budgetsService.DeleteMany(id, Convert.ToInt32(this.Year), this.Type, LoginUser.USU_CODIGO);

                            switch (delete.ResponseCode)
                            {
                                case ResponseCode.NotFound:
                                    strBuilder.Append("No se ha encontrado el Presupuesto en cuestión para elminarle las aplicaciones.");
                                    break;
                                case ResponseCode.NotFoundChild:
                                    strBuilder.Append("Las aplicaciones no se han encontrado en el Presupuesto en cuestión.");
                                    break;
                                case ResponseCode.Invalid:
                                    strBuilder.Append("No se puede eliminar las aplicaciones del Presupuesto porque algunas tienen dependencias.");
                                    break;
                                case ResponseCode.IsClose:
                                    strBuilder.Append("El Presupuesto en cuestión ya se encuentra cerrado por lo que no se le pueden eliminar aplicaciones.");
                                    break;
                            }

                            if (delete.ResponseCode != ResponseCode.Ok)
                            {
                                this.ShowMessage(this.RadNotification, "Imposible eliminar aplicaciones", strBuilder, MessageType.Warning);
                            }
                        }
                        else
                        {
                            var add = this.budgetsService.AddMany(id, Convert.ToInt32(this.Year), this.Type, LoginUser.USU_CODIGO);

                            switch (add.ResponseCode)
                            {
                                case ResponseCode.NotFound:
                                    strBuilder.Append("No se ha encontrado el Presupuesto en cuestión para agregarle las aplicaciones.");
                                    break;
                                case ResponseCode.Found:
                                    strBuilder.Append("Las aplicaciones ya se encuentran en el Presupuesto en cuestión.");
                                    break;
                                case ResponseCode.Invalid:
                                    strBuilder.Append("Ha ocurrido un error intentando agregar las aplicaciones al Presupuesto en cuestión.");
                                    break;
                                case ResponseCode.IsClose:
                                    strBuilder.Append("El Presupuesto en cuestión ya se encuentra cerrado por lo que no se le pueden agregar aplicaciones.");
                                    break;
                            }

                            if (add.ResponseCode != ResponseCode.Ok)
                            {
                                this.ShowMessage(this.RadNotification, "Imposible agragar aplicaciones", strBuilder, MessageType.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Imposible cambiar estado.");
                strBuilder.Append("Ha ocurrido un error cambiando el estado de la Aplicación en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible cambiar estado", strBuilder, MessageType.Deny);
            }
        }

        protected void RgApplications_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgApplications.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgApplications.MasterTableView.GetColumn("StatusColumn").Visible = false;
                this.RgApplications.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgApplications.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgApplications_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;
                var active = (bool)dataBoundItem.GetDataKeyValue("Active");
                var level = dataBoundItem.GetDataKeyValue("Level").ToString();

                dataBoundItem["StatusColumn"].CssClass = active ? "fas fa-hidden-eye" : "fas fa-eye";

                if (!level.Equals("CAP"))
                {
                    dataBoundItem["StatusColumn"].Style.Add("Display", "none !important");
                }
                else
                {
                    dataBoundItem["StatusColumn"].ToolTip = active ? "Ocultar Aplicación" : "Mostrar Aplicación";
                }

                dataBoundItem["EditColumn"].ToolTip = "Editar Aplicación";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Aplicación";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;
                var txtDescription = (RadTextBox)item.FindControl("txtDescription");
                var radAccount = (RadComboBox)item.FindControl("radDropAccount");

                var level = item.GetDataKeyValue("Level").ToString();

                switch (level)
                {
                    case "CAP":
                        txtDescription.MaxLength = 60;

                        break;
                    case "ART":
                        txtDescription.MaxLength = 100;

                        break;
                    case "CON":
                        txtDescription.MaxLength = 120;

                        break;
                    case "SUB":
                        txtDescription.MaxLength = 100;

                        break;
                }

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
                    foreach (GridDataItem i in this.RgApplications.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var accountId = item.GetDataKeyValue("AccountId");

                    if (accountId != null)
                    {
                        radAccount.SelectedValue = accountId.ToString();
                    }
                }
            }
        }

        protected void RgApplications_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAplication")
            {
                var strBuilder = new StringBuilder();
                var item = e.Item as GridDataItem;

                var applicationId = (int)item.GetDataKeyValue("ApplicationId");
                var level = item.GetDataKeyValue("Level").ToString();

                var application = new Application
                {
                    ApplicationId = applicationId,
                    Level = level
                };

                try
                {
                    var delete = this.applicationService.DeleteApplication(application);

                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos de la Aplicación.");

                            break;
                        case ResponseCode.Found:
                            strBuilder.Append("No puede eliminarse la aplicacón porque está asociada a algún presupuesto o documento, o tiene algún subconcepto que deoende de ella.");

                            break;
                        case ResponseCode.NotFound:
                            strBuilder.Append("No se ha encontrado la Aplicación en cuestión.");

                            break;
                    }

                    if (delete.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Imposible eliminar aplicación", strBuilder, MessageType.Warning);
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Imposible eliminar aplicación.");
                    strBuilder.Append("Ha ocurrido un error eliminando la Aplicación en cuestión.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar aplicación", strBuilder, MessageType.Deny);
                }

                var type = string.IsNullOrWhiteSpace(this.RcTypes.SelectedValue) ? string.Empty : this.RcTypes.SelectedValue;
                this.FillApplications(type, true);
            }
        }

        protected void RcTypes_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            this.FillApplications(type, true);
        }

        private void FillApplications(string type, bool manual)
        {
            var applications = new List<Application>();

            if (!string.IsNullOrEmpty(type))
            {
                var applicationsResult = this.applicationService.GetApplications(type);

                foreach (var application in applicationsResult)
                {
                    applications.Add(application);
                }
            }

            this.RgApplications.DataSource = applications;

            if (manual)
            {
                this.RgApplications.DataBind();
            }
        }

        #endregion
    }
}