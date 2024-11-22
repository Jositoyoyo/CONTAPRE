<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ExtraBudgetaryApplications.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.ExtraBudgetaryApplications" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Aplicaciones Extrapresupuestarias</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="new main-button">
                    <span class="icon"></span>
                </div>
                <div class="report">
                    <span class="icon"></span>
                    <a onclick="reportApplications()" role="button">Imprimir</a>
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

        <div id="page_extraBudgetaryApplications" class="box-block">

            <telerik:RadAjaxPanel ID="rapExtraBudgetaryApplications" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgExtraBudgetaryApplications"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgExtraBudgetaryApplications_OnNeedDataSource"
                    OnInsertCommand="RgExtraBudgetaryApplications_OnInsertCommand"
                    OnUpdateCommand="RgExtraBudgetaryApplications_OnUpdateCommand"
                    OnDeleteCommand="RgExtraBudgetaryApplications_OnDeleteCommand"
                    OnItemDataBound="RgExtraBudgetaryApplications_OnItemDataBound"
                    OnPreRender="RgExtraBudgetaryApplications_OnPreRender"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="EXTRAPRE_CODIGO, EXTRAPRE_NUMERO, CUEP_CODIGO"
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
                            <telerik:GridBoundColumn UniqueName="EXTRAPRE_NUMERO"
                                DataField="EXTRAPRE_NUMERO"
                                HeaderText="Clave"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXTRAPRE_DESCRIPCION"
                                DataField="EXTRAPRE_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CUEP_NUMERO"
                                DataField="CUEP_NUMERO"
                                HeaderText="Cuenta CPGP"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar Aplicacion"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Aplicacion"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar esta Aplicación Extrapresupuestaria ?</br></br>AVISO: Sólo puede eliminarse una Aplicación Extrapresupuestaria en caso de que</br>no se haya utilizado en ningún expediente."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Cuenta</h3>
                                <div class="form-group">

                                    <%--Clave--%>
                                    <div class="field-container field-20">
                                        <span>Clave:</span>
                                        <telerik:RadNumericTextBox ID="txtNumber"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            Value="0"
                                            MinValue="0"
                                            ShowSpinButtons="true"
                                            NumberFormat-DecimalDigits="0">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvNumber"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ApplicationGroup"
                                            ControlToValidate="txtNumber"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la clave de la Aplicación Extrapresupuestaria."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Cuenta--%>
                                    <div class="field-container field-80">
                                        <span>Cuenta PGCP:</span>
                                        <telerik:RadComboBox ID="radDropAccount"
                                            runat="server"
                                            Width="100%"
                                            DataTextField="DisplayLabel"
                                            DataValueField="CUEP_CODIGO">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>

                                <div class="form-group">

                                    <%--Descripcion--%>
                                    <div class="field-container">
                                        <span>Descripción:</span>
                                        <telerik:RadTextBox ID="txtDescription"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("EXTRAPRE_DESCRIPCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ApplicationGroup"
                                            ControlToValidate="txtDescription"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la descripción de la Aplicación Extrapresupuestaria."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="ApplicationGroup"
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
                $('#ctl00_ContentPlaceHolder_RgExtraBudgetaryApplications_ctl00_ctl02_ctl00_InitInsertButton').hide();
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

                var grid = $get("<%=this.RgExtraBudgetaryApplications.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function reportApplications() {
                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenanceExtraBudgetaryApplications';

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
