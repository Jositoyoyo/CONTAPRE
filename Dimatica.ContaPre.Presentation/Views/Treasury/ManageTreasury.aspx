<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageTreasury.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Treasury.ManageTreasury" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmManageTreasury" runat="server">
        <Windows>

            <%--Documentos Enlazados--%>
            <telerik:RadWindow ID="rwSeeBoundDocuments"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Documentos Enlazados"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="700"
                Height="500"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeeBoundDocumentsCloseHandler">
            </telerik:RadWindow>

            <%--Documentos a Enlazar --%>
            <telerik:RadWindow ID="rwBoundDocuments"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Documentos a Enlazar"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="800"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientBoundDocumentsCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_treasury" class="container" style="height: calc(100vh - 56px) !important">

        <h3 id="titleHeader" runat="server">Nuevo Apunte de Tesorería</h3>

        <%--MANAGE--%>
        <div class="manage">
            <telerik:RadAjaxPanel ID="rapManageTreasury"
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

                <div class="form-group form-group-fake">
                    <%--Año--%>
                    <div class="field-container">
                        <span>Año:</span>
                        <telerik:RadMonthYearPicker ID="RmyYear"
                            runat="server"
                            AutoPostBack="False"
                            EnableTyping="False"
                            Culture="es-ES"
                            DateInput-Culture-="es-ES"
                            MonthCellsStyle-CssClass="monthCellClass"
                            Width="80px">
                            <MonthYearNavigationSettings TodayButtonCaption="Actual"
                                OkButtonCaption="Aceptar"
                                CancelButtonCaption="Cancelar" />
                            <DateInput runat="server"
                                DateFormat="yyyy"
                                DisplayDateFormat="yyyy">
                            </DateInput>
                        </telerik:RadMonthYearPicker>
                        <asp:RequiredFieldValidator ID="rfvYear"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="TreasuryGroup"
                            ControlToValidate="rmyYear"
                            ErrorMessage=" * "
                            ToolTip="Introduzca el Año del Apunte."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>

                    <%--Tipo Registro--%>
                    <div class="field-container">
                        <span>Tipo Registro:</span>
                        <telerik:RadComboBox ID="RcRegisterTypeCode"
                            runat="server"
                            Width="150px"
                            DataValueField="TIPR_CODIGO"
                            DataTextField="DisplayLabel">
                        </telerik:RadComboBox>
                    </div>

                    <%--Fecha Registro--%>
                    <div class="field-container">
                        <span>Fecha Registro:</span>
                        <telerik:RadDatePicker ID="RdpEntryDate"
                            RenderMode="Lightweight"
                            runat="server"
                            Width="130px"
                            AutoPostBack="False"
                            Culture="es-ES"
                            EnableTyping="True"
                            MaxDate="12/31/9999"
                            MinDate="01/01/1800" />
                        <asp:RequiredFieldValidator ID="rfvEntryDate"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="TreasuryGroup"
                            ControlToValidate="RdpEntryDate"
                            ErrorMessage=" * "
                            ToolTip="Introduzca la fecha de registro del Apunte."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>

                    <%--Fecha Banco--%>
                    <div class="field-container">
                        <span>Fecha Banco: </span>
                        <telerik:RadDatePicker ID="RdpBankDate"
                            RenderMode="Lightweight"
                            runat="server"
                            Width="130px"
                            AutoPostBack="False"
                            Culture="es-ES"
                            EnableTyping="True"
                            MaxDate="12/31/9999"
                            MinDate="01/01/1800" />
                    </div>
                </div>

                <div class="form-group form-group-fake">
                    <%--Aplicacion--%>
                    <div class="field-container">
                        <span>Aplicación:</span>
                        <telerik:RadTextBox ID="RtbApplication"
                            runat="server"
                            Width="100px"
                            Text=""
                            MaxLength="50">
                        </telerik:RadTextBox>
                    </div>

                    <%--Tipo de Pago--%>
                    <div class="field-container">
                        <span>Tipo de Pago:</span>
                        <telerik:RadComboBox ID="RcPayType"
                            runat="server"
                            Width="130px">
                            <Items>
                                <telerik:RadComboBoxItem Text="< Seleccione >"
                                    Value="-1" />
                                <telerik:RadComboBoxItem Text="Talón"
                                    Value="1" />
                                <telerik:RadComboBoxItem Text="Transferencia"
                                    Value="3" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>

                    <%--N⁰ Talón o transferencia--%>
                    <div class="field-container">
                        <span>N⁰ Talón o transferencia:</span>
                        <telerik:RadTextBox ID="RtbCheckNumber"
                            runat="server"
                            Width="100px"
                            Text=""
                            MaxLength="30">
                        </telerik:RadTextBox>
                    </div>

                    <%--Procedencia del Movimiento--%>
                    <div class="field-container">
                        <span>Procedencia del Movimiento: </span>
                        <telerik:RadComboBox ID="RcOriginCode"
                            runat="server"
                            Width="130px"
                            DataValueField="ORI_CODIGO_AUX"
                            DataTextField="ORI_DESCRIPCION"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="RcOriginCode_OnSelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                </div>

                <div class="form-group form-group-fake">
                    <%--Cuenta Restringida--%>
                    <div class="field-container">
                        <span>Cuenta Restringida:</span>
                        <telerik:RadComboBox ID="RcRestrictedAccount"
                            runat="server"
                            Width="175px"
                            DataValueField="CUE_CODIGO"
                            DataTextField="DisplayDescriptionLabel">
                        </telerik:RadComboBox>
                    </div>

                    <%--Importe--%>
                    <div class="field-container">
                        <span>Importe:</span>
                        <telerik:RadNumericTextBox ID="RntTreasuryAmount"
                            runat="server"
                            RenderMode="Lightweight"
                            Width="100px"
                            Value="0"
                            MinValue="0"
                            ShowSpinButtons="False"
                            NumberFormat-DecimalDigits="2">
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="rfvTreasuryAmount"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="TreasuryGroup"
                            ControlToValidate="RntTreasuryAmount"
                            ErrorMessage=" * "
                            ToolTip="Introduzca el importe del Apunte."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>

                    <%--Signo--%>
                    <div class="field-container">
                        <span>Signo:</span>
                        <telerik:RadComboBox ID="RcTreasuryHave"
                            runat="server"
                            Width="100px">
                            <Items>
                                <telerik:RadComboBoxItem Text="+ Debe"
                                    Value="0" />
                                <telerik:RadComboBoxItem Text="- Haber"
                                    Value="1" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>

                    <%--Anulado--%>
                    <div class="field-container">
                        <span>Anulado:</span>
                        <telerik:RadCheckBox ID="checkCanceled"
                            runat="server"
                            Checked="False"
                            Text=""
                            AutoPostBack="false">
                        </telerik:RadCheckBox>
                    </div>

                    <%--Terminado--%>
                    <div class="field-container">
                        <span>Terminado:</span>
                        <telerik:RadCheckBox ID="checkFinish"
                            runat="server"
                            Checked="False"
                            Text=""
                            AutoPostBack="false">
                        </telerik:RadCheckBox>
                    </div>
                </div>


                <div class="form-group form-group-fake">
                    <%--Descripcion--%>
                    <div class="field-container">
                        <span>Descripción:</span>
                        <telerik:RadTextBox ID="RtbDescription"
                            runat="server"
                            Width="550px"
                            Text=""
                            MaxLength="255">
                        </telerik:RadTextBox>
                    </div>
                </div>

                <%--BUTTONS--%>
                <div class="form-group buttons">
                    <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                        runat="server"
                        RenderMode="Native"
                        Text="Grabar"
                        AutoPostBack="True"
                        OnClick="btnSave_OnClick"
                        ValidationGroup="TreasuryGroup">
                    </telerik:RadButton>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnSeeDocuments"
                        runat="server"
                        RenderMode="Native"
                        Text="Ver Documentos"
                        AutoPostBack="True"
                        OnClick="btnSeeDocuments_OnClick">
                    </telerik:RadButton>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnDeleteBankDate"
                        runat="server"
                        RenderMode="Native"
                        Text="Borrar Fecha Banco"
                        AutoPostBack="True"
                        OnClick="btnDeleteBankDate_OnClick">
                    </telerik:RadButton>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnBound"
                        runat="server"
                        RenderMode="Native"
                        Text="Enlazar Tesorería"
                        AutoPostBack="True"
                        OnClick="btnBound_OnClick">
                    </telerik:RadButton>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                        runat="server"
                        RenderMode="Native"
                        Text="Eliminar Apunte"
                        AutoPostBack="False"
                        OnClientClicked="btnDeleteOnClientClicked">
                    </telerik:RadButton>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnNew"
                        runat="server"
                        RenderMode="Native"
                        Text="Nuevo Apunte"
                        AutoPostBack="True"
                        OnClick="btnNew_OnClick">
                    </telerik:RadButton>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                        runat="server"
                        RenderMode="Native"
                        Text="Volver"
                        AutoPostBack="True"
                        OnClick="btnBack_OnClick">
                    </telerik:RadButton>
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

            function seeBoundDocuments(treasuryId) {
                var url = "SeeBoundDocuments.aspx?treasuryId=" + treasuryId;
                var manager = $find("<%= this.rwmManageTreasury.ClientID %>");
                var oWnd = manager.open(url, "rwSeeBoundDocuments");
            }

            function OnClientSeeBoundDocumentsCloseHandler(sender, args) {
                location.reload(true);
            }

            function boundDocuments(treasuryId, budgetYear, date) {
                var url = "BoundDocuments.aspx?treasuryId=" + treasuryId + "&year=" + budgetYear + "&date=" + date;
                var manager = $find("<%= this.rwmManageTreasury.ClientID %>");
                var oWnd = manager.open(url, "rwBoundDocuments");
            }

            function OnClientBoundDocumentsCloseHandler(sender, args) {
                location.reload(true);
            }

            function btnDeleteOnClientClicked(sender, eventArgs) {
                radconfirm("¿ Desea realmente eliminar el apunte de tesorería ?<br/>Se eliminaran también los enlaces con documentos, en caso de tenerlos.",
                    confirmDeleteCallBackFn,
                    330,
                    140,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmDeleteCallBackFn(arg)
            {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "ManageTreasury.aspx/DeleteTreasury",
                    data: JSON.stringify({}),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                        case 0:
                            radalert("Ha ocurrido un error eliminando el Apunte de Tesorería en cuestión.", 330, 140, "Imposible eliminar apunte de tesorería", null, null);
                            break;
                        case 1:
                                var url = window.location.origin + '/Views/Treasury/NotesTreasuries.aspx';
                            window.location.href = url;
                            break;
                        case 2:
                                radalert("No se puede eliminar el Apunte de Tesorería en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar apunte de tesorería", null, null);
                            break;
                        case 3:
                                radalert("No se puede eliminar el apunte porque ha sido enlazado con un Derecho Reconocido en la aplicación de Convenios.<br/>Informe a Convenios para que elimine el enlace, y vuelva a intentarlo.", 330, 140, "Imposible eliminar apunte de tesorería", null, null);
                            break;
                        case 4:
                                radalert("No se ha podido eliminar el ingreso en la aplicación de Convenios. Para poder eliminarlo en Contabilidad es necesario eliminarlo también en Convenios.<br/>Informe a Convenios para que elimine el apunte, y vuelva a intentarlo.", 330, 140, "Imposible eliminar apunte de tesorería", null, null);
                            break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar el Apunte de Tesorería en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar apunte de tesorería", null, null);
                    }
                });
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
