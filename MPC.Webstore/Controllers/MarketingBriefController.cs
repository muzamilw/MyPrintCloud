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
            MyCompanyDomainBaseReponse StoreBaseResopnse = _ICompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            ViewBag.SuccessMessage = "0";
            string ContactMobile = _ICompanyService.GetContactMobile(_myClaimHelper.loginContactID());

            ViewBag.Phone = ContactMobile;
            ProductItem Product = _IItemService.GetItemAndDetailsByItemID(ItemID);

            if (Product != null)
            {
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
                Product.ProductName = ProductName + Utils.GetKeyValueFromResourceFile("lblMarketingBrief", UserCookieManager.WBStoreId, " - Marketing Brief"); //" - Marketing Brief";
                Product.ProductCategoryID = CategoryId;
                List<ProductMarketBriefAnswer> NS = new List<ProductMarketBriefAnswer>();

                List<ProductMarketBriefQuestion> QuestionsList = _IItemService.GetMarketingInquiryQuestionsByItemID(ItemID);
                if (QuestionsList != null && QuestionsList.Count > 0)
                {
                    QuestionsList = QuestionsList.OrderBy(g => g.SortOrder).ToList();
                    ViewData["QuestionsList"] = QuestionsList;
                    if (QuestionsList != null)
                    {
                        foreach (var question in QuestionsList)
                        {
                            List<ProductMarketBriefAnswer> Ans = _IItemService.GetMarketingInquiryAnswersByQID(question.MarketBriefQuestionId);

                            foreach (ProductMarketBriefAnswer val in Ans)
                            {
                                NS.Add(val);
                            }


                        }
                        if (NS.Count > 0)
                            ViewData["Answers"] = NS;
                    }

                }
                if (StoreBaseResopnse.Organisation.SmtpServer != null && StoreBaseResopnse.Organisation.SmtpPassword != null && StoreBaseResopnse.Organisation.SmtpUserName != null)
                {
                    ViewBag.isServerSettingsSet = 1;
                }
                else 
                {
                    ViewBag.isServerSettingsSet = 0;
                }
            }
            else 
            {
                throw new Exception("Product not found.");
            }
            
            return View("PartialViews/MarketingBrief",Product);
        }

        [HttpPost]
        public ActionResult Index(ProductItem Model, string hfInqueryMesg)
        {
            try 
            {
                if(Model == null){
                    throw new Exception("Critcal Error, Model null.", null);
                    return null;

                }
                MyCompanyDomainBaseReponse StoreBaseResopnse = _ICompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                if (StoreBaseResopnse == null)
                {
                    throw new Exception("Critcal Error, StoreBaseResopnse null.", null);
                    return null;

                }
                List<string> Attachments = null;


                Model.ProductCategoryName = _IItemService.GetCategoryNameById(0, Model.ItemID);
                ViewBag.CategoryHRef = "/Category/" + Utils.specialCharactersEncoder(Model.ProductCategoryName) + "/" + _IItemService.GetCategoryIdByItemId(Model.ItemID);

                string filesData = Request.Form["listOfFileName"];
                if (!string.IsNullOrEmpty(filesData))
                {
                    List<string> filesNamesPaths = filesData.Split('|').ToList();
                    Attachments = new List<string>();
                    foreach (string fi in filesNamesPaths)
                    {
                        Attachments.Add("/mpc_content/EmailAttachments/" + fi);
                    }
                }
                string MEsg = string.Empty;

                ProductItem Item = _IItemService.GetItemAndDetailsByItemID(Model.ItemID);


                string ContactMobile = _ICompanyService.GetContactMobile(_myClaimHelper.loginContactID());

                Organisation org = _ICompanyService.GetOrganisatonById(UserCookieManager.WEBOrganisationID);
                if (Item != null)
                {
                    MEsg += Utils.GetKeyValueFromResourceFile("ltrlproductsss", UserCookieManager.WBStoreId, "Product :") + Item.ProductName + "<br />";
                    MEsg += Utils.GetKeyValueFromResourceFile("ltrlprocatgg", UserCookieManager.WBStoreId, "Product Category :") + Item.ProductCategoryName + "<br />";
                }


                MEsg += Utils.GetKeyValueFromResourceFile("ltrlcustname", UserCookieManager.WBStoreId, "Customer name :") + StoreBaseResopnse.Company.Name + "<br />";
                MEsg += Utils.GetKeyValueFromResourceFile("ltrlcontusers", UserCookieManager.WBStoreId, "Contact/user :") + UserCookieManager.WEBContactFirstName + " " + UserCookieManager.WEBContactLastName + "<br />";
                MEsg += Utils.GetKeyValueFromResourceFile("ltrllemail", UserCookieManager.WBStoreId, "Email  :") + UserCookieManager.WEBEmail + "<br />";
                MEsg += Utils.GetKeyValueFromResourceFile("ltrlphh", UserCookieManager.WBStoreId, "Phone  :") + ContactMobile + "<br /> <br />";
                MEsg += Utils.GetKeyValueFromResourceFile("lrlmaeketinbr", UserCookieManager.WBStoreId, "Marketing Brief submitted on  :") + DateTime.Now.ToString("MMMM dd, yyyy") + "<br /> <br />";





                string MesgBodyList = Request.Form["hfInqueryMesg"];
                char[] separator = new char[] { '|' };
                List<string> ansList = MesgBodyList.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

                for (int i = 0; i < ansList.Count; i++)
                {
                    MEsg += ansList[i].Replace("\n", "<br />") + "<br /> <br />";
                }

                if (Attachments != null)
                {
                    MEsg += Utils.GetKeyValueFromResourceFile("plzseeAtt", UserCookieManager.WBStoreId, "Please also see the attachments.") + "<br />";
                }

              
                string SecondEmail = _IUserManagerService.GetMarketingRoleIDByName();
                Campaign EventCampaign = _ICampaignService.GetCampaignRecordByEmailEvent((int)Events.SendInquiry, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);

                CampaignEmailParams EmailParams = new CampaignEmailParams();
                EmailParams.ContactId = _myClaimHelper.loginContactID();
                EmailParams.CompanyId = UserCookieManager.WBStoreId;
                EmailParams.OrganisationId = UserCookieManager.WEBOrganisationID;

                EmailParams.MarketingID = 1;

                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                    EmailParams.StoreId = (int)UserCookieManager.WBStoreId;
                    EmailParams.SalesManagerContactID = _myClaimHelper.loginContactID();
                    int OID = (int)org.OrganisationId;

                    if (StoreBaseResopnse.Company.MarketingBriefRecipient != null)
                    {
                        _ICampaignService.emailBodyGenerator(EventCampaign, EmailParams, null, StoreMode.Retail, OID, "", "", "", StoreBaseResopnse.Company.MarketingBriefRecipient, StoreBaseResopnse.Company.Name, SecondEmail, Attachments, "", null, "", "", "", MEsg, "", 0, "", 0);
                    }
                    else
                    {
                        SystemUser SalesMagerRec = _IUserManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
                        if (SalesMagerRec != null)
                        {
                            EmailParams.SystemUserId = SalesMagerRec.SystemUserId;

                        }

                        _ICampaignService.emailBodyGenerator(EventCampaign, EmailParams, null, StoreMode.Retail, OID, "", "", "", SalesMagerRec.Email, "", "", Attachments, "", null, "", "", "", MEsg, "", 0, "", 0);


                    }
                }
                else
                {
                    string Email = string.Empty;
                    EmailParams.StoreId = UserCookieManager.WBStoreId;
                    EmailParams.SalesManagerContactID = _myClaimHelper.loginContactID();

                    if (StoreBaseResopnse.Company.MarketingBriefRecipient != null)
                    {
                        _ICampaignService.emailBodyGenerator(EventCampaign, EmailParams, null, StoreMode.Retail, (int)org.OrganisationId, "", "", "", StoreBaseResopnse.Company.MarketingBriefRecipient, StoreBaseResopnse.Company.Name, SecondEmail, Attachments, "", null, "", "", "", MEsg, "", 0, "", 0);
                    }
                    else
                    {
                        SystemUser SalesMagerRec = _IUserManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
                        if (SalesMagerRec != null)
                        {
                            EmailParams.SystemUserId = SalesMagerRec.SystemUserId;
                            Email = SalesMagerRec.Email;
                        }

                        _ICampaignService.emailBodyGenerator(EventCampaign, EmailParams, null, StoreMode.Retail, (int)org.OrganisationId, "", "", "", Email, "", "", Attachments, "", null, "", "", "", MEsg, "", 0, "", 0);


                    }

                }

                MarketingBriefHistory briefHistoryObj = new MarketingBriefHistory();
                briefHistoryObj.CompanyId = UserCookieManager.WBStoreId;
                briefHistoryObj.OrganisationId = UserCookieManager.WEBOrganisationID;
                briefHistoryObj.HtmlMsg = MEsg;
                briefHistoryObj.CreationDate = DateTime.Now;
                briefHistoryObj.ContactId = _myClaimHelper.loginContactID();
                briefHistoryObj.ItemId = Model.ItemID;

                _IItemService.SaveMarketingBriefHistory(briefHistoryObj);

                ViewBag.SuccessMessage = Item.BriefSuccessMessage;// "Thank you for your order. Marketing will review your brief within 24-48 hours and if approved design will have the first proof back to you in 3 business days. <br /> <br /> If your brief is not approved, marketing will be in contact with you.";


                ViewBag.IsSubmitSuccessfully = true;



                return View("PartialViews/MarketingBrief", Item);
            }
            catch(Exception ex)
            {
              
                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");

                using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                throw ex;
            }
           
        }
    }
}