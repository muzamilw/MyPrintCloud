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
        Campaign GetCampaignRecordByEmailEvent(int iEmailEvent);

        bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0);

        void SendEmailToSalesManager(int Event, int ContactId, int CompanyId, int brokerid, int OrderId, Organisation ServerSettings, int BrokerAdminContactID, int CorporateManagerID, StoreMode Mode,Company company,SystemUser SaleManager,int ItemID, string NameOfBrokerComp = "", string MarketingBreifMesgSummry = "", int RFQId = 0);

    }
}
