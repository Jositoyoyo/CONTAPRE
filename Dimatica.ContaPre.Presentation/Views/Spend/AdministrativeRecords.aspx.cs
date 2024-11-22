namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class AdministrativeRecords : BasePage
    {
        #region Fields

        private IAdministrativeRecordsService administrativeRecordsService = DependencyFactory.GetInstance<IAdministrativeRecordsService>();

        private IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "AdministrativeRecords";

                var provenances = this.provenancesService.GetProvenances("G");
                provenances.Insert(
                                   0,
                                   new PRE_PROCEDENCIA
                                           {
                                                   PROC_CODIGO = -1,
                                                   PROC_DESCRIPCION = "< Seleccione >"
                                           });

                this.RcProvenances.DataSource = provenances;
                this.RcProvenances.DataBind();
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillRecords(true);
        }

        protected void RgRecords_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillRecords(false);
        }

        protected void RgRecords_OnPreRender(object sender, EventArgs e) { }

        protected void RgRecords_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAdministrativeRecord")
            {
                var item = e.Item as GridDataItem;
                var recordId = (int)item.GetDataKeyValue("EA_CODIGO");

                this.Response.Redirect($"~/Views/Spend/ManageAdministrativeRecord.aspx?id={recordId}");
            }
        }

        private void FillRecords(bool manual)
        {
            var exerciseYear = this.RmyExerciseYear.SelectedDate?.Year;
            var recordNumber = this.RntNumber.Value == null ? (int?)null : Convert.ToInt32(this.RntNumber.Value);
            var provenanceId = this.RcProvenances.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.RcProvenances.SelectedValue);
            var description = string.IsNullOrWhiteSpace(this.RtbDescription.Text) ? string.Empty : this.RtbDescription.Text;

            var records = new List<PRE_EXPEDIENTE_ADMINISTRATIVO>();

            if (exerciseYear != null || recordNumber != null || provenanceId != null || !string.IsNullOrWhiteSpace(description))
            {
                records = this.administrativeRecordsService.GetSpends(exerciseYear, recordNumber, provenanceId, description);
            }

            this.RgRecords.DataSource = records;

            if (manual)
            {
                this.RgRecords.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }

        #endregion
    }
}