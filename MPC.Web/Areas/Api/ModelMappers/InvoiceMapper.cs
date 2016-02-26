using System;
using MPC.MIS.Areas.Api.Models;
using System.Linq;
using MPC.Models.ModelMappers;
//using Invoice = MPC.Models.DomainModels.Invoice;
using DomainModels = MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class InvoiceMapper
    {
        /// <summary>
        /// Domain response to Web response invoice mapper
        /// </summary>
        public static InvoiceRequestResponseModel CreateFrom(this MPC.Models.ResponseModels.InvoiceRequestResponseModel source)
        {
            return new InvoiceRequestResponseModel
            {
                RowCount = source.RowCount,
                HeadNote = source.HeadNote,
                FootNote = source.FootNote,
                Invoices = source.Invoices.Select(invoice => invoice.CreateFrom())
            };
        }

        /// <summary>
        /// Domain invoice to web invoice mapper
        /// </summary>
        public static Invoice CreateFrom(this DomainModels.Invoice source)
        {
            return new Invoice
            {
                InvoiceId = source.InvoiceId,
                CompanyId = source.CompanyId,
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                StoreId = source.Company != null ? source.Company.StoreId : null,
                IsCustomer = source.Company != null ? source.Company.IsCustomer : (short)0,
                CompanyName = source.Company != null ? source.Company.Name : "",
                InvoiceCode = source.InvoiceCode,
                InvoiceName = source.InvoiceName,
                IsArchive = source.IsArchive,
                InvoiceDate = source.InvoiceDate,
                InvoicePostedBy = source.InvoicePostedBy,
                Status = source.Status != null ? source.Status.StatusName : "",
                InvoiceStatus = source.InvoiceStatus,
                InvoiceTotal = source.InvoiceTotal != null ? Math.Round((double)source.InvoiceTotal, 2) : 0,
                ContactName = source.CompanyContact != null ? source.CompanyContact.FirstName + " " + source.CompanyContact.LastName : "",
                FlagId = source.FlagID,
                InvoiceType = source.InvoiceType,
                GrandTotal = source.GrandTotal != null ? Math.Round((double)source.GrandTotal, 2) : 0,
                OrderNo = source.OrderNo,
                AccountNumber = source.AccountNumber,
                ReportSignedBy = source.ReportSignedBy,
                HeadNotes = source.HeadNotes,
                FootNotes = source.FootNotes,
                InvoiceDetails = source.InvoiceDetails != null ? source.InvoiceDetails.Select(i => i.CreateFrom()).ToList() : new List<InvoiceDetail>(),
                Items = source.Items != null ? source.Items.Select(i => i.CreateFromForOrder()).ToList() : new List<OrderItem>()
            };
        }
        public static DomainModels.Invoice CreateFrom(this Invoice source)
        {
            return new DomainModels.Invoice
            {
                InvoiceId = source.InvoiceId,
                CompanyId = source.CompanyId,
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                InvoiceCode = source.InvoiceCode,
                InvoiceName = source.InvoiceName,
                IsArchive = source.IsArchive,
                InvoiceDate = source.InvoiceDate,
                InvoiceStatus = source.InvoiceStatus,
                InvoiceTotal = source.InvoiceTotal != null ? Math.Round((double)source.InvoiceTotal, 2) : 0,
                FlagID = source.FlagId,
                InvoiceType = source.InvoiceType,
                GrandTotal = source.GrandTotal != null ? Math.Round((double)source.GrandTotal, 2) : 0,
                OrderNo = source.OrderNo,
                AccountNumber = source.AccountNumber,
                ReportSignedBy = source.ReportSignedBy,
                InvoicePostedBy = source.InvoicePostedBy,
                HeadNotes = source.HeadNotes,
                FootNotes = source.FootNotes,
                InvoiceDetails = source.InvoiceDetails != null ? source.InvoiceDetails.Select(i => i.CreateFrom()).ToList() : null,
                Items = source.Items != null ? source.Items.Select(i => i.CreateFromForOrder()).ToList() : null
            };
        }

        private static InvoicesListModel CreateForList(this  DomainModels.Invoice source)
        {
            int itemsTotal = 0;
            if (source.InvoiceDetails != null)
            {
                itemsTotal = source.InvoiceDetails.Count();
            }
            if (source.Items != null)
            {
                itemsTotal = itemsTotal + source.Items.Count(i => i.ItemType != 2);
            }
            return new InvoicesListModel
            {
                InvoiceId = source.InvoiceId,
                CompanyName = source.Company != null ? source.Company.Name : "",
                InvoiceCode = source.InvoiceCode,
                InvoiceName = source.InvoiceName,
                InvoiceDate = source.InvoiceDate,
                InvoiceTotal = source.InvoiceTotal,
                GrandTotal = source.GrandTotal != null ? Math.Round((double)source.GrandTotal, 2) : 0,
                StatusId = source.Status != null ? source.Status.StatusId : 0,
                Status = source.Status != null ? source.Status.StatusName : string.Empty,
                FlagId = source.FlagID,
                OrderNo = source.InvoiceType == 1 ? "false" : "true",
                ItemsCount = itemsTotal,
                InvoiceType = source.InvoiceType,
                isDirectSale = source.InvoiceType == 1 ? false : true,
            };
        }

        public static InvoiceListResponseModel CreateFromList(this MPC.Models.ResponseModels.InvoiceRequestResponseModel source)
        {
            return new InvoiceListResponseModel
            {
                RowCount = source.RowCount,
                HeadNote = source.HeadNote,
                FootNote = source.FootNote,
                Invoices = source.Invoices.Select(invoice => invoice.CreateForList())
            };

        }

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateToInvoice(this  DomainModels.Invoice source, DomainModels.Invoice target,
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
        }

        #region Order Product

        /// <summary>
        /// True if the Item is new
        /// </summary>
        private static bool IsNewItem(DomainModels.Item sourceItem)
        {
            return sourceItem.ItemId <= 0;
        }

        /// <summary>
        /// Initialize target Items
        /// </summary>
        private static void InitializeItems(DomainModels.Invoice item)
        {
            if (item.Items == null)
            {
                item.Items = new List<DomainModels.Item>();
            }
        }

        /// <summary>
        /// Update or add Items
        /// </summary>
        private static void UpdateOrAddItems(DomainModels.Invoice source, DomainModels.Invoice target, InvoiceMapperActions actions)
        {
            foreach (DomainModels.Item sourceLine in source.Items.ToList())
            {
                UpdateOrAddItem(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Items 
        /// </summary>
        private static void UpdateOrAddItem(DomainModels.Item sourceItem, DomainModels.Invoice target, InvoiceMapperActions actions)
        {
            DomainModels.Item targetLine;
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
        /// Delete sections no longer needed
        /// </summary>
        private static void DeleteItems(DomainModels.Invoice source, DomainModels.Invoice target, InvoiceMapperActions actions)
        {
            List<DomainModels.Item> linesToBeRemoved = target.Items.Where(
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
        private static void UpdateItems(DomainModels.Invoice source, DomainModels.Invoice target, InvoiceMapperActions actions)
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
        private static void UpdateToForOrder(this DomainModels.Item source, DomainModels.Item target, InvoiceMapperActions actions, bool assignJobCodes)
        {
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
        private static bool IsNewItemSection(DomainModels.ItemSection sourceItemSection)
        {
            return sourceItemSection.ItemSectionId <= 0;
        }

        /// <summary>
        /// Initialize target ItemSections
        /// </summary>
        private static void InitializeItemSections(DomainModels.Item item)
        {
            if (item.ItemSections == null)
            {
                item.ItemSections = new List<DomainModels.ItemSection>();
            }
        }

        /// <summary>
        /// Update or add Item Sections
        /// </summary>
        private static void UpdateOrAddItemSections(DomainModels.Item source, DomainModels.Item target, InvoiceMapperActions actions)
        {
            foreach (DomainModels.ItemSection sourceLine in source.ItemSections.ToList())
            {
                UpdateOrAddItemSection(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddItemSection(DomainModels.ItemSection sourceItemSection, DomainModels.Item target, InvoiceMapperActions actions)
        {
            DomainModels.ItemSection targetLine;
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
        private static bool IsNewSectionCostCentre(DomainModels.SectionCostcentre source)
        {
            return source.SectionCostcentreId <= 0;
        }

        /// <summary>
        /// Initialize target Section Cost Centres
        /// </summary>
        private static void InitializeSectionCostCentres(DomainModels.ItemSection item)
        {
            if (item.SectionCostcentres == null)
            {
                item.SectionCostcentres = new List<DomainModels.SectionCostcentre>();
            }
        }

        /// <summary>
        /// Update or add Item Sections
        /// </summary>
        private static void UpdateOrAddSectionCostCentres(DomainModels.ItemSection source, DomainModels.ItemSection target, InvoiceMapperActions actions)
        {
            foreach (DomainModels.SectionCostcentre sourceLine in source.SectionCostcentres.ToList())
            {
                UpdateOrAddSectionCostCentre(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionCostCentre(DomainModels.SectionCostcentre sourceSectionCostcentre, DomainModels.ItemSection target, InvoiceMapperActions actions)
        {
            DomainModels.SectionCostcentre targetLine;
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
        private static void DeleteSectionCostCentres(DomainModels.ItemSection source, DomainModels.ItemSection target, InvoiceMapperActions actions)
        {
            List<DomainModels.SectionCostcentre> linesToBeRemoved = target.SectionCostcentres.Where(
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
        private static void UpdateSectionCostCentres(DomainModels.ItemSection source, DomainModels.ItemSection target, InvoiceMapperActions actions)
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
        private static bool IsNewSectionCostCentreDetail(DomainModels.SectionCostCentreDetail source)
        {
            return source.SectionCostCentreDetailId <= 0;
        }

        /// <summary>
        /// Initialize target Section Cost Centres
        /// </summary>
        private static void InitializeSectionCostCentreDetails(DomainModels.SectionCostcentre item)
        {
            if (item.SectionCostCentreDetails == null)
            {
                item.SectionCostCentreDetails = new List<DomainModels.SectionCostCentreDetail>();
            }
        }

        /// <summary>
        /// Update or add Item Sections
        /// </summary>
        private static void UpdateOrAddSectionCostCentreDetails(DomainModels.SectionCostcentre source, DomainModels.SectionCostcentre target, InvoiceMapperActions actions)
        {
            foreach (DomainModels.SectionCostCentreDetail sourceLine in source.SectionCostCentreDetails.ToList())
            {
                UpdateOrAddSectionCostCentreDetail(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionCostCentreDetail(DomainModels.SectionCostCentreDetail sourceSectionCostCentreDetail, DomainModels.SectionCostcentre target,
           InvoiceMapperActions actions)
        {
            DomainModels.SectionCostCentreDetail targetLine;
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
        private static void DeleteSectionCostCentreDetails(DomainModels.SectionCostcentre source, DomainModels.SectionCostcentre target, InvoiceMapperActions actions)
        {
            List<DomainModels.SectionCostCentreDetail> linesToBeRemoved = target.SectionCostCentreDetails.Where(
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
        private static void UpdateSectionCostCentreDetails(DomainModels.SectionCostcentre source, DomainModels.SectionCostcentre target, InvoiceMapperActions actions)
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
        private static bool IsNewSectionInkCoverage(DomainModels.SectionInkCoverage source)
        {
            return source.Id <= 0;
        }

        /// <summary>
        /// Initialize target Section Ink Coverage
        /// </summary>
        private static void InitializeSectionInkCoverages(DomainModels.ItemSection item)
        {
            if (item.SectionInkCoverages == null)
            {
                item.SectionInkCoverages = new List<DomainModels.SectionInkCoverage>();
            }
        }

        /// <summary>
        /// Update or add Item Section Ink Coverages
        /// </summary>
        private static void UpdateOrAddSectionInkCoverages(DomainModels.ItemSection source, DomainModels.ItemSection target, InvoiceMapperActions actions)
        {
            foreach (DomainModels.SectionInkCoverage sourceLine in source.SectionInkCoverages.ToList())
            {
                UpdateOrAddSectionInkCoverage(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionInkCoverage(DomainModels.SectionInkCoverage sourceSectionInkCoverage, DomainModels.ItemSection target, InvoiceMapperActions actions)
        {
            DomainModels.SectionInkCoverage targetLine;
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
        private static void DeleteSectionInkCoverages(DomainModels.ItemSection source, DomainModels.ItemSection target, InvoiceMapperActions actions)
        {
            List<DomainModels.SectionInkCoverage> linesToBeRemoved = target.SectionInkCoverages.Where(
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
        private static void UpdateSectionInkCoverages(DomainModels.ItemSection source, DomainModels.ItemSection target, InvoiceMapperActions actions)
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
        private static void UpdateItemSections(DomainModels.Item source, DomainModels.Item target, InvoiceMapperActions actions)
        {
            InitializeItemSections(source);
            InitializeItemSections(target);

            UpdateOrAddItemSections(source, target, actions);
            DeleteItemSections(source, target, actions);
        }

        /// <summary>
        /// Delete Sections no longer needed
        /// </summary>
        private static void DeleteItemSections(DomainModels.Item source, DomainModels.Item target, InvoiceMapperActions actions)
        {
            List<DomainModels.ItemSection> linesToBeRemoved = target.ItemSections.Where(
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
        private static bool IsNewItemAttachment(DomainModels.ItemAttachment sourceItemAttachment)
        {
            return sourceItemAttachment.ItemAttachmentId == 0;
        }

        /// <summary>
        /// Initialize target ItemAttachments
        /// </summary>
        private static void InitializeItemAttachments(DomainModels.Item item)
        {
            if (item.ItemAttachments == null)
            {
                item.ItemAttachments = new List<DomainModels.ItemAttachment>();
            }
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddItemAttachments(DomainModels.Item source, DomainModels.Item target, InvoiceMapperActions actions)
        {
            foreach (DomainModels.ItemAttachment sourceLine in source.ItemAttachments.ToList())
            {
                UpdateOrAddItemAttachment(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Attachments 
        /// </summary>
        private static void UpdateOrAddItemAttachment(DomainModels.ItemAttachment sourceItemAttachment, DomainModels.Item target, InvoiceMapperActions actions)
        {
            DomainModels.ItemAttachment targetLine;
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
        private static void DeleteItemAttachments(DomainModels.Item source, DomainModels.Item target, InvoiceMapperActions actions)
        {
            List<DomainModels.ItemAttachment> linesToBeRemoved = target.ItemAttachments.Where(
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
        private static void UpdateItemAttachments(DomainModels.Item source, DomainModels.Item target, InvoiceMapperActions actions)
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
    }
}