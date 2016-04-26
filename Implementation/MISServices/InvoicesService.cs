using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using MailChimp.Types;
using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Implementation.XeroIntegration;
using MPC.Interfaces.Common;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.DomainModels;
using Newtonsoft.Json;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;
using Xero.Api.Example.Applications;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;
using xeroHttp = Xero.Api.Infrastructure.Http;
using Xero.Api.Core;
using Address = MPC.Models.DomainModels.Address;
using Contact = Xero.Api.Core.Model.Contact;
using File = System.IO.File;
using Invoice = MPC.Models.DomainModels.Invoice;
using Item = MPC.Models.DomainModels.Item;
using Organisation = MPC.Models.DomainModels.Organisation;

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
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly ICountryRepository countryRepostiry;
        private readonly IStateRepository stateRepository;
        private IMvcAuthenticator _authenticator;
        private ApiUser _user;
        private readonly IAuthenticator _auth;
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
            ISectionCostCentreDetailRepository sectionCostCentreDetailRepository, IInvoiceDetailRepository invoiceDetailRepository, IOrganisationRepository organisationRepository,
            ICompanyContactRepository companyContactRepository, ICountryRepository countryRepository, IStateRepository stateRepository)
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
            countryRepostiry = countryRepository;
            this.companyContactRepository = companyContactRepository;
            this.stateRepository = stateRepository;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Invoices list
        /// </summary>
        public InvoiceRequestResponseModel GetInvoicesList(InvoicesRequestModel request)
        {
            Organisation org = organisationRepository.GetOrganizatiobByID();
           var result = invoiceRepository.GetInvoicesList(request);
            result.HeadNote = org != null ? org.InvoiceHeadNote : "";
            result.FootNote = org != null ? org.InvoiceFootNote : "";
            return result;
        }

        public InvoiceRequestResponseModel SearchInvoices(GetInvoicesRequestModel request)
        {
            Organisation org = organisationRepository.GetOrganizatiobByID();
            var result = invoiceRepository.SearchInvoices(request);
            result.HeadNote = org != null ? org.InvoiceHeadNote : "";
            result.FootNote = org != null ? org.InvoiceFootNote : "";
            return result;
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
                Invoice returnInvoice = UpdateInvoice(request);
                if (returnInvoice.InvoiceStatus == Convert.ToInt16(InvoiceStatuses.Posted))
                {
                    PostDataToZapier(returnInvoice.InvoiceId);

                    string sXeroUrl = CheckForXeroPosting(returnInvoice.InvoiceId);
                    if (!string.IsNullOrEmpty(sXeroUrl))
                        returnInvoice.XeroPostUrl = sXeroUrl;
                }
                return returnInvoice;
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
            invoice.CreationDate = DateTime.Now;
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
            target.HeadNotes = source.HeadNotes;
            target.FootNotes = source.FootNotes;
            if (source.InvoiceStatus == (short) InvoiceStatuses.Posted)
            {
                target.InvoicePostingDate = DateTime.Now;
                target.InvoicePostedBy = source.InvoicePostedBy;
            }
                
        }

        public List<ZapierInvoiceDetail> GetZapierInvoiceDetail(long invoiceId)
        {
            var invDetails = invoiceRepository.GetZapierInvoiceDetails(invoiceId);
            return invDetails;
        }
        public void PostDataToZapier(long invoiceId)
        {

            var org = organisationRepository.GetOrganizatiobByID();
            if (org.IsZapierEnable == true)
            {
                List<string> invoiceUrls = organisationRepository.GetZapsUrListByOrganisation(2, org.OrganisationId);
                if (invoiceUrls != null && invoiceUrls.Count > 0)
                {
                    foreach (var sPostUrl in invoiceUrls)
                    {
                        if (!string.IsNullOrEmpty(sPostUrl))
                        {
                            var resp = GetZapierInvoiceDetail(invoiceId);
                            if (resp != null)
                            {
                                var zapDetail = resp.FirstOrDefault();
                                if(zapDetail != null && zapDetail.ContactId > 0)
                                    PostInvoiceContactToZapier(zapDetail.ContactId, org.OrganisationId, resp, sPostUrl);
                            }
                            else
                            {
                                PostInvoiceToZapier(resp, sPostUrl);
                            }
                            
                        }
                    }

                }
            }
        }

        private void PostInvoiceContactToZapier(long contactId, long organisationId, List<ZapierInvoiceDetail> invResp, string invPostUrl)
        {
            List<string> contactUrls = organisationRepository.GetZapsUrListByOrganisation(1, organisationId);
            if (contactUrls != null && contactUrls.Count > 0)
            {
                foreach (var sPostUrl in contactUrls)
                {
                    if (!string.IsNullOrEmpty(sPostUrl))
                    {
                        var resp = companyContactRepository.GetStoreContactForZapier(contactId);
                        string sData = JsonConvert.SerializeObject(resp, Formatting.None);
                        var request = System.Net.WebRequest.Create(sPostUrl);
                        request.ContentType = "application/json";
                        request.Method = "POST";
                        byte[] byteArray = Encoding.UTF8.GetBytes(sData);
                        request.ContentLength = byteArray.Length;
                        using (Stream dataStream = request.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            var response = request.GetResponse();
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                string responseFromServer = reader.ReadToEnd();
                                PostInvoiceToZapier(invResp, invPostUrl);
                            }
                        }
                    }
                }

            } 
        }

        private void PostInvoiceToZapier(List<ZapierInvoiceDetail> resp, string sPostUrl)
        {
            string sData = JsonConvert.SerializeObject(resp, Formatting.None);
            var request = System.Net.WebRequest.Create(sPostUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(sData);
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                var response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();
                }
            }
        }
        public List<ZapierInvoiceDetail> GetInvoiceDetailForZapierPooling(long organisationId)
        {
            return invoiceRepository.GetInvoiceDetailForZapierPolling(organisationId);
            
        }
        public void ArchiveInvoice(int InvoiceId)
        {
            invoiceRepository.ArchiveInvoice(InvoiceId);
        }
        public void UpdateInvoiceFromZapier(ZapierInvoiceDetail zapInvoice, long organisationId)
        {
            
            try
            {
                Invoice invoice = invoiceRepository.GetInvoiceByCode(zapInvoice.InvoiceCode,organisationId);
                if (invoice != null)
                {
                    invoice.GrandTotal = zapInvoice.InvoiceTotal;
                    invoice.TaxValue = zapInvoice.InvoiceTaxTotal;
                    invoice.InvoiceStatus = zapInvoice.InvoiceStatus == "SUBMITTED"? (short)InvoiceStatuses.Posted : (short)InvoiceStatuses.Awaiting;
                    invoice.InvoiceTotal = zapInvoice.InvoiceSubTotal;
                    invoiceRepository.Update(invoice);
                    invoiceRepository.SaveChanges();
                }
                else
                {
                    CompanyContact companyContact =
                        companyContactRepository.GetCompanyContactByNameAndEmail(zapInvoice.ContactFirstName,
                            zapInvoice.ContactEmail, organisationId);
                    
                    Invoice itemTarget = invoiceRepository.Create();
                    if (!string.IsNullOrEmpty(zapInvoice.InvoiceCode))
                    {
                        itemTarget.InvoiceCode = zapInvoice.InvoiceCode;
                    }
                    else
                    {
                        Prefix nextPrefix = prefixRepository.GetPrefixByOrganisationId(organisationId);
                        string invoiceCode = nextPrefix != null
                            ? nextPrefix.InvoicePrefix + nextPrefix.InvoiceNext
                            : string.Empty;
                        itemTarget.InvoiceCode = invoiceCode;
                    }
                    itemTarget.OrganisationId = organisationId;
                    itemTarget.InvoiceName = "Invoice For " +zapInvoice.ContactFirstName;
                    itemTarget.InvoiceType = 1;
                    itemTarget.FlagID = Convert.ToInt32(invoiceRepository.GetInvoieFlag());
                    itemTarget.InvoiceStatus = zapInvoice.InvoiceStatus == "SUBMITTED" ? (short)InvoiceStatuses.Posted : (short)InvoiceStatuses.Awaiting;
                    
                    itemTarget.InvoiceTotal = zapInvoice.InvoiceSubTotal;
                    itemTarget.GrandTotal = zapInvoice.InvoiceTotal;
                    itemTarget.TaxValue = zapInvoice.InvoiceTaxTotal;
                    if (zapInvoice.InvoiceItems != null && zapInvoice.InvoiceItems.Count > 0)
                    {
                        itemTarget.InvoiceDetails = new Collection<InvoiceDetail>();
                        foreach (var invoiceItem in zapInvoice.InvoiceItems)
                        {
                            InvoiceDetail invoiceDetail = new InvoiceDetail
                            {
                                InvoiceTitle = invoiceItem.ProductName,
                                ItemCharge = invoiceItem.NetTotal,
                                TaxValue = invoiceItem.TaxValue,
                                ItemGrossTotal = invoiceItem.GrossTotal,
                                Quantity = invoiceItem.Quantity,
                                Description = invoiceItem.ProductCode + " " + invoiceItem.ProductDescription
                            };
                            
                            itemTarget.InvoiceDetails.Add(invoiceDetail);
                        }
                    }


                    if (companyContact != null)
                    {
                        itemTarget.Company = companyContact.Company;
                        itemTarget.CompanyContact = companyContact;
                        itemTarget.AddressId = Convert.ToInt32(companyContact.AddressId);
                    }
                    else
                    {
                        Company newCompany = new Company
                        {
                            Name = zapInvoice.CustomerName,
                            URL = zapInvoice.CustomerUrl,
                            TaxRate = zapInvoice.TaxRate,
                            AccountBalance = 0,
                            AccountNumber = zapInvoice.CustomerAccountNumber,
                            CreationDate = DateTime.Now,
                            CreditLimit = 0,
                            OrganisationId = organisationId,
                            TypeId = 57,
                            DefaultNominalCode = 0,
                            Status = 0,
                            IsDisabled = 0,
                            AccountOpenDate = DateTime.Now,
                            VATRegNumber = zapInvoice.VatNumber
                            

                        };
                        if (zapInvoice.IsCustomer)
                        {
                            newCompany.StoreId = companyContactRepository.GetRetailStoreId(organisationId);
                            newCompany.IsCustomer = 1;
                        }
                        else if (zapInvoice.IsSupplier)
                            newCompany.IsCustomer = 2;
                        else
                        {
                            newCompany.StoreId = companyContactRepository.GetRetailStoreId(organisationId);
                            newCompany.IsCustomer = 0;
                        }
                        CompanyTerritory newTerritory = new CompanyTerritory
                        {
                            TerritoryCode = "DFT",
                            TerritoryName = "Default Territory",
                            Company = newCompany,
                            isDefault = true
                        };
                        Address newAddress = new Address
                        {
                            AddressName = zapInvoice.AddressName,
                            Address1 = zapInvoice.Address1,
                            Address2 = zapInvoice.Address2,
                            PostCode = zapInvoice.AddressPostalCode,
                            City = zapInvoice.AddressCity,
                            Country = countryRepostiry.GetCountryByName(zapInvoice.AddressCountry),
                            State = stateRepository.GetStateByName(zapInvoice.AddressState),
                            Company = newCompany,
                            CompanyTerritory = newTerritory,
                            isDefaultTerrorityBilling = true,
                            isDefaultTerrorityShipping = true,
                            IsDefaultAddress = true,
                            isArchived = false,
                            OrganisationId = organisationId,
                            IsDefaultShippingAddress = true
                        };
                        companyContact = new CompanyContact
                        {
                            FirstName = zapInvoice.ContactFirstName,
                            LastName = zapInvoice.ContactLastName,
                            Email = zapInvoice.ContactEmail,
                            HomeTel1 = zapInvoice.ContactPhone,
                            Mobile = zapInvoice.ContactMobile,
                            IsDefaultContact = 1,
                            isArchived = false,
                            Password = HashingManager.ComputeHashSHA1("password"),
                            isWebAccess = true,
                            canPlaceDirectOrder = false,
                            canUserPlaceOrderWithoutApproval = false,
                            isPlaceOrder = true,
                            SkypeId = zapInvoice.ContactSkypUserName,
                            OrganisationId = organisationId,
                            Company = newCompany,
                            Address = newAddress,
                            CompanyTerritory = newTerritory,
                        };
                        itemTarget.Company = companyContact.Company;
                        itemTarget.CompanyContact = companyContact;
                        itemTarget.AddressId = Convert.ToInt32(companyContact.AddressId);
                        
                    }

                    invoiceRepository.Add(itemTarget);
                    invoiceRepository.SaveChanges();
                    //companyContactRepository.Add(companyContact);
                    //companyContactRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new MPCException("Unable to Process Invoice data from Zapier", invoiceRepository.OrganisationId);
            }


        }

        public string ExportInvocie(long InvocieId)
        {
            List<string> FileHeader = new List<string>();
            long OrganisationId = invoiceRepository.OrganisationId;


            FileHeader = HeaderList(FileHeader, true);
            List<usp_ExportInvoice_Result> exportInvData = invoiceRepository.GetInvoiceDataForExport(InvocieId);




            StringBuilder PSV = new StringBuilder();
            StringBuilder CSV = new StringBuilder();
            string csv = string.Empty;
            string psv = string.Empty;

          

            foreach (string column in FileHeader)
            {
                //Add the Header row for CSV file.
                csv += column + ',';
            }

            //Add new line.
            csv += "\r\n";
            CSV.Append(csv);


        
            string CustomerRef = string.Empty;
            string TxnDate = string.Empty;
            string InvocieID = string.Empty;
            string DueDate = string.Empty;
            string ItemRef = string.Empty;

            string Description = string.Empty;
            string AmountIncTax = string.Empty;
            string Qty = string.Empty;
           
            string UnitPrice = string.Empty;
            string Status = string.Empty;
            string StoreName = string.Empty;
            string AddressName = string.Empty;
            string Address1 = string.Empty;
            string InvoiceDescription = string.Empty;


            if (exportInvData != null && exportInvData.Count() > 0)
            {
                foreach (var invRec in exportInvData)
                {
                    string cdata = string.Empty;
                    if (invRec.OrderNo != null)
                    {
                        CustomerRef = invRec.OrderNo;
                    }

                    if (invRec.CreationDate != null)
                    {
                        TxnDate = Convert.ToString(invRec.CreationDate);
                    }

                    if (!string.IsNullOrEmpty(invRec.InvoiceCode))
                        InvocieID = invRec.InvoiceCode;

                    if (invRec.InvoiceDate != null)
                    {
                        DueDate = Convert.ToString(invRec.InvoiceDate);
                    }
                    if (!string.IsNullOrEmpty(invRec.ProductCode))
                        ItemRef = invRec.ProductCode;

                    if (!string.IsNullOrEmpty(invRec.ProductName))
                        Description = invRec.ProductName;

                    if (invRec.Qty1NetTotal != null)
                    {
                        AmountIncTax = Convert.ToString(invRec.Qty1NetTotal);
                    }
                    if (invRec.Qty1 != null)
                    {
                        Qty = Convert.ToString(invRec.Qty1);
                    }
                    if (invRec.UnitPrice != null)
                    {
                        UnitPrice = Convert.ToString(invRec.UnitPrice);
                    }

                    if (invRec.StatusName != null)
                    {
                        Status = Convert.ToString(invRec.StatusName);
                    }

                    if (invRec.Name != null)
                        StoreName = Convert.ToString(invRec.Name);


                    if (invRec.AddressName != null)
                        AddressName = Convert.ToString(invRec.AddressName);

                    if (invRec.BAddress1 != null)
                        Address1 = Convert.ToString(invRec.BAddress1);

                    if (invRec.InvoiceDescription != null)
                        InvoiceDescription = Convert.ToString(invRec.InvoiceDescription.Replace("\n", " "));




                    cdata = CustomerRef + "," + TxnDate + "," + InvocieID + "," + DueDate + "," + ItemRef + "," + Description + "," + AmountIncTax + "," + Qty + "," + UnitPrice + "," +
                        Status + "," + StoreName + "," + AddressName + " " + Address1  + "," + InvoiceDescription +
                                      "\r\n";


                    CSV.Append(cdata);

                }
            }


            string PSVSavePath = string.Empty;
            string PReturnPath = string.Empty;

            string CSVSavePath = string.Empty;
            string CReturnPath = string.Empty;

            if (OrganisationId > 0)
            {
                //PSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + OrganisationId + "/" + OrganisationId + "_CompanyContactsPipeSeperated.csv");
                //PReturnPath = "/MPC_Content/Reports/" + OrganisationId + "/" + OrganisationId + "_CompanyItems.csv";

                CSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + OrganisationId + "/" + OrganisationId + "_InvoiceDetail.csv");
                CReturnPath = "/MPC_Content/Reports/" + OrganisationId + "/" + OrganisationId + "_InvoiceDetail.csv";



            }
            else
            {
                //PSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + OrganisationId + "_CompanyContactsPipeSeperated.csv");
                //PReturnPath = "/MPC_Content/Reports/" + OrganisationId + "_CompanyItems.csv";

                CSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + OrganisationId + "_InvoiceDetail.csv");
                CReturnPath = "/MPC_Content/Reports/" + OrganisationId + "_InvoiceDetail.csv";

            }

            string DirectoryPath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + OrganisationId);
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }


            //StreamWriter sw = new StreamWriter(PSVSavePath, false, Encoding.UTF8);
            //sw.Write(PSV);
            //sw.Close();

            StreamWriter csw = new StreamWriter(CSVSavePath, false, Encoding.UTF8);
            csw.Write(CSV);
            csw.Close();


            return CReturnPath;
          


        }
        public List<string> HeaderList(List<string> FileHeader, bool isFromCRM)
        {

            FileHeader.Add("CustomerRef");
            FileHeader.Add("TxnDate");
            FileHeader.Add("InvocieID");
            FileHeader.Add("DueDate");
            FileHeader.Add("ItemRef");
            FileHeader.Add("Description");
            FileHeader.Add("AmountIncTax");

            FileHeader.Add("Qty");
            FileHeader.Add("UnitPrice");
            FileHeader.Add("Status");
            FileHeader.Add("CustomerName");
            FileHeader.Add("Address");
            FileHeader.Add("Invoice Description");
           
            return FileHeader;

        }

        public string PostInvoiceToXero(string authToken, string verifier, string organisation)
        {
            _user = XeroApiHelper.User();
            _authenticator = XeroApiHelper.MvcAuthenticator();
            var orgForInvoiceId = organisationRepository.GetOrganizatiobByID();
            organisationRepository.UpdateOrganisationForXeroPosting(_user.Name, authToken,
                verifier, organisation, orgForInvoiceId.PostedInvoiceId?? 0);
            if (_authenticator != null)
            {
                var accessToken = _authenticator.RetrieveAndStoreAccessToken(_user.Name, authToken, verifier, organisation);
                PostInvoiceToXero(orgForInvoiceId.PostedInvoiceId ?? 0);
            }
           
            return "Invoice posted to Xero successfully.";
        }

        private string CheckForXeroPosting(long invoiceId)
        {
            string authorizeUrl = string.Empty;
            Organisation org = organisationRepository.GetOrganizatiobByID();
            
            if (org.IsXeroActive == true && !string.IsNullOrEmpty(org.XeroConsumerKey) && !string.IsNullOrEmpty(org.XeroConsumerSecret))
            {
                var publicConsumer = new Consumer(org.XeroConsumerKey, org.XeroConsumerSecret);
                if (!string.IsNullOrEmpty(org.XeroAuthToken) && !string.IsNullOrEmpty(org.XeroAutVerifier) && XeroCommon.Authenticator != null)
                {
                    XeroApiHelper.GetApplicationSettings(publicConsumer, XeroCommon.Authenticator);
                    _authenticator = (IMvcAuthenticator)XeroCommon.Authenticator;
                    var accessToken = _authenticator.RetrieveAndStoreAccessToken(org.XeroUserName, org.XeroAuthToken, org.XeroAutVerifier, org.XeroOrganisationCode);
                    if (accessToken != null)
                    {
                        PostInvoiceToXero(invoiceId);
                        return string.Empty;
                    }
                    else
                    {
                        return GetAccessUrl(org, invoiceId);
                    }
                    
                }
                authorizeUrl = GetAccessUrl(org, invoiceId);
                //if (!string.IsNullOrEmpty(org.XeroAuthToken) && !string.IsNullOrEmpty(org.XeroAutVerifier))
                //{
                //    var accessToken = _authenticator.RetrieveAndStoreAccessToken(org.XeroUserName, org.XeroAuthToken, org.XeroAutVerifier, org.XeroOrganisationCode);
                //    if (accessToken != null)
                //    {
                //        PostInvoiceToXero(invoiceId);
                //    }
                //    else
                //    {
                //        organisationRepository.UpdateOrganisationForXeroPosting(_user.Name, string.Empty,
                //            string.Empty, string.Empty, invoiceId);
                //        authorizeUrl = _authenticator.GetRequestTokenAuthorizeUrl(_user.Name);
                //    }

                //}
                //else
                //{
                //    organisationRepository.UpdateOrganisationForXeroPosting(_user.Name, string.Empty,
                //        string.Empty, string.Empty, invoiceId);
                //    authorizeUrl = _authenticator.GetRequestTokenAuthorizeUrl(_user.Name);
                //}
            }
            
           
            return authorizeUrl;
        }

        private string GetAccessUrl(Organisation org, long invoiceId)
        {
            string authorizeUrl = string.Empty;
            var baseApiUrl = "https://api.xero.com";
            var publicConsumer = new Consumer(org.XeroConsumerKey, org.XeroConsumerSecret);
            var memoryStore = new MemoryAccessTokenStore();
            var requestTokenStore = new MemoryRequestTokenStore();
            var publicAuthenticator = new PublicMvcAuthenticator(baseApiUrl, baseApiUrl, org.XeroCallbackUrl, memoryStore,
                publicConsumer, requestTokenStore);

            _user = XeroApiHelper.User();
            XeroApiHelper.GetApplicationSettings(publicConsumer, publicAuthenticator);
            _authenticator = (IMvcAuthenticator)publicAuthenticator;
            XeroCommon.Authenticator = publicAuthenticator;

            organisationRepository.UpdateOrganisationForXeroPosting(_user.Name, string.Empty,
                    string.Empty, string.Empty, invoiceId);
            authorizeUrl = _authenticator.GetRequestTokenAuthorizeUrl(_user.Name);
            return authorizeUrl;
        }
        public string PostInvoiceToXero(long invoiceId)
        {
            Invoice invoiceToPost = GetInvoiceById(invoiceId);
            var mpcContact = invoiceToPost.CompanyContact;

          var api = XeroApiHelper.CoreApi();
            var invoices = api.Invoices.UseFourDecimalPlaces(true);

            List<ContactPerson> contactPersons = new List<ContactPerson>();
            ContactPerson person = new ContactPerson
            {
                FirstName = mpcContact != null ? mpcContact.FirstName : string.Empty,
                LastName = mpcContact != null ? mpcContact.LastName : string.Empty,
                EmailAddress = mpcContact != null ? mpcContact.Email : string.Empty
            };

            contactPersons.Add(person);
            
            Contact xeroContact = new Contact();
            xeroContact.FirstName = mpcContact != null ? mpcContact.FirstName : string.Empty;
            xeroContact.LastName = mpcContact != null
                ? mpcContact.LastName
                : string.Empty;
            xeroContact.ContactNumber = mpcContact != null
                ? mpcContact.Mobile
                : string.Empty;
            xeroContact.EmailAddress = mpcContact != null ? mpcContact.Email : string.Empty;
            xeroContact.Name = mpcContact != null ? mpcContact.FirstName + " " + mpcContact.LastName : string.Empty;
            xeroContact.ContactPersons = contactPersons;

            var mpcAddress = mpcContact.Company.Addresses.FirstOrDefault(a => a.AddressId == invoiceToPost.AddressId);
            Xero.Api.Core.Model.Address xeroAddress = new Xero.Api.Core.Model.Address();
            xeroAddress.AddressLine1 = mpcAddress != null ? mpcAddress.Address1 : string.Empty;
            xeroAddress.AddressLine2 = mpcAddress != null ? mpcAddress.Address2 : string.Empty;
            xeroAddress.AddressLine3 = mpcAddress != null ? mpcAddress.Address3 : string.Empty;
            xeroAddress.City = mpcAddress != null ? mpcAddress.City : string.Empty;
            xeroAddress.Country = mpcAddress != null
                ? mpcAddress.Country != null ? mpcAddress.Country.CountryName : string.Empty
                : string.Empty;
            xeroAddress.Region = mpcAddress != null
                ? mpcAddress.State != null ? mpcAddress.State.StateName : string.Empty
                : string.Empty;
            xeroAddress.AddressType = AddressType.PostOfficeBox;
            xeroAddress.PostalCode = mpcAddress != null ? mpcAddress.PostCode : string.Empty;

            List<Xero.Api.Core.Model.Address> xeroAddresses = new List<Xero.Api.Core.Model.Address>();
            xeroAddresses.Add(xeroAddress);
            xeroContact.Addresses = xeroAddresses;

            List<LineItem> invItems = new List<LineItem>();

            if (invoiceToPost.InvoiceDetails != null && invoiceToPost.InvoiceDetails.Count > 0)
            {
                invoiceToPost.InvoiceDetails.ToList()
                    .ForEach(id => invItems.Add(new LineItem
                    {
                        ItemCode = string.Empty,
                        Description = !string.IsNullOrEmpty(id.InvoiceTitle) ? id.InvoiceTitle : "N/A",
                        Quantity = Convert.ToDecimal(id.Quantity),
                        LineAmount = Convert.ToDecimal(id.ItemCharge),
                        UnitAmount = Convert.ToDecimal(((id.ItemCharge) / (id.Quantity))),
                        AccountCode = "SALES",
                        TaxType = "OUTPUT"
                    }));
            }
            if (invoiceToPost.Items != null && invoiceToPost.Items.Count > 0)
            {
                invoiceToPost.Items.ToList()
                    .ForEach(p => invItems.Add(new LineItem
                    {
                        Description = !string.IsNullOrEmpty(p.ProductName) ? p.ProductName : "N/A",
                        Quantity = Convert.ToDecimal(p.Qty1?? 1),
                        LineAmount = Convert.ToDecimal(p.Qty1NetTotal),
                        UnitAmount = Convert.ToDecimal(((p.Qty1NetTotal ?? 0) / (p.Qty1 ?? 1))),
                        AccountCode = "SALES",
                        TaxType = "OUTPUT"
                    }));
            }

           

            Xero.Api.Core.Model.Invoice xeroInvoice = new Xero.Api.Core.Model.Invoice();
            xeroInvoice.Number = invoiceToPost.InvoiceCode;
           // xeroInvoice.Total = Convert.ToDecimal(invoiceToPost.InvoiceTotal);
            xeroInvoice.SubTotal = Convert.ToDecimal(invoiceToPost.InvoiceTotal);
            xeroInvoice.TotalTax = Convert.ToDecimal(invoiceToPost.TaxValue?? 0);
            xeroInvoice.Date = invoiceToPost.InvoiceDate;
            xeroInvoice.DueDate = Convert.ToDateTime(invoiceToPost.InvoiceDate).AddDays(1);
            xeroInvoice.LineItems = invItems;
            xeroInvoice.Type = InvoiceType.AccountsReceivable;
            xeroInvoice.Status = InvoiceStatus.Draft;
            xeroInvoice.Reference = invoiceToPost.OrderNo;
            xeroInvoice.Contact = xeroContact;
            invoices.Update(xeroInvoice);


            return "Invoice Posted to Xero successfully.";
        }
        #endregion

    }
    
}
