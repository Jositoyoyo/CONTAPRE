<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageAdministrativeRecord.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.ManageAdministrativeRecord" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <telerik:RadWindowManager ID="rwmManageAdministrativeRecord" runat="server">
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_spend" class="container" style="height: calc(100vh - 60px) !important">

        <h3 id="titleHeader" runat="server">Nuevo Expediente Administrativo</h3>

        <%--MANAGE--%>
        <div class="manage">

            <div class="form-group form-group-fake">
                <%--N⁰--%>
                <div class="field-container">
                    <span>N⁰ Exped. Administrativo:</span>
                    <telerik:RadNumericTextBox ID="txtOrder"
                        runat="server"
                        Width="60px"
                        RenderMode="Lightweight"
                        MinValue="0"
                        ShowSpinButtons="False"
                        NumberFormat-DecimalDigits="0"
                        Enabled="False">
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="rfvOrder"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="AdministrativeRecordGroup"
                        ControlToValidate="txtOrder"
                        ErrorMessage=" * "
                        ToolTip="Introduzca el N⁰ de Expediente Administrativo."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>

                <%--Año--%>
                <div class="field-container">
                    <span>Año Ejercicio:</span>
                    <telerik:RadMonthYearPicker ID="rmyYear"
                        runat="server"
                        Width="100px"
                        AutoPostBack="True"
                        EnableTyping="False"
                        Culture="es-ES"
                        DateInput-Culture-="es-ES"
                        MonthCellsStyle-CssClass="monthCellClass"
                        OnSelectedDateChanged="rmyYear_OnSelectedDateChanged">
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
                        ValidationGroup="AdministrativeRecordGroup"
                        ControlToValidate="rmyYear"
                        ErrorMessage=" * "
                        ToolTip="Introduzca el Año de la Modificación de Crédito."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>

                <%--Procedencia--%>
                <div class="field-container">
                    <span>Procedencia:</span>
                    <telerik:RadComboBox ID="rcProvenances"
                        runat="server"
                        Width="250px"
                        AutoPostBack="True"
                        DataTextField="PROC_DESCRIPCION"
                        DataValueField="PROC_CODIGO"
                        OnSelectedIndexChanged="rcProvenances_OnSelectedIndexChanged">
                    </telerik:RadComboBox>
                </div>

                <%--Tipo Contrato--%>
                <div class="field-container">
                    <span>Tipo de Contrato:</span>
                    <telerik:RadComboBox ID="rcType"
                        runat="server"
                        Width="250px"
                        AutoPostBack="False"
                        DataTextField="TIPC_DESCRIPCION"
                        DataValueField="TIPC_CODIGO_AUX">
                    </telerik:RadComboBox>
                </div>

                <%--Descripcion--%>
                <div class="field-container">
                    <span>Descripción:</span>
                    <telerik:RadTextBox ID="txtDescription"
                        Width="100%"
                        runat="server"
                        MaxLength="255"
                        Text="">
                    </telerik:RadTextBox>
                </div>
            </div>

            <%--BUTTONS--%>
            <div class="form-group buttons">
                <div class="save">
                    <span class="icon"></span>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                        runat="server"
                        RenderMode="Native"
                        Text="Grabar"
                        AutoPostBack="True"
                        OnClick="btnSave_OnClick"
                        ValidationGroup="AdministrativeRecordGroup">
                    </telerik:RadButton>
                </div>

                <div class="delete">
                    <span class="icon"></span>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                        runat="server"
                        RenderMode="Native"
                        Text="Eliminar"
                        AutoPostBack="True"
                        OnClick="btnDelete_OnClick">
                    </telerik:RadButton>
                </div>

                <div class="back">
                    <span class="icon"></span>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                        runat="server"
                        RenderMode="Native"
                        Text="Volver"
                        AutoPostBack="True"
                        OnClick="btnBack_OnClick">
                    </telerik:RadButton>
                </div>
            </div>
        </div>

        <div id="page_manageAdministrativeRecord" class="box-block">

            <telerik:RadAjaxPanel ID="rapManageAdministrativeRecord" runat="server"
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

                <div class="field-document">
                    <div class="new-document">
                        <span>Expedientes Contables:</span>
                    </div>

                    <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgAdministrativeRecords"
                        runat="server"
                        AllowSorting="true"
                        Culture="es-ES"
                        GroupPanelPosition="Top"
                        OnNeedDataSource="RgAdministrativeRecords_OnNeedDataSource"
                        OnPreRender="RgAdministrativeRecords_OnPreRender"
                        OnItemDataBound="RgAdministrativeRecords_OnItemDataBound"
                        OnInsertCommand="RgAdministrativeRecords_OnInsertCommand"
                        OnItemCommand="RgAdministrativeRecords_OnItemCommand"
                        CssClass="manageAdministrativeRecords-table">

                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false"
                            AllowFilteringByColumn="true"
                            DataKeyNames="EXP_CODIGO"
                            CommandItemDisplay="Top"
                            AllowPaging="True"
                            PagerStyle-AlwaysVisible="True"
                            PageSize="100"
                            NoMasterRecordsText="No Hay datos a Mostrar."
                            TableLayout="Fixed"
                            EditMode="PopUp"
                            CssClass="popup-table normal-table">

                            <CommandItemSettings ShowRefreshButton="False"
                                ShowExportToExcelButton="False"
                                ShowExportToPdfButton="False"
                                ShowAddNewRecordButton="True"
                                AddNewRecordText="Nuevo" />

                            <PagerStyle Mode="NextPrevAndNumeric"
                                PageSizeLabelText="Elementos por pagina: "
                                PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                            <Columns>
                                <telerik:GridBoundColumn UniqueName="EXP_ANO_PRESUPUESTO"
                                    DataField="EXP_ANO_PRESUPUESTO"
                                    HeaderText="Año Presup."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="EXP_NUM_EXP_CONTABLE_ANUAL"
                                    DataField="EXP_NUM_EXP_CONTABLE_ANUAL"
                                    HeaderText="N⁰ Exped. Contable"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                                    DataField="PROV_NOMBRE"
                                    HeaderText="Interesado"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="EditColumn"
                                    ButtonType="LinkButton"
                                    HeaderTooltip="Editar expediente contable"
                                    CommandName="UpdateSpendRecord"
                                    HeaderStyle-Width="40px"
                                    ItemStyle-Width="40px"
                                    ItemStyle-CssClass="fas fa-pencil-alt"
                                    Text=" "
                                    HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridButtonColumn>
                            </Columns>

                            <EditFormSettings EditFormType="Template">
                                <FormTemplate>

                                    <h3>Expediente Contable</h3>

                                    <div class="form-group form-group-fake">
                                        <%--Año--%>
                                        <div class="field-container field-30">
                                            <span>Año Presupuesto:</span>
                                            <telerik:RadComboBox ID="rcYears"
                                                runat="server"
                                                Width="100%"
                                                AutoPostBack="False"
                                                DataTextField="Value"
                                                DataValueField="Value">
                                            </telerik:RadComboBox>
                                        </div>

                                        <%--Interesado--%>
                                        <div class="field-container field-70">
                                            <span>Interesado:</span>
                                            <telerik:RadComboBox ID="radDropProviders"
                                                runat="server"
                                                Width="100%"
                                                DataValueField="PROV_CODIGO"
                                                DataTextField="PROV_NOMBRE">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>

                                    <%--BUTTONS--%>
                                    <div class="buttons">

                                        <asp:LinkButton ID="btnIncomeUpdate"
                                            runat="server"
                                            ValidationGroup="IncomeApplicationGroup"
                                            CssClass="Button Add"
                                            CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                         <%# (Container is GridEditFormInsertItem) ? "Insertar   " : "Actualizar   " %>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnIncomeCancel"
                                            runat="server"
                                            CausesValidation="False"
                                            CommandName="Cancel"
                                            CssClass="Button Cancel">
                                                Cancelar
                                        </asp:LinkButton>

                                    </div>
                                </FormTemplate>
                            </EditFormSettings>
                        </MasterTableView>

                        <ClientSettings>
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
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

            function hideGrids() {
                $('#page_manageAdministrativeRecord').hide();
            }

            function moveNewButtons() {
                var $this = $("a[title*='Nuevo']");
                $this.each(function () {
                    var $destino = $(this).closest('.field-document').children(".new-document");
                    !$destino.has("a").length && $(this).appendTo($destino).show();
                });
            }

            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "ManageAdministrativeRecord.aspx/DeleteAdministrativeRecord",
                    data: null,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("Ha ocurrido un error eliminando el Expediente Administrativo en cuestión.", 330, 140, "Imposible eliminar", null, null);
                                break;
                            case 1:
                                var url = window.location.origin + '\\Views\\Spend\\AdministrativeRecords.aspx';
                                window.location.href = url;
                                break;
                            case 2:
                                radalert("No se puede eliminar el Expediente Administrativo en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar", null, null);
                                break;
                            case 3:
                                radalert("No se puede eliminar el Expediente Administrativo en cuestión debido a que no se ha creado aún en la BD.", 330, 140, "Imposible eliminar", null, null);
                                break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar el Expediente Administrativo en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar", null, null);
                    }
                });
            }

            var radLoadingPanel = null;
            var currentUpdatedControl = null;

            $(document)
                .ajaxStart(function () {
                    radLoadingPanel = $find("ralPrincipal");
                    currentUpdatedControl = "section_spend";
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
