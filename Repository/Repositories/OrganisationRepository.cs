using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using MPC.Models.Common;

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
        public void InsertOrganisation(Organisation org,ExportOrganisation objExpOrg)
        {
            try
            {
                long OrganisationID = 0;
                Organisation newOrg = new Organisation();
                newOrg = org;
                newOrg.OrganisationId = 20;
                db.Organisations.Add(newOrg);
                
                if (db.SaveChanges() > 0)
                {
                    OrganisationID = org.OrganisationId;
                }
                
                // save paper sizes
                if(objExpOrg.PaperSizes != null && objExpOrg.PaperSizes.Count > 0)
                {
                    foreach(var size in objExpOrg.PaperSizes)
                    {
                        size.PaperSizeId = 0;
                        size.OrganisationId = OrganisationID;
                        db.PaperSizes.Add(size);

                    }

                }
                // save cost centres and its child objects
                if(objExpOrg.CostCentre != null && objExpOrg.CostCentre.Count > 0)
                { 
                    foreach(var cost in objExpOrg.CostCentre)
                    {
                        cost.CostCentreId = 0;
                        cost.OrganisationId = OrganisationID;
                        db.CostCentres.Add(cost);
                        db.SaveChanges();
                        // save cost centre instructions
                        if(cost.CostcentreInstructions != null && cost.CostcentreInstructions.Count > 0)
                        {
                            foreach(var ins in cost.CostcentreInstructions)
                            {
                                ins.InstructionId = 0;
                                ins.CostCentreId = cost.CostCentreId;
                             
                                db.CostcentreInstructions.Add(ins);
                                if(ins.CostcentreWorkInstructionsChoices != null && ins.CostcentreWorkInstructionsChoices.Count > 0)
                                {
                                    foreach(var choice in ins.CostcentreWorkInstructionsChoices)
                                    {
                                        choice.Id = 0;
                                        choice.InstructionId = ins.InstructionId;
                                      
                                        db.CostcentreWorkInstructionsChoices.Add(choice);

                                    }
                                }

                            }
                        }

                        // save cost centre resources
                        if(cost.CostcentreResources != null && cost.CostcentreResources.Count > 0)
                        {
                            foreach(var res in cost.CostcentreResources)
                            {
                                res.CostCenterResourceId = 0;
                                res.CostCentreId = cost.CostCentreId;
                           
                                db.CostcentreResources.Add(res);
                            }
                        }


                        List<CostCenterChoice> choices = objExpOrg.CostCenterChoice.Where(c => c.CostCenterId == cost.CostCentreId).ToList();

                        foreach(var choice in choices)
                        {
                            choice.CostCenterChoiceId = 0;
                            choice.CostCenterId = (int)cost.CostCentreId;
                           
                            db.CostCenterChoices.Add(choice);
                        }
                        

                        
                      

                    }
                   

                   
                }
                // cost centre questions answers
                if (objExpOrg.CostCentreQuestion != null & objExpOrg.CostCentreQuestion.Count > 0)
                {
                    foreach (var question in objExpOrg.CostCentreQuestion)
                    {
                        question.Id = 0;
                        question.CompanyId = (int)OrganisationID;

                        db.CostCentreQuestions.Add(question);
                        db.SaveChanges();
                        List<CostCentreAnswer> answers = objExpOrg.CostCentreAnswer.Where(q => q.QuestionId == question.Id).ToList();
                        if(answers != null && answers.Count > 0)
                        foreach(var ans in answers)
                        {
                            ans.QuestionId = question.Id;
                            ans.Id = 0;
                            db.CostCentreAnswers.Add(ans);
                        }
                       
                    }

                }

                // cost centre matrix and matrix detail
                if(objExpOrg.CostCentreMatrix != null && objExpOrg.CostCentreMatrix.Count > 0)
                {
                    foreach(var matrix in objExpOrg.CostCentreMatrix)
                    {
                        matrix.MatrixId = 0;
                        matrix.CompanyId = (int)OrganisationID;
                        db.CostCentreMatrices.Add(matrix);

                        db.SaveChanges();
                        List<CostCentreMatrixDetail> matrixDetail = objExpOrg.CostCentreMatrixDetail.Where(c => c.MatrixId == matrix.MatrixId).ToList();
                        if(matrixDetail != null && matrixDetail.Count > 0)
                        {
                            foreach(var matrixD in matrixDetail)
                            {
                                matrixD.Id = 0;
                                matrixD.MatrixId = matrix.MatrixId;
                                db.CostCentreMatrixDetails.Add(matrixD);

                            }
                            db.SaveChanges();
                        }
                        

                    }
                }
                // Stock Categories
                if (objExpOrg.StockCategory != null && objExpOrg.StockCategory.Count > 0)
                { 
                        foreach(var cat in objExpOrg.StockCategory)
                        {
                            cat.CategoryId = 0;
                            cat.OrganisationId = OrganisationID;
                            db.StockCategories.Add(cat);

                            if(cat.StockSubCategories != null && cat.StockSubCategories.Count > 0)
                            {
                                foreach(var subCat in cat.StockSubCategories)
                                {
                                    subCat.SubCategoryId = 0;
                                    subCat.CategoryId = cat.CategoryId;
                                    db.StockSubCategories.Add(subCat);

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
                        Sitems.StockItemId = 0;
                        Sitems.OrganisationId = OrganisationID;
                        db.StockItems.Add(Sitems);

                        if (Sitems.StockCostAndPrices != null && Sitems.StockCostAndPrices.Count > 0)
                        {
                            foreach (var costAndPrice in Sitems.StockCostAndPrices)
                            {
                                costAndPrice.CostPriceId = 0;
                                costAndPrice.ItemId = Sitems.StockItemId;
                                db.StockCostAndPrices.Add(costAndPrice);
                            }

                        }
                        db.SaveChanges();
                    }
                }
                // import reports
                if(objExpOrg.Reports != null && objExpOrg.Reports.Count > 0)
                {
                    foreach(var report in objExpOrg.Reports)
                    {
                        report.ReportId = 0;
                        report.OrganisationId = OrganisationID;
                        db.Reports.Add(report);

                        

                    }
                    
                }
                // import report notes
                if(objExpOrg.ReportNote != null && objExpOrg.ReportNote.Count > 0)
                {
                    foreach(var rptNotes in objExpOrg.ReportNote)
                    {
                        rptNotes.Id = 0;
                        rptNotes.OrganisationId = OrganisationID;
                        db.ReportNotes.Add(rptNotes);
                    }
                }
                // import prefixes
                if(objExpOrg.Prefixes != null && objExpOrg.Prefixes.Count > 0)
                {
                    foreach(var prefix in objExpOrg.Prefixes)
                    {
                        prefix.PrefixId = 0;
                        prefix.OrganisationId = OrganisationID;
                        db.Prefixes.Add(prefix);

                    }
                }

                // import lookup methods
                if(objExpOrg.LookupMethods != null && objExpOrg.LookupMethods.Count > 0)
                {
                    foreach(var lookup in objExpOrg.LookupMethods)
                    {
                        lookup.MethodId = 0;
                        lookup.OrganisationId = (int)OrganisationID;
                        db.LookupMethods.Add(lookup);

                    }

                }
                // import phrase library
                if(objExpOrg.PhraseField != null && objExpOrg.PhraseField.Count > 0)
                {
                    foreach(var PF in objExpOrg.PhraseField)
                    {
                        PF.FieldId = 0;
                        PF.OrganisationId = OrganisationID;
                        db.PhraseFields.Add(PF);

                       if(PF.Phrases != null && PF.Phrases.Count > 0)
                       {
                           foreach(var phrase in PF.Phrases)
                           {
                               phrase.PhraseId = 0;
                               phrase.FieldId = PF.FieldId;
                               phrase.CompanyId = OrganisationID;
                               db.Phrases.Add(phrase);

                           }
                       }
                    }
                    
                }
                // import section flags
                if(objExpOrg.SectionFlags != null && objExpOrg.SectionFlags.Count > 0)
                {
                    foreach(var sflag in objExpOrg.SectionFlags)
                    {
                        sflag.SectionFlagId = 0;
                        sflag.OrganisationId = OrganisationID;
                        db.SectionFlags.Add(sflag);

                    }
                }

                db.SaveChanges();




                // company flow 

                // insert company
                Company comp = new Company();
                comp = objExpOrg.Company;
                comp.CompanyId = 0;
                comp.OrganisationId = OrganisationID;
                db.Companies.Add(comp);

                // insert company domain

                if(objExpOrg.CompanyDomain != null && objExpOrg.CompanyDomain.Count > 0)
                {
                    foreach(var compDomain in objExpOrg.CompanyDomain)
                    {
                        compDomain.CompanyId = comp.CompanyId;
                        compDomain.CompanyDomainId = 0;
                        db.CompanyDomains.Add(compDomain);


                    }
                }

                //  insert cms Offers
                if(objExpOrg.CmsOffer != null && objExpOrg.CmsOffer.Count > 0)
                {
                    foreach(var cmsOffers in objExpOrg.CmsOffer)
                    {
                        cmsOffers.OfferId = 0;
                        cmsOffers.CompanyId = comp.CompanyId;
                        db.CmsOffers.Add(cmsOffers);
                    }
                }

                //import media libraries
                if(objExpOrg.MediaLibrary != null && objExpOrg.MediaLibrary.Count > 0)
                {
                    foreach(var media in objExpOrg.MediaLibrary)
                    {
                        media.MediaId = 0;
                        media.CompanyId = comp.CompanyId;
                        db.MediaLibraries.Add(media);
                    }
                }





            }
            catch(Exception ex)
            {
                throw ex;
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
    }
}
