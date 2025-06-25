
using System;
using Dimatica.ContaPre.BLL.Services;
using Dimatica.ContaPre.OL.Models;

namespace Dimatica.ContaPre.Presentation.Views.Shared.Components.Partials.Providers
{
    public partial class NewProvider : System.Web.UI.Page
    {
        private readonly ProvincesService provincesService = new ProvincesService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProvinces();
            }
        }

        private void LoadProvinces()
        {

            var provinces = this.provincesService.GetProvincesToCombo();

            provinces.Insert(0, new Provincia { ProvIdInt = -1, Provincia1 = "-- Seleccione --" });
            ddlProvince.DataSource = provinces;
            ddlProvince.DataTextField = "Provincia1"; 
            ddlProvince.DataValueField = "ProvIdInt"; 
            ddlProvince.DataBind();

        }


    }
}