<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="DocumentTypes.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.DocumentTypes" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Tipos de Documentos</h3>

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
                    <a onclick="reportDocumentTypes()" role="button">Imprimir</a>
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

        <div id="page_documentTypes" class="box-block">

            <telerik:RadAjaxPanel ID="rapDocumentTypes" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocumentTypes"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgDocumentTypes_OnNeedDataSource"
                    OnInsertCommand="RgDocumentTypes_OnInsertCommand"
                    OnUpdateCommand="RgDocumentTypes_OnUpdateCommand"
                    OnDeleteCommand="RgDocumentTypes_OnDeleteCommand"
                    OnItemDataBound="RgDocumentTypes_OnItemDataBound"
                    OnPreRender="RgDocumentTypes_OnPreRender"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="TIPD_CODIGO, TIPD_CLAVE, SingLabel, TIPD_FASE_RC_G, TIPD_FASE_AD_G, TIPD_FASE_O_G, TIPD_FASE_P_G, TIPD_FASE_DR_I, TIPD_FASE_MI_I"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="20"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings AddNewRecordText="Nuevo"
                            ShowRefreshButton="True"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="TIPD_CLAVE"
                                DataField="TIPD_CLAVE"
                                HeaderText="Clave"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TIPD_NOMBRE_CORTO"
                                DataField="TIPD_NOMBRE_CORTO"
                                HeaderText="Nombre Corto"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="20%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SingLabel"
                                DataField="SingLabel"
                                HeaderText="Signo"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TIPD_DESCRIPCION"
                                DataField="TIPD_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn UniqueName="TIPD_FASE_RC_G"
                                DataField="TIPD_FASE_RC_G"
                                DataType="System.Boolean"
                                HeaderText="RC"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn UniqueName="TIPD_FASE_AD_G"
                                DataField="TIPD_FASE_AD_G"
                                DataType="System.Boolean"
                                HeaderText="AD"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn UniqueName="TIPD_FASE_O_G"
                                DataField="TIPD_FASE_O_G"
                                DataType="System.Boolean"
                                HeaderText="O"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn UniqueName="TIPD_FASE_P_G"
                                DataField="TIPD_FASE_P_G"
                                DataType="System.Boolean"
                                HeaderText="P"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn UniqueName="TIPD_FASE_DR_I"
                                DataField="TIPD_FASE_DR_I"
                                DataType="System.Boolean"
                                HeaderText="DR"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn UniqueName="TIPD_FASE_MI_I"
                                DataField="TIPD_FASE_MI_I"
                                DataType="System.Boolean"
                                HeaderText="MI"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>
                        <%--    <telerik:GridCheckBoxColumn UniqueName="TIPD_FASE_MIsinDR_I"
                                DataField="TIPD_FASE_MIsinDR_I"
                                DataType="System.Boolean"
                                HeaderText="MIsinDR"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>--%>
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
                                ConfirmText="¿ Está seguro que desea eliminar este Tipo de Documento ?</br></br>AVISO: Sólo puede eliminarse un Tipo de Documento en caso de que</br>no se haya utilizado en ningún documento."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Tipo de Documento</h3>

                                <div class="form-group form-group-fake">
                                    <%--Tipo--%>
                                    <div class="field-container field-50">
                                        <span>Tipo:</span>
                                        <telerik:RadComboBox ID="radDropType"
                                            runat="server"
                                            Width="100%"
                                            OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="Gastos" Value="G" />
                                                <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>

                                    <%--Signo--%>
                                    <div class="field-container field-50">
                                        <span>Signo:</span>
                                        <telerik:RadComboBox ID="radDropSing"
                                            runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="+" Value="+" />
                                                <telerik:RadComboBoxItem runat="server" Text="-" Value="-" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-fake">
                                    <%--Clave--%>
                                    <div class="field-container field-50">
                                        <span>Clave:</span>
                                        <telerik:RadNumericTextBox ID="txtKey"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            Value="0"
                                            MinValue="0"
                                            ShowSpinButtons="False"
                                            NumberFormat-DecimalDigits="0">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvKey"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="DocumentTypeGroup"
                                            ControlToValidate="txtKey"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la Clave del Tipo de Documento."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Nombre Corto--%>
                                    <div class="field-container field-50">
                                        <span>Nombre corto:</span>
                                        <telerik:RadTextBox ID="txtShortName"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="20"
                                            Text='<%#this.Eval("TIPD_NOMBRE_CORTO") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvShortName"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="DocumentTypeGroup"
                                            ControlToValidate="txtShortName"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el Nombre corto del Tipo de Documento."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group form-group-fake">
                                    <%--Nombre Largo--%>
                                    <div class="field-container field-100">
                                        <span>Nombre largo:</span>
                                        <telerik:RadTextBox ID="txtDescription"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="120"
                                            Text='<%#this.Eval("TIPD_DESCRIPCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="DocumentTypeGroup"
                                            ControlToValidate="txtDescription"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el Nombre largo del Tipo de Documento."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group form-group-fake">
                                    <%--Fases--%>
                                    <span>Fases:</span>

                                    <div id="checkRcDiv" class="field-container field-20">
                                        <telerik:RadCheckBox ID="checkRc"
                                            runat="server"
                                            Checked="false"
                                            Text="RC"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
                                    </div>
                                    <div id="checkAdDiv" class="field-container field-20">
                                        <telerik:RadCheckBox ID="checkAd"
                                            runat="server"
                                            Checked="false"
                                            Text="AD"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
                                    </div>

                                    <div id="checkODiv" class="field-container field-20">
                                        <telerik:RadCheckBox ID="checkO"
                                            runat="server"
                                            Checked="false"
                                            Text="O"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
                                    </div>
                                    <div id="checkPDiv" class="field-container field-20">
                                        <telerik:RadCheckBox ID="checkP"
                                            runat="server"
                                            Checked="false"
                                            Text="P"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
                                    </div>

                                    <div id="checkDrDiv" class="field-container field-20">
                                        <telerik:RadCheckBox ID="checkDr"
                                            runat="server"
                                            Checked="false"
                                            Text="DR"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
                                    </div>
                                    <div id="checkMiDiv" class="field-container field-20">
                                        <telerik:RadCheckBox ID="checkMi"
                                            runat="server"
                                            Checked="false"
                                            Text="MI"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
                                    </div>
                        <%--            <div id="checkMiWithOutDrDiv" class="field-container field-20">
                                        <telerik:RadCheckBox ID="checkMiWithOutDr"
                                            runat="server"
                                            Checked="false"
                                            Text="MIsinDr"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
                                    </div>--%>
                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="DocumentTypeGroup"
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
                $('#ctl00_ContentPlaceHolder_RgDocumentTypes_ctl00_ctl02_ctl00_InitInsertButton').hide();
            }

            function hideEntryChecks() {
                $('#checkDrDiv').hide();
                $('#checkMiDiv').hide();
                //$('#checkMiWithOutDrDiv').hide();
                $('#checkRcDiv').show();
                $('#checkAdDiv').show();
                $('#checkODiv').show();
                $('#checkPDiv').show();
            }

            function hideSpendChecks() {
                $('#checkRcDiv').hide();
                $('#checkAdDiv').hide();
                $('#checkODiv').hide();
                $('#checkPDiv').hide();
                $('#checkDrDiv').show();
                $('#checkMiDiv').show();
                //$('#checkMiWithOutDrDiv').show();
            }

            function OnClientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                var selected = item.get_value();
                switch (selected) {
                    case "G":
                        hideEntryChecks();
                        break;
                    case "I":
                        hideSpendChecks();
                        break;
                }
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

                var grid = $get("<%=this.RgDocumentTypes.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function reportDocumentTypes() {
                var typeComp = $find("<%= this.RcTypes.ClientID %>");
                var typeValue = typeComp.get_selectedItem().get_value();

                if (typeValue === "") {
                    radalert("No se pueden ver los tipos de documentos ya que el tipo esta vacío.", 330, 140, "Imposible ver Tipo de Documentos", null, null);
                    return;
                }

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenanceDocumentTypes&type=' + typeValue;

                window.open(url, "Reporte");
            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
