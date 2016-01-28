using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModel = MPC.Models.DomainModels;


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
                    source.DeliveryNotes != null ? source.DeliveryNotes.Select(dNote => dNote.CreateFromListView()) : null
            };
        }

        /// <summary>
        /// Create Form Base response
        /// </summary>
        public static DeliveryNoteBaseResponse CreateFrom(this MPC.Models.ResponseModels.DeliveryNoteBaseResponse source)
        {
            return new DeliveryNoteBaseResponse
            {
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(flag => flag.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                DeliveryCarriers = source.DeliveryCarriers != null ? source.DeliveryCarriers.Select(flag => flag.CreateFromDropDown()) : new List<DeliveryCarrier>(),
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) :
                 new List<SystemUserDropDown>(),
                 LoggedInUser = source.LoggedInUser
            };
        }

        /// <summary>
        /// Create Form Domain Model
        /// </summary>
        public static DeliveryNote CreateFromListView(this DomainModel.DeliveryNote source)
        {
            return new DeliveryNote
            {
                DeliveryNoteId = source.DeliveryNoteId,
                Code = source.Code,
                CsNo = source.CsNo,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty,
                DeliveryDate = source.DeliveryDate,
                IsStatus = source.IsStatus,
                FlagId = source.FlagId,
                ContactCompany = source.ContactCompany,
                FlagColor = source.SectionFlag != null ? source.SectionFlag.FlagColor : string.Empty,
                OrderReff = source.OrderReff,
                CreationDateTime = source.CreationDateTime,
                OrganisationId = source.OrganisationId
            };
        }

        /// <summary>
        /// Create Form Domain Model
        /// </summary>
        public static DeliveryNote CreateFrom(this DomainModel.DeliveryNote source)
        {
            return new DeliveryNote
            {
                DeliveryNoteId = source.DeliveryNoteId,
                Code = source.Code,
                CsNo = source.CsNo,
                DeliveryDate = source.DeliveryDate,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty,
                StoreId = source.Company != null ? source.Company.StoreId : null,
                IsCustomer = source.Company != null ? source.Company.IsCustomer : (short)0,
                FlagId = source.FlagId,
                ContactCompany = source.ContactCompany,
                OrderReff = source.OrderReff,
                CreationDateTime = source.CreationDateTime,
                CompanyId = source.CompanyId,
                Comments = source.Comments,
                IsStatus = source.IsStatus,
                ContactId = source.ContactId,
                RaisedBy = source.RaisedBy,
                AddressId = source.AddressId,
                SupplierId = source.SupplierId,
                SupplierTelNo = source.SupplierTelNo,
                UserNotes = source.UserNotes,
                DeliveryNoteDetails = source.DeliveryNoteDetails != null ? source.DeliveryNoteDetails.Select(dNotesDetail => dNotesDetail.CreateFrom()).ToList() : null,
            };
        }

        /// <summary>
        /// Create Form Api Model
        /// </summary>
        public static DomainModel.DeliveryNote CreateFrom(this DeliveryNote source)
        {
            return new DomainModel.DeliveryNote
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
                Comments = source.Comments,
                IsStatus = source.IsStatus,
                RaisedBy = source.RaisedBy,
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                SupplierId = source.SupplierId,
                SupplierTelNo = source.SupplierTelNo,
                UserNotes = source.UserNotes,
                OrganisationId = source.OrganisationId,
                DeliveryNoteDetails = source.DeliveryNoteDetails != null ? source.DeliveryNoteDetails.Select(dNotesDetail => dNotesDetail.CreateFrom()).ToList() : null,
            };
        }
    }
}