<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageTransfers.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Pointing.ManageTransfers" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_pointing" class="container">

        <h3 id="titleHeader" runat="server">Transferencias Proveedores</h3>

        <div id="page_manageTransfers" class="box-block">
            <telerik:RadAjaxPanel ID="rapManageTransfers"
                runat="server"
                LoadingPanelID="ralPrincipal">
                <telerik:RadNotification ID="RadNotification"
                    runat="server"
                    RenderMode="Lightweight"
                    VisibleOnPageLoad="False"
                    Position="TopRight"
                    Width="500"
                    Height="100"
                    Animation="Fade"
                    EnableRoundedCorners="True"
                    EnableShadow="False"
                    ContentScrolling="Auto"
                    Opacity="100"
                    ShowSound="None"
                    AutoCloseDelay="0"
                    LoadContentOn="EveryShow"
                    Style="z-index: 100000"
                    OnClientShowing="showModalDiv"
                    OnClientHidden="hideModalDiv">
                </telerik:RadNotification>

                <%--MANAGE--%>
                <div class="manage">

                    <%--Transferencia--%>
                    <div class="form-group form-group-fake">
                        <%--IBAN UIMP--%>
                        <div class="field-container">
                            <strong>IBAN UIMP:</strong>
                            <telerik:RadTextBox ID="RtbIbanUimp"
                                runat="server"
                                Width="300px"
                                Text=""
                                MaxLength="36">
                            </telerik:RadTextBox>
                        </div>

                        <%--Transferencia N⁰--%>
                        <div class="field-container">
                            <strong>Transferencia N⁰:</strong>
                            <telerik:RadTextBox ID="RtbTransferNumber"
                                runat="server"
                                Width="100px"
                                Text=""
                                MaxLength="12">
                            </telerik:RadTextBox>
                        </div>

                        <%--N⁰ de Transfs.--%>
                        <div class="field-container">
                            <strong>N⁰ de Transfs.:</strong>
                            <h4 id="TxtTransfersCount" runat="server"></h4>
                        </div>

                        <%--Importe--%>
                        <div class="field-container">
                            <strong>Importe Total:</strong>
                            <h4 id="TxtTransfersAmount" runat="server"></h4>
                        </div>
                    </div>

                    <%--BUTTONS--%>
                    <div class="form-group buttons">
                        <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                            runat="server"
                            RenderMode="Native"
                            Text="Grabar"
                            AutoPostBack="True"
                            OnClick="btnSave_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnPrint"
                            runat="server"
                            RenderMode="Native"
                            Text="Imprimir"
                            AutoPostBack="True"
                            OnClick="btnPrint_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                            runat="server"
                            RenderMode="Native"
                            Text="Volver"
                            AutoPostBack="True"
                            OnClick="btnBack_OnClick">
                        </telerik:RadButton>
                    </div>

                    <%--TRANSFER 1--%>
                    <div id="transfer1" class="form-group form-group-fake transfer">

                        <%--Header--%>
                        <div class="form-group form-group-horizontal">
                            
                            <div class="field-container">
                                <h4>1</h4>
                            </div>
                            
                            <div class="field-container">
                                <h4>
                                    Beneficiario: <strong id="txtProvider1" runat="server"></strong>

                                </h4>
                            </div>

                            <div class="field-container">
                                <h4>
                                    Importe: <strong id="TxtAmount1" runat="server"></strong>
                                </h4>
                            </div>

                        </div>

                        <%--CCC--%>
                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">CCC</span>
                            </div>

                            <div class="field-container">
                                <span>Entidad:</span>
                                <telerik:RadTextBox ID="TxtCcEntity1"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Sucursal:</span>
                                <telerik:RadTextBox ID="TxtCcBranch1"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>DC:</span>
                                <telerik:RadTextBox ID="TxtCcDc1"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="2"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Cuenta:</span>
                                <telerik:RadTextBox ID="TxtCcAccount1"
                                    Width="120px"
                                    runat="server"
                                    MaxLength="10"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">IBAN:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtIban1"
                                    runat="server"
                                    Width="180px"
                                    Text=""
                                    MaxLength="36">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Concepto:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtConcept1"
                                    runat="server"
                                    Width="180px"
                                    Text=""
                                    MaxLength="75">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Entidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtEntity1"
                                    runat="server"
                                    Width="180px"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Domicilio:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtAddress1"
                                    runat="server"
                                    Width="180px"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Localidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtLocation1"
                                    runat="server"
                                    Width="180px"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                    </div>

                    <%--TRANSFER 2--%>
                    <div id="transfer2" class="form-group form-group-fake transfer">

                        <%--Header--%>
                        <div class="form-group form-group-horizontal">
                            
                            <div class="field-container">
                                <h4>2</h4>
                            </div>
                            
                            <div class="field-container">
                                <h4>
                                    Beneficiario: <strong id="txtProvider2" runat="server"></strong>
                                </h4>
                            </div>

                            <div class="field-container">
                                <h4>
                                    Importe: <strong id="TxtAmount2" runat="server"></strong>
                                </h4>
                            </div>

                        </div>

                        <%--CCC--%>
                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">CCC</span>
                            </div>

                            <div class="field-container">
                                <span>Entidad:</span>
                                <telerik:RadTextBox ID="TxtCcEntity2"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Sucursal:</span>
                                <telerik:RadTextBox ID="TxtCcBranch2"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>DC:</span>
                                <telerik:RadTextBox ID="TxtCcDc2"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="2"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Cuenta:</span>
                                <telerik:RadTextBox ID="TxtCcAccount2"
                                    Width="120px"
                                    runat="server"
                                    MaxLength="10"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">IBAN:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtIban2"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="36">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Concepto:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtConcept2"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="75">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Entidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtEntity2"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Domicilio:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtAddress2"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Localidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtLocation2"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                    </div>                    

                    <%--TRANSFER 3--%>
                    <div id="transfer3" class="form-group form-group-fake transfer">

                        <%--Header--%>
                        <div class="form-group form-group-horizontal">
                            
                            <div class="field-container">
                                <h4>3</h4>
                            </div>
                            
                            <div class="field-container">
                                <h4>
                                    Beneficiario: <strong id="txtProvider3" runat="server"></strong>
                                </h4>
                            </div>

                            <div class="field-container">
                                <h4>
                                    Importe: <strong id="TxtAmount3" runat="server"></strong>
                                </h4>
                            </div>

                        </div>

                        <%--CCC--%>
                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">CCC</span>
                            </div>

                            <div class="field-container">
                                <span>Entidad:</span>
                                <telerik:RadTextBox ID="TxtCcEntity3"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Sucursal:</span>
                                <telerik:RadTextBox ID="TxtCcBranch3"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>DC:</span>
                                <telerik:RadTextBox ID="TxtCcDc3"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="2"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Cuenta:</span>
                                <telerik:RadTextBox ID="TxtCcAccount3"
                                    Width="120px"
                                    runat="server"
                                    MaxLength="10"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">IBAN:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtIban3"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="36">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Concepto:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtConcept3"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="75">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Entidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtEntity3"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Domicilio:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtAddress3"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Localidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtLocation3"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                    </div>                    

                    <%--TRANSFER 4--%>
                    <div id="transfer4" class="form-group form-group-fake transfer">

                        <%--Header--%>
                        <div class="form-group form-group-horizontal">
                            
                            <div class="field-container">
                                <h4>4</h4>
                            </div>
                            
                            <div class="field-container">
                                <h4>
                                    Beneficiario: <strong id="txtProvider4" runat="server"></strong>
                                </h4>
                            </div>

                            <div class="field-container">
                                <h4>
                                    Importe: <strong id="TxtAmount4" runat="server"></strong>
                                </h4>
                            </div>

                        </div>

                        <%--CCC--%>
                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">CCC</span>
                            </div>

                            <div class="field-container">
                                <span>Entidad:</span>
                                <telerik:RadTextBox ID="TxtCcEntity4"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Sucursal:</span>
                                <telerik:RadTextBox ID="TxtCcBranch4"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>DC:</span>
                                <telerik:RadTextBox ID="TxtCcDc4"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="2"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Cuenta:</span>
                                <telerik:RadTextBox ID="TxtCcAccount4"
                                    Width="120px"
                                    runat="server"
                                    MaxLength="10"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">IBAN:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtIban4"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="36">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Concepto:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtConcept4"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="75">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Entidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtEntity4"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Domicilio:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtAddress4"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Localidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtLocation4"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                    </div>                    

                    <%--TRANSFER 5--%>
                    <div id="transfer5" class="form-group form-group-fake transfer">

                        <%--Header--%>
                        <div class="form-group form-group-horizontal">
                            
                            <div class="field-container">
                                <h4>5</h4>
                            </div>
                            
                            <div class="field-container">
                                <h4>
                                    Beneficiario: <strong id="txtProvider5" runat="server"></strong>
                                </h4>
                            </div>

                            <div class="field-container">
                                <h4>
                                    Importe: <strong id="TxtAmount5" runat="server"></strong>
                                </h4>
                            </div>

                        </div>

                        <%--CCC--%>
                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">CCC</span>
                            </div>

                            <div class="field-container">
                                <span>Entidad:</span>
                                <telerik:RadTextBox ID="TxtCcEntity5"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Sucursal:</span>
                                <telerik:RadTextBox ID="TxtCcBranch5"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="4"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>DC:</span>
                                <telerik:RadTextBox ID="TxtCcDc5"
                                    Width="50px"
                                    runat="server"
                                    MaxLength="2"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span>Cuenta:</span>
                                <telerik:RadTextBox ID="TxtCcAccount5"
                                    Width="120px"
                                    runat="server"
                                    MaxLength="10"
                                    Text="">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">IBAN:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtIban5"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="36">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Concepto:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtConcept5"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="75">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Entidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtEntity5"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                            <div class="field-container">
                                <span class="span-alone">Domicilio:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtAddress5"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>

                        </div>

                        <div class="form-group form-group-fake form-group-horizontal">

                            <div class="field-container">
                                <span class="span-alone">Localidad:</span>
                            </div>

                            <div class="field-container">
                                <telerik:RadTextBox ID="TxtLocation5"
                                    runat="server"
                                    Width="100%"
                                    Text=""
                                    MaxLength="100">
                                </telerik:RadTextBox>
                            </div>
                        </div>

                    </div>                    

                </div>
            </telerik:RadAjaxPanel>
        </div>
    </div>

    <telerik:RadScriptBlock runat="server">
        <script>
            var modalDiv = null;

            function showModalDiv(sender, args) {
                if (!modalDiv) {
                    modalDiv = document.createElement("div");
                    modalDiv.style.width = "100%";
                    modalDiv.style.height = "100%";
                    modalDiv.style.backgroundColor = "#aaaaaa";
                    modalDiv.style.position = "absolute";
                    modalDiv.style.left = "0px";
                    modalDiv.style.top = "0px";
                    modalDiv.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=50)";
                    modalDiv.style.opacity = ".5";
                    modalDiv.style.MozOpacity = ".5";
                    modalDiv.setAttribute("unselectable", "on");
                    modalDiv.style.zIndex = (sender.get_zIndex() - 1).toString();
                    document.body.appendChild(modalDiv);
                }
                modalDiv.style.display = "";
            }

            function hideModalDiv() {
                modalDiv.style.display = "none";
            }


            function hideDivs(count) {
                switch (count) {
                    case "0":
                        $('#transfer1').hide();
                        $('#transfer2').hide();
                        $('#transfer3').hide();
                        $('#transfer4').hide();
                        $('#transfer5').hide();
                        break;
                    case "1":
                        $('#transfer1').show();
                        $('#transfer2').hide();
                        $('#transfer3').hide();
                        $('#transfer4').hide();
                        $('#transfer5').hide();
                        break;
                    case "2":
                        $('#transfer1').show();
                        $('#transfer2').show();
                        $('#transfer3').hide();
                        $('#transfer4').hide();
                        $('#transfer5').hide();
                        break;
                    case "3":
                        $('#transfer1').show();
                        $('#transfer2').show();
                        $('#transfer3').show();
                        $('#transfer4').hide();
                        $('#transfer5').hide();
                        break;
                    case "4":
                        $('#transfer1').show();
                        $('#transfer2').show();
                        $('#transfer3').show();
                        $('#transfer4').show();
                        $('#transfer5').hide();
                        break;
                    case "5":
                        $('#transfer1').show();
                        $('#transfer2').show();
                        $('#transfer3').show();
                        $('#transfer4').show();
                        $('#transfer5').show();
                        break;
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
