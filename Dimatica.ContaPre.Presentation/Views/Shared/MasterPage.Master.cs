namespace Dimatica.ContaPre.Presentation.Views.Shared
{
    #region NameSpaces

    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using Dimatica.ContaPre.Presentation.Helpers;
    using Dimatica.ContaPre.Presentation.Helpers.SiteData;
    using Telerik.Web.UI;

    #endregion

    public partial class MasterPage : System.Web.UI.MasterPage
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {

            Thread.CurrentThread.CurrentCulture   = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            string currentHost = HttpContext.Current.Request.Url.Host;

            if (!this.IsPostBack)
            {
                if (this.Session["_currentPage"] == null)
                {
                    this.Session["_currentPage"] = "MasterPage";
                }
                
                if (this.Session["_currentSource"] == null)
                {
                    this.Session["_currentSource"] = "~/Views/Home/Home.aspx";
                }

                this.getCurrentHost();
                this.InitializeTreeView();
            }
        }

        protected void getCurrentHost()
        {
            this.lblCurrentHost.Text = HttpContext.Current.Request.Url.Host ?? "Unkwon";
        }

        private void InitializeTreeView()
        {
            var dataSource = SiteDataHelper.GetSiteDataItems();

            foreach (var item in dataSource)
            {
                if (!string.IsNullOrWhiteSpace(item.Path))
                {
                    item.Path = this.ResolveUrl(item.Path);
                }
            }

            this.RadTreeViewMenu.DataSource = dataSource;
            this.RadTreeViewMenu.DataBind();
            this.RadTreeViewMenu.CollapseAllNodes();

            foreach (RadTreeNode node in this.RadTreeViewMenu.Nodes)
            {
                if (node.Text == "Presupuestos")
                {
                    node.Expanded = true;
                    break;
                }
            }
        }

        protected void RadTreeViewMenu_NodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            if (e.Node.Level != 0)
            {
                return;
            }

            switch (e.Node.Text)
            {
                case "Presupuestos":
                    e.Node.CssClass = "menu-icon-chart-pie";
                    break;
                case "Ingresos":
                    e.Node.CssClass = "menu-icon-money-bill-wave";
                    break;
                case "Gastos":
                    e.Node.CssClass = "menu-icon-file-invoice-dollar";
                    break;
                case "Listados":
                    e.Node.CssClass = "menu-icon-list-alt";
                    break;
                case "Extrapresupuestarias":
                    e.Node.CssClass = "menu-icon-folder-open";
                    break;
                case "Tesorería":
                    e.Node.CssClass = "menu-icon-university";
                    break;
                case "Rectificaciones":
                    e.Node.CssClass = "menu-icon-edit";
                    break;
                case "Señalamientos":
                    e.Node.CssClass = "menu-icon-calendar-check";
                    break;
                case "Sistema":
                    e.Node.CssClass = "menu-icon-cog";
                    break;
                case "Tablas":
                    e.Node.CssClass = "menu-icon-table";
                    break;
                case "Development":
                    e.Node.CssClass = "menu-icon-code";
                    break;
            }
        }

        #endregion
    }
}
