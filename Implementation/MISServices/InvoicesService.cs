using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class InvoicesService : IInvoiceService
    {
        #region Private

        private readonly IInvoiceRepository invoiceRepository;
        private readonly ICostCentreRepository CostCentreRepository;
        private readonly IItemRepository itemRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IItemAttachmentRepository itemAttachmentRepository;
        private readonly IItemSectionRepository itemsectionRepository;
        private readonly ISectionCostCentreRepository sectionCostCentreRepository;
        private readonly ISectionInkCoverageRepository sectionInkCoverageRepository;
        private readonly ISectionCostCentreDetailRepository sectionCostCentreDetailRepository;
        private readonly IInvoiceDetailRepository invoiceDetailRepository;
        private readonly IOrganisationRepository organisationRepository;

        /// <summary>
        /// Creates New Item and assigns new generated code
        /// </summary>
        private Item CreateItem()
        {
            Item itemTarget = itemRepository.Create();
            itemRepository.Add(itemTarget);
            string itemCode = prefixRepository.GetNextItemCodePrefix(false);
            itemTarget.ItemCode = itemCode;
            itemTarget.OrganisationId = invoiceRepository.OrganisationId;
            return itemTarget;
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItem(Item item)
        {
            itemRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Item Attachment new generated code
        /// </summary>
        private ItemAttachment CreateItemAttachment()
        {
            ItemAttachment itemTarget = itemAttachmentRepository.Create();
            itemAttachmentRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Item Attachment
        /// </summary>
        private void DeleteItemAttachment(ItemAttachment item)
        {
            itemAttachmentRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Item Section new generated code
        /// </summary>
        private ItemSection CreateItemSection()
        {
            ItemSection itemTarget = itemsectionRepository.Create();
            itemsectionRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Item Section
        /// </summary>
        private void DeleteItemSection(ItemSection item)
        {
            itemsectionRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Section Cost Centre
        /// </summary>
        private SectionCostcentre CreateSectionCostCentre()
        {
            SectionCostcentre itemTarget = sectionCostCentreRepository.Create();
            sectionCostCentreRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Section Cost Centre
        /// </summary>
        private void DeleteSectionCostCentre(SectionCostcentre item)
        {
            sectionCostCentreRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Section Ink Coverage
        /// </summary>
        private SectionInkCoverage CreateSectionInkCoverage()
        {
            SectionInkCoverage itemTarget = sectionInkCoverageRepository.Create();
            sectionInkCoverageRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Section Ink Coverage
        /// </summary>
        private void DeleteSectionInkCoverage(SectionInkCoverage item)
        {
            sectionInkCoverageRepository.Delete(item);
        }



        /// <summary>
        /// Creates New Section Cost Centre Detail
        /// </summary>
        private SectionCostCentreDetail CreateSectionCostCentreDetail()
        {
            SectionCostCentreDetail itemTarget = sectionCostCentreDetailRepository.Create();
            sectionCostCentreDetailRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Creates New Invoice Detail
        /// </summary>
        private InvoiceDetail CreateInvoiceDetail()
        {
            InvoiceDetail itemTarget = invoiceDetailRepository.Create();
            invoiceDetailRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Section Cost Centre Detail
        /// </summary>
        private void DeleteSectionCostCentreDetail(SectionCostCentreDetail item)
        {
            sectionCostCentreDetailRepository.Delete(item);
        }

        /// <summary>
        /// Saves Image to File System
        /// </summary>
        /// <param name="mapPath">File System Path for Item</param>
        /// <param name="existingImage">Existing File if any</param>
        /// <param name="caption">Unique file caption e.g. ItemId + ItemProductCode + ItemProductName + "_thumbnail_"</param>
        /// <param name="fileName">Name of file being saved</param>
        /// <param name="fileSource">Base64 representation of file being saved</param>
        /// <param name="fileSourceBytes">Byte[] representation of file being saved</param>
        /// <returns>Path of File being saved</returns>
        private string SaveImage(string mapPath, string existingImage, string caption, string fileName,
            string fileSource, byte[] fileSourceBytes)
        {
            if (!string.IsNullOrEmpty(fileSource))
            {
                // Look if file already exists then replace it
                if (!string.IsNullOrEmpty(existingImage))
                {
                    if (Path.IsPathRooted(existingImage))
                    {
                        if (File.Exists(existingImage))
                        {
                            // Remove Existing File
                            File.Delete(existingImage);
                        }
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath("~/" + existingImage);
                        if (File.Exists(filePath))
                        {
                            // Remove Existing File
                            File.Delete(filePath);
                        }
                    }

                }

                // First Time Upload
                string imageurl = mapPath + "\\" + caption + fileName;
                File.WriteAllBytes(imageurl, fileSourceBytes);

                int indexOf = imageurl.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                imageurl = imageurl.Substring(indexOf, imageurl.Length - indexOf);
                return imageurl;
            }

            return null;
        }

        /// <summary>
        /// Save Item Attachments
        /// </summary>
        private void SaveItemAttachments(Invoice invoice)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(mpcContentPath + "/Attachments/" + itemRepository.OrganisationId + "/" + invoice.CompanyId + "/Invoices/");

            if (invoice.Items == null)
            {
                return;
            }

            foreach (Item item in invoice.Items)
            {
                string attachmentMapPath = mapPath + item.ItemId;
                DirectoryInfo directoryInfo = null;
                // Create directory if not there
                if (!Directory.Exists(attachmentMapPath))
                {
                    directoryInfo = Directory.CreateDirectory(attachmentMapPath);
                }

                if (item.ItemAttachments == null)
                {
                    continue;
                }

                foreach (ItemAttachment itemAttachment in item.ItemAttachments)
                {
                    string folderPath = directoryInfo != null ? directoryInfo.FullName : attachmentMapPath;
                    int indexOf = folderPath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                    folderPath = folderPath.Substring(indexOf, folderPath.Length - indexOf);
                    itemAttachment.FolderPath = folderPath;
                    if (SaveImage(attachmentMapPath, itemAttachment.FolderPath, "",
                        itemAttachment.FileName,
                        itemAttachment.FileSource, itemAttachment.FileSourceBytes) != null)
                    {
                        itemAttachment.FileName = itemAttachment.FileName;
                    }
                }
            }
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InvoicesService(IInvoiceRepository invoiceRepository, ICostCentreRepository costCentreRepository, IItemRepository itemRepository,
            IPrefixRepository prefixRepository, IItemAttachmentRepository itemAttachmentRepository, IItemSectionRepository itemsectionRepository,
            ISectionCostCentreRepository sectionCostCentreRepository, ISectionInkCoverageRepository sectionInkCoverageRepository,
            ISectionCostCentreDetailRepository sectionCostCentreDetailRepository, IInvoiceDetailRepository invoiceDetailRepository, IOrganisationRepository organisationRepository)
        {
            this.invoiceRepository = invoiceRepository;
            CostCentreRepository = costCentreRepository;
            this.itemRepository = itemRepository;
            this.prefixRepository = prefixRepository;
            this.itemAttachmentRepository = itemAttachmentRepository;
            this.itemsectionRepository = itemsectionRepository;
            this.sectionCostCentreRepository = sectionCostCentreRepository;
            this.sectionInkCoverageRepository = sectionInkCoverageRepository;
            this.sectionInkCoverageRepository = sectionInkCoverageRepository;
            this.sectionCostCentreDetailRepository = sectionCostCentreDetailRepository;
            this.invoiceDetailRepository = invoiceDetailRepository;
            this.organisationRepository = organisationRepository;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Invoices list
        /// </summary>
        public InvoiceRequestResponseModel GetInvoicesList(InvoicesRequestModel request)
        {
            return invoiceRepository.GetInvoicesList(request);
        }

        public InvoiceRequestResponseModel SearchInvoices(GetInvoicesRequestModel request)
        {
            return invoiceRepository.SearchInvoices(request);
        }
        public InvoiceBaseResponse GetInvoiceBaseResponse()
        {
            var response = invoiceRepository.GetInvoiceBaseResponse();
            return new InvoiceBaseResponse
            {
                SystemUsers = response.SystemUsers,
                SectionFlags = response.SectionFlags,
                CurrencySymbol = response.CurrencySymbol,
                LoggedInUserId = invoiceRepository.LoggedInUserId,
                CostCenters = CostCentreRepository.GetAllCompanyCentersForOrderItem()
            };
        }

        public Invoice GetInvoiceById(long Id)
        {
            return invoiceRepository.GetInvoiceById(Id);
        }

        public Invoice SaveInvoice(Invoice request)
        {
            request.OrganisationId = invoiceRepository.OrganisationId;
            if (request.InvoiceId > 0)
            {
                return UpdateInvoice(request);
            }
            else
            {
                return SaveNewInvoice(request);
            }


        }
        private Invoice UpdateInvoice(Invoice invoice)
        {
            Invoice oInvoice = invoiceRepository.Find(invoice.InvoiceId);
            UpdateInvoice(invoice, oInvoice);

            // Update Invoice
            invoice.UpdateTo(oInvoice, new InvoiceMapperActions
            {
                CreateItem = CreateItem,
                DeleteItem = DeleteItem,
                CreateItemSection = CreateItemSection,
                DeleteItemSection = DeleteItemSection,
                CreateSectionCostCentre = CreateSectionCostCentre,
                DeleteSectionCostCenter = DeleteSectionCostCentre,
                CreateItemAttachment = CreateItemAttachment,
                DeleteItemAttachment = DeleteItemAttachment,
                CreateSectionInkCoverage = CreateSectionInkCoverage,
                DeleteSectionInkCoverage = DeleteSectionInkCoverage,
                CreateSectionCostCenterDetail = CreateSectionCostCentreDetail,
                DeleteSectionCostCenterDetail = DeleteSectionCostCentreDetail,
                CreateInvoiceDetail = CreateInvoiceDetail,
            });

            // Save Changes
            invoiceRepository.SaveChanges();
            // Save Item Attachments
            SaveItemAttachments(oInvoice);

            // Save Changes
            invoiceRepository.SaveChanges();
            return oInvoice;
        }


        private Invoice SaveNewInvoice(Invoice invoice)
        {
            string invoiceCode = prefixRepository.GetNextInvoiceCodePrefix();
            invoice.InvoiceCode = invoiceCode;
            invoice.InvoiceType = 0;
            invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();

            // Save Item Attachments
            SaveItemAttachments(invoice);

            // Save Changes
            invoiceRepository.SaveChanges();
            return invoice;
        }


        private void UpdateInvoice(Invoice source, Invoice target)
        {
            target.CompanyId = source.CompanyId;
            target.ContactId = source.ContactId;
            target.AddressId = source.AddressId;
            target.InvoiceCode = source.InvoiceCode;
            target.InvoiceName = source.InvoiceName;
            target.IsArchive = source.IsArchive;
            target.InvoiceDate = source.InvoiceDate;
            target.InvoiceStatus = source.InvoiceStatus;
            target.InvoiceTotal = source.InvoiceTotal;
            target.FlagID = source.FlagID;
            target.InvoiceType = source.InvoiceType;
            target.GrandTotal = source.GrandTotal;
            target.OrderNo = source.OrderNo;
            target.AccountNumber = source.AccountNumber;
            target.ReportSignedBy = source.ReportSignedBy;
            target.InvoicePostedBy = source.InvoicePostedBy;
            target.HeadNotes = source.HeadNotes;
            target.FootNotes = source.FootNotes;
        }

        public List<ZapierInvoiceDetail> GetZapierInvoiceDetail()
        {
            var invDetails = invoiceRepository.GetZapierInvoiceDetails();
            //invDetails.ForEach(c => c.CurrencyCode = organisationRepository.GetOrganizatiobByID().Currency.CurrencyCode);
            return invDetails;
        }
        #endregion
    }
}
