using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ImportProductMapper
    {
        /// <summary>
        /// Company Contact Mapper Web Model -> Domain Model
        /// </summary>
        public static StagingProductPriceImport Createfrom(this ImportProducts source, long companyId)
        {
            return new StagingProductPriceImport
            {

                ProductCode = source.ProductCode,
                ProductName = source.Product_Name,
                CategoryName = source.Category,
                // finishh
                isQuantityRanged = source.isQtyRanged == "true" ? 1 : 0,
                Qty = !string.IsNullOrEmpty(source.quantity) ? Convert.ToInt32(source.quantity) : 0,
                QtyFrom = !string.IsNullOrEmpty(source.QtyRangeFrom) ? Convert.ToInt32(source.QtyRangeFrom) : 0,
                QtyTo = !string.IsNullOrEmpty(source.QtyRangeTo) ? Convert.ToInt32(source.QtyRangeTo) : 0,
                StockOptionName1 = source.StockLabel1,
                Price1 = !string.IsNullOrEmpty(source.Price1) ? Convert.ToDouble(source.Price1) : 0,
                StockOptionName2 = source.StockLabel2,
                Price2 = !string.IsNullOrEmpty(source.Price2) ? Convert.ToDouble(source.Price2) : 0,
                StockOptionName3 = source.StockLabel3,
                Price3 = !string.IsNullOrEmpty(source.Price3) ? Convert.ToDouble(source.Price2) : 0,
                StockOptionName4 = source.StockLabel4,
                Price4 = source.Price4,
                StockOptionName5 = source.StockLabel5,
                Price5 = source.Price5,
                StockOptionName6 = source.StockLabel6,
                Price6 = source.Price6,
                StockOptionName7 = source.StockLabel7,
                Price7 = source.Price7,
                StockOptionName8 = source.Price8,
               Price8 = source.Price8,
               StockOptionName9 = source.StockLabel9,
               Price9 = source.Price9,
               SupplierPrice1 = !string.IsNullOrEmpty(source.SupplierPrice1) ? Convert.ToDouble(source.SupplierPrice1) : 0,
                SupplierPrice2 = !string.IsNullOrEmpty(source.SupplierPrice2) ? Convert.ToDouble(source.SupplierPrice2) : 0,
                SupplierPrice3 = !string.IsNullOrEmpty(source.SupplierPrice3) ? Convert.ToDouble(source.SupplierPrice3) : 0,
               SupplierPrice4 = source.SupplierPrice4,
               SupplierPrice5 = source.SupplierPrice5,
               SupplierPrice6 = source.SupplierPrice6,
               SupplierPrice7 = source.SupplierPrice7,
               SupplierPrice8 = source.SupplierPrice8,
               SupplierPrice9 = source.SupplierPrice9,
               jobDescriptionTitle1 = source.JobDescription1,
               jobDescription1 = source.JobDescription1,
               jobDescriptionTitle2 = source.JobDescription2,
               jobDescription2 = source.JobDescription2,
               jobDescriptionTitle3 = source.JobDescriptionTitle3,
               jobDescription3 = source.JobDescription3,
               jobDescriptionTitle4 = source.JobDescriptionTitle4,
               jobDescription4 = source.JobDescription4,
               jobDescriptionTitle5 = source.JobDescriptionTitle5,
               jobDescription5 = source.JobDescription5,
               jobDescriptionTitle6 = source.JobDescriptionTitle6,
               jobDescription6 = source.JobDescription6,
               jobDescriptionTitle7 = source.JobDescriptionTitle7,
               jobDescription7 = source.JobDescription7


            };
        }
    }
}