<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="SeeTonnageSheets.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Treasury.SeeTonnageSheets" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_treasury" class="container">

        <h3>Ver Hoja Arqueo</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_Click"
                        Text="Buscar">
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div id="page_seeTonnageSheets" class="box-block">
            <telerik:RadAjaxPanel ID="rapSeeTonnageSheets"
                runat="server"
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
                    Style="z-index: 100000">
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
                                            <span>Año:</span>
                                            <telerik:RadMonthYearPicker ID="RmyExerciseYear"
                                                runat="server"
                                                Width="100px"
                                                AutoPostBack="False"
                                                EnableTyping="True"
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
                                            <span>Ordinal Pagador:</span>
                                            <telerik:RadComboBox ID="RcAccounts"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="DisplayDescriptionLabel"
                                                DataValueField="CUE_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>N⁰ Hoja Arqueo entre: </span>
                                            <telerik:RadNumericTextBox ID="RntSinceSheetNumber"
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
                                            <span>Y:</span>
                                            <telerik:RadNumericTextBox ID="RntUntilSheetNumber"
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
                                            <span>Fecha entre:</span>
                                            <telerik:RadDatePicker ID="RdpSinceDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>

                                        <div class="field-container">
                                            <span>y:</span>
                                            <telerik:RadDatePicker ID="RdpUntilDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgTonnageSheet"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgTonnageSheet_NeedDataSource"
                    OnInsertCommand="RgTonnageSheet_InsertCommand"
                    OnUpdateCommand="RgTonnageSheet_UpdateCommand"
                    OnDeleteCommand="RgTonnageSheet_DeleteCommand"
                    OnItemDataBound="RgTonnageSheet_ItemDataBound"
                    OnItemCommand="RgTonnageSheet_OnItemCommand"
                    OnPreRender="RgTonnageSheet_PreRender"
                    CssClass="seeTonnageSheet-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="HOJ_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings AddNewRecordText="Nuevo"
                            ShowAddNewRecordButton="false"
                            ShowRefreshButton="True"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="HOJ_NUMERO"
                                DataField="HOJ_NUMERO"
                                HeaderText="N⁰ Hoja"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="HOJ_ANO"
                                DataField="HOJ_ANO"
                                HeaderText="Año"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CUE_ORDINAL_LABEL"
                                DataField="CUE_ORDINAL_LABEL"
                                HeaderText="Ordinal Pagador"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="HOJ_FECHA"
                                DataField="HOJ_FECHA"
                                HeaderText="Fecha"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerOperation"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedOperation"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"HOJ_FECHA") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                        runat="server">
                                        <script id="scriptOperation"
                                            type="text/javascript">
                                            function DateSelectedOperation(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateOperation(sender);
                                                tableView.filter("HOJ_FECHA", date, "GreaterThanOrEqualTo");
                                            }
                                            function FormatSelectedDateOperation(picker) {
                                                var date = picker.get_selectedDate();
                                                var dateInput = picker.get_dateInput();
                                                var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());

                                                return formattedDate;
                                            }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn UniqueName="DET_IMPORTE"
                                DataField="DET_IMPORTE"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="150px" />
                            </telerik:GridNumericColumn>
                           <%-- <telerik:GridBoundColumn UniqueName="DET_IMPORTE_LABEL"
                                DataField="DET_IMPORTE_LABEL"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <%--               <telerik:GridTemplateColumn UniqueName="HOJ_ARQUEO50"
                                AllowFiltering="false"
                                HeaderText="H. Arqueo 50"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="check orange">
                                <ItemTemplate>
                                    <asp:Label runat="server" Visible="true">
                                        <i class="far fa-check-circle"></i>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="40%" />
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridImageColumn DataType="System.String"
                                DataImageUrlFields="Sheet50Image"
                                DataImageUrlFormatString="/Content/Images/{0}.png"
                                HeaderText="H.A.50"
                                ImageAlign="Middle"
                                ImageHeight="18px"
                                ImageWidth="18px"
                                HeaderStyle-ForeColor="White"
                                HeaderStyle-Width="40px"
                                AllowFiltering="false">
                            </telerik:GridImageColumn>
                            <%--         <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>--%>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar"
                                CommandName="UpdateTonnageSheet"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>
                            </FormTemplate>
                        </EditFormSettings>

                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>

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
        </div>

    </div>
</asp:Content>
