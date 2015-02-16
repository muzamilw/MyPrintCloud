using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

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
                EstimateCode = source.Estimate_Code,
                EstimateName = source.Estimate_Name,
                EnquiryId = source.EnquiryId,
                SectionFlagId = source.SectionFlagId,
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                IsDirectSale = source.isDirectSale,
                IsCreditApproved = source.IsCreditApproved,
                IsOfficialOrder = source.IsOfficialOrder,
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
                IsJobAllowedWOCreditCheck = source.IsJobAllowedWOCreditCheck,
                AllowJobWOCreditCheckSetOnDateTime = source.AllowJobWOCreditCheckSetOnDateTime,
                AllowJobWOCreditCheckSetBy = source.AllowJobWOCreditCheckSetBy,
                CustomerPo = source.CustomerPO,
                OfficialOrderSetBy = source.OfficialOrderSetBy,
                OfficialOrderSetOnDateTime = source.OfficialOrderSetOnDateTime
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
                ItemsCount = source.Items!=null ? source.Items.Count :0
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
                Estimate_Name = source.EstimateName,
                EnquiryId = source.EnquiryId,
                SectionFlagId = source.SectionFlagId,
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                isDirectSale = source.IsDirectSale,
                IsCreditApproved = source.IsCreditApproved,
                IsOfficialOrder = source.IsOfficialOrder,
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
                IsJobAllowedWOCreditCheck = source.IsJobAllowedWOCreditCheck,
                AllowJobWOCreditCheckSetOnDateTime = source.AllowJobWOCreditCheckSetOnDateTime,
                AllowJobWOCreditCheckSetBy = source.AllowJobWOCreditCheckSetBy,
                CustomerPO = source.CustomerPo,
                OfficialOrderSetBy = source.OfficialOrderSetBy,
                OfficialOrderSetOnDateTime = source.OfficialOrderSetOnDateTime
            };
        }

    }
}