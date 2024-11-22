namespace Dimatica.ContaPre.Presentation.Views.Treasury
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class PaymentRegister : BasePage
    {
        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IOriginsService originsService = DependencyFactory.GetInstance<IOriginsService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Public Properties

        public List<PRE_DOCUMENTO_CONTABLE> BackupList
        {
            get
            {
                var p = this.Session["_backupList"];

                if (p == null)
                {
                    p = new List<PRE_DOCUMENTO_CONTABLE>();
                    this.Session["_backupList"] = p;
                }

                return (List<PRE_DOCUMENTO_CONTABLE>)p;
            }

            set
            {
                this.Session["_backupList"] = value;
            }
        }

        public bool HasTonnageSheets
        {
            get
            {
                var p = this.Session["_hasTonnageSheets"];

                if (p == null)
                {
                    p = false;
                    this.Session["_hasTonnageSheets"] = p;
                }

                return (bool)p;
            }

            set
            {
                this.Session["_hasTonnageSheets"] = value;
            }
        }

        public bool HasSaved
        {
            get
            {
                var p = this.Session["_hasSaved"];

                if (p == null)
                {
                    p = false;
                    this.Session["_hasSaved"] = p;
                }

                return (bool)p;
            }

            set
            {
                this.Session["_hasSaved"] = value;
            }
        }

        public bool HasSavedGroup
        {
            get
            {
                var p = this.Session["_hasSavedGroup"];

                if (p == null)
                {
                    p = false;
                    this.Session["_hasSavedGroup"] = p;
                }

                return (bool)p;
            }

            set
            {
                this.Session["_hasSavedGroup"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "PaymentRegister";

                this.RmyYear.SelectedDate = DateTime.Now;
                this.RdDate.SelectedDate = DateTime.Now;

                var origins = this.originsService.GetOrigins();
                origins.Insert(
                               0,
                               new PRE_ORIGEN
                                       {
                                               ORI_CODIGO_AUX = -1,
                                               ORI_DESCRIPCION = "< Seleccione >"
                                       });
                this.RcPayType.DataSource = origins;
                this.RcPayType.DataBind();

                var providers = this.providersService.GetProvidersToCombo();
                providers.Insert(
                                 0,
                                 new PRE_PROVEEDOR
                                         {
                                                 PROV_CODIGO = -1,
                                                 PROV_NOMBRE = "< Seleccione >"
                                         });

                this.RcProvidersIncomes.DataSource = providers;
                this.RcProvidersIncomes.DataBind();

                this.RcProvidersSpends.DataSource = providers;
                this.RcProvidersSpends.DataBind();

                this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.FillTonnageSheet(true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();
            this.FillTonnageSheet(true);
        }

        protected void RgTonnageSheet_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillTonnageSheet(false);
        }

        protected void RgTonnageSheet_PreRender(object sender, EventArgs e)
        {
            for (var i = 0; i < this.RgTonnageSheet.Items.Count; i++)
            {
                var item = this.RgTonnageSheet.Items[i];
                var fileCode = (int)item.GetDataKeyValue("EXP_CODIGO");

                if (this.BackupList.Any(p => p.EXP_CODIGO == fileCode))
                {
                    item.Selected = true;
                }
            }
        }

        protected void RgTonnageSheet_InsertCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_UpdateCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_DeleteCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_ItemDataBound(object sender, GridItemEventArgs e) { }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            this.FillBackupList();
            this.FillTonnageSheet(true);
        }

        protected void btnReport_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();
            var errorCounts = 0;

            this.FillBackupList();

            if ((bool)this.RcbGroup.Checked)
            {
                var checkNumber = this.BackupList.FirstOrDefault().DOC_NUMERO_CHEQUE;
                var checkEquals = this.BackupList.All(p => p.DOC_NUMERO_CHEQUE.Equals(checkNumber));

                if (!checkEquals)
                {
                    strBuilder.Append("No se pueden agrupar varios pagos con diferentes números de cheque/transf.");
                    this.ShowMessage(this.RadNotification, "Imposible generar apuntes", strBuilder, MessageType.Warning);
                }
                else
                {
                    PRE_TESORERIA treasury = null;

                    foreach (var payment in this.BackupList)
                    {
                        var payFormCode = payment.DOC_NUMERO_CHEQUE.ToLower().StartsWith("t") ? 3 : 1;
                        var have = payment.ORIGEN.Equals("1") || payment.ORIGEN.Equals("3");

                        if (treasury == null)
                        {
                            treasury = new PRE_TESORERIA
                                               {
                                                       ORI_CODIGO = Convert.ToByte(payment.ORIGEN),
                                                       TES_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RdDate.SelectedDate).Year),
                                                       TIPR_CODIGO = 1,
                                                       TES_FECHA_APUNTE = this.RdDate.SelectedDate,
                                                       TES_FECHA_BANCO = null,
                                                       TES_APLICACION = payment.DOCUMENTO_APLICACION,
                                                       FOR_CODIGO = Convert.ToByte(payFormCode),
                                                       TES_NUMERO_CHEQUE = payment.DOC_NUMERO_CHEQUE,
                                                       CUE_CODIGO = null,
                                                       TES_TOTAL_IMPORTE_LIQUIDO = payment.LIQUIDO,
                                                       TES_HABER = have,
                                                       TES_DESCRIPCION = payment.PROV_NOMBRE,
                                                       TES_ANULADO = false,
                                                       TES_MARCA_0_1_255 = Convert.ToByte(1),
                                                       USU_CODIGO = LoginUser.USU_CODIGO
                                               };
                        }
                        else
                        {
                            treasury.TES_TOTAL_IMPORTE_LIQUIDO += payment.LIQUIDO;
                        }
                    }

                    var insert = this.treasuriesService.InsertTreasury(treasury);

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        strBuilder.Append("Ha ocurrido un error generado el apunte de tesorería.");
                        this.ShowMessage(this.RadNotification, "Imposible generar apunte", strBuilder, MessageType.Warning);
                    }
                    else
                    {
                        var id = (int)insert.ResponseMethod;

                        foreach (var payment in this.BackupList)
                        {
                            var document = new PRE_TESORERIA_DOCUMENTO
                                                   {
                                                           TES_CODIGO = id,
                                                           DOC_CODIGO = payment.CODIGO_DOCUMENTO,
                                                           EXP_EXTRAP_CODIGO = payment.CODIGO_EXP_EXTRAP,
                                                           TESD_IMPORTE_LIQUIDO = payment.LIQUIDO,
                                                           TESD_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RdDate.SelectedDate).Year),
                                                           TESD_NUMERO_CHEQUE = payment.DOC_NUMERO_CHEQUE,
                                                           ORI_CODIGO = Convert.ToByte(payment.ORIGEN),
                                                           TESD_DOCUMENTO = payment.DOCUMENTO_APLICACION,
                                                           TESD_APLICACION = payment.DOCUMENTO_APLICACION,
                                                           TESD_DESCRIPCION = payment.PROV_NOMBRE,
                                                           TESD_NUMERO_EXPEDIENTE = payment.NUMERO_EXPEDIENTE,
                                                           Finish = true,
                                                           USU_CODIGO = LoginUser.USU_CODIGO
                                                   };

                            var bound = this.treasuriesService.BoundDocument(document);

                            if (bound.ResponseCode == ResponseCode.Ok)
                            {
                                var connectToToOracle = Convert.ToBoolean(ConfigurationManager.AppSettings["connectToOracle"]);
                                if (connectToToOracle)
                                {
                                    if (payment.CODIGO_DOCUMENTO != null)
                                    {
                                        var bills = this.accountingDocumentsService.GetPurchases((int)payment.CODIGO_DOCUMENTO);

                                        foreach (var bill in bills)
                                        {
                                            var updatePayBank = this.accountingDocumentsService.UpdatePayBankOracle(treasury.TES_FECHA_APUNTE, (int)bill.CODFACTURAGEI);

                                            var updateHistory = this.accountingDocumentsService.UpdateHistoryOracle(bill.NCERTIFICADO, $"Se ha pagado la factura con fecha {((DateTime)treasury.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"))}");
                                        }
                                    }
                                }
                            }
                        }

                        this.HasSavedGroup = true;
                        this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();
                        this.Response.Redirect("~/Views/Treasury/PaymentRegister.aspx");
                    }
                }
            }
            else
            {
                foreach (var payment in this.BackupList)
                {
                    var payFormCode = payment.DOC_NUMERO_CHEQUE.ToLower().StartsWith("t") ? 3 : 1;

                    var treasury = new PRE_TESORERIA
                                           {
                                                   ORI_CODIGO = Convert.ToByte(payment.ORIGEN),
                                                   TES_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RdDate.SelectedDate).Year),
                                                   TIPR_CODIGO = 1,
                                                   TES_FECHA_APUNTE = this.RdDate.SelectedDate,
                                                   TES_FECHA_BANCO = null,
                                                   TES_APLICACION = payment.DOCUMENTO_APLICACION,
                                                   FOR_CODIGO = Convert.ToByte(payFormCode),
                                                   TES_NUMERO_CHEQUE = payment.DOC_NUMERO_CHEQUE,
                                                   CUE_CODIGO = null,
                                                   TES_TOTAL_IMPORTE_LIQUIDO = payment.LIQUIDO,
                                                   TES_HABER = true,
                                                   TES_DESCRIPCION = payment.PROV_NOMBRE,
                                                   TES_ANULADO = false,
                                                   TES_MARCA_0_1_255 = Convert.ToByte(1),
                                                   USU_CODIGO = LoginUser.USU_CODIGO
                                           };

                    var insert = this.treasuriesService.InsertTreasury(treasury);

                    if (insert.ResponseCode != ResponseCode.Ok)
                    {
                        errorCounts++;
                    }
                    else
                    {
                        var id = (int)insert.ResponseMethod;

                        var document = new PRE_TESORERIA_DOCUMENTO
                                               {
                                                       TES_CODIGO = id,
                                                       DOC_CODIGO = payment.CODIGO_DOCUMENTO,
                                                       EXP_EXTRAP_CODIGO = payment.CODIGO_EXP_EXTRAP,
                                                       TESD_IMPORTE_LIQUIDO = payment.LIQUIDO,
                                                       TESD_ANO_PRESUPUESTO = Convert.ToInt16(((DateTime)this.RdDate.SelectedDate).Year),
                                                       TESD_NUMERO_CHEQUE = payment.DOC_NUMERO_CHEQUE,
                                                       ORI_CODIGO = Convert.ToByte(payment.ORIGEN),
                                                       TESD_DOCUMENTO = payment.DOCUMENTO_APLICACION,
                                                       TESD_APLICACION = payment.DOCUMENTO_APLICACION,
                                                       TESD_DESCRIPCION = payment.PROV_NOMBRE,
                                                       TESD_NUMERO_EXPEDIENTE = payment.NUMERO_EXPEDIENTE,
                                                       Finish = true,
                                                       USU_CODIGO = LoginUser.USU_CODIGO
                                               };

                        var bound = this.treasuriesService.BoundDocument(document);

                        if (bound.ResponseCode == ResponseCode.Ok)
                        {
                            if (payment.CODIGO_DOCUMENTO != null)
                            {
                                var bills = this.accountingDocumentsService.GetPurchases((int)payment.CODIGO_DOCUMENTO);

                                foreach (var bill in bills)
                                {
                                    var updatePayBank = this.accountingDocumentsService.UpdatePayBankOracle(treasury.TES_FECHA_APUNTE, (int)bill.CODFACTURAGEI);

                                    var updateHistory = this.accountingDocumentsService.UpdateHistoryOracle(bill.NCERTIFICADO, $"Se ha pagado la factura con fecha {((DateTime)treasury.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"))}");
                                }
                            }
                        }
                    }
                }

                if (errorCounts == 0)
                {
                    this.HasSaved = true;
                    this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();
                    this.Response.Redirect("~/Views/Treasury/PaymentRegister.aspx");
                }
                else
                {
                    strBuilder.Append("Ha ocurrido un error generado alguno de los apuntes de tesorería.");
                    this.ShowMessage(this.RadNotification, "Imposible generar apuntes", strBuilder, MessageType.Warning);
                }
            }
        }

        private void FillTonnageSheet(bool manual)
        {
            this.HasTonnageSheets = false;

            var year = ((DateTime)this.RmyYear.SelectedDate).Year;
            var originCode = this.RcPayType.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcPayType.SelectedValue);
            var providerIncome = this.RcProvidersIncomes.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcProvidersIncomes.SelectedValue);
            var providerSpend = this.RcProvidersSpends.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcProvidersSpends.SelectedValue);
            var sinceDate = this.RdpSinceDate.SelectedDate == null ? string.Empty : ((DateTime)this.RdpSinceDate.SelectedDate).ToString("d", new CultureInfo("es-ES"));
            var untilDate = this.RdpUntilDate.SelectedDate == null ? string.Empty : ((DateTime)this.RdpUntilDate.SelectedDate).ToString("d", new CultureInfo("es-ES"));

            var payments = new List<PRE_DOCUMENTO_CONTABLE>();
            var paymentsResult = this.treasuriesService.GetPaymentsRegister(year, originCode, providerIncome, providerSpend, sinceDate, untilDate);

            // if (!this.BackupList.Any())
            // {
            // this.BackupList = paymentsResult;
            // }
            if (!this.BackupList.Any())
            {
                payments = paymentsResult;
            }
            else
            {
                foreach (var payment in this.BackupList)
                {
                    payments.Add(payment);
                }

                foreach (var payment in paymentsResult)
                {
                    if (payments.Any(p => p.EXP_CODIGO == payment.EXP_CODIGO))
                    {
                        continue;
                    }

                    payments.Add(payment);
                }
            }

            this.RgTonnageSheet.DataSource = payments;

            if (manual)
            {
                this.RgTonnageSheet.DataBind();
            }

            this.RpbFilter.CollapseAllItems();

            if (payments.Any())
            {
                this.HasTonnageSheets = true;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.btnReport.Enabled = this.BackupList.Any();

            if (this.HasSaved)
            {
                this.HasSaved = false;
                var strBuilder = new StringBuilder();
                strBuilder.Append("Los apuntes han sido generado con éxito.");
                this.ShowMessage(this.RadNotification, "Apuntes generarados", strBuilder, MessageType.Warning);
            }

            if (this.HasSavedGroup)
            {
                this.HasSavedGroup = false;
                var strBuilder = new StringBuilder();
                strBuilder.Append("El apunte agrupado ha sido generado con éxito.");
                this.ShowMessage(this.RadNotification, "Apuntes generarados", strBuilder, MessageType.Warning);
            }
        }

        private void FillBackupList()
        {
            this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();

            foreach (GridDataItem item in this.RgTonnageSheet.SelectedItems)
            {
                var document = new PRE_DOCUMENTO_CONTABLE
                                       {
                                               EXP_CODIGO = (int)item.GetDataKeyValue("EXP_CODIGO"),
                                               NUMERO_EXPEDIENTE = item.GetDataKeyValue("NUMERO_EXPEDIENTE") == null ? (int?)null : (int)item.GetDataKeyValue("NUMERO_EXPEDIENTE"),
                                               PROV_NOMBRE = (string)item.GetDataKeyValue("PROV_NOMBRE"),
                                               LIQUIDO = (decimal)item.GetDataKeyValue("LIQUIDO"),
                                               DOC_ENLAZADO_TESORERIA = (bool)item.GetDataKeyValue("DOC_ENLAZADO_TESORERIA"),
                                               CODIGO_DOCUMENTO = item.GetDataKeyValue("CODIGO_DOCUMENTO") == null ? (int?)null : (int)item.GetDataKeyValue("CODIGO_DOCUMENTO"),
                                               CODIGO_EXP_EXTRAP = item.GetDataKeyValue("CODIGO_EXP_EXTRAP") == null ? (int?)null : (int)item.GetDataKeyValue("CODIGO_EXP_EXTRAP"),
                                               DOC_NUMERO_CHEQUE = (string)item.GetDataKeyValue("DOC_NUMERO_CHEQUE"),
                                               DOC_FECHA_PROPUESTA = item.GetDataKeyValue("DOC_FECHA_PROPUESTA") == null ? (DateTime?)null : (DateTime)item.GetDataKeyValue("DOC_FECHA_PROPUESTA"),
                                               PROV_CODIGO = item.GetDataKeyValue("PROV_CODIGO") == null ? (int?)null : (int)item.GetDataKeyValue("PROV_CODIGO"),
                                               DOCUMENTO_APLICACION = (string)item.GetDataKeyValue("DOCUMENTO_APLICACION"),
                                               ORIGEN = (string)item.GetDataKeyValue("ORIGEN"),
                                               PROCEDENCIA = (string)item.GetDataKeyValue("PROCEDENCIA")
                                       };

                this.BackupList.Add(document);

                // var provenance = (string)item.GetDataKeyValue("PROCEDENCIA");
                // var entryDate = (DateTime)this.RdDate.SelectedDate;
                // var entryYear = entryDate.Year;
                // var documentApplication
                // var payFormCode = 0;
                // if (!string.IsNullOrWhiteSpace(checkNumber))
                // {
                // if (checkNumber.ToLower().StartsWith("t"))
                // {
                // payFormCode = 3;
                // }
                // else
                // {
                // payFormCode = 1;
                // }
                // }

                // var amount = (decimal)item.GetDataKeyValue("LIQUIDO");
                // var origin = (string)item.GetDataKeyValue("ORIGEN");
                // var documentTypeCode = 1;
            }
        }

        #endregion
    }
}