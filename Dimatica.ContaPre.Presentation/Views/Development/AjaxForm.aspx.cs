namespace Dimatica.ContaPre.Presentation.Views.Development
{
    #region NameSpaces

    using System;
    using System.Web.Script.Serialization;
    using System.Web.Services;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class AjaxForm : BasePage
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "EnviarEmail";
            }

        }

        [WebMethod]
        public static string ProcessRequest(string nombre)
        {
            var response = new
            {
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                nombre = nombre
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(response);
        }

        #endregion
    }
}