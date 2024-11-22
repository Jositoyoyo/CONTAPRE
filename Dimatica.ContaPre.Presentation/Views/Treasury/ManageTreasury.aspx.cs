namespace Dimatica.ContaPre.Presentation.Views.Treasury
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class ManageTreasury : BasePage
    {
        #region Static Fields and Constants

        private static IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private static ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Fields

        private IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        private IOriginsService originsService = DependencyFactory.GetInstance<IOriginsService>();

        private IRecordTypesService recordTypesService = DependencyFactory.GetInstance<IRecordTypesService>();

        #endregion

        #region Private Properties

        private PRE_TESORERIA Treasury
        {
            get
            {
                var file = this.Session["_treasury"] as PRE_TESORERIA;

                if (file == null)
                {
                    file = new PRE_TESORERIA();

                    this.Session["_treasury"] = file;
                }

                return file;
            }

            set
            {
                this.Session["_treasury"] = value;
            }
        }

        private int BoundsCount
        {
            get
            {
                var count = this.Session["_boundsCount"];

                if (count == null)
                {
                    count = 0;
                    this.Session["_boundsCount"] = count;
                }

                return Convert.ToInt32(count);
            }

            set
            {
                this.Session["_boundsCount"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteTreasury()
        {
            try
            {
                var id = ((PRE_TESORERIA)HttpContext.Current.Session["_treasury"]).TES_CODIGO;

                if (id == 0)
                {
                    return 2;
                }

                var foundInOracle = false;
                var recognizedRights = string.Empty;

                var connectToToOracle = Convert.ToBoolean(ConfigurationManager.AppSettings["connectToOracle"]);
                if (connectToToOracle)
                {
                    var found = treasuriesService.FoundTreasuryOracle(id);

                    if (found)
                    {
                        foundInOracle = true;
                        recognizedRights = treasuriesService.GetRecognizedRightsOracle(id);
                    }
                }

                if (string.IsNullOrWhiteSpace(recognizedRights))
                {
                    var documents = treasuriesService.GetDocuments(null, null, id);

                    foreach (var document in documents)
                    {
                        if (document.DOC_CODIGO != null)
                        {
                            var bills = accountingDocumentsService.GetPurchases((int)document.DOC_CODIGO);

                            foreach (var bill in bills)
                            {
                                var updatePayBank = accountingDocumentsService.UpdatePayBankOracle((DateTime?)null, (int)bill.CODFACTURAGEI);

                                var updateHistory = accountingDocumentsService.UpdateHistoryOracle(bill.NCERTIFICADO, "Se ha eliminado el apunte de tesorería vinculado al pago de la factura.");
                            }
                        }
                    }

                    if (foundInOracle)
                    {
                        var deleteTreasuryOracle = treasuriesService.DeleteTreasuryOracle(id);

                        if (deleteTreasuryOracle.ResponseCode != ResponseCode.Ok)
                        {
                            return 4;
                        }
                    }

                    var delete = treasuriesService.DeleteTreasury(id, LoginUser.USU_CODIGO);

                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            return 0;
                        case ResponseCode.Ok:
                            return 1;
                        case ResponseCode.NotFound:
                            return 2;
                    }
                }
                else
                {
                    return 3;
                }

                return 0;
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el apunte de tesorería.");
                return 0;
            }
        }

        #endregion

        #region Public Methods

        public void FillRestrictedAccounts(string type)
        {
            var restrictedAccount = this.accountRestrictedService.GetAccountsRestricted(type);
            restrictedAccount.Insert(
                                     0,
                                     new PRE_CUENTA_RESTRINGIDA
                                     {
                                         CUE_CODIGO = -1
                                     });
            this.RcRestrictedAccount.DataSource = restrictedAccount;
            this.RcRestrictedAccount.DataBind();
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var treasuryId = this.Request.QueryString["id"];

            if (!this.IsPostBack)
            {
                this.Treasury = null;

                this.RmyYear.SelectedDate = DateTime.Now;

                this.RdpEntryDate.SelectedDate = DateTime.Now;

                var recordTypes = this.recordTypesService.GetRecordTypes();
                recordTypes.Insert(
                                   0,
                                   new PRE_TIPO_REGISTRO
                                   {
                                       TIPR_CODIGO = -1
                                   });
                this.RcRegisterTypeCode.DataSource = recordTypes;
                this.RcRegisterTypeCode.DataBind();

                var originsAux = this.originsService.GetOrigins();

                var origins = new List<PRE_ORIGEN>();

                foreach (var origin in originsAux)
                {
                    if (origin.ORI_CODIGO_AUX == 1 || origin.ORI_CODIGO_AUX == 2 || origin.ORI_CODIGO_AUX == 5)
                    {
                        origins.Add(origin);
                    }
                }

                this.RcOriginCode.DataSource = origins;
                this.RcOriginCode.DataBind();

                this.FillRestrictedAccounts("G");

                if (treasuryId == null)
                {
                    this.titleHeader.InnerText = "Nuevo Apunte de Tesorería";
                }
                else
                {
                    this.titleHeader.InnerText = "Editar Apunte de Tesorería";

                    var treasury = treasuriesService.GetById(Convert.ToInt32(treasuryId));

                    if (treasury == null)
                    {
                        this.Response.Redirect("~/Views/Treasury/ManageTreasury.aspx");
                    }
                    else
                    {
                        this.Treasury = treasury;

                        this.RmyYear.SelectedDate = new DateTime(Convert.ToInt32(this.Treasury.TES_ANO_PRESUPUESTO), 1, 1);

                        if (this.Treasury.TIPR_CODIGO != null)
                        {
                            this.RcRegisterTypeCode.SelectedValue = ((int)this.Treasury.TIPR_CODIGO).ToString();
                        }

                        if (this.Treasury.ORI_CODIGO != null)
                        {
                            this.RcOriginCode.SelectedValue = ((byte)this.Treasury.ORI_CODIGO).ToString();

                            var type = string.Empty;

                            switch (this.RcOriginCode.SelectedValue)
                            {
                                case "1":
                                    type = "G";

                                    break;
                                case "2":
                                    type = "I";

                                    break;
                                case "5":
                                    type = string.Empty;

                                    break;
                            }

                            this.FillRestrictedAccounts(type);
                        }

                        if (this.Treasury.CUE_CODIGO != null)
                        {
                            this.RcRestrictedAccount.SelectedValue = ((int)this.Treasury.CUE_CODIGO).ToString();
                        }

                        this.RdpEntryDate.SelectedDate = this.Treasury.TES_FECHA_APUNTE;
                        this.RdpBankDate.SelectedDate = this.Treasury.TES_FECHA_BANCO;
                        this.RtbApplication.Text = this.Treasury.TES_APLICACION;

                        if (this.Treasury.FOR_CODIGO != null)
                        {
                            this.RcPayType.SelectedValue = ((byte)this.Treasury.FOR_CODIGO).ToString();
                        }

                        this.RtbCheckNumber.Text = this.Treasury.TES_NUMERO_CHEQUE;
                        this.RntTreasuryAmount.Value = Convert.ToDouble(this.Treasury.TES_TOTAL_IMPORTE_LIQUIDO);

                        if ((bool)this.Treasury.TES_HABER)
                        {
                            this.RcTreasuryHave.SelectedValue = "1";
                        }
                        else
                        {
                            this.RcTreasuryHave.SelectedValue = "0";
                        }

                        this.RtbDescription.Text = this.Treasury.TES_DESCRIPCION;
                        this.checkFinish.Checked = Convert.ToInt32(this.Treasury.TES_MARCA_0_1_255) != 0;
                        this.checkCanceled.Checked = this.Treasury.TES_ANULADO;

                        var boundsCount = treasuriesService.GetBoundDocumentsCount(this.Treasury.TES_CODIGO);
                        this.BoundsCount = boundsCount;
                    }
                }
            }
        }

        protected void RcOriginCode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var originCode = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;

            var type = string.Empty;

            switch (originCode)
            {
                case "1":
                    type = "G";

                    break;
                case "2":
                    type = "I";

                    break;
                case "5":
                    type = string.Empty;

                    break;
            }

            this.FillRestrictedAccounts(type);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            if (this.Treasury.TES_CODIGO == 0)
            {
                var originCode = Convert.ToByte(this.RcOriginCode.SelectedValue);

                var treasury = new PRE_TESORERIA
                {
                    ORI_CODIGO = originCode,
                    TES_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyYear.SelectedDate).Year),
                    TIPR_CODIGO = this.RcRegisterTypeCode.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcRegisterTypeCode.SelectedValue),
                    TES_FECHA_APUNTE = this.RdpEntryDate.SelectedDate,
                    TES_FECHA_BANCO = this.RdpBankDate.SelectedDate,
                    TES_APLICACION = this.RtbApplication.Text,
                    FOR_CODIGO = this.RcPayType.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(this.RcPayType.SelectedValue),
                    TES_NUMERO_CHEQUE = this.RtbCheckNumber.Text,
                    CUE_CODIGO = this.RcRestrictedAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcRestrictedAccount.SelectedValue),
                    TES_TOTAL_IMPORTE_LIQUIDO = Convert.ToDecimal(this.RntTreasuryAmount.Value),
                    TES_HABER = this.RcTreasuryHave.SelectedValue.Equals("1"),
                    TES_DESCRIPCION = this.RtbDescription.Text,
                    TES_ANULADO = (bool)this.checkCanceled.Checked,
                    TES_MARCA_0_1_255 = (byte?)((bool)this.checkFinish.Checked ? 1 : 0),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                try
                {
                    var insert = treasuriesService.InsertTreasury(treasury);

                    switch (insert.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos del Apunte de Tesorería.");

                            break;
                    }

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error insertando el Apunte de Tesorería", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        var id = (int)insert.ResponseMethod;

                        var connectToToOracle = Convert.ToBoolean(ConfigurationManager.AppSettings["connectToOracle"]);
                        if (connectToToOracle)
                        {
                            if ((bool)treasury.TES_HABER == false && originCode == 2)
                            {
                                treasury.TES_CODIGO = id;
                                var insertOracle = treasuriesService.InsertTreasuryOracle(treasury);

                                if (insertOracle.ResponseCode != ResponseCode.Ok)
                                {
                                    strBuilder.Append("Ha ocurrido un error insertando el apunte de tesoreria en Oracle.");
                                    this.ShowMessage(this.RadNotification, "Error Oracle", strBuilder, MessageType.Warning);
                                }
                            }
                        }

                        this.Response.Redirect($"~/Views/Treasury/ManageTreasury.aspx?id={id}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error insertando el Apunte de Tesorería");
                    strBuilder.Append("Ha ocurrido un error insertando el Apunte de Tesorería en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error insertando el Apunte de Tesorería", strBuilder, MessageType.Deny);
                }
            }
            else
            {
                var originCode = Convert.ToByte(this.RcOriginCode.SelectedValue);

                var treasury = new PRE_TESORERIA
                {
                    TES_CODIGO = this.Treasury.TES_CODIGO,
                    ORI_CODIGO = originCode,
                    TES_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyYear.SelectedDate).Year),
                    TIPR_CODIGO = this.RcRegisterTypeCode.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcRegisterTypeCode.SelectedValue),
                    TES_FECHA_APUNTE = this.RdpEntryDate.SelectedDate,
                    TES_FECHA_BANCO = this.RdpBankDate.SelectedDate,
                    TES_APLICACION = this.RtbApplication.Text,
                    FOR_CODIGO = this.RcPayType.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(this.RcPayType.SelectedValue),
                    TES_NUMERO_CHEQUE = this.RtbCheckNumber.Text,
                    CUE_CODIGO = this.RcRestrictedAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcRestrictedAccount.SelectedValue),
                    TES_TOTAL_IMPORTE_LIQUIDO = Convert.ToDecimal(this.RntTreasuryAmount.Value),
                    TES_HABER = this.RcTreasuryHave.SelectedValue.Equals("1"),
                    TES_DESCRIPCION = this.RtbDescription.Text,
                    TES_ANULADO = (bool)this.checkCanceled.Checked,
                    TES_MARCA_0_1_255 = (byte?)((bool)this.checkFinish.Checked ? 1 : 0),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                try
                {
                    var statusOracle = string.Empty;

                    var connectToToOracle = Convert.ToBoolean(ConfigurationManager.AppSettings["connectToOracle"]);
                    if (connectToToOracle)
                    {
                        var found = treasuriesService.FoundTreasuryOracle(treasury.TES_CODIGO);
                        if (found)
                        {
                            var recognizedRights = treasuriesService.GetRecognizedRightsOracle(treasury.TES_CODIGO);

                            if (string.IsNullOrWhiteSpace(recognizedRights))
                            {
                                if ((bool)treasury.TES_HABER || originCode != 2)
                                {
                                    statusOracle = "deleteOracle";
                                }
                                else
                                {
                                    statusOracle = "updateOracle";
                                }
                            }
                            else
                            {
                                var originalTreasury = treasuriesService.GetById(treasury.TES_CODIGO);

                                var saveTreasury = true;

                                if (originalTreasury != null)
                                {
                                    if (originalTreasury.TES_ANO_PRESUPUESTO != null && saveTreasury)
                                    {
                                        if (treasury.TES_ANO_PRESUPUESTO != originalTreasury.TES_ANO_PRESUPUESTO)
                                        {
                                            saveTreasury = false;
                                        }
                                    }

                                    if (originalTreasury.TES_FECHA_APUNTE != null && saveTreasury)
                                    {
                                        if (treasury.TES_FECHA_APUNTE != originalTreasury.TES_FECHA_APUNTE)
                                        {
                                            saveTreasury = false;
                                        }
                                    }

                                    if (originalTreasury.TES_FECHA_BANCO != null && saveTreasury)
                                    {
                                        if (treasury.TES_FECHA_BANCO != originalTreasury.TES_FECHA_BANCO)
                                        {
                                            saveTreasury = false;
                                        }
                                    }

                                    if (originalTreasury.TES_HABER != null && saveTreasury)
                                    {
                                        if ((bool)treasury.TES_HABER)
                                        {
                                            saveTreasury = false;
                                        }
                                    }

                                    if (originalTreasury.ORI_CODIGO != null && saveTreasury)
                                    {
                                        if (originCode != 2)
                                        {
                                            saveTreasury = false;
                                        }
                                    }

                                    if (originalTreasury.TES_DESCRIPCION != null && saveTreasury)
                                    {
                                        if (treasury.TES_DESCRIPCION != originalTreasury.TES_DESCRIPCION)
                                        {
                                            saveTreasury = false;
                                        }
                                    }

                                    if (originalTreasury.TES_TOTAL_IMPORTE_LIQUIDO != null && saveTreasury)
                                    {
                                        if (treasury.TES_TOTAL_IMPORTE_LIQUIDO != originalTreasury.TES_TOTAL_IMPORTE_LIQUIDO)
                                        {
                                            saveTreasury = false;
                                        }
                                    }
                                }
                                else
                                {
                                    saveTreasury = false;
                                }

                                if (!saveTreasury)
                                {
                                    strBuilder.Append("No se puede modificar el apunte de tesoreria porque está enlazado en Convenios.");
                                    this.ShowMessage(this.RadNotification, "Imposible modificar", strBuilder, MessageType.Warning);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if ((bool)treasury.TES_HABER == false && originCode == 2)
                            {
                                statusOracle = "insertOracle";
                            }
                        }
                    }

                    var update = treasuriesService.UpdateTreasury(treasury);

                    switch (update.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos del Apunte de Tesorería.");

                            break;
                        case ResponseCode.NotFound:
                            strBuilder.Append("No se ha encontrado el Apunte de Tesorería en cuestión.");

                            break;
                    }

                    if (update.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error modificando el Apunte de Tesorería", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        switch (statusOracle)
                        {
                            case "insertOracle":
                                var insertOracle = treasuriesService.InsertTreasuryOracle(treasury);
                                break;
                            case "updateOracle":
                                var updateOracle = treasuriesService.UpdateTreasuryOracle(treasury);
                                break;
                            case "deleteOracle":
                                var deleteOracle = treasuriesService.DeleteTreasuryOracle(treasury.TES_CODIGO);
                                break;
                        }

                        this.Response.Redirect($"~/Views/Treasury/ManageTreasury.aspx?id={this.Treasury.TES_CODIGO}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error modificando el Apunte de Tesorería");
                    strBuilder.Append("Ha ocurrido un error modificando el Apunte de Tesorería en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error modificando el Apunte de Tesorería", strBuilder, MessageType.Deny);

                }
            }
        }

        protected void btnDeleteBankDate_OnClick(object sender, EventArgs e)
        {
            if (this.Treasury.TES_CODIGO == 0)
            {
                this.RdpBankDate.SelectedDate = null;
            }
            else
            {
                var strBuilder = new StringBuilder();

                this.RdpBankDate.SelectedDate = null;

                try
                {
                    var statusOracle = string.Empty;

                    var connectToToOracle = Convert.ToBoolean(ConfigurationManager.AppSettings["connectToOracle"]);
                    if (connectToToOracle)
                    {
                        var found = treasuriesService.FoundTreasuryOracle(this.Treasury.TES_CODIGO);

                        if (found)
                        {
                            var recognizedRights = treasuriesService.GetRecognizedRightsOracle(this.Treasury.TES_CODIGO);

                            if (string.IsNullOrWhiteSpace(recognizedRights))
                            {
                                statusOracle = "updateOracle";
                            }
                            else
                            {
                                strBuilder.Append("No se puede modificar el apunte de tesoreria porque está enlazado en Convenios.");
                                this.ShowMessage(this.RadNotification, "Imposible modificar", strBuilder, MessageType.Warning);
                                return;
                            }
                        }
                    }

                    var deleteBankDate = treasuriesService.DeleteBankDate(this.Treasury.TES_CODIGO, LoginUser.USU_CODIGO);

                    switch (deleteBankDate.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos del Apunte de Tesorería.");

                            break;
                        case ResponseCode.NotFound:
                            strBuilder.Append("No se ha encontrado el Apunte de Tesorería en cuestión.");

                            break;
                    }

                    if (deleteBankDate.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error borrando la Fecha de Banco de el Apunte de Tesorería", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        if (statusOracle.Equals("updateOracle"))
                        {
                            var deleteBankDateOracle = treasuriesService.DeleteBankDateOracle(this.Treasury.TES_CODIGO);
                        }

                        this.Response.Redirect($"~/Views/Treasury/ManageTreasury.aspx?id={this.Treasury.TES_CODIGO}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error borrando la Fecha de Banco de el Apunte de Tesorería.");
                    strBuilder.Append("Ha ocurrido un error borrando la Fecha de Banco de el Apunte de Tesorería en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error borrando la Fecha de Banco de el Apunte de Tesorería", strBuilder, MessageType.Deny);
                }
            }
        }

        protected void btnSeeDocuments_OnClick(object sender, EventArgs e)
        {
            if (this.Treasury == null || this.Treasury.TES_CODIGO == 0)
            {
                return;
            }

            var script = $"seeBoundDocuments('{this.Treasury.TES_CODIGO}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeBoundDocuments", script, true);
        }

        protected void btnBound_OnClick(object sender, EventArgs e)
        {
            if (this.Treasury == null || this.Treasury.TES_CODIGO == 0)
            {
                return;
            }

            var script = $"boundDocuments('{this.Treasury.TES_CODIGO}', '{Convert.ToInt32(this.Treasury.TES_ANO_PRESUPUESTO)}', '{((DateTime)this.Treasury.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"))}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "boundDocuments", script, true);
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Treasury/ManageTreasury.aspx");
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Treasury/NotesTreasuries.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.Treasury == null || this.Treasury.TES_CODIGO == 0)
            {
                this.checkFinish.Enabled = false;
                this.btnBound.Enabled = false;
                this.btnSeeDocuments.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            else
            {
                if (this.BoundsCount > 0)
                {
                    if ((bool)this.checkFinish.Checked)
                    {
                        this.btnBound.Enabled = false;
                    }

                    this.btnSeeDocuments.Enabled = true;
                }
                else
                {
                    this.btnSeeDocuments.Enabled = false;
                }
            }
        }

        #endregion
    }
}