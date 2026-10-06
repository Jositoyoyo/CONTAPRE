<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="UpdateApplications.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.UpdateApplications" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="/Public/javascript/jquery-3.7.1.js"></script>
    <title></title>
    <style>
        .buttons {
            justify-content: flex-end;
            display: flex;
        }

        form {
            height: 100%;
        }

        strong {
            margin: 5px;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmUpdateApplications"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmUpdateApplications"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmUpdateApplications" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralUpdateApplications"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapUpdateApplications"
            runat="server"
            LoadingPanelID="ralUpdateApplications">
            <div class="field-document">
                <strong id="lblTitle" runat="server"></strong>
                <div class="new-document">
                    <span>Aplicaciones:</span>
                </div>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgUpdateApplications"
                    runat="server"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgUpdateApplications_OnNeedDataSource"
                    OnInsertCommand="RgUpdateApplications_OnInsertCommand"
                    OnUpdateCommand="RgUpdateApplications_OnUpdateCommand"
                    OnDeleteCommand="RgUpdateApplications_OnDeleteCommand"
                    OnItemDataBound="RgUpdateApplications_OnItemDataBound"
                    OnItemCommand="RgUpdateApplications_OnItemCommand"
                    Height="400px">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="False"
                        AllowFilteringByColumn="False"
                        DataKeyNames="DOCA_CODIGO, CACS_CODIGO, DOCA_IMPORTE, CACS_NUMERO"
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
                            <telerik:GridBoundColumn UniqueName="CACS_NUMERO"
                                DataField="CACS_NUMERO"
                                HeaderText="Aplicación"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CUEP_NUMERO"
                                DataField="CUEP_NUMERO"
                                HeaderText="Cuenta PGCP"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="40%" />
                            </telerik:GridNumericColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar Aplicación"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Aplicación"
                                CommandName="DeleteCommand"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <div class="form-group form-group-fake">
                                    <%--Aplicacion--%>
                                    <div class="field-container field-70">
                                        <span>Aplicación:</span>
                                        <telerik:RadComboBox ID="radDropApplication"
                                            runat="server"
                                            Width="100%"
                                            DataTextField="Description"
                                            DataValueField="CacsCode">
                                        </telerik:RadComboBox>
                                    </div>
                                    <%--Importe--%>
                                    <div class="field-container field-30">
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
                                            ValidationGroup="ApplicationGroup"
                                            ControlToValidate="txtAmount"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca el importe del Documento de Aplicación."
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
                debugger;
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

            var itemIndex;
            function deleteApplication(index, number) {
                itemIndex = index;
                radconfirm("¿ Desea realmente eliminar el importe asociado a la aplicación " + number + " ?",
                    confirmDeleteApplicationCallBackFn,
                    400,
                    220,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmDeleteApplicationCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                if (itemIndex) {
                    var masterTable = $find("<%= this.RgUpdateApplications.ClientID %>").get_masterTableView();
                    masterTable.fireCommand("Delete", itemIndex);
                }
            }

            function showError(response) {
                switch (response) {
                    case 0:
                        radalert("Debe completar todos los datos del Documento de la Aplicación.", 330, 140, "Error insertando Aplicación", null, null);
                        break;
                    case 1:
                        radalert("Ha ocurrido un error inesperado insertando el Documento de la Aplicación.", 330, 140, "Error insertando Aplicación", null, null);
                        break;
                    case 2:
                        radalert("Debe completar todos los datos del Documento de la Aplicación.", 330, 140, "Error modificando Aplicación", null, null);
                        break;
                    case 3:
                        radalert("No se ha encontrado el Documento de la Aplicación en cuestión para su modificación.", 330, 140, "Imposible modificar Aplicación", null, null);
                        break;
                    case 4:
                        radalert("Ha ocurrido un error inesperado modificando el Documento de la Aplicación.", 330, 140, "Error modificando Aplicación", null, null);
                        break;
                    case 5:
                        radalert("No se ha encontrado el Documento de la Aplicación en cuestión para su eliminación.", 330, 140, "Imposible eliminar Aplicación", null, null);
                        break;
                    case 6:
                        radalert("Ha ocurrido un error inesperado eliminando el Documento de la Aplicación.", 330, 140, "Error eliminando Aplicación", null, null);
                        break;
                    case 7:
                        radalert("El importe introducido supera el importe total del RC.</br>Consulte el estado del expediente y verifique los importes antes de continuar.", 330, 140, "Error insertando Aplicación", null, null);
                        break;
                    case 8:
                        radalert("El importe introducido supera el importe total del AD.</br>Consulte el estado del expediente y verifique los importes antes de continuar.", 330, 140, "Error insertando Aplicación", null, null);
                        break;
                    case 9:
                        radalert("El importe introducido supera el importe total del RC.</br>Consulte el estado del expediente y verifique los importes antes de continuar.", 330, 140, "Error modificando Aplicación", null, null);
                        break;
                    case 10:
                        radalert("El importe introducido supera el importe total del AD.</br>Consulte el estado del expediente y verifique los importes antes de continuar.", 330, 140, "Error modificando Aplicación", null, null);
                        break;
                    case 11:
                        radalert("Compruebe el estado del expediente antes de continuar.", 330, 140, "Atención", null, null);
                        break;
                }
            }

            function showBigError(cacsCode, articleNumber, articleName, year, articleBudget, articleCredit, articleDiff, hasChapter, chapterNumber, chapterName, chapterBudget, chapterCredit, chapterDiff) {
                var text = "El crédito retenido supera al presupuestado.</br>A nivel de art1culo: " + articleNumber + " - " + articleName + "(" + year + ")</br>Presupuesto Definitivo: " + articleBudget + "</br>Crédito Retenido: " + articleCredit + "</br>Diferencia: " + articleDiff;

                var size = 220;
                if (hasChapter == 1) {
                    size = 300;
                    text = text + "</br></br>A nivel de capitulo: " + chapterNumber + " - " + chapterName + "(" + year + ")</br>Presupuesto Definitivo: " + chapterBudget + "</br>Crédito Retenido: " + chapterCredit + "</br>Diferencia: " + chapterDiff;
                }

                radalert(text, 340, size, "Error", null, null);
            }

        </script>
    </telerik:RadScriptBlock>
</body>
</html>
