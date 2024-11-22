namespace Dimatica.ContaPre.Presentation.Views.Pointing
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class NewPointing : BasePage
    {
        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IExtraBudgetaryRecordsService extraBudgetaryService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        private ISingsService singsService = DependencyFactory.GetInstance<ISingsService>();

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

        public int Year
        {
            get
            {
                var p = this.Session["_currentYear"];

                if (p == null)
                {
                    p = DateTime.Now.Year;
                    this.Session["_currentYear"] = p;
                }

                return (int)p;
            }

            set
            {
                this.Session["_currentYear"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "NewPointing";

                this.RmyYear.SelectedDate = DateTime.Now;

                this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();

                var number = this.singsService.GetNextNumber(DateTime.Now.Year);

                this.RtbPointingNumber.Text = number.ToString();
                this.RtbDate.Text = DateTime.Now.ToString("d", new CultureInfo("es-ES"));

                this.Year = DateTime.Now.Year;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            foreach (GridDataItem item in this.RgDocuments.MasterTableView.Items)
            {
                var originalRepair = (bool)item.GetDataKeyValue("doc_reparado");

                var checkRepair = (RadCheckBox)item["Repair"].FindControl("checkRepair");

                if (checkRepair.Checked == originalRepair)
                {
                    continue;
                }

                var documentCode = item.GetDataKeyValue("DOC_CODIGO_AUX");

                if (documentCode != null)
                {
                    var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                    {
                        DOC_CODIGO = (int)documentCode,
                        Repair = (bool)checkRepair.Checked,
                        USU_CODIGO = LoginUser.USU_CODIGO
                    };

                    var update = this.accountingDocumentsService.UpdateRepair(accountingDocument);
                }
                else
                {
                    var fileCode = item.GetDataKeyValue("EXP_EXTRAP_CODIGO");

                    if (fileCode != null)
                    {
                        var accountingDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            EXP_EXTRAP_CODIGO = (int)fileCode,
                            Repair = (bool)checkRepair.Checked,
                            USU_CODIGO = LoginUser.USU_CODIGO
                        };

                        var update = this.extraBudgetaryService.UpdateRepair(accountingDocument);
                    }
                }
            }

            this.FillBackupList();
            this.FillDocuments(true);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.FillDocuments(true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();
            this.FillDocuments(true);
        }

        protected void RgDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillDocuments(false);
        }

        protected void RgDocuments_PreRender(object sender, EventArgs e)
        {
            //for (var i = 0; i < this.RgDocuments.Items.Count; i++)
            //{
            //    var item = this.RgDocuments.Items[i];
            //    var documentCode = item.GetDataKeyValue("DOC_CODIGO_AUX");

            //    if (documentCode != null)
            //    {
            //        if (this.BackupList.Any(p => p.DOC_CODIGO_AUX != null && p.DOC_CODIGO_AUX == (int)documentCode))
            //        {
            //            item.Selected = true;
            //        }
            //    }
            //    else
            //    {
            //        var fileCode = item.GetDataKeyValue("EXP_EXTRAP_CODIGO");

            //        if (fileCode != null)
            //        {
            //            if (this.BackupList.Any(p => p.EXP_EXTRAP_CODIGO != null && p.EXP_EXTRAP_CODIGO == (int)fileCode))
            //            {
            //                item.Selected = true;
            //            }
            //        }
            //    }
            //}
        }

        private void FillDocuments(bool manual)
        {
            var year = ((DateTime)this.RmyYear.SelectedDate).Year;

            if (year != this.Year)
            {
                this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();
            }

            var documentType = this.RcbType.SelectedValue.Equals("-1") ? null : this.RcbType.SelectedValue;

            var documents = new List<PRE_DOCUMENTO_CONTABLE>();
            var documentsResult = this.singsService.GetPendingDocuments(year, documentType);

            if (!this.BackupList.Any())
            {
                documents = documentsResult;
            }
            else
            {
                foreach (var document in this.BackupList)
                {
                    documents.Add(document);
                }

                foreach (var document in documentsResult)
                {
                    if (document.DOC_CODIGO_AUX != null)
                    {
                        if (documents.Any(p => p.DOC_CODIGO_AUX != null && p.DOC_CODIGO_AUX == document.DOC_CODIGO_AUX))
                        {
                            continue;
                        }

                        documents.Add(document);
                    }
                    else
                    {
                        if (document.EXP_EXTRAP_CODIGO != null)
                        {
                            if (documents.Any(p => p.EXP_EXTRAP_CODIGO != null && p.EXP_EXTRAP_CODIGO == document.EXP_EXTRAP_CODIGO))
                            {
                                continue;
                            }

                            documents.Add(document);
                        }
                    }
                }
            }

            this.RgDocuments.DataSource = documents;

            if (manual)
            {
                this.RgDocuments.DataBind();
            }

            if (year != this.Year)
            {
                var number = this.singsService.GetNextNumber(year);

                this.RtbPointingNumber.Text = number.ToString();

                this.Year = year;
            }

            this.RpbFilter.CollapseAllItems();
        }

        private void FillBackupList()
        {
            this.BackupList = new List<PRE_DOCUMENTO_CONTABLE>();

            foreach (GridDataItem item in this.RgDocuments.MasterTableView.Items)
            {
                var checkRepair = (RadCheckBox)item["Repair"].FindControl("checkRepair");
                var checkSelect = (RadCheckBox)item["Select"].FindControl("checkSelect");

                if (checkSelect.Checked == false)
                {
                    continue;
                }

                var document = new PRE_DOCUMENTO_CONTABLE
                {
                    DOC_CODIGO_AUX = item.GetDataKeyValue("DOC_CODIGO_AUX") == null ? (int?)null : (int)item.GetDataKeyValue("DOC_CODIGO_AUX"),
                    DOC_FECHA_ASIENTO_DIARIO = item.GetDataKeyValue("DOC_FECHA_ASIENTO_DIARIO") == null ? (DateTime?)null : (DateTime)item.GetDataKeyValue("DOC_FECHA_ASIENTO_DIARIO"),
                    EXP_EXTRAP_CODIGO = item.GetDataKeyValue("EXP_EXTRAP_CODIGO") == null ? (int?)null : (int)item.GetDataKeyValue("EXP_EXTRAP_CODIGO"),
                    NUMERO_DOCUMENTO = item.GetDataKeyValue("NUMERO_DOCUMENTO") == null ? (int?)null : (int)item.GetDataKeyValue("NUMERO_DOCUMENTO"),
                    TIPO_DOCUMENTO = (string)item.GetDataKeyValue("TIPO_DOCUMENTO"),
                    CONCEPTO = (string)item.GetDataKeyValue("CONCEPTO"),
                    PROV_NOMBRE = (string)item.GetDataKeyValue("PROV_NOMBRE"),
                    LIQUIDO = (decimal)item.GetDataKeyValue("LIQUIDO"),
                    DOC_NUMERO_CHEQUE = (string)item.GetDataKeyValue("DOC_NUMERO_CHEQUE"),
                    doc_reparado = (bool)item.GetDataKeyValue("doc_reparado"),
                    Select = (bool)checkSelect.Checked,
                    Repair = (bool)checkRepair.Checked,
                    tiene_irpf = (string)item.GetDataKeyValue("tiene_irpf")
                };

                this.BackupList.Add(document);
            }

            decimal total = 0;

            foreach (var document in this.BackupList)
            {
                if (document.Select == false)
                {
                    continue;
                }

                total += document.LIQUIDO;
            }

            this.RtbAmount.Text = total.ToString("N");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.btnGenerate.Enabled = this.BackupList.Any();
        }

        #endregion

        protected void btnGenerate_OnClick(object sender, EventArgs e)
        {
            this.FillBackupList();
            var strBuilder = new StringBuilder();

            if (!this.BackupList.Any())
            {
                strBuilder.Append("No se puede generar el señalamiento ni actualizar los reparados debido a que no hay ningún documento seleccionado.");
                this.ShowMessage(this.RadNotification, "Imposible generar señalamiento", strBuilder, MessageType.Warning);

                return;
            }

            try
            {
                var pointing = new PRE_SENALAMIENTO
                {
                    SEN_ANO = Convert.ToInt16(this.Year),
                    SEN_NUMERO = Convert.ToInt32(this.RtbPointingNumber.Text),
                    SEN_FECHA = Convert.ToDateTime(this.RtbDate.Text, new CultureInfo("es-ES")),
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var insert = this.singsService.InsertPointing(pointing);

                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Señalamiento.");

                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Señalamiento", strBuilder, MessageType.Warning);
                }
                else
                {
                    var id = (int)insert.ResponseMethod;

                    foreach (var document in this.BackupList)
                    {
                        var documentCode = document.DOC_CODIGO_AUX;
                        var fileCode = document.EXP_EXTRAP_CODIGO;

                        var insertDocument = this.singsService.InsertDocumentSing(id, documentCode, fileCode, LoginUser.USU_CODIGO);

                        if (documentCode != null)
                        {
                            var bills = this.accountingDocumentsService.GetPurchases((int)documentCode);

                            foreach (var bill in bills)
                            {
                                // TODO: aqui se llamaba a oracle
                                // expediente.actualizaHistoricoRegFra(dtFacturas.Rows(k).Item("ncertificado"), _
                                // "Se ha eliminado la factura del Se�alamiento nro. " & numeroSenalamiento & " del a�o " & anoSenalamiento & ".")
                            }
                        }
                    }

                    this.Response.Redirect($"~/Views/Pointing/ManagePointing.aspx?id={id}");
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el señalamiento.");
                strBuilder.Append("Ha ocurrido un error insertando el Señalamiento en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Señalamiento", strBuilder, MessageType.Deny);
            }
        }

        protected void btnTotal_OnClick(object sender, EventArgs e)
        {
            decimal total = 0;

            foreach (GridDataItem item in this.RgDocuments.MasterTableView.Items)
            {
                var checkSelect = (RadCheckBox)item["Select"].FindControl("checkSelect");

                if (checkSelect.Checked == false)
                {
                    continue;
                }

                var amount = (decimal)item.GetDataKeyValue("LIQUIDO");

                total += amount;
            }
            
            var script = $"seeTotal('{total:N}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "seeTotal", script, true);
        }
    }
}