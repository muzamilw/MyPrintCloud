﻿using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using System;
using System.Collections.Generic;
using MPC.Models.Common;
using System.IO;
namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Template Repository
    /// </summary>
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Template> DbSet
        {
            get
            {
                return db.Templates;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public Template Find(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        ///  Get template object by template id // added by saqib ali
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Template GetTemplate(long productID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var template = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
            return template;

        }

         // returns list of pages and objects along with template called while generating template pdf;
        public Template GetTemplate(long productID, out List<TemplatePage> listPages, out List<TemplateObject> listTemplateObjs)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var template = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
            listPages = null;
            listTemplateObjs = null;
            if(template != null)
            {
                listPages = db.TemplatePages.Where(g => g.ProductId == productID).ToList();
                listTemplateObjs = db.TemplateObjects.Where(g => g.ProductId == productID).ToList();
            }
            return template;
        }
        
        // delete template from database // added by saqib ali
        public bool DeleteTemplate(long ProductID, out long CategoryID)
        {
            try
            {
                CategoryID = 0;
                bool result = false;
                    //deleting objects
                foreach (TemplateObject c in db.TemplateObjects.Where(g => g.ProductId == ProductID))
                {
                    db.TemplateObjects.Remove(c);
                }
                    //background Images
                foreach (TemplateBackgroundImage c in db.TemplateBackgroundImages.Where(g => g.ProductId == ProductID))
                {

                    db.TemplateBackgroundImages.Remove(c);
                }
                    //delete template pages
                foreach (TemplatePage c in db.TemplatePages.Where(g => g.ProductId == ProductID))
                {

                    db.TemplatePages.Remove(c);
                }
                    //deleting the template
                foreach (Template c in db.Templates.Where(g => g.ProductId == ProductID))
                {
                    if(c.ProductCategoryId.HasValue)
                        CategoryID = c.ProductCategoryId.Value;
                    db.Templates.Remove(c);
                }
                db.SaveChanges();
                result = true;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // update template height and width and add new template pages, called while uploading pdf as template from MIS // added by saqib
        public bool updateTemplate(long productID, double pdfWidth, double pdfHeight, List<TemplatePage> listPages)
        { 
            bool result = false;
            Template objTemplate = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
            if (objTemplate != null)
            {
                objTemplate.PDFTemplateWidth = pdfWidth;
                objTemplate.PDFTemplateHeight = pdfHeight;
                objTemplate.CuttingMargin = 14.173228345;
                foreach(TemplatePage obj in listPages)
                {
                    TemplatePage objPage = new TemplatePage();
                     objPage.ProductId = productID;
                    objPage.PageNo = obj.PageNo;
                    objPage.PageName = obj.PageName;
                    objPage.IsPrintable = true;
                    objPage.Orientation = 1;
                    objPage.BackGroundType = 1;
                    objPage.BackgroundFileName = obj.BackgroundFileName;
                    objPage.PageType = 1;  // pageType(1 = without color 2 = with color )  Color C  Color M  Color Y Color K   
                    db.TemplatePages.Add(objPage);

                }
                db.SaveChanges();
                result = true;
            }
            
            return result;
        }
        // update template height and width, add new page and link old template objects with new pages, called while uploading pdf as template from MIS // added by saqib
        public bool updateTemplate(long productID, double pdfWidth, double pdfHeight, List<TemplatePage> listNewPages, List<TemplatePage> listOldPages, List<TemplateObject> listObjects)
        {
            bool result = false;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    Template objTemplate = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
                    if (objTemplate != null)
                    {
                        objTemplate.PDFTemplateWidth = pdfWidth;
                        objTemplate.PDFTemplateHeight = pdfHeight;
                        objTemplate.CuttingMargin = 14.173228345;
                        foreach (TemplatePage obj in listNewPages)
                        {
                            TemplatePage objPage = new TemplatePage();
                            objPage.ProductId = productID;
                            objPage.PageNo = obj.PageNo;
                            objPage.PageName = obj.PageName;
                            objPage.IsPrintable = true;
                            objPage.Orientation = 1;
                            objPage.BackGroundType = 1;
                            objPage.BackgroundFileName = obj.BackgroundFileName;
                            objPage.PageType = 1;  // pageType(1 = without color 2 = with color )  Color C  Color M  Color Y Color K   
                            db.TemplatePages.Add(objPage);
                            db.SaveChanges();
                            // get old page
                            var oldTemplatePage = listOldPages.Where(g => g.PageNo == obj.PageNo).SingleOrDefault();
                            // add old objects to new  template page
                            if (oldTemplatePage != null)
                            {
                                long oldPageID = oldTemplatePage.ProductPageId;
                                List<TemplateObject> oldObjs = listObjects.Where(g => g.ProductPageId == oldPageID).ToList();
                                foreach (var tempObj in oldObjs)
                                {
                                    tempObj.ProductPageId = objPage.ProductPageId;
                                    tempObj.ProductId = objTemplate.ProductId;
                                    db.TemplateObjects.Add(tempObj);
                                }
                            }
                        }
                        db.SaveChanges();
                        dbContextTransaction.Commit(); 
                        result = true;

                    }
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                } 
            }
            return result;
        }
        // copy a single template and update file paths in db // added by saqib ali
        public long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName, out List<TemplatePage> objPages, long OrganisationID, out List<TemplateBackgroundImage> objImages)
        {
            long result = 0;
            var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
            long? test = db.sp_cloneTemplate(ProductID, SubmittedBy, SubmittedByName);
            if (test.HasValue)
            {
                result = test.Value;
                var pages = db.TemplatePages.Where(g => g.ProductId == result).ToList();
                objPages = pages;
                foreach (TemplatePage oTemplatePage in pages)
                {

                    if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                    {
                        string name = oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/"));
                        oTemplatePage.BackgroundFileName = result.ToString() + "/" + name;
                    }

                }
                foreach (var item in db.TemplateObjects.Where(g => g.ProductId == result))
                {
                    if (item.IsPositionLocked == null)
                    {
                        item.IsPositionLocked = false;
                    }
                    if (item.IsHidden == null)
                    {
                        item.IsHidden = false;
                    }
                    if (item.IsEditable == null)
                    {
                        item.IsEditable = true;
                    }
                    if (item.IsTextEditable == null)
                    {
                        item.IsTextEditable = true;
                    }

                    if (item.ObjectType == 3)
                    {
                        string[] content = item.ContentString.Split('/');
                        string fileName = content[content.Length - 1];
                        if (!item.ContentString.Contains("assets/Imageplaceholder"))
                        {
                            item.ContentString = "Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + result.ToString() + "/" + fileName;
                        }
                    }
                }
                var backimgs = db.TemplateBackgroundImages.Where(g => g.ProductId == result).ToList();
                objImages = backimgs;
                foreach (TemplateBackgroundImage item in backimgs)
                {
                    string filePath = drURL + item.ImageName;
                    string filename;
                    FileInfo oFile = new FileInfo(filePath);
                    filename = oFile.Name;
                    item.ImageName = result.ToString() + "/" + filename;
                }

                db.SaveChanges();
            } else
            {
                objPages = null;
                objImages = null;
            }

            return result;
        }

        // delete template all pages and its objects // added by saqib ali
        public void DeleteTemplatePagesAndObjects(long ProductID)
        {
            try
            {

                //deleting objects
                foreach (TemplateObject c in db.TemplateObjects.Where(g => g.ProductId == ProductID))
                {
                    db.TemplateObjects.Remove(c);
                }
                //delete template pages
                foreach (TemplatePage c in db.TemplatePages.Where(g => g.ProductId == ProductID))
                {

                    db.TemplatePages.Remove(c);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // delete template all pages and its objects, called from mis while uploading pdf as template // added by saqib ali
        public void DeleteTemplatePagesAndObjects(long ProductID,out List<TemplateObject> listObjs,out List<TemplatePage> listPages)
        {
            try
            {
                listObjs = db.TemplateObjects.Where(g => g.ProductId == ProductID).ToList();
                //deleting objects
                foreach (TemplateObject c in listObjs)
                {
                    db.TemplateObjects.Remove(c);
                }
                //delete template pages
                listPages = db.TemplatePages.Where(g => g.ProductId == ProductID).ToList();
                foreach (TemplatePage c in listPages)
                {

                    db.TemplatePages.Remove(c);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID, List<ProductCategoriesView> PCview)
        {
            try
            {
               

                //The Remote mapped categoryName
                int totalPagesCount = 0;
                int SearchCount = 0;


                List<MatchingSets> templatesList = GetTemplateDataFromService(TemplateName, pageNumber - 1, out totalPagesCount, out SearchCount,CustomerID,CompanyID,PCview);


                if (templatesList.Count > 0)
                {
                    foreach (var i in templatesList)
                    {
                        ProductCategory pRec = GetDisplayOrderAndSave(Convert.ToInt16(i.ProductCategoryID));
                        if (pRec != null)
                        {
                            i.DisplayOrder = pRec.DisplayOrder;
                        }
                    }
                    templatesList = templatesList.OrderBy(c => c.DisplayOrder).ToList();

                    return templatesList;
                }
                else
                {
                   
                    return null;
                }

            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        private List<MatchingSets> GetTemplateDataFromService(string templateName, int pageNumber, out int totalPagesCount, out int SearchCount, long CustomerID, int CompanyID, List<ProductCategoriesView> PCview)
        {
          
            using (GlobalTemplateDesigner.TemplateSvcSPClient tsc = new GlobalTemplateDesigner.TemplateSvcSPClient())
            {

                string[] categoryNames = (from c in PCview
                                          select c.TemplateDesignerMappedCategoryName).ToArray();
                string NewTemplateName = templateName.Replace("Copy", "");
                List<MatchingSets> objList = new List<MatchingSets>();
                int PageSize = 15;
                var tempList = tsc.GetTemplatesbyTemplateName(out totalPagesCount, out SearchCount, NewTemplateName + ":" + CustomerID.ToString(), categoryNames, pageNumber, PageSize).ToList();
                foreach (var item in tempList)
                {
                    MatchingSets objMatchingSet = new MatchingSets();
                    objMatchingSet.BaseColorID = item.BaseColorID ?? 0;
                    objMatchingSet.CategoryName = item.CategoryName;
                    objMatchingSet.Code = item.Code;
                    objMatchingSet.Description = item.Description;
                    objMatchingSet.DisplayOrder = item.DisplayOrder;
                    objMatchingSet.FullView = item.FullView;
                    objMatchingSet.Image = item.Image;
                    objMatchingSet.IsDisabled = item.IsDisabled ?? false;
                    objMatchingSet.IsDoubleSide = item.IsDoubleSide;
                    objMatchingSet.isEditorChoice = item.isEditorChoice ?? false;
                    objMatchingSet.IsPrivate = item.IsPrivate ?? false;
                    objMatchingSet.IsUseBackGroundColor = item.IsUseBackGroundColor ?? false;
                    objMatchingSet.MultiPageCount = item.MultiPageCount ?? 0;
                    objMatchingSet.Orientation = item.Orientation ?? 0;
                    objMatchingSet.ProductCategoryID = item.ProductCategoryID;
                    objMatchingSet.ProductID = item.ProductID;
                    objMatchingSet.ProductName = item.ProductName;
                    objMatchingSet.PTempId = item.PTempId ?? 0;
                    objMatchingSet.SLThumbnail = item.SLThumbnail;
                    objMatchingSet.Status = item.Status ?? 0;
                    objMatchingSet.SubmittedBy = item.SubmittedBy ?? 0;
                    objMatchingSet.TemplateOwner = item.TemplateOwner ?? 0;
                    objMatchingSet.SuperView = item.SuperView;
                    objMatchingSet.Thumbnail = item.Thumbnail;
                    objList.Add(objMatchingSet);

                }
                //tempList = tempList.Where(c => c.Status == 3).ToList();

                return objList;

            }

        }


        public ProductCategory GetDisplayOrderAndSave(int pCID)
        {

            return (from c in db.ProductCategories
                    where c.ProductCategoryId == pCID
                    select c).FirstOrDefault();
        }
      
        public string GetTemplateNameByTemplateID(long tempID)
        {
            try
            {
                return db.Templates.Where(t => t.ProductId == tempID).Select(s => s.ProductName).FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public long CloneTemplateByTemplateID(long TempID)
        {

            try
            {
                long result = db.sp_cloneTemplate(TempID, 0, "");
                return result;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
        /// <summary>
        /// To populate the template information base on template id and item rec by zohaib 10/1/2015
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="ItemRecc"></param>
        public void populateTemplateInfo(long templateID, Item ItemRecc,out Template template,out List<TemplatePage> tempPages)
        {
            Template temp = new Template();
            template = GetTemplate(templateID);
            tempPages = GetTemplatePagesByTemplateID(templateID);
            //using (GlobalTemplateDesigner.TemplateSvcSPClient pSc = new GlobalTemplateDesigner.TemplateSvcSPClient())
            //{
            //    string option = "NoTemplate";
            //    var oTemplate = pSc.GetTemplate((int)templateID);
            //    var oTemplatePages = pSc.GetTemplatePages((int)templateID);

            //   template = ReflectTemplateObject(oTemplate);

            //    tempPages = ReflectTemplatePages(oTemplatePages);

            //}
        }


        public Template ReflectTemplateObject(GlobalTemplateDesigner.Templates ObjGlobalTempDesigner)
        {
            Template ObjTemplate = new Template();
            try
            {
                ObjTemplate.ApprovalDate = ObjGlobalTempDesigner.ApprovalDate;
                ObjTemplate.ApprovedBy = ObjGlobalTempDesigner.ApprovedBy;
                ObjTemplate.ApprovedByName = ObjGlobalTempDesigner.ApprovedByName;
                ObjTemplate.ApprovedDate = ObjGlobalTempDesigner.ApprovedDate;
                ObjTemplate.BaseColorID = ObjGlobalTempDesigner.BaseColorID;
                ObjTemplate.Code = ObjGlobalTempDesigner.Code;
                ObjTemplate.ColorHex = ObjGlobalTempDesigner.ColorHex;
                ObjTemplate.CuttingMargin = ObjGlobalTempDesigner.CuttingMargin;
                ObjTemplate.Description = ObjGlobalTempDesigner.Description;
                ObjTemplate.FullView = ObjGlobalTempDesigner.FullView;
                ObjTemplate.Image = ObjGlobalTempDesigner.Image;
                ObjTemplate.IsCorporateEditable = ObjGlobalTempDesigner.IsCorporateEditable;
                ObjTemplate.isCreatedManual = ObjGlobalTempDesigner.isCreatedManual;
                ObjTemplate.IsDisabled = ObjGlobalTempDesigner.IsDisabled;
                ObjTemplate.isEditorChoice = ObjGlobalTempDesigner.isEditorChoice;
                ObjTemplate.IsPrivate = ObjGlobalTempDesigner.IsPrivate;
                ObjTemplate.isSpotTemplate = ObjGlobalTempDesigner.isSpotTemplate;
                ObjTemplate.isWatermarkText = ObjGlobalTempDesigner.isWatermarkText;
                ObjTemplate.MatchingSetID = ObjGlobalTempDesigner.MatchingSetID;
                ObjTemplate.MatchingSetTheme = ObjGlobalTempDesigner.MatchingSetTheme;
                ObjTemplate.MPCRating = ObjGlobalTempDesigner.MPCRating;
                ObjTemplate.MultiPageCount = ObjGlobalTempDesigner.MultiPageCount;
                ObjTemplate.Orientation = ObjGlobalTempDesigner.Orientation;
                ObjTemplate.PDFTemplateHeight = ObjGlobalTempDesigner.PDFTemplateHeight;
                ObjTemplate.PDFTemplateWidth = ObjGlobalTempDesigner.PDFTemplateWidth;
                ObjTemplate.ProductCategoryId = ObjGlobalTempDesigner.ProductCategoryID;
                ObjTemplate.ProductId = ObjGlobalTempDesigner.ProductID;
                ObjTemplate.ProductName = ObjGlobalTempDesigner.ProductName;
                ObjTemplate.RejectionReason = ObjGlobalTempDesigner.RejectionReason;
                ObjTemplate.SLThumbnail = ObjGlobalTempDesigner.SLThumbnail;
                ObjTemplate.SubmitDate = ObjGlobalTempDesigner.SubmitDate;
                ObjTemplate.SubmittedBy = ObjGlobalTempDesigner.SubmittedBy;
                ObjTemplate.SubmittedByName = ObjGlobalTempDesigner.SubmittedByName;
                ObjTemplate.SuperView = ObjGlobalTempDesigner.SuperView;
                ObjTemplate.TemplateOwner = ObjGlobalTempDesigner.TemplateOwner;
                ObjTemplate.TemplateOwnerName = ObjGlobalTempDesigner.TemplateOwnerName;
                ObjTemplate.TemplateType = ObjGlobalTempDesigner.TemplateType;
                ObjTemplate.TempString = ObjGlobalTempDesigner.TempString;
                ObjTemplate.Thumbnail = ObjGlobalTempDesigner.Thumbnail;
                ObjTemplate.UsedCount = ObjGlobalTempDesigner.UsedCount;
                ObjTemplate.UserRating = ObjGlobalTempDesigner.UserRating;
                return ObjTemplate;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<TemplatePage> ReflectTemplatePages(GlobalTemplateDesigner.TemplatePages[] tempPages)
        {
            List<TemplatePage> tempPagesList = new List<TemplatePage>();
            TemplatePage objTempPage = new TemplatePage();
            try
            {
                foreach(var page in tempPages)
                {
                    objTempPage.BackgroundFileName = page.BackgroundFileName;
                    objTempPage.BackGroundType = page.BackGroundType;
                    objTempPage.ColorC = page.ColorC;
                    objTempPage.ColorK = page.ColorK;
                    objTempPage.ColorM = page.ColorM;
                    objTempPage.ColorY = page.ColorY;
                    objTempPage.hasOverlayObjects = page.hasOverlayObjects;
               //     objTempPage.Height = page.Height;
                    objTempPage.IsPrintable = page.IsPrintable;
                    objTempPage.Orientation = page.Orientation;
                    objTempPage.PageName = page.PageName;
                    objTempPage.PageNo = page.PageNo;
                    objTempPage.PageType = page.PageType;
                  //  objTempPage.ProductId = page.ProductId;
                 //   objTempPage.ProductPageId = page.ProductPageId;
                 //   objTempPage.Template = page.Template;
                //    objTempPage.Width = page.Width;
                   
                    tempPagesList.Add(objTempPage);
                   

                }
                 return tempPagesList;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
               
        /// <summary>
        /// get template pages by productID added by zohaib
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public List<TemplatePage> GetTemplatePagesByTemplateID(long ProductID)
        {
            try
            {
                return db.TemplatePages.Where(s => s.ProductId == ProductID).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        
    }
}
