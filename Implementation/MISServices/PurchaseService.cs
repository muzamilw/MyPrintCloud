using System.Linq;
using System.Text;
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
using MPC.Repository.Repositories;

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
        private readonly IGoodRecieveNoteRepository goodRecieveNoteRepository;
        private readonly ICampaignRepository campaignRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IExportReportHelper exportReportHelper;
        private readonly IDeliveryCarrierRepository deliveryCarrierRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public PurchaseService(IPurchaseRepository purchaseRepository, ISectionFlagRepository sectionFlagRepository, ISystemUserRepository systemUserRepository,
             IGoodRecieveNoteRepository goodRecieveNoteRepository,
             OrganisationRepository organisationRepository, IPrefixRepository prefixRepository, IPurchaseDetailRepository purchaseDetailRepository, IExportReportHelper ExportReportHelper, ICampaignRepository campaignRepository, IOrderRepository OrderRepository,
            ICompanyRepository companyRepository, IDeliveryCarrierRepository deliveryCarrierRepository
           )
        {
            this.purchaseRepository = purchaseRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.systemUserRepository = systemUserRepository;
            this.organisationRepository = organisationRepository;
            this.prefixRepository = prefixRepository;
            this.purchaseDetailRepository = purchaseDetailRepository;

            this.goodRecieveNoteRepository = goodRecieveNoteRepository;

            this.exportReportHelper = ExportReportHelper;
            this.exportReportHelper = ExportReportHelper;
            this.campaignRepository = campaignRepository;
            this.orderRepository = OrderRepository;
            this.companyRepository = companyRepository;
            this.campaignRepository = campaignRepository;
            this.orderRepository = OrderRepository;
            this.companyRepository = companyRepository;
            this.deliveryCarrierRepository = deliveryCarrierRepository;

        }

        #endregion

        #region Public
        public PurchaseResponseModel GetPurchases(PurchaseRequestModel requestModel)
        {
            return purchaseRepository.GetPurchases(requestModel);
        }

        /// <summary>
        /// Get Purchase Orders
        /// </summary>
        public PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel request)
        {
            Organisation org = organisationRepository.GetOrganizatiobByID();
            var response = purchaseRepository.GetPurchaseOrders(request);
            response.HeadNote = org != null ? org.PurchaseHeadNote : "";
            response.FootNote = org != null ? org.PurchaseFootNote : "";
            return response;
        }

        /// <summary>
        /// Ge Goods Received Notes
        /// </summary>
        public GoodsReceivedNotesResponseModel GetGoodsReceivedNotes(PurchaseOrderSearchRequestModel request)
        {
            return goodRecieveNoteRepository.GetGoodsReceivedNotes(request);
        }

        /// <summary>
        /// base Data for Purchase
        /// </summary>
        public PurchaseBaseResponse GetBaseData()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new PurchaseBaseResponse
            {
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Purchase),
                SystemUsers = systemUserRepository.GetAll(),
                DeliveryCarriers = deliveryCarrierRepository.GetAll(),
                LoggedInUser = organisationRepository.LoggedInUserId,
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
            // Update Purchase
            purchase.UpdateTo(purchaseTarget, new PurchaseMapperActions
            {
                CreatePurchaseDetail = CreatePurchaseDetail,
                DeletePurchaseDetail = DeletePurchaseDetail,
            });

            purchaseTarget.OrganisationId = purchaseRepository.OrganisationId;
            // Save Changes
            purchaseRepository.SaveChanges();

            // Load Company
            purchaseRepository.LoadProperty(purchaseTarget, () => purchaseTarget.Company);
            purchaseRepository.LoadProperty(purchaseTarget, () => purchaseTarget.SectionFlag);
            return purchaseTarget;
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


        public bool GeneratePO(long OrderID, long ContactID, long CompanyId, Guid CreatedBy)
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
                    string FileName = exportReportHelper.ExportPDF(100, purchase.Key, ReportType.PurchaseOrders, OrderID, string.Empty);




                    int ItemIDs = orderRepository.GetFirstItemIDByOrderId(OrderID);
                    Organisation CompOrganisation = organisationRepository.GetOrganizatiobByID();
                    Company objCompany = companyRepository.GetCompanyByCompanyID(CompanyId);
                    SystemUser saleManager = systemUserRepository.GetUserrById(objCompany.SalesAndOrderManagerId1 ?? Guid.NewGuid());


                    string SalesManagerFile = ImagePathConstants.ReportPath + CompOrganisation.OrganisationId + "/" + purchase.Key + "_PurchaseOrder.pdf";
                    campaignRepository.POEmailToSalesManager(OrderID, CompanyId, ContactID, 250, purchase.Value, SalesManagerFile, objCompany);



                    if (objCompany.IsCustomer == (int)CustomerTypes.Corporate)
                    {
                        campaignRepository.SendEmailToSalesManager((int)Events.PO_Notification_To_SalesManager, ContactID, CompanyId, OrderID, CompOrganisation, CompOrganisation.OrganisationId, 0, StoreMode.Corp, CompanyId, saleManager, ItemIDs, "", "", 0);
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

                    campaignRepository.POEmailToSupplier(OrderID, CompanyId, ContactID, 250, purchase.Value, DestinationFileSupplier, objCompany,false);


                    // SendEmailToSupplier(ServerPath, OrderID, ContactCompanyID, ContactID, 250, purchase.SupplierID ?? 0, DestinationFileSupplier);




                    // AttachmentsList.Add(FilePath);

                }

            }


        }



        public void DeletePO(long OrderID)
        {

            purchaseRepository.DeletePO(OrderID);

        }

        public string ExportPurchaseOrderToCsv(long purchaseId)
        {
            List<string> fileHeader = new List<string>();
            long organisationId = purchaseRepository.OrganisationId;


            fileHeader = HeaderList(fileHeader);
            List<usp_ExportPurchaseOrder_Result> exportPoData = purchaseRepository.GetPurchaseOrderForExport(purchaseId);
            StringBuilder sbCSV = new StringBuilder();
            string csv = string.Empty;
            
            fileHeader.ToList().ForEach(column => csv += column + ',');
            
            //Add new line.
            csv += "\r\n";
            sbCSV.Append(csv);

            fileHeader.Add("Purchase Code");
            fileHeader.Add("Purchase Date");
            fileHeader.Add("Refrence No");
            fileHeader.Add("Product Code");
            fileHeader.Add("Product Name");
            fileHeader.Add("Pack Quantity");
            fileHeader.Add("Required Quantity");
            fileHeader.Add("UnitPrice");
            fileHeader.Add("Tax Value");
            fileHeader.Add("Total Price");
            fileHeader.Add("Comments");
            fileHeader.Add("Supplier Name");
            fileHeader.Add("Supplier Contact Name");
            fileHeader.Add("Supplier Address");

            string poCode = string.Empty;
            string poDate = string.Empty;
            string refNo = string.Empty;
            string productCode = string.Empty;
            string productName = string.Empty;
            string packQty = string.Empty;
            string requiredQty = string.Empty;
            string unitPrice = string.Empty;
            string taxValue = string.Empty;
            string totalPrice = string.Empty;
            string comments = string.Empty;
            string supplierName = string.Empty;
            string supplierContact = string.Empty;
            string supplierAddress = string.Empty;


            if (exportPoData != null && exportPoData.Count() > 0)
            {
                foreach (var poRec in exportPoData)
                {
                    string cdata = string.Empty;
                    poCode = poRec.Code;
                    if (poRec.PurchaseDate != null)
                    {
                        poDate = Convert.ToString(poRec.PurchaseDate);
                    }
                    refNo = poRec.RefNo;
                    productCode = poRec.ItemCode;
                    productName = poRec.ItemName;
                    packQty = Convert.ToString(poRec.PackQty ?? 0);
                    requiredQty = Convert.ToString(poRec.Quantity ?? 0);
                    unitPrice = Convert.ToString(poRec.Price ?? 0);
                    taxValue = Convert.ToString(poRec.TaxValue ?? 0);
                    totalPrice = Convert.ToString(poRec.TotalPrice ?? 0);
                    comments = poRec.Comments;
                    supplierName = poRec.Name;
                    supplierContact = poRec.ContactName;
                    supplierAddress = poRec.Address;

                    cdata = poCode + "," + poDate + "," + refNo + "," + productCode + "," + productName + "," + packQty + "," + requiredQty + "," + unitPrice + "," + taxValue + "," +
                        totalPrice + "," + comments + "," + supplierName + "," + supplierContact + "," + supplierAddress +
                                      "\r\n";
                    sbCSV.Append(cdata);
                }
            }
            
            string CSVSavePath = string.Empty;
            string CReturnPath = string.Empty;

            if (organisationId > 0)
            {
                CSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + organisationId + "/" + organisationId + "_POExport.csv");
                CReturnPath = "/MPC_Content/Reports/" + organisationId + "/" + organisationId + "_POExport.csv";
            }
            else
            {
                CSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + organisationId + "_POExport.csv");
                CReturnPath = "/MPC_Content/Reports/" + organisationId + "_POExport.csv";

            }

            string DirectoryPath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + organisationId);
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            StreamWriter csw = new StreamWriter(CSVSavePath, false, Encoding.UTF8);
            csw.Write(sbCSV);
            csw.Close();
            
            return CReturnPath;

        }
        public List<string> HeaderList(List<string> fileHeader)
        {

            
            fileHeader.Add("Purchase Code");
            fileHeader.Add("Purchase Date");
            fileHeader.Add("Refrence No");
            fileHeader.Add("Product Code");
            fileHeader.Add("Product Name");
            fileHeader.Add("Pack Quantity");
            fileHeader.Add("Required Quantity");
            fileHeader.Add("UnitPrice");
            fileHeader.Add("Tax Value");
            fileHeader.Add("Total Price");
            fileHeader.Add("Comments");
            fileHeader.Add("Supplier Name");
            fileHeader.Add("Supplier Contact Name");
            fileHeader.Add("Supplier Address");

            return fileHeader;

        }
        #endregion
    }
}
