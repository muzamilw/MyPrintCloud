using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class MarketingBriefController : Controller
    {
        #region Private

        private readonly IItemService _IItemService;
       

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarketingBriefController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
           
       
            this._IItemService = itemService;
            
        }
        #endregion
        // GET: MarketingBrief
        public ActionResult Index(string ProductName, int ItemID)
        {
            ViewBag.LabelInquiryBrief = ProductName + " - Marketing Brief";


            ProductItem Product = _IItemService.GetItemAndDetailsByItemID(ItemID);


            //List<productm> QuestionsList = PageManager.GetMarketingInquiryQuestionsByItemID(Convert.ToInt32(Request.QueryString["ItemID"]));
            //if (QuestionsList.Count > 0)
            //{
               ViewData["QuestionsList"] = null;
            //    rptQuestionsInquiryBreif.DataSource = QuestionsList.OrderBy(i => i.SortOrder);
            //    rptQuestionsInquiryBreif.DataBind();
            //}

            return View("PartialViews/MarketingBrief");
        }
    }
}