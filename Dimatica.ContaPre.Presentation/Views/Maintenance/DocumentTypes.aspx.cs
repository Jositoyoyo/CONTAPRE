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
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class DocumentTypes : BasePage
    {
        #region Fields

        IDocumentTypesService documentTypesService = DependencyFactory.GetInstance<IDocumentTypesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "DocumentTypes";

                if (!string.IsNullOrWhiteSpace(LoginUser?.USU_I_G))
                {
                    this.RcTypes.SelectedValue = LoginUser.USU_I_G;
                }
            }
        }

        protected void RgDocumentTypes_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(this.RcTypes.SelectedValue) ? string.Empty : this.RcTypes.SelectedValue;
            this.FillDocumentTypes(type, false);
        }

        protected void RgDocumentTypes_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var radType = (RadComboBox)item.FindControl("radDropType");
            var radSing = (RadComboBox)item.FindControl("radDropSing");
            var txtKey = (RadNumericTextBox)item.FindControl("txtKey");
            var txtShortName = (RadTextBox)item.FindControl("txtShortName");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var checkRc = (RadCheckBox)item.FindControl("checkRc");
            var checkAd = (RadCheckBox)item.FindControl("checkAd");
            var checkO = (RadCheckBox)item.FindControl("checkO");
            var checkP = (RadCheckBox)item.FindControl("checkP");
            var checkDr = (RadCheckBox)item.FindControl("checkDr");
            var checkMi = (RadCheckBox)item.FindControl("checkMi");
            //var checkMiWithOutDr = (RadCheckBox)item.FindControl("checkMiWithOutDr");

            var documentType = new PRE_TIPO_DOCUMENTO
            {
                TIPD_I_G = radType.SelectedValue,
                TIPD_POSITIVO = radSing.SelectedValue.Equals("+"),
                TIPD_CLAVE = Convert.ToInt32(txtKey.Value),
                TIPD_NOMBRE_CORTO = txtShortName.Text,
                TIPD_DESCRIPCION = txtDescription.Text,
                TIPD_FASE_RC_G = (bool)checkRc.Checked,
                TIPD_FASE_AD_G = (bool)checkAd.Checked,
                TIPD_FASE_O_G = (bool)checkO.Checked,
                TIPD_FASE_P_G = (bool)checkP.Checked,
                TIPD_FASE_DR_I = (bool)checkDr.Checked,
                TIPD_FASE_MI_I = (bool)checkMi.Checked,
                TIPD_FASE_MIsinDR_I = false
            };

            try
            {
                var insert = this.documentTypesService.InsertDocumentType(documentType);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Documento.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Tipo de Documento", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Tipo de Documento.");
                strBuilder.Append("Ha ocurrido un error insertando el Tipo de Documento en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Tipo de Documento", strBuilder, MessageType.Deny);
            }
        }

        protected void RgDocumentTypes_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = (GridEditFormItem)e.Item;
            var id = (int)item.GetDataKeyValue("TIPD_CODIGO");
            var radType = (RadComboBox)item.FindControl("radDropType");
            var radSing = (RadComboBox)item.FindControl("radDropSing");
            var txtKey = (RadNumericTextBox)item.FindControl("txtKey");
            var txtShortName = (RadTextBox)item.FindControl("txtShortName");
            var txtDescription = (RadTextBox)item.FindControl("txtDescription");
            var checkRc = (RadCheckBox)item.FindControl("checkRc");
            var checkAd = (RadCheckBox)item.FindControl("checkAd");
            var checkO = (RadCheckBox)item.FindControl("checkO");
            var checkP = (RadCheckBox)item.FindControl("checkP");
            var checkDr = (RadCheckBox)item.FindControl("checkDr");
            var checkMi = (RadCheckBox)item.FindControl("checkMi");
            //var checkMiWithOutDr = (RadCheckBox)item.FindControl("checkMiWithOutDr");

            var documentType = new PRE_TIPO_DOCUMENTO
            {
                TIPD_CODIGO = id,
                TIPD_I_G = radType.SelectedValue,
                TIPD_POSITIVO = radSing.SelectedValue.Equals("+"),
                TIPD_CLAVE = Convert.ToInt32(txtKey.Value),
                TIPD_NOMBRE_CORTO = txtShortName.Text,
                TIPD_DESCRIPCION = txtDescription.Text,
                TIPD_FASE_RC_G = (bool)checkRc.Checked,
                TIPD_FASE_AD_G = (bool)checkAd.Checked,
                TIPD_FASE_O_G = (bool)checkO.Checked,
                TIPD_FASE_P_G = (bool)checkP.Checked,
                TIPD_FASE_DR_I = (bool)checkDr.Checked,
                TIPD_FASE_MI_I = (bool)checkMi.Checked,
                TIPD_FASE_MIsinDR_I = false
            };

            try
            {
                var update = this.documentTypesService.UpdateDocumentType(documentType);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Documento.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Tipo de Documento en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando el Tipo de Documento", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando el Tipo de Documento.");
                strBuilder.Append("Ha ocurrido un error modificando el Tipo de Documento en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando el Tipo de Documento", strBuilder, MessageType.Deny);
            }
        }

        protected void RgDocumentTypes_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var documentTypeId = (int)item.GetDataKeyValue("TIPD_CODIGO");

            try
            {
                var delete = this.documentTypesService.DeleteDocumentType(documentTypeId);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Tipo de Documento.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("No puede eliminarse el Tipo de Documento porque tiene asociado algún documento.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Tipo de Documento en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Tipo de Documento", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Imposible eliminar Tipo de Documento.");
                strBuilder.Append("Ha ocurrido un error eliminando el Tipo de Documento en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Tipo de Documento", strBuilder, MessageType.Deny);
            }
        }

        protected void RgDocumentTypes_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgDocumentTypes.MasterTableView.GetColumn("EditColumn").Visible = false;
                this.RgDocumentTypes.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgDocumentTypes.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }

            var selected = this.RcTypes.SelectedValue;

            this.RgDocumentTypes.MasterTableView.GetColumn("TIPD_FASE_RC_G").Visible = selected.Equals("G");
            this.RgDocumentTypes.MasterTableView.GetColumn("TIPD_FASE_AD_G").Visible = selected.Equals("G");
            this.RgDocumentTypes.MasterTableView.GetColumn("TIPD_FASE_O_G").Visible = selected.Equals("G");
            this.RgDocumentTypes.MasterTableView.GetColumn("TIPD_FASE_P_G").Visible = selected.Equals("G");

            this.RgDocumentTypes.MasterTableView.GetColumn("TIPD_FASE_DR_I").Visible = selected.Equals("I");
            this.RgDocumentTypes.MasterTableView.GetColumn("TIPD_FASE_MI_I").Visible = selected.Equals("I");
            //this.RgDocumentTypes.MasterTableView.GetColumn("TIPD_FASE_MIsinDR_I").Visible = selected.Equals("I");
        }

        protected void RgDocumentTypes_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                var radType = (RadComboBox)item.FindControl("radDropType");
                var selected = this.RcTypes.SelectedValue;
                radType.SelectedValue = selected;

                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgDocumentTypes.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
                else
                {
                    radType.Enabled = false;

                    var txtKey = (RadNumericTextBox)item.FindControl("txtKey");
                    var key = (int)item.GetDataKeyValue("TIPD_CLAVE");
                    txtKey.Value = Convert.ToDouble(key);

                    var radDropSing = (RadComboBox)item.FindControl("radDropSing");
                    var sing = item.GetDataKeyValue("SingLabel").ToString();
                    radDropSing.SelectedValue = sing;

                    var checkRc = (RadCheckBox)item.FindControl("checkRc");
                    var checkAd = (RadCheckBox)item.FindControl("checkAd");
                    var checkO = (RadCheckBox)item.FindControl("checkO");
                    var checkP = (RadCheckBox)item.FindControl("checkP");
                    var checkDr = (RadCheckBox)item.FindControl("checkDr");
                    var checkMi = (RadCheckBox)item.FindControl("checkMi");
                    //var checkMiWithOutDr = (RadCheckBox)item.FindControl("checkMiWithOutDr");

                    var rc = (bool)item.GetDataKeyValue("TIPD_FASE_RC_G");
                    checkRc.Checked = rc;
                    var ad = (bool)item.GetDataKeyValue("TIPD_FASE_AD_G");
                    checkAd.Checked = ad;
                    var o = (bool)item.GetDataKeyValue("TIPD_FASE_O_G");
                    checkO.Checked = o;
                    var p = (bool)item.GetDataKeyValue("TIPD_FASE_P_G");
                    checkP.Checked = p;

                    var dr = (bool)item.GetDataKeyValue("TIPD_FASE_DR_I");
                    checkDr.Checked = dr;
                    var mi = (bool)item.GetDataKeyValue("TIPD_FASE_MI_I");
                    checkMi.Checked = mi;
                    //var miWithOutDr = (bool)item.GetDataKeyValue("TIPD_FASE_MIsinDR_I");
                    //checkMiWithOutDr.Checked = miWithOutDr;
                }

                switch (selected)
                {
                    case "G":
                        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideEntryChecks", "hideEntryChecks();", true);
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "hideEntryChecks", "hideEntryChecks();", true);
                        break;
                    case "I":
                        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideSpendChecks", "hideSpendChecks();", true);
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "hideSpendChecks", "hideSpendChecks();", true);
                        break;
                }
            }
        }

        protected void RcTypes_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var type = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            this.FillDocumentTypes(type, true);
        }

        private void FillDocumentTypes(string type, bool manual)
        {
            var documentTypes = new List<PRE_TIPO_DOCUMENTO>();

            if (!string.IsNullOrEmpty(type))
            {
                var documentTypesResult = this.documentTypesService.GetDocumentTypes(type);

                foreach (var document in documentTypesResult)
                {
                    documentTypes.Add(document);
                }
            }

            this.RgDocumentTypes.DataSource = documentTypes;

            if (manual)
            {
                this.RgDocumentTypes.DataBind();
            }
        }

        #endregion
    }
}