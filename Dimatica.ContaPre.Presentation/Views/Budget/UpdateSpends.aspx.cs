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
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class UpdateSpends : BasePage
    {
        #region Fields

        IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        IProgramsService programsService = DependencyFactory.GetInstance<IProgramsService>();

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

        public string Year
        {
            get
            {
                var o = this.Session["_year"];

                if (o == null)
                {
                    o = string.Empty;
                    this.Session["_year"] = o;
                }

                return o.ToString();
            }

            set
            {
                this.Session["_year"] = value;
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

        private List<PRE_PROGRAMA> Programs
        {
            get
            {
                var programs = this.Session["_programs"] as List<PRE_PROGRAMA>;

                if (programs == null)
                {
                    programs = new List<PRE_PROGRAMA>();
                    this.Session["_programs"] = programs;
                }

                return programs;
            }

            set
            {
                this.Session["_programs"] = value;
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
                var year = this.Request.QueryString["year"];

                if (string.IsNullOrWhiteSpace(year))
                {
                    this.Response.Redirect("~//Views//Budget//Spends.aspx");
                }
                else
                {
                    this.Verify = false;

                    this.Year = year;

                    this.Programs = new List<PRE_PROGRAMA>();
                    var programs = this.programsService.GetPrograms();

                    foreach (var program in programs)
                    {
                        this.Programs.Add(program);
                    }

                    this.Budgets = new List<Budget>();
                    var budgets = this.budgetsService.GetBudgetsByType(Convert.ToInt32(this.Year), "G");

                    foreach (var budget in budgets)
                    {
                        this.Budgets.Add(budget);
                    }

                    var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.FinalAmount);
                    this.txtTotal.Text = total.ToString("N", new CultureInfo("es-ES"));

                    this.RgUpdateSpends.MasterTableView.GetColumn("UpdateAmountSub").Visible = false;
                    this.RgUpdateSpends.MasterTableView.GetColumn("UpdateAmountCon").Visible = false;
                    this.RgUpdateSpends.MasterTableView.GetColumn("UpdateAmountChaArt").Visible = false;
                }
            }
        }

        protected void RgUpdateSpends_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.RgUpdateSpends.DataSource = this.Budgets;
        }

    
        protected void btnCalculate_OnClick(object sender, EventArgs e)
        {
            this.CalculateTotals();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            this.CalculateTotals();

            var result = 1;

            try
            {
                foreach (GridDataItem item in this.RgUpdateSpends.MasterTableView.Items)
                {
                    var level = item.GetDataKeyValue("Level").ToString();

                    var chapter = item.GetDataKeyValue("Chapter").ToString();
                    var article = item.GetDataKeyValue("Article").ToString();
                    var concept = item.GetDataKeyValue("Concept").ToString();
                    var subConcept = item.GetDataKeyValue("SubConcept").ToString();

                    var checkNotBinding = (RadCheckBox)item["NotBinding"].FindControl("checkNotBinding");

                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).NotBinding = (bool)checkNotBinding.Checked;

                    if (level.Equals("CAP"))
                    {
                        var rcPrograms = (RadComboBox)item["Program"].FindControl("RcPrograms");

                        this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).ProgramId = Convert.ToInt32(rcPrograms.SelectedValue);
                    }

                    var amount = this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).Amount;
                    var finalAmount = this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).FinalAmount;
                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).UpdateAmount = finalAmount - amount;
                }

                foreach (var budget in this.Budgets)
                {
                    if (budget.Level.Equals("CAP"))
                    {
                        continue;
                    }

                    budget.ProgramId = this.Budgets.FirstOrDefault(b => b.ChapterId == budget.ChapterId && b.Level.Equals("CAP")).ProgramId;
                }

                var update = this.budgetsService.UpdateMany("G", this.Budgets, LoginUser.USU_CODIGO);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        result = -1;

                        break;
                    case ResponseCode.NotFound:
                        result = 2;

                        break;
                }
            }
            catch (Exception ex)
            {
                result = -1;
                LogError(ex, "Error modificando el presupuesto de gastos.");
            }

            var script = $"OnSaveSuccess({result});";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "onSaveSuccess", script, true);
        }

        protected void RgUpdateSpends_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;
                var level = dataBoundItem.GetDataKeyValue("Level").ToString();
                var chapter = dataBoundItem.GetDataKeyValue("Chapter").ToString();
                var article = dataBoundItem.GetDataKeyValue("Article").ToString();
                var concept = dataBoundItem.GetDataKeyValue("Concept").ToString();
                var subConcept = dataBoundItem.GetDataKeyValue("SubConcept").ToString();

                switch (level)
                {
                    case "CAP":
                        ((RadNumericTextBox)dataBoundItem["UpdateSubAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateConAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateChaArtAmount"].Controls[1]).Style.Add("Visibility", "visible !important");

                        if (this.Budgets.Any(b => b.Chapter.Equals(chapter) && string.IsNullOrEmpty(b.Article) == false))
                        {
                            ((RadNumericTextBox)dataBoundItem["UpdateChaArtAmount"].Controls[1]).Enabled = false;
                            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).HasSons = true;
                        }

                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#e8e8e8");

                        var rcPrograms = (RadComboBox)dataBoundItem["Program"].FindControl("RcPrograms");
                        rcPrograms.DataSource = this.Programs;
                        rcPrograms.DataBind();

                        rcPrograms.SelectedValue = this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept) && b.Level.Equals(level)).ProgramId.ToString();

                        break;
                    case "ART":
                        ((RadNumericTextBox)dataBoundItem["UpdateSubAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateConAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateChaArtAmount"].Controls[1]).Style.Add("Visibility", "visible !important");

                        if (this.Budgets.Any(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && string.IsNullOrEmpty(b.Concept) == false))
                        {
                            ((RadNumericTextBox)dataBoundItem["UpdateChaArtAmount"].Controls[1]).Enabled = false;
                            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).HasSons = true;
                        }

                        dataBoundItem.Style.Add("font-weight", "bold");
                        dataBoundItem.BackColor = ColorTranslator.FromHtml("#f5f5f5");

                        ((RadComboBox)dataBoundItem["Program"].Controls[1]).Style.Add("Visibility", "hidden !important");

                        break;
                    case "CON":
                        ((RadNumericTextBox)dataBoundItem["UpdateSubAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateConAmount"].Controls[1]).Style.Add("Visibility", "visible !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateChaArtAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");

                        if (this.Budgets.Any(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && string.IsNullOrEmpty(b.SubConcept) == false))
                        {
                            ((RadNumericTextBox)dataBoundItem["UpdateConAmount"].Controls[1]).Enabled = false;
                            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("CON")).HasSons = true;
                        }

                        ((RadComboBox)dataBoundItem["Program"].Controls[1]).Style.Add("Visibility", "hidden !important");

                        break;
                    case "SUB":
                        ((RadNumericTextBox)dataBoundItem["UpdateSubAmount"].Controls[1]).Style.Add("Visibility", "visible !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateConAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");
                        ((RadNumericTextBox)dataBoundItem["UpdateChaArtAmount"].Controls[1]).Style.Add("Visibility", "hidden !important");

                        ((RadComboBox)dataBoundItem["Program"].Controls[1]).Style.Add("Visibility", "hidden !important");

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

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept)).FinalAmount = value;

            decimal totalConcept = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("SUB")).ToList())
            {
                totalConcept += budget.FinalAmount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("CON")).FinalAmount = totalConcept;

            decimal totalArticle = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("CON")).ToList())
            {
                totalArticle += budget.FinalAmount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).FinalAmount = totalArticle;

            decimal totalChapter = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Level.Equals("ART")).ToList())
            {
                totalChapter += budget.FinalAmount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).FinalAmount = totalChapter;

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.FinalAmount);
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

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.Level.Equals("CON")).FinalAmount = value;

            decimal totalArticle = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("CON")).ToList())
            {
                totalArticle += budget.FinalAmount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).FinalAmount = totalArticle;

            decimal totalChapter = 0;

            foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Level.Equals("ART")).ToList())
            {
                totalChapter += budget.FinalAmount;
            }

            this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).FinalAmount = totalChapter;

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.FinalAmount);
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
                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).FinalAmount = (decimal)txtAmount.Value;

                    break;
                case "ART":
                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Level.Equals("ART")).FinalAmount = value;

                    decimal totalChapter = 0;

                    foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(chapter) && b.Level.Equals("ART")).ToList())
                    {
                        totalChapter += budget.FinalAmount;
                    }

                    this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Level.Equals("CAP")).FinalAmount = totalChapter;

                    break;
            }

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.FinalAmount);
            this.txtTotal.Text = total.ToString("N", new CultureInfo("es-ES"));

            this.UpdateGridValues();
        }

        private void UpdateGridValues()
        {
            foreach (GridDataItem item in this.RgUpdateSpends.MasterTableView.Items)
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
                        var rcPrograms = (RadComboBox)item["Program"].FindControl("RcPrograms");
                        this.Budgets.FirstOrDefault(b => b.Chapter.Equals(chapter) && b.Article.Equals(article) && b.Concept.Equals(concept) && b.SubConcept.Equals(subConcept)).ProgramId = Convert.ToInt32(rcPrograms.SelectedValue);
                        column = "UpdateChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "ART":
                        column = "UpdateChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "CON":
                        column = "UpdateConAmount";
                        columnComp = "txtConAmount";

                        break;
                    case "SUB":
                        column = "UpdateSubAmount";
                        columnComp = "txtSubAmount";

                        break;
                }

                var txtAmount = (RadNumericTextBox)item[column].FindControl(columnComp);

                txtAmount.Value = (double)budget.FinalAmount;
            }

            this.RgUpdateSpends.Rebind();
        }


        private void CalculateTotals()
        {
            foreach (GridDataItem item in this.RgUpdateSpends.MasterTableView.Items)
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
                        column = "UpdateChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "ART":
                        column = "UpdateChaArtAmount";
                        columnComp = "txtChaArtAmount";

                        break;
                    case "CON":
                        column = "UpdateConAmount";
                        columnComp = "txtConAmount";

                        break;
                    case "SUB":
                        column = "UpdateSubAmount";
                        columnComp = "txtSubAmount";

                        break;
                }

                var txtAmount = (RadNumericTextBox)item[column].FindControl(columnComp);

                if (!txtAmount.Enabled)
                {
                    continue;
                }

                var value = txtAmount.Value == null ? 0 : (decimal)txtAmount.Value;
                budget.FinalAmount = value;
            }

            foreach (var bud in this.Budgets.Where(b => b.Level.Equals("CON") && b.HasSons))
            {
                decimal totalConcept = 0;

                foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(bud.Chapter) && b.Article.Equals(bud.Article) && b.Concept.Equals(bud.Concept) && b.Level.Equals("SUB")).ToList())
                {
                    totalConcept += budget.FinalAmount;
                }

                bud.FinalAmount = totalConcept;
            }

            foreach (var bud in this.Budgets.Where(b => b.Level.Equals("ART") && b.HasSons))
            {
                decimal totalArticle = 0;

                foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(bud.Chapter) && b.Article.Equals(bud.Article) && b.Level.Equals("CON")).ToList())
                {
                    totalArticle += budget.FinalAmount;
                }

                bud.FinalAmount = totalArticle;
            }

            foreach (var bud in this.Budgets.Where(b => b.Level.Equals("CAP") && b.HasSons))
            {
                decimal totalChapter = 0;

                foreach (var budget in this.Budgets.Where(b => b.Chapter.Equals(bud.Chapter) && b.Level.Equals("ART")).ToList())
                {
                    totalChapter += budget.FinalAmount;
                }

                bud.FinalAmount = totalChapter;
            }

            var total = this.Budgets.Where(b => b.Level.Equals("CAP")).Sum(b => b.FinalAmount);
            this.txtTotal.Text = total.ToString("N", new CultureInfo("es-ES"));

            this.UpdateGridValues();
        }

        #endregion
    }
}