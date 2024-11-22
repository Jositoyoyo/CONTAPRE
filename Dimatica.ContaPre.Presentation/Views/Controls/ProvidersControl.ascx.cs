namespace Dimatica.ContaPre.Presentation.Views.Controls
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Models;

    using Telerik.Web.UI;

    #endregion

    public partial class ProvidersControl : System.Web.UI.UserControl
    {
        #region Public Properties

        public List<PRE_PROVEEDOR> Providers
        {
            get
            {
                var p = this.Session[this.Page.UniqueID] as List<PRE_PROVEEDOR>;

                if (p == null)
                {
                    p = new List<PRE_PROVEEDOR>();
                    this.Session[this.Page.UniqueID] = p;
                }

                return p;
            }
            set
            {
                this.Session[this.Page.UniqueID] = value;
            }
        }

        #endregion

        #region Public Methods

        public void Bind()
        {
            this.BindProviders(true);
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e) { }

        protected void RgSimilarities_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.BindProviders(false);
        }

        private void BindProviders(bool manual)
        {
            this.RgSimilarities.DataSource = this.Providers;

            if (manual)
            {
                this.RgSimilarities.DataBind();
            }
        }

        protected void RgSimilarities_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormInsertItem)
                {
                    foreach (GridDataItem i in this.RgSimilarities.Items)
                    {
                        i.Edit = false;
                        i.Expanded = false;
                    }
                }
            }
        }

        #endregion
    }
}