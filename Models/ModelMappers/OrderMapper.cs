using System.Collections.Generic;
using System.Linq;
using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{

    /// <summary>
    /// Order mapper
    /// </summary>
    public static class OrderMapper
    {
        #region Private



        #region Order

        #region Order Header

        /// <summary>
        /// Update the header
        /// </summary>
        private static void UpdateHeader(Estimate source, Estimate target)
        {
            target.Estimate_Name = source.Estimate_Name;
            target.CompanyId = source.CompanyId;
            target.AddressId = source.AddressId;
            target.ContactId = source.ContactId;
            target.isDirectSale = source.isDirectSale;
            target.StatusId = source.StatusId;
            target.SectionFlagId = source.SectionFlagId;
            target.IsCreditApproved = source.IsCreditApproved;
            target.Order_Date = source.Order_Date;
            target.FinishDeliveryDate = source.FinishDeliveryDate;
            target.CreationDate = source.CreationDate;
            target.CreationTime = source.CreationTime;
            target.HeadNotes = source.HeadNotes;
            target.FootNotes = source.FootNotes;

            // Update Order Schedule
            UpdateOrderSchedule(source, target);

            // Credit Approval
            UpdateCreditApproval(source, target);
        }

        #endregion Order Header

        #region Credit Approval
        /// <summary>
        /// Update Credit Approval
        /// </summary>
        private static void UpdateCreditApproval(Estimate source, Estimate target)
        {
            target.IsCreditApproved = source.IsCreditApproved;
            target.CreditLimitForJob = source.CreditLimitForJob;
            target.CreditLimitSetBy = source.CreditLimitSetBy;
            target.CreditLimitSetOnDateTime = source.CreditLimitSetOnDateTime;
            target.IsJobAllowedWOCreditCheck = source.IsJobAllowedWOCreditCheck;
            target.AllowJobWOCreditCheckSetBy = source.AllowJobWOCreditCheckSetBy;
            target.AllowJobWOCreditCheckSetOnDateTime = source.AllowJobWOCreditCheckSetOnDateTime;
            target.IsOfficialOrder = source.IsOfficialOrder;
            target.CustomerPO = source.CustomerPO;
            target.OfficialOrderSetBy = source.OfficialOrderSetBy;
            target.OfficialOrderSetOnDateTime = source.OfficialOrderSetOnDateTime;
        }

        #endregion Credit Approval

        #region Order Schedule
        /// <summary>
        /// Update Order Schedule
        /// </summary>
        private static void UpdateOrderSchedule(Estimate source, Estimate target)
        {
            target.ArtworkByDate = source.ArtworkByDate;
            target.DataByDate = source.DataByDate;
            target.PaperByDate = source.PaperByDate;
            target.TargetPrintDate = source.TargetPrintDate;
            target.TargetBindDate = source.TargetBindDate;
            target.Order_CreationDateTime = source.Order_CreationDateTime;
            target.OrderManagerId = source.OrderManagerId;
            target.SalesPersonId = source.SalesPersonId;
            target.SourceId = source.SourceId;
            target.OrderReportSignedBy = source.OrderReportSignedBy;
        }

        #endregion Order Schedule

        #region PrePayment

        /// <summary>
        /// True if the PrePayment is new
        /// </summary>
        private static bool IsNewPrePayment(PrePayment sourcePrePayment)
        {
            return sourcePrePayment.PrePaymentId == 0;
        }

        /// <summary>
        /// Initialize target PrePayments
        /// </summary>
        private static void InitializePrePayments(Estimate item)
        {
            if (item.PrePayments == null)
            {
                item.PrePayments = new List<PrePayment>();
            }
        }

        /// <summary>
        /// Update Pre Payments
        /// </summary>
        private static void UpdatePrePayments(Estimate source, Estimate target, OrderMapperActions actions)
        {
            InitializePrePayments(source);
            InitializePrePayments(target);

            UpdateOrAddPrePayments(source, target, actions);
            DeletePrePayments(source, target, actions);
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeletePrePayments(Estimate source, Estimate target, OrderMapperActions actions)
        {
            List<PrePayment> linesToBeRemoved = target.PrePayments.Where(
                pre => !IsNewPrePayment(pre) && source.PrePayments.All(sourcePre => sourcePre.PrePaymentId != pre.PrePaymentId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.PrePayments.Remove(line);
                actions.DeletePrePayment(line);
            });
        }

        /// <summary>
        /// Update or add Pre Payments
        /// </summary>
        private static void UpdateOrAddPrePayments(Estimate source, Estimate target, OrderMapperActions actions)
        {
            foreach (PrePayment sourceLine in source.PrePayments.ToList())
            {
                UpdateOrAddPrePayment(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Pre Payments
        /// </summary>
        private static void UpdateOrAddPrePayment(PrePayment sourcePrePayment, Estimate target, OrderMapperActions actions)
        {
            PrePayment targetLine;
            if (IsNewPrePayment(sourcePrePayment))
            {
                targetLine = actions.CreatePrePayment();
                target.PrePayments.Add(targetLine);
            }
            else
            {
                targetLine = target.PrePayments.FirstOrDefault(pre => pre.PrePaymentId == sourcePrePayment.PrePaymentId);
            }

            sourcePrePayment.UpdateTo(targetLine);
        }

        #endregion PrePayment

        // TODO: Make Relationship of Estimate and DeliveryNote, NotImplemented Yet
        #region Delivery Schedule

        #endregion Delivery Schedule

        #region Product

        #region Item Section

        /// <summary>
        /// True if the ItemSection is new
        /// </summary>
        private static bool IsNewItemSection(ItemSection sourceItemSection)
        {
            return sourceItemSection.ItemSectionId <= 0;
        }

        /// <summary>
        /// Initialize target ItemSections
        /// </summary>
        private static void InitializeItemSections(Item item)
        {
            if (item.ItemSections == null)
            {
                item.ItemSections = new List<ItemSection>();
            }
        }

        /// <summary>
        /// Update or add Item Sections
        /// </summary>
        private static void UpdateOrAddItemSections(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemSection sourceLine in source.ItemSections.ToList())
            {
                UpdateOrAddItemSection(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddItemSection(ItemSection sourceItemSection, Item target, ItemMapperActions actions)
        {
            ItemSection targetLine;
            if (IsNewItemSection(sourceItemSection))
            {
                targetLine = actions.CreateItemSection();
                target.ItemSections.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemSections.FirstOrDefault(vdp => vdp.ItemSectionId == sourceItemSection.ItemSectionId);
            }

            sourceItemSection.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete sections no longer needed
        /// </summary>
        private static void DeleteItemSections(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemSection> linesToBeRemoved = target.ItemSections.Where(
                vdp => !IsNewItemSection(vdp) && source.ItemSections.All(sourceVdp => sourceVdp.ItemSectionId != vdp.ItemSectionId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemSections.Remove(line);
                actions.DeleteItemSection(line);
            });
        }

        /// <summary>
        /// Update Videos
        /// </summary>
        private static void UpdateItemSections(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemSections(source);
            InitializeItemSections(target);

            // Set Defaults to Non-Print Section
            bool isPrintItem = true;
            if (target.ItemProductDetails != null && target.ItemProductDetails.Any() && !target.ItemSections.Any())
            {
                ItemProductDetail itemProductDetail = target.ItemProductDetails.First();
                if (itemProductDetail.isPrintItem.HasValue && !itemProductDetail.isPrintItem.Value)
                {
                    ItemSection targetItemSection = actions.CreateItemSection();
                    targetItemSection.ItemId = target.ItemId;
                    targetItemSection.SectionName = "Section 1";
                    targetItemSection.SectionNo = 1;
                    target.ItemSections.Add(targetItemSection);
                    actions.SetDefaultsForItemSection(targetItemSection);
                    isPrintItem = false;
                }
            }

            // Add Item Sections if Print Item
            if (isPrintItem)
            {
                UpdateOrAddItemSections(source, target, actions);
            }

            // Delete
            DeleteItemSections(source, target, actions);
        }

        #endregion Item Section

        #region Item Attachment

        /// <summary>
        /// True if the ItemAttachment is new
        /// </summary>
        private static bool IsNewItemAttachment(ItemAttachment sourceItemAttachment)
        {
            return sourceItemAttachment.ItemAttachmentId == 0;
        }

        /// <summary>
        /// Initialize target ItemAttachments
        /// </summary>
        private static void InitializeItemAttachments(Item item)
        {
            if (item.ItemAttachments == null)
            {
                item.ItemAttachments = new List<ItemAttachment>();
            }
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddItemAttachments(Item source, Item target, OrderMapperActions actions)
        {
            foreach (ItemAttachment sourceLine in source.ItemAttachments.ToList())
            {
                UpdateOrAddItemAttachment(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Attachments 
        /// </summary>
        private static void UpdateOrAddItemAttachment(ItemAttachment sourceItemAttachment, Item target, OrderMapperActions actions)
        {
            ItemAttachment targetLine;
            if (IsNewItemAttachment(sourceItemAttachment))
            {
                targetLine = actions.CreateItemAttachment();
                target.ItemAttachments.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemAttachments.FirstOrDefault(attachment => attachment.ItemAttachmentId == sourceItemAttachment.ItemAttachmentId);
            }
            sourceItemAttachment.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete Attachments no longer needed
        /// </summary>
        private static void DeleteItemAttachments(Item source, Item target, OrderMapperActions actions)
        {
            List<ItemAttachment> linesToBeRemoved = target.ItemAttachments.Where(
                ii => !IsNewItemAttachment(ii) && source.ItemAttachments.All(attachment => attachment.ItemAttachmentId != ii.ItemAttachmentId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemAttachments.Remove(line);
                //actions.DeleteItemAttachment(line);
            });
        }

        /// <summary>
        /// Update Attachments
        /// </summary>
        private static void UpdateItemAttachments(Item source, Item target, OrderMapperActions actions)
        {
            InitializeItemAttachments(source);
            InitializeItemAttachments(target);

            UpdateOrAddItemAttachments(source, target, actions);
            DeleteItemAttachments(source, target, actions);
        }

        #endregion Item Attachment

        #region Product Header

        /// <summary>
        /// Update the header
        /// </summary>
        private static void UpdateHeader(Item source, Item target)
        {
            target.ProductCode = source.ProductCode;
            target.ProductName = source.ProductName;
            target.ProductType = source.ProductType;
            target.IsPublished = source.IsPublished;
            target.ItemLastUpdateDateTime = DateTime.Now;
            target.EstimateId = source.EstimateId;
            target.InvoiceDescription = source.InvoiceDescription;
            target.ItemNotes = source.ItemNotes;
            
            // Update Job Description
            UpdateJobDescription(source, target);
        }
        
        /// <summary>
        /// Update Job Description
        /// </summary>
        private static void UpdateJobDescription(Item source, Item target)
        {
            target.JobDescriptionTitle1 = source.JobDescriptionTitle1;
            target.JobDescription1 = source.JobDescription1;
            target.JobDescriptionTitle2 = source.JobDescriptionTitle2;
            target.JobDescription2 = source.JobDescription2;
            target.JobDescriptionTitle3 = source.JobDescriptionTitle3;
            target.JobDescription3 = source.JobDescription3;
            target.JobDescriptionTitle4 = source.JobDescriptionTitle4;
            target.JobDescription4 = source.JobDescription4;
            target.JobDescriptionTitle5 = source.JobDescriptionTitle5;
            target.JobDescription5 = source.JobDescription5;
            target.JobDescriptionTitle6 = source.JobDescriptionTitle6;
            target.JobDescription6 = source.JobDescription6;
            target.JobDescriptionTitle7 = source.JobDescriptionTitle7;
            target.JobDescription7 = source.JobDescription7;
            target.JobDescriptionTitle8 = source.JobDescriptionTitle8;
            target.JobDescription8 = source.JobDescription8;
            target.JobDescriptionTitle9 = source.JobDescriptionTitle9;
            target.JobDescription9 = source.JobDescription9;
            target.JobDescriptionTitle10 = source.JobDescriptionTitle10;
            target.JobDescription10 = source.JobDescription10;
        }

        #endregion Product Header

        #endregion

        #endregion Order


        #endregion Private

        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this Estimate source, Estimate target,
            OrderMapperActions actions)
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

            UpdateHeader(source, target);
            UpdatePrePayments(source, target, actions);
            //UpdateItemSections(source, target, actions);
        }

        #endregion
    }
}
