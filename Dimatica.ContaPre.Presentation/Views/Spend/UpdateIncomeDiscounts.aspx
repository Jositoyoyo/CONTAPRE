<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="UpdateIncomeDiscounts.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.UpdateIncomeDiscounts" %>

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
            width: 100%;
            height: 100%;
        }

        .manage {
            padding: 15px !important;
        }

        .RadCalendarMonthView td[id*='.'] {
            display: none;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmUpdateIncomeDiscounts"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmUpdateIncomeDiscounts"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmUpdateIncomeDiscounts" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralUpdateIncomeDiscounts"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapUpdateIncomeDiscounts"
            runat="server"
            LoadingPanelID="ralUpdateIncomeDiscounts">
            <div class="manage">
                <div class="form-group form-group-fake">

                    <%--Linea--%>
                    <div class="field-container field-30">
                        <telerik:RadRadioButtonList ID="RrbLine"
                            runat="server"
                            AutoPostBack="False"
                            Width="100%">
                            <Items>
                                <telerik:ButtonListItem Text="Línea 10"
                                    Value="42"
                                    Selected="true" />
                                <telerik:ButtonListItem Text="Línea 13"
                                    Value="41" />
                            </Items>
                        </telerik:RadRadioButtonList>
                    </div>

                    <%--Año Presupuesto--%>
                    <div class="field-container field-30">
                        <span>Año Presup.:</span>
                        <telerik:RadMonthYearPicker ID="RmyBudgetYear"
                            runat="server"
                            Width="100%"
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

                    <%--Area--%>
                    <div class="field-container field-40">
                        <span>Área Origen:</span>
                        <telerik:RadComboBox ID="RcAreas"
                            runat="server"
                            Width="100%"
                            AutoPostBack="False"
                            DataTextField="DisplayLabelArea"
                            DataValueField="CEN_CODIGO">
                        </telerik:RadComboBox>
                    </div>
                </div>

                <div class="form-group form-group-fake">
                    <%--Procedencia--%>
                    <div class="field-container field-40">
                        <span>Procedencia Ing.:</span>
                        <telerik:RadComboBox ID="RcProvenances"
                            runat="server"
                            Width="100%"
                            AutoPostBack="False"
                            DataTextField="PROC_DESCRIPCION"
                            DataValueField="PROC_CODIGO">
                        </telerik:RadComboBox>
                    </div>

                    <%--Proveedor--%>
                    <div class="field-container field-60">
                        <span>Proveedor:</span>
                        <telerik:RadComboBox ID="RcProviders"
                            runat="server"
                            Width="100%"
                            AutoPostBack="False"
                            DataTextField="PROV_NOMBRE"
                            DataValueField="PROV_CODIGO">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>

            <div id="page_manageApplications"
                class="field-document">
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
                    Height="260px">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="False"
                        AllowFilteringByColumn="False"
                        DataKeyNames="DOCA_CODIGO, CACS_CODIGO, DOCA_IMPORTE"
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
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Desea realmente eliminar el importe asociado a la aplicación en cuestión ?"
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <div class="form-group form-group-fake">
                                    <%--Aplicacion--%>
                                    <div class="field-container field-50">
                                        <span>Aplicación:</span>
                                        <telerik:RadComboBox ID="radDropApplication"
                                            runat="server"
                                            Width="100%"
                                            DataTextField="CacsNumber"
                                            DataValueField="CacsCode">
                                        </telerik:RadComboBox>
                                    </div>
                                    <%--Importe--%>
                                    <div class="field-container field-50">
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
                <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                    runat="server"
                    RenderMode="Native"
                    Text="Grabar"
                    AutoPostBack="True"
                    OnClick="btnSave_OnClick">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                    runat="server"
                    RenderMode="Native"
                    Text="Eliminar"
                    AutoPostBack="False"
                    OnClientClicked="btnDeleteOnClientClicked">
                </telerik:RadButton>
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

            function hideApplications() {
                $('#btnDelete').hide();
                $('#page_manageApplications').hide();
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
                        radalert("Debe completar todos los datos del Expediente Contable.", 330, 140, "Error insertando expediente contable", null, null);
                        break;
                    case 1:
                        radalert("Ha ocurrido un error insertando el Expediente Contable en cuestión.", 330, 140, "Error insertando expediente contable", null, null);
                        break;
                    case 2:
                        radalert("Debe completar todos los datos del Documento Contable.", 330, 140, "Error insertando documento contable", null, null);
                        break;
                    case 3:
                        radalert("Debe completar todos los datos del Expediente Contable.", 330, 140, "Error modificando expediente contable", null, null);
                        break;
                    case 4:
                        radalert("Ha ocurrido un error modificando el Expediente Contable en cuestión.", 330, 140, "Error modificando expediente contable", null, null);
                        break;
                    case 5:
                        radalert("Debe completar todos los datos del Documento Contable.", 330, 140, "Error modificando documento contable", null, null);
                        break;
                    case 6:
                        radalert("Ha ocurrido un error inesperado insertando el Documento de la Aplicación.", 330, 140, "Error insertando aplicación", null, null);
                        break;
                    case 7:
                        radalert("Debe completar todos los datos del Documento de la Aplicación.", 330, 140, "Error insertando aplicación", null, null);
                        break;
                    case 8:
                        radalert("Ha ocurrido un error inesperado modificando el Documento de la Aplicación.", 330, 140, "Error modificando aplicación", null, null);
                        break;
                    case 9:
                        radalert("Debe completar todos los datos del Documento de la Aplicación.", 330, 140, "Error modificando aplicación", null, null);
                        break;
                    case 10:
                        radalert("No se ha encontrado el Documento de la Aplicación en cuestión para su modificación.", 330, 140, "Error modificando aplicación", null, null);
                        break;
                    case 11:
                        radalert("Ha ocurrido un error inesperado eliminando el Documento de la Aplicación.", 330, 140, "Error eliminando aplicación", null, null);
                        break;
                    case 12:
                        radalert("No se ha encontrado el Documento de la Aplicación en cuestión para su eliminación.", 330, 140, "Error eliminando aplicación", null, null);
                        break;
                    case 13:
                        radalert("Ha ocurrido un error inesperado eliminando el expediente contable.", 330, 140, "Error eliminando expediente contable", null, null);
                        break;
                    case 14:
                        radalert("No se ha encontrado el expediente contable en cuestión para su eliminación", 330, 140, "Error eliminando expediente contable", null, null);
                        break;
                }
            }


            function btnDeleteOnClientClicked(sender, eventArgs) {
                radconfirm("¿ Está seguro que desea eliminar el expediente contable en cuestión ?",
                    confirmDeleteCallBackFn,
                    330,
                    140,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "UpdateIncomeDiscounts.aspx/DeleteAccountingRecord",
                    data: null,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("Ha ocurrido un error eliminando el Expediente Contable en cuestión.", 330, 140, "Imposible eliminar expediente contable", null, null);
                                break;
                            case 1:
                                CloseWindows(null);
                                break;
                            case 2:
                                radalert("No se puede eliminar el Expediente Contable en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar expediente contable", null, null);
                                break;
                            case 3:
                                radalert("No se puede eliminar el Expediente Contable en cuestión debido a que no se ha creado aún en la BD.", 330, 140, "Imposible eliminar expediente contable", null, null);
                                break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar el Expediente Contable en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar expediente contable", null, null);
                    }
                });
            }

        </script>
    </telerik:RadScriptBlock>
</body>
</html>
