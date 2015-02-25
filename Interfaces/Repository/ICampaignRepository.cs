using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace MPC.Interfaces.Repository
{
    public interface ICampaignRepository : IBaseRepository<Campaign, long>
    {
        Campaign GetCampaignRecordByEmailEvent(long iEmailEvent);

        bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0);

        void SendEmailToSalesManager(int Event, int ContactId, int CompanyId, int brokerid, int OrderId, Organisation ServerSettings, int BrokerAdminContactID, int CorporateManagerID, StoreMode Mode, Company company, SystemUser SaleManager, int ItemID, string NameOfBrokerComp = "", string MarketingBreifMesgSummry = "", int RFQId = 0);
        string GetPinkCardsShopReceiptPage(int OrderId, long CorpID);
        void SendPendingCorporateUserRegistrationEmailToAdmins(int contactID, int Companyid,Organisation ServerSetting);

        void SendEmailFromQueue(System.Web.HttpContext hcontext);
        void MonitorScheduledEmails();

        bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID);

    }
}
