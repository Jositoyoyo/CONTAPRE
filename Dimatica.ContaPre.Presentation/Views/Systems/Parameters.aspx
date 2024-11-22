<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Parameters.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Systems.Parameters" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content
    ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="container" id="section_systems">

        <h3>Parámetros</h3>

        <!--BUTTONS-->
        <div class="top-buttons">

            <div class="a-buttons">
               <%-- <div class="excel">
                    <span class="icon">Excel</span>
                </div>
                <div class="refresh">
                    <span class="icon"></span>
                </div>--%>
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>
            </div>
        </div>

        <div id="page_parameters" class="box-block">

            <telerik:RadAjaxPanel ID="rapParameters" runat="server"
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

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgParameters"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgParameters_NeedDataSource"
                    OnUpdateCommand="RgParameters_UpdateCommand"
                    OnItemDataBound="RgParameters_ItemDataBound"
                    OnPreRender="RgParameters_PreRender"
                    CssClass="parameter-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="PAR_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="50"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings ShowAddNewRecordButton="false"
                            AddNewRecordText="Nuevo"
                            ShowRefreshButton="False"
                            ShowExportToExcelButton="False"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PAR_DESCRIPCION"
                                DataField="PAR_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PAR_VALOR"
                                DataField="PAR_VALOR"
                                HeaderText="Valor"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Modificar Parámetro"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                ShowFilterIcon="false"
                                EditText=" ">
                            </telerik:GridEditCommandColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>

                                <h3>Parámetros</h3>

                                <div class="form-group">

                                    <%--Valor--%>
                                    <div class="field-container">
                                        <span>Valor:</span>
                                        <telerik:RadTextBox ID="txtValue"
                                            Width="100%"
                                            runat="server"
                                            MaxLength="60"
                                            Text='<%#this.Eval("PAR_VALOR") %>'>
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvValue"
                                            runat="server"
                                            Display="Dynamic"
                                            ValidationGroup="ValueGroup"
                                            ControlToValidate="txtValue"
                                            ErrorMessage=" * "
                                            ToolTip="Introduzca un valor."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <%--BUTTONS--%>
                                <div class="buttons">

                                    <asp:LinkButton ID="btnUpdate"
                                        runat="server"
                                        ValidationGroup="ValueGroup"
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
                $('#ctl00_ContentPlaceHolder_RgParameters_ctl00_ctl02_ctl00_InitInsertButton').hide();
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

                var grid = $get("<%=this.RgParameters.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

        </script>
    </telerik:RadScriptBlock>

</asp:Content>
