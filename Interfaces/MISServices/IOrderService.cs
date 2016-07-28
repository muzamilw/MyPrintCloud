using System.Collections.Generic;
using System.IO;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Order Service
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Get All Orders
        /// </summary>
        GetOrdersResponse GetAll(GetOrdersRequest request);

        /// <summary>
        /// Get By Id
        /// </summary>
        Estimate GetById(long orderId);

        /// <summary>
        /// Delete Order
        /// </summary>
        void DeleteOrder(long orderId);

        /// <summary>
        /// Save Order
        /// </summary>
        Estimate SaveOrder(Estimate estimate);

        /// <summary>
        /// Get base data for order
        /// </summary>
        OrderBaseResponse GetBaseData();
        /// <summary>
        /// Base Data For item Details
        /// </summary>
        /// <returns></returns>
        ItemDetailBaseResponse GetBaseDataForItemDetails();

        /// <summary>
        /// Get base data for Specified Company
        /// </summary>
        OrderBaseResponseForCompany GetBaseDataForCompany(long companyId, long storeId, long orderId = 0);
        /// <summary>
        /// Get Order Statuses Count For Menu Items
        /// </summary>
        /// <returns></returns>
        OrderMenuCount GetOrderScreenMenuItemCount();

        string DownloadOrderArtwork(int OrderID, string sZipName, long WebStoreOrganisationId = 0);
        GetOrdersResponse GetOrdersForEstimates(GetOrdersRequest request);

        //string ExportPDF(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam);

        //string ExportOrderReportXML(long iRecordID, string OrderCode, string XMLFormat);

        //string ExportExcel(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam);
        bool ProgressEstimateToOrder(ProgressEstimateRequestModel requestModel);
        Estimate CloneOrder(Estimate source);
        InquiryBaseResponse GetBaseDataForInquiries();

        OrderBaseResponse GetBaseDataForEstimate();

        /// <summary>
        /// Download Attachment
        /// </summary>
        string DownloadAttachment(long id, out string fileName, out string fileType);

        bool MakeOrderArtworkProductionReady(Estimate oOrder, long WebStoreOrganisationId = 0);
        /// <summary>
        /// Clone Estimate
        /// </summary>
        /// <param name="estimateId"></param>
        /// <returns></returns>
        Estimate CloneEstimate(long estimateId);

        /// <summary>
        /// Clone Order
        /// </summary>
        Estimate CloneOrder(long estimateId);

        List<Item> GetOrderItems(long EstimateId);
        string DownloadOrderXml(int orderId, long organisationId);

        string DownloadInquiryAttachment(long id, out string fileName, out string fileTpe);

        void DeleteOrderPermanently(long orderId, string Comment);
        string GenerateDigitalItemsArtwork(long estimateId, long organisationId);

        void ExportPoPdfForWebStore(long orderId, long companyId, long contactId, long organisationId);

    }
}
