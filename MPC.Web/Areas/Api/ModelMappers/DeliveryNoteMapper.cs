using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class DeliveryNoteMapper
    {
        /// <summary>
        /// Delivery Notes mapper 
        /// </summary>
        public static DeliverNotesResposne CreateFrom(this MPC.Models.ResponseModels.GetDeliveryNoteResponse source)
        {
            return new DeliverNotesResposne
            {
                TotalCount = source.TotalCount,
                DeliveryNotes =
                    source.DeliveryNotes != null ? source.DeliveryNotes.Select(dNote => dNote.CreateFromModel()) : null
            };
        }


        public static DeliveryNoteBaseResponse CreateFrom(this MPC.Models.ResponseModels.DeliveryNoteBaseResponse source)
        {
            return new DeliveryNoteBaseResponse
            {
                SectionFlags = source.SectionFlags!=null ? source.SectionFlags.Select(flag => flag.CreateFromDropDown()):  new List<SectionFlagDropDown>(),
               SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) :
                new List<SystemUserDropDown>(),
            };
        }

        public static Models.DeliveryNotes CreateFromModel(this DeliveryNote source)
        {
            return new Models.DeliveryNotes
            {
                DeliveryNoteId = source.DeliveryNoteId,
                Code = source.Code,
                CsNo = source.CsNo,
                DeliveryDate = source.DeliveryDate,
                FlagId = source.FlagId,
                ContactCompany = source.ContactCompany,
                OrderReff = source.OrderReff,
                CreationDateTime = source.CreationDateTime
            };
        }

        public static Models.DeliveryNotes CreateDetailFromModel(this DeliveryNote source)
        {
            return new Models.DeliveryNotes
            {
                DeliveryNoteId = source.DeliveryNoteId,
                Code = source.Code,
                CsNo = source.CsNo,
                DeliveryDate = source.DeliveryDate,
                FlagId = source.FlagId,
                ContactCompany = source.ContactCompany,
                OrderReff = source.OrderReff,
                CreationDateTime = source.CreationDateTime,
                CompanyId = source.CompanyId,
                footnote = source.footnote,
                Comments = source.Comments,
                LockedBy = source.LockedBy,
                IsStatus = source.IsStatus,
                ContactId = source.ContactId,
                CustomerOrderReff = source.CustomerOrderReff,
                AddressId = source.AddressId,
                CreatedBy = source.CreatedBy,
                SupplierId = source.SupplierId,
                SupplierTelNo = source.SupplierTelNo,
                SupplierURL = source.SupplierURL,
                EstimateId = source.EstimateId,
                JobId = source.JobId,
                InvoiceId = source.InvoiceId,
                OrderId = source.OrderId,
                UserNotes = source.UserNotes,
                NotesUpdateDateTime = source.NotesUpdateDateTime,
                NotesUpdatedByUserId = source.NotesUpdatedByUserId,
                SystemSiteId = source.SystemSiteId,
                IsRead = source.IsRead,
                IsPrinted = source.IsPrinted
            };
        }
    }
}