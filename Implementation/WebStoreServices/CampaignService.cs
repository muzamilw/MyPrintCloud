using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace MPC.Implementation.WebStoreServices
{
    public class CampaignService : ICampaignService
    {
        public readonly ICampaignRepository _CampaignRepository;
        public readonly IUserManagerRepository _UserManagerRepository;
        public readonly IOrderRepository _OrderRepository;
        public readonly IOrganisationRepository _organisationRepsoitory;
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CampaignService(ICampaignRepository CampaignRepository, IUserManagerRepository UserManagerRepository,
            IOrderRepository OrderRepository,IOrganisationRepository organisationRepository)
        {
            this._CampaignRepository = CampaignRepository;
            this._UserManagerRepository = UserManagerRepository;
            this._OrderRepository = OrderRepository;
            this._organisationRepsoitory = organisationRepository;
        }


        #endregion

        public Campaign GetCampaignRecordByEmailEvent(int iEmailEvent, long OrganisationId, long CompanyId)
        {
            return _CampaignRepository.GetCampaignRecordByEmailEvent(iEmailEvent, OrganisationId, CompanyId);
        }

        public bool emailBodyGenerator(Campaign oCampaign, CampaignEmailParams variablValues, CompanyContact userRecord, StoreMode ModeOfStore, int OrganisationId, string password = "", string shopReceiptHtml = "", string emailOfSubscribedUsers = "", string emailOfSalesManager = "", string ReceiverName = "", string secondEmail = "", List<string> AttachmentsList = null, string PostCodes = "", DateTime? SubscriptionEndDate = null, string PayyPalGatwayEmail = "", string brokerCompanyName = "", string SubscriptionPath = "", string MarkBreifSumm = "", string Email1 = "", int UnOrderedTotalItems = 0, string UnOrderedItemsTotal = "", int SavedDesignsCount = 0, string ITemtypefourHtml = "")
        {
            Organisation CompOrganisation = _organisationRepsoitory.GetOrganizatiobByID(OrganisationId);
            return _CampaignRepository.emailBodyGenerator(oCampaign, CompOrganisation, variablValues, userRecord, ModeOfStore, password, shopReceiptHtml, emailOfSubscribedUsers, emailOfSalesManager, ReceiverName, secondEmail, AttachmentsList, PostCodes, SubscriptionEndDate, PayyPalGatwayEmail, brokerCompanyName, SubscriptionPath, MarkBreifSumm, Email1, UnOrderedTotalItems, UnOrderedItemsTotal, SavedDesignsCount, ITemtypefourHtml);
        }

        public void SendEmailToSalesManager(int Event, long ContactId, long CompanyId, long OrderId, long OrganisationId, int CorporateManagerID, StoreMode Mode, long StoreId, SystemUser SaleManager, string NameOfBrokerComp = "", string MarketingBreifMesgSummry = "", int RFQId = 0)
        {
            int ItemIDs = _OrderRepository.GetFirstItemIDByOrderId(OrderId);
            Organisation CompOrganisation = _organisationRepsoitory.GetOrganizatiobByID(OrganisationId);
            _CampaignRepository.SendEmailToSalesManager(Event, ContactId, CompanyId, OrderId, CompOrganisation, OrganisationId, CorporateManagerID, Mode, StoreId, SaleManager, ItemIDs, NameOfBrokerComp, MarketingBreifMesgSummry, RFQId);

        }

        public string GetPinkCardsShopReceiptPage(int OrderId, long CorpID)
        {
            return _CampaignRepository.GetPinkCardsShopReceiptPage(OrderId, CorpID);
            
        }
        public void SendPendingCorporateUserRegistrationEmailToAdmins(int contactID, int Companyid,int organisationId)
        {
            Organisation serverSetting = _organisationRepsoitory.GetOrganizatiobByID(organisationId);
            _CampaignRepository.SendPendingCorporateUserRegistrationEmailToAdmins(contactID,Companyid,serverSetting);
        }

        public void SendEmailFromQueue(HttpContext context)
        {
            try 
            {
                _CampaignRepository.SendEmailFromQueue(context);
            }
            catch(Exception ex)
            {
                string virtualFolderPth = context.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");
                
                using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                
            }
            
        }
        
        public void MonitorScheduledEmails()
        {
            _CampaignRepository.MonitorScheduledEmails();
        }
        public bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID)
        {
            return _CampaignRepository.AddMsgToTblQueue(Toemail,CC,ToName,msgbody,fromName,fromEmail,smtpUserName,ServerPass,ServerName,subject,AttachmentList,CampaignReportID);
        }
        public void EmailsToCorpUser(long orderID, long contactID, StoreMode ModeOfStore, long loggedinTerritoryId, Organisation serverSettings, long StoreId, string SalesManagerEmail)
        {
            _CampaignRepository.EmailsToCorpUser(orderID, contactID, ModeOfStore, loggedinTerritoryId, serverSettings, StoreId, SalesManagerEmail);
        }
    }

    
}
