<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="AccountsRestricted.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.AccountsRestricted" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Cuentas Restringidas</h3>

        <%--BUTTONS--%>
        <div class="top-buttons top-buttons-combobox">
            <div class="a-comboboxes">
                <asp:Label ID="LabelType"
                    runat="server"
                    Text="Tipo: "></asp:Label>
                <telerik:RadComboBox ID="RcTypes"
                    runat="server"
                    OnSelectedIndexChanged="RcTypes_OnSelectedIndexChanged"
                    AutoPostBack="True">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Gastos" Value="G" />
                        <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                    </Items>
                </telerik:RadComboBox>
            </div>
            <div class="a-buttons">
                <div class="new main-button">
                    <span class="icon"></span>
                </div>
                <div class="report">
                    <span class="icon"></span>
                    <a onclick="reportAccounts()" role="button">Imprimir</a>
                </div>
                <div class="refresh">
                    <span class="icon"></span>
                </div>
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>
            </div>
        </div>

        <div id="page_accountsRestricted" class="box-block">

            <telerik:RadAjaxPanel ID="rapAccountsRestricted" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgAccountsRestricted"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgAccountsRestricted_OnNeedDataSource"
                    OnInsertCommand="RgAccountsRestricted_OnInsertCommand"
                    OnUpdateCommand="RgAccountsRestricted_OnUpdateCommand"
                    OnDeleteCommand="RgAccountsRestricted_OnDeleteCommand"
                    OnItemDataBound="RgAccountsRestricted_OnItemDataBound"
                    OnPreRender="RgAccountsRestricted_OnPreRender"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="CUE_CODIGO, CUE_ORDINAL_PERCEPTOR, CEN_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="20"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings AddNewRecordText="Nueva"
                            ShowRefreshButton="True"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="CUE_ORDINAL_PERCEPTOR"
                                DataField="CUE_ORDINAL_PERCEPTOR"
                                HeaderText="Ord. Per."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CenLabel"
                                DataField="CenLabel"
                                HeaderText="Sede"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CUE_DESCRIPCION"
                                DataField="CUE_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar cuenta"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar cuenta"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar esta Cuenta Restringida ?</br></br>AVISO: Sólo puede eliminarse una Cuenta Restringida en caso de que</br>no tenga asociado ningún expediente."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Cuenta restringida</h3>
                                <div class="form-group form-group-fake">

                                    <%--Ordinal--%>
                                    <div class="field-container field-30">
                                        <span>Ordinal Perceptor:</span>
                                        <telerik:RadNumericTextBox ID="txtOrder"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            Value="0"
                                            MinValue="0"
                                            ShowSpinButtons="true"
                                            NumberFormat-DecimalDigits="0">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvOrder"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="AccountGroup"
                                            ControlToValidate="txtOrder"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el Ordinal de la Cuenta Restringida."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Tipo--%>
                                    <div class="field-container field-70">
                                        <span>Tipo:</span>
                                        <telerik:RadComboBox ID="radDropType"
                                            runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="Gastos" Value="G" />
                                                <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>

                                </div>

                                <div class="form-group form-group-fake">

                                    <%--Nombre--%>
                                    <div class="field-container field-50">
                                        <span>Nombre:</span>
                                        <telerik:RadTextBox ID="txtName"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("CUE_DESCRIPCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvName"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="AccountGroup"
                                            ControlToValidate="txtName"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el nombre de la Cuenta Restringida."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Sede--%>
                                    <div class="field-container field-50">
                                        <span>Sede:</span>
                                        <telerik:RadComboBox ID="radDropPlace"
                                            runat="server"
                                            Width="100%"
                                            DataTextField="DisplayLabel"
                                            DataValueField="CEN_CODIGO">
                                        </telerik:RadComboBox>
                                    </div>

                                </div>

                                <div class="form-group form-group-fake">

                                    <%--Banco--%>
                                    <div class="field-container field-50">
                                        <span>Banco/Caja:</span>
                                        <telerik:RadTextBox ID="txtBank"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("CUE_ENTIDAD") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvBank"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="AccountGroup"
                                            ControlToValidate="txtBank"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el Banco/Caja de la Cuenta Restringida."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Cuenta--%>
                                    <div class="field-container field-50">
                                        <span>Cuenta Corriente:</span>
                                        <telerik:RadTextBox ID="txtAccount"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="30"
                                            Text='<%#this.Eval("CUE_CC") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvAccount"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="AccountGroup"
                                            ControlToValidate="txtAccount"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la cuenta corriente de la Cuenta Restringida."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div class="form-group">

                                    <%--Direccion--%>
                                    <div class="field-container">
                                        <span>Dirección:</span>
                                        <telerik:RadTextBox ID="txtAddress"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("CUE_DIRECCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddress"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="AccountGroup"
                                            ControlToValidate="txtAddress"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la dirección de la Cuenta Restringida."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="AccountGroup"
                                        CssClass="Button Add"
                                        CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                         <%# (Container is GridEditFormInsertItem) ? "Insertar   " : "Actualizar   " %>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancel"
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

            </telerik:RadAjaxPanel>
        </div>
    </div>

    <telerik:RadScriptBlock runat="server">
        <script>

            function hideAction() {
                $('#ctl00_ContentPlaceHolder_RgAccountsRestricted_ctl00_ctl02_ctl00_InitInsertButton').hide();
            }

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

            function GetGridServerElement(serverId, tagName) {
                if (!tagName) {
                    tagName = "*";
                }

                var grid = $get("<%=this.RgAccountsRestricted.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function reportAccounts() {
                var typeComp = $find("<%= this.RcTypes.ClientID %>");
                var typeValue = typeComp.get_selectedItem().get_value();

                if (typeValue === "") {
                    radalert("No se pueden ver las cuentas restringidas ya que el tipo de cuenta esta vacío.", 330, 140, "Imposible ver Cuentas", null, null);
                    return;
                }

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenanceAccountsRestricted&type=' + typeValue;

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
