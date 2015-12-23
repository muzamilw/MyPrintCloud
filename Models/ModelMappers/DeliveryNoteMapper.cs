using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    /// Delivery Note Domain Mapper
    /// </summary>
    public static class DeliveryNoteMapper
    {
        #region Private
        /// <summary>
        /// Update Delivery Note
        /// </summary>
        private static void UpdateDeliveryNote(DeliveryNote source, DeliveryNote target)
        {
            target.DeliveryNoteId = source.DeliveryNoteId;
            target.CsNo = source.CsNo;
            target.DeliveryDate = source.DeliveryDate;
            target.FlagId = source.FlagId;
            target.ContactCompany = source.ContactCompany;
            target.OrderReff = source.OrderReff;
            target.CompanyId = source.CompanyId;
            target.Comments = source.Comments;
            target.IsStatus = source.IsStatus;
            target.RaisedBy = source.RaisedBy;
            target.ContactId = source.ContactId;
            target.AddressId = source.AddressId;
            target.SupplierId = source.SupplierId;
            target.SupplierTelNo = source.SupplierTelNo;
            target.UserNotes = source.UserNotes;
            target.OrganisationId = source.OrganisationId;
        }

        private static void UpdateDeliveryNoteDetails(DeliveryNote source, DeliveryNote target,
            DeliveryNoteMapperAction actions)
        {
            InitializeDeliveryNoteDetail(source);
            InitializeDeliveryNoteDetail(target);

            UpdateOrAddDeliveryNoteDetails(source, target, actions);
            DeleteDeliveryNoteDetails(source, target, actions);
        }
        /// <summary>
        /// Update or add Delivery Note Details
        /// </summary>
        private static void UpdateOrAddDeliveryNoteDetails(DeliveryNote source, DeliveryNote target, DeliveryNoteMapperAction actions)
        {
            foreach (DeliveryNoteDetail sourceLine in source.DeliveryNoteDetails.ToList())
            {
                UpdateOrAddDeliveryNoteDetail(sourceLine, target, actions);
            }
        }
        /// <summary>
        /// Update target Delivery Note Detail
        /// </summary>
        private static void UpdateOrAddDeliveryNoteDetail(DeliveryNoteDetail sourceDeliveryNoteDetail, DeliveryNote target, DeliveryNoteMapperAction actions)
        {
            DeliveryNoteDetail targetLine;
            if (IsNewDeliveryNoteDetail(sourceDeliveryNoteDetail))
            {
                targetLine = actions.CreateDeliveryNoteDetail();
                target.DeliveryNoteDetails.Add(targetLine);
            }
            else
            {
                targetLine = target.DeliveryNoteDetails.FirstOrDefault(pre => pre.DeliveryDetailid == sourceDeliveryNoteDetail.DeliveryDetailid);
            }

            sourceDeliveryNoteDetail.UpdateTo(targetLine);
        }

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this DeliveryNoteDetail source, DeliveryNoteDetail target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.DeliveryDetailid = source.DeliveryDetailid;
            target.Description = source.Description;
        }

        /// <summary>
        /// True if the Delivery Note Detail is new
        /// </summary>
        private static bool IsNewDeliveryNoteDetail(DeliveryNoteDetail sourceDeliveryNoteDetail)
        {
            return sourceDeliveryNoteDetail.DeliveryDetailid == 0;
        }

        /// <summary>
        /// Initialize target Delivery Note Detail
        /// </summary>
        private static void InitializeDeliveryNoteDetail(DeliveryNote item)
        {
            if (item.DeliveryNoteDetails == null)
            {
                item.DeliveryNoteDetails = new List<DeliveryNoteDetail>();
            }
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeleteDeliveryNoteDetails(DeliveryNote source, DeliveryNote target, DeliveryNoteMapperAction actions)
        {
            List<DeliveryNoteDetail> linesToBeRemoved = target.DeliveryNoteDetails.Where(
                pre => !IsNewDeliveryNoteDetail(pre) && source.DeliveryNoteDetails.All(sourcePre => sourcePre.DeliveryDetailid != pre.DeliveryDetailid))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.DeliveryNoteDetails.Remove(line);
                actions.DeleteDeliveryNoteDetail(line);
            });
        }
        #endregion

        #region Public
        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this DeliveryNote source, DeliveryNote target,
            DeliveryNoteMapperAction actions)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (actions == null)
            {
                throw new ArgumentNullException("actions");
            }
            UpdateDeliveryNote(source, target);
            UpdateDeliveryNoteDetails(source, target, actions);
        }

        #endregion
    }
}
