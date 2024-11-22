<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="TreasuryLines.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.TreasuryLines" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Líneas de Tesorería</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="new main-button">
                    <span class="icon"></span>
                </div>
                <div class="report">
                    <span class="icon"></span>
                    <a onclick="reportTreasuryLines()" role="button">Imprimir</a>
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

        <div id="page_treasuryLines" class="box-block">

            <telerik:RadAjaxPanel ID="rapTreasuryLines" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgTreasuryLines"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgTreasuryLines_OnNeedDataSource"
                    OnInsertCommand="RgTreasuryLines_OnInsertCommand"
                    OnUpdateCommand="RgTreasuryLines_OnUpdateCommand"
                    OnDeleteCommand="RgTreasuryLines_OnDeleteCommand"
                    OnItemDataBound="RgTreasuryLines_OnItemDataBound"
                    OnPreRender="RgTreasuryLines_OnPreRender"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="LIN_NUMERO, LIN_ORIGEN_CODIGO"
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
                            <telerik:GridBoundColumn UniqueName="LIN_NUMERO"
                                DataField="LIN_NUMERO"
                                HeaderText="Clave"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="LIN_DESCRIPCION"
                                DataField="LIN_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="LIN_ORIGEN_DESCRIPCION"
                                DataField="LIN_ORIGEN_DESCRIPCION"
                                HeaderText="Origen"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="LIN_ORIGEN_CODIGO"
                                DataField="LIN_ORIGEN_CODIGO"
                                HeaderText="Cód. Origen"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar Línea de Tesorería"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Línea de Tesorería"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar esta Línea de Tesorería ?</br></br>AVISO: Sólo puede eliminarse una Línea de Tesorería en caso de que</br>no se haya utilizado en ningún apunte de tesorería ni hoja de arqueo."
                                
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Línea de tesorería</h3>
                                <div class="form-group form-group-fake">

                                    <%--Clave--%>
                                    <div class="field-container field-30">
                                        <span>Clave:</span>
                                        <telerik:RadNumericTextBox ID="txtId"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            Value="0"
                                            MinValue="0"
                                            ShowSpinButtons="False"
                                            NumberFormat-DecimalDigits="0"
                                            ReadOnly="<%# (!(Container is GridEditFormInsertItem)) %>">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvId"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="TreasuryLinesGroup"
                                            ControlToValidate="txtId"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la clave de la Línea de Tesorería."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Descripcion--%>
                                    <div class="field-container field-70">
                                        <span>Línea de Tesorería:</span>
                                        <telerik:RadTextBox ID="txtDescription"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("LIN_DESCRIPCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="TreasuryLinesGroup"
                                            ControlToValidate="txtDescription"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la descripción de la Línea de Tesorería."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group form-group-fake">

                                    <%--Clave Origen--%>
                                    <div class="field-container field-30">
                                        <span>Clave Origen:</span>
                                        <telerik:RadNumericTextBox ID="txtOriginCode"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            Value="0"
                                            MinValue="0"
                                            ShowSpinButtons="False"
                                            NumberFormat-DecimalDigits="0">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvOriginCode"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="TreasuryLinesGroup"
                                            ControlToValidate="txtOriginCode"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la clave del origen de la Línea de Tesorería."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Origen--%>
                                    <div class="field-container field-70">
                                        <span>Origen:</span>
                                        <telerik:RadTextBox ID="txtOrigin"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("LIN_ORIGEN_DESCRIPCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvOrigin"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="TreasuryLinesGroup"
                                            ControlToValidate="txtOrigin"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el origen de la Línea de Tesorería."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="TreasuryLinesGroup"
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
                $('#ctl00_ContentPlaceHolder_RgTreasuryLines_ctl00_ctl02_ctl00_InitInsertButton').hide();
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

                var grid = $get("<%=this.RgTreasuryLines.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function reportTreasuryLines() {
                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenanceTreasuryLines';

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
