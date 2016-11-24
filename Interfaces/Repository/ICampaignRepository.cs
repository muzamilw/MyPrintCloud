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
        Campaign GetCampaignRecordByEmailEvent(long iEmailEvent, long OrganisationId, long CompanyId);

        bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0, string ITemtypefourHtml = "", string HiResArtworkLink = "");

        void SendEmailToSalesManager(int Event, long ContactId, long CompanyId, long OrderId,
            Organisation ServerSettings, long OrganisationId, int CorporateManagerID, StoreMode Mode, long StoreId,
            SystemUser SalesManager, int ItemID, string NameOfComp = "", string MarketingBreifMesgSummry = "",
            int RFQId = 0);
        string GetPinkCardsShopReceiptPage(int OrderId, long CorpID);
        void SendPendingCorporateUserRegistrationEmailToAdmins(int contactID, int Companyid,Organisation ServerSetting);

        void SendEmailFromQueue(System.Web.HttpContext hcontext);
        void MonitorScheduledEmails();

        bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID);

        void EmailsToCorpUser(long orderID, long contactID, StoreMode ModeOfStore, long loggedinTerritoryId, Organisation serverSettings, long StoreId, string SalesManagerEmail);

        void POEmailToSalesManager(long orderID, long companyID, long contactID, int reportNotesID, long supplierCompanyID, string AttachmentListStr, Company objCompany, long organisationId = 0);

        void POEmailToSupplier(long orderID, long companyID, long contactID, int reportNotesID, long supplierContactID, string AttachmentListStr, Company objCompany, bool isCancellation, long organisationId = 0);

        void stockNotificationToManagers(List<Guid> mangerList, long CompanyId, Organisation ServerSettings, StoreMode ModeOfStore, long salesId, long itemId, long emailevent, long contactId, long orderedItemid, long StockItemId, long orderID);
        List<Campaign> GetOrganisationCampaigns();
        void OrderProcessingNotificationEmail(long eventId, Estimate order);
        void ProductTemplateNotificationEmail(long eventId, string emails, long itemId);
        Campaign GetMisCampaignEmailByEvent(long emailEvent);
    }
}
