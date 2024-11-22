namespace Dimatica.ContaPre.Presentation.Views.Maintenance
{
    #region NameSpaces

    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Web.Services;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.Helpers;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Helpers.DataValidation;
    using Dimatica.ContaPre.Presentation.Views.Controls;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class Providers : BasePage
    {
        #region Fields

        IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        IProvincesService provincesService = DependencyFactory.GetInstance<IProvincesService>();

        #endregion

        #region Private Properties

        private bool IsVerify
        {
            get
            {
                var o = this.ViewState["isVerify"];

                if (o == null)
                {
                    o = false;
                    this.ViewState["isVerify"] = o;
                }

                return (bool)o;
            }

            set
            {
                this.ViewState["isVerify"] = value;
            }
        }

        private bool IsVerifyNif
        {
            get
            {
                var o = this.ViewState["isVerifyNif"];

                if (o == null)
                {
                    o = false;
                    this.ViewState["isVerifyNif"] = o;
                }

                return (bool)o;
            }

            set
            {
                this.ViewState["isVerifyNif"] = value;
            }
        }

        private bool IsVerifyIBAN
        {
            get
            {
                var o = this.ViewState["IsVerifyIBAN"];

                if (o == null)
                {
                    o = false;
                    this.ViewState["IsVerifyIBAN"] = o;
                }

                return (bool)o;
            }

            set
            {
                this.ViewState["IsVerifyIBAN"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "Providers";
            }
        }

        protected void RgProviders_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillProviders(false);
        }

        protected void RgProviders_OnInsertCommand(object sender, GridCommandEventArgs e)
        {

            var item = (GridEditFormItem)e.Item;
            var txtName = (RadTextBox)item.FindControl("txtName");
            var txtNifFirst = (RadTextBox)item.FindControl("txtNifFirst");
            var txtNifNumber = (RadTextBox)item.FindControl("txtNifNumber");
            var txtNifLast = (RadTextBox)item.FindControl("txtNifLast");
            var txtPhone = (RadTextBox)item.FindControl("txtPhone");
            var txtPerson = (RadTextBox)item.FindControl("txtPerson");
            var txtAddress = (RadTextBox)item.FindControl("txtAddress");
            var txtCp = (RadTextBox)item.FindControl("txtCp");
            var radProvince = (RadComboBox)item.FindControl("radDropProvince");
            var txtPopulation = (RadTextBox)item.FindControl("txtPopulation");
            var txtCcEntity = (RadTextBox)item.FindControl("txtCcEntity");
            var txtCcBranch = (RadTextBox)item.FindControl("txtCcBranch");
            var txtCcDc = (RadTextBox)item.FindControl("txtCcDc");
            var txtCcAccount = (RadTextBox)item.FindControl("txtCcAccount");
            var txtCcIban = (RadTextBox)item.FindControl("txtCcIban");
            var txtBranchAddress = (RadTextBox)item.FindControl("txtBranchAddress");
            var txtBranchEntity = (RadTextBox)item.FindControl("txtBranchEntity");
            var txtBranchCp = (RadTextBox)item.FindControl("txtBranchCp");
            var txtBranchLocation = (RadTextBox)item.FindControl("txtBranchLocation");
            var strBuilder = new StringBuilder();

            // si name y nifNumber, esta vacios devolvemos error
            if (string.IsNullOrWhiteSpace(txtName.Text) && string.IsNullOrWhiteSpace(txtNifNumber.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideProvidersGrid", "hideProvidersGrid();", true);
                strBuilder.Append("Se debe indicar un nombre de proveedor o numero de indentificacion valido");
                this.ShowMessage(this.RadNotification, "Error insertando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                e.Canceled = true;
                return;
            }

            var nif = $"{txtNifFirst.Text}-{txtNifNumber.Text}-{txtNifLast.Text}";

            var providers = this.providersService.GetProvidersByNameAndNif(txtName.Text, nif);

            if (providers.Any() && this.IsVerify == false)
            {
                var providersControl = (ProvidersControl)item.FindControl("providersControl");
                providersControl.Providers = providers;
                providersControl.Bind();

                this.IsVerify = true;

                strBuilder.Append("Antes de Grabar el Nuevo Proveedor compruebe que no se corresponda con alguno de los ya existentes.");
                this.ShowMessage(this.RadNotification, "Semejanzas encontradas", strBuilder, BasePage.MessageType.Warning);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showProvidersGrid", "showProvidersGrid();", true);
                e.Canceled = true;
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtNifNumber.Text))
            {
                IdentificationValidator identificationValidator = new IdentificationValidator();
                // Validación del NIF
                if (this.IsVerifyNif == false)
                {
                    if (!identificationValidator.ValidateDocument(nif.Replace("-", string.Empty).ToUpper()))
                    {
                        this.IsVerifyNif = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideProvidersGrid", "hideProvidersGrid();", true);
                        strBuilder.Append("El valor del CIF/NIF/NIE no es correcto, compruebelo antes de continuar.\n Si desea guardarlo de todas maneras pulse de nuevo Insertar.");
                        this.ShowMessage(this.RadNotification, "Error insertando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                        e.Canceled = true;
                        return;
                    }
                }
            }

            if (new[] { txtCcEntity, txtCcBranch, txtCcDc, txtCcAccount }.Any(field => !string.IsNullOrWhiteSpace(field.Text)))
            {

                if (this.IsVerifyIBAN == false)
                {
                    string realIban;

                    // Validación del IBAN
                    if (!IbanValidator.ValidateIban(txtCcEntity.Text, txtCcBranch.Text, txtCcDc.Text, txtCcAccount.Text, out realIban))
                    {
                        this.IsVerifyIBAN = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideProvidersGrid", "hideProvidersGrid();", true);
                        strBuilder.Append("Los datos bancarios no son correctos.\n Si desea guardarlo de todas maneras pulse de nuevo Insertar.s");
                        this.ShowMessage(this.RadNotification, "Error insertando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                        e.Canceled = true;
                        return;
                    }
                }

            }

            var provider = new PRE_PROVEEDOR
            {
                PROV_NOMBRE = txtName.Text,
                PROV_NIF = nif.ToUpper(),
                PROV_TELEFONO = txtPhone.Text,
                PROV_PERSONA_CONTACTO = txtPerson.Text,
                PROV_DIRECCION = txtAddress.Text,
                PROV_CODIGO_POSTAL = txtCp.Text,
                PROV_POBLACION = txtPopulation.Text,
                ProvID = radProvince.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(radProvince.SelectedValue),
                CodPaisID = radProvince.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(49),
                PROV_CC_CE = txtCcEntity.Text,
                PROV_CC_CO = txtCcBranch.Text,
                PROV_CC_DC = txtCcDc.Text,
                PROV_CC_NC = txtCcAccount.Text,
                prov_iban = txtCcIban.Text,
                PROV_DIR_SUCURSAL = txtBranchAddress.Text,
                PROV_NOMBRE_SUCURSAL = txtBranchEntity.Text,
                PROV_CP_SUCURSAL = txtBranchCp.Text,
                PROV_POBLACION_SUCURSAL = txtBranchLocation.Text,
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            try
            {
                var insert = this.providersService.InsertProvider(provider);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Proveedor.");
                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                }

            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Proveedor.");
                strBuilder.Append("Ha ocurrido un error insertando el Proveedor en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Proveedor", strBuilder, BasePage.MessageType.Deny);
            }
        }

        protected void RgProviders_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {

            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("PROV_CODIGO");
            var txtName = (RadTextBox)item.FindControl("txtName");
            var txtNifFirst = (RadTextBox)item.FindControl("txtNifFirst");
            var txtNifNumber = (RadTextBox)item.FindControl("txtNifNumber");
            var txtNifLast = (RadTextBox)item.FindControl("txtNifLast");

            var txtPhone = (RadTextBox)item.FindControl("txtPhone");
            var txtPerson = (RadTextBox)item.FindControl("txtPerson");
            var txtAddress = (RadTextBox)item.FindControl("txtAddress");
            var txtCp = (RadTextBox)item.FindControl("txtCp");
            var radProvince = (RadComboBox)item.FindControl("radDropProvince");
            var txtPopulation = (RadTextBox)item.FindControl("txtPopulation");
            var txtCcEntity = (RadTextBox)item.FindControl("txtCcEntity");
            var txtCcBranch = (RadTextBox)item.FindControl("txtCcBranch");
            var txtCcDc = (RadTextBox)item.FindControl("txtCcDc");
            var txtCcAccount = (RadTextBox)item.FindControl("txtCcAccount");
            var txtCcIban = (RadTextBox)item.FindControl("txtCcIban");
            var txtBranchAddress = (RadTextBox)item.FindControl("txtBranchAddress");
            var txtBranchEntity = (RadTextBox)item.FindControl("txtBranchEntity");
            var txtBranchCp = (RadTextBox)item.FindControl("txtBranchCp");
            var txtBranchLocation = (RadTextBox)item.FindControl("txtBranchLocation");

            // si name y nifNumber, esta vacios devolvemos error
            if (string.IsNullOrWhiteSpace(txtName.Text) && string.IsNullOrWhiteSpace(txtNifNumber.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideProvidersGrid", "hideProvidersGrid();", true);
                strBuilder.Append("Se debe indicar un nombre de proveedor o numero de indentificacion valido");
                this.ShowMessage(this.RadNotification, "Error insertando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                e.Canceled = true;
                return;
            }

            var nif = $"{txtNifFirst.Text}-{txtNifNumber.Text}-{txtNifLast.Text}";

            if (!string.IsNullOrWhiteSpace(txtNifNumber.Text))
            {

                if (this.IsVerifyNif == false)
                {
                    IdentificationValidator identificationValidator = new IdentificationValidator();

                    if (!identificationValidator.ValidateDocument(nif.Replace("-", string.Empty).ToUpper()))
                    {
                        this.IsVerifyNif = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideProvidersGrid", "hideProvidersGrid();", true);
                        strBuilder.Append("El valor del CIF/NIF/NIE no es correcto, compruebelo antes de continuar.\n Si desea guardarlo de todas maneras pulse de nuevo Actualizar.");
                        this.ShowMessage(this.RadNotification, "Error editando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                        e.Canceled = true;
                        return;
                    }
                }
            }

            if (new[] { txtCcEntity, txtCcBranch, txtCcDc, txtCcAccount }.Any(field => !string.IsNullOrWhiteSpace(field.Text)))
            {
                if (this.IsVerifyIBAN == false)
                {
                    string realIban;

                    // Validación del IBAN
                    if (!IbanValidator.ValidateIban(txtCcEntity.Text, txtCcBranch.Text, txtCcDc.Text, txtCcAccount.Text, out realIban))
                    {
                        this.IsVerifyIBAN =  true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideProvidersGrid", "hideProvidersGrid();", true);
                        strBuilder.Append("Los datos bancarios no son correctos. \n Si desea guardarlo de todas maneras pulse de nuevo Actualizar.");
                        this.ShowMessage(this.RadNotification, "Error insertando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                        e.Canceled = true;
                        return;
                    }
                }
            }

            var provider = new PRE_PROVEEDOR
            {
                PROV_CODIGO = id,
                PROV_NOMBRE = txtName.Text,
                PROV_NIF = nif.ToUpper(),
                PROV_TELEFONO = txtPhone.Text,
                PROV_PERSONA_CONTACTO = txtPerson.Text,
                PROV_DIRECCION = txtAddress.Text,
                PROV_CODIGO_POSTAL = txtCp.Text,
                PROV_POBLACION = txtPopulation.Text,
                ProvID = radProvince.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(radProvince.SelectedValue),
                CodPaisID = radProvince.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(49),
                PROV_CC_CE = txtCcEntity.Text,
                PROV_CC_CO = txtCcBranch.Text,
                PROV_CC_DC = txtCcDc.Text,
                PROV_CC_NC = txtCcAccount.Text,
                prov_iban = txtCcIban.Text,
                PROV_DIR_SUCURSAL = txtBranchAddress.Text,
                PROV_NOMBRE_SUCURSAL = txtBranchEntity.Text,
                PROV_CP_SUCURSAL = txtBranchCp.Text,
                PROV_POBLACION_SUCURSAL = txtBranchLocation.Text,
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            try
            {
                var update = this.providersService.UpdateProvider(provider);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Proveedor.");
                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Proveedor en cuestión.");
                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Proveedor", strBuilder, BasePage.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Proveedor.");
                strBuilder.Append("Ha ocurrido un error modificando el Proveedor en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Proveedor", strBuilder, BasePage.MessageType.Deny);
            }
        }

        protected void RgProviders_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var providerId = (int)item.GetDataKeyValue("PROV_CODIGO");

            try
            {
                var delete = this.providersService.DeleteProvider(providerId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Proveedor.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse el Proveedor porque tiene asociado algún expediente.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Proveedor en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Proveedor", strBuilder, BasePage.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Proveedor.");
                strBuilder.Append("Ha ocurrido un error eliminando el Proveedor en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Proveedor", strBuilder, BasePage.MessageType.Deny);
            }
        }

        protected void RgProviders_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgProviders.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgProviders.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgProviders.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgProviders_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Proveedor";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Proveedor";
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                this.IsVerify = false;
                this.IsVerifyNif = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideProvidersGrid", "hideProvidersGrid();", true);

                var item = (GridEditableItem)e.Item;
                var radProvince = (RadComboBox)item.FindControl("radDropProvince");
                var provinces = this.provincesService.GetProvincesToCombo();
                provinces.Insert(
                                 0,
                                 new Provincia
                                 {
                                     ProvIdInt = -1,
                                     Provincia1 = "< Seleccione >"
                                 });
                radProvince.DataSource = provinces.ToList();
                radProvince.DataBind();

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgProviders.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    var nif = item.GetDataKeyValue("PROV_NIF");

                    if (nif != null)
                    {
                        var txtNifFirst = (RadTextBox)item.FindControl("txtNifFirst");
                        var txtNifNumber = (RadTextBox)item.FindControl("txtNifNumber");
                        var txtNifLast = (RadTextBox)item.FindControl("txtNifLast");

                        var split = nif.ToString().Split('-');

                        for (var i = 0; i < split.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    txtNifFirst.Text = split[i];

                                    break;
                                case 1:
                                    txtNifNumber.Text = split[i];

                                    break;
                                case 2:
                                    txtNifLast.Text = split[i];

                                    break;
                            }
                        }
                    }

                    var provinceId = item.GetDataKeyValue("ProvID");

                    radProvince.SelectedValue = provinceId != null ? provinceId.ToString() : "-1";
                }
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillProviders(true);
        }

        private void FillProviders(bool manual)
        {
            var name = string.IsNullOrWhiteSpace(this.RtbName.Text) ? string.Empty : this.RtbName.Text;
            var nifFirst = string.IsNullOrWhiteSpace(this.RtbNifFirst.Text) ? string.Empty : this.RtbNifFirst.Text;
            var nifNumber = string.IsNullOrWhiteSpace(this.RtbNifNumber.Text) ? string.Empty : this.RtbNifNumber.Text;
            var nifLast = string.IsNullOrWhiteSpace(this.RtbNifLast.Text) ? string.Empty : this.RtbNifLast.Text;
            var nifAux = $"{nifFirst}-{nifNumber}-{nifLast}";
            var providerNif = nifAux.Equals("--") ? string.Empty : nifAux;

            var providers = this.providersService.FindProviders(name, providerNif);

            this.RgProviders.DataSource = providers;

            if (manual)
            {
                this.RgProviders.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static string AddProvider(
        string name, string nifFirst, string nifNumber, string nifLast, string phone,
        string person, string address, string cp, string province, string population,
        string ccc, string entity, string branch, string dc, string account,
        string branchAddress, string branchEntity, string branchCp, string branchLocation)
        {

            IdentificationValidator identificationValidator = new IdentificationValidator();
            JavaScriptSerializer js = new JavaScriptSerializer();
            IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

            // si name y nifNumber, esta vacios devolvemos error
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(nifNumber))
            {
                return js.Serialize(new
                {
                    status = "fail",
                    message = "Se debe indicar un Nombre o NIF como mínimo para poder insertar el proveedor",
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            // Validar existencia de proveedor con mismo nombre y NIF
            // Crear NIF completo
            string nif    = $"{nifFirst}-{nifNumber}-{nifLast}";
            var providers = providersService.GetProvidersByNameAndNif(name, nif);
            if (providers.Any())
            {
                return js.Serialize(new
                {
                    status = "fail",
                    message = "Proveedor ya existe con el mismo nombre o NIF",
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            if (!string.IsNullOrWhiteSpace(nifNumber))
            {
                // Validación del NIF
                if (!identificationValidator.ValidateDocument(nif.Replace("-", string.Empty).ToUpper()))
                {
                    return js.Serialize(new
                    {
                        status = "fail",
                        message = "El valor del CIF/NIF/NIE no es correcto.",
                        date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }
            }

            if (new[] { entity, branch, dc, account }.Any(field => !string.IsNullOrWhiteSpace(field)))
            {
                string realIban;

                // Validación del IBAN
                if (!IbanValidator.ValidateIban(entity, branch, dc, account, out realIban))
                {
                    return js.Serialize(new
                    {
                        status = "fail",
                        message = "Los datos bancarios no son correctos",
                        date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }
            }

            // Crear instancia de proveedor con datos proporcionados
            var provider = new PRE_PROVEEDOR
            {
                PROV_NOMBRE = name,
                PROV_NIF = nif.ToUpper(),
                PROV_TELEFONO = phone,
                PROV_PERSONA_CONTACTO = person,
                PROV_DIRECCION = address,
                PROV_CODIGO_POSTAL = cp,
                PROV_POBLACION = population,
                ProvID = province.Equals("-1") ? (byte?)null : Convert.ToByte(province),
                CodPaisID = province.Equals("-1") ? (int?)null : Convert.ToInt32(49),
                PROV_CC_CE = entity,
                PROV_CC_CO = branch,
                PROV_CC_DC = dc,
                PROV_CC_NC = account,
                prov_iban = ccc,
                PROV_DIR_SUCURSAL = branchAddress,
                PROV_NOMBRE_SUCURSAL = branchEntity,
                PROV_CP_SUCURSAL = branchCp,
                PROV_POBLACION_SUCURSAL = branchLocation,
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            try
            {
                var insert = providersService.InsertProvider(provider);

                if (insert.ResponseCode == ResponseCode.Invalid)
                {
                    return js.Serialize(new
                    {
                        status = "fail",
                        message = "Debe completar todos los datos del Proveedor.",
                        date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }

                return js.Serialize(new
                {
                    status = "success",
                    message = "Proveedor agregado correctamente",
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            catch (Exception ex)
            {
                // Loguear error si es necesario
                return js.Serialize(new
                {
                    status = "fail",
                    message = "Ha ocurrido un error insertando el Proveedor.",
                    error = ex.Message,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
        }

    }

    #endregion
}