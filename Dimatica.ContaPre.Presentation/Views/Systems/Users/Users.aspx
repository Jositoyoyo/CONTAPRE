<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Users.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Systems.Users.Users" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_systems" class="container">

        <h3>Usuarios</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">

          <div class="a-buttons">

            <div id="newUserDiv" runat="server" class="new">
                <span class="icon"></span>
                <asp:LinkButton ID="linkToManagerUser" runat="server" Text="Nuevo" CausesValidation="False" PostBackUrl="ManageUser.aspx"></asp:LinkButton>
            </div>

          </div>

        </div>

        <div id="page_users" class="box-block">

            <telerik:RadAjaxPanel ID="rapUsers" runat="server"
                LoadingPanelID="ralPrincipal">

                <%-- panel de notificaciones --%>
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

                <%-- tabla usuarios --%>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgUsers"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgUsers_OnNeedDataSource"
                    OnDeleteCommand="RgUsers_OnStatusCommand"
                    OnItemDataBound="RgUsers_OnItemDataBound"
                    OnPreRender="RgUsers_OnPreRender"
                    OnItemCommand="RgUsers_OnItemCommand"
                    CssClass="big-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView
                        AllowAutomaticInserts="true"
                        AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="USU_CODIGO, USU_NOMBRE, USU_APELLIDOS, USU_LOGIN, USU_I_G, USU_NIVEL, Obsolete"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="30"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">


                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} P�gina {0} de {1}, elementos {2} a {3} de {5}" />

                        <%-- fila de resultados usuarios --%>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="FullName"
                                DataField="FullName"
                                HeaderText="Nombre Completo"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="USU_LOGIN"
                                DataField="USU_LOGIN"
                                HeaderText="Usuario"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="30%" />
                                </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="USU_NIVEL"
                                DataField="USU_NIVEL"
                                HeaderText="Nivel"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="20%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridImageColumn DataType="System.String"
                                DataImageUrlFields="ObsoleteImage"
                                DataImageUrlFormatString="/Public/Images/{0}.png"
                                ImageAlign="Middle"
                                ImageHeight="18px"
                                ImageWidth="18px"
                                HeaderStyle-ForeColor="White"
                                HeaderStyle-Width="32px"
                                AllowFiltering="false">
                            </telerik:GridImageColumn>

                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar usuario"
                                CommandName="UpdateUser"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>

                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Cambiar estado"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                Text=" "
                                ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCI�N"
                                ConfirmText="� Est� seguro que desea cambiar el estado de este Usuario ?"
                                ConfirmDialogHeight="100px"
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

            function showModalDiv(sender, args)
            {
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