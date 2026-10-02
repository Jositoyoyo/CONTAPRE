<%@ Master Language="C#"
    AutoEventWireup="true"
    CodeBehind="MasterPage.master.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Shared.MasterPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Contabilidad Presupuestaria</title>
    <asp:ContentPlaceHolder ID="head" runat="server"></asp:ContentPlaceHolder>
    <link rel="stylesheet" href="/Content/Contapre.css" />
    <link rel="stylesheet" href="/Content/fontawesome-all.css" />
    <link href="/Content/Images/UIMP_Conf_Horizontal_RGB.png" rel="shortcut icon" type="image/x-icon" />
    <script src="/Scripts/jquery-3.7.1.js"></script>
</head>

<body id="<%= this.Session["_currentPage"] %>">

    <div id="PopupMensaje" style="display: none"></div>

    <div class="container">

        <form id="form1" runat="server">


            <telerik:RadScriptManager ID="RadScriptManager"
                runat="server"
                EnablePageMethods="True" />
            <telerik:RadSkinManager ID="rskmPrincipal"
                runat="server"
                Skin="Metro"
                ShowChooser="false" />
            <telerik:RadWindowManager ID="rwmPrincipal" runat="server" />

            <%--LOADING--%>
            <telerik:RadAjaxLoadingPanel ID="ralPrincipal"
                runat="server"
                Skin="Material"
                Transparency="0"
                Modal="True">
            </telerik:RadAjaxLoadingPanel>

            <%--HEADER--%>
            <div class="header">

                <!--Navbar-->
                <div id="menuToggle">
                    <input type="checkbox" class="fake" /><!-- A fake checkbox to can use the :checked selector -->
                    <div></div>

                    <span class="line"></span>
                    <span class="line"></span>
                    <span class="line"></span>

                    <telerik:RadTreeView ID="RadTreeViewMenu"
                        runat="server"
                        RenderMode="Lightweight"
                        DataFieldID="Id"
                        DataFieldParentID="ParentId"
                        DataValueField="Path"
                        DataTextField="Text"
                        OnNodeClick="RadTreeView_OnNodeClick"
                        OnClientNodeClicking="onClientNodeClicked">
                        <DataBindings>
                            <telerik:RadTreeNodeBinding Expanded="true"></telerik:RadTreeNodeBinding>
                        </DataBindings>
                    </telerik:RadTreeView>
                </div>

                <%--title--%>
                <h1>Contabilidad Presupuestaria</h1>

                <%--Logo--%>
                <div class="app-logo">
                    <img src="/Content/Images/UIMP_Conf_Horizontal_RGB.png" alt="Contabilidad Presupuestaria" />
                </div>

            </div>

            <asp:ContentPlaceHolder ID="ContentPlaceHolder1" runat="server"></asp:ContentPlaceHolder>

        </form>
    </div>
    <%-- FOOTER --%>
    <footer>
        <asp:Label ID="lblCurrentHost" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblCurrentDateTime" runat="server" Text=""></asp:Label>
    </footer>

   <script>
       $(document).ready(function () {
           const userLevel = '<%= this.Session["_userLevel"] %>';
        const currentPage = '<%= this.Session["_currentPage"] %>';
        const isClose = '<%= this.Session["_isClose"] %>';
        const status = '<%= this.Session["_status"] %>';
        const hasTonnageSheets = '<%= this.Session["_hasTonnageSheets"] %>';

        updateDateTime();
        setInterval(updateDateTime, 10000);

        configureButtons();
        configurePageAccess(userLevel, currentPage, isClose, status, hasTonnageSheets);
        setupMenuToggle();
    });

       // Actualiza la fecha y hora en el footer
       function updateDateTime() {
           const now = new Date();
           $('#<%= lblCurrentDateTime.ClientID %>').text(now.toLocaleString());
       }

       // Configura los botones según las características
       function configureButtons() {
           const buttonMappings = [
               { selector: "a[title*='Nuev']", target: ".top-buttons .new" },
               { selector: "input[title*='Excel']", target: ".top-buttons .excel" },
               { selector: "a[title*='Refresh']", target: ".top-buttons .refresh" }
           ];

           buttonMappings.forEach(mapping => {
               $(mapping.selector).appendTo(mapping.target).parent().show();
           });

           // Mostrar todos los botones por defecto
           $(".back, .find, .save, .new, .report, .delete, .print, .list").show();
       }

       // Configura accesos y visibilidad de botones según el contexto de la página
       function configurePageAccess(userLevel, currentPage, isClose, status, hasTonnageSheets) {
           switch (currentPage) {
               case "Incomes":
               case "Spends":
                   configureAccessIncomesAndSpends(userLevel, isClose);
                   break;
               case "GroupDr":
                   configureAccessGroupDr(userLevel, status);
                   break;
               case "PaymentRegister":
               case "AssignTonnageSheets":
                   configureAccessTonnageSheets(userLevel, hasTonnageSheets);
                   break;
           }
       }

       function configureAccessIncomesAndSpends(userLevel, isClose) {
           if (userLevel === "10") {
               $(".new, .edit, .delete").hide();
           } else if (userLevel === "50" || userLevel === "100") {
               $(".new, .report").show();
               if (isClose === "True") {
                   $(".edit, .delete, .close").hide();
               } else {
                   $(".edit, .delete, .close").show();
               }
           }
       }

       function configureAccessGroupDr(userLevel, status) {
           if (userLevel === "10") {
               $(".new, .report").hide();
           } else if (userLevel === "50" || userLevel === "100") {
               switch (status) {
                   case "":
                       $(".new, .report").hide();
                       break;
                   case "New":
                       $(".new").show();
                       $(".report").hide();
                       break;
                   case "Report":
                       $(".new").hide();
                       $(".report").show();
                       break;
               }
           }
       }

       function configureAccessTonnageSheets(userLevel, hasTonnageSheets) {
           if (userLevel === "10") {
               $(".save").hide();
           } else if (userLevel === "50" || userLevel === "100") {
               $(".save").toggle(hasTonnageSheets === "True");
           }
       }

       // Configura el comportamiento del menú toggle
       function setupMenuToggle() {
           const fakeInput = $('input.fake');
           if (fakeInput.is(':checked')) {
               fakeInput.trigger('click');
           }

           fakeInput.next('div').click(() => {
               fakeInput.trigger('click');
           });
       }

       // Manejo del clic en nodos del árbol
       function onClientNodeClicked(sender, args) {
           const node = args.get_node();
           if (node.get_nodes().get_count() > 0 || !node.get_value()?.trim()) {
               node.toggle();
               node.set_postBack(false);
           }
       }

       // Función para manejar la acción de volver atrás
       function backAction() {
           const source = '<%= this.Session["_currentSource"] %>';
           if (source) {
               window.location.href = source;
           } else {
               window.history.back();
           }
       }
   </script>


</body>
</html>
