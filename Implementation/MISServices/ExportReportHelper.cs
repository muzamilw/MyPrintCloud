using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export.Pdf.Section;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace MPC.Implementation.MISServices
{
    public class ExportReportHelper : IExportReportHelper
    {

        #region Private

        private readonly IOrganisationRepository organisationRepository;
        private readonly IReportRepository ReportRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICurrencyRepository CurrencyRepository;
        private readonly IPrePaymentRepository prePaymentRepository;
        private readonly IPipeLineSourceRepository pipeLineSourceRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IMachineRepository MachineRepository;
        private readonly IPaperSizeRepository paperSizeRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly ICostCentreRepository CostCentreRepository;
        private readonly IMarkupRepository _markupRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IPayPalResponseRepository PayPalRepsoitory;
        #endregion

        public ExportReportHelper(IOrganisationRepository organisationRepository,IReportRepository ReportRepository,IOrderRepository orderRepository, ICurrencyRepository CurrencyRepository,
            IPrePaymentRepository prePaymentRepository,IPipeLineSourceRepository pipeLineSourceRepository,ISectionFlagRepository sectionFlagRepository,IAddressRepository addressRepository,
            IMachineRepository MachineRepository, IPaperSizeRepository paperSizeRepository,IStockItemRepository stockItemRepository,ICostCentreRepository CostCentreRepository,IMarkupRepository markupRepository,
            ICompanyRepository companyRepository,IPayPalResponseRepository PayPalRepsoitory)
        {
           
            if (organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }
            if (ReportRepository == null)
            {
                throw new ArgumentNullException("ReportRepository");
            }
            if (orderRepository == null)
            {
                throw new ArgumentNullException("orderRepository");
            }
            if (CurrencyRepository == null)
            {
                throw new ArgumentNullException("CurrencyRepository");
            }
            if (prePaymentRepository == null)
            {
                throw new ArgumentNullException("prePaymentRepository");
            }
            if (pipeLineSourceRepository == null)
            {
                throw new ArgumentNullException("pipeLineSourceRepository");
            }
            if (sectionFlagRepository == null)
            {
                throw new ArgumentNullException("sectionFlagRepository");
            }
            if (addressRepository == null)
            {
                throw new ArgumentNullException("addressRepository");
            }
            if (MachineRepository == null)
            {
                throw new ArgumentNullException("MachineRepository");
            }
            if (paperSizeRepository == null)
            {
                throw new ArgumentNullException("paperSizeRepository");
            }
            if (stockItemRepository == null)
            {
                throw new ArgumentNullException("stockItemRepository");
            }
            if (CostCentreRepository == null)
            {
                throw new ArgumentNullException("CostCentreRepository");
            }
            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }
            this.organisationRepository = organisationRepository;
            this.ReportRepository = ReportRepository;
            this.orderRepository = orderRepository;
            this.CurrencyRepository = CurrencyRepository;
            this.prePaymentRepository = prePaymentRepository;
            this.pipeLineSourceRepository = pipeLineSourceRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.addressRepository = addressRepository;
            this.MachineRepository = MachineRepository;
            this.paperSizeRepository = paperSizeRepository;
            this.stockItemRepository = stockItemRepository;
            this.CostCentreRepository = CostCentreRepository;
            this._markupRepository = markupRepository;
            this.companyRepository = companyRepository;
            this.PayPalRepsoitory = PayPalRepsoitory;
        }

        public string ExportPDF(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam, long WebStoreOrganisationId = 0,bool isFromExternal = true)
        {
            string sFilePath = string.Empty;
              long OrganisationID = 0;
            string InternalPath = string.Empty;
            try
            {
              
                if (WebStoreOrganisationId > 0)
                {
                    OrganisationID = WebStoreOrganisationId;
                }
                else 
                {
                    Organisation org = organisationRepository.GetOrganizatiobByID();
                    if (org != null)
                    {
                        OrganisationID = org.OrganisationId;
                    }
                }
                Report currentReport = ReportRepository.GetReportByReportID(iReportID);
                if (currentReport.ReportId > 0)
                {
                    byte[] rptBytes = null;
                    rptBytes = System.Text.Encoding.Unicode.GetBytes(currentReport.ReportTemplate);
                    // Encoding must be done
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(rptBytes);
                    // Load it to memory stream
                    ms.Position = 0;
                    SectionReport currReport = new SectionReport();
                    string sFileName = string.Empty;
                    // FileNamesList.Add(sFileName);
                    currReport.LoadLayout(ms);
                    if (type == ReportType.JobCard)
                    {
                        sFileName = iRecordID + "JobCardReport.pdf";
                        //  FileNamesList.Add(sFileName);
                        List<usp_JobCardReport_Result> rptSource = ReportRepository.getJobCardReportResult(OrganisationID, OrderID, iRecordID);
                        currReport.DataSource = rptSource;
                    }
                    else if (type == ReportType.Order)
                    {
                        sFileName = iRecordID + "OrderReport.pdf";
                        List<usp_OrderReport_Result> rptOrderSource = ReportRepository.getOrderReportResult(OrganisationID, iRecordID);
                        currReport.DataSource = rptOrderSource;
                    }
                    else if (type == ReportType.Estimate)
                    {
                        sFileName = iRecordID + "EstimateReport.pdf";
                        List<usp_EstimateReport_Result> rptEstimateSource = ReportRepository.getEstimateReportResult(OrganisationID, iRecordID);
                        currReport.DataSource = rptEstimateSource;
                    }
                    else if (type == ReportType.Invoice)
                    {
                        sFileName = iRecordID + "InvoiceReport.pdf";
                        List<usp_InvoiceReport_Result> rptInvoiceSource = ReportRepository.getInvoiceReportResult(OrganisationID, iRecordID);
                        currReport.DataSource = rptInvoiceSource;
                    }
                    else if (type == ReportType.PurchaseOrders)
                    {
                        sFileName = iRecordID + "PurchaseReport.pdf";
                        List<usp_PurchaseOrderReport_Result> rptInvoiceSource = ReportRepository.GetPOReport(iRecordID);
                        currReport.DataSource = rptInvoiceSource;
                    }
                    else if (type == ReportType.DeliveryNotes)
                    {
                        sFileName = iRecordID + "DeliveryReport.pdf";
                        List<usp_DeliveryReport_Result> rptDeliverySource = ReportRepository.GetDeliveryNoteReport(iRecordID);
                        currReport.DataSource = rptDeliverySource;
                    }
                    else if (type == ReportType.Internal)
                    {
                        string ReportDataSource = string.Empty;
                        string ReportTemplate = string.Empty;

                        sFileName = "OrderReport.pdf";
                        DataTable dataSourceList = ReportRepository.GetReportDataSourceByReportID(iReportID, CriteriaParam);
                        currReport.DataSource = dataSourceList;
                    }
                    if (currReport != null)
                    {
                        currReport.Run();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdf = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdf.ImageQuality = ImageQuality.Highest;
                        pdf.ImageResolution = 770 * 140;
                        string Path = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/");
                        if (!Directory.Exists(Path))
                        {
                            Directory.CreateDirectory(Path);
                        }
                        // PdfExport pdf = new PdfExport();
                        sFilePath = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/") + sFileName;
                        InternalPath = "/" + ImagePathConstants.ReportPath + OrganisationID + "/" + sFileName;
                        pdf.Export(currReport.Document, sFilePath);
                        ms.Close();
                        currReport.Document.Dispose();
                        pdf.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            if (isFromExternal)
                return sFilePath;
            else
                return InternalPath;
        }

        public string ExportOrderReportXML(long iRecordID, string OrderCode, string XMLFormat, long WebStoreOrganisationId = 0)
        {
            string sFilePath = string.Empty;
            bool isCorporate = false;
            string CurrencySymbol = string.Empty;

            try
            {

                long OrganisationID = 0;
                if (WebStoreOrganisationId > 0)
                {
                    OrganisationID = WebStoreOrganisationId;
                }
                else
                {
                    Organisation org = organisationRepository.GetOrganizatiobByID();
                    if (org != null)
                    {
                        OrganisationID = org.OrganisationId;
                    }
                }
                
                Estimate orderEntity = new Estimate();
                if (iRecordID > 0)
                    orderEntity = orderRepository.GetOrderByIdforXml(iRecordID);
                if (OrderCode != "")
                    orderEntity = orderRepository.GetOrderByOrderCode(OrderCode);

                //long CurrencyID = db.Organisations.Where(c => c.OrganisationId == OrganisationId).Select(c => c.CurrencyId ?? 0).FirstOrDefault();
                Currency curr = CurrencyRepository.GetCurrencySymbolByOrganisationId(OrganisationID);
                if (curr != null)
                    CurrencySymbol = curr.CurrencySymbol;
                List<PrePayment> paymentsList = prePaymentRepository.GetPrePaymentsByOrganisatioID(iRecordID);
                if (orderEntity != null)
                {

                    XmlDocument XDoc = new XmlDocument();

                    XmlNode docNode = XDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XDoc.AppendChild(docNode);
                    // Create root node.
                    XmlElement XElemRoot = XDoc.CreateElement("Order");
                    //Add the node to the document.
                    XDoc.AppendChild(XElemRoot);

                    XmlElement XTemp = XDoc.CreateElement("OrderDetail");


                    XmlAttribute OrderUserNotesAttr = XDoc.CreateAttribute("UserNotes");
                    if (!string.IsNullOrEmpty(orderEntity.UserNotes))
                        OrderUserNotesAttr.Value = orderEntity.UserNotes;
                    else
                        OrderUserNotesAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderUserNotesAttr);


                    int SID = 0;
                    if (orderEntity.SourceId != null)
                        SID = (int)orderEntity.SourceId;

                    string SourceName = pipeLineSourceRepository.GetSourceNameByID(SID);
                    XmlAttribute OrderSourceAttr = XDoc.CreateAttribute("Source");
                    if (!string.IsNullOrEmpty(SourceName))
                    {
                        OrderSourceAttr.Value = SourceName;
                    }
                    else
                    {
                        OrderSourceAttr.Value = string.Empty;
                    }
                    XTemp.SetAttributeNode(OrderSourceAttr);


                    DateTime dtCreation = new DateTime();
                    string CreationDate = string.Empty;
                    XmlAttribute OrderCreationAttr = XDoc.CreateAttribute("CreationDate");
                    if (orderEntity.CreationDate != null)
                    {
                        dtCreation = Convert.ToDateTime(orderEntity.CreationDate);
                        CreationDate = dtCreation.ToString("dd/MMM/yyyy");
                        OrderCreationAttr.Value = CreationDate;
                    }
                    else
                    {
                        OrderCreationAttr.Value = string.Empty;
                    }

                    XTemp.SetAttributeNode(OrderCreationAttr);

                    XmlAttribute OrderFooterAttr = XDoc.CreateAttribute("OrderFooter");
                    if (!string.IsNullOrEmpty(orderEntity.FootNotes))
                        OrderFooterAttr.Value = orderEntity.FootNotes;
                    else
                        OrderFooterAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderFooterAttr);

                    XmlAttribute OrderHeaderAttr = XDoc.CreateAttribute("OrderHeader");
                    if (!string.IsNullOrEmpty(orderEntity.HeadNotes))
                        OrderHeaderAttr.Value = orderEntity.HeadNotes;
                    else
                        OrderHeaderAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderHeaderAttr);

                    XmlAttribute OrderPONumbAttr = XDoc.CreateAttribute("CustomerPORef");
                    int isPO = 1;
                    string POString = string.Empty;
                    if (orderEntity.CustomerPO != null)
                    {
                        OrderPONumbAttr.Value = orderEntity.CustomerPO;
                    }
                    else
                    {
                        OrderPONumbAttr.Value = string.Empty;
                    }
                    XTemp.SetAttributeNode(OrderPONumbAttr);


                    XmlAttribute OrderCreditAppAttr = XDoc.CreateAttribute("OrderCreditApproved");
                    int isApproved = 1;
                    string ApproveString = string.Empty;
                    if (orderEntity.IsCreditApproved != null)
                    {
                        isApproved = (int)orderEntity.IsCreditApproved;
                        if (isApproved == 1)
                            ApproveString = "True";
                        else
                            ApproveString = "False";
                    }
                    OrderCreditAppAttr.Value = ApproveString;
                    XTemp.SetAttributeNode(OrderCreditAppAttr);

                    SectionFlag oFlag = sectionFlagRepository.GetSectionFlag(orderEntity.SectionFlagId);
                    XmlAttribute OrderflagAttr = XDoc.CreateAttribute("OrderFlag");
                    if (oFlag != null)
                        OrderflagAttr.Value = oFlag.FlagName;
                    else
                        OrderflagAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderflagAttr);

                    string OrderDateSta = string.Empty;
                    //if (orderEntity.Order_Date != null)
                    DateTime dtStart = new DateTime();
                    XmlAttribute OrderDateStartAttr = XDoc.CreateAttribute("StartDeliveryDate");
                    if (orderEntity.Order_Date != null)
                    {
                        dtStart = Convert.ToDateTime(orderEntity.StartDeliveryDate);
                        OrderDateSta = dtStart.ToString("dd/MMM/yyyy");
                        OrderDateStartAttr.Value = OrderDateSta;
                    }
                    else
                        OrderDateStartAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderDateStartAttr);

                    string OrderDateFin = string.Empty;
                    //if (orderEntity.Order_Date != null)
                    DateTime dtFinsih = new DateTime();
                    XmlAttribute OrderDateFinishAttr = XDoc.CreateAttribute("FinishDeliveryDate");
                    if (orderEntity.Order_Date != null)
                    {
                        dtFinsih = Convert.ToDateTime(orderEntity.FinishDeliveryDate);
                        OrderDateFin = dtFinsih.ToString("dd/MMM/yyyy");
                        OrderDateFinishAttr.Value = OrderDateFin;
                    }
                    else
                        OrderDateFinishAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderDateFinishAttr);


                    string OrderDate = string.Empty;
                    //if (orderEntity.Order_Date != null)
                    DateTime dt = new DateTime();
                    XmlAttribute OrderDateAtt = XDoc.CreateAttribute("OrderDate");
                    if (orderEntity.Order_Date != null)
                    {
                        dt = Convert.ToDateTime(orderEntity.Order_Date);
                        OrderDate = dt.ToString("dd/MMM/yyyy");
                        OrderDateAtt.Value = OrderDate;
                    }
                    else
                        OrderDateAtt.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderDateAtt);

                    XmlAttribute OrderCod = XDoc.CreateAttribute("OrderCode");
                    OrderCod.Value = orderEntity.Order_Code; ;
                    XTemp.SetAttributeNode(OrderCod);

                    XmlAttribute status = XDoc.CreateAttribute("Status");
                    status.Value = orderEntity.Status.StatusName;
                    XTemp.SetAttributeNode(status);

                    XmlAttribute Title = XDoc.CreateAttribute("Title");
                    Title.Value = orderEntity.Estimate_Name;
                    XTemp.SetAttributeNode(Title);

                    XElemRoot.AppendChild(XTemp);

                    foreach (Item items in orderEntity.Items)
                    {
                        if (items.ItemType == 2)
                        {
                            XmlAttribute deliverycost = XDoc.CreateAttribute("DeliveryCost");
                            deliverycost.Value = Convert.ToString(items.Qty1GrossTotal ?? 0);
                            XTemp.SetAttributeNode(deliverycost);
                            XElemRoot.AppendChild(XTemp);
                        }
                    }

                    XmlElement XCompanyTemp = XDoc.CreateElement("Company");

                    XmlAttribute CredAttr = XDoc.CreateAttribute("CreditReference");
                    if (!string.IsNullOrEmpty(orderEntity.Company.CreditReference))
                        CredAttr.Value = orderEntity.Company.CreditReference;
                    else
                        CredAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(CredAttr);


                    XmlAttribute CorpHomeAttr = XDoc.CreateAttribute("CorporateHomeURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.RedirectWebstoreURL))
                        CorpHomeAttr.Value = orderEntity.Company.RedirectWebstoreURL;
                    else
                        CorpHomeAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(CorpHomeAttr);

                    XmlAttribute LinkAttr = XDoc.CreateAttribute("LinkedInURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.LinkedinURL))
                        LinkAttr.Value = orderEntity.Company.LinkedinURL;
                    else
                        LinkAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(LinkAttr);

                    XmlAttribute TwitAttr = XDoc.CreateAttribute("TwitterURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.TwitterURL))
                        TwitAttr.Value = orderEntity.Company.TwitterURL;
                    else
                        TwitAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(TwitAttr);


                    XmlAttribute FaceAttr = XDoc.CreateAttribute("FacebookURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.FacebookURL))
                        FaceAttr.Value = orderEntity.Company.FacebookURL;
                    else
                        FaceAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(FaceAttr);

                    XmlAttribute WebUrlAttr = XDoc.CreateAttribute("WebURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.URL))
                        WebUrlAttr.Value = orderEntity.Company.URL;
                    else
                        WebUrlAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(WebUrlAttr);




                    XmlAttribute AccBalAttr = XDoc.CreateAttribute("AccountBalance");
                    if (orderEntity.Company.AccountBalance != null)
                        AccBalAttr.Value = Convert.ToString(orderEntity.Company.AccountBalance);
                    else
                        AccBalAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(AccBalAttr);


                    XmlAttribute VatRefAttr = XDoc.CreateAttribute("VATRefNumber");
                    if (!string.IsNullOrEmpty(orderEntity.Company.VATRegReference))
                        VatRefAttr.Value = orderEntity.Company.VATRegReference;
                    else
                        VatRefAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(VatRefAttr);


                    XmlAttribute VatRegAttr = XDoc.CreateAttribute("VATRegNumber");
                    if (!string.IsNullOrEmpty(orderEntity.Company.VATRegNumber))
                        VatRegAttr.Value = orderEntity.Company.VATRegNumber;
                    else
                        VatRegAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(VatRegAttr);


                    XmlAttribute CreditLimitAttr = XDoc.CreateAttribute("CreditLimit");
                    if (orderEntity.Company.CreditLimit != null)
                        CreditLimitAttr.Value = Convert.ToString(orderEntity.Company.CreditLimit);
                    else
                        CreditLimitAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(CreditLimitAttr);

                    XmlAttribute NotesAttr = XDoc.CreateAttribute("Notes");
                    if (!string.IsNullOrEmpty(orderEntity.Company.Notes))
                        NotesAttr.Value = orderEntity.Company.Notes;
                    else
                        NotesAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(NotesAttr);


                    int CustTypeS = orderEntity.Company.IsCustomer;
                    if (CustTypeS == 3)
                        isCorporate = true;

                    if (isCorporate)
                    {

                        XmlAttribute WebAccessAttr = XDoc.CreateAttribute("WebAccessCode");
                        if (!string.IsNullOrEmpty(orderEntity.Company.WebAccessCode))
                            WebAccessAttr.Value = orderEntity.Company.WebAccessCode;
                        else
                            WebAccessAttr.Value = string.Empty;
                        XCompanyTemp.SetAttributeNode(WebAccessAttr);

                    }



                    XmlAttribute AccAtNoAttr = XDoc.CreateAttribute("AccountNo");
                    if (!string.IsNullOrEmpty(orderEntity.Company.AccountNumber))
                        AccAtNoAttr.Value = orderEntity.Company.AccountNumber;
                    else
                        AccAtNoAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(AccAtNoAttr);

                    XmlAttribute phneAttr = XDoc.CreateAttribute("Phone");
                    if (!string.IsNullOrEmpty(orderEntity.Company.PhoneNo))
                        phneAttr.Value = orderEntity.Company.PhoneNo;
                    else
                        phneAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(phneAttr);



                    int CustType = orderEntity.Company.IsCustomer;
                    string CustomerType = string.Empty;
                    if (CustType == 1)
                        CustomerType = "Retail Customer";
                    else if (CustType == 2)
                        CustomerType = "Supplier";
                    else if (CustType == 3)
                    {
                        CustomerType = "Corporate Store";
                        isCorporate = true;
                    }
                    else if (CustType == 4)
                    {
                        CustomerType = "Retail Store";
                    }

                    XmlAttribute typeAttr = XDoc.CreateAttribute("Type");
                    typeAttr.Value = CustomerType;
                    XCompanyTemp.SetAttributeNode(typeAttr);


                    XmlAttribute comp = XDoc.CreateAttribute("Name");
                    if (orderEntity.Company != null)
                        comp.Value = orderEntity.Company.Name;
                    else
                        comp.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(comp);


                    XElemRoot.AppendChild(XCompanyTemp);

                    XmlElement XContactTemp = XDoc.CreateElement("CompanyContact");

                    XmlAttribute cSkypeAtt = XDoc.CreateAttribute("SkypeID");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.SkypeId))
                        cSkypeAtt.Value = orderEntity.CompanyContact.SkypeId;
                    else
                        cSkypeAtt.Value = "";
                    XContactTemp.SetAttributeNode(cSkypeAtt);

                    XmlAttribute cFaxAtt = XDoc.CreateAttribute("FAX");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.FAX))
                        cFaxAtt.Value = orderEntity.CompanyContact.FAX;
                    else
                        cFaxAtt.Value = "";
                    XContactTemp.SetAttributeNode(cFaxAtt);


                    XmlAttribute cDirectAtt = XDoc.CreateAttribute("DirectLine");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.HomeTel1))
                        cDirectAtt.Value = orderEntity.CompanyContact.HomeTel1;
                    else
                        cDirectAtt.Value = "";
                    XContactTemp.SetAttributeNode(cDirectAtt);


                    XmlAttribute cCellAtt = XDoc.CreateAttribute("CellNumbber");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.Mobile))
                        cCellAtt.Value = orderEntity.CompanyContact.Mobile;
                    else
                        cCellAtt.Value = "";
                    XContactTemp.SetAttributeNode(cCellAtt);


                    XmlAttribute cJobAtt = XDoc.CreateAttribute("JobTitle");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.JobTitle))
                        cJobAtt.Value = orderEntity.CompanyContact.JobTitle;
                    else
                        cJobAtt.Value = "";
                    XContactTemp.SetAttributeNode(cJobAtt);



                    XmlAttribute cEmailAtt = XDoc.CreateAttribute("Email");
                    cEmailAtt.Value = orderEntity.CompanyContact.Email;
                    XContactTemp.SetAttributeNode(cEmailAtt);



                    XmlAttribute contName = XDoc.CreateAttribute("Name");
                    string FullName = orderEntity.CompanyContact.FirstName + " " + orderEntity.CompanyContact.LastName;
                    contName.Value = FullName;
                    XContactTemp.SetAttributeNode(contName);


                    XElemRoot.AppendChild(XContactTemp);
                    // att area contact

                    XmlElement XAddressTemp = XDoc.CreateElement("ShippingAddress");

                    //  orderEntity.tbl_contacts.tbl_addresses.pos
                    Address contAddress = orderEntity.CompanyContact.Address;

                    XmlAttribute CountAttr = XDoc.CreateAttribute("Country");
                    if (orderEntity.CompanyContact.Address != null)
                    {
                        if (contAddress.Country != null)
                            CountAttr.Value = contAddress.Country.CountryName;
                        else
                            CountAttr.Value = string.Empty;
                    }
                    else
                    {
                        CountAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(CountAttr);

                    XmlAttribute StateAttr = XDoc.CreateAttribute("State");
                    if (contAddress.State != null)
                    {
                        if (!string.IsNullOrEmpty(contAddress.State.StateName))
                            StateAttr.Value = contAddress.State.StateName;
                        else
                            StateAttr.Value = string.Empty;
                    }
                    else
                    {
                        StateAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(StateAttr);


                    XmlAttribute CityAttr = XDoc.CreateAttribute("City");
                    if (contAddress != null)
                    {
                        if (!string.IsNullOrEmpty(contAddress.City))
                            CityAttr.Value = contAddress.City;
                        else
                            CityAttr.Value = string.Empty;
                    }
                    else
                    {
                        CityAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(CityAttr);

                    XmlAttribute PostAttr = XDoc.CreateAttribute("PostCode");
                    if (contAddress != null)
                    {
                        if (!string.IsNullOrEmpty(contAddress.PostCode))
                            PostAttr.Value = contAddress.PostCode;
                        else
                            PostAttr.Value = string.Empty;
                    }
                    else
                    {
                        PostAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(PostAttr);

                    XmlAttribute AddName = XDoc.CreateAttribute("Address");
                    if (contAddress != null)
                        AddName.Value = contAddress.Address1 + " " + contAddress.Address2;
                    XAddressTemp.SetAttributeNode(AddName);

                    XElemRoot.AppendChild(XAddressTemp);
                    // att area address

                    if (orderEntity.BillingAddressId != null)
                    {
                        XmlElement XBillingAddressTemp = XDoc.CreateElement("BillingAddress");

                        Address adress = addressRepository.GetAddressByIdforXML(orderEntity.BillingAddressId ?? 0);

                        XmlAttribute CountaAttr = XDoc.CreateAttribute("Country");
                        if (adress != null)
                        {
                            if (adress.Country != null)
                                CountaAttr.Value = adress.Country.CountryName;
                            else
                                CountaAttr.Value = string.Empty;
                        }
                        else
                        {
                            CountaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(CountaAttr);


                        XmlAttribute StateaAttr = XDoc.CreateAttribute("State");
                        if (adress != null)
                        {
                            if (adress.State != null)
                                StateaAttr.Value = adress.State.StateName;
                            else
                                StateaAttr.Value = string.Empty;
                        }
                        else
                        {
                            StateaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(StateaAttr);


                        XmlAttribute CityaAttr = XDoc.CreateAttribute("City");
                        if (adress != null)
                        {
                            if (!string.IsNullOrEmpty(adress.City))
                                CityaAttr.Value = adress.City;
                            else
                                CityaAttr.Value = string.Empty;
                        }
                        else
                        {
                            CityaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(CityaAttr);



                        XmlAttribute PostaAttr = XDoc.CreateAttribute("PostCode");
                        if (adress != null)
                        {
                            if (!string.IsNullOrEmpty(adress.PostCode))
                                PostaAttr.Value = adress.PostCode;
                            else
                                PostaAttr.Value = string.Empty;
                        }
                        else
                        {
                            PostaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(PostaAttr);

                        XmlAttribute AddaName = XDoc.CreateAttribute("Address");
                        if (adress != null)
                            AddaName.Value = adress.Address1 + " " + adress.Address2;
                        else
                            AddaName.Value = string.Empty;
                        XBillingAddressTemp.SetAttributeNode(AddaName);

                        XElemRoot.AppendChild(XBillingAddressTemp);
                    }


                    XmlElement ItemElemRoot = XDoc.CreateElement("Items");
                    XElemRoot.AppendChild(ItemElemRoot);

                    foreach (Item items in orderEntity.Items)
                    {
                        if (items.ItemType != 2)
                        {

                            //  string ItemCount = "ItemNo " + itemsCount;
                            XmlElement ItemNoElemRoot = XDoc.CreateElement("Item");

                            string qtyMarkup = string.Empty;
                            if (items.Qty1MarkUp1Value != null)
                            {
                                qtyMarkup = Convert.ToString(items.Qty1MarkUp1Value);
                                if (qtyMarkup.StartsWith("-"))
                                {
                                    XmlAttribute DiscAttr = XDoc.CreateAttribute("Discount");

                                    DiscAttr.Value = Convert.ToString(items.Qty1MarkUp1Value);
                                    ItemNoElemRoot.SetAttributeNode(DiscAttr);
                                }
                            }

                            XmlAttribute GrandAttr = XDoc.CreateAttribute("GrandTotal");
                            string grandtotal = string.Empty;
                            if (items.Qty1GrossTotal != null)
                                grandtotal = CurrencySymbol + Convert.ToString(items.Qty1GrossTotal);
                            GrandAttr.Value = grandtotal;
                            ItemNoElemRoot.SetAttributeNode(GrandAttr);

                            XmlAttribute VatAttr = XDoc.CreateAttribute("VAT");
                            string VAT = string.Empty;
                            if (items.Qty1Tax1Value != null)
                                VAT = CurrencySymbol + Convert.ToString(items.Qty1Tax1Value);
                            VatAttr.Value = VAT;
                            ItemNoElemRoot.SetAttributeNode(VatAttr);

                            XmlAttribute netAttr = XDoc.CreateAttribute("NetTotal");
                            string nettotal = string.Empty;
                            if (items.Qty1NetTotal != null)
                                nettotal = CurrencySymbol + Convert.ToString(items.Qty1NetTotal);
                            netAttr.Value = nettotal;
                            ItemNoElemRoot.SetAttributeNode(netAttr);


                            XmlAttribute qtyAttr = XDoc.CreateAttribute("Quantity");
                            string qty = string.Empty;
                            if (items.Qty1 != null)
                                qty = Convert.ToString(items.Qty1);
                            qtyAttr.Value = qty;
                            ItemNoElemRoot.SetAttributeNode(qtyAttr);


                            if (items.Status != null)
                            {
                                if (items.Status.StatusId != 17)
                                {

                                    XmlAttribute JoAttr = XDoc.CreateAttribute("JobCode");
                                    JoAttr.Value = items.JobCode;
                                    ItemNoElemRoot.SetAttributeNode(JoAttr);
                                }

                            }

                            XmlAttribute codeAttr = XDoc.CreateAttribute("ItemCode");
                            codeAttr.Value = items.ItemCode;
                            ItemNoElemRoot.SetAttributeNode(codeAttr);


                            string stat = string.Empty;
                            if (items.Status != null)
                                stat = items.Status.StatusName;
                            else
                                stat = "N/A";

                            XmlAttribute JobStatus = XDoc.CreateAttribute("Status");
                            JobStatus.Value = stat;
                            ItemNoElemRoot.SetAttributeNode(JobStatus);


                            string ProductFullName = items.ProductName;


                            XmlAttribute Prod = XDoc.CreateAttribute("ProductName");
                            Prod.Value = ProductFullName;
                            ItemNoElemRoot.SetAttributeNode(Prod);

                            // ItemXTemp.InnerText = ProductFullName;
                            ItemElemRoot.AppendChild(ItemNoElemRoot);
                            // attr area items


                            XmlElement ItemXTemp = XDoc.CreateElement("JobDescriptions1");
                            string JobDescription1 = string.Empty;
                            JobDescription1 = items.JobDescriptionTitle1 + ": " + items.JobDescription1;
                            if (JobDescription1 != ": ")
                                ItemXTemp.InnerText = JobDescription1;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions2");
                            string JobDescription2 = string.Empty;
                            JobDescription2 = items.JobDescriptionTitle2 + ": " + items.JobDescription2;
                            if (JobDescription2 != ": ")
                                ItemXTemp.InnerText = JobDescription2;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);


                            ItemXTemp = XDoc.CreateElement("JobDescriptions3");
                            string JobDescription3 = string.Empty;
                            JobDescription3 = items.JobDescriptionTitle3 + ": " + items.JobDescription3;
                            if (JobDescription3 != ": ")
                                ItemXTemp.InnerText = JobDescription3;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);



                            ItemXTemp = XDoc.CreateElement("JobDescriptions4");
                            string JobDescription4 = string.Empty;
                            JobDescription4 = items.JobDescriptionTitle4 + ": " + items.JobDescription4;
                            if (JobDescription4 != ": ")
                                ItemXTemp.InnerText = JobDescription4;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions5");
                            string JobDescription5 = string.Empty;
                            JobDescription5 = items.JobDescriptionTitle5 + ": " + items.JobDescription5;
                            if (JobDescription5 != ": ")
                                ItemXTemp.InnerText = JobDescription5;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions6");
                            string JobDescription6 = string.Empty;
                            JobDescription6 = items.JobDescriptionTitle6 + ": " + items.JobDescription6;
                            if (JobDescription6 != ": ")
                                ItemXTemp.InnerText = JobDescription6;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions7");
                            string JobDescription7 = string.Empty;
                            JobDescription7 = items.JobDescriptionTitle7 + ": " + items.JobDescription7;
                            if (JobDescription7 != ": ")
                                ItemXTemp.InnerText = JobDescription7;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);


                            ItemXTemp = XDoc.CreateElement("InvoiceDescription");
                            string invDesc = string.Empty;
                            invDesc = items.InvoiceDescription;
                            if (!string.IsNullOrEmpty(invDesc))
                                ItemXTemp.InnerText = invDesc;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);


                            if (items.ItemAttachments != null)
                            {
                                if (items.ItemAttachments.Count > 0)
                                {
                                    XmlElement ItemXTempAttachments = XDoc.CreateElement("Attachments");
                                    ItemNoElemRoot.AppendChild(ItemXTempAttachments);
                                    foreach (var v in items.ItemAttachments)
                                    {
                                        XmlElement AttachmentXTemp = XDoc.CreateElement("Attachment");
                                        XmlAttribute AttributeAtt = XDoc.CreateAttribute("Name");
                                        AttributeAtt.Value = v.FileName;
                                        AttachmentXTemp.SetAttributeNode(AttributeAtt);
                                        ItemXTempAttachments.AppendChild(AttachmentXTemp);

                                    }
                                }
                            }


                            XmlElement SectionsElemRoot = XDoc.CreateElement("ItemSections");
                            ItemNoElemRoot.AppendChild(SectionsElemRoot);

                            if (items.ItemSections != null)
                            {
                                if (items.ItemSections.Count > 0)
                                {
                                    foreach (var s in items.ItemSections)
                                    {
                                        XmlElement SectionsXTemp = XDoc.CreateElement("Section");
                                        SectionsElemRoot.AppendChild(SectionsXTemp);


                                        XmlAttribute platesAttr = XDoc.CreateAttribute("Plates");
                                        if (s.Side1PlateQty != null)
                                        {
                                            platesAttr.Value = Convert.ToString(s.Side1PlateQty);

                                        }
                                        else
                                            platesAttr.Value = "N/A";
                                        SectionsXTemp.SetAttributeNode(platesAttr);



                                        XmlAttribute incPlate = XDoc.CreateAttribute("IsPlateSupplied");
                                        if (s.IsPlateSupplied == true)
                                        {
                                            incPlate.Value = "True";

                                        }
                                        else
                                            incPlate.Value = "False";
                                        SectionsXTemp.SetAttributeNode(incPlate);



                                        XmlAttribute incGutter = XDoc.CreateAttribute("IncludeGutter");
                                        if (s.IncludeGutter == true)
                                        {
                                            incGutter.Value = "True";

                                        }
                                        else
                                            incGutter.Value = "False";
                                        SectionsXTemp.SetAttributeNode(incGutter);


                                        XmlAttribute PaperSupplied = XDoc.CreateAttribute("IsPaperSupplied");
                                        if (s.IsPaperSupplied == true)
                                        {
                                            PaperSupplied.Value = "True";

                                        }
                                        else
                                            PaperSupplied.Value = "False";
                                        SectionsXTemp.SetAttributeNode(PaperSupplied);


                                        int InkID = 0;
                                        if (s.PlateInkId != null)
                                            InkID = (int)s.PlateInkId;

                                        string InkString = MachineRepository.GetInkPlatesSidesByInkID(InkID);
                                        XmlAttribute inkAttr = XDoc.CreateAttribute("Ink");
                                        if (!string.IsNullOrEmpty(InkString))
                                            inkAttr.Value = InkString;
                                        else
                                            inkAttr.Value = "N/A";
                                        SectionsXTemp.SetAttributeNode(inkAttr);



                                        bool iscustome = false;
                                        bool isItemSizeCustom = false;

                                        XmlAttribute itemSizAttr = XDoc.CreateAttribute("ItemSizeCustom");
                                        if (s.IsItemSizeCustom == true)
                                        {
                                            itemSizAttr.Value = "True";
                                            isItemSizeCustom = true;
                                        }
                                        else
                                            itemSizAttr.Value = "False";
                                        SectionsXTemp.SetAttributeNode(itemSizAttr);


                                        XmlAttribute SecSizAttr = XDoc.CreateAttribute("SectionSizeCustom");
                                        if (s.IsSectionSizeCustom == true)
                                        {
                                            SecSizAttr.Value = "True";
                                            iscustome = true;
                                        }
                                        else
                                            SecSizAttr.Value = "False";
                                        SectionsXTemp.SetAttributeNode(SecSizAttr);


                                        if (iscustome)
                                        {

                                            XmlAttribute secHeightAttr = XDoc.CreateAttribute("SectionSizeHeight");
                                            if (s.SectionSizeHeight != null)
                                                secHeightAttr.Value = Convert.ToString(s.SectionSizeHeight);
                                            else
                                                secHeightAttr.Value = "";
                                            SectionsXTemp.SetAttributeNode(secHeightAttr);

                                            XmlAttribute secwidthAttr = XDoc.CreateAttribute("SectionSizeWidth");
                                            if (s.SectionSizeWidth != null)
                                                secwidthAttr.Value = Convert.ToString(s.SectionSizeWidth);
                                            else
                                                secwidthAttr.Value = "";
                                            SectionsXTemp.SetAttributeNode(secwidthAttr);

                                        }
                                        else
                                        {
                                            string PaperString = string.Empty;
                                            int PSSID = 0;
                                            if (s.SectionSizeId != null)
                                                PSSID = (int)s.SectionSizeId;

                                            // N Chance

                                            var data = paperSizeRepository.GetPaperSizesByID(PSSID);

                                            if (data != null)
                                            {
                                                foreach (var v in data)
                                                {
                                                    PaperString = v.Name + " " + v.Height + "x" + v.Width;
                                                }

                                            }
                                            XmlAttribute secwidthssAttr = XDoc.CreateAttribute("SectionSize");
                                            if (!string.IsNullOrEmpty(PaperString))
                                                secwidthssAttr.Value = PaperString;
                                            else
                                                secwidthssAttr.Value = "";
                                            SectionsXTemp.SetAttributeNode(secwidthssAttr);


                                        }


                                        if (isItemSizeCustom)
                                        {
                                            XmlAttribute itemSizHeightAttr = XDoc.CreateAttribute("ItemSizeHeight");
                                            if (s.ItemSizeHeight != null)
                                                itemSizHeightAttr.Value = Convert.ToString(s.ItemSizeHeight);
                                            else
                                                itemSizHeightAttr.Value = "N/A";
                                            SectionsXTemp.SetAttributeNode(itemSizHeightAttr);

                                            XmlAttribute itemSizwidthAttr = XDoc.CreateAttribute("ItemSizeWidth");
                                            if (s.ItemSizeWidth != null)
                                                itemSizwidthAttr.Value = Convert.ToString(s.ItemSizeWidth);
                                            else
                                                itemSizwidthAttr.Value = "N/A";
                                            SectionsXTemp.SetAttributeNode(itemSizwidthAttr);

                                        }
                                        else
                                        {


                                            string PaperSString = string.Empty;
                                            int PSID = 0;
                                            if (s.ItemSizeId != null)
                                                PSID = (int)s.ItemSizeId;

                                            var data2 = paperSizeRepository.GetPaperSizesByID(PSID);

                                            if (data2 != null)
                                            {
                                                foreach (var v in data2)
                                                {
                                                    PaperSString = v.Name + " " + v.Height + "x" + v.Width;
                                                }

                                            }

                                            // N chance

                                            XmlAttribute itemSizeAttr = XDoc.CreateAttribute("ItemSize");
                                            if (!string.IsNullOrEmpty(PaperSString))
                                                itemSizeAttr.Value = PaperSString;
                                            else
                                                itemSizeAttr.Value = "N/A";
                                            SectionsXTemp.SetAttributeNode(itemSizeAttr);

                                        }


                                        XmlAttribute GuiloAttr = XDoc.CreateAttribute("Guillotin");

                                        int GID = 0;
                                        if (s.GuillotineId != null)
                                            GID = (int)s.GuillotineId;
                                        string GuillotinName = MachineRepository.GetMachineByID(GID);
                                        if (!string.IsNullOrEmpty(GuillotinName))
                                            GuiloAttr.Value = GuillotinName;
                                        else
                                            GuiloAttr.Value = string.Empty;
                                        SectionsXTemp.SetAttributeNode(GuiloAttr);




                                        XmlAttribute StockAttr = XDoc.CreateAttribute("Stock");

                                        int StockID = 0;
                                        if (s.StockItemID1 != null)
                                            StockID = (int)s.StockItemID1;
                                        string StockName = stockItemRepository.GetStockName(StockID);
                                        if (!string.IsNullOrEmpty(StockName))
                                            StockAttr.Value = StockName;
                                        else
                                            StockAttr.Value = string.Empty;
                                        SectionsXTemp.SetAttributeNode(StockAttr);


                                        XmlAttribute PressAttr = XDoc.CreateAttribute("Press");
                                        int PID = 0;
                                        if (s.PressId != null)
                                            PID = (int)s.PressId;

                                        string PressName = MachineRepository.GetMachineByID(PID);
                                        if (!string.IsNullOrEmpty(PressName))
                                            PressAttr.Value = PressName;
                                        else
                                            PressAttr.Value = string.Empty;
                                        SectionsXTemp.SetAttributeNode(PressAttr);


                                        XmlAttribute AttributeSec = XDoc.CreateAttribute("Name");
                                        AttributeSec.Value = s.SectionName;
                                        SectionsXTemp.SetAttributeNode(AttributeSec);

                                        SectionsElemRoot.AppendChild(SectionsXTemp);

                                        // attr secComes here


                                        XmlElement SectXTemp = XDoc.CreateElement("Quantities");

                                        XmlAttribute AttributeQty3 = XDoc.CreateAttribute("Quantity3");
                                        if (s.Qty3 != null)
                                            AttributeQty3.Value = Convert.ToString(s.Qty3);
                                        else
                                            AttributeQty3.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributeQty3);


                                        XmlAttribute AttributeQty2 = XDoc.CreateAttribute("Quantity2");
                                        if (s.Qty2 != null)
                                            AttributeQty2.Value = Convert.ToString(s.Qty2);
                                        else
                                            AttributeQty2.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributeQty2);

                                        XmlAttribute AttributeQty1 = XDoc.CreateAttribute("Quantity1");
                                        if (s.Qty1 != null)
                                            AttributeQty1.Value = Convert.ToString(s.Qty1);
                                        else
                                            AttributeQty1.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributeQty1);

                                        SectionsXTemp.AppendChild(SectXTemp);


                                        SectXTemp = XDoc.CreateElement("CostCenterTotals");

                                        double Qty3Total = s.SectionCostcentres.Sum(c => c.Qty3NetTotal ?? 0);
                                        XmlAttribute AttributeCS3 = XDoc.CreateAttribute("Quantity3");
                                        AttributeCS3.Value = CurrencySymbol + Convert.ToString(Qty3Total);
                                        SectXTemp.SetAttributeNode(AttributeCS3);


                                        double Qty2Total = s.SectionCostcentres.Sum(c => c.Qty2NetTotal ?? 0);
                                        XmlAttribute AttributeCS2 = XDoc.CreateAttribute("Quantity2");
                                        AttributeCS2.Value = CurrencySymbol + Convert.ToString(Qty2Total);
                                        SectXTemp.SetAttributeNode(AttributeCS2);


                                        double Qty1Total = s.SectionCostcentres.Sum(c => c.Qty1NetTotal ?? 0);
                                        XmlAttribute AttributeCS1 = XDoc.CreateAttribute("Quantity1");

                                        AttributeCS1.Value = CurrencySymbol + Convert.ToString(Qty1Total);
                                        SectXTemp.SetAttributeNode(AttributeCS1);

                                        SectionsXTemp.AppendChild(SectXTemp);

                                        SectXTemp = XDoc.CreateElement("Signatures");

                                        double Sig3 = 0;
                                        if (s.SimilarSections != null)
                                            Sig3 = (double)s.SimilarSections * Qty3Total;

                                        XmlAttribute AttributeSS3 = XDoc.CreateAttribute("Quantity3");
                                        AttributeSS3.Value = CurrencySymbol + Convert.ToString(Sig3);

                                        SectXTemp.SetAttributeNode(AttributeSS3);


                                        double Sig2 = 0;
                                        if (s.SimilarSections != null)
                                            Sig2 = (double)s.SimilarSections * Qty2Total;
                                        XmlAttribute AttributeSS2 = XDoc.CreateAttribute("Quantity2");
                                        AttributeSS2.Value = CurrencySymbol + Convert.ToString(Sig2);
                                        SectXTemp.SetAttributeNode(AttributeSS2);

                                        double Sig1 = 0;
                                        if (s.SimilarSections != null)
                                            Sig1 = (double)s.SimilarSections * Qty1Total;
                                        XmlAttribute AttributeSS1 = XDoc.CreateAttribute("Quantity1");
                                        AttributeSS1.Value = CurrencySymbol + Convert.ToString(Sig1);

                                        SectXTemp.SetAttributeNode(AttributeSS1);


                                        XmlAttribute SimilarSections = XDoc.CreateAttribute("SimilarSignature");
                                        if (s.SimilarSections != null)
                                            SimilarSections.Value = Convert.ToString(s.SimilarSections);
                                        else
                                            SimilarSections.Value = "0";
                                        SectXTemp.SetAttributeNode(SimilarSections);

                                        SectionsXTemp.AppendChild(SectXTemp);

                                        SectXTemp = XDoc.CreateElement("Markups");

                                        double Mrk1 = 0;
                                        if (s.BaseCharge1 != null)
                                        {
                                            double Add = Qty1Total + Sig1;
                                            Mrk1 = (double)s.BaseCharge1 - Add;
                                        }

                                        double Mrk2 = 0;
                                        if (s.BaseCharge2 != null)
                                        {
                                            double Add = Qty2Total + Sig2;
                                            Mrk2 = (double)s.BaseCharge2 - Add;
                                        }
                                        double Mrk3 = 0;
                                        if (s.Basecharge3 != null)
                                        {
                                            double Add = Qty3Total + Sig3;
                                            Mrk3 = (double)s.Basecharge3 - Add;
                                        }


                                        //string MarkupName3 = ObjectContext.tbl_markup.Where(m => m.MarkUpID == MarkUp3).Select(a => a.MarkUpName).FirstOrDefault();
                                        XmlAttribute AttributeMarkup3 = XDoc.CreateAttribute("Quantity3");

                                        AttributeMarkup3.Value = CurrencySymbol + Convert.ToString(Mrk3);

                                        SectXTemp.SetAttributeNode(AttributeMarkup3);


                                        XmlAttribute AttributeMarkup2 = XDoc.CreateAttribute("Quantity2");
                                        AttributeMarkup2.Value = CurrencySymbol + Convert.ToString(Mrk2);

                                        SectXTemp.SetAttributeNode(AttributeMarkup2);



                                        XmlAttribute AttributeMarkup1 = XDoc.CreateAttribute("Quantity1");
                                        AttributeMarkup1.Value = CurrencySymbol + Convert.ToString(Mrk1);

                                        SectXTemp.SetAttributeNode(AttributeMarkup1);

                                        SectionsXTemp.AppendChild(SectXTemp);

                                        SectXTemp = XDoc.CreateElement("SectionSubTotal");


                                        XmlAttribute AttributesubTotal3 = XDoc.CreateAttribute("Quantity3");
                                        if (s.Basecharge3 != null)
                                            AttributesubTotal3.Value = CurrencySymbol + Convert.ToString(s.Basecharge3);
                                        else
                                            AttributesubTotal3.Value = "0";

                                        SectXTemp.SetAttributeNode(AttributesubTotal3);

                                        XmlAttribute AttributesubTotal2 = XDoc.CreateAttribute("Quantity2");
                                        if (s.BaseCharge2 != null)
                                            AttributesubTotal2.Value = CurrencySymbol + Convert.ToString(s.BaseCharge2);
                                        else
                                            AttributesubTotal2.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributesubTotal2);

                                        XmlAttribute AttributesubTotal1 = XDoc.CreateAttribute("Quantity1");
                                        if (s.BaseCharge1 != null)
                                            AttributesubTotal1.Value = CurrencySymbol + Convert.ToString(s.BaseCharge1);
                                        else
                                            AttributesubTotal1.Value = "0";

                                        SectXTemp.SetAttributeNode(AttributesubTotal1);


                                        SectionsXTemp.AppendChild(SectXTemp);



                                        if (s.SectionCostcentres != null)
                                        {
                                            List<long> CostCenterIDs = s.SectionCostcentres.Select(v => v.CostCentreId ?? 0).ToList();
                                            if (CostCenterIDs != null && CostCenterIDs.Count > 0)
                                            {
                                                var ProductData = CostCentreRepository.GetCostCentresforxml(CostCenterIDs);


                                                XmlElement SectionCostCenterElemRoot = XDoc.CreateElement("SectionCostCenters");
                                                SectionsXTemp.AppendChild(SectionCostCenterElemRoot);

                                                foreach (var cc in ProductData)
                                                {

                                                    XmlElement SCCNoXTemp = XDoc.CreateElement("CostCenter");
                                                    // SCCXTemp.InnerText = SupplierName;
                                                    SectionCostCenterElemRoot.AppendChild(SCCNoXTemp);


                                                    double nettot = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1NetTotal ?? 0).FirstOrDefault();
                                                    XmlAttribute NetAttr = XDoc.CreateAttribute("NetTotal");
                                                    NetAttr.InnerText = CurrencySymbol + Convert.ToString(nettot);
                                                    SCCNoXTemp.SetAttributeNode(NetAttr);

                                                    double markupVal = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1MarkUpValue ?? 0).FirstOrDefault();
                                                    XmlAttribute MarkUpValAttr = XDoc.CreateAttribute("MarkupValue");
                                                    MarkUpValAttr.Value = CurrencySymbol + Convert.ToString(markupVal);
                                                    SCCNoXTemp.SetAttributeNode(MarkUpValAttr);



                                                    int MID = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1MarkUpID ?? 0).FirstOrDefault();
                                                    string MarkName = _markupRepository.GetMarkupNamebyID(MID);
                                                    XmlAttribute MarkUpAttr = XDoc.CreateAttribute("Markup");
                                                    if (!string.IsNullOrEmpty(MarkName))
                                                        MarkUpAttr.Value = MarkName;
                                                    else
                                                        MarkUpAttr.Value = "N/A";
                                                    SCCNoXTemp.SetAttributeNode(MarkUpAttr);


                                                    double QtyChrge = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1Charge ?? 0).FirstOrDefault();
                                                    XmlAttribute PriceAttr = XDoc.CreateAttribute("Price");
                                                    if (QtyChrge > 0)
                                                        PriceAttr.Value = CurrencySymbol + Convert.ToString(QtyChrge);
                                                    else
                                                        PriceAttr.Value = "0";
                                                    SCCNoXTemp.SetAttributeNode(PriceAttr);

                                                    double EstimateTime = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1EstimatedTime).FirstOrDefault();
                                                    XmlAttribute EstimAttr = XDoc.CreateAttribute("EstimateProductionTime");
                                                    if (EstimateTime > 0)
                                                        EstimAttr.Value = Convert.ToString(EstimateTime);
                                                    else
                                                        EstimAttr.Value = "0";
                                                    SCCNoXTemp.SetAttributeNode(EstimAttr);

                                                    string SupplierName = companyRepository.GetSupplierNameByID(cc.PreferredSupplierId ?? 0);
                                                    XmlAttribute SuppNameAttr = XDoc.CreateAttribute("SupplierName");
                                                    if (!string.IsNullOrEmpty(SupplierName))
                                                        SuppNameAttr.Value = SupplierName;
                                                    else
                                                        SuppNameAttr.Value = "";
                                                    SCCNoXTemp.SetAttributeNode(SuppNameAttr);


                                                    XmlAttribute AttributeCostCenter = XDoc.CreateAttribute("Name");
                                                    AttributeCostCenter.Value = cc.Name;
                                                    SCCNoXTemp.SetAttributeNode(AttributeCostCenter);

                                                    SectionCostCenterElemRoot.AppendChild(SCCNoXTemp);


                                                    string WorkInstruction = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1WorkInstructions ?? "N/A").FirstOrDefault();
                                                    XmlElement SCCXTemp = XDoc.CreateElement("WorkInstruction");
                                                    SCCXTemp.InnerText = WorkInstruction;
                                                    SCCNoXTemp.AppendChild(SCCXTemp);

                                                }
                                            }
                                        }


                                    }
                                }
                            }

                        }
                    }
                    // end of att area items

                    if (paymentsList != null && paymentsList.Count > 0)
                    {

                        XmlElement ItemElemRootPayment = XDoc.CreateElement("Payments");
                        XElemRoot.AppendChild(ItemElemRootPayment);


                        foreach (var payment in paymentsList)
                        {
                            XmlElement PaymentXTemp = XDoc.CreateElement("Payment");


                            string reference = PayPalRepsoitory.GetPayPalReference(iRecordID);
                            //select top 1 transactionid from tbl_paypalResponses where orderid = tbl_estimates.estimateid
                            XmlAttribute cRef = XDoc.CreateAttribute("Reference");
                            if (payment.PaymentMethodId == 1)
                                cRef.Value = reference;
                            else
                            {
                                if (payment.ReferenceCode != null)
                                    cRef.Value = payment.ReferenceCode;
                                else
                                    cRef.Value = "";
                            }

                            PaymentXTemp.SetAttributeNode(cRef);

                            XmlAttribute cAmount = XDoc.CreateAttribute("Amount");
                            if (payment.Amount != null)
                                cAmount.Value = CurrencySymbol + Convert.ToString(payment.Amount);
                            else
                                cAmount.Value = "";


                            PaymentXTemp.SetAttributeNode(cAmount);



                            DateTime dtPayment = new DateTime();
                            string paymentDate = string.Empty;
                            XmlAttribute cPDate = XDoc.CreateAttribute("Date");
                            if (payment.PaymentDate != null)
                            {
                                dtPayment = Convert.ToDateTime(payment.PaymentDate);
                                paymentDate = dtPayment.ToString("dd/MMM/yyyy");
                                cPDate.Value = paymentDate;
                            }
                            else
                                cPDate.Value = string.Empty;
                            PaymentXTemp.SetAttributeNode(cPDate);


                            XmlAttribute cPType = XDoc.CreateAttribute("Type");
                            if (payment.PaymentMethodId == 1)
                                cPType.Value = "Paypal";
                            else
                                cPType.Value = "On Account";
                            PaymentXTemp.SetAttributeNode(cPType);

                            ItemElemRootPayment.AppendChild(PaymentXTemp);


                        }
                    }

                    // att area payment

                    string sFileName = orderEntity.Order_Code + "_" + "OrderXML.xml";
                    // FileNamesList.Add(sFileName);
                    string Path = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID);
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }
                    sFilePath = Path + "/" + sFileName;
                    XDoc.Save(sFilePath);
                    //db.Configuration.LazyLoadingEnabled = false;
                    //db.Configuration.ProxyCreationEnabled = false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sFilePath;
        }


        public string ExportExcel(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam , long WebStoreOrganisationId = 0,bool isFromExternal = true)
        {
            string sFilePath = string.Empty;
             string InternalPath = string.Empty;
            try
            {
                long OrganisationID = 0;
                if (WebStoreOrganisationId > 0)
                {
                    OrganisationID = WebStoreOrganisationId;
                }
                else 
                {
                    Organisation org = organisationRepository.GetOrganizatiobByID();
                    if (org != null)
                    {
                        OrganisationID = org.OrganisationId;
                    }
                }
               
                Report currentReport = ReportRepository.GetReportByReportID(iReportID);
                if (currentReport.ReportId > 0)
                {
                    byte[] rptBytes = null;
                    rptBytes = System.Text.Encoding.Unicode.GetBytes(currentReport.ReportTemplate);
                    // Encoding must be done
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(rptBytes);
                    // Load it to memory stream
                    ms.Position = 0;
                    SectionReport currReport = new SectionReport();
                    string sFileName = iRecordID + "OrderReport.xls";
                    // FileNamesList.Add(sFileName);
                    currReport.LoadLayout(ms);
                    if (type == ReportType.JobCard)
                    {
                        sFileName = iRecordID + "JobCardReport.xls";
                        //  FileNamesList.Add(sFileName);
                        List<usp_JobCardReport_Result> rptSource = ReportRepository.getJobCardReportResult(OrganisationID, OrderID, iRecordID);
                        currReport.DataSource = rptSource;
                    }
                    else if (type == ReportType.Order)
                    {

                        List<usp_OrderReport_Result> rptOrderSource = ReportRepository.getOrderReportResult(OrganisationID, OrderID);
                        currReport.DataSource = rptOrderSource;
                    }
                    else if (type == ReportType.Internal)
                    {
                        string ReportDataSource = string.Empty;
                        string ReportTemplate = string.Empty;

                        DataTable dataSourceList = ReportRepository.GetReportDataSourceByReportID(iReportID, CriteriaParam);
                        currReport.DataSource = dataSourceList;
                    }
                    if (currReport != null)
                    {
                        currReport.Run();
                        GrapeCity.ActiveReports.Export.Excel.Section.XlsExport xls = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport();
                        xls.MinColumnWidth = 1;
                        
                        string Path = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/");
                        if (!Directory.Exists(Path))
                        {
                            Directory.CreateDirectory(Path);
                        }
                        // PdfExport pdf = new PdfExport();
                        sFilePath = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/") + sFileName;
                           InternalPath = "/" + ImagePathConstants.ReportPath + OrganisationID + "/" + sFileName;
                        xls.Export(currReport.Document, sFilePath);
                       
                        ms.Close();
                        currReport.Document.Dispose();
                        xls.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
              if (isFromExternal)
                return sFilePath;
            else
                return InternalPath;
        }


    }
}
