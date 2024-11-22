namespace Dimatica.ContaPre.Presentation.Views.Maintenance
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class NewApplications : BasePage
    {
        #region Fields

        IAccountPgcpService accountService = DependencyFactory.GetInstance<IAccountPgcpService>();

        IApplicationService applicationService = DependencyFactory.GetInstance<IApplicationService>();

        #endregion

        #region Public Properties

        public string Type
        {
            get
            {
                var type = this.Session["_type"];

                if (type == null)
                {
                    type = string.Empty;
                    this.Session["_type"] = type;
                }

                return type.ToString();
            }

            set
            {
                this.Session["_type"] = value;
            }
        }

        public string Status
        {
            get
            {
                var status = this.Session["_status"];

                if (status == null)
                {
                    status = string.Empty;
                    this.Session["_status"] = status;
                }

                return status.ToString();
            }

            set
            {
                this.Session["_status"] = value;
            }
        }

        public string Level
        {
            get
            {
                var level = this.Session["_level"];

                if (level == null)
                {
                    level = string.Empty;
                    this.Session["_level"] = level;
                }

                return level.ToString();
            }

            set
            {
                this.Session["_level"] = value;
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
                var type = this.Request.QueryString["type"];

                if (string.IsNullOrWhiteSpace(type))
                {
                    this.Response.Redirect("~//Views//Maintenance//Applications.aspx");
                }
                else
                {
                    this.Type = type;
                    this.Status = "view";

                    this.FillChapters();

                    this.FillArticles(string.Empty);

                    this.FillConcepts(string.Empty);

                    this.FillSubconcepts(string.Empty);

                    this.ValidateComponents();
                }
            }
        }


        protected void RcChapters_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var chapter = e.Value.Equals("-1") ? string.Empty : e.Value;

            this.FillArticles(chapter);

            this.FillConcepts(string.Empty);

            this.FillSubconcepts(string.Empty);
        }

        protected void RcArticles_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var article = e.Value.Equals("-1") ? string.Empty : e.Value;

            this.FillConcepts(article);

            this.FillSubconcepts(string.Empty);
        }

        protected void RcConcepts_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var concept = e.Value.Equals("-1") ? string.Empty : e.Value;

            this.FillSubconcepts(concept);
        }

        private void FillChapters()
        {
            var chapters = this.applicationService.GetChapters(this.Type);

            chapters.Insert(
                            0,
                            new PRE_CAPITULO
                                    {
                                            CAP_CODIGO_AUX = -1,
                                            CAPITULO_LABEL = "< Seleccione >"
                                    });

            this.RcChapters.DataSource = chapters;
            this.RcChapters.DataBind();
        }

        private void FillArticles(string chapter)
        {
            var articles = new List<PRE_ARTICULO>();

            if (!string.IsNullOrWhiteSpace(chapter))
            {
                articles = this.applicationService.GetArticles(this.Type, Convert.ToInt32(chapter));
            }

            articles.Insert(
                            0,
                            new PRE_ARTICULO
                            {
                                ART_CODIGO_AUX = -1,
                                ARTICULO_LABEL = "< Seleccione >"
                            });

            this.RcArticles.DataSource = articles;
            this.RcArticles.DataBind();
        }

        private void FillConcepts(string article)
        {
            var concepts = new List<PRE_CONCEPTO>();

            if (!string.IsNullOrWhiteSpace(article))
            {
                concepts = this.applicationService.GetConcepts(this.Type, Convert.ToInt32(article));
            }

            concepts.Insert(
                            0,
                            new PRE_CONCEPTO
                            {
                                CON_CODIGO_AUX = -1,
                                CONCEPTO_LABEL = "< Seleccione >"
                            });

            this.RcConcepts.DataSource = concepts;
            this.RcConcepts.DataBind();
        }

        private void FillSubconcepts(string concept)
        {
            var subconcepts = new List<PRE_SUBCONCEPTO>();

            if (!string.IsNullOrWhiteSpace(concept))
            {
                subconcepts = this.applicationService.GetSubconcepts(this.Type, Convert.ToInt32(concept));
            }

            subconcepts.Insert(
                               0,
                               new PRE_SUBCONCEPTO
                               {
                                   SUB_CODIGO_AUX = -1,
                                   SUBCONCEPTO_LABEL = "< Seleccione >"
                               });

            this.RcSubconcepts.DataSource = subconcepts;
            this.RcSubconcepts.DataBind();
        }

        private void ValidateComponents()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideGrids", $"hideGrids('{this.Status}');", true);

            switch (this.Status)
            {
                case "view":
                    this.RcChapters.Enabled = true;
                    this.btnNewChapter.Enabled = true;
                    this.RcArticles.Enabled = true;
                    this.btnNewArticle.Enabled = !this.RcChapters.SelectedValue.Equals("-1");
                    this.RcConcepts.Enabled = true;
                    this.btnNewConcept.Enabled = !this.RcArticles.SelectedValue.Equals("-1");
                    this.RcSubconcepts.Enabled = true;
                    this.btnNewSubconcept.Enabled = !this.RcConcepts.SelectedValue.Equals("-1");

                    break;
                case "edit":
                    this.RcChapters.Enabled = false;
                    this.btnNewChapter.Enabled = false;
                    this.RcArticles.Enabled = false;
                    this.btnNewArticle.Enabled = false;
                    this.RcConcepts.Enabled = false;
                    this.btnNewConcept.Enabled = false;
                    this.RcSubconcepts.Enabled = false;
                    this.btnNewSubconcept.Enabled = false;

                    break;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            switch (this.Level)
            {
                case "CAP":
                    var chapter = new PRE_CAPITULO
                    {
                        CAP_I_G = this.Type,
                        CAP_NUMERO = Convert.ToByte(this.txtNumber.Text),
                        CAP_DESCRIPCION = this.txtDescription.Text,
                        CUEP_CODIGO = this.radDropAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.radDropAccount.SelectedValue),
                        USU_CODIGO = LoginUser.USU_CODIGO
                    };

                    try
                    {
                        var insert = this.applicationService.InsertChapter(chapter);

                        var message = string.Empty;
                        switch (insert.ResponseCode)
                        {

                            case ResponseCode.Invalid:
                                message = "Debe completar todos los datos del Capítulo.";

                                break;
                            case ResponseCode.Found:
                                message = "Ya existe un Capítulo con ese número.";

                                break;
                        }

                        if (insert.ResponseCode != ResponseCode.Ok)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('{message}', 'Error insertanto el Capítulo');", true);
                        }
                        else
                        {
                            this.FillChapters();
                            this.FillArticles(string.Empty);
                            this.FillConcepts(string.Empty);
                            this.FillSubconcepts(string.Empty);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex, "Error insertando el capítulo.");
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('Ha ocurrido un error inesperado insertando el Capítulo en cuestión.', 'Error insertanto el Capítulo');", true);
                    }
                    break;
                case "ART":
                    var article = new PRE_ARTICULO
                                          {
                                                  ART_I_G = this.Type,
                                                  ART_NUMERO = Convert.ToByte(this.txtNumber.Text),
                                                  ART_DESCRIPCION = this.txtDescription.Text,
                                                  CAP_CODIGO = Convert.ToByte(this.RcChapters.SelectedValue),
                                                  CUEP_CODIGO = this.radDropAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.radDropAccount.SelectedValue),
                                                  USU_CODIGO = LoginUser.USU_CODIGO
                                          };

                    try
                    {
                        var insert = this.applicationService.InsertArticle(article);

                        var message = string.Empty;
                        switch (insert.ResponseCode)
                        {

                            case ResponseCode.Invalid:
                                message = "Debe completar todos los datos del Artículo.";

                                break;
                            case ResponseCode.Found:
                                message = "Ya existe un Artículo con ese número.";

                                break;
                        }

                        if (insert.ResponseCode != ResponseCode.Ok)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('{message}', 'Error insertanto el Artículo');", true);
                        }
                        else
                        {
                            this.FillArticles(this.RcChapters.SelectedValue);
                            this.FillConcepts(string.Empty);
                            this.FillSubconcepts(string.Empty);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex, "Error insertando el artículo.");
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('Ha ocurrido un error inesperado insertando el Artículo en cuestión.', 'Error insertanto el Artículo');", true);
                    }
                    break;
                case "CON":
                    var concept = new PRE_CONCEPTO
                                          {
                                                  CON_I_G = this.Type,
                                                  CON_NUMERO = Convert.ToByte(this.txtNumber.Text),
                                                  CON_DESCRIPCION = this.txtDescription.Text,
                                                  ART_CODIGO = Convert.ToByte(this.RcArticles.SelectedValue),
                                                  CUEP_CODIGO = this.radDropAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.radDropAccount.SelectedValue),
                                                  USU_CODIGO = LoginUser.USU_CODIGO
                                          };

                    try
                    {
                        var insert = this.applicationService.InsertConcept(concept);

                        var message = string.Empty;
                        switch (insert.ResponseCode)
                        {

                            case ResponseCode.Invalid:
                                message = "Debe completar todos los datos del Concepto.";

                                break;
                            case ResponseCode.Found:
                                message = "Ya existe un Concepto con ese número.";

                                break;
                        }

                        if (insert.ResponseCode != ResponseCode.Ok)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('{message}', 'Error insertanto el Artículo');", true);
                        }
                        else
                        {
                            this.FillConcepts(this.RcArticles.SelectedValue);
                            this.FillSubconcepts(string.Empty);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex, "Error insertando el concepto.");
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('Ha ocurrido un error inesperado insertando el Concepto en cuestión.', 'Error insertanto el Concepto');", true);
                    }
                    break;
                case "SUB":
                    var subconcept = new PRE_SUBCONCEPTO
                                          {
                                                  SUB_I_G = this.Type,
                                                  SUB_NUMERO = this.txtNumber.Text,
                                                  SUB_DESCRIPCION = this.txtDescription.Text,
                                                  CON_CODIGO = Convert.ToInt32(this.RcConcepts.SelectedValue),
                                                  CUEP_CODIGO = this.radDropAccount.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.radDropAccount.SelectedValue),
                                                  USU_CODIGO = LoginUser.USU_CODIGO
                                          };

                    try
                    {
                        var insert = this.applicationService.InsertSuboncept(subconcept);
                        var message = string.Empty;
                        switch (insert.ResponseCode)
                        {

                            case ResponseCode.Invalid:
                                message = "Debe completar todos los datos del Suboncepto.";

                                break;
                            case ResponseCode.Found:
                                message = "Ya existe un Suboncepto con ese número.";

                                break;
                        }

                        if (insert.ResponseCode != ResponseCode.Ok)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('{message}', 'Error insertanto el Artículo');", true);
                        }
                        else
                        {
                            this.FillSubconcepts(this.RcConcepts.SelectedValue);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex, "Error insertando el subconcepto.");
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", $"showError('Ha ocurrido un error inesperado insertando el Suboncepto en cuestión.', 'Error insertanto el Suboncepto');", true);
                    }
                    break;
            }

            this.Status = "view";
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            this.Status = "view";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.ValidateComponents();
        }

        protected void btnNewChapter_OnClick(object sender, EventArgs e)
        {
            this.currentAction.InnerText = "Nuevo Capítulo";
            this.currentLabel.InnerText = "Capítulo";
            this.currentParent.InnerText = string.Empty;
            this.Level = "CAP";
            this.ResetNew();
            this.Status = "edit";
        }

        protected void btnNewArticle_OnClick(object sender, EventArgs e)
        {
            this.currentAction.InnerText = "Nuevo Artículo";
            this.currentLabel.InnerText = "Artículo";
            this.currentParent.InnerText = this.RcChapters.SelectedItem.Text.Split('-')[0].Trim();
            this.Level = "ART";
            this.ResetNew();
            this.Status = "edit";
        }

        protected void btnNewConcept_OnClick(object sender, EventArgs e)
        {
            this.currentAction.InnerText = "Nuevo Concepto";
            this.currentLabel.InnerText = "Concepto";
            this.currentParent.InnerText = this.RcArticles.SelectedItem.Text.Split('-')[0].Trim();
            this.Level = "CON";
            this.ResetNew();
            this.Status = "edit";
        }

        protected void btnNewSubconcept_OnClick(object sender, EventArgs e)
        {
            this.currentAction.InnerText = "Nuevo Subconcepto";
            this.currentLabel.InnerText = "Subconcepto";
            this.currentParent.InnerText = this.RcConcepts.SelectedItem.Text.Split('-')[0].Trim();
            this.Level = "SUB";
            this.ResetNew();
            this.Status = "edit";
        }

        private void ResetNew()
        {
            this.txtNumber.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
            var accounts = this.accountService.GetAccountsToCombo();
            accounts.Insert(
                            0,
                            new PRE_CUENTA_PGCP
                            {
                                CUEP_CODIGO = -1,
                                CUEP_NUMERO = "< Seleccione >"
                            });
            this.radDropAccount.DataSource = accounts;
            this.radDropAccount.DataBind();

            switch (this.Level)
            {
                case "CAP":
                    this.txtNumber.MaxLength = 3;
                    this.txtDescription.MaxLength = 60;

                    break;
                case "ART":
                    this.txtNumber.MaxLength = 3;
                    this.txtDescription.MaxLength = 100;

                    break;
                case "CON":
                    this.txtNumber.MaxLength = 3;
                    this.txtDescription.MaxLength = 120;

                    break;
                case "SUB":
                    this.txtNumber.MaxLength = 2;
                    this.txtDescription.MaxLength = 100;

                    break;
            }
        }

        #endregion
    }
}