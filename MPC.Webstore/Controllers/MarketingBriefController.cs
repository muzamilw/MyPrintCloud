using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using System.Runtime.Caching;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class MarketingBriefController : Controller
    {
        #region Private

        private readonly IItemService _IItemService;
        private readonly ICampaignService _ICampaignService;
        private readonly ICompanyService _ICompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IUserManagerService _IUserManagerService;
    
        private int count = 1;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarketingBriefController(IItemService itemService, IWebstoreClaimsHelperService myClaimHelper, ICampaignService CampaignService, IUserManagerService UserManagerService, ICompanyService CompanyService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }

           
            this._IItemService = itemService;
            this._myClaimHelper = myClaimHelper;
            this._ICampaignService = CampaignService;
            this._IUserManagerService = UserManagerService;
            this._ICompanyService = CompanyService;
        }
        #endregion
        // GET: MarketingBrief
        public ActionResult Index(string ProductName, int ItemID, long CategoryId)
        {
            ViewBag.SuccessMessage = "0";
            string ContactMobile = _ICompanyService.GetContactMobile(_myClaimHelper.loginContactID());

            ViewBag.Phone = ContactMobile;
            ProductItem Product = _IItemService.GetItemAndDetailsByItemID(ItemID);
            if (CategoryId == 0)
            {
                Product.ProductCategoryName = _IItemService.GetCategoryNameById(0, ItemID);
                ViewBag.CategoryHRef = "/Category/" + Utils.specialCharactersEncoder(Product.ProductCategoryName) + "/" + CategoryId;
            }
            else
            {
                Product.ProductCategoryName = _IItemService.GetCategoryNameById(CategoryId, 0);
                ViewBag.CategoryHRef = "/Category/" + Utils.specialCharactersEncoder(Product.ProductCategoryName) + "/" + CategoryId;
            }
            Product.ProductName = ProductName + " - Marketing Brief";
            Product.ProductCategoryID = CategoryId;
            List<ProductMarketBriefAnswer> NS = new List<ProductMarketBriefAnswer>();

            List<ProductMarketBriefQuestion> QuestionsList = _IItemService.GetMarketingInquiryQuestionsByItemID(ItemID);
            if (QuestionsList.Count > 0)
            {
                ViewData["QuestionsList"] = QuestionsList;
                if (QuestionsList != null)
                {
                    foreach (var question in QuestionsList)
                    {
                        List<ProductMarketBriefAnswer> Ans = _IItemService.GetMarketingInquiryAnswersByQID(question.MarketBriefQuestionId);
                       
                        foreach(ProductMarketBriefAnswer val in Ans)
                        {
                            NS.Add(val);
                        }

                 
                    }
                    if (NS.Count > 0)
                        ViewData["Answers"] = NS;
                }
               
            }

            return View("PartialViews/MarketingBrief",Product);
        }

        [HttpPost]
        public ActionResult Index(ProductItem Model, string hfInqueryMesg)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            List<string> Attachments = null;
            

            Model.ProductCategoryName = _IItemService.GetCategoryNameById(0, Model.ItemID);
            ViewBag.CategoryHRef = "/Category/" + Utils.specialCharactersEncoder(Model.ProductCategoryName) + "/" + _IItemService.GetCategoryIdByItemId(Model.ItemID);
           
            if (Request.Files.Count > 0)
            {
                List<string> filesNamesPaths = new List<string>();

                if (Request.Files != null)
                {
                    Attachments = new List<string>();
                    string folderPath = ""; // from file table // @Components.ImagePathConstants.EmailAttachmentPath;
                    string virtualFolderPth = string.Empty;

                    virtualFolderPth = @Server.MapPath(folderPath);
                    if (!System.IO.Directory.Exists(virtualFolderPth))
                        System.IO.Directory.CreateDirectory(virtualFolderPth);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase postedFile = Request.Files[i];

                        string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetFileName(postedFile.FileName));

                        Attachments.Add(folderPath + fileName);
                        postedFile.SaveAs(virtualFolderPth + fileName);
                    }
                }
            }
            string MEsg = string.Empty;
          
            ProductItem Item = _IItemService.GetItemAndDetailsByItemID(Model.ItemID);
           // MyCompanyDomainBaseResponse baseResponse = _ICompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

          //  MyCompanyDomainBaseResponse baseResponseorg = _ICompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _ICompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            string ContactMobile = _ICompanyService.GetContactMobile(_myClaimHelper.loginContactID());

            Organisation org = _ICompanyService.GetOrganisatonById(StoreBaseResopnse.Organisation.OrganisationId);
            if (Item != null)
            {
                MEsg += "Product : " + Item.ProductName + "<br />";
                MEsg += "Product Category : " + Item.ProductCategoryName + "<br />";
            }


            MEsg += "Customer name :  " + StoreBaseResopnse.Company.Name + "<br />";
            MEsg += "Contact/user :   " + UserCookieManager.WEBContactFirstName + " " + UserCookieManager.WEBContactLastName + "<br />";
            MEsg += "Email  :  " + UserCookieManager.WEBEmail + "<br />";
            MEsg += "Phone  :  " + ContactMobile + "<br /> <br />";
            MEsg += "Marketing Brief submitted on  :  " + DateTime.Now.ToString("MMMM dd, yyyy") + "<br /> <br />";

          


        
            string MesgBodyList = Request.Form["hfInqueryMesg"];
            char[] separator = new char[] { '|' };
            List<string> ansList = MesgBodyList.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < ansList.Count; i++)
            {
                MEsg += ansList[i].Replace("\n", "<br />") + "<br /> <br />";
            }

            if (Attachments != null)
            {
                MEsg += "Please also see the attachments." + "<br />";
            }

         
            string SecondEmail = _IUserManagerService.GetMarketingRoleIDByName();
            Campaign EventCampaign = _ICampaignService.GetCampaignRecordByEmailEvent((int)Events.SendInquiry, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);

            CampaignEmailParams EmailParams = new CampaignEmailParams();
            EmailParams.ContactId = _myClaimHelper.loginContactID();
            EmailParams.CompanyId = UserCookieManager.WBStoreId;
            EmailParams.OrganisationId = 1;

            EmailParams.MarketingID = 1;

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                EmailParams.StoreId = (int)UserCookieManager.WBStoreId;
                EmailParams.SalesManagerContactID = _myClaimHelper.loginContactID();
                int OID = (int)org.OrganisationId;
                _ICampaignService.emailBodyGenerator(EventCampaign, EmailParams, null, StoreMode.Retail, OID, "", "", "", StoreBaseResopnse.Company.MarketingBriefRecipient, StoreBaseResopnse.Company.Name, SecondEmail, Attachments, "", null, "", "", "", MEsg, "", 0, "", 0);
                
               
            }
            else
            {
                string Email = string.Empty;
                SystemUser SalesMagerRec = _IUserManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
                if (SalesMagerRec != null)
                {
                    EmailParams.SystemUserId = SalesMagerRec.SystemUserId;
                    Email = SalesMagerRec.Email;
                }
                else
                {
                    EmailParams.SystemUserId = null;
                  
                }
                EmailParams.StoreId = UserCookieManager.WBStoreId;
                EmailParams.SalesManagerContactID = _myClaimHelper.loginContactID();
               
                 
                int OID = (int)org.OrganisationId;
                _ICampaignService.emailBodyGenerator(EventCampaign, EmailParams, null, StoreMode.Retail, OID, "", "", "", Email, "", "", Attachments, "", null, "", "", "", MEsg, "", 0, "", 0);

               
            }

            //if (Item != null)
            //{
               // if (!string.IsNullOrEmpty(Item.BriefSuccessMessage))
               // {
            ViewBag.SuccessMessage = Item.BriefSuccessMessage;// "Thank you for your order. Marketing will review your brief within 24-48 hours and if approved design will have the first proof back to you in 3 business days. <br /> <br /> If your brief is not approved, marketing will be in contact with you.";
               // }

           // }

            ViewBag.IsSubmitSuccessfully = true;
          
            StoreBaseResopnse = null;
            return View("PartialViews/MarketingBrief",Item);
        }
    }
}