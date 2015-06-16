using System.Linq;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;
    using System.Collections.Generic;

    /// <summary>
    /// Estimate Mapper
    /// </summary>
    public static class EstimateMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Estimate CreateFrom(this DomainModels.Estimate source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            Estimate estimate = new Estimate
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                EstimateId = source.EstimateId,
                CompanyId = source.CompanyId,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty,
                StoreId = source.Company != null ? source.Company.StoreId : null,
                IsCustomer = source.Company != null ? source.Company.IsCustomer : (short)0,
                StatusId = source.StatusId,
                Status = source.Status != null ? source.Status.StatusName : string.Empty,
                EstimateCode = source.Estimate_Code,
                EstimateName = source.Estimate_Name,
                EnquiryId = source.EnquiryId,
                SectionFlagId = source.SectionFlagId,
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                IsDirectSale = source.isDirectSale,
                IsCreditApproved = source.IsCreditApproved == 1,
                IsOfficialOrder = source.IsOfficialOrder == 1,
                OrderDate = source.Order_Date,
                StartDeliveryDate = source.StartDeliveryDate,
                FinishDeliveryDate = source.FinishDeliveryDate,
                HeadNotes = source.HeadNotes,
                FootNotes = source.FootNotes,
                ArtworkByDate = source.ArtworkByDate,
                DataByDate = source.DataByDate,
                PaperByDate = source.PaperByDate,
                TargetBindDate = source.TargetBindDate,
                XeroAccessCode = source.XeroAccessCode,
                TargetPrintDate = source.TargetPrintDate,
                OrderCreationDateTime = source.Order_CreationDateTime,
                OrderManagerId = source.OrderManagerId,
                SalesPersonId = source.SalesPersonId,
                SourceId = source.SourceId,
                CreditLimitForJob = source.CreditLimitForJob,
                CreditLimitSetBy = source.CreditLimitSetBy,
                CreditLimitSetOnDateTime = source.CreditLimitSetOnDateTime,
                IsJobAllowedWOCreditCheck = source.IsJobAllowedWOCreditCheck == 1,
                AllowJobWOCreditCheckSetOnDateTime = source.AllowJobWOCreditCheckSetOnDateTime,
                AllowJobWOCreditCheckSetBy = source.AllowJobWOCreditCheckSetBy,
                CustomerPo = source.CustomerPO,
                OfficialOrderSetBy = source.OfficialOrderSetBy,
                OfficialOrderSetOnDateTime = source.OfficialOrderSetOnDateTime,
                OrderCode = source.Order_Code,
                OrderReportSignedBy = source.OrderReportSignedBy,
                IsEstimate = source.isEstimate,
                EstimateTotal = source.Estimate_Total,
                CreationDate = source.CreationDate,
                CreationTime = source.CreationTime,
                RefEstimateId = source.RefEstimateId,
                Items = source.Items != null ? source.Items.Select(sc => sc.CreateFromForOrder()).OrderBy(item => item.ProductName).ToList() :
                new List<OrderItem>(),
                ItemsCount = source.Items != null ? source.Items.Count : 0,
                PrePayments = source.PrePayments != null ? source.PrePayments.Select(sc => sc.CreateFrom()).OrderBy(payment => payment.ReferenceCode).ToList() :
                new List<PrePayment>(),
                ShippingInformations = source.ShippingInformations != null ? source.ShippingInformations.Select(sc => sc.CreateFrom()).OrderBy(sc => sc.ItemName).ToList() :
                new List<ShippingInformation>()
            };

            return estimate;
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static EstimateListView CreateFromForListView(this DomainModels.Estimate source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            EstimateListView estimate = new EstimateListView
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                EstimateId = source.EstimateId,
                EstimateCode = source.Estimate_Code,
                StatusId = source.StatusId,
                EstimateName = source.Estimate_Name,
                EnquiryId = source.EnquiryId,
                CompanyId = source.CompanyId,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty,
                CreatedBy = source.Created_by,
                CreationDate = source.CreationDate,
                CreationTime = source.CreationTime,
                SectionFlagId = source.SectionFlagId,
                OrderCode = source.Order_Code,
                IsEstimate = source.isEstimate,
                ItemsCount = source.Items != null ? source.Items.Count : 0,
                Status = source.Status.StatusName,
                EstimateTotal = source.Estimate_Total,
                IsDirectSale = source.isDirectSale,
                SectionFlagColor = source.SectionFlag != null ? source.SectionFlag.FlagColor : null,
                OrderDate = source.Order_Date
            };

            return estimate;
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.Estimate CreateFrom(this Estimate source)
        {
            return new DomainModels.Estimate
            {
                EstimateId = source.EstimateId,
                Estimate_Code = source.EstimateCode,
                StatusId = source.StatusId,
                Estimate_Name = source.EstimateName,
                EnquiryId = source.EnquiryId,
                SectionFlagId = source.SectionFlagId,
                CompanyId = source.CompanyId,
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                isDirectSale = source.IsDirectSale,
                IsCreditApproved = source.IsCreditApproved == true ? 1 : 0,
                IsOfficialOrder = source.IsOfficialOrder == true ? 1 : 0,
                Order_Date = source.OrderDate,
                StartDeliveryDate = source.StartDeliveryDate,
                FinishDeliveryDate = source.FinishDeliveryDate,
                HeadNotes = source.HeadNotes,
                FootNotes = source.FootNotes,
                ArtworkByDate = source.ArtworkByDate,
                DataByDate = source.DataByDate,
                PaperByDate = source.PaperByDate,
                TargetBindDate = source.TargetBindDate,
                XeroAccessCode = source.XeroAccessCode,
                TargetPrintDate = source.TargetPrintDate,
                Order_CreationDateTime = source.OrderCreationDateTime,
                OrderManagerId = source.OrderManagerId,
                SalesPersonId = source.SalesPersonId,
                SourceId = source.SourceId,
                CreditLimitForJob = source.CreditLimitForJob,
                CreditLimitSetBy = source.CreditLimitSetBy,
                CreditLimitSetOnDateTime = source.CreditLimitSetOnDateTime,
                IsJobAllowedWOCreditCheck = source.IsJobAllowedWOCreditCheck == true ? 1 : 0,
                AllowJobWOCreditCheckSetOnDateTime = source.AllowJobWOCreditCheckSetOnDateTime,
                AllowJobWOCreditCheckSetBy = source.AllowJobWOCreditCheckSetBy,
                CustomerPO = source.CustomerPo,
                OfficialOrderSetBy = source.OfficialOrderSetBy,
                OrderReportSignedBy = source.OrderReportSignedBy,
                OfficialOrderSetOnDateTime = source.OfficialOrderSetOnDateTime,
                isEstimate = source.IsEstimate,
                Estimate_Total = source.EstimateTotal,
                PrePayments = source.PrePayments != null ? source.PrePayments.Select(sc => sc.CreateFrom()).ToList() : new List<DomainModels.PrePayment>(),
                Items = source.Items != null ? source.Items.Select(sc => sc.CreateFromForOrder()).ToList() :
                new List<DomainModels.Item>(),
                ShippingInformations = source.ShippingInformations != null ? source.ShippingInformations.Select(sc => sc.CreateFrom()).ToList() :
                new List<DomainModels.ShippingInformation>(),
            };
        }

        /// <summary>
        /// Orders for Company edit
        /// </summary>
        public static OrdersForCrmResponse CreateFrom(this MPC.Models.ResponseModels.OrdersForCrmResponse source)
        {
            return new OrdersForCrmResponse
            {
                RowCount = source.RowCount,
                OrdersList = source.Orders.Select(order => order.CreateFromForListView())
            };
        }

    }
}