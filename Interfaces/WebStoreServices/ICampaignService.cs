using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ICampaignService
    {
        Campaign GetCampaignRecordByEmailEvent(int iEmailEvent, long OrganisationId, long CompanyId);

        bool emailBodyGenerator(Campaign oCampaign, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, int OrganisationId, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0, string ITemtypefourHtml = "");

        void SendEmailToSalesManager(int Event, long ContactId, long CompanyId, long OrderId, long OrganisationId,
            int CorporateManagerID, StoreMode Mode, long StoreId, SystemUser SaleManager, string NameOfBrokerComp = "",
            string MarketingBreifMesgSummry = "", int RFQId = 0);
        string GetPinkCardsShopReceiptPage(int OrderId, long CorpID);
        void SendPendingCorporateUserRegistrationEmailToAdmins(int contactID, int Companyid,int OrganisationId);

        void SendEmailFromQueue(System.Web.HttpContext hcontext);

        void MonitorScheduledEmails();

        bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID);
        void EmailsToCorpUser(long orderID, long contactID, StoreMode ModeOfStore, long loggedinTerritoryId, Organisation serverSettings, long StoreId, string SalesManagerEmail);
   
    }
}
