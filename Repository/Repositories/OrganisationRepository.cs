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
using AutoMapper;
using System.Text.RegularExpressions;

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

            Mapper.CreateMap<Organisation, Organisation>()
           .ForMember(x => x.Country, opt => opt.Ignore())
           .ForMember(x => x.SystemUsers, opt => opt.Ignore())
           .ForMember(x => x.Estimates, opt => opt.Ignore())
           .ForMember(x => x.Currency, opt => opt.Ignore())
            .ForMember(x => x.CmsSkinPageWidgets, opt => opt.Ignore())
           .ForMember(x => x.State, opt => opt.Ignore());


            Organisation org = db.Organisations.Where(o => o.OrganisationId == organisationId).FirstOrDefault();
            var omappedItem = Mapper.Map<Organisation, Organisation>(org);


            return omappedItem;
        }
        public string InsertOrganisation(long OID, ExportOrganisation objExpCorporate, ExportOrganisation objExpRetail, bool isCorpStore, ExportSets Sets, string SubDomain, string timelog)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                List<string> DestinationsPath = new List<string>();
                try
                {
                    DateTime st = DateTime.Now;
                    DateTime end = DateTime.Now;

                 
                    long OrganisationID = 0;
                    Organisation newOrg = new Organisation();

                    ImportOrganisation ImportIDs = new ImportOrganisation();
                    ImportIDs.CostCentreIDs = new List<long>();
                    objExpCorporate = Sets.ExportStore1;
                    if (objExpCorporate.Company != null)
                    {
                        ImportIDs.OldCompanyID = objExpCorporate.Company.CompanyId;
                       
                    }
                    objExpRetail = Sets.ExportRetailStore1;
                    if (objExpRetail.RetailCompany != null)
                    {
                        ImportIDs.RetailOldCompanyID = objExpRetail.RetailCompany.CompanyId;
                    }

                    Organisation objOrg = db.Organisations.Where(o => o.OrganisationId == OID).FirstOrDefault();

                    Organisation objExpOrg = Sets.ExportOrganisationSet1.Organisation;
                    //objOrg.OrganisationName = objExpOrg.OrganisationName;

                 //   objOrg.OrganisationName = objExpOrg.Organisation.OrganisationName;

                    objOrg.Address1 = objExpOrg.Address1;
                    objOrg.Address2 = objExpOrg.Address2;
                    objOrg.Address3 = objExpOrg.Address3;
                    objOrg.City = objExpOrg.City;
                     objOrg.StateId = objExpOrg.StateId;
                     objOrg.CountryId = objExpOrg.CountryId;
                     objOrg.ZipCode = objExpOrg.ZipCode;
                     objOrg.Tel = objExpOrg.Tel;
                    objOrg.Fax = objExpOrg.Fax;
                    objOrg.Mobile = objExpOrg.Mobile;
                    objOrg.Email = objExpOrg.Email;
                    objOrg.URL = objExpOrg.URL;
                    objOrg.WebsiteLogo = objExpOrg.WebsiteLogo;
                    objOrg.MISLogo = objExpOrg.MISLogo;
                    objOrg.TaxRegistrationNo = objExpOrg.TaxRegistrationNo;
                    objOrg.LicenseLevel = objExpOrg.LicenseLevel;
                    objOrg.CustomerAccountNumber = objExpOrg.CustomerAccountNumber;
                    objOrg.SmtpServer = objExpOrg.SmtpServer;
                    objOrg.SmtpUserName = objExpOrg.SmtpUserName;
                    objOrg.SmtpPassword = objExpOrg.SmtpPassword;
                    objOrg.SystemWeightUnit = objExpOrg.SystemWeightUnit;
                    objOrg.CurrencyId = objExpOrg.CurrencyId;
                     objOrg.LanguageId = objExpOrg.LanguageId;
                     objOrg.BleedAreaSize = objExpOrg.BleedAreaSize;
                     objOrg.ShowBleedArea = objExpOrg.ShowBleedArea;
                     objOrg.isXeroIntegrationRequired = objExpOrg.isXeroIntegrationRequired;
                     objOrg.XeroApiId = objExpOrg.XeroApiId;
                     objOrg.XeroApiKey = objExpOrg.XeroApiKey;
                     objOrg.TaxServiceUrl = objExpOrg.TaxServiceUrl;
                    objOrg.TaxServiceKey = objExpOrg.TaxServiceKey;

                    db.SaveChanges();
                    ImportIDs.NewOrganisationID = OID;
                    ImportIDs.OldOrganisationID = objExpOrg.OrganisationId;
                    OrganisationID = OID;

                    end = DateTime.Now;
                    timelog += "organisation update " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;
                    //// save paper sizes
                    if (Sets.ExportOrganisationSet1.PaperSizes != null && Sets.ExportOrganisationSet1.PaperSizes.Count > 0)
                    {
                        foreach (var size in Sets.ExportOrganisationSet1.PaperSizes)
                        {
                            PaperSize Osize = new PaperSize();
                            Osize = size;
                            Osize.PaperSizeId = 0;
                            size.OrganisationId = OrganisationID;
                            db.PaperSizes.Add(size);


                        }
                        db.SaveChanges();

                    }


                    end = DateTime.Now;
                    timelog += "paper size insert " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    // save cost centres and its child objects
                    if (Sets.ExportOrganisationSet1.CostCentre != null && Sets.ExportOrganisationSet1.CostCentre.Count > 0)
                    {
                        foreach (var cost in Sets.ExportOrganisationSet1.CostCentre)
                        {
                            long CID = cost.CostCentreId;
                            ImportIDs.CostCentreIDs.Add(cost.CostCentreId);
                            int oldCostId = (int)cost.CostCentreId;
                            CostCentre cc = new CostCentre();

                            cc = cost;
                            cc.CostCentreId = 0;
                            cc.CCIDOption3 = oldCostId;
                            cc.OrganisationId = OrganisationID;
                            db.CostCentres.Add(cc);
                            // save cost centre instructions
                            if (cost.CostcentreInstructions != null && cost.CostcentreInstructions.Count > 0)
                            {
                                foreach (var ins in cost.CostcentreInstructions)
                                {
                                    CostcentreInstruction instruction = new CostcentreInstruction();

                                    instruction = ins;
                                    instruction.InstructionId = 0;
                                    instruction.CostCentreId = cc.CostCentreId;
                                    db.CostcentreInstructions.Add(ins);
                                    //db.CostcentreInstructions.Add(ins);
                                    //cc.CostcentreInstructions.Add(instruction);

                                    if (ins.CostcentreWorkInstructionsChoices != null && ins.CostcentreWorkInstructionsChoices.Count > 0)
                                    {
                                        foreach (var choice in ins.CostcentreWorkInstructionsChoices)
                                        {
                                            CostcentreWorkInstructionsChoice Objchoice = new CostcentreWorkInstructionsChoice();
                                            Objchoice = choice;
                                            Objchoice.Id = 0;
                                            Objchoice.InstructionId = instruction.InstructionId;

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
                                    resource.CostCentreId = cc.CostCentreId;
                                    //cc.CostcentreResources.Add(res);
                                    db.CostcentreResources.Add(res);
                                }

                            }



                            db.SaveChanges();

                        }



                    }
                    end = DateTime.Now;
                    timelog += "CostCentre insert" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    // cost centre questions answers
                    if (Sets.ExportOrganisationSet1.CostCentreQuestion != null & Sets.ExportOrganisationSet1.CostCentreQuestion.Count > 0)
                    {
                        foreach (var question in Sets.ExportOrganisationSet1.CostCentreQuestion)
                        {
                            long QID = question.Id;
                            CostCentreQuestion Objquestion = new CostCentreQuestion();
                            Objquestion = question;

                            Objquestion.Id = 0;
                            Objquestion.CompanyId = (int)OrganisationID;

                            db.CostCentreQuestions.Add(Objquestion);
                            db.SaveChanges();
                            List<CostCentreAnswer> answers = Sets.ExportOrganisationSet1.CostCentreAnswer.Where(q => q.QuestionId == QID).ToList();
                            if (answers != null && answers.Count > 0)
                            {
                                foreach (var ans in answers)
                                {
                                    CostCentreAnswer answer = new CostCentreAnswer();
                                    answer = ans;

                                    answer.QuestionId = Objquestion.Id;
                                    answer.Id = 0;
                                    db.CostCentreAnswers.Add(answer);
                                }
                                db.SaveChanges();
                            }


                        }


                    }
                    end = DateTime.Now;
                    timelog += "CostCentre Question insert" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    //List<long> NewIDMatrixIds = new List<long>();
                    Dictionary<long, long> NewIDMatrixIds = new Dictionary<long, long>();
                    // cost centre matrix and matrix detail
                    if (Sets.ExportOrganisationSet1.CostCentreMatrix != null && Sets.ExportOrganisationSet1.CostCentreMatrix.Count > 0)
                    {
                        foreach (var matrix in Sets.ExportOrganisationSet1.CostCentreMatrix)
                        {
                            int MID = matrix.MatrixId;
                            CostCentreMatrix Objmatrix = new CostCentreMatrix();
                            Objmatrix = matrix;
                            Objmatrix.MatrixId = 0;
                            Objmatrix.OrganisationId = (int)OrganisationID;
                            if (MID > 0)
                                Objmatrix.RowsCount = MID;
                            db.CostCentreMatrices.Add(Objmatrix);


                            // NewIDMatrixIds.Add(Objmatrix.MatrixId,MID);

                        }

                        db.SaveChanges();


                    }
                    
                    if (Sets.ExportOrganisationSet1.SuppliersList != null && Sets.ExportOrganisationSet1.SuppliersList.Count > 0)
                    {
                        foreach (var supplier in Sets.ExportOrganisationSet1.SuppliersList)
                        {
                            long SID = supplier.CompanyId;
                            Company comp = new Company();
                            comp = supplier;
                            comp.OrganisationId = OrganisationID;
                            comp.IsDisabled = 0;
                            comp.TaxPercentageId = (int)SID;

                            comp.CompanyDomains = null;


                            comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                            comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);
                            comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                            comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                            if (comp.CmsPages != null && comp.CmsPages.Count > 0)
                            {
                                comp.CmsPages.ToList().ForEach(x => x.PageCategory = null);
                                comp.CmsPages.ToList().ForEach(x => x.Company = null);
                                comp.CmsPages.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            }
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                            {
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsPage = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Company = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Organisation = null);

                            }

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                                comp.CompanyBannerSets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.RaveReviews != null && comp.RaveReviews.Count > 0)
                                comp.RaveReviews.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Addresses != null && comp.Addresses.Count > 0)
                                comp.Addresses.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyContacts != null && comp.CompanyContacts.Count > 0)
                                comp.CompanyContacts.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Campaigns != null && comp.Campaigns.Count > 0)
                                comp.Campaigns.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                                comp.CompanyCostCentres.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                                comp.CmsSkinPageWidgets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.FieldVariables != null && comp.FieldVariables.Count > 0)
                                comp.FieldVariables.ToList().ForEach(c => c.OrganisationId = OrganisationID);

                            db.Configuration.LazyLoadingEnabled = false;
                            db.Configuration.ProxyCreationEnabled = false;
                        

                            db.Companies.Add(comp);
                            //db.SaveChanges();
                           // oCID = comp.CompanyId;

                            // NewIDMatrixIds.Add(Objmatrix.MatrixId,MID);

                        }

                        db.SaveChanges();


                    }

                    List<CostCentreMatrix> Lstmatrix = db.CostCentreMatrices.Where(c => c.OrganisationId == OrganisationID).ToList();

                    if (Lstmatrix != null && Lstmatrix.Count > 0)
                    {
                        foreach (var matrix in Lstmatrix)
                        {

                            List<CostCentreMatrixDetail> matrixDetail = Sets.ExportOrganisationSet1.CostCentreMatrixDetail.Where(c => c.MatrixId == matrix.RowsCount).ToList();
                            if (matrixDetail != null && matrixDetail.Count > 0)
                            {

                                foreach (var MD in matrixDetail)
                                {
                                    CostCentreMatrixDetail CCMD = new CostCentreMatrixDetail();
                                    CCMD = MD;
                                    CCMD.Id = 0;
                                    CCMD.MatrixId = matrix.MatrixId;
                                    db.CostCentreMatrixDetails.Add(CCMD);
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                    //if (Sets.ExportOrganisationSet1.CostCentreMatrixDetail != null && Sets.ExportOrganisationSet1.CostCentreMatrixDetail.Count > 0)
                    //{
                    //    if(NewIDMatrixIds != null && NewIDMatrixIds.Count > 0)
                    //    {
                    //        foreach (var MatrixID in NewIDMatrixIds)
                    //        {
                    //            List<CostCentreMatrixDetail> matrixDetail = Sets.ExportOrganisationSet1.CostCentreMatrixDetail.Where(c => c.MatrixId == MatrixID.Value).ToList();
                    //            if (matrixDetail != null && matrixDetail.Count > 0)
                    //            {
                    //                foreach (var matrixD in matrixDetail)
                    //                {
                    //                    CostCentreMatrixDetail CCMD = new CostCentreMatrixDetail();
                    //                    CCMD = matrixD;
                    //                    CCMD.Id = 0;
                    //                    CCMD.MatrixId = (int)MatrixID.Key;
                    //                    db.CostCentreMatrixDetails.Add(CCMD);

                    //                }
                    //                db.SaveChanges();
                    //            }

                    //        }


                    //    }




                    //}
                    end = DateTime.Now;
                    timelog += "CostCentre Matrix insert " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    Dictionary<long, long> StockCatIds = new Dictionary<long, long>();
                    // Stock Categories
                    if (Sets.ExportOrganisationSet2.StockCategory != null && Sets.ExportOrganisationSet2.StockCategory.Count > 0)
                    {
                        foreach (var cat in Sets.ExportOrganisationSet2.StockCategory)
                        {
                            long gg = cat.CategoryId;
                            StockCategory SC = new StockCategory();
                            SC = cat;

                            SC.CategoryId = 0;
                            SC.OrganisationId = OrganisationID;
                            SC.TaxId = (int)gg;
                            //if(SC.StockItems != null)
                            //{
                            //    SC.StockItems.ToList().ForEach(s => s.OrganisationId = OrganisationID);
                            //}
                            db.StockCategories.Add(SC);


                            if (cat.StockSubCategories != null && cat.StockSubCategories.Count > 0)
                            {
                                foreach (var subCat in cat.StockSubCategories)
                                {
                                    string ggf = Convert.ToString(subCat.SubCategoryId);
                                   
                                    StockSubCategory SSC = new StockSubCategory();
                                    SSC = subCat;
                                    SSC.SubCategoryId = 0;
                                    SSC.CategoryId = SC.CategoryId;
                                    SSC.Description = ggf;
                                    db.StockSubCategories.Add(subCat);



                                }
                            }

                        }
                        db.SaveChanges();



                    }

                    end = DateTime.Now;
                    timelog += "stock category insert " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    //List<long> OldStockCatIds = new List<long>();
                    //// stock items



                    List<StockCategory> STOCKCat = db.StockCategories.Where(d => d.OrganisationId == OrganisationID).ToList();

                    if (STOCKCat != null && STOCKCat.Count > 0)
                    {
                        List<Company> Suppliers = db.Companies.Where(s => s.OrganisationId == OrganisationID && s.IsCustomer == 2).ToList();
                        foreach (var osc in STOCKCat)
                        {
                            if (osc.StockSubCategories != null && osc.StockSubCategories.Count > 0)
                            {

                                foreach (var sts in osc.StockSubCategories)
                                {
                                    //List<StockItem> stocks = Sets.ExportOrganisationSet4.StockItem.Where(c => c.CategoryId == osc.TaxId).ToList();
                                    List<StockItem> stocks = Sets.ExportOrganisationSet4.StockItem.Where(c => c.CategoryId == osc.TaxId && c.SubCategoryId == Convert.ToInt64(sts.Description)).ToList();
                                    if (stocks != null && stocks.Count > 0)
                                    {
                                        foreach (var s in stocks)
                                        {
                                            long OldSupplierID = s.SupplierId ?? 0;
                                            long OldStockID = s.StockItemId;
                                            StockItem objSI = new StockItem();
                                            objSI = s;
                                            if (Suppliers != null && Suppliers.Count > 0)
                                            {
                                                long NewSID = Suppliers.Where(c => c.TaxPercentageId == (int)OldSupplierID).Select(x => x.CompanyId).FirstOrDefault();
                                                if (NewSID > 0)
                                                {
                                                    objSI.SupplierId = NewSID;
                                                }
                                                else
                                                {
                                                    objSI.SupplierId = null;
                                                }
                                            }

                                            objSI.RollStandards = (int)OldStockID;
                                            objSI.StockItemId = 0;
                                            objSI.CategoryId = osc.CategoryId;
                                            objSI.SubCategoryId = sts.SubCategoryId;
                                            objSI.OrganisationId = OrganisationID;

                                            db.StockItems.Add(objSI);

                                        }
                                    }

                                }

                            }
                            else
                            {
                                List<StockItem> stocks = Sets.ExportOrganisationSet4.StockItem.Where(c => c.CategoryId == Convert.ToInt64(osc.Description)).ToList();
                                if (stocks != null && stocks.Count > 0)
                                {
                                   

                                    foreach (var s in stocks)
                                    {
                                        long OldSupplierID = s.SupplierId ?? 0;
                                        long OldStockID = s.StockItemId;
                                        StockItem objSI = new StockItem();
                                        objSI = s;
                                        if(Suppliers != null && Suppliers.Count > 0)
                                        {
                                            long NewSID = Suppliers.Where(c => c.TaxPercentageId == (int)OldSupplierID).Select(x => x.CompanyId).FirstOrDefault();
                                            if (NewSID > 0)
                                            {
                                                objSI.SupplierId = NewSID;
                                            }
                                            else
                                            {
                                                objSI.SupplierId = null;
                                            }
                                        }
                                        objSI.RollStandards = (int)OldStockID;
                                        objSI.StockItemId = 0;
                                        objSI.CategoryId = osc.CategoryId;
                                        objSI.SubCategoryId = null;
                                        objSI.OrganisationId = OrganisationID;

                                        db.StockItems.Add(objSI);

                                    }
                                }
                            }
                        }
                        db.SaveChanges();

                    }

                    
                    ////if (Sets.ExportOrganisationSet4.StockItem != null && Sets.ExportOrganisationSet4.StockItem.Count > 0)
                    ////{
                    ////    foreach (var Sitems in Sets.ExportOrganisationSet4.StockItem)
                    ////    {

                            
                    ////        StockItem SI = new StockItem();
                    ////        SI = Sitems;

                    ////        SI.StockItemId = 0;

                    ////        SI.OrganisationId = OrganisationID;
                    ////        db.StockItems.Add(SI);



                    ////    }
                    ////    db.SaveChanges();
                    ////}

                    //end = DateTime.Now;
                    //timelog += "stock item insert " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    //st = DateTime.Now;

                    // import reports
                    if (Sets.ExportOrganisationSet2.Reports != null && Sets.ExportOrganisationSet2.Reports.Count > 0)
                    {
                        foreach (var report in Sets.ExportOrganisationSet2.Reports)
                        {
                            Report rpt = new Report();
                            rpt = report;
                            rpt.ReportId = 0;
                            rpt.OrganisationId = OrganisationID;
                            db.Reports.Add(rpt);

                        }
                        db.SaveChanges();

                    }
                    end = DateTime.Now;
                    timelog += "reports insert" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    // import report notes
                    if (Sets.ExportOrganisationSet2.ReportNote != null && Sets.ExportOrganisationSet2.ReportNote.Count > 0)
                    {
                        foreach (var rptNotes in Sets.ExportOrganisationSet2.ReportNote)
                        {
                            ReportNote rptNote = new ReportNote();
                            rptNotes.Id = 0;
                            rptNotes.OrganisationId = OrganisationID;
                            db.ReportNotes.Add(rptNotes);

                        }
                        db.SaveChanges();
                    }

                    // import prefixes
                    if (Sets.ExportOrganisationSet2.Prefixes != null && Sets.ExportOrganisationSet2.Prefixes.Count > 0)
                    {
                        foreach (var prefix in Sets.ExportOrganisationSet2.Prefixes)
                        {
                            Prefix pref = new Prefix();
                            pref = prefix;
                            pref.PrefixId = 0;
                            pref.OrganisationId = OrganisationID;
                            db.Prefixes.Add(pref);

                        }
                        db.SaveChanges();
                    }
                    end = DateTime.Now;
                    timelog += "prefix insert" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    // import lookup methods
                    if (Sets.ExportOrganisationSet3.LookupMethods != null && Sets.ExportOrganisationSet3.LookupMethods.Count > 0)
                    {
                        foreach (var lookup in Sets.ExportOrganisationSet3.LookupMethods)
                        {
                            LookupMethod LM = new LookupMethod();
                            LM = lookup;

                            LM.MethodId = 0;
                            LM.OrganisationId = (int)OrganisationID;
                            db.LookupMethods.Add(LM);

                        }
                        db.SaveChanges();
                    }
                    end = DateTime.Now;
                    timelog += "looku method insert" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    // import phrase library
                    if (Sets.ExportOrganisationSet3.PhraseField != null && Sets.ExportOrganisationSet3.PhraseField.Count > 0)
                    {
                        foreach (var PF in Sets.ExportOrganisationSet3.PhraseField)
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
                                    objPh.FieldId = objPF.FieldId;
                                    objPh.CompanyId = OrganisationID;
                                    db.Phrases.Add(phrase);
                                    // PF.Phrases.Add(objPh);

                                }
                            }
                        }
                        db.SaveChanges();

                    }
                    end = DateTime.Now;
                    timelog += "phrase field insert" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    // import section flags
                    if (Sets.ExportOrganisationSet3.SectionFlags != null && Sets.ExportOrganisationSet3.SectionFlags.Count > 0)
                    {
                        foreach (var sflag in Sets.ExportOrganisationSet3.SectionFlags)
                        {
                            SectionFlag SF = new SectionFlag();
                            SF = sflag;
                            SF.SectionFlagId = 0;
                            SF.OrganisationId = OrganisationID;
                            db.SectionFlags.Add(SF);

                        }
                        db.SaveChanges();
                    }
                    end = DateTime.Now;
                    timelog += "section insert" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;
                    
                    //company flow 

                    // region to import corporate store
                   
                    // insert company
                    long oCID = 0;
                    long oRetailCID = 0;
                    List<CostCentre> costC = db.CostCentres.Where(c => c.OrganisationId == OrganisationID).ToList();
                     if(isCorpStore == true) // import corporate store
                     {
                         Company comp = new Company();
                         comp = objExpCorporate.Company;
                         comp.OrganisationId = OrganisationID;
                         comp.IsDisabled = 0;
                        

                         comp.CompanyDomains = null;

                      
                         comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                         comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);
                         comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                         comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                         if (comp.CmsPages != null && comp.CmsPages.Count > 0)
                         {
                             comp.CmsPages.ToList().ForEach(x => x.PageCategory = null);
                             comp.CmsPages.ToList().ForEach(x => x.Company = null);
                             comp.CmsPages.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         }
                         if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                         {
                             comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsPage = null);
                             comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Company = null);
                             comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Organisation = null);
                          
                         }

                         if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                             comp.CompanyBannerSets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.RaveReviews != null && comp.RaveReviews.Count > 0)
                             comp.RaveReviews.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.Addresses != null && comp.Addresses.Count > 0)
                             comp.Addresses.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.CompanyContacts != null && comp.CompanyContacts.Count > 0)
                             comp.CompanyContacts.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.Campaigns != null && comp.Campaigns.Count > 0)
                             comp.Campaigns.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                             comp.CompanyCostCentres.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                             comp.CmsSkinPageWidgets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.FieldVariables != null && comp.FieldVariables.Count > 0)
                             comp.FieldVariables.ToList().ForEach(c => c.OrganisationId = OrganisationID);

                         db.Configuration.LazyLoadingEnabled = false;
                         db.Configuration.ProxyCreationEnabled = false;
                         if(comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                         {
                             foreach(var ccc in comp.CompanyCostCentres)
                             {
                                 long id = costC.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == ccc.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();
                                 if (id > 0)
                                 {

                                     ccc.CostCentreId = id;
                                 }
                                 else
                                 {
                                     id = costC.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                     ccc.CostCentreId = id;
                                 }
                                 
                             }
                         }

                         db.Companies.Add(comp);
                         db.SaveChanges();
                         oCID = comp.CompanyId;

                         end = DateTime.Now;
                         timelog += "company add" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                         st = DateTime.Now;
                         
                         // add companydomain
                         string DomainName = SubDomain + "/store/" + objExpCorporate.Company.WebAccessCode;
                         CompanyDomain domain = new CompanyDomain();
                         domain.Domain = DomainName;
                         domain.CompanyId = oCID;
                         db.CompanyDomains.Add(domain);
                         db.SaveChanges();

                         end = DateTime.Now;
                         timelog += "company domain add" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                         st = DateTime.Now;
                         
                         //List<CmsPage> cmsPages = Sets.ExportStore4;
                         //if (cmsPages != null && cmsPages.Count > 0)
                         //{
                         //    foreach (var Page in cmsPages)
                         //    {
                         //        Page.OrganisationId = OrganisationID;
                         //        Page.PageCategory = null;
                         //        Page.CompanyId = oCID;
                         //        db.CmsPages.Add(Page);
                         //    }
                         //    db.SaveChanges();
                         //}

                         long OldCatIds = 0;
                         // product categories
                         List<ProductCategory> prodCats = Sets.ExportStore2;
                         if (prodCats != null && prodCats.Count > 0)
                         {
                             foreach (var cat in prodCats)
                             {
                                 if (cat.ProductCategoryId != null)
                                     cat.ContentType = cat.ProductCategoryId.ToString(); // 8888
                                 //if(cat.ParentCategoryId != null)
                                 //    cat.Description2 = cat.ParentCategoryId.ToString(); // 11859

                                 //cat.ParentCategoryId = null;
                                 cat.Sides = (int)cat.ProductCategoryId;
                                 cat.OrganisationId = OrganisationID;
                                 cat.CompanyId = oCID;
                                 db.ProductCategories.Add(cat);
                                 db.SaveChanges();



                                 ////  var gg = comp.Items.Where(c => c.ProductCategoryItems.t)
                                 //if (comp.Items != null && comp.Items.Count > 0)
                                 //{
                                 //    foreach (var itm in comp.Items)
                                 //    {
                                 //        if (itm.ProductCategoryItems != null)
                                 //        {
                                 //            List<ProductCategoryItem> pcis = itm.ProductCategoryItems.Where(c => c.CategoryId == OldCatIds).ToList();
                                 //            if (pcis != null && pcis.Count > 0)
                                 //            {
                                 //                foreach (var pc in pcis)
                                 //                {
                                 //                    pc.CategoryId = cat.ProductCategoryId;
                                 //                }
                                 //            }

                                 //        }



                                 //    }
                                 //    db.SaveChanges();
                                 //}



                             }


                         }


                         // 
                         if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                         {
                             foreach (var item in comp.ProductCategories)
                             {
                                 if (item.ParentCategoryId > 0) // 11859
                                 {


                                     //  string scat = item.Description2;
                                     var pCat = db.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
                                     if (pCat != null)
                                     {
                                         item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                                         db.SaveChanges();
                                     }
                                 }
                             }
                         }


                         //  import items
                         List<CostCentre> CostCentres = db.CostCentres.Where(c => c.OrganisationId == OrganisationID).ToList();
                         List<StockItem> stockitems = db.StockItems.Where(c => c.OrganisationId == OrganisationID).ToList();
                         List<Item> items = Sets.ExportStore3;
                         if (items != null && items.Count > 0)
                         {
                             foreach (var item in items)
                             {

                                 item.OrganisationId = OrganisationID;
                                 item.CompanyId = oCID;
                                 item.SmartFormId = null;
                                 if(item.ItemSections != null && item.ItemSections.Count > 0)
                                 {
                                     foreach(var itm in item.ItemSections)
                                     {
                                         if(stockitems != null && stockitems.Count > 0)
                                         {
                                             long SID = stockitems.Where(c => c.RollStandards == itm.StockItemID1).Select(s => s.StockItemId).FirstOrDefault();
                                             if (SID > 0)
                                             {
                                                 itm.StockItemID1 = SID;
                                             }
                                             else
                                             {
                                                 SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                 itm.StockItemID1 = SID;


                                             }
                                         }
                                         itm.PressId = null;
                                         
                                     }
                                 }
                                 if (item.ItemStockOptions != null && item.ItemStockOptions.Count > 0)
                                 {
                                     foreach (var iso in item.ItemStockOptions)
                                     {
                                         if (stockitems != null && stockitems.Count > 0)
                                         {
                                             long SID = stockitems.Where(c => c.RollStandards == iso.StockId).Select(s => s.StockItemId).FirstOrDefault();
                                             if (SID > 0)
                                             {
                                                 iso.StockId = SID;
                                             }
                                             else
                                             {
                                                 SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                 iso.StockId = SID;


                                             }
                                         }
                                         if(iso.ItemAddonCostCentres != null && iso.ItemAddonCostCentres.Count > 0)
                                         {
                                             foreach(var itmAdd in iso.ItemAddonCostCentres)
                                             {
                                                 if (CostCentres != null && CostCentres.Count > 0)
                                                 {

                                                     long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == itmAdd.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();
                                                     if (id > 0)
                                                     {

                                                         itmAdd.CostCentreId = id;
                                                     }
                                                     else
                                                     {
                                                         id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                                         itmAdd.CostCentreId = id;
                                                     }

                                                     
                                                 }
                                             }

                                            
                                         }


                                     }
                                 }
                                 if (item.ProductCategoryItems != null && item.ProductCategoryItems.Count > 0)
                                 {
                                     foreach (var pci in item.ProductCategoryItems)
                                     {
                                         if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                                         {
                                             long PID = comp.ProductCategories.Where(c => c.Sides == pci.CategoryId).Select(x => x.ProductCategoryId).FirstOrDefault();
                                             if (PID > 0)
                                             {
                                                 pci.CategoryId = PID;
                                             }
                                             else
                                             {
                                                 PID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                 pci.CategoryId = PID;


                                             }
                                         }

                                     }
                                 }

                                 if (item.ItemRelatedItems != null && item.ItemRelatedItems.Count > 0)
                                 {
                                     foreach (var pci in item.ItemRelatedItems)
                                     {
                                         pci.RelatedItemId = item.ItemId;
                                     }
                                 }

                                 db.Items.Add(item);

                             }

                             db.SaveChanges();

                         }
                         end = DateTime.Now;
                         timelog += "company items add" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                         st = DateTime.Now;
                         
                         //// product categories
                         //List<ProductCategory> prodCats = Sets.ExportStore2;
                         //if (prodCats != null && prodCats.Count > 0)
                         //{
                         //    foreach (var cat in prodCats)
                         //    {
                         //        cat.OrganisationId = OrganisationID;
                         //        cat.CompanyId = oCID;
                         //        db.ProductCategories.Add(cat);

                         //    }
                         //    db.SaveChanges();
                         //}

                        // List<long> OldCatIds = new List<long>();
                        
                         //7
                         end = DateTime.Now;
                         timelog += "product category add" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                         st = DateTime.Now;
                         if (objExpCorporate.TemplateColorStyle != null && objExpCorporate.TemplateColorStyle.Count > 0)
                         {
                             foreach(var color in objExpCorporate.TemplateColorStyle)
                             {
                                 TemplateColorStyle objColor = new TemplateColorStyle();
                                 objColor.CustomerId = (int)oCID;
                                 db.TemplateColorStyles.Add(objColor);
                             }
                             db.SaveChanges();
                         }
                         end = DateTime.Now;
                         timelog += "template color style add" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                         st = DateTime.Now;
                     }
                     else // import retail store
                     {

                         Company comp = new Company();
                         comp = objExpRetail.RetailCompany;
                         comp.OrganisationId = OrganisationID;
                         comp.IsDisabled = 0;
                         comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                         comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);

                         //comp.CompanyContacts.ToList().ForEach(c => c.TerritoryId = null);
                         //comp.CompanyContacts.ToList().ForEach(c => c.AddressId = null);
                         comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                         comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                         if (comp.CmsPages != null && comp.CmsPages.Count > 0)
                         {
                             comp.CmsPages.ToList().ForEach(x => x.PageCategory = null);
                             comp.CmsPages.ToList().ForEach(x => x.Company = null);
                         }
                         if(comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                         {
                             comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsPage = null);
                             comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Company = null);
                             comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Organisation = null);
                             comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsSkinPageWidgetParams = null);
                         }
                        


                         // setting organisationid 

                         if(comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                              comp.CompanyBannerSets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if(comp.RaveReviews != null && comp.RaveReviews.Count > 0)
                              comp.RaveReviews.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if(comp.Addresses != null && comp.Addresses.Count > 0)
                              comp.Addresses.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.CompanyContacts != null && comp.CompanyContacts.Count > 0)
                             comp.CompanyContacts.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.Campaigns != null && comp.Campaigns.Count > 0)
                             comp.Campaigns.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                             comp.CompanyCostCentres.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                            comp.CmsSkinPageWidgets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                         if (comp.FieldVariables != null && comp.FieldVariables.Count > 0)
                             comp.FieldVariables.ToList().ForEach(c => c.OrganisationId = OrganisationID);


                         //comp.CmsPages.ToList().ForEach(c => c.)
                         db.Companies.Add(comp);
                         db.SaveChanges();
                         oRetailCID = comp.CompanyId;


                         List<CmsPage> cmsPages = Sets.ExportRetailStore4;
                         if (cmsPages != null && cmsPages.Count > 0)
                         {
                             foreach(var Page in cmsPages)
                             {
                               
                                 Page.PageCategory = null;
                                 Page.CompanyId = oRetailCID;
                                 Page.OrganisationId = OrganisationID;
                                 db.CmsPages.Add(Page);
                             }
                             db.SaveChanges();
                         }
                         //  import items
                         List<Item> items = Sets.ExportRetailStore3;
                         if (items != null && items.Count > 0)
                         {
                             foreach (var item in items)
                             {
                          
                                 item.OrganisationId = OrganisationID;
                                 item.CompanyId = oRetailCID;
                                 db.Items.Add(item);

                             }

                             db.SaveChanges();

                         }


                         // product categories
                         List<ProductCategory> prodCats = Sets.ExportRetailStore2;
                         if (prodCats != null && prodCats.Count > 0)
                         {
                             foreach (var cat in prodCats)
                             {
                                 if(cat.ProductCategoryId != null)
                                     cat.ContentType = cat.ProductCategoryId.ToString(); // 8888
                                 //if(cat.ParentCategoryId != null)
                                 //    cat.Description2 = cat.ParentCategoryId.ToString(); // 11859

                                 //cat.ParentCategoryId = null;
                                 cat.OrganisationId = OrganisationID;
                                 cat.CompanyId = oRetailCID;
                                 db.ProductCategories.Add(cat);
                                
                             }
                             db.SaveChanges();
                         }


                         // 
                         if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                         {
                             foreach(var item in comp.ProductCategories)
                             {
                                 if (item.ParentCategoryId > 0) // 11859
                                 {


                                   //  string scat = item.Description2;
                                     var pCat = db.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
                                     if (pCat != null)
                                     {
                                         item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                                         db.SaveChanges();
                                     }
                                 }
                             }
                         }
                         //


                         if (objExpRetail.RetailTemplateColorStyle != null && objExpRetail.RetailTemplateColorStyle.Count > 0)
                         {
                             foreach (var color in objExpRetail.RetailTemplateColorStyle)
                             {
                                 TemplateColorStyle objColor = new TemplateColorStyle();
                                 objColor.CustomerId = (int)oCID;
                                 db.TemplateColorStyles.Add(objColor);
                             }
                             db.SaveChanges();
                         }


                     }
                    
                

                         Organisation org = objOrg;
                         string DestinationMISLogoFilePath = string.Empty;
                         string DestinationWebSiteLogoFilePath = string.Empty;
                         string DestinationThumbPath = string.Empty;
                         string DestinationMainPath = string.Empty;
                         string DestinationReportPath = string.Empty;
                         
                         string DestinationLanguageDirectory = string.Empty;
                         string DestinationLanguageFilePath = string.Empty;
                         end = DateTime.Now;
                         timelog += "start copying organisation files" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                         st = DateTime.Now;
                         if (org != null)
                         {
                             // language Files
                             string Sourcelanguagefiles = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Resources/" + ImportIDs.OldOrganisationID);

                             DestinationLanguageFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Resources/" + ImportIDs.NewOrganisationID);

                             if (Directory.Exists(Sourcelanguagefiles))
                                 Copy(Sourcelanguagefiles, DestinationLanguageFilePath);



                             //if (Directory.Exists(Sourcelanguagefiles))
                             //{
                             //    foreach (string newPath in Directory.GetFiles(Sourcelanguagefiles, "*.*", SearchOption.AllDirectories))
                             //    {
                             //        if (File.Exists(newPath))
                             //        {

                             //            string FileName = Path.GetFileName(newPath);

                             //            DestinationLanguageFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Resources/" + ImportIDs.NewOrganisationID + "/" + FileName);


                             //            // define destination directory
                             //            string directoty = Path.GetDirectoryName(newPath);
                             //            string[] stringSeparators = new string[] { "MPC_Content" };
                             //            if (!string.IsNullOrEmpty(directoty))
                             //            {
                             //                string[] result = directoty.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                             //                string FolderName = result[1];
                             //                if (!string.IsNullOrEmpty(FolderName))
                             //                {
                             //                    string[] folder = FolderName.Split('\\');
                             //                    DestinationLanguageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Resources/" + ImportIDs.NewOrganisationID + "/" + folder[5]);

                             //                    DestinationLanguageFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Resources/" + ImportIDs.NewOrganisationID + "/" + folder[5] + "/" + FileName);

                             //                }
                             //            }

                             //            if (!System.IO.Directory.Exists(DestinationLanguageDirectory))
                             //            {
                             //                Directory.CreateDirectory(DestinationLanguageDirectory);
                             //                if (Directory.Exists(DestinationLanguageDirectory))
                             //                {
                             //                    if (!File.Exists(DestinationLanguageFilePath))
                             //                        File.Copy(newPath, DestinationLanguageFilePath);
                             //                }
                             //            }
                             //            else
                             //            {
                             //                if (!File.Exists(DestinationLanguageFilePath))
                             //                    File.Copy(newPath, DestinationLanguageFilePath);
                             //            }

                             //        }

                             //    }

                             //}





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
                                 org.MISLogo = "MPC_Content/Organisations/" + ImportIDs.NewOrganisationID + "/" + ImportIDs.NewOrganisationID + "_MISLogo.png";
                                 org.WebsiteLogo = "MPC_Content/Organisations/" + ImportIDs.NewOrganisationID + "/" + ImportIDs.NewOrganisationID + "_WebstoreLogo.png";

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
                                     cos.ThumbnailImageURL = "MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID + "/" + NewThumbnailURL;
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
                                     cos.MainImageURL = "MPC_Content/CostCentres/" + ImportIDs.NewOrganisationID + "/" + NewMainImageURL;
                                 }



                             }

                         }
                         // copy report banners

                         List<ReportNote> notes = db.ReportNotes.Where(c => c.OrganisationId == OrganisationID).ToList();
                         if (notes != null && notes.Count > 0)
                         {
                             foreach (var report in Sets.ExportOrganisationSet2.ReportNote)
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
                                     report.ReportBanner = "MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + ReportPathNew;
                                 }

                             }
                         }
                         end = DateTime.Now;
                         timelog += "end copying organisation files" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                         st = DateTime.Now; 
                        // copy company files
                         if (isCorpStore == true)// copy corporate store
                         {
                             end = DateTime.Now;
                             timelog += "start copying corporate files" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                             st = DateTime.Now; 

                             CopyCorporateCompanyFiles(oCID, DestinationsPath, ImportIDs);

                             end = DateTime.Now;
                             timelog += "end copying corporate files" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                             st = DateTime.Now; 
                         }
                         else // copy retail store
                         {
                             CopyRetailCompanyFiles(oRetailCID, DestinationsPath, ImportIDs);
                         }
                       

                         db.SaveChanges();
                        dbContextTransaction.Commit();

                        string SourceImportOrg = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation");

                        if(Directory.Exists(SourceImportOrg))
                        {


                            Directory.Delete(SourceImportOrg,true);
                        }

                        return timelog;
                        
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

        public void CopyCorporateCompanyFiles(long oCID, List<string> DestinationsPath,ImportOrganisation ImportIDs)
        {
            try
            {
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
                         string DestinationLanguageFilePath = string.Empty;
                         string DestinationItemAttachmentsPath = string.Empty;
                         string DestinationFont1 = string.Empty;
                         string DestinationFont2 = string.Empty;
                         string DestinationFont3 = string.Empty;
                         

                Company ObjCompany = db.Companies.Where(c => c.CompanyId == oCID).FirstOrDefault();

                if (ObjCompany != null)
                {
                    // company logo
                    string CompanyPathOld = string.Empty;
                    string CompanylogoPathNew = string.Empty;
                    if (ObjCompany.Image != null)
                    {
                        CompanyPathOld = Path.GetFileName(ObjCompany.Image);

                        CompanylogoPathNew = CompanyPathOld.Replace(ImportIDs.OldCompanyID + "_", oCID + "_");

                        string DestinationCompanyLogoFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + CompanylogoPathNew);
                        DestinationsPath.Add(DestinationCompanyLogoFilePath);
                        string DestinationCompanyLogoDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID);
                        string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/" + CompanyPathOld);
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
                        ObjCompany.Image = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + CompanylogoPathNew;

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

                                DestinationContactFilesPath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Contacts/" + contact.ContactId + "/" + NewContactImage);
                                DestinationsPath.Add(DestinationContactFilesPath);
                                string DestinationContactFilesDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Contacts/" + contact.ContactId);
                                string ContactFilesSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/Contacts/" + OldContactID + "/" + OldContactImage);

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
                                contact.image = "MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Contacts/" + contact.ContactId + "/" + NewContactImage;

                            }
                        }
                    }
                    Dictionary<string, string> dictionaryMediaIds = new Dictionary<string, string>();
                    // copy media files
                    if (ObjCompany.MediaLibraries != null && ObjCompany.MediaLibraries.Count > 0)
                    {
                        foreach (var media in ObjCompany.MediaLibraries)
                        {
                            string OldMediaFilePath = string.Empty;
                            string NewMediaFilePath = string.Empty;
                            string OldMediaID = string.Empty;
                            string NewMediaID = string.Empty;
                            if (media.FilePath != null)
                            {
                                string name = Path.GetFileName(media.FilePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    OldMediaID = SplitMain[0];

                                }
                                if (media.MediaId > 0)
                                    NewMediaID = Convert.ToString(media.MediaId);

                                dictionaryMediaIds.Add(OldMediaID, NewMediaID);

                                OldMediaFilePath = Path.GetFileName(media.FilePath);
                                NewMediaFilePath = OldMediaFilePath.Replace(OldMediaID + "_", media.MediaId + "_");

                                DestinationMediaFilesPath = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + NewMediaFilePath);
                                DestinationsPath.Add(DestinationMediaFilesPath);
                                string DestinationMediaFilesDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID);
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
                                media.FilePath = "MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + NewMediaFilePath;

                                // set banners path

                            }

                        }
                    }

                    if (ObjCompany.CompanyBannerSets != null && ObjCompany.CompanyBannerSets.Count > 0)
                    {
                        foreach (var sets in ObjCompany.CompanyBannerSets)
                        {


                            if (sets.CompanyBanners != null && sets.CompanyBanners.Count > 0)
                            {
                                foreach (var bann in sets.CompanyBanners)
                                {
                                    if (!string.IsNullOrEmpty(bann.ImageURL))
                                    {
                                        string OldMediaID = string.Empty;
                                        string newMediaID = string.Empty;
                                        string name = Path.GetFileName(bann.ImageURL);
                                        string[] SplitMain = name.Split('_');
                                        if (SplitMain[0] != string.Empty)
                                        {
                                            OldMediaID = SplitMain[0];

                                        }
                                        if(dictionaryMediaIds != null && dictionaryMediaIds.Count > 0)
                                          newMediaID = dictionaryMediaIds.Where(s => s.Key == OldMediaID).Select(s => s.Value).FirstOrDefault().ToString();

                                        string NewBannerPath = name.Replace(OldMediaID + "_", newMediaID + "_");

                                        bann.ImageURL = "MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + NewBannerPath;
                                    }
                                }
                            }

                        }
                    }

                    if (ObjCompany.CmsPages != null && ObjCompany.CmsPages.Count > 0)
                    {
                        foreach (var pages in ObjCompany.CmsPages)
                        {
                            if (!string.IsNullOrEmpty(pages.PageBanner))
                            {
                                //string OldMediaID = string.Empty;
                                //string newMediaID = string.Empty;
                                string name = Path.GetFileName(pages.PageBanner);
                                pages.PageBanner = "MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + name;
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
                                if (SplitMain[1] != string.Empty)
                                {
                                    ProdCatID = SplitMain[1];

                                }

                                OldThumbnailPath = Path.GetFileName(prodCat.ThumbnailPath);
                                NewThumbnailPath = OldThumbnailPath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");



                                DestinationThumbPathCat = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewThumbnailPath);
                                DestinationsPath.Add(DestinationThumbPathCat);
                                string DestinationThumbDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories");
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
                                prodCat.ThumbnailPath = "MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewThumbnailPath;
                            }

                            if (!string.IsNullOrEmpty(prodCat.ImagePath))
                            {
                                string OldImagePath = string.Empty;
                                string NewImagePath = string.Empty;

                                string name = Path.GetFileName(prodCat.ImagePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ProdCatID = SplitMain[1];

                                }

                                OldImagePath = Path.GetFileName(prodCat.ImagePath);
                                NewImagePath = OldImagePath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");

                                DestinationImagePath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewImagePath);
                                DestinationsPath.Add(DestinationImagePath);
                                string DestinationImageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories");
                                string ImageSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/ProductCategories/" + OldImagePath);
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
                                prodCat.ImagePath = "MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewImagePath;
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
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                item.ThumbnailPath = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewThumbnailPath;
                            }

                            // main image
                            if (!string.IsNullOrEmpty(item.ImagePath))
                            {

                                string OldImagePath = string.Empty;
                                string NewImagePath = string.Empty;


                                string name = Path.GetFileName(item.ImagePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                item.ImagePath = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewImagePath;
                            }

                            // Gird image
                            if (!string.IsNullOrEmpty(item.GridImage))
                            {
                                string OldGridPath = string.Empty;
                                string NewGridPath = string.Empty;

                                string name = Path.GetFileName(item.GridImage);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                item.GridImage = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewGridPath;
                            }

                            // file 1
                            if (!string.IsNullOrEmpty(item.File1))
                            {
                                string OldF1Path = string.Empty;
                                string NewF1Path = string.Empty;

                                string name = Path.GetFileName(item.File1);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                item.File1 = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF1Path;

                            }

                            // file 2
                            if (!string.IsNullOrEmpty(item.File2))
                            {
                                string OldF2Path = string.Empty;
                                string NewF2Path = string.Empty;

                                string name = Path.GetFileName(item.File2);

                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                        if (File.Exists(File2SourcePath))
                                        {
                                            if (!File.Exists(DestinationFile2Path))
                                                File.Copy(File2SourcePath, DestinationFile2Path);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(File2SourcePath))
                                    {
                                        if (!File.Exists(DestinationFile2Path))
                                            File.Copy(File2SourcePath, DestinationFile2Path);
                                    }

                                }
                                item.File2 = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF2Path;
                            }

                            // file 3
                            if (!string.IsNullOrEmpty(item.File3))
                            {
                                string OldF3Path = string.Empty;
                                string NewF3Path = string.Empty;

                                string name = Path.GetFileName(item.File3);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                item.File3 = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF3Path;
                            }

                            // file 4
                            if (!string.IsNullOrEmpty(item.File4))
                            {
                                string OldF4Path = string.Empty;
                                string NewF4Path = string.Empty;

                                string name = Path.GetFileName(item.File4);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                item.File4 = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF4Path;
                            }

                            // file 5
                            if (!string.IsNullOrEmpty(item.File5))
                            {
                                string OldF5Path = string.Empty;
                                string NewF5Path = string.Empty;

                                string name = Path.GetFileName(item.File5);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                item.File5 = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewF5Path;
                            }
                            if(item.TemplateId != null && item.TemplateId > 0)
                            {
                                if(item.DesignerCategoryId == 0 && item.DesignerCategoryId == null)
                                {
                                    if (item.Template != null)
                                    {
                                        if (item.Template.TemplateFonts != null && item.Template.TemplateFonts.Count > 0)
                                        {
                                            foreach (var fonts in item.Template.TemplateFonts)
                                            {
                                                string DestinationFontDirectory = string.Empty;
                                                string FontSourcePath = string.Empty;
                                                string FontSourcePath1 = string.Empty;
                                                string FontSourcePath2 = string.Empty;

                                                if (!string.IsNullOrEmpty(fonts.FontPath))
                                                {
                                                    DestinationFont1 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".eot");

                                                    DestinationFont2 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".ttf");

                                                    DestinationFont3 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".woff");

                                                    DestinationFontDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath);

                                                    FontSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".eot");

                                                    FontSourcePath1 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".ttf");

                                                    FontSourcePath2 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".woff");

                                                    if (!System.IO.Directory.Exists(DestinationFontDirectory))
                                                    {
                                                        Directory.CreateDirectory(DestinationFontDirectory);
                                                        if (Directory.Exists(DestinationFontDirectory))
                                                        {
                                                            if (File.Exists(FontSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationFont1))
                                                                    File.Copy(FontSourcePath, DestinationFont1);
                                                            }

                                                            if (File.Exists(FontSourcePath1))
                                                            {
                                                                if (!File.Exists(DestinationFont2))
                                                                    File.Copy(FontSourcePath1, DestinationFont2);

                                                            }

                                                            if (File.Exists(FontSourcePath2))
                                                            {
                                                                if (!File.Exists(DestinationFont3))
                                                                    File.Copy(FontSourcePath2, DestinationFont3);

                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(FontSourcePath))
                                                        {
                                                            if (!File.Exists(DestinationFont1))
                                                                File.Copy(FontSourcePath, DestinationFont1);
                                                        }

                                                        if (File.Exists(FontSourcePath1))
                                                        {
                                                            if (!File.Exists(DestinationFont2))
                                                                File.Copy(FontSourcePath1, DestinationFont2);

                                                        }

                                                        if (File.Exists(FontSourcePath2))
                                                        {
                                                            if (!File.Exists(DestinationFont3))
                                                                File.Copy(FontSourcePath2, DestinationFont3);

                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    DestinationFont1 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontFile + ".eot");
                                                    DestinationFont2 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontFile + ".ttf");
                                                    DestinationFont3 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontFile + ".woff");

                                                    DestinationFontDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts");

                                                    FontSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontFile + ".eot");

                                                    FontSourcePath1 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontFile + ".ttf");

                                                    FontSourcePath2 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontFile + ".woff");

                                                    if (!System.IO.Directory.Exists(DestinationFontDirectory))
                                                    {
                                                        Directory.CreateDirectory(DestinationFontDirectory);
                                                        if (Directory.Exists(DestinationFontDirectory))
                                                        {
                                                            if (File.Exists(FontSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationFont1))
                                                                    File.Copy(FontSourcePath, DestinationFont1);
                                                            }

                                                            if (File.Exists(FontSourcePath1))
                                                            {
                                                                if (!File.Exists(DestinationFont2))
                                                                    File.Copy(FontSourcePath1, DestinationFont2);

                                                            }

                                                            if (File.Exists(FontSourcePath2))
                                                            {
                                                                if (!File.Exists(DestinationFont3))
                                                                    File.Copy(FontSourcePath2, DestinationFont3);

                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(FontSourcePath))
                                                        {
                                                            if (!File.Exists(DestinationFont1))
                                                                File.Copy(FontSourcePath, DestinationFont1);
                                                        }

                                                        if (File.Exists(FontSourcePath1))
                                                        {
                                                            if (!File.Exists(DestinationFont2))
                                                                File.Copy(FontSourcePath1, DestinationFont2);

                                                        }

                                                        if (File.Exists(FontSourcePath2))
                                                        {
                                                            if (!File.Exists(DestinationFont3))
                                                                File.Copy(FontSourcePath2, DestinationFont3);

                                                        }

                                                    }

                                                }

                                                DestinationsPath.Add(DestinationFont1);
                                                DestinationsPath.Add(DestinationFont2);
                                                DestinationsPath.Add(DestinationFont3);


                                            }

                                        }
                                        // template background images
                                        if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                                        {
                                            foreach(var tempImg in item.Template.TemplateBackgroundImages)
                                            {
                                                if(!string.IsNullOrEmpty(tempImg.ImageName))
                                                {
                                                    string name = tempImg.ImageName;

                                                    string ImageName = Path.GetFileName(tempImg.ImageName);

                                                    string NewPath = tempImg.ProductId + "/" + ImageName;

                                                    string[] tempID = tempImg.ImageName.Split('/');

                                                    string OldTempID = tempID[0];
                                                 
                                                
                                                    string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/Templates/"  + NewPath);
                                                    DestinationsPath.Add(DestinationTempBackGroundImages);
                                                    string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/Templates/" + tempImg.ProductId);
                                                    string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + ImportIDs.OldOrganisationID + "/Templates/" + OldTempID + "/" + ImageName);
                                                    if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                                                {
                                                    Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                                                    if (Directory.Exists(DestinationTempBackgroundDirectory))
                                                    {
                                                        if (File.Exists(FileBackGroundSourcePath))
                                                        {
                                                            if (!File.Exists(DestinationTempBackGroundImages))
                                                                File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                        }


                                                    }

                                                }
                                                else
                                                {
                                                    if (File.Exists(FileBackGroundSourcePath))
                                                    {
                                                        if (!File.Exists(DestinationTempBackGroundImages))
                                                            File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                    }

                                                }
                                                    tempImg.ImageName = NewPath;
                                                }
                                                
                                            }
                                        }

                                    }

                                    

                                    
                                }
                               
                            }
                           

                        }
                    }
                    // site.css
                    DestinationSiteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Site.css");
                    DestinationsPath.Add(DestinationSiteFile);
                    string DestinationSiteFileDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID);
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
                    DestinationSpriteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Sprite.png");
                    DestinationsPath.Add(DestinationSpriteFile);
                    string DestinationSpriteDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID);
                    string SourceSpriteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.OldCompanyID + "/Sprite.png");
                    if (!System.IO.Directory.Exists(DestinationSpriteDirectory))
                    {
                        Directory.CreateDirectory(DestinationSpriteDirectory);
                        if (Directory.Exists(DestinationSpriteDirectory))
                        {
                            if (File.Exists(SourceSiteFile))
                            {
                                if (!File.Exists(DestinationSpriteFile))
                                    File.Copy(SourceSpriteFile, DestinationSpriteFile);

                            }

                        }
                        else
                        {
                            if (File.Exists(SourceSiteFile))
                            {
                                if (!File.Exists(DestinationSpriteFile))
                                    File.Copy(SourceSpriteFile, DestinationSpriteFile);
                            }
                        }


                    }
                    else
                    {
                        if (File.Exists(SourceSiteFile))
                        {
                            if (!File.Exists(DestinationSpriteFile))
                                File.Copy(SourceSpriteFile, DestinationSpriteFile);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
          


        }

        public void CopyRetailCompanyFiles(long oCID, List<string> DestinationsPath, ImportOrganisation ImportIDs)
        {
            try
            {
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
                string DestinationLanguageFilePath = string.Empty;
                string DestinationItemAttachmentsPath = string.Empty;
                string DestinationFont1 = string.Empty;
                string DestinationFont2 = string.Empty;
                string DestinationFont3 = string.Empty;
                         
                Company ObjCompany = db.Companies.Where(c => c.CompanyId == oCID).FirstOrDefault();

                if (ObjCompany != null)
                {
                    // company logo
                    string CompanyPathOld = string.Empty;
                    string CompanylogoPathNew = string.Empty;
                    if (ObjCompany.Image != null)
                    {
                        CompanyPathOld = Path.GetFileName(ObjCompany.Image);

                        CompanylogoPathNew = CompanyPathOld.Replace(ImportIDs.RetailOldCompanyID + "_", oCID + "_");

                        string DestinationCompanyLogoFilePath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + CompanylogoPathNew);
                        DestinationsPath.Add(DestinationCompanyLogoFilePath);
                        string DestinationCompanyLogoDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID);
                        string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.RetailOldCompanyID + "/" + CompanyPathOld);
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
                        ObjCompany.Image = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + CompanylogoPathNew;
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

                                DestinationContactFilesPath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Contacts/" + contact.ContactId + "/" + NewContactImage);
                                DestinationsPath.Add(DestinationContactFilesPath);
                                string DestinationContactFilesDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Contacts/" + contact.ContactId);
                                string ContactFilesSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.RetailOldCompanyID + "/Contacts/" + OldContactID + "/" + OldContactImage);
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
                                contact.image = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Contacts/" + contact.ContactId + "/" + NewContactImage;
                            }
                        }
                    }
                    Dictionary<string, string> dictionaryMediaIds = new Dictionary<string, string>();

                    // copy media files
                    if (ObjCompany.MediaLibraries != null && ObjCompany.MediaLibraries.Count > 0)
                    {
                        foreach (var media in ObjCompany.MediaLibraries)
                        {
                            string OldMediaFilePath = string.Empty;
                            string NewMediaFilePath = string.Empty;
                            string OldMediaID = string.Empty;
                            string NewMediaID = string.Empty;
                            if (media.FilePath != null)
                            {
                                string name = Path.GetFileName(media.FilePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    OldMediaID = SplitMain[0];

                                }

                                if (media.MediaId > 0)
                                    NewMediaID = Convert.ToString(media.MediaId);

                                dictionaryMediaIds.Add(OldMediaID, NewMediaID);

                                OldMediaFilePath = Path.GetFileName(media.FilePath);
                                NewMediaFilePath = OldMediaFilePath.Replace(OldMediaID + "_", media.MediaId + "_");

                                DestinationMediaFilesPath = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + NewMediaFilePath);
                                DestinationsPath.Add(DestinationMediaFilesPath);
                                string DestinationMediaFilesDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID);
                                string MediaFilesSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Media/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.RetailOldCompanyID + "/" + OldMediaFilePath);
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
                                media.FilePath = "/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + NewMediaFilePath;
                            }

                        }
                    }

                    if (ObjCompany.CompanyBannerSets != null && ObjCompany.CompanyBannerSets.Count > 0)
                    {
                        foreach (var sets in ObjCompany.CompanyBannerSets)
                        {


                            if (sets.CompanyBanners != null && sets.CompanyBanners.Count > 0)
                            {
                                foreach (var bann in sets.CompanyBanners)
                                {
                                    if (!string.IsNullOrEmpty(bann.ImageURL))
                                    {
                                        string OldMediaID = string.Empty;
                                        string newMediaID = string.Empty;
                                        string name = Path.GetFileName(bann.ImageURL);
                                        string[] SplitMain = name.Split('_');
                                        if (SplitMain[0] != string.Empty)
                                        {
                                            OldMediaID = SplitMain[0];

                                        }
                                        if (dictionaryMediaIds != null && dictionaryMediaIds.Count > 0)
                                            newMediaID = dictionaryMediaIds.Where(s => s.Key == OldMediaID).Select(s => s.Value).FirstOrDefault().ToString();

                                        string NewBannerPath = name.Replace(OldMediaID + "_", newMediaID + "_");

                                        bann.ImageURL = "/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + NewBannerPath;
                                    }
                                }
                            }

                        }
                    }

                    if (ObjCompany.CmsPages != null && ObjCompany.CmsPages.Count > 0)
                    {
                        foreach (var pages in ObjCompany.CmsPages)
                        {
                            if (!string.IsNullOrEmpty(pages.PageBanner))
                             {
                                 string name = Path.GetFileName(pages.PageBanner);
                                 pages.PageBanner = "/MPC_Content/Media/" + ImportIDs.NewOrganisationID + "/" + oCID + "/" + name;
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
                                if (SplitMain[1] != string.Empty)
                                {
                                    ProdCatID = SplitMain[1];

                                }

                                OldThumbnailPath = Path.GetFileName(prodCat.ThumbnailPath);
                                NewThumbnailPath = OldThumbnailPath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");



                                DestinationThumbPathCat = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewThumbnailPath);
                                DestinationsPath.Add(DestinationThumbPathCat);
                                string DestinationThumbDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories");
                                string ThumbSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.RetailOldCompanyID + "/ProductCategories/" + OldThumbnailPath);
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
                                prodCat.ThumbnailPath = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewThumbnailPath;
                            }

                            if (!string.IsNullOrEmpty(prodCat.ImagePath))
                            {
                                string OldImagePath = string.Empty;
                                string NewImagePath = string.Empty;

                                string name = Path.GetFileName(prodCat.ImagePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ProdCatID = SplitMain[1];

                                }

                                OldImagePath = Path.GetFileName(prodCat.ImagePath);
                                NewImagePath = OldImagePath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");

                                DestinationImagePath = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewImagePath);
                                DestinationsPath.Add(DestinationImagePath);
                                string DestinationImageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories");
                                string ImageSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.RetailOldCompanyID + "/ProductCategories/" + OldImagePath);

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
                                prodCat.ImagePath = "/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/ProductCategories/" + NewImagePath;
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
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

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
                                        if (File.Exists(File2SourcePath))
                                        {
                                            if (!File.Exists(DestinationFile2Path))
                                                File.Copy(File2SourcePath, DestinationFile2Path);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(File2SourcePath))
                                    {
                                        if (!File.Exists(DestinationFile2Path))
                                            File.Copy(File2SourcePath, DestinationFile2Path);
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
                            if (item.TemplateId != null && item.TemplateId > 0)
                            {
                                if (item.DesignerCategoryId == 0 && item.DesignerCategoryId == null)
                                {
                                    if (item.Template != null)
                                    {
                                        if (item.Template.TemplateFonts != null && item.Template.TemplateFonts.Count > 0)
                                        {
                                            foreach (var fonts in item.Template.TemplateFonts)
                                            {
                                                string DestinationFontDirectory = string.Empty;
                                                string FontSourcePath = string.Empty;
                                                string FontSourcePath1 = string.Empty;
                                                string FontSourcePath2 = string.Empty;

                                                if (!string.IsNullOrEmpty(fonts.FontPath))
                                                {
                                                    DestinationFont1 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".eot");

                                                    DestinationFont2 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".ttf");

                                                    DestinationFont3 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".woff");

                                                    DestinationFontDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontPath);

                                                    FontSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".eot");

                                                    FontSourcePath1 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".ttf");

                                                    FontSourcePath2 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".woff");

                                                    if (!System.IO.Directory.Exists(DestinationFontDirectory))
                                                    {
                                                        Directory.CreateDirectory(DestinationFontDirectory);
                                                        if (Directory.Exists(DestinationFontDirectory))
                                                        {
                                                            if (File.Exists(FontSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationFont1))
                                                                    File.Copy(FontSourcePath, DestinationFont1);
                                                            }

                                                            if (File.Exists(FontSourcePath1))
                                                            {
                                                                if (!File.Exists(DestinationFont2))
                                                                    File.Copy(FontSourcePath1, DestinationFont2);

                                                            }

                                                            if (File.Exists(FontSourcePath2))
                                                            {
                                                                if (!File.Exists(DestinationFont3))
                                                                    File.Copy(FontSourcePath2, DestinationFont3);

                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(FontSourcePath))
                                                        {
                                                            if (!File.Exists(DestinationFont1))
                                                                File.Copy(FontSourcePath, DestinationFont1);
                                                        }

                                                        if (File.Exists(FontSourcePath1))
                                                        {
                                                            if (!File.Exists(DestinationFont2))
                                                                File.Copy(FontSourcePath1, DestinationFont2);

                                                        }

                                                        if (File.Exists(FontSourcePath2))
                                                        {
                                                            if (!File.Exists(DestinationFont3))
                                                                File.Copy(FontSourcePath2, DestinationFont3);

                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    DestinationFont1 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontFile + ".eot");
                                                    DestinationFont2 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontFile + ".ttf");
                                                    DestinationFont3 = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts/" + fonts.FontFile + ".woff");

                                                    DestinationFontDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/WebFonts");

                                                    FontSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontFile + ".eot");

                                                    FontSourcePath1 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontFile + ".ttf");

                                                    FontSourcePath2 = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/" + ImportIDs.OldOrganisationID + "/WebFonts/" + fonts.FontFile + ".woff");

                                                    if (!System.IO.Directory.Exists(DestinationFontDirectory))
                                                    {
                                                        Directory.CreateDirectory(DestinationFontDirectory);
                                                        if (Directory.Exists(DestinationFontDirectory))
                                                        {
                                                            if (File.Exists(FontSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationFont1))
                                                                    File.Copy(FontSourcePath, DestinationFont1);
                                                            }

                                                            if (File.Exists(FontSourcePath1))
                                                            {
                                                                if (!File.Exists(DestinationFont2))
                                                                    File.Copy(FontSourcePath1, DestinationFont2);

                                                            }

                                                            if (File.Exists(FontSourcePath2))
                                                            {
                                                                if (!File.Exists(DestinationFont3))
                                                                    File.Copy(FontSourcePath2, DestinationFont3);

                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(FontSourcePath))
                                                        {
                                                            if (!File.Exists(DestinationFont1))
                                                                File.Copy(FontSourcePath, DestinationFont1);
                                                        }

                                                        if (File.Exists(FontSourcePath1))
                                                        {
                                                            if (!File.Exists(DestinationFont2))
                                                                File.Copy(FontSourcePath1, DestinationFont2);

                                                        }

                                                        if (File.Exists(FontSourcePath2))
                                                        {
                                                            if (!File.Exists(DestinationFont3))
                                                                File.Copy(FontSourcePath2, DestinationFont3);

                                                        }

                                                    }

                                                }

                                                DestinationsPath.Add(DestinationFont1);
                                                DestinationsPath.Add(DestinationFont2);
                                                DestinationsPath.Add(DestinationFont3);


                                            }

                                        }
                                        // template background images
                                        if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                                        {
                                            foreach (var tempImg in item.Template.TemplateBackgroundImages)
                                            {
                                                if (!string.IsNullOrEmpty(tempImg.ImageName))
                                                {
                                                    string name = tempImg.ImageName;

                                                    string ImageName = Path.GetFileName(tempImg.ImageName);

                                                    string NewPath = tempImg.ProductId + "/" + ImageName;

                                                    string[] tempID = tempImg.ImageName.Split('/');

                                                    string OldTempID = tempID[0];


                                                    string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/Templates/" + NewPath);
                                                    DestinationsPath.Add(DestinationTempBackGroundImages);
                                                    string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + ImportIDs.NewOrganisationID + "/Templates/" + tempImg.ProductId);
                                                    string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + ImportIDs.OldOrganisationID + "/Templates/" + OldTempID + "/" + ImageName);
                                                    if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                                                    {
                                                        Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                                                        if (Directory.Exists(DestinationTempBackgroundDirectory))
                                                        {
                                                            if (File.Exists(FileBackGroundSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationTempBackGroundImages))
                                                                    File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                            }


                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(FileBackGroundSourcePath))
                                                        {
                                                            if (!File.Exists(DestinationTempBackGroundImages))
                                                                File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                        }

                                                    }
                                                    tempImg.ImageName = NewPath;
                                                }

                                            }
                                        }

                                    }




                                }

                            }
                            // attachments
                            if (item.ItemAttachments != null && item.ItemAttachments.Count > 0)
                            {
                                foreach (var itemAttach in item.ItemAttachments)
                                {
                                    string OldAttachPath = string.Empty;
                                    string NewAttachPath = string.Empty;

                                    string name = Path.GetFileName(itemAttach.FolderPath);
                                    string[] SplitMain = name.Split('_');
                                    if (SplitMain[0] != string.Empty)
                                    {
                                        ItemID = SplitMain[0];

                                    }

                                    OldAttachPath = Path.GetFileName(item.File5);
                                    NewAttachPath = OldAttachPath.Replace(ItemID + "_", item.ItemId + "_");

                                    DestinationItemAttachmentsPath = HttpContext.Current.Server.MapPath("/MPC_Content/Attachments/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewAttachPath);
                                    DestinationsPath.Add(DestinationItemAttachmentsPath);
                                    string DestinationItemAttachmentDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Attachments/" + ImportIDs.NewOrganisationID + "/" + item.ItemId);
                                    string AttachmentSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Attachments/" + ImportIDs.OldOrganisationID + "/" + ItemID + "/" + OldAttachPath);
                                    if (!System.IO.Directory.Exists(DestinationItemAttachmentDirectory))
                                    {
                                        Directory.CreateDirectory(DestinationItemAttachmentDirectory);
                                        if (Directory.Exists(DestinationItemAttachmentDirectory))
                                        {
                                            if (File.Exists(AttachmentSourcePath))
                                            {
                                                if (!File.Exists(DestinationItemAttachmentsPath))
                                                    File.Copy(AttachmentSourcePath, DestinationItemAttachmentsPath);
                                            }


                                        }

                                    }
                                    else
                                    {
                                        if (File.Exists(AttachmentSourcePath))
                                        {
                                            if (!File.Exists(DestinationItemAttachmentsPath))
                                                File.Copy(AttachmentSourcePath, DestinationItemAttachmentsPath);
                                        }

                                    }
                                    itemAttach.FolderPath = DestinationItemAttachmentsPath;

                                }

                            }
                        }
                    }
                    // site.css
                    DestinationSiteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Site.css");
                    DestinationsPath.Add(DestinationSiteFile);
                    string DestinationSiteFileDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID);
                    string SourceSiteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.RetailOldCompanyID + "/Site.css");
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
                    DestinationSpriteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID + "/Sprite.png");
                    DestinationsPath.Add(DestinationSpriteFile);
                    string DestinationSpriteDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Assets/" + ImportIDs.NewOrganisationID + "/" + oCID);
                    string SourceSpriteFile = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Assets/" + ImportIDs.OldOrganisationID + "/" + ImportIDs.RetailOldCompanyID + "/Sprite.png");
                    if (!System.IO.Directory.Exists(DestinationSpriteDirectory))
                    {
                        Directory.CreateDirectory(DestinationSpriteDirectory);
                        if (Directory.Exists(DestinationSpriteDirectory))
                        {
                            if (File.Exists(SourceSiteFile))
                            {
                                if (!File.Exists(DestinationSpriteFile))
                                    File.Copy(SourceSpriteFile, DestinationSpriteFile);
                            }
                          

                        }
                        else
                        {
                            if (File.Exists(SourceSpriteFile))
                            {

                                if (!File.Exists(DestinationSpriteFile))
                                    File.Copy(SourceSpriteFile, DestinationSpriteFile);
                            }

                        }


                    }
                    else
                    {
                        if (File.Exists(SourceSpriteFile))
                        {
                            if (!File.Exists(DestinationSpriteFile))
                                File.Copy(SourceSpriteFile, DestinationSpriteFile);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        void Copy(string sourceDir, string targetDir)
        {
            if(!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
          

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)),true);
            }
                

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

        public void DeletePhysicallFiles(string Path)
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }

        #endregion
        public Organisation GetCompanySiteDataWithTaxes()
        {
            Organisation compSite = null;


            List<Organisation> companySitesList = db.Organisations.ToList();

                if (companySitesList.Count > 0)
                    compSite = companySitesList[0];
            
            return compSite;
        }
        
        public void DeleteOrganisationBySP(long OrganisationID)
        { 
            try
            {

                db.usp_DeleteOrganisation(Convert.ToInt32(OrganisationID));
            }
            catch(Exception ex)
            {
                throw ex;

            }
        }
    }
}
