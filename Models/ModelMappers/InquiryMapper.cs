using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    public static class InquiryMapper
    {
        public static void UpdateTo(this DomainModels.Inquiry source, DomainModels.Inquiry target,
           InquiryMapperActions actions)
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

            UpdateInquiry(source, target);
            UpdateInquiryItems(source, target, actions);
            UpdateInquiryAttachments(source, target, actions);
        }

        private static void UpdateInquiry(DomainModels.Inquiry source, DomainModels.Inquiry target)
        {
            target.InquiryId = source.InquiryId;
            target.Title = source.Title;
            target.ContactId = source.ContactId;
            target.CreatedDate = source.CreatedDate;
            target.SourceId = source.SourceId;
            target.CompanyId = source.CompanyId;
            target.RequireByDate = source.RequireByDate;
            target.SystemUserId = source.SystemUserId;
            target.Status = source.Status;
            target.IsDirectInquiry = source.IsDirectInquiry;
            target.FlagId = source.FlagId;
            //target.InquiryCode = source.InquiryCode;
            target.CreatedBy = source.CreatedBy;
            //target.OrganisationId = source.OrganisationId;
        }

        private static void UpdateInquiryItems(DomainModels.Inquiry source, DomainModels.Inquiry target, InquiryMapperActions actions)
        {
            InitializeInquiryItems(source);
            InitializeInquiryItems(target);

            UpdateOrAddInquiryItems(source, target, actions);
            DeleteInquiryItems(source, target, actions);
        }
        private static void UpdateOrAddInquiryItems(DomainModels.Inquiry source, DomainModels.Inquiry target, InquiryMapperActions actions)
        {
            foreach (DomainModels.InquiryItem sourceLine in source.InquiryItems.ToList())
            {
                UpdateOrAddInquiryItem(sourceLine, target, actions);
            }
        }
        private static void UpdateOrAddInquiryItem(DomainModels.InquiryItem sourceInquiryItem, DomainModels.Inquiry target, InquiryMapperActions actions)
        {
            DomainModels.InquiryItem targetLine;
            if (IsNewInquiryItem(sourceInquiryItem))
            {
                targetLine = actions.CreateInquiryItem();
                target.InquiryItems.Add(targetLine);
            }
            else
            {
                targetLine = target.InquiryItems.FirstOrDefault(pre => pre.InquiryItemId == sourceInquiryItem.InquiryItemId);
            }

            sourceInquiryItem.UpdateTo(targetLine);
        }
        private static bool IsNewInquiryItem(DomainModels.InquiryItem sourceInquiryItem)
        {
            return sourceInquiryItem.InquiryItemId == 0;
        }
        private static void DeleteInquiryItems(DomainModels.Inquiry source, DomainModels.Inquiry target, InquiryMapperActions actions)
        {
            List<DomainModels.InquiryItem> linesToBeRemoved = target.InquiryItems.Where(
                pre => !IsNewInquiryItem(pre) && source.InquiryItems.All(sourcePre => sourcePre.InquiryItemId != pre.InquiryItemId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.InquiryItems.Remove(line);
                actions.DeleteInquiryItem(line);
            });
        }

        private static void UpdateInquiryAttachments(DomainModels.Inquiry source, DomainModels.Inquiry target, InquiryMapperActions actions)
        {
            InitializeInquiryAttachments(source);
            InitializeInquiryAttachments(target);

            UpdateOrAddInquiryAttachments(source, target, actions);
            DeleteInquiryAttachments(source, target, actions);
        }
        /// <summary>
        /// Delete Attachments no longer needed
        /// </summary>
        private static void DeleteInquiryAttachments(Inquiry source, Inquiry target, InquiryMapperActions actions)
        {
            List<InquiryAttachment> linesToBeRemoved = target.InquiryAttachments.Where(
                ii => !IsNewItemAttachment(ii) && source.InquiryAttachments.All(attachment => attachment.AttachmentId != ii.AttachmentId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.InquiryAttachments.Remove(line);
                actions.DeleteInquiryAttachment(line);
            });
        }
        /// <summary>
        /// Update or add Inquiry Attachments
        /// </summary>
        private static void UpdateOrAddInquiryAttachments(Inquiry source, Inquiry target, InquiryMapperActions actions)
        {
            foreach (InquiryAttachment sourceLine in source.InquiryAttachments.ToList())
            {
                UpdateOrAddInquiryAttachment(sourceLine, target, actions);
            }
        }
        /// <summary>
        /// Update target Attachments 
        /// </summary>
        private static void UpdateOrAddInquiryAttachment(InquiryAttachment sourceItemAttachment, Inquiry target, InquiryMapperActions actions)
        {
            InquiryAttachment targetLine;
            if (IsNewItemAttachment(sourceItemAttachment))
            {
                targetLine = actions.CreateInquiryAttachment();
                target.InquiryAttachments.Add(targetLine);
            }
            else
            {
                targetLine = target.InquiryAttachments.FirstOrDefault(attachment => attachment.AttachmentId == sourceItemAttachment.AttachmentId);
            }
            sourceItemAttachment.UpdateTo(targetLine);
        }
        /// <summary>
        /// True if the InquiryAttachment is new
        /// </summary>
        private static bool IsNewItemAttachment(InquiryAttachment sourceItemAttachment)
        {
            return sourceItemAttachment.AttachmentId == 0;
        }
        /// <summary>
        /// Initialize target InquiryAttachments
        /// </summary>
        private static void InitializeInquiryAttachments(Inquiry inquiry)
        {
            if (inquiry.InquiryAttachments== null)
            {
                inquiry.InquiryAttachments = new List<InquiryAttachment>();
            }
        }
        private static void InitializeInquiryItems(DomainModels.Inquiry item)
        {
            if (item.InquiryItems == null)
            {
                item.InquiryItems = new List<DomainModels.InquiryItem>();
            }
        }
    }
}
