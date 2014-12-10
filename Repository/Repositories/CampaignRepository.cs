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


        //public bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", HttpContext oHttpContext = null, string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0)
        //{
          
        //        //string mesgBody = null;
        //        //string To = null;
        //        //string ToName = null;
        //        //string mailFrom = null;
        //        //string fromName = null;
        //        //string smtpServer = null;
        //        //string smtpUserName = null;
        //        //string mailPassword = null;
        //        //string HtmlText = null;


        //        //if (oHttpContext == null)
        //        //    oHttpContext = HttpContext.Current;



        //        //bool result = false;

        //        //if (oCampaign != null)
        //        //{
        //        //    if (SeverSettings != null)
        //        //    {
        //        //        smtpUserName = SeverSettings.SmtpUserName;
        //        //        smtpServer = SeverSettings.SmtpServer;
        //        //        mailPassword = SeverSettings.SmtpPassword;
        //        //    }

        //        //    HtmlText = oCampaign.HTMLMessageA;





        //        //    if (string.IsNullOrEmpty(HtmlText))
        //        //    {
        //        //        if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
        //        //        {
        //        //            CountOfEmailsFailed += 1;
        //        //        }
        //        //        //do nothing
        //        //        return false;
        //        //    }
        //        //    else
        //        //    {

        //        //        string DecodedHtml = "";

        //        //        if (HttpContext.Current == null)
        //        //            DecodedHtml = oHttpContext.Server.HtmlDecode(HtmlText);
        //        //        else
        //        //            DecodedHtml = HttpContext.Current.Server.HtmlDecode(HtmlText);


        //        //        PropertyInfo[] propertyInfos = variablValues.GetType().GetProperties();
        //        //        // RESOLVE VARIABLES FOR MESS BODY

        //        //        DecodedHtml = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, password, PostCodes, Convert.ToString(SubscriptionEndDate), PayyPalGatwayEmail, SubscriptionPath, MarkBreifSumm, UnOrderedTotalItems, UnOrderedItemsTotal, SavedDesignsCount);

        //        //        mesgBody = DecodedHtml;

        //        //        // RESOLVE VARIABLES FOR SUBJECT
        //        //        if (HttpContext.Current == null)
        //        //            DecodedHtml = oHttpContext.Server.HtmlDecode(oCampaign.SubjectA);
        //        //        else
        //        //            DecodedHtml = HttpContext.Current.Server.HtmlDecode(oCampaign.SubjectA);

        //        //        DecodedHtml = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, "", "", "", "", SubscriptionPath, MarkBreifSumm);


        //        //        oCampaign.SubjectA = DecodedHtml;
        //        //        //
        //        //        if (string.IsNullOrEmpty(oCampaign.FromName) || string.IsNullOrEmpty(oCampaign.FromAddress))
        //        //        {
        //        //            fromName = ""; // Name of the sender
        //        //            mailFrom = "";
        //        //        }
        //        //        else
        //        //        {
        //        //            // RESOLVE VARIABLES FOR From Name and from Address

        //        //            if (HttpContext.Current == null)
        //        //                DecodedHtml = oHttpContext.Server.HtmlDecode(oCampaign.FromName);
        //        //            else
        //        //                DecodedHtml = HttpContext.Current.Server.HtmlDecode(oCampaign.FromName);
        //        //            fromName = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, "", "", "", "", SubscriptionPath, MarkBreifSumm); //oCampaign.FromName; // Name of the sender


        //        //            // RESOLVE VARIABLES FOR from Address

        //        //            if (HttpContext.Current == null)
        //        //                DecodedHtml = oHttpContext.Server.HtmlDecode(oCampaign.FromAddress);
        //        //            else
        //        //                DecodedHtml = HttpContext.Current.Server.HtmlDecode(oCampaign.FromAddress);
        //        //            mailFrom = ResolveVariablesInHtml(DecodedHtml, propertyInfos, variablValues, SeverSettings, ModeOfStore, emailOfSalesManager, oHttpContext, "", "", "", "", SubscriptionPath, MarkBreifSumm);// oCampaign.FromAddress;

        //        //        }

        //        //        if (oCampaign.EmailEvent == (int)Events.OnlineOrder)
        //        //        {
        //        //            mesgBody += shopReceiptHtml;
        //        //        }
        //        //        if (userRecord != null)
        //        //        {
        //        //            To = userRecord.Email;
        //        //            ToName = userRecord.FirstName + " " + userRecord.LastName;
        //        //        }
        //        //        else if (!string.IsNullOrEmpty(Email1) && userRecord == null)
        //        //        {
        //        //            To = Email1;
        //        //            ToName = Email1;
        //        //        }
        //        //        else if (!string.IsNullOrEmpty(emailOfSubscribedUsers))// order information will be sent to the subscribed users
        //        //        {
        //        //            To = emailOfSubscribedUsers;
        //        //            ToName = ReceiverName;
        //        //        }
        //        //        else if (!string.IsNullOrEmpty(emailOfSalesManager))
        //        //        {
        //        //            // order information will be sent to the sales manager

        //        //            To = emailOfSalesManager;
        //        //            ToName = ReceiverName;
        //        //        }
        //        //        else
        //        //        {


        //        //            userRecord = UsersManager.GetContactByID(Convert.ToInt32(variablValues.ContactID));
        //        //            if (userRecord != null)
        //        //            {
        //        //                To = userRecord.Email;
        //        //                ToName = userRecord.FirstName + " " + userRecord.LastName;
        //        //            }
        //        //        }

        //        //        if (string.IsNullOrEmpty(To) || string.IsNullOrEmpty(smtpUserName) || string.IsNullOrEmpty(smtpServer))
        //        //        {
        //        //            if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
        //        //            {
        //        //                CountOfEmailsFailed += 1;
        //        //            }
        //        //            return false;
        //        //        }
        //        //        else
        //        //        {

        //        //            if (ContactManager.ValidatEmail(To))
        //        //            {

        //        //                if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
        //        //                {
        //        //                    result = AddMsgToTblQueue(To, secondEmail, ToName, mesgBody, fromName, mailFrom, smtpUserName, mailPassword, smtpServer, oCampaign.SubjectA, AttachmentsList, Convert.ToInt32(oCampaign.CampaignReportID));
        //        //                }
        //        //                else
        //        //                {
        //        //                    result = AddMsgToTblQueue(To, secondEmail, ToName, mesgBody, fromName, mailFrom, smtpUserName, mailPassword, smtpServer, oCampaign.SubjectA, AttachmentsList, 0);
        //        //                }

        //        //                if (oCampaign.EmailEvent == (int)EmailEvents.OnlineOrder)
        //        //                {
        //        //                    if (result)
        //        //                    {
        //        //                        UpdateEstimateRecord(variablValues.EstimateID);
        //        //                    }
        //        //                }
        //        //            }
        //        //            else
        //        //            {
        //        //                if (oCampaign.CampaignType == Convert.ToInt32(Campaigns.MarketingCampaign))
        //        //                {
        //        //                    CountOfEmailsFailed += 1;
        //        //                }
        //        //            }

        //        //            return result;
        //        //        }
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    return false;
        //        //}
        //    return true;
        //    }

    }
}
