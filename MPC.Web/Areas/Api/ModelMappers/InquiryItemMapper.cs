using System;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class InquiryItemMapper
    {
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.InquiryItem CreateFrom(this InquiryItem source)
        {
            return new DomainModels.InquiryItem
            {
                InquiryItemId = source.InquiryItemId,
                DeliveryDate = source.DeliveryDate,
                InquiryId = source.InquiryId,
                Notes = source.Notes,
                ProductId = source.ProductId,
                Title = source.Title
            };
        }
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static InquiryItem CreateFrom(this DomainModels.InquiryItem source)
        {
            return new InquiryItem
            {
                InquiryItemId = source.InquiryItemId,
                DeliveryDate = source.DeliveryDate,
                InquiryId = source.InquiryId,
                Notes = source.Notes,
                ProductId = source.ProductId,
                Title = source.Title,
                MarketingSource = source.PipeLineProduct!= null ? source.PipeLineProduct.Description : string.Empty
            };
        }
    }
}