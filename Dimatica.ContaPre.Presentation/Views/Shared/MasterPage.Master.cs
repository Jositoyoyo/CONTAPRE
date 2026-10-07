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
                if (node.Text == "Presupuestos" || node.Text == "Ingresos" || node.Text == "Gastos" || node.Text == "Tesorería")
                {
                    node.Expanded = true;
                }
            }
        }

        #endregion
    }
}
