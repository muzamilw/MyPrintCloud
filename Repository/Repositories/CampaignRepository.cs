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
using System.Web;
using System.IO;
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

        public Campaign GetCampaignRecordByEmailEvent(long iEmailEvent, long OrganisationId, long CompanyId)
        {
            try
            {
                return db.Campaigns.Where(c => c.EmailEvent == iEmailEvent && c.CompanyId == CompanyId && c.OrganisationId == OrganisationId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0,string ITemtypefourHtml="")
        {
            try
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

                        DecodedHtml = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, password, PostCodes, Convert.ToString(SubscriptionEndDate), PayyPalGatwayEmail, SubscriptionPath, MarkBreifSumm, UnOrderedTotalItems, UnOrderedItemsTotal, SavedDesignsCount, ITemtypefourHtml);

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

                        //if (string.IsNullOrEmpty(To) || string.IsNullOrEmpty(smtpUserName) || string.IsNullOrEmpty(smtpServer))
                        //{
                        //    if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
                        //    {
                        //        CountOfEmailsFailed += 1;
                        //    }
                        //    return false;
                        //}
                        //else
                        //{

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
                                        UpdateEstimateRecord(variablValues.EstimateId);
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
                        //}
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public string GetPinkCardsShopReceiptPage(int OrderId, long CorpID)
        {
            try
            {
                string URl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/ReceiptPlain.aspx?OrderID=" + OrderId + "&CorpID=" + CorpID;
                WebClient myClient = new WebClient();
                Stream response = myClient.OpenRead(URl);
                StreamReader streamreader = new StreamReader(response);
                string pageHtml = streamreader.ReadToEnd();
                return pageHtml;
            }
            catch (Exception ex)
            {

                // LoggingManager.LogBLLException(e);
                return null;
            }
        }
        public void SendEmailToSalesManager(int Event, long ContactId, long CompanyId, long OrderId, Organisation ServerSettings, long OrganisationId, int CorporateManagerID, StoreMode Mode, long StoreId, SystemUser SalesManager, int ItemID, string NameOfComp = "", string MarketingBreifMesgSummry = "", int RFQId = 0)
        {
            try
            {
                List<CompanyContact> listOfApprovers = new List<CompanyContact>();
                
                if (SalesManager != null)
                {
                    Campaign EventCampaign = GetCampaignRecordByEmailEvent(Event, OrganisationId, StoreId);
                    CampaignEmailParams EmailParams = new CampaignEmailParams();
                    EmailParams.ContactId = ContactId;
                    EmailParams.CompanyId = CompanyId;
                    EmailParams.OrganisationId = OrganisationId;
                    EmailParams.AddressId = CompanyId;
                    EmailParams.SystemUserId = SalesManager.SystemUserId;
                    EmailParams.InquiryId = RFQId;
                    EmailParams.StoreId = StoreId;
                    EmailParams.SalesManagerContactID = ContactId;
                  
                    if (CorporateManagerID > 0)
                    {
                        EmailParams.CorporateManagerID = CorporateManagerID;
                    }
                    if (OrderId > 0)
                    {
                        EmailParams.EstimateId = OrderId;

                        EmailParams.ItemId = ItemID;
                    }

                    if (!string.IsNullOrEmpty(MarketingBreifMesgSummry))
                    {
                        EmailParams.MarketingID = 1;
                        emailBodyGenerator(EventCampaign, ServerSettings, EmailParams, null, Mode, "", "", "", SalesManager.Email, SalesManager.FullName, "", null, "", null, "", NameOfComp, null, "", MarketingBreifMesgSummry);
                    }
                    else
                    {
                        emailBodyGenerator(EventCampaign, ServerSettings, EmailParams, null, Mode, "", "", "", SalesManager.Email, SalesManager.FullName,"", null, "", null, "", NameOfComp);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }



        }

        private string ResolveVariablesInHtml(string HtmlDocToResolve, PropertyInfo[] propertyInfos, CampaignEmailParams variablValues, Organisation OrganizationRec, StoreMode Mode, string OrgSMEmail, System.Web.HttpContext oContext, string password = "", string PostCodes = "", string SubscriptionEndDate = "", string PayyPalGatwayEmail = "", string subScriptionPath = "", string BreifSummry = "", int EstmateTotalItems = 0, string EstimateTotall = "", int CountOFSaveDesigns = 0, string ITemtypefourHtml = "")
        {
            try
            {
                string tagValue = null;
                bool HasVariableValue = false;
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
                                    else if (Tag.Contains("AssetID"))
                                    {
                                        HtmlDocToResolve = HtmlDocToResolve.Replace(Tag, ITemtypefourHtml);
                                    }
                                    else
                                    {
                                        foreach (PropertyInfo propertyInfo in propertyInfos)
                                        {

                                            if (propertyInfo.Name == tagRecord.CriteriaFieldName)
                                            {

                                                if (propertyInfo.Name == "SystemUserId")
                                                {
                                                    if (!string.IsNullOrEmpty(Convert.ToString(propertyInfo.GetValue(variablValues, null))))
                                                    {
                                                        HasVariableValue = true;
                                                    }
                                                }
                                                else if (Convert.ToInt32(propertyInfo.GetValue(variablValues, null)) > 0)
                                                {
                                                    HasVariableValue = true;
                                                }

                                                if (HasVariableValue)
                                                {
                                                    if (propertyInfo.Name == "ApprovarID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (Tag.Contains("StoreName"))
                                                    {
                                                        //if (Mode == StoreMode.Retail)
                                                        //{
                                                        //    if (OrganizationRec != null)
                                                        //    {
                                                        //        tagValue = OrganizationRec.OrganisationName;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        tagValue = "";
                                                        //    }
                                                        //}
                                                        //else
                                                        //{
                                                       tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                       // }
                                                    }
                                                    else if (propertyInfo.Name == "AddressId")
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
                                                    else if (propertyInfo.Name == "CorporateManagerID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ContactID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (propertyInfo.Name == "SupplierCompanyID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "CompanyId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (propertyInfo.Name == "StoreId")
                                                    {

                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "CompanyId", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                        tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;

                                                    }
                                                    else if (propertyInfo.Name == "SalesManagerContactID")
                                                    {

                                                        tagValue = OrgSMEmail;

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
                                                            tagValue = "/mis/api/DownloadArtworkWebStore?OrderId=" + orderid + "&OrganisationId=" + OrganizationRec.OrganisationId + "&formatxml=1"; // "mis/Services/OrderSvc.svc/DownloadOrderXMLByID?OrderID=" + orderid + "&Format=1";

                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;

                                                            // tagValue = "<a href="+tagValue+" target='_blank'>Download 
                                                        }
                                                    }
                                                    else if (Tag == "«OrderArtworkFile1:7»")
                                                    {
                                                        int orderid = Convert.ToInt32(propertyInfo.GetValue(variablValues, null));
                                                        if (orderid > 0)
                                                        {
                                                            tagValue = "/mis/api/DownloadArtworkWebStore?OrderId=" + orderid + "&OrganisationId=" + OrganizationRec.OrganisationId;

                                                            tagValue = oContext.Request.Url.Scheme + "://" + oContext.Request.Url.Authority + "/" + tagValue;
                                                        }
                                                    }
                                                    else if (propertyInfo.Name == "orderedItemID")
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, "ItemID", Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else if (propertyInfo.Name == "SystemUserId")
                                                    {
                                                        Guid SystemUserId = (Guid)propertyInfo.GetValue(variablValues, null);
                                                        tagValue = DynamicQueryToSystemUserRecord(tagRecord.RefFieldName, tagRecord.RefTableName, propertyInfo.Name, SystemUserId);
                                                    }
                                                    else if (propertyInfo.Name == "AssetID")
                                                    {
                                                        tagValue = ITemtypefourHtml;
                                                    }
                                                    else if (propertyInfo.Name.Contains("SupplierCompanyID"))
                                                    {
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, "CompanyId", propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));
                                                    }
                                                    else
                                                        tagValue = DynamicQueryToGetRecord(tagRecord.RefFieldName, tagRecord.RefTableName, propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(variablValues, null)));


                                                    if (tagRecord != null)
                                                    {
                                                        if (Tag.Contains("OrderTotal:7"))
                                                        {
                                                            tagValue = string.Format("{0:n}", Math.Round(Convert.ToDouble(tagValue), 2));
                                                        }
                                                        if (Tag.Contains("DeliveryDate") || Tag.Contains("CreationDate"))
                                                        {
                                                            if(!string.IsNullOrEmpty(tagValue))
                                                            {
                                                                DateTime tagDate = Convert.ToDateTime(tagValue);
                                                                tagValue = tagDate.Day + "/" + tagDate.Month + "/" + tagDate.Year;
                                                            }
                                                            
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
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public CampaignEmailVariable GetTag(string tag)
        {
            try
            {
                CampaignEmailVariable result = (from c in db.CampaignEmailVariables
                                                where c.VariableTag == tag
                                                select c).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;

            }




        }

        public string DynamicQueryToGetRecord(string feildname, string tblname, string keyName, int keyValue)
        {
            try
            {

                string oResult = null;
                //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");

                System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");
                oResult = result.FirstOrDefault();
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public string DynamicQueryToSystemUserRecord(string feildname, string tblname, string keyName, Guid keyValue)
        {
            try
            {

                string oResult = null;
                //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");

                System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= '" + keyValue + "'", "");
                oResult = result.FirstOrDefault();
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public string DynamicQueryToGetAddressByCompanyID(string feildname, string tblname, string keyName, int keyValue)
        {
            try
            {
                string oResult = null;
                //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>(("select top 1 cast(" + feildname + " as varchar) from " + tblname + " where " + keyName + "= " + keyValue + " and IsDefaultAddress = 1", "");

                System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar) from " + tblname + " where " + keyName + "= " + keyValue + " and IsDefaultAddress = 1", "");
                oResult = result.FirstOrDefault();
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;

            }



        }
        public string DynamicQueryToGetBrokerImageURL(string feildname, string tblname, string keyName, int keyValue)
        {
            try
            {

                string oResult = null;

                //System.Data.Objects.ObjectResult<string> result = db.Database.SqlQuery()<string>("select " + feildname + " from " + tblname + " where " + keyName + "= " + keyValue + "", "");
                System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select " + feildname + " from " + tblname + " where " + keyName + "= " + keyValue + "", "");
                oResult = result.FirstOrDefault();
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public Address GetAddressById(int addressid)
        {
            try
            {
                return db.Addesses.Where(c => c.AddressId == addressid).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public PrePayment DynamicQueryToGetPaymentRec(string feildname, string tblname, string keyName, int keyValue)
        {
            try
            {
                PrePayment oResult = null;

                //System.Data.Objects.ObjectResult<PrePayment> result =  db.Database <PrePayment>("select * from " + tblname + " where " + feildname + "= " + keyValue + "", "");
                System.Data.Entity.Infrastructure.DbRawSqlQuery<PrePayment> result = db.Database.SqlQuery<PrePayment>("select * from " + tblname + " where " + feildname + "= " + keyValue + "", "");
                oResult = result.FirstOrDefault();
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public PaymentMethod GetPaymentMethods(int ID)
        {
            try
            {
                return db.PaymentMethods.Where(t => t.PaymentMethodId == ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }



        }
        public CompanyContact GetContactByID(int contactID)
        {
            try
            {
                return db.CompanyContacts.Where(u => u.ContactId == contactID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }




        }
        public static bool ValidatEmail(string email)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID)
        {
            try
            {
                if (smtpUserName != null && ServerName != null && ServerPass != null) 
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
                    emailQueue.SendDateTime = DateTime.Now;
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
                  
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public void UpdateEstimateRecord(long estimateId)
        {
            try
            {
                Estimate estimate = db.Estimates.Where(c => c.EstimateId == estimateId).FirstOrDefault();

                estimate.isEmailSent = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public void SendPendingCorporateUserRegistrationEmailToAdmins(int contactID, int Companyid, Organisation serverSetting)
        {
            try
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
                    Campaign CorporateOrderForApprovalCampaign = GetCampaignRecordByEmailEvent((int)Events.CorporateRegistrationForApproval, serverSetting.OrganisationId, Companyid);
                    Organisation SeverSettings = serverSetting;
                    foreach (CompanyContact corpRec in listOfApprovers)
                    {
                        obj.ApprovarID = (int)corpRec.ContactId;
                        obj.ContactId = contactID;
                        obj.CompanyId = Companyid;
                        obj.SalesManagerContactID = corpRec.ContactId;
                        obj.StoreId = Companyid;
                        obj.AddressId = Companyid;
                        emailBodyGenerator(CorporateOrderForApprovalCampaign, SeverSettings, obj, corpRec, StoreMode.Corp, "", "", "", corpRec.Email, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public void SendEmailFromQueue(System.Web.HttpContext hcontext)
        {
            try
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
                            if (string.IsNullOrEmpty(record.SMTPPassword) || string.IsNullOrEmpty(record.SMTPUserName) || string.IsNullOrEmpty(record.SMTPServer))
                            {
                                record.ErrorResponse = "smtp Settings not found.";
                                record.AttemptCount++;
                                db.SaveChanges();
                            }
                            else 
                            {
                                if (SendEmail(record, hcontext, out ErrorMsg))
                                {
                                    if (record.FileAttachment != null)
                                    {
                                        res = true;
                                    }

                                    if (res)
                                    {

                                        //string filePath = string.Empty;
                                        //string[] Allfiles = record.FileAttachment.Split('|');
                                        //foreach (var file in Allfiles)
                                        //{
                                        //    filePath = hcontext.Server.MapPath(file);
                                        //    if (File.Exists(filePath))
                                        //        File.Delete(filePath);
                                        //}

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
                        else
                        {
                            record.AttemptCount++;
                            db.SaveChanges();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               throw ex;
            }
        }



        private bool SendEmail(CampaignEmailQueue oEmailBody, System.Web.HttpContext context, out string ErrorMsg)
        {
            try
            {
                bool isFileExists = true;

                if (string.IsNullOrEmpty(oEmailBody.EmailFrom))
                {
                    ErrorMsg = "";
                    return false;
                }
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
                            if (File.Exists(FilePath))
                            {
                                data = new Attachment(FilePath, MediaTypeNames.Application.Octet);
                                ContentDisposition disposition = data.ContentDisposition;
                                disposition.CreationDate = System.IO.File.GetCreationTime(FilePath);
                                disposition.ModificationDate = System.IO.File.GetLastWriteTime(FilePath);
                                disposition.ReadDate = System.IO.File.GetLastAccessTime(FilePath);
                                disposition.FileName = fname;
                                objMail.Attachments.Add(data);
                            }
                            else 
                            {
                                isFileExists = false;
                            }
                        }
                    }
                }
                if (isFileExists == true)
                {
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
                else 
                {
                    ErrorMsg = "Attachment not found.";
                    return false;
                }
             

            }
            catch (Exception ex)
            {
                throw ex;

            }

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

        public void EmailsToCorpUser(long orderID, long contactID, StoreMode ModeOfStore, long loggedinTerritoryId, Organisation serverSettings, long StoreId, string SalesManagerEmail)
        {
            try
            {
                int admin = Convert.ToInt32(Roles.Adminstrator);
                int Manager = Convert.ToInt32(Roles.Manager);
                CampaignEmailParams obj = new CampaignEmailParams();
                List<CompanyContact> listOfApprovers = new List<CompanyContact>();
              
                    long ContactCompnyID = (from c in db.CompanyContacts
                                           where c.ContactId == contactID
                                           select c.CompanyId).FirstOrDefault();

                    listOfApprovers = (from c in db.CompanyContacts
                                       join cc in db.Companies on ContactCompnyID equals cc.CompanyId
                                       where (c.ContactRoleId == admin || 
                                       (c.ContactRoleId == Manager && c.TerritoryId == loggedinTerritoryId)) 
                                       && (cc.IsCustomer == (int)CustomerTypes.Corporate) 
                                       && c.CompanyId == ContactCompnyID
                                       select c).ToList();
                    if (listOfApprovers.Count() > 0)
                    {
                        Campaign CorporateOrderForApprovalCampaign = GetCampaignRecordByEmailEvent((int)Events.CorporateOrderForApproval, serverSettings.OrganisationId, StoreId);

                        foreach (CompanyContact corpRec in listOfApprovers)
                        {
                            obj.ApprovarID = (int)corpRec.ContactId;
                            obj.ContactId = contactID;
                            obj.EstimateId = orderID;
                            obj.SalesManagerContactID = corpRec.ContactId;
                            obj.StoreId = StoreId;
                            obj.AddressId = ContactCompnyID;
                            obj.CompanyId = ContactCompnyID;
                            obj.OrganisationId = serverSettings.OrganisationId;
                            emailBodyGenerator(CorporateOrderForApprovalCampaign, serverSettings, obj, corpRec, ModeOfStore, "", "", "", SalesManagerEmail, "", corpRec.Email);
                        }
                    }
                
            }
            catch (Exception e)
            {
               
            }
        }


        public void POEmailToSalesManager(long orderID, long companyID, long contactID, int reportNotesID, long supplierCompanyID, string AttachmentListStr,Company objCompany)
        {
            Guid saleManagerId = new Guid();
            List<string> AttachmentList = new List<string>();
            string[] objs = AttachmentListStr.Split('|');
            foreach (string item in objs)
            {
                AttachmentList.Add(item);
            }

            CompanyContact user = db.CompanyContacts.Where(c => c.ContactId == contactID).FirstOrDefault();
            //tbl_company_sites ServerSettings = CompanySiteManager.GetCompanySite();
            Organisation ServerSettings = db.Organisations.Where(c => c.OrganisationId == OrganisationId).FirstOrDefault();
            SystemUser SalesManager = null;

            db.Configuration.LazyLoadingEnabled = false;

            if(objCompany.IsCustomer == 3)// corporate
            {
                saleManagerId = objCompany.SalesAndOrderManagerId1 ?? Guid.NewGuid();
            }
            else
            {
                // if retail
                var manageriD = db.Companies.Where(c => c.CompanyId == objCompany.StoreId).Select(c => c.SalesAndOrderManagerId1).FirstOrDefault();
                saleManagerId = manageriD ?? Guid.NewGuid();
            }
            
            SalesManager = db.SystemUsers.Where(c => c.SystemUserId == saleManagerId).FirstOrDefault();
            if (SalesManager != null)
            {
                
                CampaignEmailParams CEP = new CampaignEmailParams();
                Campaign EventCampaign = GetCampaignRecordByEmailEvent((long)Events.PO_Notification_To_SalesManager, OrganisationId, companyID);
                CEP.EstimateId = orderID;
                CEP.CompanyId = companyID;
                CEP.ContactId = contactID;
                CEP.StoreId = companyID;
                CEP.SalesManagerContactID = contactID;
                CEP.OrganisationId = OrganisationId;
                CEP.AddressId = companyID;
                CEP.SystemUserId = SalesManager.SystemUserId;
                CEP.Id = reportNotesID;
                CEP.SupplierCompanyID = (int)supplierCompanyID;
                if (objCompany.IsCustomer == 3)
                {
                    //emailBodyGenerator(EventCampaign,ser)
                    emailBodyGenerator(EventCampaign, ServerSettings, CEP, null, StoreMode.Corp, "", "", "", SalesManager.Email, "", "", AttachmentList);
                    // emailBodyGenerator(EventCampaign, ServerSettings, CEP, null, StoreMode.Corp, "", "", "", SalesManager.Email, "", "", AttachmentList, "", null, "", "", null, "", "", "", 0, "", 0);
                }
                else
                {
                    emailBodyGenerator(EventCampaign, ServerSettings, CEP, null, StoreMode.Retail, "", "", "", SalesManager.Email, "", "", AttachmentList);
                }

                
            }
        }
        public void POEmailToSupplier(long orderID, long companyID, long contactID, int reportNotesID, long supplierContactID, string AttachmentListStr, Company objCompany, bool isCancellation)
        {
             Guid saleManagerId  = new Guid();
            List<string> AttachmentList = new List<string>();
            string[] objs = AttachmentListStr.Split('|');
            foreach (string item in objs)
            {
                AttachmentList.Add(item);
            }
            //UsersManager usermgr = new UsersManager();

            // here is problem supplier user is null .. bcox in parameter we are sendin
            Campaign EventCampaign = new Campaign();
            CompanyContact supplieruser = db.CompanyContacts.Where(c => c.CompanyId == supplierContactID && c.IsDefaultContact == 1).FirstOrDefault();
            Organisation ServerSettings = db.Organisations.Where(c => c.OrganisationId == OrganisationId).FirstOrDefault();

            SystemUser SalesManager = null;

            if (objCompany.IsCustomer == 3)// corporate
            {
                saleManagerId = objCompany.SalesAndOrderManagerId1 ?? Guid.NewGuid();
            }
            else
            {
                // if retail
                var manageriD = db.Companies.Where(c => c.CompanyId == objCompany.StoreId).Select(c => c.SalesAndOrderManagerId1).FirstOrDefault();
                saleManagerId = manageriD ?? Guid.NewGuid();
            }

            SalesManager = db.SystemUsers.Where(c => c.SystemUserId == saleManagerId).FirstOrDefault();
            if(SalesManager != null)
            {
                if (supplieruser != null)
                {

                    CampaignEmailParams CEP = new CampaignEmailParams();
                    if (isCancellation)
                    {
                        EventCampaign = GetCampaignRecordByEmailEvent(Convert.ToInt16(Events.PO_CancellationEmail_To_Supplier), OrganisationId, companyID);
                    }
                    else
                    {
                        EventCampaign = GetCampaignRecordByEmailEvent(Convert.ToInt16(Events.PO_Notification_To_Supplier), OrganisationId, companyID);
                    }

                    CEP.EstimateId = orderID;
                    CEP.CompanyId = companyID;
                    CEP.ContactId = contactID;
                    CEP.StoreId = companyID;
                    CEP.SalesManagerContactID = contactID;
                    CEP.OrganisationId = OrganisationId;
                    CEP.AddressId = companyID;
                    CEP.Id = reportNotesID;
                    CEP.EstimateId = orderID;
                    CEP.SupplierCompanyID = supplieruser.CompanyId;
                    if (objCompany.IsCustomer == 3)
                    {

                        emailBodyGenerator(EventCampaign, ServerSettings, CEP, supplieruser, StoreMode.Corp, "", "", "", SalesManager.Email, "", "", AttachmentList);

                    }
                    else
                    {
                        emailBodyGenerator(EventCampaign, ServerSettings, CEP, supplieruser, StoreMode.Retail, "", "", "", SalesManager.Email, "", "", AttachmentList);
                    }

                }
            }
           
        }
        public void stockNotificationToManagers(List<Guid> mangerList, long CompanyId, Organisation ServerSettings, StoreMode ModeOfStore, long salesId, long itemId, long emailevent, long contactId, long orderedItemid, long StockItemId, long OrderId)
        {
            try
            {

                CampaignEmailParams obj = new CampaignEmailParams();

                List<SystemUser> Managers = db.SystemUsers.Where(s => mangerList.Contains(s.SystemUserId)).ToList();
                if (Managers.Count() > 0)
                {
                    Campaign stockCampaign = GetCampaignRecordByEmailEvent(emailevent, ServerSettings.OrganisationId, CompanyId);

                    foreach (SystemUser stRec in Managers)
                    {
                        obj.SystemUserId = stRec.SystemUserId;
                        obj.SalesManagerContactID = salesId;
                        obj.StoreId = CompanyId;
                        obj.CompanyId = CompanyId;
                        obj.OrganisationId = ServerSettings.OrganisationId;
                        obj.ItemId = itemId;
                        obj.ContactId = contactId;
                        obj.orderedItemID = (int)orderedItemid;
                        obj.StockItemId = StockItemId;
                        obj.EstimateId = OrderId;
                        emailBodyGenerator(stockCampaign, ServerSettings, obj, null, ModeOfStore, "", "", "", stRec.Email, stRec.FullName);

                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
