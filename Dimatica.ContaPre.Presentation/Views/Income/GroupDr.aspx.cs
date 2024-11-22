namespace Dimatica.ContaPre.Presentation.Views.Income
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class GroupDr : BasePage
    {
        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        private IDocumentTypesService documentTypesService = DependencyFactory.GetInstance<IDocumentTypesService>();

        #endregion

        #region Public Properties

        public string Status
        {
            get
            {
                var p = this.Session["_status"].ToString();

                return p;
            }

            set
            {
                this.Session["_status"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "GroupDr";

                this.RmyExerciseYear.SelectedDate = DateTime.Now;

                var documents = this.documentTypesService.GetDocumentTypesDrPhase();
                documents.Insert(
                                 0,
                                 new PRE_TIPO_DOCUMENTO
                                         {
                                                 TIPD_CODIGO = -1
                                         });

                this.RcDocuments.DataSource = documents;
                this.RcDocuments.DataBind();
            }
        }

        protected void RgDocuments_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillDocuments(false);
        }

        protected void RgDocuments_OnPreRender(object sender, EventArgs e)
        {
            // if (this.Status == "Report")
            // {
            // this.RgDocuments.MasterTableView.GetColumn("Number").Visible = true;
            // this.RgDocuments.MasterTableView.GetColumn("SelectColumn").Visible = false;
            // }
            // else
            // {
            // this.RgDocuments.MasterTableView.GetColumn("Number").Visible = false;
            // this.RgDocuments.MasterTableView.GetColumn("SelectColumn").Visible = true;
            // }

            // this.RgDocuments.Rebind();
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.RgDocuments.MasterTableView.GetColumn("SelectColumn").Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgDocuments.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
            else
            {
                for (var i = 0; i < this.RgDocuments.Items.Count; i++)
                {
                    var item = this.RgDocuments.Items[i];
                    var check = (bool)item.GetDataKeyValue("DOC_AGRUPADO_DR_I");

                    if (check)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected void RgDocumentsGroup_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillDocuments(false);
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillDocuments(true);
        }

        private void FillDocuments(bool manual)
        {
            this.Status = string.Empty;

            var budgetYear = this.RmyBudgetYear.SelectedDate?.Year;
            var exerciseYear = this.RmyExerciseYear.SelectedDate?.Year;
            var documentId = this.RcDocuments.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(this.RcDocuments.SelectedValue);
            var groupNumber = this.TxtGroup.Value == null ? (int?)null : Convert.ToInt32(this.TxtGroup.Value);

            var documents = new List<PRE_EXPEDIENTE_CONTABLE>();
            this.RgDocuments.DataSource = documents;
            this.RgDocumentsGroup.DataSource = documents;

            if (manual)
            {
                this.RgDocuments.DataBind();
                this.RgDocumentsGroup.DataBind();
            }

            if (exerciseYear != null)
            {
                documents = this.accountingRecordsService.GetIncomesDr(budgetYear, exerciseYear, groupNumber, documentId);
            }

            if (documents.Any())
            {
                if (groupNumber != null)
                {
                    this.Status = "Report";
                    this.TxtNewGroup.Text = string.Empty;
                    this.TxtNewGroup.Enabled = false;

                    this.RgDocumentsGroup.DataSource = documents;

                    if (manual)
                    {
                        this.RgDocumentsGroup.DataBind();
                    }
                }
                else
                {
                    this.Status = "New";
                    this.TxtNewGroup.Text = this.accountingRecordsService.GetLastDrNumber((int)exerciseYear).ToString();
                    this.TxtNewGroup.Enabled = true;
                  
                    this.RgDocuments.DataSource = documents;

                    if (manual)
                    {
                        this.RgDocuments.DataBind();
                    }
                }
            }
            
            this.RpbFilter.CollapseAllItems();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideShowGrids", $"hideShowGrids('{this.Status}');", true);
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            var strBuilder = new StringBuilder();

            if (this.RgDocuments.SelectedItems.Count == 0)
            {
                strBuilder.Append("Debe seleccionar al menos un documento para poder agrupar.");
                this.ShowMessage(this.RadNotification, "Imposible agrupar documentos", strBuilder, MessageType.Warning);

                return;
            }

            var newGroupNumber = this.TxtNewGroup.Value == null ? (int?)null : Convert.ToInt32(this.TxtNewGroup.Value);

            if (newGroupNumber == null)
            {
                strBuilder.Append("Debe decir cual es el N⁰ de Agrupación para poder agrupar.");
                this.ShowMessage(this.RadNotification, "Imposible agrupar documentos", strBuilder, MessageType.Warning);

                return;
            }

            var documentsId = new List<int>();

            foreach (GridDataItem item in this.RgDocuments.SelectedItems)
            {
                var id = (int)item.GetDataKeyValue("DOC_CODIGO");
                documentsId.Add(id);
            }

            try
            {
                var update = this.accountingRecordsService.GroupDr((int)newGroupNumber, LoginUser.USU_CODIGO, documentsId);
                var message = string.Empty;
                var messageType = MessageType.Ok;

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Ha ocurrido un error agrupando los documentos en cuestión.");
                        message = "Error agrupando los documentos";
                        messageType = MessageType.Warning;

                        break;
                    case ResponseCode.Ok:
                        strBuilder.Append($"Los DR seleccionados han sido correctamente agrupados con el número {newGroupNumber}.");
                        message = "Documentos agrupados";
                        messageType = MessageType.Ok;
                        this.TxtNewGroup.Text = string.Empty;
                        break;
                }

                this.ShowMessage(this.RadNotification, message, strBuilder, messageType);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error agrupando los documentos Dr.");
                strBuilder.Append("Ha ocurrido un error agrupando los documentos en cuestión.");
                this.ShowMessage(this.RadNotification, "Error agrupando los documentos", strBuilder, MessageType.Deny);
            }
        }

        #endregion
    }
}