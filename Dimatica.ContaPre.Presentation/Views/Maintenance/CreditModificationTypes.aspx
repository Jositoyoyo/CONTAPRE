<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="CreditModificationTypes.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.CreditModificationTypes" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Tipos Modificación de Crédito</h3>

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
                    <a onclick="reportCreditModificationTypes()" role="button">Imprimir</a>
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

        <div id="page_creditModificationTypes" class="box-block">

            <telerik:RadAjaxPanel ID="rapCreditModificationTypes" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgCreditModificationTypes"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgCreditModificationTypes_OnNeedDataSource"
                    OnInsertCommand="RgCreditModificationTypes_OnInsertCommand"
                    OnUpdateCommand="RgCreditModificationTypes_OnUpdateCommand"
                    OnDeleteCommand="RgCreditModificationTypes_OnDeleteCommand"
                    OnItemDataBound="RgCreditModificationTypes_OnItemDataBound"
                    OnPreRender="RgCreditModificationTypes_OnPreRender"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="TIPM_CODIGO, TIPM_NUMERO"
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
                            <telerik:GridBoundColumn UniqueName="TIPM_NUMERO"
                                DataField="TIPM_NUMERO"
                                HeaderText="Número"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TIPM_DESCRIPCION"
                                DataField="TIPM_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar tipo de modificación"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar tipo de modificación"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar este Tipo de Modificación de Crédito ?</br></br>AVISO: Sólo puede eliminarse un Tipo de Modificación de Crédito en caso de que</br>no se haya utilizado en ningún expediente de modificación."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Créditos</h3>
                                <div class="form-group form-group-fake">

                                    <%--Tipo--%>
                                    <div class="field-container field-80">
                                        <span>Tipo:</span>
                                        <telerik:RadComboBox ID="radDropType"
                                            runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="Gastos" Value="G" />
                                                <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>

                                    <%--Numero--%>
                                    <div class="field-container field-20">
                                        <span>Número:</span>
                                        <telerik:RadNumericTextBox ID="txtNumber"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            Value="0"
                                            MinValue="0"
                                            ShowSpinButtons="False"
                                            NumberFormat-DecimalDigits="0">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvNumber"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="CreditModificactionGroup"
                                            ControlToValidate="txtNumber"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el Número del Tipo de Modificación de Crédito."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div class="form-group">

                                    <%--Descripción--%>
                                    <div class="field-container">
                                        <span>Descripción:</span>
                                        <telerik:RadTextBox ID="txtDescription"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("TIPM_DESCRIPCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="CreditModificactionGroup"
                                            ControlToValidate="txtDescription"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la descripción del Tipo de Modificación de Crédito."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="CreditModificactionGroup"
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
                $('#ctl00_ContentPlaceHolder_RgCreditModificationTypes_ctl00_ctl02_ctl00_InitInsertButton').hide();
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

                var grid = $get("<%=this.RgCreditModificationTypes.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function reportCreditModificationTypes() {
                var typeComp = $find("<%= this.RcTypes.ClientID %>");
                var typeValue = typeComp.get_selectedItem().get_value();

                if (typeValue === "") {
                    radalert("No se pueden ver los tipos de modificaciones de crédito ya que el tipo esta vacío.", 330, 140, "Imposible ver Tipos de Modif.", null, null);
                    return;
                }

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenanceCreditModificationTypes&type=' + typeValue;

                window.open(url, "Reporte");
            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
