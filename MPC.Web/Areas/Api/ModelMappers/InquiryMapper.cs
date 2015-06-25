using System;
using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.ModelMappers;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class InquiryMapper
    {
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.Inquiry CreateFrom(this Inquiry source)
        {
            return new DomainModels.Inquiry
            {
               InquiryId = source.InquiryId,
               Title = source.Title,
               ContactId = source.ContactId,
               CreatedDate = source.CreatedDate ?? DateTime.Now,
               SourceId = source.SourceId,
               CompanyId = source.CompanyId,
               RequireByDate = source.RequireByDate,
               SystemUserId = source.SystemUserId,
               Status = source.Status,
               IsDirectInquiry = source.IsDirectInquiry,
               FlagId = source.FlagId,
               InquiryCode = source.InquiryCode,
               CreatedBy = source.CreatedBy,
               OrganisationId = source.OrganisationId,
               InquiryAttachments = source.InquiryAttachments != null ? source.InquiryAttachments.Select(x=> x.CreateFrom()).ToList(): null,
               InquiryItems = source.InquiryItems != null ? source.InquiryItems.Select(x=> x.CreateFrom()).ToList(): null
            };
        }
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Inquiry CreateFrom(this DomainModels.Inquiry source)
        {
            return new Inquiry
            {
                InquiryId = source.InquiryId,
                Title = source.Title,
                ContactId = source.ContactId,
                CreatedDate = source.CreatedDate,
                SourceId = source.SourceId,
                CompanyId = source.CompanyId,
                CompanyName = source.Company != null? source.Company.Name : string.Empty,
                RequireByDate = source.RequireByDate,
                SystemUserId = source.SystemUserId,
                Status = source.Status,
                IsDirectInquiry = source.IsDirectInquiry,
                FlagId = source.FlagId,
                InquiryCode = source.InquiryCode,
                CreatedBy = source.CreatedBy,
                OrganisationId = source.OrganisationId,
                EstimateId = source.EstimateId,
                InquiryItemsCount =source.InquiryItems != null ? source.InquiryItems.Count: 0,
                InquiryAttachments = source.InquiryAttachments != null ? source.InquiryAttachments.Select(x => x.CreateFrom()).ToList() : null,
                InquiryItems = source.InquiryItems != null ? source.InquiryItems.Select(x => x.CreateFrom()).ToList() : null
            };
        }

        public static InquiryListView CreateFromForListView(this DomainModels.Inquiry source)
        {
            return new InquiryListView
            {
                CompanyId = source.CompanyId,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty,
                ContactId = source.ContactId,
                ContactName = source.CompanyContact != null? source.CompanyContact.Email: string.Empty,
                InquiryId = source.InquiryId,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Title = source.Title,
                FlagId = source.FlagId,
                InquiryCode = source.InquiryCode,
                IsDirectInquiry = source.IsDirectInquiry,
                OrganisationId = source.OrganisationId,
                RequireByDate = source.RequireByDate,
                SourceId = source.SourceId,
                Status = source.Status,
                SystemUserId = source.SystemUserId,
                InquiryItemsCount = source.InquiryItems.Count
                
            };
        }

       
    }
}