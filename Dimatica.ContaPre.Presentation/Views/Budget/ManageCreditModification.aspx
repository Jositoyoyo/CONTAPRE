<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageCreditModification.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Budget.ManageCreditModification" %>

<%@ Register Src="~/Views/Shared/Components/Modals/CustomRawWindow/CustomRawWindow.ascx" TagPrefix="uc" TagName="CustomRawWindow" %>
<%@ Register Src="~/Views/Shared/Components/Alerts/CustomRadAlert/CustomRadAlert.ascx" TagPrefix="uc" TagName="CustomRadAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc:CustomRawWindow ID="customModal" runat="server" />
    <uc:CustomRadAlert ID="CustomRadAlert" runat="server" />
    <!-- Page Content -->
    <div id="section_budget" class="container" style="height: calc(100vh - 56px) !important">

        <h3 id="titleHeader" runat="server">Nueva Modificación</h3>

        <%--MANAGE--%>
        <div class="manage">

            <div class="form-group form-group-fake">

                <%--Año--%>
                <div class="field-container">
                    <span>Año:</span>
                    <telerik:RadMonthYearPicker ID="rmyYear"
                        runat="server"
                        Width="100px"
                        OnSelectedDateChanged="RmyYear_OnSelectedDateChanged"
                        AutoPostBack="True"
                        EnableTyping="False"
                        Culture="es-ES"
                        DateInput-Culture-="es-ES"
                        MonthCellsStyle-CssClass="monthCellClass">
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
                        ValidationGroup="CreditModificationGroup"
                        ControlToValidate="rmyYear"
                        ErrorMessage=" * "
                        ToolTip="Introduzca el Año de la Modificación de Crédito."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>

                <%--N⁰--%>
                <div class="field-container">
                    <span>N⁰:</span>
                    <telerik:RadNumericTextBox ID="txtOrder"
                        runat="server"
                        Width="60px"
                        RenderMode="Lightweight"
                        MinValue="0"
                        ShowSpinButtons="False"
                        NumberFormat-DecimalDigits="0"
                        Enabled="False">
                    </telerik:RadNumericTextBox>
                </div>

                <%--Fecha Propuesta--%>
                <div class="field-container">
                    <span>Fecha Propuesta:</span>
                    <telerik:RadDatePicker ID="dateProposal"
                        RenderMode="Lightweight"
                        runat="server"
                        Width="120px"
                        AutoPostBack="False"
                        Culture="es-ES"
                        EnableTyping="False"
                        MaxDate="12/31/9999"
                        MinDate="01/01/1800" />
                    <asp:RequiredFieldValidator ID="rfvProposal"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="CreditModificationGroup"
                        ControlToValidate="dateProposal"
                        ErrorMessage=" * "
                        ToolTip="Introduzca la Fecha de Propuesta de la Modificación de Crédito."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>

                <%--Fecha Asiento--%>
                <div class="field-container">
                    <span>Fecha Asiento:</span>
                    <telerik:RadDatePicker ID="dateEfective"
                        RenderMode="Lightweight"
                        runat="server"
                        Width="120px"
                        AutoPostBack="False"
                        Culture="es-ES"
                        EnableTyping="False"
                        MaxDate="12/31/9999"
                        MinDate="01/01/1800" />
                    <asp:RequiredFieldValidator ID="rfvEfective"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="CreditModificationGroup"
                        ControlToValidate="dateEfective"
                        ErrorMessage=" * "
                        ToolTip="Introduzca la Fecha de Asiento de la Modificación de Crédito."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>

                <%--Fecha Movimiento--%>
                <div class="field-container">
                    <span>Fecha Movimiento:</span>
                    <telerik:RadDatePicker ID="dateCreated"
                        RenderMode="Lightweight"
                        runat="server"
                        Width="120px"
                        AutoPostBack="False"
                        Culture="es-ES"
                        EnableTyping="False"
                        Enabled="false"
                        MaxDate="12/31/9999"
                        MinDate="01/01/1800" />
                </div>

                <%--Tipo Modif. Ingresos--%>
                <div class="field-container">
                    <span>Tipo Modif. Ingresos:</span>
                    <telerik:RadComboBox ID="rcModificationTypeIncomes"
                        runat="server"
                        Width="100%"
                        AutoPostBack="False"
                        DataTextField="DisplayLabel"
                        DataValueField="TIPM_CODIGO">
                    </telerik:RadComboBox>
                </div>

                <%--Tipo Modif. Gastos--%>
                <div class="field-container">
                    <span>Tipo Modif. Gastos:</span>
                    <telerik:RadComboBox ID="rcModificationTypeSpends"
                        runat="server"
                        Width="100%"
                        AutoPostBack="False"
                        DataTextField="DisplayLabel"
                        DataValueField="TIPM_CODIGO">
                    </telerik:RadComboBox>
                </div>

                <%--Descripcion--%>
                <div class="field-container">
                    <span>Descripción:</span>
                    <telerik:RadTextBox ID="txtDescription"
                        Width="100%"
                        runat="server"
                        MaxLength="120"
                        Text="">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="CreditModificationGroup"
                        ControlToValidate="txtDescription"
                        ErrorMessage=" * "
                        ToolTip="Introduzca la Descripción de la Modificación de Crédito."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">

                <%--Estado--%>
                <div class="field-container" style="display: flex; flex-direction: row">
                    <span>Estado de la modificación:</span>
                    <p id="txtStatus"
                        runat="server"
                        style="color: green; margin-left: 5px">
                        No Ejecutada
                    </p>
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
                        ValidationGroup="CreditModificationGroup">
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

        <div id="page_manageCreditModification" class="box-block">

            <telerik:RadAjaxPanel ID="rapManageCreditModification" runat="server"
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

                <div class="divided-screen">

                    <div class="divided-screen-50">

                        <div class="new-application">
                            <strong>Aplicaciones de Ingresos:</strong>
                        </div>

                        <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgIncomeApplications"
                            runat="server"
                            AllowSorting="true"
                            Culture="es-ES"
                            GroupPanelPosition="Top"
                            OnNeedDataSource="RgIncomeApplications_OnNeedDataSource"
                            OnPreRender="RgIncomeApplications_OnPreRender"
                            OnItemDataBound="RgIncomeApplications_OnItemDataBound"
                            OnInsertCommand="RgIncomeApplications_OnInsertCommand"
                            OnUpdateCommand="RgIncomeApplications_OnUpdateCommand"
                            OnDeleteCommand="RgIncomeApplications_OnDeleteCommand">

                            <GroupingSettings CaseSensitive="false" />

                            <MasterTableView AutoGenerateColumns="false"
                                AllowFilteringByColumn="true"
                                DataKeyNames="PRE_CODIGO, MOD_CODIGO, CACS_CODIGO, MODP_IMPORTE, SingLabel"
                                CommandItemDisplay="Top"
                                AllowPaging="false"
                                PagerStyle-AlwaysVisible="false"
                                NoMasterRecordsText="No Hay datos a Mostrar."
                                TableLayout="Fixed"
                                EditMode="PopUp"
                                CssClass="popup-table small-table">

                                <CommandItemSettings ShowRefreshButton="False"
                                    ShowExportToExcelButton="False"
                                    ShowExportToPdfButton="False"
                                    ShowAddNewRecordButton="true"
                                    AddNewRecordText="Nuevo" />

                                <PagerStyle Mode="NextPrevAndNumeric"
                                    PageSizeLabelText="Elementos por pagina: "
                                    PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="CACS_NUMERO_Income"
                                        DataField="CACS_NUMERO"
                                        HeaderText="Aplicación"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn UniqueName="AmountLabel_Income"
                                        DataField="MODP_IMPORTE"
                                        HeaderText="Importe"
                                        DataFormatString="{0:N}"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right"
                                        FilterControlWidth="100%">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridNumericColumn>
                                    <%--<telerik:GridBoundColumn UniqueName="AmountLabel_Income"
                                        DataField="AmountLabel"
                                        HeaderText="Importe"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn UniqueName="SingLabel_Income"
                                        DataField="SingLabel"
                                        HeaderText="Signo"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridEditCommandColumn UniqueName="EditColumn_Income"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Modificar Aplicación"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-pencil-alt"
                                        ShowFilterIcon="false"
                                        EditText=" ">
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn_Income"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Eliminar Aplicación"
                                        CommandName="Delete"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-trash-alt"
                                        Text=" " ConfirmDialogType="RadWindow"
                                        ConfirmTitle="ATENCIÓN"
                                        ConfirmText="¿ Está seguro que desea eliminar esta Aplicación ?"
                                        ConfirmDialogHeight="100px"
                                        HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridButtonColumn>
                                </Columns>

                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>

                                        <h3>Aplicación de Ingreso</h3>

                                        <div class="form-group form-group-fake">
                                            <%--Aplicacion--%>
                                            <div class="field-container field-40">
                                                <span>Aplicación:</span>
                                                <telerik:RadComboBox ID="radDropIncomeAplication"
                                                    runat="server"
                                                    Width="100%"
                                                    Enabled="<%# ((Container is GridEditFormInsertItem)) %>"
                                                    DataValueField="CacsCode"
                                                    DataTextField="CacsNumber">
                                                </telerik:RadComboBox>
                                            </div>

                                            <%--Importe--%>
                                            <div class="field-container field-45">
                                                <span>Importe:</span>
                                                <telerik:RadNumericTextBox ID="txtIncomeAmount"
                                                    runat="server"
                                                    RenderMode="Lightweight"
                                                    Value="0"
                                                    MinValue="0"
                                                    ShowSpinButtons="False">
                                                </telerik:RadNumericTextBox>
                                                <asp:RequiredFieldValidator ID="rfvIncomeAmount"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ValidationGroup="IncomeApplicationGroup"
                                                    ControlToValidate="txtIncomeAmount"
                                                    ErrorMessage=" * "
                                                    ToolTip="Introduzca el importe de la Aplicación de Ingreso."
                                                    ForeColor="Red">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <%--Signo--%>
                                            <div class="field-container field-15">
                                                <span>Signo:</span>
                                                <telerik:RadComboBox ID="radDropIncomeSing"
                                                    runat="server" Width="100%">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="+" Value="+" />
                                                        <telerik:RadComboBoxItem runat="server" Text="-" Value="-" />
                                                    </Items>
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

                        <div class="monto-total">
                            <strong>Total:</strong>
                            <telerik:RadTextBox ID="txtTotalIncomes"
                                Width="80px"
                                runat="server"
                                MaxLength="80"
                                Text="0,00"
                                Enabled="False">
                            </telerik:RadTextBox>
                        </div>
                    </div>

                    <div class="divided-screen-50">

                        <div class="new-application">
                            <strong>Aplicaciones de Gastos:</strong>
                        </div>

                        <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgSpendApplications"
                            runat="server"
                            AllowSorting="true"
                            Culture="es-ES"
                            GroupPanelPosition="Top"
                            OnNeedDataSource="RgSpendApplications_OnNeedDataSource"
                            OnPreRender="RgSpendApplications_OnPreRender"
                            OnItemDataBound="RgSpendApplications_OnItemDataBound"
                            OnInsertCommand="RgSpendApplications_OnInsertCommand"
                            OnUpdateCommand="RgSpendApplications_OnUpdateCommand"
                            OnDeleteCommand="RgSpendApplications_OnDeleteCommand">

                            <GroupingSettings CaseSensitive="false" />

                            <MasterTableView AutoGenerateColumns="false"
                                AllowFilteringByColumn="true"
                                DataKeyNames="PRE_CODIGO, MOD_CODIGO, CACS_CODIGO, MODP_IMPORTE, SingLabel"
                                CommandItemDisplay="Top"
                                AllowPaging="false"
                                PagerStyle-AlwaysVisible="false"
                                NoMasterRecordsText="No Hay datos a Mostrar."
                                TableLayout="Fixed"
                                EditMode="PopUp"
                                CssClass="popup-table small-table">

                                <CommandItemSettings ShowRefreshButton="False"
                                    ShowExportToExcelButton="False"
                                    ShowExportToPdfButton="False"
                                    ShowAddNewRecordButton="true"
                                    AddNewRecordText="Nuevo" />

                                <PagerStyle Mode="NextPrevAndNumeric"
                                    PageSizeLabelText="Elementos por pagina: "
                                    PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="CACS_NUMERO_Spend"
                                        DataField="CACS_NUMERO"
                                        HeaderText="Aplicación"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn UniqueName="AmountLabel_Spend"
                                        DataField="MODP_IMPORTE"
                                        HeaderText="Importe"
                                        DataFormatString="{0:N}"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right"
                                        FilterControlWidth="100%">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridNumericColumn>
                                    <%--<telerik:GridBoundColumn UniqueName="AmountLabel_Spend"
                                        DataField="AmountLabel"
                                        HeaderText="Importe"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn UniqueName="SingLabel_Spend"
                                        DataField="SingLabel"
                                        HeaderText="Signo"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridEditCommandColumn UniqueName="EditColumn_Spend"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Modificar Aplicación"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-pencil-alt"
                                        ShowFilterIcon="false"
                                        EditText=" ">
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn_Spend"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Eliminar Aplicación"
                                        CommandName="Delete"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-trash-alt"
                                        Text=" " ConfirmDialogType="RadWindow"
                                        ConfirmTitle="ATENCIÓN"
                                        ConfirmText="¿ Está seguro que desea eliminar esta Aplicación ?"
                                        ConfirmDialogHeight="100px"
                                        HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridButtonColumn>
                                </Columns>

                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>

                                        <h3>Aplicación de Ingreso</h3>

                                        <div class="form-group form-group-fake">
                                            <%--Aplicacion--%>
                                            <div class="field-container field-40">
                                                <span>Aplicación:</span>
                                                <telerik:RadComboBox ID="radDropSpendAplication"
                                                    runat="server"
                                                    Width="100%"
                                                    Enabled="<%# ((Container is GridEditFormInsertItem)) %>"
                                                    DataValueField="CacsCode"
                                                    DataTextField="CacsNumber">
                                                </telerik:RadComboBox>
                                            </div>

                                            <%--Importe--%>
                                            <div class="field-container field-45">
                                                <span>Importe:</span>
                                                <telerik:RadNumericTextBox ID="txtSpendAmount"
                                                    runat="server"
                                                    RenderMode="Lightweight"
                                                    Value="0"
                                                    MinValue="0"
                                                    ShowSpinButtons="False">
                                                </telerik:RadNumericTextBox>
                                                <asp:RequiredFieldValidator ID="rfvSpendAmount"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ValidationGroup="SpendApplicationGroup"
                                                    ControlToValidate="txtSpendAmount"
                                                    ErrorMessage=" * "
                                                    ToolTip="Introduzca el importe de la Aplicación de Gasto."
                                                    ForeColor="Red">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <%--Signo--%>
                                            <div class="field-container field-15">
                                                <span>Signo:</span>
                                                <telerik:RadComboBox ID="radDropSpendSing"
                                                    runat="server" Width="100%">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="+" Value="+" />
                                                        <telerik:RadComboBoxItem runat="server" Text="-" Value="-" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>

                                        <%--BUTTONS--%>
                                        <div class="buttons">

                                            <asp:LinkButton ID="btnSpendUpdate"
                                                runat="server"
                                                ValidationGroup="SpendApplicationGroup"
                                                CssClass="Button Add"
                                                CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                         <%# (Container is GridEditFormInsertItem) ? "Insertar   " : "Actualizar   " %>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnSpendCancel"
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

                        <div class="monto-total">
                            <strong>Importe Total:</strong>
                            <telerik:RadTextBox ID="txtTotalSpends"
                                Width="80px"
                                runat="server"
                                MaxLength="80"
                                Text="0,00"
                                Enabled="False">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                </div>

                <%--BUTTONS--%>
                <div class="form-group buttons">
                    <telerik:RadButton ButtonType="LinkButton" ID="btnGenerateFile"
                        runat="server"
                        RenderMode="Native"
                        Text="Generar Exp."
                        AutoPostBack="True"
                        OnClick="btnGenerateFile_Click">
                    </telerik:RadButton>
                    <telerik:RadButton ButtonType="LinkButton" ID="btnReport"
                        runat="server"
                        RenderMode="Native"
                        Text="Imprimir"
                        AutoPostBack="True"
                        OnClick="btnReport_Click">
                    </telerik:RadButton>
                    <telerik:RadButton
                        ButtonType="LinkButton"
                        ID="btnExecute"
                        runat="server"
                        RenderMode="Native"
                        Text="Ejecutar Mod."
                        AutoPostBack="True"
                        OnClick="btnExecute_OnClick">
                    </telerik:RadButton>



                </div>

            </telerik:RadAjaxPanel>
        </div>
    </div>

    <telerik:RadScriptBlock runat="server">

        <script>

            var modalDiv = null;

            function showModalDiv(sender, args)
            {
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

            function hideModalDiv()
            {
                modalDiv.style.display = "none";
            }

            function hideGrids()
            {
                $('#page_manageCreditModification').hide();
            }

            function moveNewButtons()
            {
                var $this = $("a[title*='Nuevo']");
                $this.each(function () {
                    var $destino = $(this).closest('.divided-screen-50').children(".new-application");
                    !$destino.has("a").length && $(this).appendTo($destino).show();
                });
            }

            function printCreditModification(id, description, year, order, proposalDate, incomeType, incomeDescription, spendType, spendDescription)
            {
                var urlSpend = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=spendCreditModification&id= ' + id + '&description=' + description + '&year=' + year
                    + '&order=' + order + '&proposalDate=' + proposalDate + '&modificationType=' + spendType + '&modificationTypeDescription=' + spendDescription;
                window.open(urlSpend, "Modificación de Crédito Gastos");

                var urlIncome = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=incomeCreditModification&id= ' + id + '&description=' + description + '&year=' + year
                    + '&order=' + order + '&proposalDate=' + proposalDate + '&modificationType=' + incomeType + '&modificationTypeDescription=' + incomeDescription;
                window.open(urlIncome, "Modificación de Crédito Ingresos");
            }


            function confirmExecute()
            {

                openCustomModal2({
                    title: 'Confirmación',
                    message: '¿Estás seguro de que deseas realizar esta acción?',
                    width: 450,
                    height: 200,
                    okText: 'Sí',
                    cancelText: 'No',
                    callback: function (isConfirmed) {
                        if (isConfirmed)
                        {
                            executeCreditModificationAjax();
                        }
                    }
                });
            }

            function executeCreditModificationAjax()
            {

                openCustomRadAlert2({
                    title: 'Enviando datos',
                    message: 'Enviado datos al servidor. Espere por favor...',
                    width: 350,
                    height: 300,
                    buttonText: '',
                    showCustomRadAlertCloseButton: false,
                    callback: null
                });

                const sUrl = "<%= ResolveUrl("~/Views/Budget/ManageCreditModification.aspx/ExecuteCreditModification") %>";

                $.ajax({
                    type: "POST",
                    url: sUrl,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response)
                    {
                        OnExecuteSuccess(response.d); 
                    },
                    error: function (xhr, status, error) {
                        console.error("Error en la solicitud AJAX: ", error);
                        openCustomRadAlert('<strong>Error:</strong> Hubo un error al ejecutar la solicitud', 350, 250, 'Error', null);

                    }
                });
            }

            // mejorar esta ruina
            function OnExecuteSuccess(response)
            {
                switch (response) {
                    case 0:
                        openCustomRadAlert2({
                            title: 'Error en la operación',
                            message: 'Ha ocurrido un error ejecutando la Modificación de Crédito',
                            width: 350,
                            height: 300,
                            buttonText: 'Aceptar',
                            showCustomRadAlertCloseButton: true,
                            callback: null
                        });
                        break;
                    case 1:
                         openCustomRadAlert2({
                            title: 'Éxito en la operación',
                            message: 'Éxito al ejecutar la Modificación de Crédito. ',
                            width: 350,
                            height: 300,
                            buttonText: 'Aceptar',
                            showCustomRadAlertCloseButton: true,
                            callback: function () {
                                window.location.reload(true);
                            }
                        });
                        break;
                    case 2:
                        openCustomRadAlert2({
                            title: 'No se puede ejecutar la Modificación de Crédito',
                            message: 'Una modificación de crédito solo puede ejecutarse si los presupuestos de Ingresos y de Gastos del año correspondiente están cerrados.<br/><br/>Cierre el presupuesto de Ingresos del año correspondiente y a continuación ejecute la modificación de crédiicación de crédito',
                            width: 350,
                            height: 300,
                            buttonText: 'Aceptar',
                            showCustomRadAlertCloseButton: true,
                            callback: null
                        });
                        break;
                    case 3:
                        openCustomRadAlert2({
                            title: 'No se puede ejecutar la Modificación de Crédito',
                            message: 'Una modificación de crédito solo puede ejecutarse si los presupuestos de Ingresos y de Gastos del año correspondiente están cerrados.<br/><br/>Cierre el presupuesto de Gastos del año correspondiente y a continuación ejecute la modificación de crédito.',
                            width: 350,
                            height: 300,
                            buttonText: 'Aceptar',
                            showCustomRadAlertCloseButton: true,
                            callback: null
                        });
                        break;
                }
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
