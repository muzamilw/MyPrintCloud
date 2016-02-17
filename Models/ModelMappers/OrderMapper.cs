using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Forms.VisualStyles;
using MPC.Models.Common;
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
            target.BillingAddressId = source.AddressId;
            target.ContactId = source.ContactId;
            target.isDirectSale = source.isDirectSale;
            target.StatusId = source.StatusId;
            target.SectionFlagId = source.SectionFlagId;
            target.IsCreditApproved = source.IsCreditApproved;
            target.Order_Date = source.Order_Date;
            target.EstimateDate = source.EstimateDate;
            target.FinishDeliveryDate = source.FinishDeliveryDate;
            target.CreationDate = (!source.CreationDate.HasValue || source.CreationDate.Value <= DateTime.MinValue) ? DateTime.Now : source.CreationDate;
            target.CreationTime = source.CreationTime <= DateTime.MinValue ? DateTime.Now : source.CreationTime;
            target.HeadNotes = source.HeadNotes;
            target.FootNotes = source.FootNotes;
            target.isEstimate = source.isEstimate;

            target.Estimate_Total = source.Estimate_Total;

            target.EnquiryId = source.EnquiryId;

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
            target.ReportSignedBy = source.ReportSignedBy;
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

        #region Delivery Schedule

        /// <summary>
        /// True if the ShippingInformation is new
        /// </summary>
        private static bool IsNewShippingInformation(ShippingInformation sourceShippingInformation)
        {
            return sourceShippingInformation.ShippingId <= 0;
        }

        /// <summary>
        /// Initialize target ShippingInformations
        /// </summary>
        private static void InitializeShippingInformations(Estimate item)
        {
            if (item.ShippingInformations == null)
            {
                item.ShippingInformations = new List<ShippingInformation>();
            }
        }

        /// <summary>
        /// Update Pre Payments
        /// </summary>
        private static void UpdateShippingInformations(Estimate source, Estimate target, OrderMapperActions actions)
        {
            InitializeShippingInformations(source);
            InitializeShippingInformations(target);

            UpdateOrAddShippingInformations(source, target, actions);
            DeleteShippingInformations(source, target, actions);
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeleteShippingInformations(Estimate source, Estimate target, OrderMapperActions actions)
        {
            List<ShippingInformation> linesToBeRemoved = target.ShippingInformations.Where(
                pre => !IsNewShippingInformation(pre) && source.ShippingInformations.All(sourcePre => sourcePre.ShippingId != pre.ShippingId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ShippingInformations.Remove(line);
                actions.DeleteShippingInformation(line);
            });
        }

        /// <summary>
        /// Update or add Pre Payments
        /// </summary>
        private static void UpdateOrAddShippingInformations(Estimate source, Estimate target, OrderMapperActions actions)
        {
            foreach (ShippingInformation sourceLine in source.ShippingInformations.ToList())
            {
                UpdateOrAddShippingInformation(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Shipping Information
        /// </summary>
        private static void UpdateOrAddShippingInformation(ShippingInformation sourceShippingInformation, Estimate target, OrderMapperActions actions)
        {
            ShippingInformation targetLine;
            if (IsNewShippingInformation(sourceShippingInformation))
            {
                targetLine = actions.CreateShippingInformation();
                target.ShippingInformations.Add(targetLine);
            }
            else
            {
                targetLine = target.ShippingInformations.FirstOrDefault(pre => pre.ShippingId == sourceShippingInformation.ShippingId);
            }

            sourceShippingInformation.UpdateTo(targetLine);
        }

        #endregion Delivery Schedule

        #region Order Product

        /// <summary>
        /// True if the Item is new
        /// </summary>
        private static bool IsNewItem(Item sourceItem)
        {
            return sourceItem.ItemId <= 0;
        }

        /// <summary>
        /// Initialize target Items
        /// </summary>
        private static void InitializeItems(Estimate item)
        {
            if (item.Items == null)
            {
                item.Items = new List<Item>();
            }
        }

        /// <summary>
        /// Update or add Items
        /// </summary>
        private static void UpdateOrAddItems(Estimate source, Estimate target, OrderMapperActions actions)
        {
            foreach (Item sourceLine in source.Items.ToList())
            {
                UpdateOrAddItem(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Items 
        /// </summary>
        private static void UpdateOrAddItem(Item sourceItem, Estimate target, OrderMapperActions actions)
        {
            Item targetLine;
            if (IsNewItem(sourceItem))
            {
                targetLine = actions.CreateItem();
                target.Items.Add(targetLine);
            }
            else
            {
                targetLine = target.Items.FirstOrDefault(item => item.ItemId == sourceItem.ItemId);
            }

            
            // If Order is in Production then assign Job Codes to Items
            bool assignJobCodes = target.StatusId == (short)OrderStatus.InProduction;
            if (target.StatusId == (short)OrderStatus.Completed_NotShipped)
                sourceItem.StatusId = (short)ItemStatuses.ShippedInvoiced;
            else if (target.StatusId == (short)OrderStatus.Invoice)
                sourceItem.StatusId = (short)ItemStatuses.ShippedInvoiced;
            
            if (target.StatusId == (short)OrderStatus.InProduction && (sourceItem.StatusId == (short)ItemStatuses.ShippedInvoiced || sourceItem.StatusId == (short)ItemStatuses.ReadyForShipping))
                sourceItem.StatusId = (short)ItemStatuses.NeedAssigning;
            if ((target.StatusId == (short)OrderStatus.ConfirmedOrder || target.StatusId == (short)OrderStatus.PendingOrder) && sourceItem.StatusId != (short)ItemStatuses.NotProgressedToJob)
                sourceItem.StatusId = (short)ItemStatuses.NotProgressedToJob;

            
            sourceItem.UpdateToForOrder(targetLine, actions, assignJobCodes);
        }

        /// <summary>
        /// Delete sections no longer needed
        /// </summary>
        private static void DeleteItems(Estimate source, Estimate target, OrderMapperActions actions)
        {
            List<Item> linesToBeRemoved = target.Items.Where(
                vdp => !IsNewItem(vdp) && source.Items.All(sourceVdp => sourceVdp.ItemId != vdp.ItemId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.Items.Remove(line);
                actions.DeleteItem(line);
            });
        }

        /// <summary>
        /// Update Item Sections
        /// </summary>
        private static void UpdateItems(Estimate source, Estimate target, OrderMapperActions actions)
        {
            InitializeItems(source);
            InitializeItems(target);

            UpdateOrAddItems(source, target, actions);

            // Delete
            DeleteItems(source, target, actions);
        }

        /// <summary>
        /// Update Item 
        /// </summary>
        private static void UpdateToForOrder(this Item source, Item target, OrderMapperActions actions, bool assignJobCodes)
        {
            // Update Header
            UpdateHeader(source, target, assignJobCodes, actions);

            // Update Item Sections
            UpdateItemSections(source, target, actions);

            // Update Item Attachments
            UpdateItemAttachments(source, target, actions);
        }

        #endregion Order Product

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
        private static void UpdateOrAddItemSections(Item source, Item target, OrderMapperActions actions)
        {
            foreach (ItemSection sourceLine in source.ItemSections.ToList())
            {
                UpdateOrAddItemSection(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddItemSection(ItemSection sourceItemSection, Item target, OrderMapperActions actions)
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

            sourceItemSection.UpdateToForOrder(targetLine);

            // Update Section Cost Centres
            UpdateSectionCostCentres(sourceItemSection, targetLine, actions);

            // Update Section Ink Coverages
            UpdateSectionInkCoverages(sourceItemSection, targetLine, actions);
        }

        #region Section Cost Centres

        /// <summary>
        /// True if the Section Cost Centre is new
        /// </summary>
        private static bool IsNewSectionCostCentre(SectionCostcentre source)
        {
            return source.SectionCostcentreId <= 0;
        }

        /// <summary>
        /// Initialize target Section Cost Centres
        /// </summary>
        private static void InitializeSectionCostCentres(ItemSection item)
        {
            if (item.SectionCostcentres == null)
            {
                item.SectionCostcentres = new List<SectionCostcentre>();
            }
        }

        /// <summary>
        /// Update or add Item Sections
        /// </summary>
        private static void UpdateOrAddSectionCostCentres(ItemSection source, ItemSection target, OrderMapperActions actions)
        {
            foreach (SectionCostcentre sourceLine in source.SectionCostcentres.ToList())
            {
                UpdateOrAddSectionCostCentre(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionCostCentre(SectionCostcentre sourceSectionCostcentre, ItemSection target, OrderMapperActions actions)
        {
            SectionCostcentre targetLine;
            if (IsNewSectionCostCentre(sourceSectionCostcentre))
            {
                targetLine = actions.CreateSectionCostCentre();
                target.SectionCostcentres.Add(targetLine);
            }
            else
            {
                targetLine = target.SectionCostcentres.FirstOrDefault(vdp => vdp.SectionCostcentreId == sourceSectionCostcentre.SectionCostcentreId);
            }

            sourceSectionCostcentre.UpdateTo(targetLine);

            // Update Section Cost Centres
            UpdateSectionCostCentreDetails(sourceSectionCostcentre, targetLine, actions);
        }

        /// <summary>
        /// Delete section cost centres no longer needed
        /// </summary>
        private static void DeleteSectionCostCentres(ItemSection source, ItemSection target, OrderMapperActions actions)
        {
            List<SectionCostcentre> linesToBeRemoved = target.SectionCostcentres.Where(
                vdp => !IsNewSectionCostCentre(vdp) && source.SectionCostcentres.All(sourceVdp => sourceVdp.SectionCostcentreId != vdp.SectionCostcentreId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.SectionCostcentres.Remove(line);
                actions.DeleteSectionCostCenter(line);
            });
        }

        /// <summary>
        /// Update Section Cost Centres
        /// </summary>
        private static void UpdateSectionCostCentres(ItemSection source, ItemSection target, OrderMapperActions actions)
        {
            InitializeSectionCostCentres(source);
            InitializeSectionCostCentres(target);

            UpdateOrAddSectionCostCentres(source, target, actions);

            // Delete
            DeleteSectionCostCentres(source, target, actions);
        }

        #endregion Section Cost Centres

        #region Section Cost Centre Detail

        /// <summary>
        /// True if the Section Cost Centre is new
        /// </summary>
        private static bool IsNewSectionCostCentreDetail(SectionCostCentreDetail source)
        {
            return source.SectionCostCentreDetailId <= 0;
        }

        /// <summary>
        /// Initialize target Section Cost Centres
        /// </summary>
        private static void InitializeSectionCostCentreDetails(SectionCostcentre item)
        {
            if (item.SectionCostCentreDetails == null)
            {
                item.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }
        }

        /// <summary>
        /// Update or add Item Sections
        /// </summary>
        private static void UpdateOrAddSectionCostCentreDetails(SectionCostcentre source, SectionCostcentre target, OrderMapperActions actions)
        {
            foreach (SectionCostCentreDetail sourceLine in source.SectionCostCentreDetails.ToList())
            {
                UpdateOrAddSectionCostCentreDetail(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionCostCentreDetail(SectionCostCentreDetail sourceSectionCostCentreDetail, SectionCostcentre target,
            OrderMapperActions actions)
        {
            SectionCostCentreDetail targetLine;
            if (IsNewSectionCostCentreDetail(sourceSectionCostCentreDetail))
            {
                targetLine = actions.CreateSectionCostCenterDetail();
                target.SectionCostCentreDetails.Add(targetLine);
            }
            else
            {
                targetLine = target.SectionCostCentreDetails.FirstOrDefault(vdp => vdp.SectionCostCentreDetailId ==
                    sourceSectionCostCentreDetail.SectionCostCentreDetailId);
            }

            sourceSectionCostCentreDetail.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete section cost centres no longer needed
        /// </summary>
        private static void DeleteSectionCostCentreDetails(SectionCostcentre source, SectionCostcentre target, OrderMapperActions actions)
        {
            List<SectionCostCentreDetail> linesToBeRemoved = target.SectionCostCentreDetails.Where(
                vdp => !IsNewSectionCostCentreDetail(vdp) && source.SectionCostCentreDetails.All(sourceVdp =>
                    sourceVdp.SectionCostCentreDetailId != vdp.SectionCostCentreDetailId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.SectionCostCentreDetails.Remove(line);
                actions.DeleteSectionCostCenterDetail(line);
            });
        }

        /// <summary>
        /// Update Section Cost Centres
        /// </summary>
        private static void UpdateSectionCostCentreDetails(SectionCostcentre source, SectionCostcentre target, OrderMapperActions actions)
        {
            InitializeSectionCostCentreDetails(source);
            InitializeSectionCostCentreDetails(target);

            UpdateOrAddSectionCostCentreDetails(source, target, actions);

            // Delete
            DeleteSectionCostCentreDetails(source, target, actions);
        }

        #endregion Section Cost Centre Detail


        #region Section Ink Coverage

        /// <summary>
        /// True if the Section Ink COverage is new
        /// </summary>
        private static bool IsNewSectionInkCoverage(SectionInkCoverage source)
        {
            return source.Id <= 0;
        }

        /// <summary>
        /// Initialize target Section Ink Coverage
        /// </summary>
        private static void InitializeSectionInkCoverages(ItemSection item)
        {
            if (item.SectionInkCoverages == null)
            {
                item.SectionInkCoverages = new List<SectionInkCoverage>();
            }
        }

        /// <summary>
        /// Update or add Item Section Ink Coverages
        /// </summary>
        private static void UpdateOrAddSectionInkCoverages(ItemSection source, ItemSection target, OrderMapperActions actions)
        {
            foreach (SectionInkCoverage sourceLine in source.SectionInkCoverages.ToList())
            {
                UpdateOrAddSectionInkCoverage(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionInkCoverage(SectionInkCoverage sourceSectionInkCoverage, ItemSection target, OrderMapperActions actions)
        {
            SectionInkCoverage targetLine;
            if (IsNewSectionInkCoverage(sourceSectionInkCoverage))
            {
                targetLine = actions.CreateSectionInkCoverage();
                target.SectionInkCoverages.Add(targetLine);
            }
            else
            {
                targetLine = target.SectionInkCoverages.FirstOrDefault(vdp => vdp.Id == sourceSectionInkCoverage.Id);
            }

            sourceSectionInkCoverage.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete section ink coverage no longer needed
        /// </summary>
        private static void DeleteSectionInkCoverages(ItemSection source, ItemSection target, OrderMapperActions actions)
        {
            List<SectionInkCoverage> linesToBeRemoved = target.SectionInkCoverages.Where(
                vdp => !IsNewSectionInkCoverage(vdp) && source.SectionInkCoverages.All(sourceVdp => sourceVdp.Id != vdp.Id))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.SectionInkCoverages.Remove(line);
                actions.DeleteSectionInkCoverage(line);
            });
        }

        /// <summary>
        /// Update Section Cost Centres
        /// </summary>
        private static void UpdateSectionInkCoverages(ItemSection source, ItemSection target, OrderMapperActions actions)
        {
            InitializeSectionInkCoverages(source);
            InitializeSectionInkCoverages(target);

            UpdateOrAddSectionInkCoverages(source, target, actions);

            // Delete
            DeleteSectionInkCoverages(source, target, actions);
        }

        #endregion Section Ink Coverage


        /// <summary>
        /// Update Item Sections
        /// </summary>
        private static void UpdateItemSections(Item source, Item target, OrderMapperActions actions)
        {
            InitializeItemSections(source);
            InitializeItemSections(target);

            UpdateOrAddItemSections(source, target, actions);
            DeleteItemSections(source, target, actions);
        }

        /// <summary>
        /// Delete Sections no longer needed
        /// </summary>
        private static void DeleteItemSections(Item source, Item target, OrderMapperActions actions)
        {
            List<ItemSection> linesToBeRemoved = target.ItemSections.Where(
                ii => !IsNewItemSection(ii) && source.ItemSections.All(section => section.ItemSectionId != ii.ItemSectionId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemSections.Remove(line);
                actions.DeleteItemSection(line);
            });
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
                actions.DeleteItemAttachment(line);
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
        private static void UpdateHeader(Item source, Item target, bool assignJobCodes, OrderMapperActions actions)
        {
            target.ProductCode = source.ProductCode;
            target.ProductName = source.ProductName;
            target.ProductType = source.ProductType;
            target.ItemType = source.ItemType;
            target.IsPublished = source.IsPublished;
            target.ItemLastUpdateDateTime = DateTime.Now;
            target.EstimateId = source.EstimateId;
            target.InvoiceDescription = source.InvoiceDescription;
            target.ItemNotes = source.ItemNotes;
            target.ItemId = source.ItemId;
            target.SupplierId = source.SupplierId;
            target.SupplierId2 = source.SupplierId2;
            target.IsFinishedGoodPrivate = source.IsFinishedGoodPrivate;
            target.StatusId = source.StatusId;
            if (source.RefItemId != null)
            {
                target.RefItemId = source.RefItemId;
            }

            // Update Charges
            UpdateCharges(source, target);

            // Update Job Description
            UpdateJobDescription(source, target, assignJobCodes, actions);
        }

        /// <summary>
        /// Updates Charges
        /// </summary>
        private static void UpdateCharges(Item source, Item target)
        {
            target.Qty1 = source.Qty1;
            target.Qty2 = source.Qty2;
            target.Qty3 = source.Qty3;
            target.Qty1MarkUpId1 = source.Qty1MarkUpId1;
            target.Qty2MarkUpId2 = source.Qty2MarkUpId2;
            target.Qty3MarkUpId3 = source.Qty3MarkUpId3;
            target.Qty1NetTotal = source.Qty1NetTotal;
            target.Qty2NetTotal = source.Qty2NetTotal;
            target.Qty3NetTotal = source.Qty3NetTotal;
            target.Tax1 = source.Tax1;
            target.Qty1Tax1Value = source.Qty1Tax1Value;
            target.Qty2Tax1Value = source.Qty2Tax1Value;
            target.Qty3Tax1Value = source.Qty3Tax1Value;
            target.Qty1GrossTotal = source.Qty1GrossTotal;
            target.Qty2GrossTotal = source.Qty2GrossTotal;
            target.Qty3GrossTotal = source.Qty3GrossTotal;
        }

        /// <summary>
        /// Update Job Description
        /// </summary>
        private static void UpdateJobDescription(Item source, Item target, bool assignJobCodes, OrderMapperActions actions)
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
            target.JobStatusId = source.JobStatusId;
            target.JobManagerId = source.JobManagerId;
            target.JobProgressedBy = source.JobProgressedBy;
            target.JobEstimatedStartDateTime = source.JobEstimatedStartDateTime;
            target.JobEstimatedCompletionDateTime = source.JobEstimatedCompletionDateTime;
            target.JobCardPrintedBy = source.JobCardPrintedBy;
            target.JobCode = source.JobCode;

            // If Job Code is Already Assigned then skip
            if (!assignJobCodes || !string.IsNullOrEmpty(target.JobCode))
            {
                return;
            }

            // Get Next Job Code
            target.JobCode = actions.GetNextJobCode();
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
            UpdateShippingInformations(source, target, actions);
            UpdateItems(source, target, actions);
        }

        public static void AddInvoice(this Estimate source, Invoice target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            UpdateInvoice(source, target);
            UpdateItemsInvoiceIds(target, source);
        }

        public static void UpdateItemsInvoiceIds(Invoice source, Estimate target)
        {
            InitializeItems(target);
            foreach (var item in target.Items)
            {
                item.InvoiceId = source.InvoiceId;
            }
        }

        /// <summary>
        /// Update the header
        /// </summary>
        private static void UpdateInvoice(Estimate source, Invoice target)
        {
            //target.Order_Date = source.Order_Date;
            //target.FinishDeliveryDate = source.FinishDeliveryDate;
            //target.CreationDate = (!source.CreationDate.HasValue || source.CreationDate.Value <= DateTime.MinValue) ? DateTime.Now : source.CreationDate;
            //target.CreationTime = source.CreationTime <= DateTime.MinValue ? DateTime.Now : source.CreationTime;

            target.CompanyId = source.CompanyId;
            target.EstimateId = source.EstimateId;
            target.ContactId = source.ContactId;
            target.AddressId = source.AddressId;
            target.InvoiceName = source.Estimate_Name;
            target.InvoiceDate = DateTime.Now;
            target.InvoiceTotal = source.Estimate_Total;
            //target.FlagID = source.SectionFlagId;
            target.AccountNumber = source.AccountNumber;
            target.ReportSignedBy = source.OrderReportSignedBy;
            //target.InvoicePostedBy = source.OrderReportSignedBy;
            target.HeadNotes = source.HeadNotes;
            target.FootNotes = source.FootNotes;
        }
        #endregion
    }
}
