using System.Collections.Generic;
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
        /// Get base data for Specified Company
        /// </summary>
        OrderBaseResponseForCompany GetBaseDataForCompany(long companyId, long storeId);
        /// <summary>
        /// Get Order Statuses Count For Menu Items
        /// </summary>
        /// <returns></returns>
        OrderMenuCount GetOrderScreenMenuItemCount();

        PtvDTO GetPTV(PTVRequestModel request);
        PtvDTO GetPTVCalculation(PTVRequestModel request);
        BestPressResponse GetBestPresses(ItemSection currentSection);

        ItemSection GetUpdatedSectionCostCenters(UpdateSectionCostCentersRequest request);

        string DownloadOrderArtwork(int OrderID, string sZipName);
        GetOrdersResponse GetOrdersForEstimates(GetOrdersRequest request);

        string ExportPDF(int iReportID, long iRecordID, ReportType type, long OrderID);

        string ExportOrderReportXML(long iRecordID, string OrderCode, string XMLFormat);

        string ExportExcel(int iReportID, long iRecordID, ReportType type, long OrderID);
    }
}
