<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="UpdateExtraBudgetaryDiscounts.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.UpdateExtraBudgetaryDiscounts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="~/Content/Contapre.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <title></title>
    <style>
        .buttons {
            justify-content: flex-end;
            display: flex;
        }

        form {
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmUpdateExtraBudgetaryDiscounts"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmUpdateExtraBudgetaryDiscounts"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmUpdateExtraBudgetaryDiscounts" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralUpdateExtraBudgetaryDiscounts"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapUpdateExtraBudgetaryDiscounts"
            runat="server"
            LoadingPanelID="ralUpdateExtraBudgetaryDiscounts">

            <div id="page_manageApplications"
                class="field-document">
                <div class="new-document">
                    <span>Aplicaciones Extrapresupuestarias:</span>
                </div>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgExtraBudgetary"
                    runat="server"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgExtraBudgetary_OnNeedDataSource"
                    OnInsertCommand="RgExtraBudgetary_OnInsertCommand"
                    OnUpdateCommand="RgExtraBudgetary_OnUpdateCommand"
                    OnDeleteCommand="RgExtraBudgetary_OnDeleteCommand"
                    OnItemDataBound="RgExtraBudgetary_OnItemDataBound"
                    Height="395px">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="False"
                        AllowFilteringByColumn="False"
                        DataKeyNames="EXP_EXTRAP_CODIGO, EXTRAPRE_CODIGO, EXP_EXTRAP_IMPORTE, EXP_EXTRAP_FECHA"
                        CommandItemDisplay="Top"
                        AllowSorting="False"
                        AllowPaging="False"
                        PagerStyle-AlwaysVisible="False"
                        NoMasterRecordsText="No Hay datos a Mostrar.">

                        <CommandItemSettings ShowExportToExcelButton="False"
                            ShowAddNewRecordButton="True"
                            AddNewRecordText="Nuevo"
                            ShowRefreshButton="false"
                            ShowExportToPdfButton="false" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="EXTRAPRE_LABEL"
                                DataField="EXTRAPRE_LABEL"
                                HeaderText="Aplic. Extrapresupuestaria"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXP_EXTRAP_FECHA"
                                DataField="EXP_EXTRAP_FECHA"
                                HeaderText="Fecha"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="AmountLabel"
                                DataField="EXP_EXTRAP_IMPORTE"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Desea realmente eliminar el descuento extrapresupuestario en cuestión ?"
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <div class="form-group form-group-fake">
                                    <%--Aplicacion--%>
                                    <div class="field-container field-60">
                                        <span>Aplicación:</span>
                                        <telerik:RadComboBox ID="radDropApplication"
                                            runat="server"
                                            Width="100%"
                                            DataTextField="DisplayLabel"
                                            DataValueField="EXTRAPRE_CODIGO">
                                        </telerik:RadComboBox>
                                    </div>

                                    <%--Fecha--%>
                                    <div class="field-container field-25">
                                        <span>Fecha:</span>
                                        <telerik:RadDatePicker ID="dateOperation"
                                            RenderMode="Lightweight"
                                            runat="server"
                                            Width="120px"
                                            AutoPostBack="False"
                                            Culture="es-ES"
                                            EnableTyping="True"
                                            MaxDate="12/31/9999"
                                            MinDate="01/01/1800" />
                                        <asp:RequiredFieldValidator ID="rfvProposal"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="DiscountGroup"
                                            ControlToValidate="dateOperation"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca la Fecha de Propuesta del Descuento."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--Importe--%>
                                    <div class="field-container field-15">
                                        <span>Importe:</span>
                                        <telerik:RadNumericTextBox ID="txtAmount"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            Value="0"
                                            MinValue="0"
                                            ShowSpinButtons="False">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvAmount"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="DiscountGroup"
                                            ControlToValidate="txtAmount"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el importe del Descuento."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="DiscountGroup"
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
                <telerik:RadButton ButtonType="LinkButton" ID="btnAccept"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnCloseClientClicked"
                    Text="Aceptar"
                    AutoPostBack="False">
                </telerik:RadButton>
            </div>
        </telerik:RadAjaxPanel>
        <script>
            function moveNewButtons() {
                var $this = $("a[title*='Nuevo']");
                $this.each(function () {
                    var $destino = $(this).closest('.field-document').children(".new-document");
                    !$destino.has("a").length && $(this).appendTo($destino).show();
                });
            }
        </script>
    </form>
    <telerik:RadScriptBlock runat="server">
        <script>
            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

            function CloseWindows(response) {
                var wnd = GetRadWindow();
                if (wnd) {
                    wnd.close(response);
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

            function showError(response) {
                switch (response) {
                    case 0:
                        radalert("Debe completar todos los datos del Descuento Extrapresupuestario.", 330, 140, "Error insertando descuento extrapresupuestario", null, null);
                        break;
                    case 1:
                        radalert("Ha ocurrido un error insertando el Descuento Extrapresupuestario en cuestión.", 330, 140, "Error insertando descuento extrapresupuestario", null, null);
                        break;
                    case 2:
                        radalert("Debe completar todos los datos del Descuento Extrapresupuestario.", 330, 140, "Error modificando descuento extrapresupuestario", null, null);
                        break;
                    case 3:
                        radalert("No se ha encontrado el Descuento Extrapresupuestario en cuestión para su modificación.", 330, 140, "Error modificando descuento extrapresupuestario", null, null);
                        break;
                    case 4:
                        radalert("Ha ocurrido un error modificando el Descuento Extrapresupuestario en cuestión.", 330, 140, "Error modificando descuento extrapresupuestario", null, null);
                        break;
                    case 5:
                        radalert("Ha ocurrido un error eliminando el Descuento Extrapresupuestario en cuestión.", 330, 140, "Error eliminando descuento extrapresupuestario", null, null);
                        break;
                    case 6:
                        radalert("No se ha encontrado el Descuento Extrapresupuestario en cuestión para su eliminación.", 330, 140, "Error eliminando descuento extrapresupuestario", null, null);
                        break;
                }
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
