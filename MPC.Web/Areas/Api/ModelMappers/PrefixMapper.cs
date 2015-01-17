using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class PrefixMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Prefix CreateFrom(this MPC.Models.DomainModels.Prefix source)
        {
            return new Prefix
            {
                DeliveryNPrefix = source.DeliveryNPrefix,
                DeliveryNStart = source.DeliveryNStart,
                DeliveryNNext = source.DeliveryNNext,
                EnquiryPrefix = source.EnquiryPrefix,
                EnquiryStart = source.EnquiryStart,
                EnquiryNext = source.EnquiryNext,
                EstimatePrefix = source.EstimatePrefix,
                EstimateStart = source.EstimateStart,
                EstimateNext = source.EstimateNext,
                InvoicePrefix = source.InvoicePrefix,
                InvoiceNext = source.InvoiceNext,
                InvoiceStart = source.InvoiceStart,
                JobPrefix =  source.JobPrefix,
                JobStart = source.JobStart,
                JobNext = source.JobNext,
                PoNext = source.PoNext,
                PoStart = source.PoStart,
                PoPrefix = source.PoPrefix,
                GofNext = source.GofNext,
                GofPrefix = source.GofPrefix,
                GofStart = source.GofStart,
                ItemNext = source.ItemNext,
                ItemStart = source.ItemStart,
                ItemPrefix = source.ItemPrefix,
                JobCardNext = source.JobCardNext,
                JobCardPrefix = source.JobCardPrefix,
                JobCardStart = source.JobCardStart,
                GrnNext = source.GrnNext,
                GrnPrefix = source.GrnPrefix,
                GrnStart =  source.GrnStart,
                ProductNext = source.ProductNext,
                ProductPrefix = source.ProductPrefix,
                ProductStart = source.ProductStart,
                FinishedGoodsNext = source.FinishedGoodsNext,
                FinishedGoodsPrefix = source.FinishedGoodsPrefix,
                FinishedGoodsStart = source.FinishedGoodsStart,
                OrderNext = source.OrderNext,
                OrderPrefix = source.OrderPrefix,
                OrderStart = source.OrderStart,
                StockItemNext = source.StockItemNext,
                StockItemPrefix = source.StockItemPrefix,
                StockItemStart = source.StockItemStart,
                OrganisationId =  source.OrganisationId,
                PrefixId = source.PrefixId,
                SystemSiteId = source.SystemSiteId
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.Prefix CreateFrom(this Prefix source)
        {
            return new MPC.Models.DomainModels.Prefix
            {
                DeliveryNPrefix = source.DeliveryNPrefix,
                DeliveryNStart = source.DeliveryNStart,
                DeliveryNNext = source.DeliveryNNext,
                EnquiryPrefix = source.EnquiryPrefix,
                EnquiryStart = source.EnquiryStart,
                EnquiryNext = source.EnquiryNext,
                EstimatePrefix = source.EstimatePrefix,
                EstimateStart = source.EstimateStart,
                EstimateNext = source.EstimateNext,
                InvoicePrefix = source.InvoicePrefix,
                InvoiceNext = source.InvoiceNext,
                InvoiceStart = source.InvoiceStart,
                JobPrefix = source.JobPrefix,
                JobStart = source.JobStart,
                JobNext = source.JobNext,
                PoNext = source.PoNext,
                PoStart = source.PoStart,
                PoPrefix = source.PoPrefix,
                GofNext = source.GofNext,
                GofPrefix = source.GofPrefix,
                GofStart = source.GofStart,
                ItemNext = source.ItemNext,
                ItemStart = source.ItemStart,
                ItemPrefix = source.ItemPrefix,
                JobCardNext = source.JobCardNext,
                JobCardPrefix = source.JobCardPrefix,
                JobCardStart = source.JobCardStart,
                GrnNext = source.GrnNext,
                GrnPrefix = source.GrnPrefix,
                GrnStart = source.GrnStart,
                ProductNext = source.ProductNext,
                ProductPrefix = source.ProductPrefix,
                ProductStart = source.ProductStart,
                FinishedGoodsNext = source.FinishedGoodsNext,
                FinishedGoodsPrefix = source.FinishedGoodsPrefix,
                FinishedGoodsStart = source.FinishedGoodsStart,
                OrderNext = source.OrderNext,
                OrderPrefix = source.OrderPrefix,
                OrderStart = source.OrderStart,
                StockItemNext = source.StockItemNext,
                StockItemPrefix = source.StockItemPrefix,
                StockItemStart = source.StockItemStart,
                OrganisationId = source.OrganisationId,
                PrefixId = source.PrefixId,
                SystemSiteId = source.SystemSiteId
            };
        }
    }
}