<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="NewIncomes.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Budget.NewIncomes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            font-family: "Source Sans Pro", sans-serif;
            font-size: 16px;
        }

        .form-group {
            margin-bottom: .625rem;
        }

            .form-group > span {
                margin-bottom: .3125rem;
                display: block;
            }

        .buttons {
            justify-content: flex-end;
            display: flex;
        }

            .buttons button.RadButton {
                background: #8a1715;
                color: white;
                display: block;
                width: auto;
                min-width: 120px;
                text-align: center;
                font-weight: 300;
                font-size: 14px !important;
                padding: .625rem .625rem .625rem .8725rem;
                margin: 0 .625rem 0 .625rem;
                border-radius: 4px;
                border: none;
                cursor: pointer;
            }
                .buttons button.RadButton:last-child {
                    margin-right:0;
                }

            .buttons button.rbHovered {
                background-color: #3d3e47 !important;
                color: white !important;
            }

        .rgMasterTable {
            border: none;
            border-collapse: collapse;
        }

            .rgMasterTable tr th.rgHeader {
                border-bottom: 1px solid #ddd;
                background: #3d3e47;
                color: white;
            }

                .rgMasterTable tr th.rgHeader:first-child a {
                    font-size: 12px;
                    color: white;
                    line-height: 110%;
                    display: block;
                }

            .rgMasterTable tr.rgFilterRow,
            .rgMasterTable tr.rgRow,
            .rgMasterTable tr.rgAltRow {
                background: white;
            }

                .rgMasterTable tr.rgFilterRow > td,
                .rgMasterTable tr.rgRow > td,
                .rgMasterTable tr.rgAltRow > td {
                    border: none !important;
                }

            .rgMasterTable td:last-child {
                padding-right: 1.25rem;
            }

        .riSelect {
            width: 20px;
            right: 1px;
        }

            .riSelect .riUp,
            .riSelect .riDown {
                width: 20px;
                background-color: transparent;
                border: 0;
            }

        html body .RadInput_Metro .riTextBox,
        html body .RadInputMgr_Metro {
            border: 1px solid #3d3e47 !important; /*IE11*/
        }

        .hiddenText {
            visibility: hidden !important;
        }

        .monthCellClass {
            display: none;
        }

            .monthCellClass .rcSelected {
                display: none;
            }

            td[id*="rcMView_ene"], td[id*="rcMView_feb"], td[id*="rcMView_mar"], td[id*="rcMView_abr"], td[id*="rcMView_may"], td[id*="rcMView_jun"], td[id*="rcMView_jul"], td[id*="rcMView_ago"], td[id*="rcMView_sep"], td[id*="rcMView_oct"], td[id*="rcMView_nov"], td[id*="rcMView_dic"] {
                display: none !important;
            }
    </style>
    <style>
        .RadCalendarFastNavPopup {
            padding: 0;
        }

        table.RadCalendarMonthView {
            font-family: "Source Sans Pro", sans-serif!important;
            font-size: 14px!important;
            border: 1px solid #3d3e47!important;
        }

            table.RadCalendarMonthView a {
                padding: 0;
            }

            table.RadCalendarMonthView td {
                padding: .3125rem 0;
            }

                table.RadCalendarMonthView td a[id*='NavigationPrevLink'],
                table.RadCalendarMonthView td a[id*='NavigationNextLink'] {
                    background-repeat: no-repeat;
                    background-size: 100%;
                    background-position: 0 0;
                    display: block;
                    background-size: 50%;
                    background-position: center -40px;
                    text-indent: -9999px;
                }

                table.RadCalendarMonthView td a[id*='NavigationPrevLink'] {
                    background-image: url(/Content/Images/sprite-arrow-left.png);
                }

                table.RadCalendarMonthView td a[id*='NavigationNextLink'] {
                    background-image: url(/Content/Images/sprite-arrow-right.png);
                }

                table.RadCalendarMonthView td.rcSelected {
                    background-color: #8a1715;                    
                }

                table.RadCalendarMonthView td.rcSelected a {
                        border: none;
                        background: transparent;
                        padding: 0;
                    }

                table.RadCalendarMonthView td:hover {
                    background-color: #8a1715;   
                }

                 table.RadCalendarMonthView td.rcButtons {
                    padding: .625rem;
                }
                
                 table.RadCalendarMonthView td.rcButtons a,
                 table.RadCalendarMonthView td.rcButtons input {
                        background: #3d3e47; 
                        color: white;
                        display: block;
                        width: auto;
                        text-align: center;
                        font-size: 14px;
                        font-weight: 300;
                        border-radius: 4px;
                        border: none;
                        cursor: pointer;
                        min-width: 0;
                        padding: .3125rem;
                        float: left;
                        margin: 0 .3125rem 0 0;                        
                    }

                    table.RadCalendarMonthView td.rcButtons input:last-child {
                    margin-right: 0;
                }

                    table.RadCalendarMonthView td.rcButtons:hover {
                        background-color: transparent;
                    }

            table.RadCalendarMonthView td:not([class]) a { 
                    width: 40px;
                    height: 20px;
            }

            table.RadCalendarMonthView td:not([class]):last-child {
                float: right;
            }
    </style>
    <style>
        .RadPicker a.rcCalPopup,
        .RadPicker a.rcTimePopup{
            background-repeat: no-repeat!important;
            background-size: 100%!important;
            background-position: 0 -44px!important;
            display: block!important;
        }
        .RadPicker a.rcCalPopup:hover, .RadPicker a.rcCalPopup:active,
        .RadPicker a.rcTimePopup:hover, .RadPicker a.rcTimePopup:active{
            background-position: 0 0!important;
        }

        .RadPicker .rcCalPopup {
            background-image: url(/Content/images/sprite-calendario.png)!important;
            background-color: transparent!important;
            border: none !important;            
        }
            .RadPicker .rcCalPopup::before {
                content: none!important;
            }

        .RadPicker .rcSelect {
            position: static;
            overflow: visible;
            margin-left: .625rem;            
        }
            .RadPicker .rcSelect a.rcCalPopup {
                background-color: white;               
            }
                .RadPicker .rcSelect a.rcCalPopup:hover, .RadPicker .rcSelect a.rcCalPopup:active {
                    background-position: center -52px!important;
                    background-color: white!important;
                }

        .RadPicker .rcTimePopup {
            background-image: url(/Content/images/iconos-aena/sprite-reloj.png);
            border: none;            
        }
            .RadPicker .rcTimePopup::before {
                content: none!important;
            }

        .RadPicker .RadInput {
            display: flex !important;
            align-items: center;
        }
        
            .RadPicker .RadInput a {
                background-color: transparent!important;
                width: 26px!important;
                height: 26px!important;
            }

            .RadPicker .RadInput .riTextBox {
                font-family: "Source Sans Pro", sans-serif!important;
                font-size: 14px!important;
                border: 1px solid #3d3e47!important;
                float: none!important;
                padding: .3125rem!important;
                line-height: 120%!important;                
            }

                .RadPicker .RadInput .riTextBox:hover {
                    background: transparent;
                }

        .RadPicker .t-ie .RadInput,
        .RadPicker .t-ie .RadInputMgr {
            height: auto!important;
        }
    </style>
    <style>
         .RadWindow .rwWindowContent .rwDialogPopup div a,
         .RadWindow .rwWindowContent .rwDialogPopup div a:hover {
	         background: #8a1715;
	         color: white;
	         display: block;
	         width: auto;
	         min-width: 120px;
	         text-align: center;
	         font-size: 14px;
	         font-weight: 300;
	         padding: 0.625rem 0.625rem 0.625rem 0.8725rem;
	         border-radius: 4px;
	         border: none;
	         cursor: pointer;
	         -moz-transition: all 0.2s;
	         -o-transition: all 0.2s;
	         -webkit-transition: all 0.2s;
	         transition: all 0.2s;
        }
         .RadWindow .rwWindowContent .rwDialogPopup div a:hover {
	         color: white;
	         background: #3d3e47;
	         border: none;
        }
         .RadWindow {
	         border: 1px solid #3d3e47;
	         width: auto !important;
	         height: auto !important;
	         padding: 0 !important;
        }
         .RadWindow .rwCorner, .RadWindow .rwFooterRow, .RadWindow .rwTopResize {
	         display: none !important;
        }
         .RadWindow .rwTitlebarControls, .RadWindow .rwTitleWrapper {
	         background-color: #3d3e47 !important;
        }
         .RadWindow .rwTitlebarControls, .RadWindow .rwTitleWrapper {
	         padding: 0.625rem !important;
        }
         .RadWindow .rwTitlebarControls .rwIcon, .RadWindow .rwTitleWrapper .rwIcon {
	         display: none !important;
        }
         .RadWindow .rwTitlebarControls .rwIcon::before, .RadWindow .rwTitleWrapper .rwIcon::before {
	         content: "" !important;
        }
         .RadWindow .rwTitlebarControls tr:first-child td:first-child {
	         display: none !important;
        }
         .RadWindow .rwTitlebarControls tr:first-child td em {
	             padding-left: .625rem;
        }
         .RadWindow .rwWindowContent .rwDialogPopup {
	         background-image: none !important;
	         padding: 0 !important;
        }
         .RadWindow .rwWindowContent .rwDialogPopup .rwDialogText {
	         font-size: 14px !important;
	         margin: 0 0 1.875rem !important;
        }
         .RadWindow .rwWindowContent .rwDialogPopup div {
	         display: flex;
	         justify-content: space-around;
	         margin: 0.625rem 0;
        }
         .RadWindow .rwWindowContent .rwDialogPopup div a {
	         height: auto;
	         margin: 0 !important;
        }
         .RadWindow .rwWindowContent .rwDialogPopup div a .rwInnerSpan, .RadWindow .rwWindowContent .rwDialogPopup div a .rwOuterSpan {
	         float: none !important;
	         color: white;
        }
         .RadWindow .rwWindowContent .rwDialogPopup div a:hover {
	         background-color: #8a1715;
        }
         .RadWindow.tabla-presupuestos {
	         width: 900px !important;
	         border: 0 !important;
        }
         .RadWindow .rwTable {
	         height: auto !important;
        }
         .RadWindow .rwTitleWrapper {
	         height: auto !important;
        }
         .RadWindow .rwTitleWrapper .rwIcon {
	         display: none !important;
        }
         .RadWindow .rwTitleWrapper .rwTitle {
	         font-size: 14px !important;
	         padding: 0 0 0 0.625rem !important;
	         width: auto !important;
	         text-transform: uppercase !important;
        }
         .RadWindow .rwTitleBar {
	         background-color: #3d3e47 !important;
	         width: 100% !important;
	         margin: 0 !important;
        }
         .RadWindow .rwCommands {
	         float: none !important;
	         border: none !important;
	         margin: 0 !important;
	         top: 0.625rem !important;
	         right: 0.625rem !important;
        }
         .RadWindow .rwCloseButton {
	         width: 20px !important;
	         height: 20px !important;
	         border: none !important;
	         background: url(/Content/images/sprite-close.png) !important;
	         background-size: 100% !important;
	         background-position: 0 -20px !important;
	         background-repeat: no-repeat !important;
        }
         .RadWindow .rwCloseButton::before {
	         content: "" !important;
        }
    </style>
    <style>
        .monto-total {
            text-align: right;
            margin:.625rem 66px 1.25rem 0;
        }

            .monto-total strong {
                vertical-align: middle;
                margin-right: .3125rem;
            }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmNewIncomes"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmNewIncomes"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmNewIncomes" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralNewIncomes"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapNewIncomes"
            runat="server"
            LoadingPanelID="ralNewIncomes">

            <div class="form-group">
                <strong>Año:</strong>
                <telerik:RadMonthYearPicker ID="RmyDates"
                    runat="server"
                    Width="70px"
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

            <div class="form-group">
                
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgNewIncomes"
                    runat="server"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgNewIncomes_OnNeedDataSource"
                    OnItemDataBound="RgNewIncomes_OnItemDataBound"
                    Height="500px">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="False"
                        AllowFilteringByColumn="False"
                        DataKeyNames="Level, Chapter, Article, Concept, SubConcept"
                        CommandItemDisplay="None"
                        AllowSorting="False"
                        AllowPaging="False"
                        PagerStyle-AlwaysVisible="False"
                        NoMasterRecordsText="No Hay datos a Mostrar.">

                        <CommandItemSettings ShowExportToExcelButton="False"
                            ShowAddNewRecordButton="False"
                            ShowRefreshButton="false"
                            ShowExportToPdfButton="false" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="Chapter"
                                DataField="Chapter"
                                HeaderText="Cap."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="35px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Article"
                                DataField="Article"
                                HeaderText="Art."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="35px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Concept"
                                DataField="Concept"
                                HeaderText="Con."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="35px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SubConcept"
                                DataField="SubConcept"
                                HeaderText="Sub."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="35px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Description"
                                DataField="Description"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="SubAmount"
                                DataField="Amount"
                                HeaderText="Por Subcon.">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtSubAmount"
                                        runat="server"
                                        RenderMode="Lightweight"
                                        Width="100%"
                                        Value='<%#Convert.ToDouble(this.Eval("Amount")) %>'
                                        EmptyMessage=""
                                        ShowSpinButtons="False"
                                        AutoPostBack="False"
                                        CssClass="hiddenText"
                                        Culture="es-ES">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="ConAmount"
                                DataField="Amount"
                                HeaderText="Por Con.">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtConAmount"
                                        runat="server"
                                        RenderMode="Lightweight"
                                        Width="100%"
                                        Value='<%#Convert.ToDouble(this.Eval("Amount")) %>'
                                        EmptyMessage=""
                                        ShowSpinButtons="False"
                                        AutoPostBack="False"
                                        CssClass="hiddenText"
                                        Culture="es-ES">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="ChaArtAmount"
                                DataField="Amount"
                                HeaderText="Por Cap. y Art.">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtChaArtAmount"
                                        runat="server"
                                        RenderMode="Lightweight"
                                        Width="100%"
                                        Value='<%#Convert.ToDouble(this.Eval("Amount")) %>'
                                        EmptyMessage=""
                                        ShowSpinButtons="False"
                                        AutoPostBack="False"
                                        CssClass="hiddenText"
                                        Culture="es-ES">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="AmountSub"
                                DataField="AmountSub"
                                HeaderText="Por Subcon."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="AmountCon"
                                DataField="AmountCon"
                                HeaderText="Por Conc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="AmountChaArt"
                                DataField="AmountChaArt"
                                HeaderText="Por Cap. y Art."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="NotBinding"
                                DataField="NotBinding"
                                HeaderText="No Vinc.">
                                <ItemTemplate>
                                    <telerik:RadCheckBox ID="checkNotBinding"
                                        runat="server"
                                        Checked='<%#this.Eval("NotBinding") %>'
                                        Text=""
                                        AutoPostBack="false">
                                    </telerik:RadCheckBox>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>

                <div class="monto-total">
                    <strong>Total:</strong>
                    <telerik:RadTextBox ID="txtTotal"
                        Width="100px"
                        runat="server"
                        MaxLength="80"
                        Text="0,00"
                        Enabled="False">
                    </telerik:RadTextBox>
                </div>
            </div>

            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnApplications"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnApplicationsClientClicked"
                    Text="Aplicaciones"
                    AutoPostBack="True">
                </telerik:RadButton>
                <%--<telerik:RadButton ButtonType="LinkButton" ID="btnUpdate"
                    runat="server"
                    RenderMode="Native"
                    OnClick="btnUpdate_OnClick"
                    Text="Modificar"
                    AutoPostBack="True"
                    Visible="False">
                </telerik:RadButton>--%>
                <telerik:RadButton ButtonType="LinkButton" ID="btnCalculate"
                    runat="server"
                    RenderMode="Native"
                    OnClick="btnCalculate_OnClick"
                    Text="Calcular Totales"
                    AutoPostBack="True">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                    runat="server"
                    RenderMode="Native"
                    OnClick="btnSave_OnClick"
                    Text="Grabar"
                    AutoPostBack="True">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnCancel"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnCloseClientClicked"
                    Text="Cancelar"
                    AutoPostBack="False">
                </telerik:RadButton>
            </div>
        </telerik:RadAjaxPanel>
    </form>

    <telerik:RadScriptBlock runat="server">
        <script>

            //function PreventPostback(sender, eventArgs) {
            //    var asd = eventArgs.get_newValue();
            //}

            function CloseWindows(response) {
                var wnd = GetRadWindow();
                if (wnd) {
                    wnd.close(response);
                }
            }

            function OnApplicationsClientClicked(sender, eventArgs) {
                CloseWindows(2);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                radconfirm("No se ha salvado ningún valor en la BD. ¿ Está seguro que desea salir de todas maneras ?",
                    confirmCallBackFn,
                    330,
                    140,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                CloseWindows(null);
            }

            function OnEmptyValues(response) {
                radalert("Imposible crear el Presupuesto en cuestión debido a que todos los valores se encuetran en 0.", 330, 140, "Valores en 0", null, null);
            }

            function OnSaveSuccess(response) {
                switch (response) {
                    case -1:
                        radalert("Debe completar todos los datos del Presupuesto.", 330, 140, "Error insertando el presupuesto", null, null);
                    break;
                    case 0:
                        radalert("Ya existe un Presupuesto de Ingreso para el año seleccionado.", 330, 140, "Presupuesto existente", null, null);
                    break;
                    case 1:
                        CloseWindows(response);
                    break;
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) {
                    oWindow = window.radWindow;
                }
                else if (window.frameElement.radWindow) {
                    oWindow = window.frameElement.radWindow;
                }

                return oWindow;
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
