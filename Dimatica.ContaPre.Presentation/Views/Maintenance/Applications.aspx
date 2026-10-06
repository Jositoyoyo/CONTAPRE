<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Applications.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.Applications" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmApplications" runat="server">
        <Windows>

            <%--Aplicaciiones--%>
            <telerik:RadWindow ID="rwNewApplications"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Aplicaciones"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="510"
                Height="510"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientNewApplicationsCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_maintenance" class="container">

        <h3>Aplicaciones</h3>

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
                <div class="new">
                    <span class="icon"></span>
                    <a onclick="newApplications()" role="button">Nueva</a>
                </div>
                <div class="print">
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

        <div id="page_applications" class="box-block">

            <telerik:RadAjaxPanel ID="rapApplications" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgApplications"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgApplications_OnNeedDataSource"
                    OnUpdateCommand="RgApplications_OnUpdateCommand"
                    OnDeleteCommand="RgApplications_OnStatusCommand"
                    OnItemDataBound="RgApplications_OnItemDataBound"
                    OnPreRender="RgApplications_OnPreRender"
                    OnItemCommand="RgApplications_OnItemCommand"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="ApplicationId, Level, Description, AccountId, Active"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings AddNewRecordText="Nueva"
                            ShowRefreshButton="True"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False"
                            ShowAddNewRecordButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="Chapter"
                                DataField="Chapter"
                                HeaderText="Cap."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Article"
                                DataField="Article"
                                HeaderText="Art."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Concept"
                                DataField="Concept"
                                HeaderText="Con."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SubConcept"
                                DataField="SubConcept"
                                HeaderText="Sub."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Description"
                                DataField="Description"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="AccountNumber"
                                DataField="AccountNumber"
                                HeaderText="C.PGCP"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar aplicación"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar aplicación"
                                CommandName="DeleteAplication"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar esta Aplicación ?</br></br>AVISO: Sólo puede eliminarse una aplicación en caso de que</br>no se haya utilizado en ningún presupuesto ni documento."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                            <telerik:GridImageColumn DataType="System.String"
                                DataImageUrlFields="ActiveImage"
                                DataImageUrlFormatString="/Public/Images/{0}.png"
                                ImageAlign="Baseline"
                                ImageHeight="20px"
                                ImageWidth="20px"
                                HeaderStyle-ForeColor="White"
                                HeaderStyle-Width="40px"
                                AllowFiltering="false">
                            </telerik:GridImageColumn>
                            <telerik:GridButtonColumn UniqueName="StatusColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Cambiar estado"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                Text=" "
                                ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea cambiar el estado de esta Aplicación ?"
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template" PopUpSettings-Modal="False">
                            <FormTemplate>

                                <h3>Aplicación</h3>
                                <div class="form-group">

                                    <%--Descripción--%>
                                    <div class="field-container">
                                        <span>Descripción:</span>
                                        <telerik:RadTextBox ID="txtDescription"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="80"
                                            Text='<%#this.Eval("Description") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ApplicationGroup"
                                            ControlToValidate="txtDescription"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la descripción de la Aplicación."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div class="form-group">

                                    <%--Cuenta PGCP--%>
                                    <div class="field-container">
                                        <span>Cuenta PGCP:</span>
                                        <telerik:RadComboBox ID="radDropAccount"
                                            runat="server"
                                            Width="100%"
                                            DataTextField="DisplayLabel"
                                            DataValueField="CUEP_CODIGO">
                                        </telerik:RadComboBox>
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
                $('#ctl00_ContentPlaceHolder_RgApplications_ctl00_ctl02_ctl00_InitInsertButton').hide();
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

                var grid = $get("<%=this.RgApplications.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function newApplications() {
                var typeComp = $find("<%= this.RcTypes.ClientID %>");
                var typeValue = typeComp.get_value();

                var url = "NewApplications.aspx?type=" + typeValue;
                var manager = $find("<%= this.rwmApplications.ClientID %>");
                var oWnd = manager.open(url, "rwNewApplications");
            }

            function OnClientNewApplicationsCloseHandler(sender, args) {
                location.reload(true);
            }

            function reportApplications() {
                var typeComp = $find("<%= this.RcTypes.ClientID %>");
                var typeValue = typeComp.get_selectedItem().get_value();

                if (typeValue === "") {
                    radalert("No se pueden ver las aplicaciones ya que el tipo de aplicación esta vacío.", 330, 140, "Imposible ver Aplicaciones", null, null);
                    return;
                }

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=maintenanceApplications&type=' + typeValue;

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
