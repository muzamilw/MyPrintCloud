//using Invoice = MPC.Models.DomainModels.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    public static class InvoiceDomainMapper
    {


        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this  Invoice source, Invoice target,
            InvoiceMapperActions actions)
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

            UpdateItems(source, target, actions);
            UpdateInvoiceDetails(source, target, actions);
        }

        #region Order Product

        /// <summary>
        /// True if the Item is new
        /// </summary>
        private static bool IsNewItem(Item sourceItem)
        {
            return sourceItem.ItemId <= 0;
        }

        /// <summary>
        /// True if the Item is new
        /// </summary>
        private static bool IsNewItem(InvoiceDetail sourceItem)
        {
            return sourceItem.InvoiceDetailId <= 0;
        }

        /// <summary>
        /// Initialize target Items
        /// </summary>
        private static void InitializeItems(Invoice item)
        {
            if (item.Items == null)
            {
                item.Items = new List<Item>();
            }
        }

        /// <summary>
        /// Initialize target Details
        /// </summary>
        private static void InitializeInvoiceDetials(Invoice item)
        {
            if (item.InvoiceDetails == null)
            {
                item.InvoiceDetails = new List<InvoiceDetail>();
            }
        }
        /// <summary>
        /// Update or add Items
        /// </summary>
        private static void UpdateOrAddItems(Invoice source, Invoice target, InvoiceMapperActions actions)
        {
            foreach (Item sourceLine in source.Items.ToList())
            {
                UpdateOrAddItem(sourceLine, target, actions);
            }
        }
        /// <summary>
        /// Update or add invoice Detail Items
        /// </summary>
        private static void UpdateOrAddInvoiceDetails(Invoice source, Invoice target, InvoiceMapperActions actions)
        {
            foreach (InvoiceDetail sourceLine in source.InvoiceDetails.ToList())
            {
                UpdateOrAddInvoiceDetail(sourceLine, target, actions);
            }
        }
        /// <summary>
        /// Update target Items 
        /// </summary>
        private static void UpdateOrAddItem(Item sourceItem, Invoice target, InvoiceMapperActions actions)
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
            //bool assignJobCodes = target.StatusId == (short)OrderStatus.InProduction;

            sourceItem.UpdateToForOrder(targetLine, actions, false);
        }

        /// <summary>
        /// Update target Items 
        /// </summary>
        private static void UpdateOrAddInvoiceDetail(InvoiceDetail sourceItem, Invoice target, InvoiceMapperActions actions)
        {
            InvoiceDetail targetLine;
            if (IsNewItem(sourceItem))
            {
                targetLine = actions.CreateInvoiceDetail();
                target.InvoiceDetails.Add(targetLine);
            }
            else
            {
                targetLine = target.InvoiceDetails.FirstOrDefault(item => item.InvoiceDetailId == sourceItem.InvoiceDetailId);
            }

            sourceItem.UpdateTo(targetLine, actions);
        }


        public static void UpdateTo(this InvoiceDetail source, InvoiceDetail target, InvoiceMapperActions actions)
        {
            target.InvoiceTitle = source.InvoiceTitle;
            target.ItemCharge = source.ItemCharge;
            target.FlagId = source.FlagId;
            target.Quantity = source.Quantity;
            target.ItemTaxValue = source.ItemTaxValue;
            target.Description = source.Description;
            target.TaxValue = source.TaxValue;
            target.ItemGrossTotal = source.ItemGrossTotal;
        }


        /// <summary>
        /// Delete sections no longer needed
        /// </summary>
        private static void DeleteItems(Invoice source, Invoice target, InvoiceMapperActions actions)
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
        private static void UpdateItems(Invoice source, Invoice target, InvoiceMapperActions actions)
        {
            InitializeItems(source);
            InitializeItems(target);

            UpdateOrAddItems(source, target, actions);

            // Delete
            DeleteItems(source, target, actions);
        }

        /// <summary>
        /// Update Invoice Details Items
        /// </summary>
        private static void UpdateInvoiceDetails(Invoice source, Invoice target, InvoiceMapperActions actions)
        {
            InitializeInvoiceDetials(source);
            InitializeInvoiceDetials(target);

            UpdateOrAddInvoiceDetails(source, target, actions);
        }


        /// <summary>
        /// Update Item 
        /// </summary>
        private static void UpdateToForOrder(this Item source, Item target, InvoiceMapperActions actions, bool assignJobCodes)
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
        private static void UpdateOrAddItemSections(Item source, Item target, InvoiceMapperActions actions)
        {
            foreach (ItemSection sourceLine in source.ItemSections.ToList())
            {
                UpdateOrAddItemSection(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddItemSection(ItemSection sourceItemSection, Item target, InvoiceMapperActions actions)
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
        private static void UpdateOrAddSectionCostCentres(ItemSection source, ItemSection target, InvoiceMapperActions actions)
        {
            foreach (SectionCostcentre sourceLine in source.SectionCostcentres.ToList())
            {
                UpdateOrAddSectionCostCentre(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionCostCentre(SectionCostcentre sourceSectionCostcentre, ItemSection target, InvoiceMapperActions actions)
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
        private static void DeleteSectionCostCentres(ItemSection source, ItemSection target, InvoiceMapperActions actions)
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
        private static void UpdateSectionCostCentres(ItemSection source, ItemSection target, InvoiceMapperActions actions)
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
        private static void UpdateOrAddSectionCostCentreDetails(SectionCostcentre source, SectionCostcentre target, InvoiceMapperActions actions)
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
           InvoiceMapperActions actions)
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
        private static void DeleteSectionCostCentreDetails(SectionCostcentre source, SectionCostcentre target, InvoiceMapperActions actions)
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
        private static void UpdateSectionCostCentreDetails(SectionCostcentre source, SectionCostcentre target, InvoiceMapperActions actions)
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
        private static void UpdateOrAddSectionInkCoverages(ItemSection source, ItemSection target, InvoiceMapperActions actions)
        {
            foreach (SectionInkCoverage sourceLine in source.SectionInkCoverages.ToList())
            {
                UpdateOrAddSectionInkCoverage(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionInkCoverage(SectionInkCoverage sourceSectionInkCoverage, ItemSection target, InvoiceMapperActions actions)
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
        private static void DeleteSectionInkCoverages(ItemSection source, ItemSection target, InvoiceMapperActions actions)
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
        private static void UpdateSectionInkCoverages(ItemSection source, ItemSection target, InvoiceMapperActions actions)
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
        private static void UpdateItemSections(Item source, Item target, InvoiceMapperActions actions)
        {
            InitializeItemSections(source);
            InitializeItemSections(target);

            UpdateOrAddItemSections(source, target, actions);
            DeleteItemSections(source, target, actions);
        }

        /// <summary>
        /// Delete Sections no longer needed
        /// </summary>
        private static void DeleteItemSections(Item source, Item target, InvoiceMapperActions actions)
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
        private static void UpdateOrAddItemAttachments(Item source, Item target, InvoiceMapperActions actions)
        {
            foreach (ItemAttachment sourceLine in source.ItemAttachments.ToList())
            {
                UpdateOrAddItemAttachment(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Attachments 
        /// </summary>
        private static void UpdateOrAddItemAttachment(ItemAttachment sourceItemAttachment, Item target, InvoiceMapperActions actions)
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
        private static void DeleteItemAttachments(Item source, Item target, InvoiceMapperActions actions)
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
        private static void UpdateItemAttachments(Item source, Item target, InvoiceMapperActions actions)
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
        private static void UpdateHeader(Item source, Item target, bool assignJobCodes, InvoiceMapperActions actions)
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
            target.RefItemId = source.RefItemId;

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
        private static void UpdateJobDescription(Item source, Item target, bool assignJobCodes, InvoiceMapperActions actions)
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
            // target.JobCode = actions.GetNextJobCode();
        }

        #endregion Product Header

        #endregion
    }
}