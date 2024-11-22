<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Programs.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.Programs" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Programas</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="new main-button">
                    <span class="icon"></span>
                </div>
                <div class="report">
                    <span class="icon"></span>
                    <a onclick="reportPrograms()" role="button">Imprimir</a>
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

        <div id="page_programs" class="box-block">

            <telerik:RadAjaxPanel ID="rapPrograms" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgPrograms"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgPrograms_OnNeedDataSource"
                    OnInsertCommand="RgPrograms_OnInsertCommand"
                    OnUpdateCommand="RgPrograms_OnUpdateCommand"
                    OnDeleteCommand="RgPrograms_OnDeleteCommand"
                    OnItemDataBound="RgPrograms_OnItemDataBound"
                    OnPreRender="RgPrograms_OnPreRender"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="PRO_CODIGO, PRO_POR_DEFECTO"
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
                            <telerik:GridBoundColumn UniqueName="PRO_NUMERO"
                                DataField="PRO_NUMERO"
                                HeaderText="Número"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PRO_DESCRIPCION"
                                DataField="PRO_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn UniqueName="PRO_POR_DEFECTO"
                                DataField="PRO_POR_DEFECTO"
                                DataType="System.Boolean"
                                HeaderText="Por Defecto"
                                StringFalseValue="False"
                                StringTrueValue="True"
                                AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar Programa"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Programa"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar este Programa ?</br></br>AVISO: Sólo puede eliminarse un Programa en caso de que</br>no se haya utilizado en ningún presupusto o expediente."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Programa</h3>
                                <div class="form-group form-group-fake">

                                    <%--Número--%>
                                    <div class="field-container field-20">
                                        <span>Número:</span>
                                        <telerik:RadTextBox ID="txtNumber"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="10"
                                            Text='<%#this.Eval("PRO_NUMERO") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvNumber"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="PayFormGroup"
                                            ControlToValidate="txtNumber"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el número del Programa."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Por Defecto--%>
                                    <div class="field-container field-20">
                                        <span>Por Defecto:</span>
                                        <telerik:RadCheckBox ID="checkDefault"
                                            runat="server"
                                            Checked="false"
                                            AutoPostBack="false">
                                        </telerik:RadCheckBox>
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
                                            Text='<%#this.Eval("PRO_DESCRIPCION") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="PayFormGroup"
                                            ControlToValidate="txtDescription"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la descripción del Programa."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="PayFormGroup"
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
                $('#ctl00_ContentPlaceHolder_RgPrograms_ctl00_ctl02_ctl00_InitInsertButton').hide();
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

                var grid = $get("<%=this.RgPrograms.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function reportPrograms() {
                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenancePrograms';

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
