﻿using Microsoft.Practices.Unity;
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
using System.Data.Objects;
using System.Data;
using System.IO;
using System.Reflection;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

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

            return db.Campaigns.Where(c => c.EmailEvent == iEmailEvent).FirstOrDefault();
                //var email = (from c in db.Campaigns
                //                 where c.EmailEvent == iEmailEvent
                //                 select c).FirstOrDefault();
                //return email;
         
        }


        public bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0)
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


           
           HttpContext oHttpContext = HttpContext.Current;



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


                        userRecord = GetContactByID(Convert.ToInt32(variablValues.ContactId));
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

        public void SendEmailToSalesManager(int Event, int ContactId, int CompanyId, int brokerid, int OrderId, Organisation ServerSettings, int BrokerAdminContactID, int CorporateManagerID, StoreMode Mode,Company company,SystemUser SalesManager, int ItemID, string NameOfBrokerComp = "", string MarketingBreifMesgSummry = "", int RFQId = 0)
        {

            //UsersManager usermgr = new UsersManager();
          //  SystemUser SalesManager = null;
            List<CompanyContact> listOfApprovers = new List<CompanyContact>();
           // SalesManager = GetSalesManagerDataByID(Convert.ToInt32(company.SalesAndOrderManagerId1));
            if (SalesManager != null)
            {
                Campaign EventCampaign = GetCampaignRecordByEmailEvent(Event);
                CampaignEmailParams EmailParams = new CampaignEmailParams();
                EmailParams.ContactId = ContactId;
                EmailParams.CompanyId = CompanyId;
                EmailParams.CompanySiteID = 1;
                EmailParams.AddressID = CompanyId;
                EmailParams.SystemUserID = SalesManager.SystemUserId;
                EmailParams.InquiryID = RFQId;
                if (Mode == StoreMode.Retail)
                {
                    EmailParams.StoreID = ServerSettings.OrganisationId;
                }
                else
                {
                    EmailParams.StoreID = CompanyId;
                }

                EmailParams.SalesManagerContactID = ContactId;
                if (brokerid > 0)
                {
                    EmailParams.CompanyId = brokerid;
                    EmailParams.BrokerID = brokerid;
                    EmailParams.BrokerContactID = BrokerAdminContactID;
                    EmailParams.SalesManagerContactID = BrokerAdminContactID;
                    EmailParams.StoreID = brokerid;
                    EmailParams.AddressID = brokerid;
                    int admin = Convert.ToInt32(Roles.Adminstrator);
                    int Manager = Convert.ToInt32(Roles.Manager);


                    listOfApprovers = (from c in db.CompanyContacts
                                       join cc in db.Companies on brokerid equals cc.CompanyId
                                       where (c.ContactRoleId == admin || c.ContactRoleId == Manager) && cc.IsCustomer == (int)CustomerTypes.Broker  && c.CompanyId == brokerid
                                       select c).ToList();

                }
                if (CorporateManagerID > 0)
                {
                    EmailParams.CorporateManagerID = CorporateManagerID;
                }
                if (OrderId > 0)
                {
                    EmailParams.EstimateID = OrderId;

                    EmailParams.ItemID = ItemID;
                }

                if (!string.IsNullOrEmpty(MarketingBreifMesgSummry))
                {
                    EmailParams.MarketingID = 1;
                    emailBodyGenerator(EventCampaign, ServerSettings, EmailParams, null, Mode, "", "", "", SalesManager.Email, SalesManager.FullName, "", null, "", null, "", NameOfBrokerComp, null, "", MarketingBreifMesgSummry);
                }
                else
                {
                    emailBodyGenerator(EventCampaign, ServerSettings, EmailParams, null, Mode, "", "", "", SalesManager.Email, SalesManager.FullName, "", null, "", null, "", NameOfBrokerComp);
                }
                if (brokerid > 0 && OrderId == 0)
                {
                    if (listOfApprovers != null)
                    {
                        foreach (var approver in listOfApprovers)
                        {
                            EmailParams.SystemUserID = 0;
                            EmailParams.ApprovarID = (int)approver.ContactId;
                            if (!string.IsNullOrEmpty(MarketingBreifMesgSummry))
                            {
                                EmailParams.MarketingID = 1;
                                emailBodyGenerator(EventCampaign, ServerSettings, EmailParams, null, Mode, "", "", "", approver.Email, approver.FirstName, "", null, "", null, "", NameOfBrokerComp, null, "", MarketingBreifMesgSummry);
                            }
                            else
                            {
                                emailBodyGenerator(EventCampaign, ServerSettings, EmailParams, null, Mode, "", "", "", approver.Email, approver.FirstName, "", null, "", null, "", NameOfBrokerComp);
                            }
                        }
                    }
                }
            }
          
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
                                                            tagValue = DynamicQueryToGetAddressByCompanyID(tagRecord.RefFieldName, tagRecord.RefTableName, "CompanyId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                        }

                                                    }
                                                    else if (propertyInfo.Name == "BrokerID")
                                                    {
                                                        if (Tag.Contains("Broker_Website"))
                                                        {
                                                            tagValue = DynamicQueryToGetBrokerImageURL(tagRecord.RefFieldName, tagRecord.RefTableName, "CompanyId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                            tagValue = oContext.Request.Url.Scheme + "://" + tagValue;
                                                        }
                                                        if (Tag.Contains("StoreDomainName"))
                                                        {
                                                            tagValue = DynamicQueryToGetBrokerImageURL(tagRecord.RefFieldName, tagRecord.RefTableName, "CompanyId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
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
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "CompanyId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (propertyInfo.Name == "StoreID")
                                                    {
                                                        if (Mode == StoreMode.Broker || Mode == StoreMode.Corp)// if broker mode then Company name == Broker company
                                                        {
                                                            tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "CompanyId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;

                                                        }
                                                        else
                                                        {
                                                            tagValue = DynamicQueryToGetRecord("WebsiteLogo", "organisation", "OrganisationId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
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

            System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");
            oResult = result.FirstOrDefault();
            return oResult;
        }
        public string DynamicQueryToGetAddressByCompanyID(string feildname, string tblname, string keyName, int keyValue)
        {
          
                    string oResult = null;
                    //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>(("select top 1 cast(" + feildname + " as varchar) from " + tblname + " where " + keyName + "= " + keyValue + " and IsDefaultAddress = 1", "");

                    System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar) from " + tblname + " where " + keyName + "= " + keyValue + " and IsDefaultAddress = 1", "");
                   oResult = result.FirstOrDefault();
                    return oResult;
           

        }
        public string DynamicQueryToGetBrokerImageURL(string feildname, string tblname, string keyName, int keyValue)
        {
          
                    string oResult = null;

                    //System.Data.Objects.ObjectResult<string> result = db.Database.SqlQuery()<string>("select " + feildname + " from " + tblname + " where " + keyName + "= " + keyValue + "", "");
                    System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select " + feildname + " from " + tblname + " where " + keyName + "= " + keyValue + "", "");
                    oResult = result.FirstOrDefault();
                    return oResult;
             

        }
        public  Address GetAddressById(int addressid)
        {
            
            return db.Addesses.Where(c => c.AddressId == addressid).FirstOrDefault();

        }
        public PrePayment DynamicQueryToGetPaymentRec(string feildname, string tblname, string keyName, int keyValue)
        {
          
                    PrePayment oResult = null;
          
                    //System.Data.Objects.ObjectResult<PrePayment> result =  db.Database <PrePayment>("select * from " + tblname + " where " + feildname + "= " + keyValue + "", "");
                    System.Data.Entity.Infrastructure.DbRawSqlQuery<PrePayment> result = db.Database.SqlQuery<PrePayment>("select * from " + tblname + " where " + feildname + "= " + keyValue + "", "");
                    oResult = result.FirstOrDefault();
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

            CampaignEmailQueue emailQueue = new CampaignEmailQueue();

            emailQueue.To = Toemail;
            emailQueue.Cc = CC;
            emailQueue.ToName = ToName;
            emailQueue.Body = msgbody;
            emailQueue.FromName = fromName;
            emailQueue.EmailFrom = fromEmail;
            emailQueue.Subject = subject;
            emailQueue.IsDeliverd = 0;
            emailQueue.SMTPUserName = smtpUserName;
            emailQueue.SMTPServer = ServerName;
            emailQueue.SMTPPassword = ServerPass;
            emailQueue.AttemptCount = 0;
            emailQueue.CampaignReportId = CampaignReportID;
            string fileAttachment = "";
            if (AttachmentList != null)
            {
                foreach (string item in AttachmentList)
                {
                    fileAttachment += item + "|";
                }
                emailQueue.FileAttachment = fileAttachment;
            }

            db.CampaignEmailQueues.Add(emailQueue);
            db.SaveChanges();
                    return true;
              
        }
        public void UpdateEstimateRecord(long estimateId)
        {

                    Estimate estimate = db.Estimates.Where(c => c.EstimateId == estimateId).FirstOrDefault();

                    estimate.isEmailSent = true;

                    db.SaveChanges();
              
        }
        public void SendPendingCorporateUserRegistrationEmailToAdmins(int contactID, int Companyid,Organisation serverSetting)
        {
            
                int admin = Convert.ToInt32(Roles.Adminstrator);
                CampaignEmailParams obj = new CampaignEmailParams();
                List<CompanyContact> listOfApprovers = new List<CompanyContact>();
               
                listOfApprovers = (from c in db.CompanyContacts
                                       join cc in db.Companies on Companyid equals cc.CompanyId
                                       
                                   where c.ContactRoleId == admin && cc.IsCustomer == (int)CustomerTypes.Corporate && c.CompanyId == Companyid
                                       select c).ToList();
                    if (listOfApprovers.Count() > 0)
                    {
                        Campaign CorporateOrderForApprovalCampaign = GetCampaignRecordByEmailEvent((int)Events.CorporateRegistrationForApproval);
                        Organisation SeverSettings = serverSetting;
                        foreach (CompanyContact corpRec in listOfApprovers)
                        {
                            obj.ApprovarID = (int)corpRec.ContactId;
                            obj.ContactId = contactID;
                            obj.CompanyId = Companyid;
                            obj.SalesManagerContactID = corpRec.ContactId;
                            obj.StoreID = Companyid;
                            obj.AddressID = Companyid;
                            emailBodyGenerator(CorporateOrderForApprovalCampaign, SeverSettings, obj, corpRec, StoreMode.Corp, "", "", "", corpRec.Email, "");
                        }
                    }
                }

        public void SendEmailFromQueue(System.Web.HttpContext hcontext)
        {

            bool res = false;
            int? isCampaignPaused = 0;

            List<CampaignEmailQueue> allrecords = (from c in db.CampaignEmailQueues
                                                               where c.IsDeliverd == 0 && c.AttemptCount < 5
                                                               select c).ToList();

              if (allrecords != null)
              {
                        string ErrorMsg = string.Empty;

                        foreach (CampaignEmailQueue record in allrecords)
                        {
                            ErrorMsg = string.Empty;
                            if (isCampaignPaused == 0 && record.CampaignReportId != null)
                            {
                                isCampaignPaused = (from c in db.Campaigns
                                                    where c.CampaignReportId == record.CampaignReportId
                                                    select c.Status).FirstOrDefault();
                            }
                            if (isCampaignPaused != Convert.ToInt32(ScheduledStatus.Paused))
                            {
                                if (SendEmail(record, hcontext, out ErrorMsg))
                                {
                                    if (record.FileAttachment != null)
                                    {
                                        res = true;
                                    }

                                    if (res)
                                    {
                                       
                                            string filePath = string.Empty;
                                            string[] Allfiles = record.FileAttachment.Split('|');
                                            foreach (var file in Allfiles)
                                            {
                                                filePath = hcontext.Server.MapPath(file);
                                                if (File.Exists(filePath))
                                                    File.Delete(filePath);
                                            }
                                      
                                        }
                                    }
                                    db.CampaignEmailQueues.Remove(record);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    record.ErrorResponse = ErrorMsg;
                                    record.AttemptCount++;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }

        private bool SendEmail(CampaignEmailQueue oEmailBody, System.Web.HttpContext context, out string ErrorMsg)
        {

            MailMessage objMail = new MailMessage();
            bool retVal = false;
           
                string smtp = oEmailBody.SMTPServer;
                string SmtpUserName = oEmailBody.SMTPUserName;
                string SenderPassword = oEmailBody.SMTPPassword;
                string FromEmail = oEmailBody.EmailFrom;
                string FromName = oEmailBody.FromName;
                string ToName = oEmailBody.ToName;
                string MailTo = oEmailBody.To;
                string CC = oEmailBody.Cc;


                Attachment data = null;
                if (oEmailBody.FileAttachment != null)
                {
                    string[] Allfiles = oEmailBody.FileAttachment.Split('|');
                    foreach (string temp in Allfiles)
                    {
                        if (temp != "")
                        {
                            string fname = temp;
                            if (temp.Contains('_'))
                            {
                                string[] abc = temp.Split('_');
                                fname = abc[abc.Length - 1];
                            }
                            else
                            {
                                string[] abc = temp.Split('/');
                                fname = abc[abc.Length - 1];
                            }

                            string FilePath = context.Server.MapPath(temp);
                            data = new Attachment(FilePath, MediaTypeNames.Application.Octet);
                            ContentDisposition disposition = data.ContentDisposition;
                            disposition.CreationDate = System.IO.File.GetCreationTime(FilePath);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(FilePath);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(FilePath);
                            disposition.FileName = fname;
                            objMail.Attachments.Add(data);
                        }
                    }
                }

                SmtpClient objSmtpClient = new SmtpClient(smtp);
                objSmtpClient.Credentials = new NetworkCredential(SmtpUserName, SenderPassword);
                objMail.From = new MailAddress(FromEmail, FromName);
                objMail.To.Add(new MailAddress(MailTo, ToName));
                if (!string.IsNullOrEmpty(CC))
                {
                    if (!string.IsNullOrWhiteSpace(CC))
                        objMail.CC.Add(new MailAddress(CC));
                }

                objMail.IsBodyHtml = true;
                objMail.Body = oEmailBody.Body;
                objMail.Subject = oEmailBody.Subject;

                objSmtpClient.Send(objMail);

                if (data != null)
                {
                    objMail.Attachments.Remove(data);
                    data.Dispose();
                }
                retVal = true;
                ErrorMsg = "";

         
                objMail.Dispose();
                if (objMail != null)
                    objMail = null;

            return retVal;
        }

        private static bool validURL(string url)
        {
            Uri urlCheck = new Uri(url);
            WebRequest request = WebRequest.Create(urlCheck);
            request.Timeout = 5000;//Timeout set to 5 seconds

            WebResponse response;
            try
            {
                response = request.GetResponse();
                if (request.RequestUri != response.ResponseUri)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("denied"))
                    return true; //url exists but access is denied
                else
                    return false; //url does not exist
            }

        }

        public void MonitorScheduledEmails()
        {
            
                    //int InProgress = Convert.ToInt32(ScheduledStatus.InProgress);
                    ////get the list of active news letters which are in progress
                    //var ActiveCampgns =  db.Campaigns.Where(g => g.Status == InProgress && g.CampaignReportId != null).ToList();


                    //foreach (var item in ActiveCampgns)
                    //{


                    //    int CampaignID = (int)item.CampaignId;
                    //    int CampaignReportID = item.CampaignReportId.Value;

                    //    var oReportCampgn = db.cam.Where(g => g.CampaignReportID == CampaignReportID).FirstOrDefault();

                    //    //getting the counts

                    //    int FailedCount = context.tbl_CampaignEmailQueue.Where(g => g.CampaignReportID == CampaignReportID && g.AttemptCount == 5).Count();

                    //    int Undelivered = context.tbl_CampaignEmailQueue.Where(g => g.CampaignReportID == CampaignReportID && g.AttemptCount < 5).Count();
                    //    if (oReportCampgn != null)
                    //    {
                    //        int Delivered = oReportCampgn.TotalCount.Value - FailedCount - Undelivered;


                    //        //updating the status
                    //        oReportCampgn.TotalCount = oReportCampgn.TotalCount - CountOfEmailsFailed;
                    //        oReportCampgn.TotalDeliverd = Delivered;
                    //        oReportCampgn.TotalFailed = FailedCount;


                    //        if (Undelivered == 0)
                    //        {
                    //            oReportCampgn.EndDate = DateTime.Now;
                    //            item.Status = Convert.ToInt32(ScheduledStatus.Compeleted); //completed
                    //            item.CampaignReportID = null;   //resetting it.
                    //            string ReportSummery = string.Format("<BR /> {0} <br/>", "Report of " + item.CampaignName);
                    //            ReportSummery += "<BR /><BR />";
                    //            ReportSummery += string.Format(" The campaign starts at {0}", oReportCampgn.StartDate + ",");
                    //            ReportSummery += "<BR /><BR /> total" + oReportCampgn.TotalCount + " emails are composed for this campaign";
                    //            ReportSummery += " from which total delivered emails are " + oReportCampgn.TotalDeliverd + "<BR /><BR />and undelivered emails are " + oReportCampgn.TotalFailed;
                    //            ReportSummery += "<BR /><BR />The End date time of campaign is " + oReportCampgn.EndDate;
                    //            ReportSummery += "<BR /><BR />---------------------------------------------------------------------------------- <BR /><BR />";
                    //            ReportSummery += "Please do not reply to this mail as this is a system generated email. <BR />";
                    //            oReportCampgn.Report = ReportSummery;
                    //            if (item.EmailLogFile ?? false)
                    //            {
                    //                tbl_company_sites ServerSettings = GetSeverSettings();
                    //                if (!string.IsNullOrEmpty(item.EmailLogFileAddress) && !string.IsNullOrEmpty(item.EmailLogFileAddress2))
                    //                {
                    //                    AddMsgToTblQueue(item.EmailLogFileAddress, item.EmailLogFileAddress2, item.EmailLogFileAddress, ReportSummery, item.FromName, item.FromAddress, ServerSettings.SmtpUserName, ServerSettings.SmtpPassword, ServerSettings.SmtpServer, "Report", null, 0);
                    //                }
                    //                else if (!string.IsNullOrEmpty(item.EmailLogFileAddress) && string.IsNullOrEmpty(item.EmailLogFileAddress2))
                    //                {
                    //                    AddMsgToTblQueue(item.EmailLogFileAddress, "", item.EmailLogFileAddress, ReportSummery, item.FromName, item.FromAddress, ServerSettings.SmtpUserName, ServerSettings.SmtpPassword, ServerSettings.SmtpServer, "Report", null, 0);
                    //                }
                    //                else if (string.IsNullOrEmpty(item.EmailLogFileAddress) && !string.IsNullOrEmpty(item.EmailLogFileAddress2))
                    //                {
                    //                    AddMsgToTblQueue(item.EmailLogFileAddress2, "", item.EmailLogFileAddress2, ReportSummery, item.FromName, item.FromAddress, ServerSettings.SmtpUserName, ServerSettings.SmtpPassword, ServerSettings.SmtpServer, "Report", null, 0);
                    //                }
                    //            }
                    //        }
                    //        context.SaveChanges();
                    //    }
                    //}
              
           
        }


    }
}