<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="LoadingPanel.aspx.cs" 
    Inherits="Dimatica.ContaPre.Presentation.Views.Shared.LoadingPanel" %>


        <telerik:RadAjaxLoadingPanel 
    ID="RadAjaxLoadingPanel1"
    runat="server"
    Skin="Material"
    Transparency="0"
    Modal="True">
    <asp:Label ID="Label2" runat="server" ForeColor="Red">Loading... </asp:Label>
</telerik:RadAjaxLoadingPanel>
    

    <telerik:RadScriptBlock runat="server">
        <script>


            function showLoading(app, args) {
                var loadingPanel = $find('<%= RadAjaxLoadingPanel1.ClientID %>');
                loadingPanel.show('<%= RadAjaxLoadingPanel1.ClientID %>');
            }

            function hideLoading(app, args) {
                var loadingPanel = $find('<%= RadAjaxLoadingPanel1.ClientID %>');
                loadingPanel.hide('<%= RadAjaxLoadingPanel1.ClientID %>');
            }



        </script>
    </telerik:RadScriptBlock>


