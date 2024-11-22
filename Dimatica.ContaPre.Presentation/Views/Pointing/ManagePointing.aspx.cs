namespace Dimatica.ContaPre.Presentation.Views.Pointing
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

    #endregion

    public partial class ManagePointing : BasePage
    {
        #region Static Fields and Constants

        private static IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private static ISingsService singsService = DependencyFactory.GetInstance<ISingsService>();

        #endregion

        #region Private Properties

        private PRE_SENALAMIENTO Pointing
        {
            get
            {
                var pointing = this.Session["_pointing"] as PRE_SENALAMIENTO;

                if (pointing == null)
                {
                    pointing = new PRE_SENALAMIENTO();

                    this.Session["_pointing"] = pointing;
                }

                return pointing;
            }

            set
            {
                this.Session["_pointing"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeletePointing()
        {
            try
            {
                var id = ((PRE_SENALAMIENTO)HttpContext.Current.Session["_pointing"]).SEN_CODIGO;

                if (id == 0)
                {
                    return 3;
                }

                var delete = singsService.DeletePointing(id, LoginUser.USU_CODIGO);

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
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el señalamiento.");
                return 0;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var pointingId = this.Request.QueryString["id"];

            if (!this.IsPostBack)
            {
                if (pointingId == null)
                {
                    this.Response.Redirect("~/Views/Pointing/Sings.aspx");
                }
                else
                {
                    var pointing = singsService.GetById(Convert.ToInt32(pointingId));

                    if (pointing == null)
                    {
                        this.Response.Redirect("~/Views/Pointing/Sings.aspx");
                    }
                    else
                    {
                        this.Pointing = pointing;

                        this.TxtPointingNumber.InnerText = this.Pointing.SEN_NUMERO == null ? string.Empty : this.Pointing.SEN_NUMERO.ToString();
                        this.TxtYear.InnerText = this.Pointing.SEN_ANO == null ? string.Empty : this.Pointing.SEN_ANO.ToString();
                        this.TxtDate.InnerText = this.Pointing.SEN_FECHA == null ? string.Empty : ((DateTime)this.Pointing.SEN_FECHA).ToString("d", new CultureInfo("es-ES"));
                    }
                }
            }
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Pointing/NewPointing.aspx");
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var message = $"¿ Desea realmente eliminar el señalamiento {this.Pointing.SEN_NUMERO} del año {this.Pointing.SEN_ANO} ?";
            this.rwmManagePointing.RadConfirm(message, "confirmDeleteCallBackFn", 330, 140, null, "Confirmación");
        }

        protected void btnTransfers_OnClick(object sender, EventArgs e)
        {
            var groupTransfers = this.checkGroup.Checked == false ? "0" : "1";
            this.Response.Redirect($"~/Views/Pointing/ManageTransfers.aspx?id={this.Pointing.SEN_CODIGO}&group={groupTransfers}");
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Pointing/Sings.aspx");
        }

        protected void RgDocuments_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var documents = new List<PRE_SENALAMIENTO_DOCUMENTO>();

            if (this.Pointing.SEN_CODIGO != 0)
            {
                documents = singsService.GetDocumentsByPointingId(this.Pointing.SEN_CODIGO);
            }

            decimal fullAmount = 0;
            decimal liquidAmount = 0;
            var checkNumber = string.Empty;
            var equalsCheck = false;

            if (documents.Any())
            {
                checkNumber = documents.FirstOrDefault().NUMERO_CHEQUE;
            }

            if (documents.All(d => d.NUMERO_CHEQUE.Equals(checkNumber)))
            {
                equalsCheck = true;
            }

            for (var i = 0; i < documents.Count; i++)
            {
                fullAmount += documents[i].IMPORTE_INTEGRO;
                liquidAmount += documents[i].IMPORTE_LIQUIDO;

                if (equalsCheck)
                {
                    if (i == documents.Count - 1)
                    {
                        documents[i].TOTAL_IMPORTE_LIQUIDO = liquidAmount;
                    }
                    else
                    {
                        documents[i].TOTAL_IMPORTE_LIQUIDO = -1;
                        documents[i].NUMERO_CHEQUE = string.Empty;
                    }
                }
                else
                {
                    documents[i].TOTAL_IMPORTE_LIQUIDO = documents[i].IMPORTE_LIQUIDO;
                }
            }

            this.RgDocuments.DataSource = documents;

            this.txtFullAmount.Text = fullAmount.ToString("N");
            this.txtLiquidAmount.Text = liquidAmount.ToString("N");
        }

        protected void RgDocuments_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var count = this.RgDocuments.MasterTableView.Items.Count;
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var documentPointingId = (int)item.GetDataKeyValue("SEND_CODIGO");

            try
            {
                var delete = singsService.DeleteDocument(documentPointingId, LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Documento.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Documento en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Documento", strBuilder, MessageType.Warning);
                }
                else
                {
                    //var documentId = item.GetDataKeyValue("DOC_CODIGO");

                    //if (documentId != null)
                    //{
                        //var bills = accountingDocumentsService.GetPurchases((int)documentId);

                        //foreach (var bill in bills)
                        //{
                            // TODO: aqui se llamaba a oracle
                            // expediente.actualizaHistoricoRegFra(dtFacturas.Rows(k).Item("ncertificado"), _
                            // "Se ha eliminado la factura del Se�alamiento nro. " & numeroSenalamiento & " del a�o " & anoSenalamiento & ".")
                        //}
                    //}

                    if (count == 1)
                    {
                        var deletePointing = singsService.DeletePointing(this.Pointing.SEN_CODIGO, LoginUser.USU_CODIGO);

                        if (deletePointing.ResponseCode != ResponseCode.Ok)
                        {
                            strBuilder.Append("Ha ocurrido un error eliminando el Señalamiento en cuestión.");
                            this.ShowMessage(this.RadNotification, "Imposible eliminar Señalamiento", strBuilder, MessageType.Deny);
                        }
                        else
                        {
                            this.Response.Redirect("~/Views/Pointing/Sings.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el documento.");
                strBuilder.Append("Ha ocurrido un error eliminando el Documento en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Documento", strBuilder, MessageType.Deny);
            }
        }

        #endregion

        protected void btnReport_OnClick(object sender, EventArgs e)
        {
            var pointingId = this.Pointing.SEN_CODIGO;
            var pointingNumber = this.Pointing.SEN_NUMERO == null ? string.Empty : this.Pointing.SEN_NUMERO.ToString();
            var pointingYear = this.Pointing.SEN_ANO == null ? string.Empty : this.Pointing.SEN_ANO.ToString();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showReport", $"showReport('{pointingId}', '{pointingNumber}', '{pointingYear}');", true);
        }
    }
}