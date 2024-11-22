namespace Dimatica.ContaPre.Presentation.Views.Treasury
{
    #region NameSpaces

    using System;
    using System.Linq;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class NotesTreasuries : BasePage
    {
        #region Fields

        private IOriginsService originsService = DependencyFactory.GetInstance<IOriginsService>();

        private IPayFormsService payFormsService = DependencyFactory.GetInstance<IPayFormsService>();

        private IPayTypesService payTypesService = DependencyFactory.GetInstance<IPayTypesService>();

        private IRecordTypesService recordTypesService = DependencyFactory.GetInstance<IRecordTypesService>();

        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "NotesTreasuries";
                this.RmyExerciseYear.SelectedDate = DateTime.Now;

                var origins = this.originsService.GetOrigins();
                origins.Insert(
                               0,
                               new PRE_ORIGEN
                                       {
                                               ORI_CODIGO_AUX = -1,
                                               ORI_DESCRIPCION = "< Seleccione >"
                                       });
                this.RcOriginCode.DataSource = origins;
                this.RcOriginCode.DataBind();

                var payForms = this.payFormsService.GetPayForms();
                payForms.Insert(
                                0,
                                new PRE_FORMA_PAGO
                                        {
                                                FOR_CODIGO_AUX = -1
                                        });
                this.RcPayFormCode.DataSource = payForms;
                this.RcPayFormCode.DataBind();

                var recordTypes = this.recordTypesService.GetRecordTypes();
                recordTypes.Insert(
                                   0,
                                   new PRE_TIPO_REGISTRO
                                           {
                                                   TIPR_CODIGO = -1
                                           });
                this.RcRegisterTypeCode.DataSource = recordTypes;
                this.RcRegisterTypeCode.DataBind();
            }
        }

        protected void RgTonnageSheet_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillTreasuriesFilters(false);
        }

        protected void RgTonnageSheet_InsertCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_UpdateCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_DeleteCommand(object sender, GridCommandEventArgs e) { }

        protected void RgTonnageSheet_ItemDataBound(object sender, GridItemEventArgs e) { }

        protected void RgTonnageSheet_PreRender(object sender, EventArgs e) { }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.FillTreasuriesFilters(true);
        }

        private void FillTreasuriesFilters(bool manual)
        {
            var exerciseYear = this.RmyExerciseYear.SelectedDate?.Year;
            var origingCode = this.RcOriginCode.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcOriginCode.SelectedValue);
            var amount = this.RntTreasuryAmount.Value == null ? (decimal?)null : Convert.ToDecimal(this.RntTreasuryAmount.Value);
            var sinceBankDate = this.RdpSinceBankDate.SelectedDate?.ToString("yyyy-MM-dd");
            var untilBankDate = this.RdpUntilBankDate.SelectedDate?.ToString("yyyy-MM-dd");
            var payFormCode = this.RcPayFormCode.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcPayFormCode.SelectedValue);
            var checkNumber = string.IsNullOrWhiteSpace(this.RtbCheckNumber.Text) ? null : this.RtbCheckNumber.Text;
            var treasuryHave = this.RcTreasuryHave.SelectedValue.Equals("-1") ? (bool?)null : this.RcTreasuryHave.SelectedValue.Equals("1");
            var description = string.IsNullOrWhiteSpace(this.txtDescription.Text) ? null : this.txtDescription.Text;
            var sinceDateEntry = this.RdpSinceEntryDate.SelectedDate?.ToString("yyyy-MM-dd");
            var untilDateEntry = this.RdpUntilEntryDate.SelectedDate?.ToString("yyyy-MM-dd");
            var registerTypeCode = this.RcRegisterTypeCode.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcRegisterTypeCode.SelectedValue);
            var isCanceled = this.RcIsCanceled.SelectedValue.Equals("-1") ? (bool?)null : this.RcIsCanceled.SelectedValue.Equals("1");
            var isBound = this.RcIsBound.SelectedValue.Equals("-1") ? (bool?)null : this.RcIsBound.SelectedValue.Equals("1");
            var pendingDate = this.RdpPendingDate.SelectedDate?.ToString("yyyy-MM-dd");
            var sinceDocYear = this.RmyDocSince.SelectedDate?.Year;
            var untilDocYear = this.RmyDocUntil.SelectedDate?.Year;

            var treasuries = this.treasuriesService.GetTreasuryByFilters((int?)exerciseYear, (int?)origingCode, (decimal?)amount, sinceBankDate, untilBankDate, (int?)payFormCode, checkNumber, (bool?)treasuryHave, description, sinceDateEntry, untilDateEntry, (int?)registerTypeCode, (bool?)isCanceled, (bool?)isBound, pendingDate, sinceDocYear, untilDocYear, string.Empty);

            this.RgTonnageSheet.DataSource = treasuries;    

            if (manual)
            {
                this.RgTonnageSheet.DataBind();
            }

            var total = treasuries.Sum(r => r.TES_TOTAL_IMPORTE_LIQUIDO);
            this.txtTotal.Text = ((decimal)total).ToString("N");

            this.RpbFilter.CollapseAllItems();
        }

        protected void RgTonnageSheet_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateTreasury")
            {
                var item = e.Item as GridDataItem;
                var treasuryId = (int)item.GetDataKeyValue("TES_CODIGO");

                this.Response.Redirect($"~/Views/Treasury/ManageTreasury.aspx?id={treasuryId}");
            }
        }

        #endregion
    }
}