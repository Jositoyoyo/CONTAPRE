<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="NewApplications.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Maintenance.NewApplications" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="~/Content/Contapre.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <title></title>
    <style>

        .buttons:not(.form-group){
            width: auto;
            display:block;
        }

            .container .RadAjaxPanel .buttons:not(.form-group) .RadButton{
                margin-top: 1.1rem;
                min-width:140px;
            }

                .container .RadAjaxPanel .buttons .RadButton:last-child{
                    margin-right:0;
                }

        .form-group{
            justify-content: space-between;
        }

        form {
            height: 100%;
        }

        .container{
            padding:1.25rem;
        }

            .container h3{
                margin-bottom:.625rem;
            }

        .RadComboBox{
            margin-right:1.25rem;
        }

        .current-parent{
            display: flex;
            align-items: center;
        }
            .current-parent span{
                margin-right:.625rem
            }
    </style>
</head>

<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmNewApplications"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmNewApplications"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmNewApplications" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralNewApplications"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapNewApplications"
            runat="server"
            LoadingPanelID="ralNewApplications">

            <div class="form-group">
                <div class="field-container">
                    <span>Capítulo:</span>
                    <telerik:RadComboBox ID="RcChapters"
                        runat="server"
                        Width="300px"
                        AutoPostBack="True"
                        DataTextField="CAPITULO_LABEL"
                        DataValueField="CAP_CODIGO_AUX"
                        OnSelectedIndexChanged="RcChapters_OnSelectedIndexChanged">
                    </telerik:RadComboBox>
                </div>                
                <div class="buttons">
                    <telerik:RadButton ButtonType="LinkButton" ID="btnNewChapter"
                        runat="server"
                        RenderMode="Native"
                        Text="Nuevo Capítulo"
                        AutoPostBack="True"
                        OnClick="btnNewChapter_OnClick">
                    </telerik:RadButton>
                </div>                
            </div>

            <div class="form-group">
                <div class="field-container">
                    <span>Artículo:</span>
                    <telerik:RadComboBox ID="RcArticles"
                        runat="server"
                        Width="300px"
                        AutoPostBack="True"
                        DataTextField="ARTICULO_LABEL"
                        DataValueField="ART_CODIGO_AUX"
                        OnSelectedIndexChanged="RcArticles_OnSelectedIndexChanged">
                    </telerik:RadComboBox>
                </div>
                <div class="buttons">
                    <telerik:RadButton ButtonType="LinkButton" ID="btnNewArticle"
                        runat="server"
                        RenderMode="Native"
                        Text="Nuevo Artículo"
                        AutoPostBack="True"
                        OnClick="btnNewArticle_OnClick">
                    </telerik:RadButton>
                </div>
            </div>

            <div class="form-group">
                <div class="field-container">
                    <span>Concepto:</span>
                    <telerik:RadComboBox ID="RcConcepts"
                        runat="server"
                        Width="300px"
                        AutoPostBack="True"
                        DataTextField="CONCEPTO_LABEL"
                        DataValueField="CON_CODIGO_AUX"
                        OnSelectedIndexChanged="RcConcepts_OnSelectedIndexChanged">
                    </telerik:RadComboBox>
                </div>
                <div class="buttons">
                    <telerik:RadButton ButtonType="LinkButton" ID="btnNewConcept"
                        runat="server"
                        RenderMode="Native"
                        Text="Nuevo Concepto"
                        AutoPostBack="True"
                        OnClick="btnNewConcept_OnClick">
                    </telerik:RadButton>
                </div>
            </div>

            <div class="form-group">
                <div class="field-container">
                    <span>Subconcepto:</span>
                    <telerik:RadComboBox ID="RcSubconcepts"
                        runat="server"
                        Width="300px"
                        AutoPostBack="False"
                        DataTextField="SUBCONCEPTO_LABEL"
                        DataValueField="SUB_CODIGO_AUX">
                    </telerik:RadComboBox>
                </div>
                <div class="buttons">
                    <telerik:RadButton ButtonType="LinkButton" ID="btnNewSubconcept"
                        runat="server"
                        RenderMode="Native"
                        Text="Nuevo Subconcepto"
                        AutoPostBack="True"
                        OnClick="btnNewSubconcept_OnClick">
                    </telerik:RadButton>
                </div>

            </div>

            <%--FORM "NUEVO"--%>
            <div id="newApplicationDiv">

                <h3 id="currentAction" runat="server"></h3>

                <div class="form-group">
                    <div class="field-container field-20">
                        <span id="currentLabel" runat="server"></span>
                        <div class="current-parent">
                            <span id="currentParent" runat="server"></span>
                            <%--<telerik:RadNumericTextBox ID="txtNumber"
                                runat="server"
                                Width="60px"
                                RenderMode="Lightweight"
                                MinValue="0"
                                MaxValue="127"
                                MaxLength="3"
                                ShowSpinButtons="False"
                                NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="txtNumber"
                                Width="100%"
                                runat="server"
                                MaxLength="3"
                                Text="">
                            </telerik:RadTextBox>
                        </div>
                        <%--         <asp:RegularExpressionValidator ID="revNumber"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="ApplicationGroup"
                            ControlToValidate="txtNumber1"
                            ErrorMessage=" * "
                            ToolTip="Formato inválido."
                            ForeColor="Red"
                            ValidationExpression="^[0-9]*$">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RangeValidator ID="rvNumber"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="ApplicationGroup"
                            ControlToValidate="txtNumber"
                            ErrorMessage=" * "
                            ToolTip="Formato inválido."
                            ForeColor="Red"
                            MinimumValue="0"
                            MaximumValue="127"
                            Type="Integer" />
                        <asp:RequiredFieldValidator ID="rfvNumber"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="ApplicationGroup"
                            ControlToValidate="txtNumber"
                            ErrorMessage=" * "
                            ToolTip="Introduzca el número de la Aplicación."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="field-container field-80">
                        <span>Descripción:</span>
                        <telerik:RadTextBox ID="txtDescription"
                            Width="100%"
                            runat="server"
                            MaxLength="60"
                            Text="">
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
            </div>

            <%--BUTTONS--%>
            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                    runat="server"
                    RenderMode="Native"
                    Text="Grabar"
                    ValidationGroup="ApplicationGroup"
                    AutoPostBack="True"
                    OnClick="btnSave_OnClick">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnCancel"
                    runat="server"
                    RenderMode="Native"
                    Text="Cancelar"
                    AutoPostBack="True"
                    OnClick="btnCancel_OnClick">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnClose"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnCloseClientClicked"
                    Text="Cerrar"
                    AutoPostBack="False">
                </telerik:RadButton>
            </div>

            <script>
                function hideGrids(status) {
                    switch (status) {
                        case "view":
                            $('#btnSave').hide();
                            $('#btnCancel').hide();
                            $('#newApplicationDiv').hide();
                            break;
                        case "edit":
                            $('#btnSave').show();
                            $('#btnCancel').show();
                            $('#newApplicationDiv').show();
                            break;
                    }
                }
            </script>
        </telerik:RadAjaxPanel>
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

            function showError(message, title) {
                radalert(message, 330, 140, title, null, null);
            }

        </script>
    </telerik:RadScriptBlock>
</body>
</html>
