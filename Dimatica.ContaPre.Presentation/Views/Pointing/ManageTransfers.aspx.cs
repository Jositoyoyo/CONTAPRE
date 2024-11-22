namespace Dimatica.ContaPre.Presentation.Views.Pointing
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class ManageTransfers : BasePage
    {
        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        private ISingsService singsService = DependencyFactory.GetInstance<ISingsService>();

        #endregion

        #region Private Properties

        private PRE_SENALAMIENTO Pointing
        {
            get
            {
                var pointing = this.Session["_currentPointing"] as PRE_SENALAMIENTO;

                if (pointing == null)
                {
                    pointing = new PRE_SENALAMIENTO();

                    this.Session["_currentPointing"] = pointing;
                }

                return pointing;
            }

            set
            {
                this.Session["_currentPointing"] = value;
            }
        }

        private List<PRE_SENALAMIENTO_DOCUMENTO> Transfers
        {
            get
            {
                var groupTransfers = this.Session["_transfers"];

                if (groupTransfers == null)
                {
                    groupTransfers = new List<PRE_SENALAMIENTO_DOCUMENTO>();
                    this.Session["_transfers"] = groupTransfers;
                }

                return (List<PRE_SENALAMIENTO_DOCUMENTO>)groupTransfers;
            }

            set
            {
                this.Session["_transfers"] = value;
            }
        }

        private bool GroupTransfers
        {
            get
            {
                var groupTransfers = this.Session["_group"];

                if (groupTransfers == null)
                {
                    groupTransfers = false;
                    this.Session["_group"] = groupTransfers;
                }

                return Convert.ToBoolean(groupTransfers);
            }

            set
            {
                this.Session["_group"] = value;
            }
        }

        private bool ShowNotification
        {
            get
            {
                var notification = this.Session["_showNotification"];

                if (notification == null)
                {
                    notification = false;
                    this.Session["_showNotification"] = notification;
                }

                return Convert.ToBoolean(notification);
            }

            set
            {
                this.Session["_showNotification"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var pointingId = this.Request.QueryString["id"];
                var groupTransfers = this.Request.QueryString["group"];

                if (string.IsNullOrWhiteSpace(pointingId) || string.IsNullOrWhiteSpace(groupTransfers))
                {
                    this.Response.Redirect("~/Views/Pointing/Sings.aspx");
                }
                else
                {
                    var pointing = this.singsService.GetById(Convert.ToInt32(pointingId));

                    if (pointing == null)
                    {
                        this.Response.Redirect("~/Views/Pointing/Sings.aspx");
                    }
                    else
                    {
                        this.Pointing = pointing;
                        this.GroupTransfers = groupTransfers.Equals("1");
                        var transfers = this.singsService.GetTransfersByPointingId(this.Pointing.SEN_CODIGO, this.GroupTransfers);
                        this.Transfers = transfers;

                        this.RtbIbanUimp.Text = "ES95 9000 0001 2002 0000 8606";
                        this.TxtTransfersAmount.InnerText = this.Pointing.SEN_TOTAL_LIQUIDO.ToString("N");

                        if (this.GroupTransfers && this.Transfers.Count > 1)
                        {
                            this.ShowNotification = true;
                            transfers = this.singsService.GetTransfersByPointingId(this.Pointing.SEN_CODIGO, false);
                            this.Transfers = transfers;

                            this.FillTransfers();
                        }
                        else
                        {
                            this.FillTransfers();
                        }
                    }
                }
            }
        }

        private void FillTransfers()
        {
            this.RtbTransferNumber.Text = this.Transfers.Any(t => t.PROV_CODIGO != null) ? this.Transfers.FirstOrDefault(t => t.PROV_CODIGO != null).NUMERO_CHEQUE : string.Empty;
            this.TxtTransfersCount.InnerText = this.Transfers.Count.ToString();
           
            for (var i = 0; i < this.Transfers.Count; i++)
            {
                var providerName = string.IsNullOrWhiteSpace(this.Transfers[i].PERCEPTOR) ? string.Empty : this.Transfers[i].PERCEPTOR;
                var amount = this.Transfers[i].IMPORTE_LIQUIDO.ToString("N");
                var ccEntity = this.Transfers[i].PROV_CC_CE;
                var ccBranch = this.Transfers[i].PROV_CC_CO;
                var ccDc = this.Transfers[i].PROV_CC_DC;
                var ccAccount = this.Transfers[i].PROV_CC_NC;
                var iban = this.Transfers[i].PROV_IBAN;
                var concept = this.Transfers[i].DOC_FACTURA;
                var entity = this.Transfers[i].PROV_NOMBRE_SUCURSAL;
                var address = this.Transfers[i].PROV_DIR_SUCURSAL;
                var location = this.Transfers[i].PROV_POBLACION_SUCURSAL;

                switch (i)
                {
                    case 0:
                        this.txtProvider1.InnerText = providerName;
                        this.TxtAmount1.InnerText = amount;
                        this.TxtCcEntity1.Text = ccEntity;
                        this.TxtCcBranch1.Text = ccBranch;
                        this.TxtCcDc1.Text = ccDc;
                        this.TxtCcAccount1.Text = ccAccount;
                        this.TxtIban1.Text = iban;
                        this.TxtConcept1.Text = concept;
                        this.TxtEntity1.Text = entity;
                        this.TxtAddress1.Text = address;
                        this.TxtLocation1.Text = location;

                        break;
                    case 1:
                        this.txtProvider2.InnerText = providerName;
                        this.TxtAmount2.InnerText = amount;
                        this.TxtCcEntity2.Text = ccEntity;
                        this.TxtCcBranch2.Text = ccBranch;
                        this.TxtCcDc2.Text = ccDc;
                        this.TxtCcAccount2.Text = ccAccount;
                        this.TxtIban2.Text = iban;
                        this.TxtConcept2.Text = concept;
                        this.TxtEntity2.Text = entity;
                        this.TxtAddress2.Text = address;
                        this.TxtLocation2.Text = location;

                        break;
                    case 2:
                        this.txtProvider3.InnerText = providerName;
                        this.TxtAmount3.InnerText = amount;
                        this.TxtCcEntity3.Text = ccEntity;
                        this.TxtCcBranch3.Text = ccBranch;
                        this.TxtCcDc3.Text = ccDc;
                        this.TxtCcAccount3.Text = ccAccount;
                        this.TxtIban3.Text = iban;
                        this.TxtConcept3.Text = concept;
                        this.TxtEntity3.Text = entity;
                        this.TxtAddress3.Text = address;
                        this.TxtLocation3.Text = location;

                        break;
                    case 3:
                        this.txtProvider4.InnerText = providerName;
                        this.TxtAmount4.InnerText = amount;
                        this.TxtCcEntity4.Text = ccEntity;
                        this.TxtCcBranch4.Text = ccBranch;
                        this.TxtCcDc4.Text = ccDc;
                        this.TxtCcAccount4.Text = ccAccount;
                        this.TxtIban4.Text = iban;
                        this.TxtConcept4.Text = concept;
                        this.TxtEntity4.Text = entity;
                        this.TxtAddress4.Text = address;
                        this.TxtLocation4.Text = location;

                        break;
                    case 4:
                        this.txtProvider5.InnerText = providerName;
                        this.TxtAmount5.InnerText = amount;
                        this.TxtCcEntity5.Text = ccEntity;
                        this.TxtCcBranch5.Text = ccBranch;
                        this.TxtCcDc5.Text = ccDc;
                        this.TxtCcAccount5.Text = ccAccount;
                        this.TxtIban5.Text = iban;
                        this.TxtConcept5.Text = concept;
                        this.TxtEntity5.Text = entity;
                        this.TxtAddress5.Text = address;
                        this.TxtLocation5.Text = location;

                        break;
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.GroupTransfers && this.Transfers.Count > 1)
            {
                if (this.ShowNotification)
                {
                    this.ShowNotification = false;
                    var strBuilder = new StringBuilder();
                    strBuilder.Append("Compruebe los datos y pulse de nuevo [Agrupar].");
                    this.ShowMessage(this.RadNotification, "Datos incorrectos", strBuilder, MessageType.Warning);
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideDivs", $"hideDivs('{this.Transfers.Count}');", true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            try
            {
                var checkNumber = this.Transfers.Any(t => t.PROV_CODIGO != null) ? this.Transfers.FirstOrDefault(t => t.PROV_CODIGO != null).NUMERO_CHEQUE : string.Empty;

                var errorProvider = false;
                var errorConcept = false;
                var errorNumber = false;

                for (var i = 0; i < this.Transfers.Count; i++)
                {
                    var providerCode = this.Transfers[i].PROV_CODIGO;

                    var concept = string.Empty;

                    if (providerCode != null)
                    {
                        var ccEntity = string.Empty;
                        var ccBranch = string.Empty;
                        var ccDc = string.Empty;
                        var ccAccount = string.Empty;
                        var entity = string.Empty;
                        var address = string.Empty;
                        var location = string.Empty;

                        switch (i)
                        {
                            case 0:
                                ccEntity = this.TxtCcEntity1.Text;
                                ccBranch = this.TxtCcBranch1.Text;
                                ccDc = this.TxtCcDc1.Text;
                                ccAccount = this.TxtCcAccount1.Text;
                                entity = this.TxtEntity1.Text;
                                address = this.TxtAddress1.Text;
                                location = this.TxtLocation1.Text;
                                concept = this.TxtConcept1.Text;

                                break;
                            case 1:
                                ccEntity = this.TxtCcEntity2.Text;
                                ccBranch = this.TxtCcBranch2.Text;
                                ccDc = this.TxtCcDc2.Text;
                                ccAccount = this.TxtCcAccount2.Text;
                                entity = this.TxtEntity2.Text;
                                address = this.TxtAddress2.Text;
                                location = this.TxtLocation2.Text;
                                concept = this.TxtConcept2.Text;

                                break;
                            case 2:
                                ccEntity = this.TxtCcEntity3.Text;
                                ccBranch = this.TxtCcBranch3.Text;
                                ccDc = this.TxtCcDc3.Text;
                                ccAccount = this.TxtCcAccount3.Text;
                                entity = this.TxtEntity3.Text;
                                address = this.TxtAddress3.Text;
                                location = this.TxtLocation3.Text;
                                concept = this.TxtConcept3.Text;

                                break;
                            case 3:
                                ccEntity = this.TxtCcEntity4.Text;
                                ccBranch = this.TxtCcBranch4.Text;
                                ccDc = this.TxtCcDc4.Text;
                                ccAccount = this.TxtCcAccount4.Text;
                                entity = this.TxtEntity4.Text;
                                address = this.TxtAddress4.Text;
                                location = this.TxtLocation4.Text;
                                concept = this.TxtConcept4.Text;

                                break;
                            case 4:
                                ccEntity = this.TxtCcEntity5.Text;
                                ccBranch = this.TxtCcBranch5.Text;
                                ccDc = this.TxtCcDc5.Text;
                                ccAccount = this.TxtCcAccount5.Text;
                                entity = this.TxtEntity5.Text;
                                address = this.TxtAddress5.Text;
                                location = this.TxtLocation5.Text;
                                concept = this.TxtConcept5.Text;

                                break;
                        }

                        var updateProvider = this.providersService.UpdateBranchDatas((int)providerCode, ccEntity, ccBranch, ccDc, ccAccount, entity, address, location, LoginUser.USU_CODIGO);

                        if (updateProvider.ResponseCode != ResponseCode.Ok)
                        {
                            errorProvider = true;
                        }
                    }

                    if (!this.GroupTransfers)
                    {
                        var documentCode = this.Transfers[i].DOC_CODIGO_AUX;

                        if (!string.IsNullOrWhiteSpace(documentCode))
                        {
                            var updateConcept = this.accountingDocumentsService.UpdateBillConcept(Convert.ToInt32(documentCode), concept, LoginUser.USU_CODIGO);

                            if (updateConcept.ResponseCode != ResponseCode.Ok)
                            {
                                errorConcept = true;
                            }

                            if (!this.RtbTransferNumber.Text.Equals(checkNumber))
                            {
                                var updateTransferNumber = this.accountingDocumentsService.UpdateTransferNumber(Convert.ToInt32(documentCode), this.RtbTransferNumber.Text, LoginUser.USU_CODIGO);

                                if (updateTransferNumber.ResponseCode != ResponseCode.Ok)
                                {
                                    errorNumber = true;
                                }
                            }
                        }
                    }
                }

                if (!this.RtbTransferNumber.Text.Equals(checkNumber))
                {
                    var updateTransferNumber = this.accountingDocumentsService.UpdateTransferNumber(this.Pointing.SEN_CODIGO, this.RtbTransferNumber.Text, LoginUser.USU_CODIGO);

                    if (updateTransferNumber.ResponseCode != ResponseCode.Ok)
                    {
                        errorNumber = true;
                    }
                }

                if (this.GroupTransfers)
                {
                    var documentCode = this.Transfers.Any() ? this.Transfers.FirstOrDefault().DOC_CODIGO_AUX : string.Empty;

                    if (!string.IsNullOrWhiteSpace(documentCode))
                    {
                        var splits = documentCode.Split(',');

                        foreach (var code in splits)
                        {
                            if (string.IsNullOrWhiteSpace(code))
                            {
                                continue;
                            }

                            var updateConcept = this.accountingDocumentsService.UpdateBillConcept(Convert.ToInt32(code), this.TxtConcept1.Text, LoginUser.USU_CODIGO);

                            if (updateConcept.ResponseCode != ResponseCode.Ok)
                            {
                                errorConcept = true;
                            }

                            if (!this.RtbTransferNumber.Text.Equals(checkNumber))
                            {
                                var updateTransferNumber = this.accountingDocumentsService.UpdateTransferNumber(Convert.ToInt32(code), this.RtbTransferNumber.Text, LoginUser.USU_CODIGO);

                                if (updateTransferNumber.ResponseCode != ResponseCode.Ok)
                                {
                                    errorNumber = true;
                                }
                            }
                        }
                    }
                }

                if (errorProvider)
                {
                    strBuilder.Append("Ha ocurrido un error actualizando la información de algún proveedor.<br/>");
                }

                if (errorConcept)
                {
                    strBuilder.Append("Ha ocurrido un error actualizando la información de alguna factura.<br/>");
                }

                if (errorNumber)
                {
                    strBuilder.Append("Ha ocurrido un error actualizando la información de alguna transferencia.<br/>");
                }

                if (errorProvider || errorConcept || errorNumber)
                {
                    this.ShowMessage(this.RadNotification, "Imposible grabar la información", strBuilder, MessageType.Warning);
                }
                else
                {
                    strBuilder.Append("La información de las transferencias se ha actualizado satisfactoriamente.");
                    this.ShowMessage(this.RadNotification, "Información actualizada", strBuilder, MessageType.Ok);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando la información de las facturas del señalamiento.");
                strBuilder.Append("Ha ocurrido un error grabando la información de las facturas del señalamiento en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible grabar la información", strBuilder, MessageType.Deny);
            }
        }

        protected void btnPrint_OnClick(object sender, EventArgs e) { }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect($"~/Views/Pointing/ManagePointing.aspx?id={this.Pointing.SEN_CODIGO}");
        }

        #endregion
    }
}