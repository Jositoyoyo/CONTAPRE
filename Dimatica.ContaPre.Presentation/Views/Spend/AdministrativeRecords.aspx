<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="AdministrativeRecords.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.AdministrativeRecords" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_spend" class="container">

        <h3>Expedientes Administrativos</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_OnClick"
                        Text="Buscar"></asp:LinkButton>
                </div>
                <div class="new">
                    <span class="icon"></span>
                    <a href="ManageAdministrativeRecord.aspx" role="button">Nuevo</a>
                </div>
            </div>
        </div>

        <div id="page_administrativeRecords" class="box-block">

            <telerik:RadAjaxPanel ID="rapAdministrativeRecords" runat="server"
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
                                    <div class="form-group form-group-fake">
                                        <div class="field-container">
                                            <span>Año Ejer.:</span>
                                            <telerik:RadMonthYearPicker ID="RmyExerciseYear"
                                                runat="server"
                                                Width="100px"
                                                AutoPostBack="False"
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
                                        </div>

                                        <div class="field-container">
                                            <span>N⁰ Exped. Admin.:</span>
                                            <telerik:RadNumericTextBox ID="RntNumber"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="75px"
                                                MinValue="0"
                                                MaxValue="9999"
                                                MaxLength="4"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Procedencia Gasto:</span>
                                            <telerik:RadComboBox ID="RcProvenances"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="PROC_DESCRIPCION"
                                                DataValueField="PROC_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Descripción:</span>
                                            <telerik:RadTextBox ID="RtbDescription"
                                                Width="100%"
                                                runat="server"
                                                MaxLength="60"
                                                Text="">
                                            </telerik:RadTextBox>
                                        </div>
                                     </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgRecords"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgRecords_OnNeedDataSource"
                    OnPreRender="RgRecords_OnPreRender"
                    OnItemCommand="RgRecords_OnItemCommand"
                    CssClass="incomeDrRecords-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="EA_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed">

                        <CommandItemSettings ShowRefreshButton="False"
                            ShowExportToExcelButton="False"
                            ShowExportToPdfButton="False"
                            ShowAddNewRecordButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="EA_NUMERO"
                                DataField="EA_NUMERO"
                                HeaderText="Núm."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EA_ANO_EJERCICIO"
                                DataField="EA_ANO_EJERCICIO"
                                HeaderText="Año Ejer."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PROC_DESCRIPCION"
                                DataField="PROC_DESCRIPCION"
                                HeaderText="Procedencia"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TIPC_DESCRIPCION"
                                DataField="TIPC_DESCRIPCION"
                                HeaderText="Tipo Contrato"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EA_DESCRIPCION"
                                DataField="EA_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EA_CODIGO"
                                DataField="EA_CODIGO"
                                HeaderText="Clave"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar expediente administrativo"
                                CommandName="UpdateAdministrativeRecord"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>
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

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
