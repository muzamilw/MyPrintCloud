using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    public class CampaignService : ICampaignService
    {
        public readonly ICampaignRepository _CampaignRepository;
        public readonly IUserManagerRepository _UserManagerRepository;
        public readonly IOrderRepository _OrderRepository;

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CampaignService(ICampaignRepository CampaignRepository, IUserManagerRepository UserManagerRepository,
            IOrderRepository OrderRepository)
        {
            this._CampaignRepository = CampaignRepository;
            this._UserManagerRepository = UserManagerRepository;
            this._OrderRepository = OrderRepository;
        }


        #endregion

        public Campaign GetCampaignRecordByEmailEvent(int iEmailEvent)
        {
            return _CampaignRepository.GetCampaignRecordByEmailEvent(iEmailEvent);
        }

        public bool emailBodyGenerator(Campaign oCampaign, Organisation SeverSettings, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0)
        {

            return _CampaignRepository.emailBodyGenerator(oCampaign, SeverSettings, variablValues, userRecord, ModeOfStore, password, shopReceiptHtml, emailOfSubscribedUsers, emailOfSalesManager, ReceiverName, secondEmail, AttachmentsList, PostCodes, SubscriptionEndDate, PayyPalGatwayEmail, brokerCompanyName, SubscriptionPath, MarkBreifSumm, Email1, UnOrderedTotalItems, UnOrderedItemsTotal, SavedDesignsCount);
        }

        public void SendEmailToSalesManager(int Event, int ContactId, int CompanyId, int brokerid, int OrderId, Organisation ServerSettings, int BrokerAdminContactID, int CorporateManagerID, StoreMode Mode, Company company, SystemUser SaleManager, int ItemID, string NameOfBrokerComp = "", string MarketingBreifMesgSummry = "", int RFQId = 0)
        {
            SystemUser SaleManagerParam = _UserManagerRepository.GetSalesManagerDataByID(Convert.ToInt32(company.SalesAndOrderManagerId1));
            int ItemIDs = _OrderRepository.GetFirstItemIDByOrderId(OrderId);
            _CampaignRepository.SendEmailToSalesManager(Event, ContactId, CompanyId, brokerid, OrderId, ServerSettings, BrokerAdminContactID, CorporateManagerID, Mode, company, SaleManager, ItemIDs, NameOfBrokerComp, MarketingBreifMesgSummry, RFQId);

        }
    }

    
}
