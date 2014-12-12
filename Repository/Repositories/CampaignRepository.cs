using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MPC.Repository.Repositories
{
    public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
    {
         public static int CountOfEmailsFailed = 0;
        public CampaignRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Campaign> DbSet
        {
            get
            {
                return db.Campaigns;
            }
        }

        public Campaign GetCampaignRecordByEmailEvent(int iEmailEvent)
        {
          
                var email = (from c in db.Campaigns
                                 where c.EmailEvent == iEmailEvent
                                 select c).FirstOrDefault();
                return email;
         
        }


        public bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", HttpContext oHttpContext = null, string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0)
        {

            string mesgBody = null;
            string To = null;
            string ToName = null;
            string mailFrom = null;
            string fromName = null;
            string smtpServer = null;
            string smtpUserName = null;
            string mailPassword = null;
            string HtmlText = null;


            if (oHttpContext == null)
                oHttpContext = HttpContext.Current;



            bool result = false;

            if (oCampaign != null)
            {
                if (SeverSettings != null)
                {
                    smtpUserName = SeverSettings.SmtpUserName;
                    smtpServer = SeverSettings.SmtpServer;
                    mailPassword = SeverSettings.SmtpPassword;
                }

                HtmlText = oCampaign.HTMLMessageA;





                if (string.IsNullOrEmpty(HtmlText))
                {
                    if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
                    {
                        CountOfEmailsFailed += 1;
                    }
                    //do nothing
                    return false;
                }
                else
                {

                    string DecodedHtml = "";

                    if (HttpContext.Current == null)
                        DecodedHtml = oHttpContext.Server.HtmlDecode(HtmlText);
                    else
                        DecodedHtml = HttpContext.Current.Server.HtmlDecode(HtmlText);


                    PropertyInfo[] propertyInfos = variablValues.GetType().GetProperties();
                    // RESOLVE VARIABLES FOR MESS BODY

                    DecodedHtml = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, password, PostCodes, Convert.ToString(SubscriptionEndDate), PayyPalGatwayEmail, SubscriptionPath, MarkBreifSumm, UnOrderedTotalItems, UnOrderedItemsTotal, SavedDesignsCount);

                    mesgBody = DecodedHtml;

                    // RESOLVE VARIABLES FOR SUBJECT
                    if (HttpContext.Current == null)
                        DecodedHtml = oHttpContext.Server.HtmlDecode(oCampaign.SubjectA);
                    else
                        DecodedHtml = HttpContext.Current.Server.HtmlDecode(oCampaign.SubjectA);

                    DecodedHtml = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, "", "", "", "", SubscriptionPath, MarkBreifSumm);


                    oCampaign.SubjectA = DecodedHtml;
                    //
                    if (string.IsNullOrEmpty(oCampaign.FromName) || string.IsNullOrEmpty(oCampaign.FromAddress))
                    {
                        fromName = ""; // Name of the sender
                        mailFrom = "";
                    }
                    else
                    {
                        // RESOLVE VARIABLES FOR From Name and from Address

                        if (HttpContext.Current == null)
                            DecodedHtml = oHttpContext.Server.HtmlDecode(oCampaign.FromName);
                        else
                            DecodedHtml = HttpContext.Current.Server.HtmlDecode(oCampaign.FromName);
                        fromName = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, "", "", "", "", SubscriptionPath, MarkBreifSumm); //oCampaign.FromName; // Name of the sender


                        // RESOLVE VARIABLES FOR from Address

                        if (HttpContext.Current == null)
                            DecodedHtml = oHttpContext.Server.HtmlDecode(oCampaign.FromAddress);
                        else
                            DecodedHtml = HttpContext.Current.Server.HtmlDecode(oCampaign.FromAddress);
                        mailFrom = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, "", "", "", "", SubscriptionPath, MarkBreifSumm);// oCampaign.FromAddress;

                    }

                    if (oCampaign.EmailEvent == (int)Events.OnlineOrder)
                    {
                        mesgBody += shopReceiptHtml;
                    }
                    if (userRecord != null)
                    {
                        To = userRecord.Email;
                        ToName = userRecord.FirstName + " " + userRecord.LastName;
                    }
                    else if (!string.IsNullOrEmpty(Email1) && userRecord == null)
                    {
                        To = Email1;
                        ToName = Email1;
                    }
                    else if (!string.IsNullOrEmpty(emailOfSubscribedUsers))// order information will be sent to the subscribed users
                    {
                        To = emailOfSubscribedUsers;
                        ToName = ReceiverName;
                    }
                    else if (!string.IsNullOrEmpty(emailOfSalesManager))
                    {
                        // order information will be sent to the sales manager

                        To = emailOfSalesManager;
                        ToName = ReceiverName;
                    }
                    else
                    {


                        userRecord = GetContactByID(Convert.ToInt32(variablValues.ContactID));
                        if (userRecord != null)
                        {
                            To = userRecord.Email;
                            ToName = userRecord.FirstName + " " + userRecord.LastName;
                        }
                    }

                    if (string.IsNullOrEmpty(To) || string.IsNullOrEmpty(smtpUserName) || string.IsNullOrEmpty(smtpServer))
                    {
                        if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
                        {
                            CountOfEmailsFailed += 1;
                        }
                        return false;
                    }
                    else
                    {

                        if (ValidatEmail(To))
                        {

                            if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
                            {
                                result = AddMsgToTblQueue(To, secondEmail, ToName, mesgBody, fromName, mailFrom, smtpUserName, mailPassword, smtpServer, oCampaign.SubjectA, AttachmentsList, Convert.ToInt32(oCampaign.CampaignReportId));
                            }
                            else
                            {
                                result = AddMsgToTblQueue(To, secondEmail, ToName, mesgBody, fromName, mailFrom, smtpUserName, mailPassword, smtpServer, oCampaign.SubjectA, AttachmentsList, 0);
                            }

                            if (oCampaign.EmailEvent == (int)Events.OnlineOrder)
                            {
                                if (result)
                                {
                                    UpdateEstimateRecord(variablValues.EstimateID);
                                }
                            }
                        }
                        else
                        {
                            if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
                            {
                                CountOfEmailsFailed += 1;
                            }
                        }

                        return result;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private string ResolveVariablesInHtml(string HtmlDocToResolve, PropertyInfo[] propertyInfos, CampaignEmailParams variablValues, Organisation OrganizationRec, StoreMode Mode, string OrgSMEmail, System.Web.HttpContext oContext, string password = "", string PostCodes = "", string SubscriptionEndDate = "", string PayyPalGatwayEmail = "", string subScriptionPath = "", string BreifSummry = "", int EstmateTotalItems = 0, string EstimateTotall = "", int CountOFSaveDesigns = 0)
        {
                string tagValue = null;
                do
                {
                    int firstindex = HtmlDocToResolve.IndexOf("«");
                    int lastindex = HtmlDocToResolve.IndexOf("»");
                    if (firstindex < 0 && lastindex > 0)
                    {
                        HtmlDocToResolve = HtmlDocToResolve.Replace("»", "");
                    }
                    else
                    {
                        int subtract = (lastindex - firstindex) + 1;
                        if (subtract < 0)
                        {
                            string s = HtmlDocToResolve.Substring(lastindex, 1);
                            HtmlDocToResolve = HtmlDocToResolve.Replace(s, "");
                        }
                        else
                        {
                            if (firstindex == -1 || lastindex == -1)
                            {
                                // decoded html returned as it is(if no variable found)
                            }
                            else
                            {

                                string Tag = HtmlDocToResolve.Substring(firstindex, subtract);



                                var tagRecord = GetTag(Tag);
                                if (tagRecord != null)
                                {

                                    if (Tag.Contains("Password") || Tag.Contains("password"))
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, password);
                                    }
                                    else if (Tag.Contains("PostCodeAreas"))
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, PostCodes);
                                    }
                                    else if (Tag.Contains("SubscriptionEndDate"))
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, SubscriptionEndDate.ToString());
                                    }
                                    else if (Tag.Contains("StoreOnlineGatewayReceipt"))
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, PayyPalGatwayEmail);
                                    }
                                    else
                                    {
                                        foreach (PropertyInfo propertyInfo in propertyInfos)
                                        {



                                            if (propertyInfo.Name == tagRecord.CriteriaFieldName)
                                            {
                                                if (Convert.ToInt32(propertyInfo.GetValue(variablValues, null)) > 0)
                                                {
                                                    if (propertyInfo.Name == "ApprovarID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (Tag.Contains("StoreName"))
                                                    {
                                                        if (Mode == StoreMode.Retail)
                                                        {
                                                            if (OrganizationRec != null)
                                                            {
                                                                tagValue = OrganizationRec.OrganisationName;
                                                            }
                                                            else
                                                            {
                                                                tagValue = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                        }


                                                    }
                                                    else if (propertyInfo.Name == "AddressID")
                                                    {
                                                        if (Mode == StoreMode.Retail)
                                                        {
                                                            if (Tag.Contains("«AddressTel1:46»"))
                                                            {
                                                                tagValue = OrganizationRec.Tel;
                                                            }
                                                            if (Tag.Contains("«AddressEmail:44»"))
                                                            {
                                                                tagValue = OrganizationRec.Email;
                                                            }
                                                            if (Tag.Contains("«AddressCity:41»"))
                                                            {
                                                                tagValue = OrganizationRec.City;

                                                            }
                                                            if (Tag.Contains("«Address1:38»"))
                                                            {
                                                                tagValue = OrganizationRec.Address1;
                                                            }
                                                            if (Tag.Contains("«Address2:39»"))
                                                            {
                                                                tagValue = OrganizationRec.Address2;
                                                            }
                                                            if (Tag.Contains("«AddressPostCode:42»"))
                                                            {
                                                                tagValue = OrganizationRec.ZipCode;

                                                            }
                                                            if (Tag.Contains("«AddressURL:45»"))
                                                            {
                                                                tagValue = OrganizationRec.URL;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            tagValue = DynamicQueryToGetAddressByCompanyID(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactCompanyID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                        }

                                                    }
                                                    else if (propertyInfo.Name == "BrokerID")
                                                    {
                                                        if (Tag.Contains("Broker_Website"))
                                                        {
                                                            tagValue = DynamicQueryToGetBrokerImageURL(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactCompanyID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                            tagValue = oContext.Request.Url.Scheme + "://" + tagValue;
                                                        }
                                                        if (Tag.Contains("StoreDomainName"))
                                                        {
                                                            tagValue = DynamicQueryToGetBrokerImageURL(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactCompanyID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue + "/login";
                                                        }
                                                    }
                                                    else if (propertyInfo.Name == "BrokerContactID")
                                                    {
                                                        if (Tag.Contains("Broker_Address"))
                                                        {
                                                            // address
                                                            tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                            Address Address = GetAddressById(Convert.ToInt32(tagValue));
                                                            tagValue = Address.Address1 + Address.Address2 + ", " + Address.City + ", " + Address.State + ", " + Address.Country + ", " + Address.PostCode;
                                                        }
                                                        else
                                                        {
                                                            tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                        }

                                                    }
                                                    else if (propertyInfo.Name == "CorporateManagerID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (propertyInfo.Name == "SupplierCompanyID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactCompanyID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (propertyInfo.Name == "StoreID")
                                                    {
                                                        if (Mode == StoreMode.Broker || Mode == StoreMode.Corp)// if broker mode then Company name == Broker company
                                                        {
                                                            tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactCompanyID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;

                                                        }
                                                        else
                                                        {
                                                            tagValue = DynamicQueryToGetRecord("WebsiteLogo", "tbl_company_sites", "CompanySiteID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;
                                                        }
                                                    }
                                                    else if (propertyInfo.Name == "SalesManagerContactID")
                                                    {
                                                        if (Mode == StoreMode.Broker)// if broker mode then Email == Broker company contact email
                                                        {
                                                            tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                        }
                                                        else
                                                        {
                                                            tagValue = OrgSMEmail;
                                                        }
                                                    }
                                                    else if (propertyInfo.Name == "SubscriberID")
                                                    {
                                                        if (Tag.Contains("SubscriptionLink"))
                                                        {
                                                            tagValue = subScriptionPath;
                                                        }
                                                    }
                                                    else if (propertyInfo.Name == "MarketingID")
                                                    {
                                                        if (Tag.Contains("MarketingBrief"))
                                                        {
                                                            tagValue = BreifSummry;
                                                        }
                                                    }
                                                    else if (Tag == "«OrderPaymentStatus:7»")
                                                    {
                                                        PrePayment rec = DynamicQueryToGetPaymentRec(tagRecord.RefFieldName, tagRecord.RefTableName, propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                        if (rec != null)
                                                        {
                                                            PaymentMethod Paymentrec = GetPaymentMethods(Convert.ToInt32(rec.PaymentMethodId));
                                                            string PaymMesg = "Order payment processed via " + Paymentrec.MethodName + " on " + (DateTime)rec.PaymentDate;
                                                            HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, PaymMesg);
                                                        }
                                                        else
                                                        {
                                                            string Msg = "Order placed on Account on ";
                                                            tagValue = DynamicQueryToGetRecord("CreationDate", "tbl_estimates", propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));

                                                            HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, Msg + tagValue);
                                                        }
                                                    }
                                                    else if (Tag == "«OrderXMLFile1:7»")
                                                    {
                                                        int orderid = Convert.ToInt32(propertyInfo.GetValue(variablValues, null));

                                                        if (orderid > 0)
                                                        {
                                                            tagValue = "mis/Services/OrderSvc.svc/DownloadOrderXMLByID?OrderID=" + orderid + "&Format=1";

                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;

                                                            // tagValue = "<a href="+tagValue+" target='_blank'>Download 
                                                        }
                                                    }
                                                    else if (Tag == "«OrderArtworkFile1:7»")
                                                    {
                                                        int orderid = Convert.ToInt32(propertyInfo.GetValue(variablValues, null));
                                                        if (orderid > 0)
                                                        {
                                                            tagValue = "mis/Services/OrderSvc.svc/GenerateOrderArtworkArchive?OrderID=" + orderid;

                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;
                                                        }
                                                    }
                                                    else if (propertyInfo.Name == "orderedItemID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ItemID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));


                                                    if (tagRecord != null)
                                                    {
                                                        if (Tag.Contains("OrderTotal:7"))
                                                        {
                                                            tagValue = string.Format("{0:n}", Math.Round(Convert.ToDouble(tagValue), 2));
                                                        }
                                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, tagValue);
                                                    }

                                                    //else
                                                    //LoggingManager.LogEntry("tag value is null for tag = " + Tag);

                                                }
                                                else
                                                {
                                                    HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, "");
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (Tag == "«CustomerUnOrderedTotalItems:93»")
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, Convert.ToString(EstmateTotalItems));
                                    }

                                    else if (Tag == "«CustomerUnOrderedItemsPrice:93»")
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, Convert.ToString(EstimateTotall));
                                    }

                                    if (Tag == "«CountOfSavedDesigns:93»")
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, Convert.ToString(CountOFSaveDesigns));
                                    }

                                    // string s = HtmlDocToResolve.Substring(firstindex, 1);
                                    HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, "");
                                }
                            }
                        }
                    }
                } while (HtmlDocToResolve.Contains("«") || HtmlDocToResolve.Contains("»"));
                return HtmlDocToResolve;
           
        }


        public CampaignEmailVariable GetTag(string tag)
        {

            CampaignEmailVariable result = (from c in db.CampaignEmailVariables
                                                           where c.VariableTag == tag
                                                           select c).FirstOrDefault();
            return result;
               
         
        }

        public string DynamicQueryToGetRecord(string feildname, string tblname, string keyName, int keyValue)
        {

            string oResult = null;
            //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");


            //oResult = result.FirstOrDefault();
            return oResult;
        }
        public string DynamicQueryToGetAddressByCompanyID(string feildname, string tblname, string keyName, int keyValue)
        {
          
                    string oResult = null;
                    //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>("select top 1 cast(" + feildname + " as varchar) from " + tblname + " where " + keyName + "= " + keyValue + " and IsDefaultAddress = 1", "");


                    //oResult = result.FirstOrDefault();
                    return oResult;
           

        }
        public string DynamicQueryToGetBrokerImageURL(string feildname, string tblname, string keyName, int keyValue)
        {
          
                    string oResult = null;

                    //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>("select " + feildname + " from " + tblname + " where " + keyName + "= " + keyValue + "", "");

                    //oResult = result.FirstOrDefault();
                    return oResult;
             

        }
        public  Address GetAddressById(int addressid)
        {
            
            return db.Addesses.Where(c => c.AddressId == addressid).FirstOrDefault();

        }
        public PrePayment DynamicQueryToGetPaymentRec(string feildname, string tblname, string keyName, int keyValue)
        {
          
                    PrePayment oResult = null;

                    //System.Data.Objects.ObjectResult<PrePayment> result = db.ExecuteStoreQuery<PrePayment>("select * from " + tblname + " where " + feildname + "= " + keyValue + "", "");

                    //oResult = result.FirstOrDefault();
                    return oResult;
            

        }
        public PaymentMethod GetPaymentMethods(int ID)
        {
           
             return db.PaymentMethods.Where(t => t.PaymentMethodId == ID).FirstOrDefault();

           
        }
        public CompanyContact GetContactByID(int contactID)
        {
            return db.CompanyContacts.Where(u => u.ContactId == contactID).FirstOrDefault();
            
        }
        public static bool ValidatEmail(string email)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(email, "^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID)
        {
           
                    //campaigne emailQueue = new tbl_CampaignEmailQueue();

                    //emailQueue.To = Toemail;
                    //emailQueue.Cc = CC;
                    //emailQueue.ToName = ToName;
                    //emailQueue.Body = msgbody;
                    //emailQueue.FromName = fromName;
                    //emailQueue.EmailFrom = fromEmail;
                    //emailQueue.Subject = subject;
                    //emailQueue.IsDeliverd = 0;
                    //emailQueue.SMTPUserName = smtpUserName;
                    //emailQueue.SMTPServer = ServerName;
                    //emailQueue.SMTPPassword = ServerPass;
                    //emailQueue.AttemptCount = 0;
                    //emailQueue.CampaignReportID = CampaignReportID;
                    //string fileAttachment = "";
                    //if (AttachmentList != null)
                    //{
                    //    foreach (string item in AttachmentList)
                    //    {
                    //        fileAttachment += item + "|";
                    //    }
                    //    emailQueue.FileAttachment = fileAttachment;
                    //}

                    //context.AddTotbl_CampaignEmailQueue(emailQueue);
                    //context.SaveChanges();
                    return true;
              
        }
        public void UpdateEstimateRecord(long estimateId)
        {

                    Estimate estimate = db.Estimates.Where(c => c.EstimateId == estimateId).FirstOrDefault();

                    estimate.isEmailSent = true;

                    db.SaveChanges();
              
        }

    }
}
