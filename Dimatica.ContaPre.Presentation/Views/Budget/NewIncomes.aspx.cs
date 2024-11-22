namespace Dimatica.ContaPre.Presentation.Views.Budget
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class NewIncomes : BasePage
    {
        #region Fields

        IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        #endregion

        #region Public Properties

        public bool Verify
        {
            get
            {
                var p = (bool)this.Session["_verify"];

                return p;
            }

            set
            {
                this.Session["_verify"] = value;
            }
        }

        #endregion

        #region Private Properties

        private List<Budget> Budgets
        {
            get
            {
                var budgets = this.Session["_budgets"] as List<Budget>;

                if (budgets == null)
                {
                    budgets = new List<Budget>();
                    this.Session["_budgets"] = budgets;
                }

                return budgets;
            }

            set
            {
                this.Session["_budgets"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                this.RmyDates.SelectedDate = DateTime.Now;
                this.Budgets = new List<Budget>();
                this.Verify = false;
                var budgets = this.budgetsService.GetBudgetsToNewByType("I");

                foreach (var budget in budgets)
                {
                    this.Budgets.Add(budget);
                }

                this.RgNewIncomes.MasterTableView.GetColumn("AmountSub").Visible = false;
                this.RgNewIncomes.MasterTableView.GetColumn("AmountCon").Visible = false;
                this.RgNewIncomes.MasterTableView.GetColumn("AmountChaArt").Visible = false;
            }
        }

        protected void RgNewIncomes_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.RgNewIncomes.DataSource = this.Budgets;
        }

        protected void btnCalculate_OnClick(object sender, EventArgs e)
        {
            this.CalculateTotals();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            this.CalculateTotals();

            if (this.Budgets.All(b => b.Amount == 0))
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "onEmptyValues", "OnEmptyValues();", true);
            }
            else
            {
                var result = 1;

                try
                {
                    var year = ((DateTime)this.RmyDates.SelectedDate).Year;

                    foreach (GridDataItem item in this.RgNewIncomes.MasterTableView.Items)
                    {
                        var level = item.GetDataKeyValue("Level").ToString();

                        var chapter = item.GetDataKeyValue("Chapter").ToString();
                        var article = item.GetDataKeyValue("Article").ToString();
                        var concept = item.GetDataKeyValue("Concept").ToString();
                        var subConcept = item.GetDataKeyValue("SubConcept").ToString();

                        var checkNotBinding = (RadCheckBox)item["NotBinding"].FindControl("checkNotBinding");

                        this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).NotBinding = (bool)checkNotBinding.Checked;
                        this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).ProgramId = null;
                    }

                    var insert = this.budgetsService.InsertMany(year, "I", this.Budgets, LoginUser.USU_CODIGO);

                    switch (insert.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            result = -1;

                            break;
                        case ResponseCode.Found:
                            result = 0;

                            break;
                    }
                }
                catch (Exception ex)
                {
                    result = -1;
                    LogError(ex, "Error insertando el presupuesto de ingreso.");
                }

                var script = $"OnSaveSuccess({result});";
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "onSaveSuccess", script, true);
            }
        }

        protected void RgNewIncomes_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;
                var level = dataBoundItem.GetDataKeyValue("Level").ToString();
                var chapter = dataBoundItem.GetDataKeyValue("Chapter").ToString();
                var article = dataBoundItem.GetDataKeyValue("Article").ToString();
                var concept = dataBoundItem.GetDataKeyValue("Concept").ToString();

                switch (level)
                {
                    case "CAP":
                        ((RadNumericTextBox)dataBoundItem["SubAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["ConAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["ChaArtAmount"].Controls[1]).Style.Add("Visibility", "visible !important");

                        if (this.Budgets.Any(b => b.Chapter.Equals(chapter) && string.IsNullOrEmpty(b.Article) == false))
                        {
                            ((RadNumericTextBox)dataBoundItem["ChaArtAmount"].Controls[1]).Enabled = false;
                            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).HasSons = true;
                        }

                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#e8e8e8");

                        break;
                    case "ART":
                        ((RadNumericTextBox)dataBoundItem["SubAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["ConAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["ChaArtAmount"].Controls[1]).Style.Add("Visibility", "visible !important");

                        if (this.Budgets.Any(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && string.IsNullOrEmpty(b.Concept) == false))
                        {
                            ((RadNumericTextBox)dataBoundItem["ChaArtAmount"].Controls[1]).Enabled = false;
                            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).HasSons = true;
                        }

                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#f5f5f5");

                        break;
                    case "CON":
                        ((RadNumericTextBox)dataBoundItem["SubAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["ConAmount"].Controls[1]).Style.Add("Visibility", "visible !important");
                        ((RadNumericTextBox)dataBoundItem["ChaArtAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");

                        if (this.Budgets.Any(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && string.IsNullOrEmpty(b.SubConcept) == false))
                        {
                            ((RadNumericTextBox)dataBoundItem["ConAmount"].Controls[1]).Enabled = false;
                            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("CON")).HasSons = true;
                        }

                        break;
                    case "SUB":
                        ((RadNumericTextBox)dataBoundItem["SubAmount"].Controls[1]).Style.Add("Visibility", "visible !important");
                        ((RadNumericTextBox)dataBoundItem["ConAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["ChaArtAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");

                        break;
                }
            }
        }

        protected void txtSubAmount_OnTextChanged(object sender, EventArgs e)
        {
            var txtAmount = (RadNumericTextBox)sender;
            var value = txtAmount.Value == null ? 0 : (decimal)txtAmount.Value;
            var dataBoundItem = (GridDataItem)txtAmount.BindingContainer;

            var chapter = dataBoundItem.GetDataKeyValue("Chapter").ToString();
            var article = dataBoundItem.GetDataKeyValue("Article").ToString();
            var concept = dataBoundItem.GetDataKeyValue("Concept").ToString();
            var subConcept = dataBoundItem.GetDataKeyValue("SubConcept").ToString();

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept)).Amount = value;

            decimal totalConcept = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("SUB")).ToList())
            {
                totalConcept += budget.Amount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("CON")).Amount = totalConcept;

            decimal totalArticle = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("CON")).ToList())
            {
                totalArticle += budget.Amount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).Amount = totalArticle;

            decimal totalChapter = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Level.Equals("ART")).ToList())
            {
                totalChapter += budget.Amount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).Amount = totalChapter;

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.Amount);
            this.txtTotal.Text = total.ToString("N", new CultureInfo("es-ES"));

            this.UpdateGridValues();
        }

        protected void txtConAmount_OnTextChanged(object sender, EventArgs e)
        {
            var txtAmount = (RadNumericTextBox)sender;
            var value = txtAmount.Value == null ? 0 : (decimal)txtAmount.Value;
            var dataBoundItem = (GridDataItem)txtAmount.BindingContainer;

            var chapter = dataBoundItem.GetDataKeyValue("Chapter").ToString();
            var article = dataBoundItem.GetDataKeyValue("Article").ToString();
            var concept = dataBoundItem.GetDataKeyValue("Concept").ToString();

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("CON")).Amount = value;

            decimal totalArticle = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("CON")).ToList())
            {
                totalArticle += budget.Amount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).Amount = totalArticle;

            decimal totalChapter = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Level.Equals("ART")).ToList())
            {
                totalChapter += budget.Amount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).Amount = totalChapter;

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.Amount);
            this.txtTotal.Text = total.ToString("N", new CultureInfo("es-ES"));

            this.UpdateGridValues();
        }

        protected void txtChaArtAmount_OnTextChanged(object sender, EventArgs e)
        {
            var txtAmount = (RadNumericTextBox)sender;
            var value = txtAmount.Value == null ? 0 : (decimal)txtAmount.Value;
            var dataBoundItem = (GridDataItem)txtAmount.BindingContainer;

            var chapter = dataBoundItem.GetDataKeyValue("Chapter").ToString();
            var article = dataBoundItem.GetDataKeyValue("Article").ToString();
            var level = dataBoundItem.GetDataKeyValue("Level").ToString();

            switch (level)
            {
                case "CAP":
                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).Amount = (decimal)txtAmount.Value;

                    break;
                case "ART":
                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).Amount = value;

                    decimal totalChapter = 0;

                    foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Level.Equals("ART")).ToList())
                    {
                        totalChapter += budget.Amount;
                    }

                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).Amount = totalChapter;

                    break;
            }

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.Amount);
            this.txtTotal.Text = total.ToString("N", new CultureInfo("es-ES"));

            this.UpdateGridValues();
        }

        private void UpdateGridValues()
        {
            foreach (GridDataItem item in this.RgNewIncomes.MasterTableView.Items)
            {
                var chapter = item.GetDataKeyValue("Chapter").ToString();
                var article = item.GetDataKeyValue("Article").ToString();
                var concept = item.GetDataKeyValue("Concept").ToString();
                var subConcept = item.GetDataKeyValue("SubConcept").ToString();

                var budget = this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept));

                if (budget == null)
                {
                    continue;
                }

                var checkNotBinding = (RadCheckBox)item["NotBinding"].FindControl("checkNotBinding");

                this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept)).NotBinding = (bool)checkNotBinding.Checked;

                var column = string.Empty;
                var columnComp = string.Empty;

                switch (budget.Level)
                {
                    case "CAP":
                        column = "ChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "ART":
                        column = "ChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "CON":
                        column = "ConAmount";
                        columnComp = "txtConAmount";

                        break;
                    case "SUB":
                        column = "SubAmount";
                        columnComp = "txtSubAmount";

                        break;
                }

                var txtAmount = (RadNumericTextBox)item[column].FindControl(columnComp);

                txtAmount.Value = (double)budget.Amount;
            }

            this.RgNewIncomes.Rebind();
        }

        private void CalculateTotals()
        {
            foreach (GridDataItem item in this.RgNewIncomes.MasterTableView.Items)
            {
                var chapter = item.GetDataKeyValue("Chapter").ToString();
                var article = item.GetDataKeyValue("Article").ToString();
                var concept = item.GetDataKeyValue("Concept").ToString();
                var subConcept = item.GetDataKeyValue("SubConcept").ToString();

                var budget = this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept));

                if (budget == null)
                {
                    continue;
                }

                var column = string.Empty;
                var columnComp = string.Empty;

                switch (budget.Level)
                {
                    case "CAP":
                        column = "ChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "ART":
                        column = "ChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "CON":
                        column = "ConAmount";
                        columnComp = "txtConAmount";

                        break;
                    case "SUB":
                        column = "SubAmount";
                        columnComp = "txtSubAmount";

                        break;
                }

                var txtAmount = (RadNumericTextBox)item[column].FindControl(columnComp);

                if (!txtAmount.Enabled)
                {
                    continue;
                }

                var value = txtAmount.Value == null ? 0 : (decimal)txtAmount.Value;
                budget.Amount = value;
            }

            foreach (var bud in this.Budgets.Where(b => b.Level.Equals("CON") && b.HasSons))
            {
                decimal totalConcept = 0;

                foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(bud.Chapter) && b.Article.Equals(bud.Article) && b.Concept.Equals(bud.Concept) && b.Level.Equals("SUB")).ToList())
                {
                    totalConcept += budget.Amount;
                }

                bud.Amount = totalConcept;
            }

            foreach (var bud in this.Budgets.Where(b => b.Level.Equals("ART") && b.HasSons))
            {
                decimal totalArticle = 0;

                foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(bud.Chapter) && b.Article.Equals(bud.Article) && b.Level.Equals("CON")).ToList())
                {
                    totalArticle += budget.Amount;
                }

                bud.Amount = totalArticle;
            }

            foreach (var bud in this.Budgets.Where(b => b.Level.Equals("CAP") && b.HasSons))
            {
                decimal totalChapter = 0;

                foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(bud.Chapter) && b.Level.Equals("ART")).ToList())
                {
                    totalChapter += budget.Amount;
                }

                bud.Amount = totalChapter;
            }

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.Amount);
            this.txtTotal.Text = total.ToString("N", new CultureInfo("es-ES"));

            this.UpdateGridValues();
        }

        #endregion

    }
}