namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeeStatusRecord : BasePage
    {
        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        private IExtraBudgetaryRecordsService extraBudgetaryRecordsService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        #endregion

        #region Public Properties

        public int FileId
        {
            get
            {
                var o = this.Session["_fileId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_fileId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_fileId"] = value;
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
                var fileId = this.Request.QueryString["fileId"];

                if (string.IsNullOrWhiteSpace(fileId))
                {
                    this.Response.Redirect("~//Views//Spend//SpendRecords.aspx");
                }
                else
                {
                    this.FileId = Convert.ToInt32(fileId);
                }
            }
        }

        protected void RgResume_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var resume = new List<StatusRecord>();

            if (this.FileId != 0)
            {
                var rcPositiveAmount = this.accountingRecordsService.GetRcAmount(this.FileId, true, null);
                var rcNegativeAmount = this.accountingRecordsService.GetRcAmount(this.FileId, false, null);

                var adPositiveAmount = this.accountingRecordsService.GetAdAmount(this.FileId, true, null);
                var adNegativeAmount = this.accountingRecordsService.GetAdAmount(this.FileId, false, null);

                var oPositiveAmount = this.accountingRecordsService.GetOAmount(this.FileId, true, null);
                var oNegativeAmount = this.accountingRecordsService.GetOAmount(this.FileId, false, null);

                var pPositiveAmount = this.accountingRecordsService.GetPAmount(this.FileId, true, null);
                var pNegativeAmount = this.accountingRecordsService.GetPAmount(this.FileId, false, null);

                var rcPending = (rcPositiveAmount - rcNegativeAmount) - (adPositiveAmount - adNegativeAmount);
                var adPending = (adPositiveAmount - adNegativeAmount) - (oPositiveAmount - oNegativeAmount);
                var oPending = (oPositiveAmount - oNegativeAmount) - (pPositiveAmount - pNegativeAmount);

                var iDiscounts = this.accountingRecordsService.GetDiscountIEcAmount(this.FileId);
                var eeDiscounts = this.extraBudgetaryRecordsService.GetSumAmount(this.FileId);

                var discounts = iDiscounts - eeDiscounts;
                var rcLessP = (rcPositiveAmount - rcNegativeAmount) - (pPositiveAmount - pNegativeAmount);

                if (rcPositiveAmount - rcNegativeAmount == adPositiveAmount - adNegativeAmount)
                {
                    if (adPositiveAmount - rcNegativeAmount == oPositiveAmount - oNegativeAmount)
                    {
                        if (oPositiveAmount - oNegativeAmount == pPositiveAmount - pNegativeAmount)
                        {
                            var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, true, LoginUser.USU_CODIGO);
                        }
                        else
                        {
                            var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                        }
                    }
                    else
                    {
                        var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                    }
                }
                else
                {
                    var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                }

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "RC +",
                                           Accumulated = rcPositiveAmount,
                                           Pending = rcPending
                                   });
                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "RC -",
                                           Accumulated = rcNegativeAmount,
                                           Pending = 0
                                   });

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "AD +",
                                           Accumulated = adPositiveAmount,
                                           Pending = adPending
                                   });
                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "AD -",
                                           Accumulated = adNegativeAmount,
                                           Pending = 0
                                   });

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "O +",
                                           Accumulated = oPositiveAmount,
                                           Pending = oPending
                                   });
                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "O -",
                                           Accumulated = oNegativeAmount,
                                           Pending = 0
                                   });

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "P +",
                                           Accumulated = pPositiveAmount,
                                           Pending = 0
                                   });
                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "P -",
                                           Accumulated = pNegativeAmount,
                                           Pending = 0
                                   });

                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "Descuentos",
                                           Accumulated = 0,
                                           Pending = discounts
                                   });
                resume.Add(
                           new StatusRecord
                                   {
                                           Column = "RC - P",
                                           Accumulated = 0,
                                           Pending = rcLessP
                                   });
            }

            this.RgResume.DataSource = resume;
        }

        protected void RgResume_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataBoundItem = e.Item as GridDataItem;

                dataBoundItem["Column"].ForeColor = Color.White;
                dataBoundItem["Column"].BackColor = ColorTranslator.FromHtml("#3d3e47");
            }
        }

        #endregion
    }
}