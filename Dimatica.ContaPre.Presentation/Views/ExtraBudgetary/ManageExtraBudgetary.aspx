<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageExtraBudgetary.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.ExtraBudgetary.ManageExtraBudgetary"
    ValidateRequest="false" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmManageExtraBudgetary" runat="server">
        <Windows>
            <%--estado expediente--%>
            <telerik:RadWindow ID="rwSeeStatusApplication"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Estado Aplicación"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="400"
                Height="225"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeeStatusApplicationCloseHandler">
            </telerik:RadWindow>

            <%--Apuntes de Tesorería--%>
            <telerik:RadWindow ID="rwSeeTreasuryNotes"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Apuntes de Tesorería"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="800"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeeTreasuryNotesCloseHandler">
            </telerik:RadWindow>

            <%--Descuentos extrapresupuestarias--%>
            <telerik:RadWindow ID="rwUpdateDiscounts"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Descuentos extrapresupuestarias"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="600"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientUpdateDiscountsCloseHandler">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_extraBudgetary" class="container" style="height: calc(100vh - 56px) !important">

        <h3 id="titleHeader" runat="server">Nuevo Exped. Extrapresupuestario</h3>

        <div id="page_manageExtraBudgetary" class="box-block">
            <telerik:RadAjaxPanel ID="rapManageExtraBudgetary"
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
                    <div class="form-group form-group-fake">

                        <%--Tipo--%>
                        <div class="field-container">
                            <span>Tipo:</span>
                            <telerik:RadComboBox ID="RcTypes"
                                runat="server"
                                Width="130px"
                                AutoPostBack="True"
                                DataTextField="TIP_EXTRAP_DESCRIPCION"
                                DataValueField="TIP_EXTRAP_CODIGO_AUX"
                                OnSelectedIndexChanged="RcTypes_OnSelectedIndexChanged">
                            </telerik:RadComboBox>
                        </div>

                        <%--Año--%>
                        <div class="field-container">
                            <span>Año:</span>
                            <telerik:RadMonthYearPicker ID="RmyYear"
                                runat="server"
                                AutoPostBack="True"
                                EnableTyping="False"
                                Culture="es-ES"
                                DateInput-Culture-="es-ES"
                                MonthCellsStyle-CssClass="monthCellClass"
                                Width="100px"
                                OnSelectedDateChanged="RmyYear_OnSelectedDateChanged">
                                <MonthYearNavigationSettings TodayButtonCaption="Actual"
                                    OkButtonCaption="Aceptar"
                                    CancelButtonCaption="Cancelar" />
                                <DateInput runat="server"
                                    DateFormat="yyyy"
                                    DisplayDateFormat="yyyy">
                                </DateInput>
                            </telerik:RadMonthYearPicker>
                        </div>

                        <%--N⁰ Exped.--%>
                        <div class="field-container">
                            <span>N⁰ Exped.:</span>
                            <telerik:RadNumericTextBox ID="RntFileNumber"
                                runat="server"
                                RenderMode="Lightweight"
                                Width="75px"
                                MinValue="0"
                                MaxValue="9999"
                                MaxLength="4"
                                ShowSpinButtons="False"
                                NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>
                        </div>

                        <%--Fecha--%>
                        <div class="field-container">
                            <span>Fecha:</span>
                            <telerik:RadDatePicker ID="RdDate"
                                RenderMode="Lightweight"
                                runat="server"
                                Width="150px"
                                AutoPostBack="False"
                                Culture="es-ES"
                                EnableTyping="True"
                                MaxDate="12/31/9999"
                                MinDate="01/01/1800" />
                            <asp:RequiredFieldValidator ID="rfvDate"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="ExtraBudgetaryGroup"
                                ControlToValidate="RdDate"
                                ErrorMessage=" * "
                                ToolTip="Introduzca la fecha del Expediente."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </div>

                        <%--Aplic. Extrapresup.--%>
                        <div class="field-container">
                            <span>Aplic. Extrapresup.:</span>
                            <telerik:RadComboBox ID="RcExtraBudgetaryApplications"
                                runat="server"
                                Width="300px"
                                AutoPostBack="True"
                                DataTextField="DisplayLabel"
                                DataValueField="EXTRAPRE_CODIGO">
                            </telerik:RadComboBox>
                        </div>

                        <%--Importe--%>
                        <div class="field-container">
                            <span>Importe:</span>
                            <telerik:RadNumericTextBox ID="RntAmount"
                                runat="server"
                                RenderMode="Lightweight"
                                Width="150px"
                                MinValue="0"
                                ShowSpinButtons="False"
                                Culture="es-ES">
                            </telerik:RadNumericTextBox>
                        </div>

                        <%--Ordinal Pagador--%>
                        <div class="field-container">
                            <span>Ordinal Pagador:</span>
                            <telerik:RadComboBox ID="RntRestrictedAccount"
                                runat="server"
                                Width="100%"
                                DataValueField="CUE_CODIGO"
                                DataTextField="DisplayLabel">
                            </telerik:RadComboBox>
                        </div>

                        <%--Enlazado--%>
                        <div class="field-container">
                            <span>Enlazado:</span>
                            <telerik:RadCheckBox ID="RcbBinding"
                                runat="server"
                                Checked="False"
                                Text=""
                                AutoPostBack="false">
                            </telerik:RadCheckBox>
                        </div>

                        <%--Cuenta PGCP--%>
                        <div class="field-container">
                            <span>Cuenta PGCP:</span>
                            <telerik:RadTextBox ID="RtbPgcpAccount"
                                runat="server"
                                Width="100px"
                                Text=""
                                MaxLength="5">
                            </telerik:RadTextBox>
                        </div>

                        <%--Procedencia--%>
                        <div id="divProvenance" class="field-container">
                            <span>Procedencia:</span>
                            <telerik:RadNumericTextBox ID="RntProvenance"
                                runat="server"
                                RenderMode="Lightweight"
                                Width="75px"
                                MinValue="0"
                                MaxValue="9999"
                                MaxLength="4"
                                ShowSpinButtons="False"
                                NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>
                        </div>

                        <%--Señalamiento--%>
                        <div id="divSing" class="field-container">
                            <span>Señalamiento:</span>
                            <telerik:RadTextBox ID="RtbSing"
                                runat="server"
                                Width="75px"
                                Text=""
                                Enabled="False"
                                MaxLength="20">
                            </telerik:RadTextBox>
                        </div>

                        <%--Descripcion--%>
                        <div class="field-container">
                            <span>Descripción:</span>
                            <telerik:RadTextBox ID="RtbDescription"
                                runat="server"
                                Width="100%"
                                Text=""
                                MaxLength="255">
                            </telerik:RadTextBox>
                        </div>
                    </div>

                    <%--BUTTONS--%>
                    <div class="form-group buttons">
                        <telerik:RadButton ButtonType="LinkButton" ID="btnStatusApplication"
                            runat="server"
                            RenderMode="Native"
                            Text="Estado Aplicación"
                            AutoPostBack="True"
                            OnClick="btnStatusApplication_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnNewInterested"
                            runat="server"
                            RenderMode="Native"
                            Text="Nuevo Interesado"
                            AutoPostBack="True"
                            OnClick="btnNewProvider_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnNewThirds"
                            runat="server"
                            RenderMode="Native"
                            Text="Nuevo Tercero"
                            AutoPostBack="True"
                            OnClick="btnNewProvider_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnExtraBudgetaryDiscounts"
                            runat="server"
                            RenderMode="Native"
                            Text="Dtos. Extrapresupuep."
                            AutoPostBack="True"
                            OnClick="btnExtraBudgetaryDiscounts_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnShowTreasury"
                            runat="server"
                            RenderMode="Native"
                            Text="Ver A. Tesorería"
                            AutoPostBack="True"
                            OnClick="btnShowTreasury_OnClick">
                        </telerik:RadButton>
                    </div>

                    <div class="form-group form-group-fake">
                        <div style="width: calc(50% - 1.25rem); margin: .625rem .625rem 0;">
                            <strong>Datos Necesarios para P.M.P.:</strong>
                            <%--Interesado--%>
                            <div class="field-container">
                                <span>Interesado:</span>
                                <telerik:RadComboBox ID="RcInterested"
                                    runat="server"
                                    Width="100%"
                                    AutoPostBack="False"
                                    DataTextField="PROV_NOMBRE"
                                    DataValueField="PROV_CODIGO">
                                </telerik:RadComboBox>
                            </div>

                            <%--Forma de Pago--%>
                            <div class="field-container">
                                <span>Forma de Pago:</span>
                                <telerik:RadComboBox ID="RcPayForms"
                                    runat="server"
                                    Width="100%"
                                    AutoPostBack="False"
                                    DataTextField="DisplayLabel"
                                    DataValueField="FOR_CODIGO_AUX">
                                </telerik:RadComboBox>
                            </div>

                            <%--Ordinal Percertor--%>
                            <div class="field-container">
                                <span>Ordinal Percertor:</span>
                                <telerik:RadComboBox ID="RcPercertor"
                                    runat="server"
                                    Width="100%"
                                    DataValueField="CUE_CODIGO"
                                    DataTextField="DisplayLabel">
                                </telerik:RadComboBox>
                            </div>

                            <%--Tipo de Pago--%>
                            <div class="field-container">
                                <span>Tipo de Pago:</span>
                                <telerik:RadComboBox ID="RcPayTypes"
                                    runat="server"
                                    Width="100%"
                                    DataValueField="TIPP_CODIGO_AUX"
                                    DataTextField="DisplayLabel">
                                </telerik:RadComboBox>
                            </div>

                            <%--N⁰ Cheque--%>
                            <div class="field-container">
                                <span>N⁰ Cheque:</span>
                                <telerik:RadTextBox ID="RtbCheckNumber"
                                    runat="server"
                                    Width="150px"
                                    Text=""
                                    MaxLength="60">
                                </telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="width: calc(50% - 1.25rem); margin: .625rem .625rem 0;">
                            <strong>Datos Necesarios para M.I. y rectificaciones:</strong>
                            <%--Tercero--%>
                            <div class="field-container">
                                <span>Tercero:</span>
                                <telerik:RadComboBox ID="RcThirds"
                                    runat="server"
                                    Width="100%"
                                    AutoPostBack="False"
                                    DataTextField="PROV_NOMBRE"
                                    DataValueField="PROV_CODIGO">
                                </telerik:RadComboBox>
                            </div>

                            <%--N⁰ Hoja Arqueo--%>
                            <div class="field-container">
                                <span>N⁰ Hoja Arqueo:</span>
                                <telerik:RadNumericTextBox ID="RntTonnageSheet"
                                    runat="server"
                                    RenderMode="Lightweight"
                                    MinValue="0"
                                    ShowSpinButtons="False"
                                    NumberFormat-DecimalDigits="0"
                                    Width="150px">
                                </telerik:RadNumericTextBox>
                            </div>

                            <%--Año Hoja Arqueo--%>
                            <div class="field-container">
                                <span>Año Hoja Arqueo:</span>
                                <telerik:RadMonthYearPicker ID="RmyTonnageSheetYear"
                                    runat="server"
                                    AutoPostBack="False"
                                    EnableTyping="False"
                                    Culture="es-ES"
                                    DateInput-Culture-="es-ES"
                                    MonthCellsStyle-CssClass="monthCellClass"
                                    Width="150px">
                                    <MonthYearNavigationSettings TodayButtonCaption="Actual"
                                        OkButtonCaption="Aceptar"
                                        CancelButtonCaption="Cancelar" />
                                    <DateInput runat="server"
                                        DateFormat="yyyy"
                                        DisplayDateFormat="yyyy">
                                    </DateInput>
                                </telerik:RadMonthYearPicker>
                            </div>

                            <%--N⁰ Hoja Arqueo 50--%>
                            <div class="field-container">
                                <span>N⁰ Hoja Arqueo 50:</span>
                                <telerik:RadNumericTextBox ID="RntTonnageSheet50"
                                    runat="server"
                                    RenderMode="Lightweight"
                                    MinValue="0"
                                    ShowSpinButtons="False"
                                    NumberFormat-DecimalDigits="0"
                                    Width="150px">
                                </telerik:RadNumericTextBox>
                            </div>

                            <%--Año Hoja Arqueo 50--%>
                            <div class="field-container">
                                <span>Año Hoja Arqueo 50:</span>
                                <telerik:RadMonthYearPicker ID="RmyTonnageSheet50Year"
                                    runat="server"
                                    AutoPostBack="False"
                                    EnableTyping="False"
                                    Culture="es-ES"
                                    DateInput-Culture-="es-ES"
                                    MonthCellsStyle-CssClass="monthCellClass"
                                    Width="150px">
                                    <MonthYearNavigationSettings TodayButtonCaption="Actual"
                                        OkButtonCaption="Aceptar"
                                        CancelButtonCaption="Cancelar" />
                                    <DateInput runat="server"
                                        DateFormat="yyyy"
                                        DisplayDateFormat="yyyy">
                                    </DateInput>
                                </telerik:RadMonthYearPicker>
                            </div>
                        </div>
                    </div>

                    <%--BUTTONS--%>
                    <div class="form-group buttons">
                        <telerik:RadButton ButtonType="LinkButton" ID="btnNew"
                            runat="server"
                            RenderMode="Native"
                            Text="Nuevo Exped."
                            AutoPostBack="True"
                            OnClick="btnNew_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                            runat="server"
                            RenderMode="Native"
                            Text="Grabar"
                            AutoPostBack="True"
                            OnClick="btnSave_OnClick"
                            ValidationGroup="ExtraBudgetaryGroup">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                            runat="server"
                            RenderMode="Native"
                            Text="Eliminar"
                            AutoPostBack="True"
                            OnClick="btnDelete_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnReport"
                            runat="server"
                            RenderMode="Native"
                            Text="Imprimir"
                            AutoPostBack="True"
                            OnClick="btnReport_OnClick">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                            runat="server"
                            RenderMode="Native"
                            Text="Volver"
                            AutoPostBack="True"
                            OnClick="btnBack_OnClick">
                        </telerik:RadButton>
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

            function hideSing() {
                $('#<%=this.btnExtraBudgetaryDiscounts.ClientID %>').hide();
                $('#<%=this.btnShowTreasury.ClientID %>').hide();
                $('#<%=this.btnDelete.ClientID %>').hide();
                $('#<%=this.btnReport.ClientID %>').hide();
                $('#divProvenance').hide();
                $('#divSing').hide();
            }

            function btnDeleteOnClientClicked(sender, eventArgs) {
                radconfirm("¿ Está seguro que desea eliminar el expediente extrapresupuestario en cuestión ?<br/>Se eliminarán todos los datos asociados a dicho expediente:<br/>- Apuntes de tesorería.<br/>- Hojas de arqueo.",
                    confirmDeleteCallBackFn,
                    330,
                    140,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "ManageExtraBudgetary.aspx/DeleteExtraBudgetary",
                    data: null,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("Ha ocurrido un error eliminando el Expediente Extrapresupuestario en cuestión.", 330, 140, "Imposible eliminar expediente extrapresupuestario", null, null);
                                break;
                            case 1:
                                var url = window.location.origin + '\\Views\\ExtraBudgetary\\ExtraBudgetaries.aspx';
                                window.location.href = url;
                                break;
                            case 2:
                                radalert("No se puede eliminar el Expediente Extrapresupuestario en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar expediente extrapresupuestario", null, null);
                                break;
                            case 3:
                                radalert("No se puede eliminar el Expediente Extrapresupuestario en cuestión debido a que no se ha creado aún en la BD.", 330, 140, "Imposible eliminar expediente extrapresupuestario", null, null);
                                break;
                            case 4:
                                radalert("Ha ocurrido un error eliminando el Descuento del Expediente Extrapresupuestario en cuestión.", 330, 140, "Imposible eliminar expediente extrapresupuestario", null, null);
                                break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar el Expediente Extrapresupuestario en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar expediente extrapresupuestario", null, null);
                    }
                });

                //PageMethods.DeleteAccountingRecord(OnDeleteAccountingRecordSuccess);
            }


            function seeStatusApplication(year, application, applicationText) {
                if (application == "-1") {
                    radalert("No se puede ver el estado de la aplicación porque no hay aplicación seleccionada.", 330, 140, "Imposible ver estado", null, null);
                    return;
                }

                var url = "SeeStatusApplication.aspx?year=" + year + "&application=" + application + "&applicationText=" + applicationText;
                var manager = $find("<%= this.rwmManageExtraBudgetary.ClientID %>");
                var oWnd = manager.open(url, "rwSeeStatusApplication");
            }

            function OnClientSeeStatusApplicationCloseHandler(sender, args) {
                location.reload(true);
            }

            function seeTreasuryNotes(extraBudgetaryId, treasuryId) {
                if (treasuryId == "-1") {
                    radalert("No se pueden ver los apuntes de tesorería porque no el Expediente en cuestión no está enlazado.", 330, 140, "Imposible ver apuntes", null, null);
                    return;
                }

                var url = "SeeTreasuryNotes.aspx?extraBudgetaryId=" + extraBudgetaryId + "&treasuryId=" + treasuryId;
                var manager = $find("<%= this.rwmManageExtraBudgetary.ClientID %>");
                var oWnd = manager.open(url, "rwSeeTreasuryNotes");
            }

            function OnClientSeeTreasuryNotesCloseHandler(sender, args) {
                location.reload(true);
            }

            function updateDiscounts(id) {
                var url = "UpdateDiscounts.aspx?id=" + id;
                var manager = $find("<%= this.rwmManageExtraBudgetary.ClientID %>");
                var oWnd = manager.open(url, "rwUpdateDiscounts");
            }

            function OnClientUpdateDiscountsCloseHandler(sender, args) {
                location.reload(true);
            }

            function showReport(extraBudgetaryId, type) {
                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=extraBudgetaryCurrent&extraBudgetary=' + extraBudgetaryId + '&type=' + type;

                window.open(url, "Reporte");
            }

            var radLoadingPanel = null;
            var currentUpdatedControl = null;

            $(document)
                .ajaxStart(function () {
                    radLoadingPanel = $find("ralPrincipal");
                    currentUpdatedControl = "section_extraBudgetary";
                    radLoadingPanel.show(currentUpdatedControl);
                })
                .ajaxStop(function () {
                    if (radLoadingPanel != null) {
                        radLoadingPanel.hide(currentUpdatedControl);
                    }
                    radLoadingPanel = null;
                    currentUpdatedControl = null;
                });
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
