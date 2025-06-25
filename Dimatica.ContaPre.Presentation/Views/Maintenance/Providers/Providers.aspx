<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Providers.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.Providers.Providers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Proveedores</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">

                <div class="new main-button">
                    <span class="icon"></span>
                </div>
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton
                        ID="btnFind"
                        runat="server"
                        OnClick="btnFind_OnClick"
                        Text="Buscar"
                        ValidationGroup="ProviderGroup"></asp:LinkButton>
                </div>
                <div class="report">
                    <span class="icon"></span>
                    <a onclick="reportPayTypes()" role="button">Imprimir</a>
                </div>
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>

            </div>
        </div>

        <div id="page_providers" class="box-block">

            <telerik:RadAjaxPanel ID="rapProviders" runat="server"
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
                <telerik:RadPanelBar ID="RpbFilter"
                    runat="server"
                    RenderMode="Lightweight"
                    Width="100%"
                    Height="100%">
                    <Items>
                        <telerik:RadPanelItem runat="server"
                            Text="Filtros"
                            Expanded="False">
                            <ContentTemplate>
                                <div class="manage">
                                    <div class="form-group form-group form-group-fake">
                                        <div class="field-container">
                                            <span>Nombre:</span>
                                            <telerik:RadTextBox ID="RtbName"
                                                runat="server"
                                                Width="100%"
                                                MaxLength="60"
                                                Text="">
                                            </telerik:RadTextBox>
                                        </div>

                                        <div class="field-container" style="display: block">
                                            <span style="display: block">NIF:</span>
                                            <telerik:RadTextBox ID="RtbNifFirst"
                                                runat="server"
                                                Width="50px"
                                                MaxLength="1"
                                                Text="">
                                            </telerik:RadTextBox>
                                            <asp:RegularExpressionValidator ID="revNifFirst"
                                                runat="server"
                                                Display="Dynamic"
                                                ValidationGroup="ProviderGroup"
                                                ControlToValidate="RtbNifFirst"
                                                ErrorMessage=" * "
                                                ToolTip="Formato inválido."
                                                ForeColor="Red"
                                                ValidationExpression="[a-zA-Z]">
                                            </asp:RegularExpressionValidator>
                                            <telerik:RadTextBox ID="RtbNifNumber"
                                                runat="server"
                                                Width="100px"
                                                MaxLength="8"
                                                Text="">
                                            </telerik:RadTextBox>
                                            <asp:RegularExpressionValidator ID="revNifNumber"
                                                runat="server"
                                                Display="Dynamic"
                                                ValidationGroup="ProviderGroup"
                                                ControlToValidate="RtbNifNumber"
                                                ErrorMessage=" * "
                                                ToolTip="Formato inválido."
                                                ForeColor="Red"
                                                ValidationExpression="\d{7,8}">
                                            </asp:RegularExpressionValidator>
                                            <telerik:RadTextBox ID="RtbNifLast"
                                                runat="server"
                                                Width="50px"
                                                MaxLength="1"
                                                Text="">
                                            </telerik:RadTextBox>
                                            <asp:RegularExpressionValidator ID="revNifLast"
                                                runat="server"
                                                Display="Dynamic"
                                                ValidationGroup="ProviderGroup"
                                                ControlToValidate="RtbNifLast"
                                                ErrorMessage=" * "
                                                ToolTip="Formato inválido."
                                                ForeColor="Red"
                                                ValidationExpression="[a-zA-Z]">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgProviders"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgProviders_OnNeedDataSource"
                    OnInsertCommand="RgProviders_OnInsertCommand"
                    OnUpdateCommand="RgProviders_OnUpdateCommand"
                    OnDeleteCommand="RgProviders_OnDeleteCommand"
                    OnItemDataBound="RgProviders_OnItemDataBound"
                    OnPreRender="RgProviders_OnPreRender"
                    CssClass="provider-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="PROV_CODIGO, PROV_NIF, ProvID"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table popup-table-large">

                        <CommandItemSettings AddNewRecordText="Nuevo"
                            ShowRefreshButton="False"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PROV_CODIGO"
                                DataField="PROV_CODIGO"
                                HeaderText="Código"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                                DataField="PROV_NOMBRE"
                                HeaderText="Nombre"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PROV_NIF"
                                DataField="PROV_NIF"
                                HeaderText="Nif"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="20%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar Proveedor"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Proveedor"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar este Proveedor ?</br></br>AVISO: Sólo puede eliminarse un Proveedor en caso de que</br>no tenga asociado ningún expediente."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Proveedor</h3>
                                <div class="form-group form-group-fake">

                                    <%--Nombre--%>
                                    <div class="field-container field-30">
                                        <span>Nombre:</span>
                                        <telerik:RadTextBox ID="txtName"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("PROV_NOMBRE") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvName"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ProviderGroup"
                                            ControlToValidate="txtName"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el Nombre del Proveedor."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--NifFist--%>
                                    <div class="field-container field-5 nif">
                                        <span>NIF:</span>
                                        <telerik:RadTextBox ID="txtNifFirst"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="1"
                                            Text="">
                                        </telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="revNifFirst"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ProviderGroup"
                                            ControlToValidate="txtNifFirst"
                                            ErrorMessage=" * "
                                            ToolTip="Formato inválido."
                                            ForeColor="Red"
                                            ValidationExpression="[a-zA-Z]">
                                        </asp:RegularExpressionValidator>
                                    </div>

                                    <%--guion--%>
                                    <div class="field-container field-5 nif-guion">
                                        <span>-</span>
                                    </div>

                                    <%--NifNumber--%>
                                    <div class="field-container field-15 nif-num">
                                        <em>núm.:</em>
                                        <telerik:RadTextBox ID="txtNifNumber"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="8"
                                            Text="">
                                        </telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="revNifNumber"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ProviderGroup"
                                            ControlToValidate="txtNifNumber"
                                            ErrorMessage=" * "
                                            ToolTip="Formato inválido."
                                            ForeColor="Red"
                                            ValidationExpression="\d{7,8}">
                                        </asp:RegularExpressionValidator>
                                        <%--          <asp:CustomValidator ID="CvNif"
                                            runat="server"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ClientValidationFunction="validateNif"
                                            ErrorMessage=" * "
                                            ToolTip="Formato inválido."
                                            ValidationGroup="ProviderGroup"
                                            ForeColor="Red">
                                        </asp:CustomValidator>--%>
                                    </div>

                                    <%--guion--%>
                                    <div class="field-container field-5 nif-guion">
                                        <span>-</span>
                                    </div>

                                    <%--NifLast--%>
                                    <div class="field-container field-5 nif-letra">
                                        <em>letra:</em>
                                        <telerik:RadTextBox ID="txtNifLast"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="1"
                                            Text="">
                                        </telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="revNifLast"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ProviderGroup"
                                            ControlToValidate="txtNifLast"
                                            ErrorMessage=" * "
                                            ToolTip="Formato inválido."
                                            ForeColor="Red"
                                            ValidationExpression="[a-zA-Z]">
                                        </asp:RegularExpressionValidator>
                                    </div>

                                    <%--Telefono--%>
                                    <div class="field-container field-15">
                                        <span>Teléfono:</span>
                                        <telerik:RadTextBox ID="txtPhone"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="20"
                                            Text='<%#this.Eval("PROV_TELEFONO") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--Persona--%>
                                    <div class="field-container field-20">
                                        <span>Persona contacto:</span>
                                        <telerik:RadTextBox ID="txtPerson"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="75"
                                            Text='<%#this.Eval("PROV_PERSONA_CONTACTO") %>'>
                                        </telerik:RadTextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-fake">

                                    <%--Direccion--%>
                                    <div class="field-container field-35">
                                        <span>Dirección:</span>
                                        <telerik:RadTextBox ID="txtAddress"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="100"
                                            Text='<%#this.Eval("PROV_DIRECCION") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--CP--%>
                                    <div class="field-container field-10">
                                        <span>C.P.:</span>
                                        <telerik:RadTextBox ID="txtCp"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="5"
                                            Text='<%#this.Eval("PROV_CODIGO_POSTAL") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--Provincia--%>
                                    <div class="field-container field-20">
                                        <span>Provincia:</span>
                                        <telerik:RadComboBox ID="radDropProvince"
                                            runat="server"
                                            Width="100%"
                                            DataTextField="Provincia1"
                                            DataValueField="ProvIdInt">
                                        </telerik:RadComboBox>
                                    </div>

                                    <%--Población--%>
                                    <div class="field-container field-35">
                                        <span>Población:</span>
                                        <telerik:RadTextBox ID="txtPopulation"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="50"
                                            Text='<%#this.Eval("PROV_POBLACION") %>'>
                                        </telerik:RadTextBox>
                                    </div>
                                </div>

                                <h4><span>Datos bancarios</span></h4>

                                <div class="form-group form-group-fake">

                                    <%--IbanCC--%>
                                    <div style="display: none !important">
                                        <span>Iban:</span>
                                        <telerik:RadTextBox ID="txtCcIban"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="50"
                                            Text="">
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--CCC--%>
                                    <div class="field-container field-10">
                                        <strong>CCC</strong>
                                        <asp:CustomValidator ID="cvBankAccount"
                                            runat="server"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ClientValidationFunction="validateIban"
                                            ErrorMessage=" * "
                                            ToolTip="IBAN con formato iválido."
                                            ValidationGroup="ProviderGroup"
                                            ForeColor="Red">
                                        </asp:CustomValidator>
                                    </div>

                                    <%--EntidadCC--%>
                                    <div class="field-container field-15">
                                        <span>Entidad:</span>
                                        <telerik:RadTextBox ID="txtCcEntity"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="4"
                                            Text='<%#this.Eval("PROV_CC_CE") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--SucursalCC--%>
                                    <div class="field-container field-15">
                                        <span>Sucursal:</span>
                                        <telerik:RadTextBox ID="txtCcBranch"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="4"
                                            Text='<%#this.Eval("PROV_CC_CO") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--DCCC--%>
                                    <div class="field-container field-10">
                                        <span>DC:</span>
                                        <telerik:RadTextBox ID="txtCcDc"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="2"
                                            Text='<%#this.Eval("PROV_CC_DC") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--CuentaCC--%>
                                    <div class="field-container field-25">
                                        <span>Cuenta:</span>
                                        <telerik:RadTextBox ID="txtCcAccount"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="10"
                                            Text='<%#this.Eval("PROV_CC_NC") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                </div>

                                <div class="form-group form-group-fake">

                                    <%--DomicilioBranch--%>
                                    <div class="field-container field-20">
                                        <span>Domicilio:</span>
                                        <telerik:RadTextBox ID="txtBranchAddress"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="100"
                                            Text='<%#this.Eval("PROV_DIR_SUCURSAL") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--EntidadBranch--%>
                                    <div class="field-container field-20">
                                        <span>Entidad:</span>
                                        <telerik:RadTextBox ID="txtBranchEntity"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="50"
                                            Text='<%#this.Eval("PROV_NOMBRE_SUCURSAL") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--CpBranch--%>
                                    <div class="field-container field-20">
                                        <span>C.P.:</span>
                                        <telerik:RadTextBox ID="txtBranchCp"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="5"
                                            Text='<%#this.Eval("PROV_CP_SUCURSAL") %>'>
                                        </telerik:RadTextBox>
                                    </div>

                                    <%--LocalidadBranch--%>
                                    <div class="field-container field-40">
                                        <span>Localidad:</span>
                                        <telerik:RadTextBox ID="txtBranchLocation"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="50"
                                            Text='<%#this.Eval("PROV_POBLACION_SUCURSAL") %>'>
                                        </telerik:RadTextBox>
                                    </div>
                                </div>

                                <%--Se pone un div para ocultarlo despues--%>
                                <div id="similaritiesProviders">

                                    <h4 style="border-bottom: 1px solid red !important"><span style="color: red">Semejanzas</span></h4>

                                    <Controls:ProvidersControl ID="providersControl"
                                        runat="server" />
                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="ProviderGroup"
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
                $('#ctl00_ContentPlaceHolder_RgProviders_ctl00_ctl02_ctl00_InitInsertButton').hide();
            }

            function hideProvidersGrid() {
                $('#similaritiesProviders').hide();
            }

            function showProvidersGrid() {
                $('#similaritiesProviders').show();
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

                var grid = $get("<%=this.RgProviders.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function validateIban(sender, args) {
                var ccEntity = GetGridServerElement('txtCcEntity', 'input');
                var ccEntityComp = $find(ccEntity.id);
                var ccEntityText = ccEntityComp.get_value();
                var ccBranch = GetGridServerElement('txtCcBranch', 'input');
                var ccBranchComp = $find(ccBranch.id);
                var ccBranchText = ccBranchComp.get_value();
                var ccDc = GetGridServerElement('txtCcDc', 'input');
                var ccDcComp = $find(ccDc.id);
                var ccDcText = ccDcComp.get_value();
                var ccAccount = GetGridServerElement('txtCcAccount', 'input');
                var ccAccountComp = $find(ccAccount.id);
                var ccAccountText = ccAccountComp.get_value();
                var ccIbanI = GetGridServerElement('txtCcIban', 'input');
                var ccIbanComp = $find(ccIbanI.id);
                ccIbanComp.set_value('');

                if (ccEntityText == "" && ccBranchText == "" && ccDcText == "" && ccAccountText == "") {
                    return true;
                }

                var length = ccEntityText.length + ccBranchText.length + ccDcText.length + ccAccountText.length;
                if (length != 20) {
                    args.IsValid = false;
                    return false;
                }

                var iban = ccEntityText + ccBranchText;
                var mod1 = iban % 97;
                iban = '' + mod1 + ccDcText + ccAccountText.substring(0, 2);
                mod1 = iban % 97;
                iban = '' + mod1 + ccAccountText.substring(2, ccAccountText.length) + '142800';
                var modIban = iban % 97;
                var ccIban = 98 - modIban;
                if (ccIban < 10) {
                    ccIban = '0' + ccIban;
                }

                var realIban = "ES" + ccIban + ccEntityText + ccBranchText + ccDcText + ccAccountText;

                realIban = realIban.toUpperCase();
                realIban = trim(realIban);
                realIban = realIban.replace(/\s/g, "");

                var letter1, letter2, num1, num2;
                var auxIban;

                if (realIban.length != 24) {
                    args.IsValid = false;
                    return false;
                }

                letter1 = realIban.substring(0, 1);
                letter2 = realIban.substring(1, 2);

                num1 = getnumIBAN(letter1);
                num2 = getnumIBAN(letter2);

                auxIban = realIban.substring(4) + String(num1) + String(num2) + realIban.substring(2, 4);

                var part = auxIban.substring(0, 9);
                var rest = part % 97;
                part = '' + rest + auxIban.substring(9, 17);
                rest = part % 97;
                part = '' + rest + auxIban.substring(17, 21);
                rest = part % 97;
                part = '' + rest + auxIban.substring(21);
                rest = part % 97;

                if (rest != 1) {
                    args.IsValid = false;
                    return false;
                }

                ccIbanComp.set_value(realIban);
                return true;
            }

            function trim(myString) {
                return myString.replace(/^\s+/g, '').replace(/\s+$/g, '');
            }

            function getnumIBAN(letter) {
                var lsLetters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
                return lsLetters.search(letter) + 10;
            }

            function validateNif(sender, args) {
                var nifFirst = GetGridServerElement('txtNifFirst', 'input');
                var nifFirstComp = $find(nifFirst.id);
                var nifFirstText = nifFirstComp.get_value();
                var nifNumber = GetGridServerElement('txtNifNumber', 'input');
                var nifNumberComp = $find(nifNumber.id);
                var nifNumberText = nifNumberComp.get_value();
                var nifLast = GetGridServerElement('txtNifLast', 'input');
                var nifLastComp = $find(nifLast.id);
                var nifLastText = nifLastComp.get_value();

                if (nifNumberText == "" && nifFirstText == "" && nifLastText == "") {
                    return true;
                }

                if (nifNumberText == "") {
                    args.IsValid = false;
                    return false;
                }

                if (nifFirstText == "" && nifLastText == "") {
                    args.IsValid = false;
                    return false;
                }

                var nifToValidate = nifFirstText + nifNumberText + nifLastText;
                nifToValidate = nifToValidate.toUpperCase().replace(/\s/, '');

                var valid = false;
                var type = spainIdType(nifToValidate);

                switch (type) {
                    case 'dni':
                        valid = validDNI(nifToValidate);
                        break;
                    case 'nie':
                        valid = validNIE(nifToValidate);
                        break;
                    case 'cif':
                        valid = validCIF(nifToValidate);
                        break;
                }

                if (valid == false) {
                    args.IsValid = false;
                    return false;
                }

                return true;
            }

            function spainIdType(str) {
                if (str.match(DNI_REGEX)) {
                    return 'dni';
                }
                if (str.match(CIF_REGEX)) {
                    return 'cif';
                }
                if (str.match(NIE_REGEX)) {
                    return 'nie';
                }
            };

            function validDNI(dni) {
                var dni_letters = "TRWAGMYFPDXBNJZSQVHLCKE";
                var letter = dni_letters.charAt(parseInt(dni, 10) % 23);

                return letter == dni.charAt(8);
            };

            function validNIE(nie) {
                var nie_prefix = nie.charAt(0);

                switch (nie_prefix) {
                    case 'X': nie_prefix = 0; break;
                    case 'Y': nie_prefix = 1; break;
                    case 'Z': nie_prefix = 2; break;
                }

                return validDNI(nie_prefix + nie.substr(1));

            };

            function validCIF(cif) {

                var match = cif.match(CIF_REGEX);
                var letter = match[1],
                    number = match[2],
                    control = match[3];

                var even_sum = 0;
                var odd_sum = 0;
                var n;

                for (var i = 0; i < number.length; i++) {
                    n = parseInt(number[i], 10);

                    if (i % 2 === 0) {
                        n *= 2;

                        odd_sum += n < 10 ? n : n - 9;
                    } else {
                        even_sum += n;
                    }

                }
                debugger
                var control_digit = (10 - (even_sum + odd_sum).toString().substr(-1));
                var control_letter = 'JABCDEFGHI'.substr(control_digit, 1);

                if (letter.match(/[ABEH]/)) {
                    return control == control_digit;

                } else if (letter.match(/[KPQS]/)) {
                    return control == control_letter;
                } else {
                    return control == control_digit || control == control_letter;
                }

            };

            var DNI_REGEX = /^(\d{8})([A-Z])$/;
            var CIF_REGEX = /^([ABCDEFGHJKLMNPQRSUVW])(\d{7})([0-9A-J])$/;
            var NIE_REGEX = /^[XYZ]\d{7,8}[A-Z]$/;

            function reportPayTypes() {
                var nameComp = $find("<%= this.RtbName.ClientID %>");
                var nameValue = nameComp.get_value();

                var nifFirstComp = $find("<%= this.RtbNifFirst.ClientID %>");
                var nifFirstValue = nifFirstComp.get_value();

                var nifNumberComp = $find("<%= this.RtbNifNumber.ClientID %>");
                var nifNumberValue = nifNumberComp.get_value();

                var nifLastComp = $find("<%= this.RtbNifLast.ClientID %>");
                var nifLastValue = nifLastComp.get_value();

                var nifAux = nifFirstValue + '-' + nifNumberValue + '-' + nifLastValue;
                var providerNif = nifAux === '--' ? '' : nifAux;

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenanceProviders';

                if (nameValue !== '') {
                    url = url + '&name=' + nameValue;
                }

                if (providerNif !== '') {
                    url = url + '&nif=' + providerNif;
                }

                window.open(url, "Reporte");
            }

        </script>
    </telerik:RadScriptBlock>

</asp:Content>
