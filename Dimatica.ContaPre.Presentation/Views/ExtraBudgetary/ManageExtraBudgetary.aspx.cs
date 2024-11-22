namespace Dimatica.ContaPre.Presentation.Views.ExtraBudgetary
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    using Telerik.Web.UI.Calendar;

    #endregion

    public partial class ManageExtraBudgetary : BasePage
    {
        #region Static Fields and Constants

        private static IExtraBudgetaryRecordsService extraBudgetaryRecordsService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        private static ITonnageSheetsService tonnageSheetsService = DependencyFactory.GetInstance<ITonnageSheetsService>();

        private static ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Fields

        private IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        private IExtraBudgetaryApplicationsService extraBudgetaryApplicationsService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();

        private IPayFormsService payFormsService = DependencyFactory.GetInstance<IPayFormsService>();

        private IPayTypesService payTypesService = DependencyFactory.GetInstance<IPayTypesService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Properties

        private PRE_EXP_EXTRAPRE ExtraBudgetary
        {
            get
            {
                var file = this.Session["_extraBudgetary"] as PRE_EXP_EXTRAPRE;

                if (file == null)
                {
                    file = new PRE_EXP_EXTRAPRE();

                    this.Session["_extraBudgetary"] = file;
                }

                return file;
            }

            set
            {
                this.Session["_extraBudgetary"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteExtraBudgetary()
        {
            try
            {
                var id = ((PRE_EXP_EXTRAPRE)HttpContext.Current.Session["_extraBudgetary"]).EXP_EXTRAP_CODIGO;

                if (id == 0)
                {
                    return 3;
                }

                var type = ((PRE_EXP_EXTRAPRE)HttpContext.Current.Session["_extraBudgetary"]).TIP_EXTRAP_CODIGO;

                if (type == null)
                {
                    return 3;
                }

                if (type.ToString().Equals("1"))
                {
                    var delete = extraBudgetaryRecordsService.DeleteExtraBudgetaryDiscount(id);

                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            return 4;
                        case ResponseCode.Ok:
                            return 1;
                    }

                    return 0;
                }
                else
                {
                    var treasuryCode = ((PRE_EXP_EXTRAPRE)HttpContext.Current.Session["_extraBudgetary"]).TES_CODIGO;

                    if (treasuryCode != null)
                    {
                        var deleteTreasury = treasuriesService.DeleteDocument((int)treasuryCode, LoginUser.USU_CODIGO);
                    }

                    var tonnageSheet = ((PRE_EXP_EXTRAPRE)HttpContext.Current.Session["_extraBudgetary"]).HOJ_NUMERO;
                    var tonnageSheetYear = ((PRE_EXP_EXTRAPRE)HttpContext.Current.Session["_extraBudgetary"]).ANO_HOJA;

                    if (tonnageSheet != null && tonnageSheetYear != null)
                    {
                        var sheetCode = tonnageSheetsService.GetTonnageSheetCode((int)tonnageSheetYear, (int)tonnageSheet, false);

                        if (sheetCode != -1)
                        {
                            var deleteSheet = tonnageSheetsService.DeleteTonnageSheet(sheetCode, LoginUser.USU_CODIGO);
                        }
                    }

                    var tonnageSheet50 = ((PRE_EXP_EXTRAPRE)HttpContext.Current.Session["_extraBudgetary"]).HOJ_NUMERO50;
                    var tonnageSheetYear50 = ((PRE_EXP_EXTRAPRE)HttpContext.Current.Session["_extraBudgetary"]).ANO_HOJA50;

                    if (tonnageSheet50 != null && tonnageSheetYear50 != null)
                    {
                        var sheetCode = tonnageSheetsService.GetTonnageSheetCode((int)tonnageSheetYear50, (int)tonnageSheet50, true);

                        if (sheetCode != -1)
                        {
                            var deleteSheet = tonnageSheetsService.DeleteTonnageSheet(sheetCode, LoginUser.USU_CODIGO);
                        }
                    }

                    var delete = extraBudgetaryRecordsService.DeleteExtraBudgetary(id, LoginUser.USU_CODIGO);

                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            return 0;
                        case ResponseCode.Ok:
                            return 1;
                        case ResponseCode.NotFound:
                            return 2;
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el expediente extrapresupuestario.");
                return 0;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var extraBudgetaryId = this.Request.QueryString["id"];

            if (extraBudgetaryId == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideSing", $"hideSing();", true);
                this.RcbBinding.Enabled = false;
            }

            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "ManageExtraBudgetary";

                this.ExtraBudgetary = null;

                var types = this.extraBudgetaryApplicationsService.GetExtraBudgetaryTypes();

                types.Insert(
                             0,
                             new PRE_TIPO_EXTRAP
                                     {
                                             TIP_EXTRAP_CODIGO_AUX = -1,
                                             TIP_EXTRAP_DESCRIPCION = "< Seleccione >"
                                     });

                if (extraBudgetaryId == null)
                {
                    if (types.Any(t => t.TIP_EXTRAP_CODIGO_AUX == 1))
                    {
                        types.Remove(types.FirstOrDefault(t => t.TIP_EXTRAP_CODIGO_AUX == 1));
                    }
                }

                this.RcTypes.DataSource = types;
                this.RcTypes.DataBind();

                this.RdDate.SelectedDate = DateTime.Now;

                var extraBudgetaryApplications = this.extraBudgetaryApplicationsService.GetExtraBudgetaryApplicationsToCombo();

                extraBudgetaryApplications.Insert(
                                                  0,
                                                  new PRE_EXTRAPRESUPUESTARIA
                                                          {
                                                                  EXTRAPRE_CODIGO = -1,
                                                                  EXTRAPRE_DESCRIPCION = "< Seleccione >"
                                                          });

                this.RcExtraBudgetaryApplications.DataSource = extraBudgetaryApplications;
                this.RcExtraBudgetaryApplications.DataBind();

                var providers = this.providersService.GetProvidersToCombo();

                providers.Insert(
                                 0,
                                 new PRE_PROVEEDOR
                                         {
                                                 PROV_CODIGO = -1,
                                                 PROV_NOMBRE = "< Seleccione >"
                                         });

                this.RcInterested.DataSource = providers;
                this.RcInterested.DataBind();

                var payForms = this.payFormsService.GetPayForms();

                payForms.Insert(
                                0,
                                new PRE_FORMA_PAGO
                                        {
                                                FOR_CODIGO_AUX = -1
                                        });

                this.RcPayForms.DataSource = payForms;
                this.RcPayForms.DataBind();

                var percertors = this.accountRestrictedService.GetAccountsRestricted("G");

                percertors.Insert(
                                  0,
                                  new PRE_CUENTA_RESTRINGIDA
                                          {
                                                  CUE_CODIGO = -1
                                          });

                this.RcPercertor.DataSource = percertors.ToList();
                this.RcPercertor.DataBind();

                var payTypes = this.payTypesService.GetPayTypes();

                payTypes.Insert(
                                0,
                                new PRE_TIPO_PAGO
                                        {
                                                TIPP_CODIGO_AUX = -1
                                        });

                this.RcPayTypes.DataSource = payTypes.ToList();
                this.RcPayTypes.DataBind();

                this.RcThirds.DataSource = providers;
                this.RcThirds.DataBind();

                if (extraBudgetaryId == null)
                {
                    this.titleHeader.InnerText = "Nuevo Exped. Extrapresupuestario";
                    this.RmyYear.SelectedDate = DateTime.Now;

                    var number = this.extraBudgetaryApplicationsService.GetNextNumber(((DateTime)this.RmyYear.SelectedDate).Year);

                    this.RntFileNumber.Value = number;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideSing", $"hideSing();", true);
                    this.RcbBinding.Enabled = false;
                }
                else
                {
                    this.titleHeader.InnerText = "Editar Exped. Extrapresupuestario";

                    var extraBudgetary = extraBudgetaryRecordsService.GetById(Convert.ToInt32(extraBudgetaryId));

                    if (extraBudgetary == null)
                    {
                        this.Response.Redirect("~/Views/ExtraBudgetary/ExtraBudgetaries.aspx");
                    }
                    else
                    {
                        this.ExtraBudgetary = extraBudgetary;

                        if (this.ExtraBudgetary.TIP_EXTRAP_CODIGO != null)
                        {
                            this.RcTypes.SelectedValue = this.ExtraBudgetary.TIP_EXTRAP_CODIGO.ToString();
                        }

                        var year = Convert.ToInt32(this.ExtraBudgetary.EXP_EXTRAP_ANO_PRESUPUESTO);
                        this.RmyYear.SelectedDate = new DateTime(year, 1, 1);

                        this.RntFileNumber.Value = this.ExtraBudgetary.EXP_EXTRAP_NUMERO;
                        this.RdDate.SelectedDate = this.ExtraBudgetary.EXP_EXTRAP_FECHA;
                        this.RcExtraBudgetaryApplications.SelectedValue = this.ExtraBudgetary.EXTRAPRE_CODIGO.ToString();

                        if (this.ExtraBudgetary.EXP_EXTRAP_IMPORTE != null)
                        {
                            this.RntAmount.Value = Convert.ToDouble(this.ExtraBudgetary.EXP_EXTRAP_IMPORTE);
                        }

                        var restrictedAccount = new List<PRE_CUENTA_RESTRINGIDA>();

                        switch (this.RcTypes.SelectedValue)
                        {
                            case "2":
                            case "4":
                                restrictedAccount = this.accountRestrictedService.GetAccountsRestricted("I");
                                this.RntRestrictedAccount.DataSource = restrictedAccount.ToList();
                                this.RntRestrictedAccount.DataBind();

                                this.RntRestrictedAccount.SelectedValue = "10";

                                break;
                            case "1":
                            case "3":
                            case "5":
                                restrictedAccount = this.accountRestrictedService.GetAccountsRestricted("G");
                                this.RntRestrictedAccount.DataSource = restrictedAccount.ToList();
                                this.RntRestrictedAccount.DataBind();

                                this.RntRestrictedAccount.SelectedValue = "23";

                                break;
                        }

                        if (this.ExtraBudgetary.CUE_CODIGO_PAGADOR != null)
                        {
                            this.RntRestrictedAccount.SelectedValue = this.ExtraBudgetary.CUE_CODIGO_PAGADOR.ToString();
                        }

                        this.RcbBinding.Checked = this.ExtraBudgetary.EXP_ENLAZADO_TESORERIA;
                        this.RtbPgcpAccount.Text = this.ExtraBudgetary.CUEP_NUMERO;

                        if (this.ExtraBudgetary.EXP_NUM_EXP_CONTABLE_ANUAL != null)
                        {
                            this.RntProvenance.Value = this.ExtraBudgetary.EXP_NUM_EXP_CONTABLE_ANUAL;
                        }

                        this.RtbDescription.Text = this.ExtraBudgetary.EXP_EXTRAP_TEXTO;

                        if (this.ExtraBudgetary.PROV_CODIGO_PROVEEDOR != null)
                        {
                            this.RcInterested.SelectedValue = this.ExtraBudgetary.PROV_CODIGO_PROVEEDOR.ToString();
                        }

                        if (this.ExtraBudgetary.FOR_CODIGO != null)
                        {
                            this.RcPayForms.SelectedValue = this.ExtraBudgetary.FOR_CODIGO.ToString();
                        }

                        if (this.ExtraBudgetary.CUE_CODIGO != null)
                        {
                            this.RcPercertor.SelectedValue = this.ExtraBudgetary.CUE_CODIGO.ToString();
                        }

                        if (this.ExtraBudgetary.TIPP_CODIGO != null)
                        {
                            this.RcPayTypes.SelectedValue = this.ExtraBudgetary.TIPP_CODIGO.ToString();
                        }

                        this.RtbCheckNumber.Text = this.ExtraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE;

                        if (this.ExtraBudgetary.PROV_CODIGO_TERCERO != null)
                        {
                            this.RcThirds.SelectedValue = this.ExtraBudgetary.PROV_CODIGO_TERCERO.ToString();
                        }

                        if (this.ExtraBudgetary.HOJ_NUMERO != null)
                        {
                            this.RntTonnageSheet.Value = this.ExtraBudgetary.HOJ_NUMERO;
                        }

                        if (this.ExtraBudgetary.ANO_HOJA != null)
                        {
                            var sheetYear = Convert.ToInt32(this.ExtraBudgetary.ANO_HOJA);
                            this.RmyTonnageSheetYear.SelectedDate = new DateTime(sheetYear, 1, 1);
                        }

                        if (this.ExtraBudgetary.HOJ_NUMERO50 != null)
                        {
                            this.RntTonnageSheet50.Value = this.ExtraBudgetary.HOJ_NUMERO50;
                        }

                        if (this.ExtraBudgetary.ANO_HOJA50 != null)
                        {
                            var sheetYear = Convert.ToInt32(this.ExtraBudgetary.ANO_HOJA50);
                            this.RmyTonnageSheet50Year.SelectedDate = new DateTime(sheetYear, 1, 1);
                        }

                        if (this.ExtraBudgetary.SEN_NUMERO != null)
                        {
                            this.RtbSing.Text = this.ExtraBudgetary.SEN_NUMERO.ToString();
                        }
                    }
                }
            }
        }

        protected void RcTypes_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var typeCode = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            var restrictedAccount = new List<PRE_CUENTA_RESTRINGIDA>();

            switch (typeCode)
            {
                case "-1":
                    this.RntRestrictedAccount.DataSource = restrictedAccount.ToList();
                    this.RntRestrictedAccount.DataBind();

                    break;
                case "2":
                case "4":
                    restrictedAccount = this.accountRestrictedService.GetAccountsRestricted("I");
                    this.RntRestrictedAccount.DataSource = restrictedAccount.ToList();
                    this.RntRestrictedAccount.DataBind();

                    this.RntRestrictedAccount.SelectedValue = "10";

                    break;
                case "1":
                case "3":
                case "5":
                    restrictedAccount = this.accountRestrictedService.GetAccountsRestricted("G");
                    this.RntRestrictedAccount.DataSource = restrictedAccount.ToList();
                    this.RntRestrictedAccount.DataBind();

                    this.RntRestrictedAccount.SelectedValue = "23";

                    break;
            }
        }

        protected void RmyYear_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (e.NewDate == null)
            {
                this.RntFileNumber.Value = null;
            }
            else
            {
                var year = e.NewDate?.Year;

                if (this.ExtraBudgetary.EXP_EXTRAP_CODIGO != 0)
                {
                    if (this.ExtraBudgetary.EXP_EXTRAP_CODIGO == year)
                    {
                        this.RntFileNumber.Value = Convert.ToDouble(this.ExtraBudgetary.EXP_EXTRAP_NUMERO);
                    }
                }
                else
                {
                    var number = this.extraBudgetaryApplicationsService.GetNextNumber((int)year);

                    this.RntFileNumber.Value = number;
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var typeCode = this.RcTypes.SelectedValue;
            var pmpEnable = false;
            var miEnable = false;

            if (!string.IsNullOrWhiteSpace(typeCode))
            {
                switch (typeCode)
                {
                    case "2":
                    case "4":
                        miEnable = true;

                        break;
                    case "1":
                    case "3":
                    case "5":
                        pmpEnable = true;

                        break;
                }
            }

            this.RcInterested.Enabled = pmpEnable;
            this.RcPayForms.Enabled = pmpEnable;
            this.RcPercertor.Enabled = pmpEnable;
            this.RcPayTypes.Enabled = pmpEnable;
            this.RtbCheckNumber.Enabled = pmpEnable;

            this.RcThirds.Enabled = miEnable;
            this.RntTonnageSheet.Enabled = miEnable;
            this.RmyTonnageSheetYear.Enabled = miEnable;
            this.RntTonnageSheet50.Enabled = miEnable;
            this.RmyTonnageSheet50Year.Enabled = miEnable;

            this.btnStatusApplication.Enabled = !this.RcExtraBudgetaryApplications.SelectedValue.Equals("-1");
            this.btnShowTreasury.Enabled = this.ExtraBudgetary.TES_CODIGO != null;
            this.btnDelete.Enabled = this.ExtraBudgetary.EXP_EXTRAP_CODIGO != 0;
            this.btnExtraBudgetaryDiscounts.Enabled = !string.IsNullOrWhiteSpace(typeCode) && typeCode.Equals("3");

            if (this.ExtraBudgetary.EXP_EXTRAP_CODIGO == 0)
            {
                this.btnReport.Enabled = false;
            }
            else
            {
                if (this.ExtraBudgetary.TIP_EXTRAP_CODIGO == null)
                {
                    this.btnReport.Enabled = false;
                }
                else
                {
                    if (this.ExtraBudgetary.TIP_EXTRAP_CODIGO.ToString().Equals("2") || this.ExtraBudgetary.TIP_EXTRAP_CODIGO.ToString().Equals("3"))
                    {
                        this.btnReport.Enabled = true;
                    }
                    else
                    {
                        this.btnReport.Enabled = false;
                    }
                }
            }
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/ExtraBudgetary/ManageExtraBudgetary.aspx");
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            var typeCode = this.RcTypes.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(this.RcTypes.SelectedValue);

            if (typeCode == null)
            {
                strBuilder.Append("Antes de grabar seleccione un Tipo de Expediente Extrapresuepuestario.");
                this.ShowMessage(this.RadNotification, "Imposible Grabar", strBuilder, MessageType.Warning);

                return;
            }

            var fileNumber = this.RntFileNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntFileNumber.Value);

            if (typeCode == 1)
            {
                fileNumber = null;
            }
            else
            {
                if (fileNumber == null)
                {
                    strBuilder.Append("Antes de grabar introduzca un Número de Expediente.");
                    this.ShowMessage(this.RadNotification, "Imposible Grabar", strBuilder, MessageType.Warning);

                    return;
                }
            }

            var extraBudgetaryApplication = this.RcExtraBudgetaryApplications.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcExtraBudgetaryApplications.SelectedValue);

            if (extraBudgetaryApplication == null)
            {
                strBuilder.Append("Antes de grabar seleccione una Aplicación Extrapresupuestaria.");
                this.ShowMessage(this.RadNotification, "Imposible Grabar", strBuilder, MessageType.Warning);

                return;
            }

            if (this.ExtraBudgetary.EXP_EXTRAP_CODIGO == 0)
            {
                var extraBudgetary = new PRE_EXP_EXTRAPRE
                                             {
                                                     TIP_EXTRAP_CODIGO = typeCode,
                                                     EXP_EXTRAP_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyYear.SelectedDate).Year),
                                                     EXP_EXTRAP_NUMERO = fileNumber,
                                                     EXP_EXTRAP_FECHA = this.RdDate.SelectedDate,
                                                     EXTRAPRE_CODIGO = (int)extraBudgetaryApplication,
                                                     EXP_EXTRAP_IMPORTE = this.RntAmount.Value == null ? 0 : Convert.ToDecimal(this.RntAmount.Value),
                                                     CUE_CODIGO_PAGADOR = Convert.ToInt32(this.RntRestrictedAccount.SelectedValue),
                                                     EXP_EXTRAP_TEXTO = this.RtbDescription.Text,
                                                     CUEP_NUMERO = this.RtbPgcpAccount.Text,
                                                     EXP_NUM_EXP_CONTABLE_ANUAL = null,
                                                     EXP_NUM_EXP_EXTRAPRE = null,
                                                     EXP_CODIGO = null,
                                                     DOC_CODIGO = null,
                                                     TES_CODIGO = null,
                                                     EXP_ENLAZADO_TESORERIA = false,
                                                     EXP_EXTRAP_PAGADO = false,
                                                     USU_CODIGO = LoginUser.USU_CODIGO
                                             };

                if (typeCode == 3 || typeCode == 4 || typeCode == 5)
                {
                    extraBudgetary.PROV_CODIGO_PROVEEDOR = this.RcInterested.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcInterested.SelectedValue);
                    extraBudgetary.FOR_CODIGO = this.RcPayForms.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(this.RcPayForms.SelectedValue);
                    extraBudgetary.CUE_CODIGO = this.RcPercertor.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcPercertor.SelectedValue);
                    extraBudgetary.TIPP_CODIGO = this.RcPayTypes.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(this.RcPayTypes.SelectedValue);
                    extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE = this.RtbCheckNumber.Text;

                    if (typeCode == 3)
                    {
                        extraBudgetary.PROV_CODIGO_TERCERO = null;
                        extraBudgetary.HOJ_NUMERO = null;
                        extraBudgetary.ANO_HOJA = null;
                        extraBudgetary.HOJ_NUMERO50 = null;
                        extraBudgetary.ANO_HOJA50 = null;
                    }
                }

                if (typeCode == 1 || typeCode == 2 || typeCode == 4 || typeCode == 5)
                {
                    extraBudgetary.PROV_CODIGO_TERCERO = this.RcThirds.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcThirds.SelectedValue);
                    extraBudgetary.HOJ_NUMERO = this.RntTonnageSheet.Value == null ? (int?)null : Convert.ToInt32(this.RntTonnageSheet.Value);
                    extraBudgetary.ANO_HOJA = this.RmyTonnageSheetYear.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)this.RmyTonnageSheetYear.SelectedDate).Year);
                    extraBudgetary.HOJ_NUMERO50 = this.RntTonnageSheet50.Value == null ? (int?)null : Convert.ToInt32(this.RntTonnageSheet50.Value);
                    extraBudgetary.ANO_HOJA50 = this.RmyTonnageSheet50Year.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)this.RmyTonnageSheet50Year.SelectedDate).Year);

                    if (typeCode == 1 || typeCode == 2)
                    {
                        extraBudgetary.PROV_CODIGO_PROVEEDOR = null;
                        extraBudgetary.FOR_CODIGO = null;
                        extraBudgetary.CUE_CODIGO = null;
                        extraBudgetary.TIPP_CODIGO = null;
                        extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE = null;
                    }
                }

                try
                {
                    var insert = extraBudgetaryRecordsService.InsertExtraBudgetary(extraBudgetary);

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        strBuilder.Append("Debe completar todos los datos del Expediente Extrapresupuestario.");
                        this.ShowMessage(this.RadNotification, "Error insertando el Expediente Extrapresupuestario", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        var id = (int)insert.ResponseMethod;
                        this.Response.Redirect($"~/Views/ExtraBudgetary/ManageExtraBudgetary.aspx?id={id}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error insertando el Expediente Extrapresupuestario.");
                    strBuilder.Append("Ha ocurrido un error insertando el Expediente Extrapresupuestario en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error insertando el Expediente Extrapresupuestario", strBuilder, MessageType.Deny);
                }
            }
            else
            {
                var extraBudgetary = new PRE_EXP_EXTRAPRE
                                             {
                                                     EXP_EXTRAP_CODIGO = this.ExtraBudgetary.EXP_EXTRAP_CODIGO,
                                                     TIP_EXTRAP_CODIGO = typeCode,
                                                     EXP_EXTRAP_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RmyYear.SelectedDate).Year),
                                                     EXP_EXTRAP_NUMERO = fileNumber,
                                                     EXP_EXTRAP_FECHA = this.RdDate.SelectedDate,
                                                     EXTRAPRE_CODIGO = (int)extraBudgetaryApplication,
                                                     EXP_EXTRAP_IMPORTE = this.RntAmount.Value == null ? 0 : Convert.ToDecimal(this.RntAmount.Value),
                                                     CUE_CODIGO_PAGADOR = Convert.ToInt32(this.RntRestrictedAccount.SelectedValue),
                                                     EXP_EXTRAP_TEXTO = this.RtbDescription.Text,
                                                     CUEP_NUMERO = this.RtbPgcpAccount.Text,
                                                     EXP_NUM_EXP_CONTABLE_ANUAL = this.RntProvenance.Value == null ? (int?)null : Convert.ToInt32(this.RntProvenance.Value),
                                                     EXP_NUM_EXP_EXTRAPRE = null,
                                                     EXP_CODIGO = null,
                                                     DOC_CODIGO = null,
                                                     TES_CODIGO = null,
                                                     EXP_ENLAZADO_TESORERIA = this.RcbBinding.Checked,
                                                     EXP_EXTRAP_PAGADO = false,
                                                     USU_CODIGO = LoginUser.USU_CODIGO
                                             };

                if (typeCode == 3 || typeCode == 4 || typeCode == 5)
                {
                    extraBudgetary.PROV_CODIGO_PROVEEDOR = this.RcInterested.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcInterested.SelectedValue);
                    extraBudgetary.FOR_CODIGO = this.RcPayForms.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(this.RcPayForms.SelectedValue);
                    extraBudgetary.CUE_CODIGO = this.RcPercertor.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcPercertor.SelectedValue);
                    extraBudgetary.TIPP_CODIGO = this.RcPayTypes.SelectedValue.Equals("-1") ? (byte?)null : Convert.ToByte(this.RcPayTypes.SelectedValue);
                    extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE = this.RtbCheckNumber.Text;

                    if (typeCode == 3)
                    {
                        extraBudgetary.PROV_CODIGO_TERCERO = null;
                        extraBudgetary.HOJ_NUMERO = null;
                        extraBudgetary.ANO_HOJA = null;
                        extraBudgetary.HOJ_NUMERO50 = null;
                        extraBudgetary.ANO_HOJA50 = null;
                    }
                }

                if (typeCode == 1 || typeCode == 2 || typeCode == 4 || typeCode == 5)
                {
                    extraBudgetary.PROV_CODIGO_TERCERO = this.RcThirds.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcThirds.SelectedValue);
                    extraBudgetary.HOJ_NUMERO = this.RntTonnageSheet.Value == null ? (int?)null : Convert.ToInt32(this.RntTonnageSheet.Value);
                    extraBudgetary.ANO_HOJA = this.RmyTonnageSheetYear.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)this.RmyTonnageSheetYear.SelectedDate).Year);
                    extraBudgetary.HOJ_NUMERO50 = this.RntTonnageSheet50.Value == null ? (int?)null : Convert.ToInt32(this.RntTonnageSheet50.Value);
                    extraBudgetary.ANO_HOJA50 = this.RmyTonnageSheet50Year.SelectedDate == null ? (short?)null : Convert.ToInt16(((DateTime)this.RmyTonnageSheet50Year.SelectedDate).Year);

                    if (typeCode == 1 || typeCode == 2)
                    {
                        extraBudgetary.PROV_CODIGO_PROVEEDOR = null;
                        extraBudgetary.FOR_CODIGO = null;
                        extraBudgetary.CUE_CODIGO = null;
                        extraBudgetary.TIPP_CODIGO = null;
                        extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE = null;
                    }
                }

                try
                {
                    var update = extraBudgetaryRecordsService.UpdateExtraBudgetaryAll(extraBudgetary);

                    switch (update.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            strBuilder.Append("Debe completar todos los datos del Expediente Extrapresupuestario.");

                            break;
                        case ResponseCode.NotFound:
                            strBuilder.Append("No se ha encontrado el Expediente Extrapresupuestario en cuestión.");

                            break;
                    }

                    if (update.ResponseCode != ResponseCode.Ok)
                    {
                        this.ShowMessage(this.RadNotification, "Error modificando el Expediente Extrapresupuestario", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        this.Response.Redirect($"~/Views/ExtraBudgetary/ManageExtraBudgetary.aspx?id={this.ExtraBudgetary.EXP_EXTRAP_CODIGO}");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error modificando el Expediente Extrapresupuestario.");
                    strBuilder.Append("Ha ocurrido un error modificando el Expediente Extrapresupuestario en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error modificando el Expediente Extrapresupuestario", strBuilder, MessageType.Deny);
                }
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            string message;

            if (this.ExtraBudgetary.TIP_EXTRAP_CODIGO == null)
            {
                return;
            }

            if (this.ExtraBudgetary.TIP_EXTRAP_CODIGO.ToString().Equals("1"))
            {
                message = $"¿ Está seguro que desea eliminar el descuento del expediente extrapresupuestario número {this.ExtraBudgetary.EXP_EXTRAP_NUMERO} ?";
            }
            else
            {
                message = "¿ Está seguro que desea eliminar el expediente extrapresupuestario en cuestión ?<br/>Se eliminarán todos los datos asociados a dicho expediente:<br/>- Apuntes de tesorería.<br/>- Hojas de arqueo.";
            }

            this.rwmManageExtraBudgetary.RadConfirm(message, "confirmDeleteCallBackFn", 330, 140, null, "Confirmación");
        }


        protected void btnStatusApplication_OnClick(object sender, EventArgs e)
        {
            var year = ((DateTime)this.RmyYear.SelectedDate).Year;
            var application = this.RcExtraBudgetaryApplications.SelectedValue;
            var applicationText = this.RcExtraBudgetaryApplications.SelectedItem.Text;

            var script = $"seeStatusApplication('{year}', '{application}', '{applicationText}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeStatusApplication", script, true);
        }

        protected void btnExtraBudgetaryDiscounts_OnClick(object sender, EventArgs e)
        {
            var extraBudgetaryId = this.ExtraBudgetary.EXP_EXTRAP_CODIGO;
            var script = $"updateDiscounts('{extraBudgetaryId}');";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "updateDiscounts", script, true);
        }

        protected void btnShowTreasury_OnClick(object sender, EventArgs e)
        {
            var extraBudgetaryId = this.ExtraBudgetary.EXP_EXTRAP_CODIGO;
            var treasuryId = this.ExtraBudgetary.TES_CODIGO ?? -1;

            var script = $"seeTreasuryNotes('{extraBudgetaryId}', '{treasuryId}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeTreasuryNotes", script, true);
        }

        protected void btnNewProvider_OnClick(object sender, EventArgs e)
        {
            this.Session["_currentSource"] = this.Request.Url.AbsoluteUri;
            this.Response.Redirect("~/Views/Maintenance/Providers.aspx");
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/ExtraBudgetary/ExtraBudgetaries.aspx");
        }

        protected void btnReport_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showReport", $"showReport('{this.ExtraBudgetary.EXP_EXTRAP_CODIGO}', '{this.ExtraBudgetary.TIP_EXTRAP_CODIGO}')", true);
        }

        #endregion

    }
}