using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace MPC.Implementation.MISServices
{
    public class PurchaseService : IPurchaseService
    {
        #region Private

        private readonly IPurchaseRepository purchaseRepository;
        private readonly IPurchaseDetailRepository purchaseDetailRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ISystemUserRepository systemUserRepository;
        private readonly IOrganisationRepository organisationRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly ICampaignRepository campaignRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IExportReportHelper exportReportHelper;
        private readonly ICompanyRepository companyRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public PurchaseService(IPurchaseRepository purchaseRepository, ISectionFlagRepository sectionFlagRepository, ISystemUserRepository systemUserRepository,
            IOrganisationRepository organisationRepository, IPrefixRepository prefixRepository, IPurchaseDetailRepository purchaseDetailRepository, IExportReportHelper ExportReportHelper, ICampaignRepository campaignRepository, IOrderRepository OrderRepository,
            ICompanyRepository companyRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.systemUserRepository = systemUserRepository;
            this.organisationRepository = organisationRepository;
            this.prefixRepository = prefixRepository;
            this.purchaseDetailRepository = purchaseDetailRepository;
            this.exportReportHelper = ExportReportHelper;
            this.campaignRepository = campaignRepository;
            this.orderRepository = OrderRepository;
            this.companyRepository = companyRepository;
        }

        #endregion

        #region Public
        public PurchaseResponseModel GetPurchases(PurchaseRequestModel requestModel)
        {
            return purchaseRepository.GetPurchases(requestModel);
        }

        public PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel request)
        {
            return purchaseRepository.GetPurchaseOrders(request);
        }

        /// <summary>
        /// base Data for Purchase
        /// </summary>
        public PurchaseBaseResponse GetBaseData()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new PurchaseBaseResponse
            {
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Order),
                SystemUsers = systemUserRepository.GetAll(),
                CurrencySymbol = organisation != null ? (organisation.Currency != null ? organisation.Currency.CurrencySymbol : string.Empty) : string.Empty
            };
        }

        /// <summary>
        /// Save Purchase
        /// </summary>
        public Purchase SavePurchase(Purchase purchase)
        {
            // Get Purchase if exists else create new
            Purchase purchaseTarget = GetById(purchase.PurchaseId) ?? CreatePurchase();
            // Update Order
            purchase.UpdateTo(purchaseTarget, new PurchaseMapperActions
            {
                CreatePurchaseDetail = CreatePurchaseDetail,
                DeletePurchaseDetail = DeletePurchaseDetail,
            });

            // Save Changes
            purchaseRepository.SaveChanges();
            return GetById(purchaseTarget.PurchaseId);
        }

        /// <summary>
        /// 
        /// </summary>
        public Purchase GetPurchaseById(int purchaseId)
        {
            return purchaseRepository.Find(purchaseId);
        }

        /// <summary>
        /// Creates New Purchase Detail
        /// </summary>
        private PurchaseDetail CreatePurchaseDetail()
        {
            PurchaseDetail itemTarget = purchaseDetailRepository.Create();
            purchaseDetailRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Purchase Detail
        /// </summary>
        private void DeletePurchaseDetail(PurchaseDetail purchaseDetail)
        {
            purchaseDetailRepository.Delete(purchaseDetail);
        }
        /// <summary>
        /// Get By Id
        /// </summary>
        public Purchase GetById(long purchaseId)
        {
            return purchaseRepository.Find(purchaseId);
        }

        /// <summary>
        /// Creates New Purchse and assigns new generated code
        /// </summary>
        private Purchase CreatePurchase()
        {
            string code = prefixRepository.GetNextPurchaseCodePrefix();
            Purchase itemTarget = purchaseRepository.Create();
            purchaseRepository.Add(itemTarget);
            itemTarget.Code = code;
            return itemTarget;
        }

        /// <summary>
        /// Delete Purchase Order
        /// </summary>
        public void DeletePurchaseOrder(int purchaseId)
        {
            purchaseRepository.Delete(purchaseRepository.Find(purchaseId));
            purchaseRepository.SaveChanges();
        }


        public bool GeneratePO(long OrderID,long ContactID, long CompanyId,Guid CreatedBy)
        {


            string ServerPath = HttpContext.Current.Request.Url.Host;
            bool IsPOGenerate = purchaseRepository.GeneratePO(OrderID, CreatedBy);
            if (IsPOGenerate)
            {
                POEmail(ServerPath, OrderID, ContactID, CompanyId);
            }

            
            
            return true;
        }

        public void POEmail(string ServerPath, long OrderID, long ContactID, long CompanyId)
        {
           // string szDirectory = WebConfigurationManager.AppSettings["VirtualDirectory"].ToString();
            List<string> AttachmentsList = new List<string>();


            var ListPurchases = purchaseRepository.GetPurchasesList(OrderID);


            if (ListPurchases != null)
            {


                foreach (var purchase in ListPurchases)
                {
                    string FileName = exportReportHelper.ExportPDF(100, purchase.Key,ReportType.PurchaseOrders,OrderID,string.Empty);

                   


                    int ItemIDs = orderRepository.GetFirstItemIDByOrderId(OrderID);
                    Organisation CompOrganisation = organisationRepository.GetOrganizatiobByID();
                    Company objCompany = companyRepository.GetCompanyByCompanyID(CompanyId);
                    SystemUser saleManager = systemUserRepository.GetUserrById(objCompany.SalesAndOrderManagerId1 ?? Guid.NewGuid());


                    string SalesManagerFile = ImagePathConstants.ReportPath + CompOrganisation.OrganisationId + "/" + purchase.Key + "_PurchaseOrder.pdf";
                    campaignRepository.POEmailToSalesManager(OrderID,CompanyId,ContactID,250,purchase.Value,SalesManagerFile,objCompany);
                    
                    
                    
                    if(objCompany.IsCustomer == (int)CustomerTypes.Corporate)
                    {
                        campaignRepository.SendEmailToSalesManager((int)Events.PO_Notification_To_SalesManager, ContactID, CompanyId, OrderID, CompOrganisation, CompOrganisation.OrganisationId,0, StoreMode.Corp, CompanyId,saleManager, ItemIDs, "", "", 0);
                    }
                    else
                    {
                        campaignRepository.SendEmailToSalesManager((int)Events.PO_Notification_To_SalesManager, ContactID, CompanyId, OrderID, CompOrganisation, CompOrganisation.OrganisationId, 0, StoreMode.Retail, CompanyId, saleManager, ItemIDs, "", "", 0);
                    }


                    string SourceFile = FileName;
                    string DestinationFileSupplier = ImagePathConstants.ReportPath + CompOrganisation.OrganisationId + "/" + purchase.Value + "/" + purchase.Key + "_PurchaseOrder.pdf";

                    string DestinationPhysicalFileSupplier = HttpContext.Current.Server.MapPath(DestinationFileSupplier);
                    if (File.Exists(SourceFile))
                    {
                        File.Copy(SourceFile, DestinationPhysicalFileSupplier);
                    }

                    campaignRepository.POEmailToSupplier(OrderID,CompanyId,ContactID,250,purchase.Value,DestinationFileSupplier,objCompany);

                    
                   // SendEmailToSupplier(ServerPath, OrderID, ContactCompanyID, ContactID, 250, purchase.SupplierID ?? 0, DestinationFileSupplier);




                    // AttachmentsList.Add(FilePath);

                }
             
            }


        }



        public void DeletePO(long OrderID)
        {

            purchaseRepository.DeletePO(OrderID);

        }
        #endregion
    }
}
