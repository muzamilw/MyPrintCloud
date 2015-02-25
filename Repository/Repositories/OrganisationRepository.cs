using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using MPC.Models.Common;
using System.Web;
using System.IO;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Company Sites Repository
    /// </summary>
    public class OrganisationRepository : BaseRepository<Organisation>, IOrganisationRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrganisationRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Organisation> DbSet
        {
            get
            {
                return db.Organisations;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Company Sites for User Domain Key
        /// </summary>
        public override IEnumerable<Organisation> GetAll()
        {
            return DbSet.Where(cs => cs.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Organisation By ID
        /// </summary>
        public Organisation GetOrganizatiobByID()
        {
            return DbSet.FirstOrDefault(cs => cs.OrganisationId == OrganisationId);
        }

        public Organisation GetOrganizatiobByID(long organisationId)
        {
            return DbSet.FirstOrDefault(cs => cs.OrganisationId == organisationId);
        }

        public Organisation GetOrganizatiobByOrganisationID(long organisationId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
          
            return db.Organisations.Where(o => o.OrganisationId == organisationId).FirstOrDefault();
        }
        public void InsertOrganisation(long OID,ExportOrganisation objExpOrg)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                List<string> DestinationsPath = new List<string>();
                try
                {

                 
                    long OrganisationID = 0;
                    Organisation newOrg = new Organisation();

                    ImportOrganisation ImportIDs = new ImportOrganisation();
                    ImportIDs.CostCentreIDs = new List<long>();
                    if (objExpOrg.Company != null)
                    {
                        ImportIDs.OldCompanyID = objExpOrg.Company.CompanyId;
                    }

                    Organisation objOrg = db.Organisations.Where(o => o.OrganisationId == OID).FirstOrDefault();
                    objOrg.OrganisationName = objExpOrg.Organisation.OrganisationName;
                    objOrg.Address1 = objExpOrg.Organisation.Address1;
                    objOrg.Address2 = objExpOrg.Organisation.Address2;
                    objOrg.Address3 = objExpOrg.Organisation.Address3;
                    objOrg.City = objExpOrg.Organisation.City;
                     objOrg.StateId = objExpOrg.Organisation.StateId;
                     objOrg.CountryId = objExpOrg.Organisation.CountryId;
                     objOrg.ZipCode = objExpOrg.Organisation.ZipCode;
                     objOrg.Tel = objExpOrg.Organisation.Tel;
                    objOrg.Fax = objExpOrg.Organisation.Fax;
                    objOrg.Mobile = objExpOrg.Organisation.Mobile;
                    objOrg.Email = objExpOrg.Organisation.Email;
                    objOrg.URL = objExpOrg.Organisation.URL;
                    objOrg.WebsiteLogo = objExpOrg.Organisation.WebsiteLogo;
                    objOrg.MISLogo = objExpOrg.Organisation.MISLogo;
                    objOrg.TaxRegistrationNo = objExpOrg.Organisation.TaxRegistrationNo;
                    objOrg.LicenseLevel = objExpOrg.Organisation.LicenseLevel;
                    objOrg.CustomerAccountNumber = objExpOrg.Organisation.CustomerAccountNumber;
                    objOrg.SmtpServer = objExpOrg.Organisation.SmtpServer;
                    objOrg.SmtpUserName = objExpOrg.Organisation.SmtpUserName;
                    objOrg.SmtpPassword = objExpOrg.Organisation.SmtpPassword;
                    objOrg.SystemWeightUnit = objExpOrg.Organisation.SystemWeightUnit;
                    objOrg.CurrencyId = objExpOrg.Organisation.CurrencyId;
                     objOrg.LanguageId = objExpOrg.Organisation.LanguageId;
                     objOrg.BleedAreaSize = objExpOrg.Organisation.BleedAreaSize;
                     objOrg.ShowBleedArea = objExpOrg.Organisation.ShowBleedArea;
                     objOrg.isXeroIntegrationRequired = objExpOrg.Organisation.isXeroIntegrationRequired;
                     objOrg.XeroApiId = objExpOrg.Organisation.XeroApiId;
                     objOrg.XeroApiKey = objExpOrg.Organisation.XeroApiKey;
                     objOrg.TaxServiceUrl = objExpOrg.Organisation.TaxServiceUrl;
                    objOrg.TaxServiceKey = objExpOrg.Organisation.TaxServiceKey;

                    db.SaveChanges();
                    ImportIDs.NewOrganisationID = OID;
                    ImportIDs.OldOrganisationID = objExpOrg.Organisation.OrganisationId;
                    OrganisationID = OID;
                   
                    // save paper sizes
                    if (objExpOrg.PaperSizes != null && objExpOrg.PaperSizes.Count > 0)
                    {
                        foreach (var size in objExpOrg.PaperSizes)
                        {
                            PaperSize Osize = new PaperSize();
                            Osize = size;
                            Osize.PaperSizeId = 0;
                            size.OrganisationId = OrganisationID;
                            db.PaperSizes.Add(size);
                            

                        }
                        db.SaveChanges();

                    }
                 



                    // save cost centres and its child objects
                    if (objExpOrg.CostCentre != null && objExpOrg.CostCentre.Count > 0)
                    {
                        foreach (var cost in objExpOrg.CostCentre)
                        {
                            long CID = cost.CostCentreId;
                            ImportIDs.CostCentreIDs.Add(cost.CostCentreId);
                            CostCentre cc = new CostCentre();
                            cc.CostCentreId = 0;

                            // save cost centre instructions
                            if (cost.CostcentreInstructions != null && cost.CostcentreInstructions.Count > 0)
                            {
                                foreach (var ins in cost.CostcentreInstructions)
                                {
                                    CostcentreInstruction instruction = new CostcentreInstruction();

                                    instruction = ins;
                                    instruction.InstructionId = 0;
                                    instruction.CostCentreId = cost.CostCentreId;

                                    db.CostcentreInstructions.Add(ins);
                                    //cc.CostcentreInstructions.Add(instruction);

                                    if (ins.CostcentreWorkInstructionsChoices != null && ins.CostcentreWorkInstructionsChoices.Count > 0)
                                    {
                                        foreach (var choice in ins.CostcentreWorkInstructionsChoices)
                                        {
                                            CostcentreWorkInstructionsChoice Objchoice = new CostcentreWorkInstructionsChoice();
                                            Objchoice = choice;
                                            Objchoice.Id = 0;
                                            Objchoice.InstructionId = ins.InstructionId;

                                            db.CostcentreWorkInstructionsChoices.Add(choice);
                                            //instruction.CostcentreWorkInstructionsChoices.Add(choice);

                                        }
                                    }

                                }

                            }

                            // save cost centre resources
                            if (cost.CostcentreResources != null && cost.CostcentreResources.Count > 0)
                            {
                                foreach (var res in cost.CostcentreResources)
                                {
                                    CostcentreResource resource = new CostcentreResource();
                                    resource = res;
                                    resource.CostCenterResourceId = 0;
                                    resource.CostCentreId = cost.CostCentreId;
                                    //cc.CostcentreResources.Add(res);
                                    db.CostcentreResources.Add(res);
                                }

                            }

                            List<CostCenterChoice> choices = objExpOrg.CostCenterChoice.Where(c => c.CostCenterId == CID).ToList();
                            if (choices != null && choices.Count > 0)
                            {

                                foreach (var choice in choices)
                                {
                                    CostCenterChoice choi = new CostCenterChoice();
                                    choi = choice;

                                    choi.CostCenterChoiceId = 0;
                                    choi.CostCenterId = (int)cost.CostCentreId;

                                    db.CostCenterChoices.Add(choi);
                                }

                            }
                            cc = cost;
                            cc.CostCentreId = 0;
                            cc.OrganisationId = OrganisationID;

                            db.CostCentres.Add(cc);

                            db.SaveChanges();

                        }



                    }
                    // cost centre questions answers
                    if (objExpOrg.CostCentreQuestion != null & objExpOrg.CostCentreQuestion.Count > 0)
                    {
                        foreach (var question in objExpOrg.CostCentreQuestion)
                        {
                            long QID = question.Id;
                            CostCentreQuestion Objquestion = new CostCentreQuestion();
                            Objquestion = question;

                            Objquestion.Id = 0;
                            Objquestion.CompanyId = (int)OrganisationID;

                            db.CostCentreQuestions.Add(Objquestion);
                            db.SaveChanges();
                            List<CostCentreAnswer> answers = objExpOrg.CostCentreAnswer.Where(q => q.QuestionId == QID).ToList();
                            if (answers != null && answers.Count > 0)
                            {
                                foreach (var ans in answers)
                                {
                                    CostCentreAnswer answer = new CostCentreAnswer();
                                    answer = ans;

                                    answer.QuestionId = question.Id;
                                    answer.Id = 0;
                                    db.CostCentreAnswers.Add(answer);
                                }
                                db.SaveChanges();
                            }


                        }


                    }

                    // cost centre matrix and matrix detail
                    if (objExpOrg.CostCentreMatrix != null && objExpOrg.CostCentreMatrix.Count > 0)
                    {
                        foreach (var matrix in objExpOrg.CostCentreMatrix)
                        {
                            long MID = matrix.MatrixId;
                            CostCentreMatrix Objmatrix = new CostCentreMatrix();
                            Objmatrix = matrix;
                            Objmatrix.MatrixId = 0;
                            Objmatrix.OrganisationId = (int)OrganisationID;
                            db.CostCentreMatrices.Add(Objmatrix);

                            db.SaveChanges();
                            List<CostCentreMatrixDetail> matrixDetail = objExpOrg.CostCentreMatrixDetail.Where(c => c.MatrixId == MID).ToList();
                            if (matrixDetail != null && matrixDetail.Count > 0)
                            {
                                foreach (var matrixD in matrixDetail)
                                {
                                    CostCentreMatrixDetail CCMD = new CostCentreMatrixDetail();
                                    CCMD = matrixD;
                                    CCMD.Id = 0;
                                    CCMD.MatrixId = matrix.MatrixId;
                                    db.CostCentreMatrixDetails.Add(CCMD);

                                }
                                db.SaveChanges();
                            }


                        }
                    }
                    
                    
                    // Stock Categories
                    if (objExpOrg.StockCategory != null && objExpOrg.StockCategory.Count > 0)
                    {
                        foreach (var cat in objExpOrg.StockCategory)
                        {
                            StockCategory SC = new StockCategory();
                            SC = cat;

                            SC.CategoryId = 0;
                            SC.OrganisationId = OrganisationID;
                            db.StockCategories.Add(SC);

                            if (cat.StockSubCategories != null && cat.StockSubCategories.Count > 0)
                            {
                                foreach (var subCat in cat.StockSubCategories)
                                {
                                    StockSubCategory SSC = new StockSubCategory();
                                    SSC = subCat;
                                    SSC.SubCategoryId = 0;
                                    SSC.CategoryId = cat.CategoryId;
                                    db.StockSubCategories.Add(subCat);
                                    // cat.StockSubCategories.Add(SSC);

                                }
                            }
                        }
                        db.SaveChanges();


                    }
                   

                    // stock items
                    if (objExpOrg.StockItem != null && objExpOrg.StockItem.Count > 0)
                    {
                        foreach (var Sitems in objExpOrg.StockItem)
                        {
                            StockItem SI = new StockItem();
                            SI = Sitems;
                            SI.StockItemId = 0;
                            SI.OrganisationId = OrganisationID;
                            db.StockItems.Add(SI);

                            if (Sitems.StockCostAndPrices != null && Sitems.StockCostAndPrices.Count > 0)
                            {
                                foreach (var costAndPrice in Sitems.StockCostAndPrices)
                                {
                                    StockCostAndPrice SCP = new StockCostAndPrice();
                                    SCP = costAndPrice;
                                    SCP.CostPriceId = 0;
                                    SCP.ItemId = Sitems.StockItemId;
                                    db.StockCostAndPrices.Add(costAndPrice);
                                    // Sitems.StockCostAndPrices.Add(SCP);
                                }

                            }

                        }
                        db.SaveChanges();
                    }
                    // import reports
                    if (objExpOrg.Reports != null && objExpOrg.Reports.Count > 0)
                    {
                        foreach (var report in objExpOrg.Reports)
                        {
                            Report rpt = new Report();
                            rpt = report;
                            rpt.ReportId = 0;
                            rpt.OrganisationId = OrganisationID;
                            db.Reports.Add(rpt);

                        }
                        db.SaveChanges();

                    }
                    // import report notes
                    if (objExpOrg.ReportNote != null && objExpOrg.ReportNote.Count > 0)
                    {
                        foreach (var rptNotes in objExpOrg.ReportNote)
                        {
                            ReportNote rptNote = new ReportNote();
                            rptNotes.Id = 0;
                            rptNotes.OrganisationId = OrganisationID;
                            db.ReportNotes.Add(rptNotes);

                        }
                        db.SaveChanges();
                    }
                    // import prefixes
                    if (objExpOrg.Prefixes != null && objExpOrg.Prefixes.Count > 0)
                    {
                        foreach (var prefix in objExpOrg.Prefixes)
                        {
                            Prefix pref = new Prefix();
                            pref = prefix;
                            pref.PrefixId = 0;
                            pref.OrganisationId = OrganisationID;
                            db.Prefixes.Add(pref);

                        }
                        db.SaveChanges();
                    }

                    // import lookup methods
                    if (objExpOrg.LookupMethods != null && objExpOrg.LookupMethods.Count > 0)
                    {
                        foreach (var lookup in objExpOrg.LookupMethods)
                        {
                            LookupMethod LM = new LookupMethod();
                            LM = lookup;

                            LM.MethodId = 0;
                            LM.OrganisationId = (int)OrganisationID;
                            db.LookupMethods.Add(LM);

                        }
                        db.SaveChanges();
                    }
                    // import phrase library
                    if (objExpOrg.PhraseField != null && objExpOrg.PhraseField.Count > 0)
                    {
                        foreach (var PF in objExpOrg.PhraseField)
                        {
                            PhraseField objPF = new PhraseField();
                            objPF = PF;
                            objPF.FieldId = 0;
                            objPF.OrganisationId = OrganisationID;
                            db.PhraseFields.Add(objPF);

                            if (PF.Phrases != null && PF.Phrases.Count > 0)
                            {
                                foreach (var phrase in PF.Phrases)
                                {
                                    Phrase objPh = new Phrase();
                                    objPh = phrase;
                                    objPh.PhraseId = 0;
                                    objPh.FieldId = PF.FieldId;
                                    objPh.CompanyId = OrganisationID;
                                    db.Phrases.Add(phrase);
                                    // PF.Phrases.Add(objPh);

                                }
                            }
                        }
                        db.SaveChanges();

                    }
                    // import section flags
                    if (objExpOrg.SectionFlags != null && objExpOrg.SectionFlags.Count > 0)
                    {
                        foreach (var sflag in objExpOrg.SectionFlags)
                        {
                            SectionFlag SF = new SectionFlag();
                            SF = sflag;
                            SF.SectionFlagId = 0;
                            SF.OrganisationId = OrganisationID;
                            db.SectionFlags.Add(SF);

                        }
                        db.SaveChanges();
                    }

                        //company flow 

                        // insert company
                      
                        Company comp = new Company();
                        comp = objExpOrg.Company;
                        comp.OrganisationId = OrganisationID;
                        comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                        comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);
                        comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                        comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                        db.Companies.Add(comp);
                        db.SaveChanges();
                        long oCID = comp.CompanyId;
                        // insert company domain

                        //if (objExpOrg.CompanyDomain != null && objExpOrg.CompanyDomain.Count > 0)
                        //{

                        //    foreach (var compDomain in objExpOrg.CompanyDomain)
                        //    {
                        //        CompanyDomain objDomain = new CompanyDomain();
                        //        objDomain = compDomain;
                        //        objDomain.Company = comp;
                        //        db.CompanyDomains.Add(objDomain);

                        //    }
                        //    // db.SaveChanges();

                        //}

                        ////   insert cms Offers
                        //if (objExpOrg.CmsOffer != null && objExpOrg.CmsOffer.Count > 0)
                        //{
                        //    foreach (var cmsOffers in objExpOrg.CmsOffer)
                        //    {
                        //        CmsOffer offers = new CmsOffer();
                        //        offers = cmsOffers;
                        //        offers.Company = comp;
                        //        db.CmsOffers.Add(offers);
                        //    }
                        //    //  db.SaveChanges();
                        //}

                        ////import media libraries
                        //if (objExpOrg.MediaLibrary != null && objExpOrg.MediaLibrary.Count > 0)
                        //{
                        //    foreach (var media in objExpOrg.MediaLibrary)
                        //    {
                        //        MediaLibrary ML = new MediaLibrary();
                        //        ML = media;

                        //        ML.Company = comp;
                        //        db.MediaLibraries.Add(ML);

                        //    }
                        //}

                        //// import banners

                        //if (objExpOrg.CompanyBannerSet != null && objExpOrg.CompanyBannerSet.Count > 0)
                        //{
                        //    foreach (var banSet in objExpOrg.CompanyBannerSet)
                        //    {
                        //        CompanyBannerSet CBS = new CompanyBannerSet();
                        //        CBS = banSet;
                        //        CBS.Company = comp;
                        //        CBS.OrganisationId = OrganisationID;
                        //        db.CompanyBannerSets.Add(CBS);

                        //        if (banSet.CompanyBanners != null && banSet.CompanyBanners.Count > 0)
                        //        {
                        //            foreach (var bann in banSet.CompanyBanners)
                        //            {
                        //                CompanyBanner CB = new CompanyBanner();
                        //                CB = bann;
                        //                CB.CompanySetId = banSet.CompanySetId;
                        //                db.CompanyBanners.Add(CB);
                        //            }
                        //        }
                        //        //    db.SaveChanges();
                        //    }
                        //}
                        //// import secondary pages
                        //if (objExpOrg.SecondaryPages != null && objExpOrg.SecondaryPages.Count > 0)
                        //{
                        //    foreach (var page in objExpOrg.SecondaryPages)
                        //    {
                        //        CmsPage CP = new CmsPage();
                        //        CP = page;
                        //        CP.Company = comp;
                        //        CP.OrganisationId = OrganisationID;
                        //        db.CmsPages.Add(CP);


                        //    }
                        //    //db.SaveChanges();
                        //}

                        //// import rave reviews
                        //if (objExpOrg.RaveReview != null && objExpOrg.RaveReview.Count > 0)
                        //{
                        //    foreach (var rave in objExpOrg.RaveReview)
                        //    {
                        //        RaveReview RR = new RaveReview();
                        //        RR = rave;

                        //        RR.Company = comp;
                        //        RR.OrganisationId = OrganisationID;
                        //        db.RaveReviews.Add(RR);
                        //    }
                        //    //db.SaveChanges();
                        //}

                        // import company territories

                        //if (objExpOrg.CompanyTerritory != null && objExpOrg.CompanyTerritory.Count > 0)
                        //{
                        //    foreach (var territory in objExpOrg.CompanyTerritory)
                        //    {
                        //        CompanyTerritory CT = new CompanyTerritory();
                        //        CT = territory;
                        //        CT.Company = comp;

                        //        db.CompanyTerritories.Add(CT);
                        //    }
                        //    //db.SaveChanges();
                        //}

                        //// import addresses
                        //if (objExpOrg.Address != null && objExpOrg.Address.Count > 0)
                        //{
                        //    foreach (var address in objExpOrg.Address)
                        //    {
                        //        Address add = new Address();
                        //        add = address;
                        //        add.Company = comp;
                        //        add.OrganisationId = OrganisationID;

                        //        db.Addesses.Add(add);
                        //    }
                        //    //db.SaveChanges();
                        //}
                        //// import contacts
                        //if (objExpOrg.CompanyContact != null && objExpOrg.CompanyContact.Count > 0)
                        //{
                        //    foreach (var contact in objExpOrg.CompanyContact)
                        //    {
                        //        CompanyContact CC = new CompanyContact();
                        //        CC = contact;
                        //        CC.Company = comp;
                        //        if (comp.Addresses != null)
                        //        {
                        //            CC.Address = comp.Addresses.FirstOrDefault();
                        //        }
                        //        else
                        //        {
                        //            CC.Address = db.Addesses.FirstOrDefault();
                        //        }

                        //        CC.OrganisationId = OrganisationID;
                        //        db.CompanyContacts.Add(CC);
                        //    }
                        //    //  db.SaveChanges();

                        //}


                        //// import product category
                        //if (objExpOrg.ProductCategory != null && objExpOrg.ProductCategory.Count > 0)
                        //{
                        //    foreach (var category in objExpOrg.ProductCategory)
                        //    {
                        //        ProductCategory PC = new ProductCategory();
                        //        PC = category;
                        //        PC.Company = db.Companies.FirstOrDefault();
                        //        PC.OrganisationId = OrganisationID;
                        //        //  comp.ProductCategories.Add(PC);
                        //        db.ProductCategories.Add(PC);
                        //    }
                        //    //  db.SaveChanges();

                        //}
                        ////  import items
                        //if (objExpOrg.Items != null && objExpOrg.Items.Count > 0)
                        //{
                        //    foreach (var item in objExpOrg.Items)
                        //    {
                        //        Item objitem = new Item();
                        //        objitem = item;
                        //        objitem.Company = db.Companies.FirstOrDefault();
                        //        objitem.OrganisationId = OrganisationID;
                        //        db.Items.Add(objitem);

                        //        if (item.ItemSections != null && item.ItemSections.Count > 0)
                        //        {
                        //            foreach (var sec in item.ItemSections)
                        //            {
                        //                ItemSection IS = new ItemSection();
                        //                IS = sec;
                        //                IS.ItemId = item.ItemId;
                        //                db.ItemSections.Add(IS);
                        //                // item.ItemSections.Add(IS);

                        //                if (sec.SectionCostcentres != null && sec.SectionCostcentres.Count > 0)
                        //                {
                        //                    foreach (var scc in sec.SectionCostcentres)
                        //                    {
                        //                        SectionCostcentre Objscc = new SectionCostcentre();
                        //                        Objscc = scc;
                        //                        Objscc.ItemSectionId = sec.ItemSectionId;
                        //                        db.SectionCostcentres.Add(Objscc);
                        //                        //sec.SectionCostcentres.Add(Objscc);
                        //                        if (scc.SectionCostCentreResources != null && scc.SectionCostCentreResources.Count > 0)
                        //                        {
                        //                            foreach (var sccr in scc.SectionCostCentreResources)
                        //                            {
                        //                                SectionCostCentreResource ObjSCCR = new SectionCostCentreResource();
                        //                                ObjSCCR = sccr;
                        //                                ObjSCCR.SectionCostcentreId = scc.SectionCostcentreId;
                        //                                db.SectionCostCentreResources.Add(ObjSCCR);
                        //                                // scc.SectionCostCentreResources.Add(ObjSCCR);

                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //            }


                        //        }

                        //    }


                        //}
                       //  db.SaveChanges();
                                        // import Entities Files

                                        // import organisation images

                                        //Organisation org = db.Organisations.Where(o => o.OrganisationId == OrganisationID).FirstOrDefault();

                         Organisation org = objOrg;
                         string DestinationMISLogoFilePath = string.Empty;
                         string DestinationWebSiteLogoFilePath = string.Empty;
                         string DestinationThumbPath = string.Empty;
                         string DestinationMainPath = string.Empty;
                         string DestinationContactFilesPath = string.Empty;
                         string DestinationMediaFilesPath = string.Empty;
                         string DestinationReportPath = string.Empty;
                         string DestinationThumbPathCat = string.Empty;
                         string DestinationThumbnailPath = string.Empty;
                         string DestinationImagePath = string.Empty;
                         string DestinationGridPath = string.Empty;
                         string DestinationFile1Path = string.Empty;
                         string DestinationFile2Path = string.Empty;
                         string DestinationFil3Path = string.Empty;
                         string DestinationFile4Path = string.Empty;
                         string DestinationFile5Path = string.Empty;
                         string DestinationSiteFile = string.Empty;
                         string DestinationSpriteFile = string.Empty;
                         string DestinationLanguageDirectory = string.Empty;
                         if (org != null)
                         {
                             // language Files
                             string Sourcelanguagefiles = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Resources/" + ImportIDs.OldOrganisationID);
                             
                             

                             foreach (string newPath in Directory.GetFiles(Sourcelanguagefiles, "*.*", SearchOption.AllDirectories))
                             {
                                 if (File.Exists(newPath))
                                 {
                                     
                                     string FileName = Path.GetFileName(newPath);
                                     string DestinationLanguageFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Resources/" + ImportIDs.NewOrganisationID + "/" + FileName);
                                     
                                     // define destination directory
                                      string directoty = Path.GetDirectoryName(newPath);
                                    string[] stringSeparators = new string[] { "MPC_Content" };
                                    if (!string.IsNullOrEmpty(directoty))
                                    {
                                        string[] result = directoty.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                                        string FolderName = result[1];
                                        if (!string.IsNullOrEmpty(FolderName))
                                        {
                                            string[] folder = FolderName.Split('\\');
                                            DestinationLanguageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Resources/" + ImportIDs.NewOrganisationID + "/" + folder[5]);

                                        }
                                    }
                                     
                                     if (!System.IO.Directory.Exists(DestinationLanguageDirectory))
                                     {
                                         Directory.CreateDirectory(DestinationLanguageDirectory);
                                         if (Directory.Exists(DestinationLanguageDirectory))
                                         {
                                             if (!File.Exists(DestinationLanguageFilePath))
                                                 File.Copy(newPath, DestinationLanguageFilePath);
                                         }
                                     }
                                     else
                                     {
                                         if (!File.Exists(DestinationLanguageFilePath))
                                             File.Copy(newPath, DestinationLanguageFilePath);
                                     }

                                 }

                             }
                            

                             
                             

                             // MIS Logo
                             string MISlogoPathOld = string.Empty;
                             string MISlogoPathNew = string.Empty;
                             if (org.MISLogo != null)
                             {
                                 MISlogoPathOld = Path.GetFileName(org.MISLogo);

                                 MISlogoPathNew = MISlogoPathOld.Replace(ImportIDs.OldOrganisationID + "_", ImportIDs.NewOrganisationID + "_");

                                 DestinationMISLogoFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Organisations/" + ImportIDs.NewOrganisationID + "/" + MISlogoPathNew);
                                 DestinationsPath.Add(DestinationMISLogoFilePath);
                                 string DestinationMISLogoDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Organisations/" + ImportIDs.NewOrganisationID);
                                 string MISLogoSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Organisations/" + ImportIDs.OldOrganisationID + "/" + MISlogoPathOld);
                                 if (!System.IO.Directory.Exists(DestinationMISLogoFilePath))
                                 {
                                     Directory.CreateDirectory(DestinationMISLogoDirectory);
                                     if (Directory.Exists(DestinationMISLogoDirectory))
                                     {
                                         if (File.Exists(MISLogoSourcePath))
                                         {
                                             if (!File.Exists(DestinationMISLogoFilePath))
                                                 File.Copy(MISLogoSourcePath, DestinationMISLogoFilePath);
                                         }


                                     }


                                 }
                                 else
                                 {
                                     if (File.Exists(MISLogoSourcePath))
                                     {
                                         if (!File.Exists(DestinationMISLogoFilePath))
                                             File.Copy(MISLogoSourcePath, DestinationMISLogoFilePath);
                                     }
                                 }

                             }


                             // website Logo

                             string WebsiteLogoOld = string.Empty;
                             string WebsiteLogoNew = string.Empty;
                             if (!string.IsNullOrEmpty(org.WebsiteLogo))
                             {
                                 WebsiteLogoOld = Path.GetFileName(org.WebsiteLogo);
                                 WebsiteLogoNew = WebsiteLogoOld.Replace(ImportIDs.OldOrganisationID + "_", ImportIDs.NewOrganisationID + "_");
                                 DestinationWebSiteLogoFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Organisations/" + ImportIDs.NewOrganisationID + "/" + WebsiteLogoNew);
                                 DestinationsPath.Add(DestinationWebSiteLogoFilePath);
                                 string DestinationWebSiteLogoDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Organisations/" + ImportIDs.NewOrganisationID);
                                 string WebSiteLogoSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Organisations/" + ImportIDs.OldOrganisationID + "/" + WebsiteLogoOld);
                                 if (!System.IO.Directory.Exists(DestinationWebSiteLogoDirectory))
                                 {
                                     Directory.CreateDirectory(DestinationWebSiteLogoDirectory);
                                     if (Directory.Exists(DestinationWebSiteLogoFilePath))
                                     {
                                         if (File.Exists(WebSiteLogoSourcePath))
                                         {
                                             if (!File.Exists(DestinationWebSiteLogoFilePath))
                                                 File.Copy(WebSiteLogoSourcePath, DestinationWebSiteLogoFilePath);
                                         }


                                     }


                                 }
                                 else
                                 {
                                     if (File.Exists(WebSiteLogoSourcePath))
                                     {
                                         if (!File.Exists(DestinationWebSiteLogoFilePath))
                                             File.Copy(WebSiteLogoSourcePath, DestinationWebSiteLogoFilePath);
                                     }

                                 }
                                 org.MISLogo = "/MPC_Content/Organisations/" + ImportIDs.NewOrganisationID + "/" + ImportIDs.NewOrganisationID + "_MISLogo.png";
                                 org.WebsiteLogo = "/MPC_Content/Organisations/" + ImportIDs.NewOrganisationID + "/" + ImportIDs.NewOrganisationID + "_WebstoreLogo.png";

                             }




                         }

                         // cost centre images

                         List<CostCentre> costcentres = db.CostCentres.Where(o => o.OrganisationId == OrganisationID).ToList();
                         if (costcentres != null && costcentres.Count > 0)
                         {
                             string OldCostCentreID = string.Empty;
                             string OldCostCentreName = string.Empty;
                             foreach (var cos in costcentres)
                             {
                                 // copy thumbnail images
                                 string OldThumbnailURL = string.Empty;
                                 string NewThumbnailURL = string.Empty;
                                 if (cos.ThumbnailImageURL != null)
                                 {
                                     // 123_costName_thumbnail.jpg"
                                     string FileName = Path.GetFileName(cos.ThumbnailImageURL);
                                     string[] SplitThumbnail = FileName.Split('_');

                                     if (SplitThumbnail[0] != string.Empty)
                                     {
                                         OldCostCentreID = SplitThumbnail[0];

                                     }


                                     OldThumbnailURL = Path.GetFileName(cos.ThumbnailImageURL);

                                     NewThumbnailURL = OldThumbnailURL.Replace(OldCostCentreID + "_", cos.CostCentreId + "_");

                                     DestinationThumbPath = HttpContext.Current.Server.MapPath("/MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID + "/" + NewThumbnailURL);
                                     DestinationsPath.Add(DestinationThumbPath);
                                     string SourceThumbPath = HttpContext.Current.Server.MapPath("/MPC_Content/CostCentres/" + ImportIDs.OldOrganisationID + "/" + OldThumbnailURL);
                                     string DestinationCostCentreDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID);
                                     if (!System.IO.Directory.Exists(DestinationCostCentreDirectory))
                                     {
                                         Directory.CreateDirectory(DestinationCostCentreDirectory);
                                         if (Directory.Exists(DestinationCostCentreDirectory))
                                         {
                                             if (File.Exists(SourceThumbPath))
                                             {
                                                 if (!File.Exists(DestinationThumbPath))
                                                     File.Copy(SourceThumbPath, DestinationThumbPath);
                                             }


                                         }


                                     }
                                     else
                                     {
                                         if (File.Exists(SourceThumbPath))
                                         {
                                             if (!File.Exists(DestinationThumbPath))
                                                 File.Copy(SourceThumbPath, DestinationThumbPath);
                                         }

                                     }
                                     cos.ThumbnailImageURL = "/MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID + "/" + NewThumbnailURL;
                                 }

                                 // copy image URLs
                                 string OldMainImageURL = string.Empty;
                                 string NewMainImageURL = string.Empty;
                                 if (cos.MainImageURL != null)
                                 {
                                     string name = Path.GetFileName(cos.MainImageURL);
                                     string[] SplitMain = name.Split('_');
                                     if (SplitMain[0] != string.Empty)
                                     {
                                         OldCostCentreID = SplitMain[0];

                                     }

                                     OldMainImageURL = Path.GetFileName(cos.MainImageURL);
                                     NewMainImageURL = OldMainImageURL.Replace(OldCostCentreID + "_", cos.CostCentreId + "_");
                                     DestinationMainPath = HttpContext.Current.Server.MapPath("/MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID + "/" + NewMainImageURL);
                                     DestinationsPath.Add(DestinationMainPath);


                                     string SourceMainPath = HttpContext.Current.Server.MapPath("/MPC_Content/CostCentres/" + ImportIDs.OldOrganisationID + "/" + OldMainImageURL);
                                     string DestinationCostCentreDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID);
                                     if (!System.IO.Directory.Exists(DestinationCostCentreDirectory))
                                     {
                                         Directory.CreateDirectory(DestinationCostCentreDirectory);
                                         if (Directory.Exists(DestinationCostCentreDirectory))
                                         {
                                             if (File.Exists(SourceMainPath))
                                             {
                                                 if (!File.Exists(DestinationMainPath))
                                                     File.Copy(SourceMainPath, DestinationMainPath);
                                             }


                                         }


                                     }
                                     else
                                     {
                                         if (File.Exists(SourceMainPath))
                                         {
                                             if (!File.Exists(DestinationMainPath))
                                                 File.Copy(SourceMainPath, DestinationMainPath);
                                         }

                                     }
                                     cos.MainImageURL = "/MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID + "/" + NewMainImageURL;
                                 }



                             }

                         }

                         // copy company files
                         Company ObjCompany = db.Companies.Where(c => c.CompanyId == oCID).FirstOrDefault();

                         if (ObjCompany != null)
                         {
                             // company logo
                             string CompanyPathOld = string.Empty;
                             string CompanylogoPathNew = string.Empty;
                             if (ObjCompany.Image != null)
                             {
                                 CompanyPathOld = Path.GetFileName(ObjCompany.Image);

                                 CompanylogoPathNew = CompanyPathOld.Replace(ImportIDs.OldCompanyID + "_", ObjCompany.CompanyId + "_");

                                 string DestinationCompanyLogoFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/" + CompanylogoPathNew);
                                 DestinationsPath.Add(DestinationCompanyLogoFilePath);
                                 string DestinationCompanyLogoDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId);
                                 string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ObjCompany.CompanyId + "/" + CompanyPathOld);
                                 if (!System.IO.Directory.Exists(DestinationCompanyLogoDirectory))
                                 {
                                     Directory.CreateDirectory(DestinationCompanyLogoDirectory);
                                     if (Directory.Exists(DestinationCompanyLogoDirectory))
                                     {
                                         if (File.Exists(CompanyLogoSourcePath))
                                         {
                                             if (!File.Exists(DestinationCompanyLogoFilePath))
                                                 File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
                                         }


                                     }


                                 }
                                 else
                                 {
                                     if (File.Exists(CompanyLogoSourcePath))
                                     {
                                         if (!File.Exists(DestinationCompanyLogoFilePath))
                                             File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
                                     }
                                 }

                             }


                             if (ObjCompany.CompanyContacts != null && ObjCompany.CompanyContacts.Count > 0)
                             {
                                 foreach (var contact in ObjCompany.CompanyContacts)
                                 {
                                     string OldContactImage = string.Empty;
                                     string NewContactImage = string.Empty;
                                     string OldContactID = string.Empty;
                                     if (contact.image != null)
                                     {
                                         string name = Path.GetFileName(contact.image);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             OldContactID = SplitMain[0];

                                         }

                                         OldContactImage = Path.GetFileName(contact.image);
                                         NewContactImage = OldContactImage.Replace(OldContactID + "_", contact.ContactId + "_");

                                         DestinationContactFilesPath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/Contacts/" + NewContactImage);
                                         DestinationsPath.Add(DestinationContactFilesPath);
                                         string DestinationContactFilesDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/Contacts/" + contact.ContactId);
                                         string ContactFilesSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/Contacts/" + OldContactImage);
                                         if (!System.IO.Directory.Exists(DestinationContactFilesDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationContactFilesDirectory);
                                             if (Directory.Exists(DestinationContactFilesDirectory))
                                             {
                                                 if (File.Exists(ContactFilesSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationContactFilesPath))
                                                         File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
                                                 }


                                             }



                                         }
                                         else
                                         {
                                             if (File.Exists(ContactFilesSourcePath))
                                             {
                                                 if (!File.Exists(DestinationContactFilesPath))
                                                     File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
                                             }

                                         }
                                         contact.image = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/Contacts/" + NewContactImage;
                                     }
                                 }
                             }

                             // copy media files
                             if (ObjCompany.MediaLibraries != null && ObjCompany.MediaLibraries.Count > 0)
                             {
                                 foreach (var media in ObjCompany.MediaLibraries)
                                 {
                                     string OldMediaFilePath = string.Empty;
                                     string NewMediaFilePath = string.Empty;
                                     string OldMediaID = string.Empty;
                                     if (media.FilePath != null)
                                     {
                                         string name = Path.GetFileName(media.FilePath);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             OldMediaID = SplitMain[0];

                                         }


                                         OldMediaFilePath = Path.GetFileName(media.FilePath);
                                         NewMediaFilePath = OldMediaFilePath.Replace(OldMediaID + "_", media.MediaId + "_");

                                         DestinationMediaFilesPath = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/" + NewMediaFilePath);
                                         DestinationsPath.Add(DestinationMediaFilesPath);
                                         string DestinationMediaFilesDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId);
                                         string MediaFilesSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Media/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/" + OldMediaFilePath);
                                         if (!System.IO.Directory.Exists(DestinationMediaFilesDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationMediaFilesDirectory);
                                             if (Directory.Exists(DestinationMediaFilesDirectory))
                                             {
                                                 if (File.Exists(MediaFilesSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationMediaFilesPath))
                                                         File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
                                                 }


                                             }



                                         }
                                         else
                                         {
                                             if (File.Exists(MediaFilesSourcePath))
                                             {
                                                 if (!File.Exists(DestinationMediaFilesPath))
                                                     File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
                                             }

                                         }
                                         media.FilePath = "/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/" + NewMediaFilePath;
                                     }

                                 }
                             }
                             List<ReportNote> notes = db.ReportNotes.Where(c => c.OrganisationId == OrganisationID).ToList();
                             if (notes != null && notes.Count > 0)
                             {
                                 foreach (var report in objExpOrg.ReportNote)
                                 {
                                     string ReportPathOld = string.Empty;
                                     string ReportPathNew = string.Empty;
                                     string ReportNoteID = string.Empty;
                                     if (report.ReportBanner != null)
                                     {
                                         string name = Path.GetFileName(report.ReportBanner);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ReportNoteID = SplitMain[0];

                                         }
                                         ReportPathOld = Path.GetFileName(report.ReportBanner);

                                         ReportPathNew = ReportPathOld.Replace(ReportNoteID + "_", report.Id + "_");

                                         DestinationReportPath = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + ReportPathNew);
                                         DestinationsPath.Add(DestinationReportPath);
                                         string DestinationReportDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID);
                                         string ReportSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Media/" + ImportIDs.OldOrganisationID + "/" + ReportPathOld);
                                         if (!System.IO.Directory.Exists(DestinationReportDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationReportDirectory);
                                             if (Directory.Exists(DestinationReportDirectory))
                                             {
                                                 if (File.Exists(ReportSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationReportPath))
                                                         File.Copy(ReportSourcePath, DestinationReportPath);
                                                 }


                                             }


                                         }
                                         else
                                         {
                                             if (File.Exists(ReportSourcePath))
                                             {
                                                 if (!File.Exists(DestinationReportPath))
                                                     File.Copy(ReportSourcePath, DestinationReportPath);
                                             }
                                         }
                                         report.ReportBanner = "/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + ReportPathNew;
                                     }

                                 }
                             }

                             if (ObjCompany.ProductCategories != null && ObjCompany.ProductCategories.Count > 0)
                             {
                                 foreach (var prodCat in ObjCompany.ProductCategories)
                                 {
                                     string ProdCatID = string.Empty;
                                     string CatName = string.Empty;

                                     if (!string.IsNullOrEmpty(prodCat.ThumbnailPath))
                                     {
                                         string OldThumbnailPath = string.Empty;
                                         string NewThumbnailPath = string.Empty;

                                         string name = Path.GetFileName(prodCat.ThumbnailPath);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ProdCatID = SplitMain[0];

                                         }

                                         OldThumbnailPath = Path.GetFileName(prodCat.ThumbnailPath);
                                         NewThumbnailPath = OldThumbnailPath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");



                                         DestinationThumbPathCat = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/ProductCategories/" + NewThumbnailPath);
                                         DestinationsPath.Add(DestinationThumbPathCat);
                                         string DestinationThumbDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/ProductCategories");
                                         string ThumbSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/ProductCategories/" + OldThumbnailPath);
                                         if (!System.IO.Directory.Exists(DestinationThumbDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationThumbDirectory);
                                             if (Directory.Exists(DestinationThumbDirectory))
                                             {
                                                 if (File.Exists(ThumbSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationThumbPathCat))
                                                         File.Copy(ThumbSourcePath, DestinationThumbPathCat);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(ThumbSourcePath))
                                             {
                                                 if (!File.Exists(DestinationThumbPathCat))
                                                     File.Copy(ThumbSourcePath, DestinationThumbPathCat);
                                             }

                                         }
                                         prodCat.ThumbnailPath = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/ProductCategories/" + NewThumbnailPath;
                                     }

                                     if (!string.IsNullOrEmpty(prodCat.ImagePath))
                                     {
                                         string OldImagePath = string.Empty;
                                         string NewImagePath = string.Empty;

                                         string name = Path.GetFileName(prodCat.ImagePath);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ProdCatID = SplitMain[0];

                                         }

                                         OldImagePath = Path.GetFileName(prodCat.ThumbnailPath);
                                         NewImagePath = OldImagePath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");

                                         DestinationImagePath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/ProductCategories/" + prodCat.ProductCategoryId + "_" + prodCat.CategoryName + "_Banner.png");
                                         DestinationsPath.Add(DestinationImagePath);
                                         string DestinationImageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/ProductCategories");
                                         string ImageSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/ProductCategories/" + ProdCatID + "_" + CatName + "_Banner.png");
                                         if (!System.IO.Directory.Exists(DestinationImageDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationImageDirectory);
                                             if (Directory.Exists(DestinationImageDirectory))
                                             {
                                                 if (File.Exists(ImageSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationImagePath))
                                                         File.Copy(ImageSourcePath, DestinationImagePath);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(ImageSourcePath))
                                             {
                                                 if (!File.Exists(DestinationImagePath))
                                                     File.Copy(ImageSourcePath, DestinationImagePath);
                                             }

                                         }
                                         prodCat.ImagePath = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/ProductCategories/" + NewImagePath;
                                     }


                                 }
                             }

                             if (ObjCompany.Items != null && ObjCompany.Items.Count > 0)
                             {
                                 string ItemID = string.Empty;
                                 string ItemName = string.Empty;
                                 foreach (var item in ObjCompany.Items)
                                 {
                                     // thumbnail images
                                     if (!string.IsNullOrEmpty(item.ThumbnailPath))
                                     {
                                         string OldThumbnailPath = string.Empty;
                                         string NewThumbnailPath = string.Empty;

                                         string name = Path.GetFileName(item.ThumbnailPath);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }
                                         OldThumbnailPath = Path.GetFileName(item.ThumbnailPath);
                                         NewThumbnailPath = OldThumbnailPath.Replace(ItemID + "_", item.ItemId + "_");


                                         DestinationThumbnailPath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewThumbnailPath);
                                         DestinationsPath.Add(DestinationThumbnailPath);
                                         string DestinationThumbnailDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string ThumbnailSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldThumbnailPath);
                                         if (!System.IO.Directory.Exists(DestinationThumbnailDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationThumbnailDirectory);
                                             if (Directory.Exists(DestinationThumbnailDirectory))
                                             {
                                                 if (File.Exists(ThumbnailSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationThumbnailPath))
                                                         File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(ThumbnailSourcePath))
                                             {
                                                 if (!File.Exists(DestinationThumbnailPath))
                                                     File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
                                             }

                                         }
                                         item.ThumbnailPath = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewThumbnailPath;
                                     }

                                     // main image
                                     if (!string.IsNullOrEmpty(item.ImagePath))
                                     {

                                         string OldImagePath = string.Empty;
                                         string NewImagePath = string.Empty;


                                         string name = Path.GetFileName(item.ImagePath);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }

                                         OldImagePath = Path.GetFileName(item.ImagePath);
                                         NewImagePath = OldImagePath.Replace(ItemID + "_", item.ItemId + "_");


                                         DestinationImagePath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewImagePath);
                                         DestinationsPath.Add(DestinationImagePath);
                                         string DestinationImageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string ImageSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldImagePath);
                                         if (!System.IO.Directory.Exists(DestinationImageDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationImageDirectory);
                                             if (Directory.Exists(DestinationImageDirectory))
                                             {
                                                 if (File.Exists(ImageSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationImagePath))
                                                         File.Copy(ImageSourcePath, DestinationImagePath);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(ImageSourcePath))
                                             {
                                                 if (!File.Exists(DestinationImagePath))
                                                     File.Copy(ImageSourcePath, DestinationImagePath);
                                             }

                                         }
                                         item.ImagePath = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewImagePath;
                                     }

                                     // Gird image
                                     if (!string.IsNullOrEmpty(item.GridImage))
                                     {
                                         string OldGridPath = string.Empty;
                                         string NewGridPath = string.Empty;

                                         string name = Path.GetFileName(item.GridImage);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }

                                         OldGridPath = Path.GetFileName(item.GridImage);
                                         NewGridPath = OldGridPath.Replace(ItemID + "_", item.ItemId + "_");

                                         DestinationGridPath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewGridPath);
                                         DestinationsPath.Add(DestinationGridPath);
                                         string DestinationGridDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string GridSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldGridPath);
                                         if (!System.IO.Directory.Exists(DestinationGridDirectory))
                                         {
                                             Directory.CreateDirectory(DestinationGridDirectory);
                                             if (Directory.Exists(DestinationGridDirectory))
                                             {
                                                 if (File.Exists(GridSourcePath))
                                                 {
                                                     if (!File.Exists(DestinationGridPath))
                                                         File.Copy(GridSourcePath, DestinationGridPath);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(GridSourcePath))
                                             {
                                                 if (!File.Exists(DestinationGridPath))
                                                     File.Copy(GridSourcePath, DestinationGridPath);

                                             }
                                         }
                                         item.GridImage = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewGridPath;
                                     }

                                     // file 1
                                     if (!string.IsNullOrEmpty(item.File1))
                                     {
                                         string OldF1Path = string.Empty;
                                         string NewF1Path = string.Empty;

                                         string name = Path.GetFileName(item.File1);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }

                                         OldF1Path = Path.GetFileName(item.File1);
                                         NewF1Path = OldF1Path.Replace(ItemID + "_", item.ItemId + "_");

                                         DestinationFile1Path = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF1Path);
                                         DestinationsPath.Add(DestinationFile1Path);
                                         string DestinationFile1Directory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string File1SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldF1Path);
                                         if (!System.IO.Directory.Exists(DestinationFile1Directory))
                                         {
                                             Directory.CreateDirectory(DestinationFile1Directory);
                                             if (Directory.Exists(DestinationFile1Directory))
                                             {
                                                 if (File.Exists(File1SourcePath))
                                                 {
                                                     if (!File.Exists(DestinationFile1Path))
                                                         File.Copy(File1SourcePath, DestinationFile1Path);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(File1SourcePath))
                                             {
                                                 if (!File.Exists(DestinationFile1Path))
                                                     File.Copy(File1SourcePath, DestinationFile1Path);
                                             }

                                         }
                                         item.File1 = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF1Path;

                                     }

                                     // file 2
                                     if (!string.IsNullOrEmpty(item.File2))
                                     {
                                         string OldF2Path = string.Empty;
                                         string NewF2Path = string.Empty;

                                         string name = Path.GetFileName(item.File2);

                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }

                                         OldF2Path = Path.GetFileName(item.File2);
                                         NewF2Path = OldF2Path.Replace(ItemID + "_", item.ItemId + "_");

                                         DestinationFile2Path = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF2Path);
                                         DestinationsPath.Add(DestinationFile2Path);
                                         string DestinationFile2Directory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string File2SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldF2Path);
                                         if (!System.IO.Directory.Exists(DestinationFile2Directory))
                                         {
                                             Directory.CreateDirectory(DestinationFile2Directory);
                                             if (Directory.Exists(DestinationFile2Directory))
                                             {
                                                 if (File.Exists(DestinationFile2Path))
                                                 {
                                                     if (!File.Exists(DestinationFile2Path))
                                                         File.Copy(DestinationFile2Path, DestinationFile2Path);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(DestinationFile2Path))
                                             {
                                                 if (!File.Exists(DestinationFile2Path))
                                                     File.Copy(DestinationFile2Path, DestinationFile2Path);
                                             }

                                         }
                                         item.File2 = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF2Path;
                                     }

                                     // file 3
                                     if (!string.IsNullOrEmpty(item.File3))
                                     {
                                         string OldF3Path = string.Empty;
                                         string NewF3Path = string.Empty;

                                         string name = Path.GetFileName(item.File3);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }

                                         OldF3Path = Path.GetFileName(item.File3);
                                         NewF3Path = OldF3Path.Replace(ItemID + "_", item.ItemId + "_");

                                         DestinationFil3Path = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF3Path);
                                         DestinationsPath.Add(DestinationFil3Path);
                                         string DestinationFile3Directory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string File3SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldF3Path);
                                         if (!System.IO.Directory.Exists(DestinationFile3Directory))
                                         {
                                             Directory.CreateDirectory(DestinationFile3Directory);
                                             if (Directory.Exists(DestinationFile3Directory))
                                             {
                                                 if (File.Exists(File3SourcePath))
                                                 {
                                                     if (!File.Exists(DestinationFil3Path))
                                                         File.Copy(File3SourcePath, DestinationFil3Path);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(File3SourcePath))
                                             {
                                                 if (!File.Exists(DestinationFil3Path))
                                                     File.Copy(File3SourcePath, DestinationFil3Path);
                                             }

                                         }
                                         item.File3 = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF3Path;
                                     }

                                     // file 4
                                     if (!string.IsNullOrEmpty(item.File4))
                                     {
                                         string OldF4Path = string.Empty;
                                         string NewF4Path = string.Empty;

                                         string name = Path.GetFileName(item.File4);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }

                                         OldF4Path = Path.GetFileName(item.File4);
                                         NewF4Path = OldF4Path.Replace(ItemID + "_", item.ItemId + "_");

                                         DestinationFile4Path = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF4Path);
                                         DestinationsPath.Add(DestinationFile4Path);
                                         string DestinationFile4Directory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string File4SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldF4Path);
                                         if (!System.IO.Directory.Exists(DestinationFile4Directory))
                                         {
                                             Directory.CreateDirectory(DestinationFile4Directory);
                                             if (Directory.Exists(DestinationFile4Directory))
                                             {
                                                 if (File.Exists(File4SourcePath))
                                                 {
                                                     if (!File.Exists(DestinationFile4Path))
                                                         File.Copy(File4SourcePath, DestinationFile4Path);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(File4SourcePath))
                                             {
                                                 if (!File.Exists(DestinationFile4Path))
                                                     File.Copy(File4SourcePath, DestinationFile4Path);
                                             }

                                         }
                                         item.File4 = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF4Path;
                                     }

                                     // file 5
                                     if (!string.IsNullOrEmpty(item.File5))
                                     {
                                         string OldF5Path = string.Empty;
                                         string NewF5Path = string.Empty;

                                         string name = Path.GetFileName(item.File5);
                                         string[] SplitMain = name.Split('_');
                                         if (SplitMain[0] != string.Empty)
                                         {
                                             ItemID = SplitMain[0];

                                         }

                                         OldF5Path = Path.GetFileName(item.File5);
                                         NewF5Path = OldF5Path.Replace(ItemID + "_", item.ItemId + "_");

                                         DestinationFile5Path = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF5Path);
                                         DestinationsPath.Add(DestinationFile5Path);
                                         string DestinationFile5Directory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                         string File5SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Products/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldF5Path);
                                         if (!System.IO.Directory.Exists(DestinationFile5Directory))
                                         {
                                             Directory.CreateDirectory(DestinationFile5Directory);
                                             if (Directory.Exists(DestinationFile5Directory))
                                             {
                                                 if (File.Exists(File5SourcePath))
                                                 {
                                                     if (!File.Exists(DestinationFile5Path))
                                                         File.Copy(File5SourcePath, DestinationFile5Path);
                                                 }


                                             }

                                         }
                                         else
                                         {
                                             if (File.Exists(File5SourcePath))
                                             {
                                                 if (!File.Exists(DestinationFile5Path))
                                                     File.Copy(File5SourcePath, DestinationFile5Path);
                                             }

                                         }
                                         item.File5 = "/MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF5Path;
                                     }

                                 }
                             }
                             // site.css
                             DestinationSiteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/Site.css");
                             DestinationsPath.Add(DestinationSiteFile);
                             string DestinationSiteFileDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId);
                             string SourceSiteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/Site.css");
                             if (!System.IO.Directory.Exists(DestinationSiteFileDirectory))
                             {
                                 Directory.CreateDirectory(DestinationSiteFileDirectory);
                                 if (Directory.Exists(DestinationSiteFileDirectory))
                                 {
                                     if (File.Exists(SourceSiteFile))
                                     {
                                         if (!File.Exists(DestinationSiteFile))
                                             File.Copy(SourceSiteFile, DestinationSiteFile);
                                     }


                                 }


                             }
                             else
                             {
                                 if (File.Exists(SourceSiteFile))
                                 {
                                     if (!File.Exists(DestinationSiteFile))
                                         File.Copy(SourceSiteFile, DestinationSiteFile);
                                 }

                             }

                             // sprite.png
                             DestinationSpriteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId + "/Sprite.png");
                             DestinationsPath.Add(DestinationSpriteFile);
                             string DestinationSpriteDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + ObjCompany.CompanyId);
                             string SourceSpriteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/Sprite.png");
                             if (!System.IO.Directory.Exists(DestinationSpriteDirectory))
                             {
                                 Directory.CreateDirectory(DestinationSpriteDirectory);
                                 if (Directory.Exists(DestinationSpriteDirectory))
                                 {
                                     if (!File.Exists(DestinationSpriteFile))
                                         File.Copy(SourceSpriteFile, DestinationSpriteFile);

                                 }
                                 else
                                 {
                                     if (!File.Exists(DestinationSpriteFile))
                                         File.Copy(SourceSpriteFile, DestinationSpriteFile);
                                 }


                             }
                             else
                             {
                                 if (!File.Exists(DestinationSpriteFile))
                                     File.Copy(SourceSpriteFile, DestinationSpriteFile);
                             }
                         }


                         db.SaveChanges();
                        dbContextTransaction.Commit(); 
                        
                       // 
                   // }
                }
                catch (Exception ex)
                {
                    
                    dbContextTransaction.Rollback();
                    
                    // Delete files if it was copied before exception
                    if(DestinationsPath != null)
                    {
                        foreach(string Path in DestinationsPath)
                        {
                            DeletePhysicallFiles(Path);
                        }
                    }

                    
                    throw ex;
                }
            }

            
                
        }
        
        public void DeletePhysicallFiles(string Path)
        {
            if(File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
        #endregion
    }
}
