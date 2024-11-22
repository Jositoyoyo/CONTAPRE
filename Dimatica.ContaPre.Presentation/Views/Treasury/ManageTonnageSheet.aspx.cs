namespace Dimatica.ContaPre.Presentation.Views.Treasury
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
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

    public partial class ManageTonnageSheet : BasePage
    {
        #region Static Fields and Constants

        private static ITonnageSheetsService tonageSheetsService = DependencyFactory.GetInstance<ITonnageSheetsService>();

        #endregion

        #region Private Properties

        private PRE_HOJA_ARQUEO TonnageSheet
        {
            get
            {
                var sheet = this.Session["_tonnageSheet"] as PRE_HOJA_ARQUEO;

                if (sheet == null)
                {
                    sheet = new PRE_HOJA_ARQUEO();

                    this.Session["_tonnageSheet"] = sheet;
                }

                return sheet;
            }

            set
            {
                this.Session["_tonnageSheet"] = value;
            }
        }

        #endregion

        #region Public Static Methods

        [WebMethod]
        public static int DeleteTonnageSheet()
        {
            try
            {
                var id = ((PRE_HOJA_ARQUEO)HttpContext.Current.Session["_tonnageSheet"]).HOJ_CODIGO;

                if (id == 0)
                {
                    return 3;
                }

                var delete = tonageSheetsService.DeleteTonnageSheet(id, LoginUser.USU_CODIGO);

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
                LogError(ex, "Error eliminando la Hoja de Arqueo.");
                return 0;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var tonnageId = this.Request.QueryString["id"];

            if (tonnageId == null)
            {
                this.Response.Redirect("~/Views/Treasury/SeeTonnageSheets.aspx");

                return;
            }

            if (!this.IsPostBack)
            {
                this.TonnageSheet = null;

                var tonnageSheet = tonageSheetsService.GetById(Convert.ToInt32(tonnageId));

                if (tonnageSheet == null)
                {
                    this.Response.Redirect("~/Views/Treasury/SeeTonnageSheets.aspx");

                    return;
                }

                this.TonnageSheet = tonnageSheet;
                this.TxtYear.InnerText = this.TonnageSheet.HOJ_ANO.ToString();
                this.TxtSheetNumber.InnerText = this.TonnageSheet.HOJ_NUMERO.ToString();
                this.TxtSheetDate.InnerText = this.TonnageSheet.HOJ_FECHA == null ? "-" : ((DateTime)this.TonnageSheet.HOJ_FECHA).ToString("d", new CultureInfo("es-ES"));
                this.TxtSicaiNumber.InnerText = this.TonnageSheet.HOJ_NUMERO_SICAI == null ? "-" : this.TonnageSheet.HOJ_NUMERO_SICAI.ToString();
                this.TxtSicaiDate.InnerText = this.TonnageSheet.HOJ_FECHA_SICAI == null ? "-" : ((DateTime)this.TonnageSheet.HOJ_FECHA_SICAI).ToString("d", new CultureInfo("es-ES"));
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var message = $"¿ Desea realmente eliminar la hoja de arqueo {this.TonnageSheet.HOJ_NUMERO} del año {this.TonnageSheet.HOJ_ANO} y todos sus apuntes ?";
            this.rwmManageTonnageSheet.RadConfirm(message, "confirmDeleteCallBackFn", 330, 140, null, "Confirmación");
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Treasury/SeeTonnageSheets.aspx");
        }

        protected void RgTonnageSheetDetails_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var details = new List<PRE_DETALLE_HOJA_ARQUEO>();

            if (this.TonnageSheet != null && this.TonnageSheet.HOJ_CODIGO != 0)
            {
                details = tonageSheetsService.GetDetailsById(this.TonnageSheet.HOJ_CODIGO);
            }

            this.RgTonnageSheetDetails.DataSource = details;
        }

        protected void RgTonnageSheetDetails_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var detailId = (int)item.GetDataKeyValue("DET_CODIGO");

            try
            {
                var delete = tonageSheetsService.DeleteDetail(detailId, LoginUser.USU_CODIGO);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Detalle de Hoja de Arqueo.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado el Detalle de Hoja de Arqueo en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Detalle de Hoja de Arqueo", strBuilder, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el detalle de la Hoja de Arqueo.");
                strBuilder.Append("Ha ocurrido un error eliminando el Detalle de Hoja de Arqueo en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Detalle de Hoja de Arqueo", strBuilder, MessageType.Deny);
            }
        }

        protected void btnReport_OnClick(object sender, EventArgs e)
        {
            var tonnageSheetCode = this.TonnageSheet.HOJ_CODIGO;
            var sheetNumber = this.TonnageSheet.HOJ_NUMERO;
            var is50 = this.TonnageSheet.HOJ_ARQUEO50;
            var date = ((DateTime)this.TonnageSheet.HOJ_FECHA).ToString("yyyy-MM-dd");

            var script = $"printTonnageSheet('{tonnageSheetCode}', '{sheetNumber}', '{is50}', '{date}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "printTonnageSheet", script, true);
        }

        #endregion
    }
}