using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.DomainModels;
using Newtonsoft.Json;

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
                    }

                }
            }
            
            
            
            
            
            
            
           // var org = organisationRepository.GetOrganizatiobByID();
            //string sPostUrl = string.Empty;
            //sPostUrl = org.IsZapierEnable == true ? org.CreateInvoiceZapTargetUrl : string.Empty;
            //if (!string.IsNullOrEmpty(sPostUrl))
            //{
            //    var resp = GetZapierInvoiceDetail(invoiceId);
            //    string sData = JsonConvert.SerializeObject(resp, Formatting.None);
            //    var request = System.Net.WebRequest.Create(sPostUrl);
            //    request.ContentType = "application/json";
            //    request.Method = "POST";
            //    byte[] byteArray = Encoding.UTF8.GetBytes(sData);
            //    request.ContentLength = byteArray.Length;
            //    using (Stream dataStream = request.GetRequestStream())
            //    {
            //        dataStream.Write(byteArray, 0, byteArray.Length);
            //        var response = request.GetResponse();
            //    }
            //}


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
            long OrganisationId = 0;


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
            FileHeader.Add("StoreName");
            FileHeader.Add("AddressName");

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


            if (exportInvData != null && exportInvData.Count() > 0)
            {
                foreach (var invRec in exportInvData)
                {
                    string cdata = string.Empty;
                    if (invRec.AccountNumber != null)
                    {
                        CustomerRef = invRec.AccountNumber;
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




                    cdata = CustomerRef + "," + TxnDate + "," + InvocieID + "," + DueDate + "," + ItemRef + "," + Description + "," + AmountIncTax + "," + Qty + "," + UnitPrice + "," +
                        Status + "," + StoreName + "," + AddressName + " " + Address1  +
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
           
            return FileHeader;

        }
        #endregion

    }
}
