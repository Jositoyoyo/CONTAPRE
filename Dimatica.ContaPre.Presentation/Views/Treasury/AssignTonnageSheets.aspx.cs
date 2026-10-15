namespace Dimatica.ContaPre.Presentation.Views.Treasury
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
    using Dimatica.ContaPre.BLL.UseCases;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class AssignTonnageSheets : BasePage
    {
        #region Fields

        private ITonnageSheetsService tonnageSheetsService = DependencyFactory.GetInstance<ITonnageSheetsService>();

        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        private ISaveTonnageSheetUseCase saveTonnageSheetUseCase = DependencyFactory.GetInstance<ISaveTonnageSheetUseCase>();

        #endregion

        #region Public Properties

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

        public int TonnageSheetCode
        {
            get
            {
                var p = this.Session["_tonnageSheetCode"];

                if (p == null)
                {
                    p = 0;
                    this.Session["_tonnageSheetCode"] = p;
                }

                return (int)p;
            }

            set
            {
                this.Session["_tonnageSheetCode"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "AssignTonnageSheets";

                this.RmyExerciseYear.SelectedDate = DateTime.Now;
                this.RdDate.SelectedDate = DateTime.Now;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (this.RntSheetNumber.Value == null)
            {
                return;
            }

            var strBuilder = new StringBuilder();

            if (this.RgTonnageSheet.SelectedItems.Count == 0)
            {
                strBuilder.Append("No se puede grabar la información porque no hay ninguna hoja de arqueo seleccionada.");
                this.ShowMessage(this.RadNotification, "Imposible grabar información", strBuilder, MessageType.Warning);
                return;
            }

            var isUpdate = this.TonnageSheetCode != 0;

            try
            {
                var userId = LoginUser.USU_CODIGO;
                var tonnageSheet = new PRE_HOJA_ARQUEO
                {
                    HOJ_CODIGO = this.TonnageSheetCode,
                    HOJ_FECHA = this.RdDate.SelectedDate
                };

                if (!isUpdate)
                {
                    var item = (GridDataItem)this.RgTonnageSheet.Items[0];
                    tonnageSheet.HOJ_ANO = Convert.ToInt16(((DateTime)this.RmyExerciseYear.SelectedDate).Year);
                    tonnageSheet.HOJ_NUMERO = Convert.ToInt32(this.RntSheetNumber.Value);
                    tonnageSheet.HOJ_ARQUEO50 = this.RrbFifty.SelectedValue == "1";
                    tonnageSheet.CUE_CODIGO = item.GetDataKeyValue("CUE_CODIGO") == null
                        ? (int?)null
                        : (int)item.GetDataKeyValue("CUE_CODIGO");
                }

                var details = new List<PRE_DETALLE_HOJA_ARQUEO>();
                foreach (GridDataItem item in this.RgTonnageSheet.Items)
                {
                    details.Add(new PRE_DETALLE_HOJA_ARQUEO
                    {
                        DET_FECHA_APUNTE = (DateTime)item.GetDataKeyValue("DET_FECHA_APUNTE"),
                        DET_IMPORTE = (decimal)item.GetDataKeyValue("DET_IMPORTE"),
                        DOC_CODIGO = item.GetDataKeyValue("DOC_CODIGO") == null ? (int?)null : (int)item.GetDataKeyValue("DOC_CODIGO"),
                        EXP_EXTRAP_CODIGO = item.GetDataKeyValue("EXP_EXTRAP_CODIGO") == null ? (int?)null : (int)item.GetDataKeyValue("EXP_EXTRAP_CODIGO"),
                        DET_NUMERO_EXPEDIENTE = item.GetDataKeyValue("DET_NUMERO_EXPEDIENTE") == null ? (int?)null : (int)item.GetDataKeyValue("DET_NUMERO_EXPEDIENTE"),
                        EXP_ANO_PRESUPUESTO = Convert.ToInt16((int)item.GetDataKeyValue("EXP_ANO_PRESUPUESTO")),
                        LIN_NUMERO = item.GetDataKeyValue("LIN_NUMERO") == null ? (int?)null : (int)item.GetDataKeyValue("LIN_NUMERO"),
                        MARCADO = item.Selected
                    });
                }

                var result = this.saveTonnageSheetUseCase.SaveTonnageSheet(tonnageSheet, details, userId);

                if (result.ResponseCode != ResponseCode.Ok)
                {
                    if (result.ResponseCode == ResponseCode.Invalid)
                    {
                        strBuilder.Append("Debe completar todos los datos de la Hoja de Arqueo.");
                    }
                    else if (isUpdate && result.ResponseCode == ResponseCode.NotFound)
                    {
                        strBuilder.Append("No se ha encontrado la Hoja de Arqueo en cuestión.");
                    }

                    var title = isUpdate ? "Error modificando la Hoja de Arqueo" : "Error insertando la Hoja de Arqueo";
                    this.ShowMessage(this.RadNotification, title, strBuilder, MessageType.Warning);
                    return;
                }

                this.FillTonnageSheets(true);
            }
            catch (Exception ex)
            {
                var operation = isUpdate ? "modificando" : "insertando";
                var title = isUpdate ? "Error modificando la Hoja de Arqueo" : "Error insertando la Hoja de Arqueo";
                LogError(ex, "Error " + operation + " la Hoja de Arqueo.");
                strBuilder.Append("Ha ocurrido un error " + operation + " la Hoja de Arqueo en cuestión.");
                this.ShowMessage(this.RadNotification, title, strBuilder, MessageType.Deny);
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            if (this.RntSheetNumber.Value == null)
            {
                this.createReport.Visible = false;
                return;
            }

            this.RgTonnageSheet.Visible = true;
            this.FillTonnageSheets(true);
        }

        protected void btnReport_OnClick(object sender, EventArgs e)
        {
            if (this.RntSheetNumber.Value == null)
            {
                return;
            }

            var strBuilder = new StringBuilder();

            if (this.RgTonnageSheet.SelectedItems.Count == 0)
            {
                strBuilder.Append("No se puede hacer hoja de arqueo porque no hay ninguna hoja de arqueo seleccionada.");
                this.ShowMessage(this.RadNotification, "Imposible hacer hoja", strBuilder, MessageType.Warning);
                return;
            }

            var sheetNumber = this.RntSheetNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntSheetNumber.Value);
            var is50 = this.RrbFifty.SelectedValue;
            var date = ((DateTime)this.RdDate.SelectedDate).ToString("yyyy-MM-dd");

            if (this.TonnageSheetCode == 0 || sheetNumber == null)
            {
                strBuilder.Append("No se puede hacer hoja de arqueo porque faltan datos por seleccionar.");
                this.ShowMessage(this.RadNotification, "Imposible hacer hoja", strBuilder, MessageType.Warning);
                return;
            }

            try
            {
                var tonnageSheet = new PRE_HOJA_ARQUEO
                {
                    HOJ_CODIGO = this.TonnageSheetCode,
                    HOJ_FECHA = this.RdDate.SelectedDate,
                    USU_CODIGO = LoginUser.USU_CODIGO
                };

                var update = this.tonnageSheetsService.UpdateTonnageSheet(tonnageSheet);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Hoja de Arqueo.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Hoja de Arqueo en cuestión.");

                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error modificando la Hoja de Arqueo", strBuilder, MessageType.Warning);
                    return;
                }

                var script = $"printTonnageSheet('{this.TonnageSheetCode}', '{sheetNumber}', '{is50}', '{date}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "printTonnageSheet", script, true);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error modificando la Hoja de Arqueo.");
                strBuilder.Append("Ha ocurrido un error modificando la Hoja de Arqueo en cuestión.");
                this.ShowMessage(this.RadNotification, "Error modificando la Hoja de Arqueo", strBuilder, MessageType.Deny);
            }
        }


        protected void RgTonnageSheet_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillTonnageSheets(false);
        }

        protected void RgTonnageSheet_OnPreRender(object sender, EventArgs e)
        {
            for (var i = 0; i < this.RgTonnageSheet.Items.Count; i++)
            {
                var item = this.RgTonnageSheet.Items[i];
                var check = (bool)item.GetDataKeyValue("MARCADO");

                if (check)
                {
                    item.Selected = true;
                }
            }
        }

        private void FillTonnageSheets(bool manual)
        {
            this.HasTonnageSheets = false;
            this.btnReport.Enabled = false;

            var exerciseYear = this.RmyExerciseYear.SelectedDate?.Year;
            var sheetNumber = this.RntSheetNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntSheetNumber.Value);
            var is50 = this.RrbFifty.SelectedValue.Equals("1");

            var sheets = new List<PRE_HOJA_ARQUEO>();

            if (exerciseYear != null && sheetNumber != null)
            {
                var sheetCode = this.tonnageSheetsService.GetTonnageSheetCode((int)exerciseYear, (int)sheetNumber, is50);

                if (sheetCode != -1)
                {
                    this.TonnageSheetCode = sheetCode;
                    sheets = this.tonnageSheetsService.GetTonnageSheetsDetails((int)exerciseYear, (int)sheetNumber, is50);
                }
                else
                {
                    this.TonnageSheetCode = 0;
                    sheets = this.tonnageSheetsService.GetTonnageSheetsDetailsWithoutSheet((int)exerciseYear, (int)sheetNumber, is50);
                }
            }

            this.RgTonnageSheet.DataSource = sheets;

            if (manual)
            {
                this.RgTonnageSheet.DataBind();
            }

            this.createReport.Visible = sheets.Any();

            if (sheets.Any())
            {
                this.HasTonnageSheets = true;
                this.btnReport.Enabled = true;
            }
        }

        #endregion

    }
}
