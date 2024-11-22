namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class CheckPurchases : BasePage
    {
        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        private IBillPurchasesService billPurchasesService = DependencyFactory.GetInstance<IBillPurchasesService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "CheckPurchases";

                var providers = this.providersService.GetBillProvidersToCombo();
                providers.Insert(
                                 0,
                                 new PRE_PROVEEDOR
                                 {
                                     PROV_CODIGO = -1,
                                     PROV_NOMBRE = "< Seleccione >"
                                 });
                this.RcProviders.DataSource = providers;
                this.RcProviders.DataBind();
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillRecords(true);
        }

        protected void RgPurchases_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillRecords(false);
        }

        protected void RgPurchases_OnPreRender(object sender, EventArgs e) { }

        protected void RgPurchases_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateSpendRecord")
            {
                var item = e.Item as GridDataItem;
                var recordId = (int)item.GetDataKeyValue("EXP_CODIGO");

                this.Session["_currentSource"] = this.Request.Url.AbsoluteUri;
                this.Response.Redirect($"~/Views/Spend/ManageSpendRecord.aspx?id={recordId}");
            }
        }

        private void FillRecords(bool manual)
        {
            var result = new List<PRE_FACTURA_COMPRA>();
            var providerId = this.RcProviders.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.RcProviders.SelectedValue);
            var billNumber = string.IsNullOrWhiteSpace(this.RtbNumber.Text) ? string.Empty : this.RtbNumber.Text;
            var roAmount = this.RntRoAmount.Value == null ? (decimal?)null : Convert.ToDecimal(this.RntRoAmount.Value);
            var exerciseYear = this.RmyExerciseYear.SelectedDate?.Year;
            var billDate = this.RdpBillDate.SelectedDate?.ToString("dd/MM/yyyy");
            var roDate = this.RdpRoDate.SelectedDate?.ToString("dd/MM/yyyy");
            var administrativeId = this.RntAdministrative.Value == null ? (int?)null : Convert.ToInt32(this.RntAdministrative.Value);
            var billAmount = this.RntFraAmount.Value == null ? (decimal?)null : Convert.ToDecimal(this.RntFraAmount.Value);

            if (providerId != null || !string.IsNullOrWhiteSpace(billNumber) || roAmount != null || exerciseYear != null || !string.IsNullOrWhiteSpace(billDate) || !string.IsNullOrWhiteSpace(roDate) || administrativeId != null || billAmount != null)
            {
                var purchases = this.billPurchasesService.GetBillPurchases(exerciseYear, administrativeId, billNumber, billDate, billAmount, roDate, providerId, roAmount, string.Empty);

                foreach (var purchase in purchases)
                {
                    result.Add(purchase);
                }
            }

            this.RgPurchases.DataSource = result;

            if (manual)
            {
                this.RgPurchases.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }

        #endregion

        protected void RgPurchases_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var strBuilder = new StringBuilder();
            var item = e.Item as GridDataItem;

            var id = (int)item.GetDataKeyValue("FA_CODIGO");
            var bound = item.GetDataKeyValue("BoundLabel").ToString();

            try
            {
                if (bound.Equals("Si"))
                {
                    strBuilder.Append("La factura no se puede eliminar porque está enlazada a un documento contable.");
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Factura", strBuilder, MessageType.Warning);
                    return;
                }

                var delete = this.billPurchasesService.DeleteBillPurchase(id);

                switch (delete.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos de la Factura.");

                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("La factura no se puede eliminar porque está enlazada a un documento contable.");

                        break;
                    case ResponseCode.NotFound:
                        strBuilder.Append("No se ha encontrado la Factura en cuestión.");

                        break;
                }

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Imposible eliminar Factura", strBuilder, MessageType.Warning);
                }
                else
                {
                    var billGeiCode = item.GetDataKeyValue("CODFACTURAGEI");
                    var billGei = billGeiCode == null ? (int?)null : Convert.ToInt32(billGeiCode);

                    // TODO: aqui se llamaba a oracle
                    //expediente.actualizaFechaTrasladoGEI("", codFacturaGEI, bModificado)
                    //expediente.actualizaImportadaContabl("N", codFacturaGEI, bModificado)
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando la factura.");
                strBuilder.Append("Ha ocurrido un error eliminando la Factura en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible eliminar Factura", strBuilder, MessageType.Deny);
            }
        }

        protected void RgPurchases_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["EditColumn"].ToolTip = "Editar Factura";
                dataBoundItem["DeleteColumn"].ToolTip = "Eliminar Factura";

                //var prov = (string)dataBoundItem.GetDataKeyValue("PROV_NOMBRE");
                //((GridButtonColumn)dataBoundItem["DeleteColumn"]).ConfirmText = $"Delete {prov}";
                //(this.RgPurchases.MasterTableView.GetColumn("DeleteColumn") as GridButtonColumn).ConfirmText = $"Delete {prov}";
            }
        }
    }
}