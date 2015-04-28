using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using System;
using System.Collections.Generic;
using MPC.Models.Common;
using System.IO;
using MPC.Common;
using MPC.ExceptionHandling;
using WebSupergoo.ABCpdf7;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml;
namespace MPC.Repository.Repositories
{
    public class objTextStyles
    {
        public string textColor { get; set; }
        public string fontName { get; set; }
        public string fontSize { get; set; }
        public string fontWeight { get; set; }
        public string fontStyle { get; set; }

        public string characterIndex { get; set; }
        public string textCMYK { get; set; }
    }
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
        /// 
        public Template GetTemplate(long productID,bool loadPages)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Template template = null;
            if (loadPages)
            {
                template = db.Templates.Include("TemplatePages").Where(g => g.ProductId == productID).SingleOrDefault();
            } else
            {
                template = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
            }
           

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
            // add default cutting margin if not available 
            if (template.CuttingMargin.HasValue)
                template.CuttingMargin = DesignerUtils.PointToPixel(template.CuttingMargin.Value);
            else
                template.CuttingMargin = DesignerUtils.PointToPixel(14.173228345);

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
        // update template height and width , called while uploading pdf as background // added by saqib ali
        public bool updateTemplate(long productID, double pdfWidth, double pdfHeight,int count)
        {
            bool result = false;
            Template objTemplate = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
            if (objTemplate != null)
            {
                objTemplate.PDFTemplateWidth = pdfWidth;
                objTemplate.PDFTemplateHeight = pdfHeight;
                objTemplate.CuttingMargin = 14.173228345;

                List<TemplatePage> listpages = db.TemplatePages.Where(g => g.ProductId == productID).ToList();
                if (listpages.Count > count)
                    listpages = listpages.Take(count).ToList();
                foreach(TemplatePage page in listpages)
                {
                    page.Height = pdfHeight;
                    page.Width = pdfWidth;
                }
                db.SaveChanges();
                result = true;
            }

            return result;
        }
        // update template pages called from designer while uploading pdf as background
        public bool updateTemplatePages(int count,long productId)
        {
            foreach (var tempPage in db.TemplatePages.Where(g => g.ProductId == productId).ToList())
            {
                if (tempPage.PageNo <= count)
                {
                    tempPage.BackGroundType = 1;
                    tempPage.BackgroundFileName = productId.ToString() + "/Side" + tempPage.PageNo.ToString() + ".pdf";
                    tempPage.PageType = 1;  // pageType(1 = without color 2 = with color )  Color C  Color M  Color Y Color K   
                }
            }

            db.SaveChanges();
            return true;
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
                // copy template variables    
                var listVariables = db.TemplateVariables.Where(g => g.TemplateId == ProductID).ToList();
                if (listVariables.Count > 0)
                {
                    foreach (var obj in listVariables)
                    {
                        MPC.Models.DomainModels.TemplateVariable objVariable = new Models.DomainModels.TemplateVariable();
                        objVariable.VariableId = obj.VariableId;
                        objVariable.TemplateId = result;
                        db.TemplateVariables.Add(objVariable);
                    }
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
        // create a new template , called while downloading new template from v2 server //added by saqib ali
        public long SaveTemplateLocally(Template oTemplate, List<TemplatePage> oTemplatePages, List<TemplateObject> oTemplateObjects, List<TemplateBackgroundImage> oTemplateImages, List<TemplateFont> oTemplateFonts, long organisationID, out List<TemplateFont> fontsToDownload, int mode, long localTemplateID)
        {
            long newProductID = 0;
            long newPageID = 0;
            long oldPageID = 0;
            fontsToDownload = new List<TemplateFont>();
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    int i = 1;
                   // oTemplate.EntityKey = null;
                    if (mode == 1)
                    {
                        db.Templates.Add(oTemplate);
                        db.SaveChanges();
                        newProductID = oTemplate.ProductId;
                    }
                    else
                    {
                        var template = db.Templates.Where(g => g.ProductId == localTemplateID).SingleOrDefault();
                        if (template != null)
                        {
                            template.Orientation = oTemplate.Orientation;
                            template.PDFTemplateHeight = oTemplate.PDFTemplateHeight;
                            template.PDFTemplateWidth = oTemplate.PDFTemplateWidth;

                        }
                        db.SaveChanges();
                        newProductID = template.ProductId;
                        List<TemplatePage> oldPages = db.TemplatePages.Where(g => g.ProductId == localTemplateID).ToList();
                        foreach (var page in oldPages)
                        {
                            db.TemplatePages.Remove(page);
                        }
                        // delete old objects 
                        List<TemplateObject> oldObjects = db.TemplateObjects.Where(g => g.ProductId == localTemplateID).ToList();
                        foreach (var obj in oldObjects)
                        {
                            db.TemplateObjects.Remove(obj);
                        }
                        //delete old images (to be done)
                    }


                    foreach (var oPage in oTemplatePages)
                    {
                        oldPageID = oPage.ProductPageId;
                        oPage.ProductId = newProductID;
                        db.TemplatePages.Add(oPage);
                        db.SaveChanges();
                        newPageID = oPage.ProductPageId;
                        foreach (var item in oTemplateObjects.Where(g => g.ProductPageId == oldPageID))
                        {
                            item.ProductPageId = newPageID;
                            item.ProductId = newProductID;

                            //updating the path if it is an image.
                            if (item.ObjectType == 3)
                            {
                                string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));
                                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.
                                if (!item.ContentString.Contains("assets/Imageplaceholder"))
                                {
                                    item.ContentString = "Designer/Organisation"+organisationID+"/Templates/" + newProductID.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                                } else
                                {
                                    // add new place holder path here 
                                }
                            }
                            db.TemplateObjects.Add(item);
                        }
                        //page
                        if (oPage.BackGroundType == 1 || oPage.BackGroundType == 3)
                        {
                            oPage.BackgroundFileName = newProductID.ToString() + "/" + oPage.BackgroundFileName.Substring(oPage.BackgroundFileName.IndexOf("/"), oPage.BackgroundFileName.Length - oPage.BackgroundFileName.IndexOf("/"));
                        }

                    }
                    db.SaveChanges();

                    List<TemplateFont> oLocalFonts = db.TemplateFonts.ToList();
                    foreach (var objFont in oTemplateFonts)
                    {
                        bool found = false;
                        foreach (var objLocal in oLocalFonts)
                        {
                            if (objLocal.CustomerId == objFont.CustomerId && objLocal.FontName == objFont.FontName && objLocal.FontFile == objFont.FontFile)
                            {
                                // checking if font exists
                                found = true;
                                fontsToDownload.Add(objFont);
                            }
                        }

                        if (!found)
                        {
                            // font not found// adding font instance to db     
                            TemplateFont newObjFont = new TemplateFont();
                            newObjFont.CustomerId = objFont.CustomerId;
                            newObjFont.DisplayIndex = objFont.DisplayIndex;
                            newObjFont.FontBytes = objFont.FontBytes;
                            newObjFont.FontDisplayName = objFont.FontDisplayName;
                            newObjFont.FontFile = objFont.FontFile;
                            newObjFont.FontName = objFont.FontName;
                            newObjFont.IsEnable = objFont.IsEnable;
                            newObjFont.IsPrivateFont = objFont.IsPrivateFont;
                            newObjFont.ProductFontId = objFont.ProductFontId;
                            newObjFont.ProductId = objFont.ProductId;
                            newObjFont.FontPath = objFont.FontPath;
                            db.TemplateFonts.Add(newObjFont);
                        }
                    }
                    db.SaveChanges();
                    //page
                    //DownloadFile(RemoteUrlBasePath + oTemplate.LowResPDFTemplates, BasePath + newProductID.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/")));
                    //oTemplate.LowResPDFTemplates = newProductID.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/"));


                    //side 2
                    //DownloadFile(RemoteUrlBasePath + oTemplate.Side2LowResPDFTemplates, BasePath + newProductID.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/")));
                    //oTemplate.Side2LowResPDFTemplates = newProductID.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/"));


                    foreach (TemplateBackgroundImage item in oTemplateImages)
                    {   
                        item.ProductId = newProductID;
                        string NewLocalFileName = newProductID.ToString() + "/" + Path.GetFileName(item.ImageName);
                        item.ImageName = NewLocalFileName;
                        db.TemplateBackgroundImages.Add(item);
                    }


                    db.SaveChanges();

                    dbContextTransaction.Commit(); 
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    dbContextTransaction.Dispose();
                }
            }
            return newProductID;
        }
        //called while saving template from designer // added by saqib ali 
        public void SaveTemplate(long productID, List<TemplatePage> listPages, List<TemplateObject> listObjects)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var objProduct = new Template();

                    objProduct = db.Templates.Where(g => g.ProductId == productID).Single();

                    foreach (TemplateObject c in db.TemplateObjects.Where(g => g.ProductId == productID))
                    {
                        db.TemplateObjects.Remove(c);
                    }
                    db.SaveChanges();

                    foreach(TemplateObject obj in listObjects)
                    {
                        db.TemplateObjects.Add(obj);
                    }
                    db.SaveChanges();
                    var dbTemplatePages = db.TemplatePages.Where(g => g.ProductId == productID).ToList();
                    foreach(TemplatePage objPage in listPages)
                    {
                        for (int i = 0; i < dbTemplatePages.Count; i++)
                        {
                            if (dbTemplatePages[i].ProductPageId == objPage.ProductPageId)
                            {
                                dbTemplatePages[i].ColorC = objPage.ColorC;
                                dbTemplatePages[i].ColorM = objPage.ColorM;
                                dbTemplatePages[i].ColorY = objPage.ColorY;
                                dbTemplatePages[i].ColorK = objPage.ColorK;
                                dbTemplatePages[i].BackGroundType = objPage.BackGroundType;
                                dbTemplatePages[i].IsPrintable = objPage.IsPrintable;
                                if (objPage.BackgroundFileName != "")
                                {
                                    dbTemplatePages[i].BackgroundFileName = objPage.BackgroundFileName;
                                }
                                break;
                            }
                        }
                    }
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    dbContextTransaction.Dispose();
                }
            }
        }
        //called from designer for retail store // added by saqib ali 
        public Template CreateTemplate(long productID,long categoryIdv2,double height,double width,long itemId)
        {
            Template result = null;
            if (productID == 0)
            {
                Template oTemplate = new Template();
                oTemplate.Status = 1;
                oTemplate.ProductName = "Untitled design";
                oTemplate.ProductId = 0;
               // oTemplate.ProductCategoryId = categoryIdv2;
                oTemplate.CuttingMargin = (DesignerUtils.MMToPoint(5));
                oTemplate.PDFTemplateHeight =(DesignerUtils.MMToPoint(height));
                oTemplate.PDFTemplateWidth = (DesignerUtils.MMToPoint(width));
                db.Templates.Add(oTemplate);
                db.SaveChanges();

                TemplatePage tpage = new TemplatePage();
                tpage.Orientation = 1;
                tpage.PageType = 1;
                tpage.PageNo = 1;
                tpage.ProductId = oTemplate.ProductId;
                tpage.BackGroundType = 2;
                tpage.ColorC = 0;
                tpage.ColorK = 0;
                tpage.ColorM = 0;
                tpage.ColorY = 0;
                tpage.PageName = "Front";
                db.TemplatePages.Add(tpage);
                var item = db.Items.Where(g => g.ItemId == itemId).SingleOrDefault();
                if(item != null)
                {
                    item.TemplateId = oTemplate.ProductId;
                }
                db.SaveChanges();
                result = db.Templates.Include("TemplatePages").Where(g => g.ProductId == oTemplate.ProductId).SingleOrDefault();

            }
            return result;
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
            template = GetTemplate(templateID, false);
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


        //public Template ReflectTemplateObject(GlobalTemplateDesigner.Templates ObjGlobalTempDesigner)
        //{
        //    Template ObjTemplate = new Template();
        //    try
        //    {
        //        ObjTemplate.ApprovalDate = ObjGlobalTempDesigner.ApprovalDate;
        //        ObjTemplate.ApprovedBy = ObjGlobalTempDesigner.ApprovedBy;
        //        ObjTemplate.ApprovedByName = ObjGlobalTempDesigner.ApprovedByName;
        //        ObjTemplate.ApprovedDate = ObjGlobalTempDesigner.ApprovedDate;
        //        ObjTemplate.BaseColorID = ObjGlobalTempDesigner.BaseColorID;
        //        ObjTemplate.Code = ObjGlobalTempDesigner.Code;
        //        ObjTemplate.ColorHex = ObjGlobalTempDesigner.ColorHex;
        //        ObjTemplate.CuttingMargin = ObjGlobalTempDesigner.CuttingMargin;
        //        ObjTemplate.Description = ObjGlobalTempDesigner.Description;
        //        ObjTemplate.FullView = ObjGlobalTempDesigner.FullView;
        //        ObjTemplate.Image = ObjGlobalTempDesigner.Image;
        //        ObjTemplate.IsCorporateEditable = ObjGlobalTempDesigner.IsCorporateEditable;
        //        ObjTemplate.isCreatedManual = ObjGlobalTempDesigner.isCreatedManual;
        //        ObjTemplate.IsDisabled = ObjGlobalTempDesigner.IsDisabled;
        //        ObjTemplate.isEditorChoice = ObjGlobalTempDesigner.isEditorChoice;
        //        ObjTemplate.IsPrivate = ObjGlobalTempDesigner.IsPrivate;
        //        ObjTemplate.isSpotTemplate = ObjGlobalTempDesigner.isSpotTemplate;
        //        ObjTemplate.isWatermarkText = ObjGlobalTempDesigner.isWatermarkText;
        //        ObjTemplate.MatchingSetID = ObjGlobalTempDesigner.MatchingSetID;
        //        ObjTemplate.MatchingSetTheme = ObjGlobalTempDesigner.MatchingSetTheme;
        //        ObjTemplate.MPCRating = ObjGlobalTempDesigner.MPCRating;
        //        ObjTemplate.MultiPageCount = ObjGlobalTempDesigner.MultiPageCount;
        //        ObjTemplate.Orientation = ObjGlobalTempDesigner.Orientation;
        //        ObjTemplate.PDFTemplateHeight = ObjGlobalTempDesigner.PDFTemplateHeight;
        //        ObjTemplate.PDFTemplateWidth = ObjGlobalTempDesigner.PDFTemplateWidth;
        //        ObjTemplate.ProductCategoryId = ObjGlobalTempDesigner.ProductCategoryID;
        //        ObjTemplate.ProductId = ObjGlobalTempDesigner.ProductID;
        //        ObjTemplate.ProductName = ObjGlobalTempDesigner.ProductName;
        //        ObjTemplate.RejectionReason = ObjGlobalTempDesigner.RejectionReason;
        //        ObjTemplate.SLThumbnail = ObjGlobalTempDesigner.SLThumbnail;
        //        ObjTemplate.SubmitDate = ObjGlobalTempDesigner.SubmitDate;
        //        ObjTemplate.SubmittedBy = ObjGlobalTempDesigner.SubmittedBy;
        //        ObjTemplate.SubmittedByName = ObjGlobalTempDesigner.SubmittedByName;
        //        ObjTemplate.SuperView = ObjGlobalTempDesigner.SuperView;
        //        ObjTemplate.TemplateOwner = ObjGlobalTempDesigner.TemplateOwner;
        //        ObjTemplate.TemplateOwnerName = ObjGlobalTempDesigner.TemplateOwnerName;
        //        ObjTemplate.TemplateType = ObjGlobalTempDesigner.TemplateType;
        //        ObjTemplate.TempString = ObjGlobalTempDesigner.TempString;
        //        ObjTemplate.Thumbnail = ObjGlobalTempDesigner.Thumbnail;
        //        ObjTemplate.UsedCount = ObjGlobalTempDesigner.UsedCount;
        //        ObjTemplate.UserRating = ObjGlobalTempDesigner.UserRating;
        //        return ObjTemplate;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<TemplatePage> ReflectTemplatePages(GlobalTemplateDesigner.TemplatePages[] tempPages)
        //{
        //    List<TemplatePage> tempPagesList = new List<TemplatePage>();
        //    TemplatePage objTempPage = new TemplatePage();
        //    try
        //    {
        //        foreach(var page in tempPages)
        //        {
        //            objTempPage.BackgroundFileName = page.BackgroundFileName;
        //            objTempPage.BackGroundType = page.BackGroundType;
        //            objTempPage.ColorC = page.ColorC;
        //            objTempPage.ColorK = page.ColorK;
        //            objTempPage.ColorM = page.ColorM;
        //            objTempPage.ColorY = page.ColorY;
        //            objTempPage.hasOverlayObjects = page.hasOverlayObjects;
        //       //     objTempPage.Height = page.Height;
        //            objTempPage.IsPrintable = page.IsPrintable;
        //            objTempPage.Orientation = page.Orientation;
        //            objTempPage.PageName = page.PageName;
        //            objTempPage.PageNo = page.PageNo;
        //            objTempPage.PageType = page.PageType;
        //          //  objTempPage.ProductId = page.ProductId;
        //         //   objTempPage.ProductPageId = page.ProductPageId;
        //         //   objTempPage.Template = page.Template;
        //        //    objTempPage.Width = page.Width;
                   
        //            tempPagesList.Add(objTempPage);
                   

        //        }
        //         return tempPagesList;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
               
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

        public double getOrganisationBleedArea(long organisationID)
        {
            double bleedArea = 0;
            try
            {
                if(organisationID !=0)
                {
                    var organisation = db.Organisations.Where(g => g.OrganisationId == organisationID).SingleOrDefault();
                    if(organisation.BleedAreaSize.HasValue)
                        bleedArea = organisation.BleedAreaSize.Value;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return bleedArea;
        }

        public void regeneratePDFs(long productID, long OrganisationID, bool printCuttingMargins, bool isMultipageProduct, bool drawBleedArea, double bleedAreaSize)
        {
            if (drawBleedArea == false)
            {
                bleedAreaSize = 0;
            }
            GenerateTemplatePdf(productID, OrganisationID, printCuttingMargins, false, false, false, bleedAreaSize, isMultipageProduct);

        }

        private bool GenerateTemplatePdf(long productID, long OrganisationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners, bool isDrawHiddenObjs, double bleedareaSize, bool isMultipageProduct)
        {
            bool result = false;
            try
            {

                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
                string fontsUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/WebFonts/");
                if (!Directory.Exists(drURL + productID))
                {
                    Directory.CreateDirectory(drURL + productID);
                }
                List<TemplatePage> oTemplatePages = new List<TemplatePage>();
                List<TemplateObject> oTemplateObjects = new List<TemplateObject>();
                Template objProduct = GetTemplate(productID, out oTemplatePages, out oTemplateObjects);
                if (isMultipageProduct)
                {
                    bool hasOverlayObject = false;
                    byte[] PDFFile = generatePDF(objProduct, oTemplatePages, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, false, OrganisationID, bleedareaSize, true);
                    //writing the PDF to FS
                    System.IO.File.WriteAllBytes(drURL + productID + "/pages.pdf", PDFFile);
                    //gernating 
                    generatePagePreviewMultiplage(PDFFile, drURL + productID + "/", objProduct.CuttingMargin.Value, 150, isroundCorners);
                    if (hasOverlayObject)
                    {
                        byte[] overlayPDFFile = generatePDF(objProduct, oTemplatePages, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, true, OrganisationID, bleedareaSize, true); ;// generatePDF(objProduct, oTemplatePages, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, objSettings.printCropMarks, false, out hasOverlayObject, true, true);
                        System.IO.File.WriteAllBytes(drURL + productID + "/pagesoverlay.pdf", PDFFile);
                        generatePagePreviewMultiplage(overlayPDFFile, drURL + productID + "/", objProduct.CuttingMargin.Value, 150, isroundCorners);
                    }
                    result = true;
                }
                else
                {


                    foreach (TemplatePage objPage in oTemplatePages)
                    {
                        bool hasOverlayObject = false;
                        byte[] PDFFile = generatePDF(objProduct, objPage, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, false, OrganisationID, bleedareaSize, true);
                        //writing the PDF to FS
                        System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + ".pdf", PDFFile);
                        //generate and write overlay image to FS 
                        generatePagePreview(PDFFile, drURL, productID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, isroundCorners);
                        if (hasOverlayObject)
                        {
                            // generate overlay PDF 
                            byte[] overlayPDFFile = generatePDF(objProduct, objPage, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, true, OrganisationID, bleedareaSize, true);
                            // writing overlay pdf to FS 
                            System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
                            // generate and write overlay image to FS 
                            generatePagePreview(overlayPDFFile, drURL, productID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, isroundCorners);
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return result;
        }


        // generating multipage pdf 
        private byte[] generatePDF(Template objProduct, List<TemplatePage> productPages, List<TemplateObject> listTemplateObjects, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode, long OrganisationID, double bleedareaSize, bool drawBleedArea)
        {
            hasOverlayObject = false;
            Doc doc = new Doc();
            try
            {
                var FontsList = GetFontList();
                doc.TopDown = true;
                foreach (var objProductPage in productPages)
                {
                    try
                    {

                        if (!isoverLayMode)
                        {
                            if (objProductPage.BackGroundType == 1)  //PDF background
                            {
                                if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
                                {
                                    using (var cPage = new Doc())
                                    {
                                        try
                                        {
                                            cPage.Read(ProductFolderPath + objProductPage.BackgroundFileName);
                                            doc.Append(cPage);
                                            doc.PageNumber = objProductPage.PageNo.Value;
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new Exception("Appedning", ex);
                                        }
                                        finally
                                        {
                                            cPage.Dispose();
                                        }

                                    }

                                }
                            }
                            else if (objProductPage.BackGroundType == 2) //background color
                            {
                                //if (objProductPage.Orientation == 1) //standard 
                                //{
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                //}
                                //else
                                //{
                                //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                //}
                                doc.AddPage();
                                doc.PageNumber = objProductPage.PageNo.Value;
                                LoadBackColor(ref doc, objProductPage);
                            }
                            else if (objProductPage.BackGroundType == 3) //background Image
                            {

                                //if (objProductPage.Orientation == 1) //standard 
                                //{
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                //}
                                //else
                                //{
                                //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                //}
                                doc.AddPage();
                                doc.PageNumber = objProductPage.PageNo.Value;
                                LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
                            }
                        }
                        else
                        {
                            //if (objProductPage.Orientation == 1) //standard 
                            //{
                            doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                            doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                            //}
                            //else
                            //{
                            //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                            //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            //}
                            doc.AddPage();
                            doc.PageNumber = objProductPage.PageNo.Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new MPCException(ex.ToString(), OrganisationID);
                    }


                    double YFactor = 0;
                    double XFactor = 0;
                    // int RowCount = 0;




                    List<TemplateObject> oParentObjects = null;

                    if (IsDrawHiddenObjects)
                    {
                        if (isoverLayMode == true)
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                        else
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                    }
                    else
                    {
                        if (isoverLayMode == true)
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                        else
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                    }
                    int count = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).Count();

                    if (count > 0)
                    {
                        hasOverlayObject = true;
                    }
                    foreach (var objObjects in oParentObjects)
                    {

                        if (XFactor != objObjects.PositionX)
                        {
                            if (objObjects.ContentString == "")
                                YFactor = objObjects.PositionY.Value - 7;
                            else
                                YFactor = 0;
                            XFactor = objObjects.PositionX.Value;
                        }



                        if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                        {


                            int VAlign = 1, HAlign = 1;

                            HAlign = objObjects.Allignment.Value;

                            VAlign = objObjects.VAllignment.Value;

                            double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


                            if (VAlign == 1 || VAlign == 2)
                                currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
                            bool isTemplateSpot = false;
                            if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
                                isTemplateSpot = true;

                            AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot);



                        }
                        // object type 13 real state property image 

                        else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
                        {
                            //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
                            // {
                            if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
                            {
                                if (objObjects.ClippedInfo == null)
                                {
                                    LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                }
                                else
                                {
                                    LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                }
                            }
                            //  }
                            //  else
                            // {
                            //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            // }
                        }
                        else if (objObjects.ObjectType == 5)    //line vector
                        {
                            DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 6)    //line vector
                        {
                            DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 7)    //line vector
                        {
                            DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 9)    //svg Path
                        {
                            GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        }

                    }
                    double TrimBoxSize = 5;
                    double BleedBoxSize = 0;
                    if (drawBleedArea)
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null) // sytem.web.confiurationmanager
                        {
                            TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
                        }
                        doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(TrimBoxSize)).ToString());
                        if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
                        {
                            double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
                            doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(ArtBoxSize)).ToString());

                        }
                        //if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
                        //{
                        //    BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
                        //    doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(BleedBoxSize)).ToString());
                        //}
                        if (bleedareaSize != 0)
                        {

                            doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(bleedareaSize)).ToString());
                        }
                    }
                    //crop marks or margins
                    if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0 && drawCuttingMargins)
                    {
                        //doc.CropBox.Height = doc.MediaBox.Height;
                        //doc.CropBox.Width = doc.MediaBox.Width;


                        bool isWaterMarkText = objProduct.isWatermarkText ?? true;

                        int FontID = 0;
                        var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();
                        if (pFont != null)
                        {
                            string path = "";
                            if (pFont.FontPath == null)
                            {
                                path = "";
                            }
                            else
                            {  // customer fonts 

                                path = pFont.FontPath;
                            }
                            if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
                                FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
                        }

                        doc.Font = FontID;
                        double trimboxSizeCuttingLines = 0;
                        if (TrimBoxSize != 5)
                            trimboxSizeCuttingLines = TrimBoxSize;
                        DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value, TrimBoxSize, BleedBoxSize);
                    }

                    if (IsDrawBGText == true)
                    {
                        DrawBackgrounText(ref doc);
                    }
                    int res = 300;
                    if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
                    {
                        res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
                    }
                    doc.Rendering.DotsPerInch = res;

                    //if (ShowHighResPDF == false)
                    //    opage.Session["PDFFile"] = doc.GetData();
                    //OpenPage(opage, "Admin/Products/ViewPdf.aspx");
                }
                return doc.GetData();
            }
            catch (Exception ex)
            {
                throw new Exception("ShowPDF", ex);
            }
            finally
            {
                doc.Dispose();
            }

        }

        public List<TemplateFont> GetFontList()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.TemplateFonts.ToList();

        }
        private void LoadBackGroundImage(ref Doc oPdf, TemplatePage oTemplate, string imgPath)
        {

            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
                oPdf.PageNumber = oTemplate.PageNo.Value;
                oPdf.Layer = oPdf.LayerCount + 1;
                XImage oImg = new XImage();
                bool bFileExists = false;
                string FilePath = imgPath + oTemplate.BackgroundFileName;
                bFileExists = System.IO.File.Exists(FilePath);
                if (bFileExists)
                {
                    oImg.SetFile(FilePath);
                    oPdf.AddImageObject(oImg, true);
                    oPdf.Transform.Reset();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackGroundArtWork", ex);
            }

        }
        private void LoadBackColor(ref Doc oPdf, TemplatePage oTemplate)
        {
            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;

                oPdf.PageNumber = oTemplate.PageNo.Value;
                oPdf.Layer = oPdf.LayerCount + 1;
                oPdf.Color.Cyan = oTemplate.ColorC.Value;
                oPdf.Color.Magenta = oTemplate.ColorM.Value;
                oPdf.Color.Yellow = oTemplate.ColorY.Value;
                oPdf.Color.Black = oTemplate.ColorK.Value;
                oPdf.FillRect();
            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackColor", ex);
            }

        }

        private void AddTextObject(TemplateObject ooBject, int PageNo, List<TemplateFont> oFonts, ref Doc oPdf, string Font, double OPosX, double OPosY, double OWidth, double OHeight, bool isTemplateSpot)
        {

            try
            {
                oPdf.TextStyle.Outline = 0;
                oPdf.TextStyle.Strike = false;
                oPdf.TextStyle.Bold = false;
                oPdf.TextStyle.Italic = false;
                oPdf.TextStyle.CharSpacing = 0;
                oPdf.PageNumber = PageNo;
                if (ooBject.CharSpacing != null)
                {
                    oPdf.TextStyle.CharSpacing = Convert.ToDouble(ooBject.CharSpacing.Value);
                }

                //    OPosY  =OPosY - (ooBject.FontSize.Value / 21);
                double yRPos = 0;
                if (oPdf.TopDown == true)
                    yRPos = oPdf.MediaBox.Height - ooBject.PositionY.Value;
                if (ooBject.ColorType.Value == 3)
                {
                    if (isTemplateSpot)
                    {
                        if (ooBject.IsSpotColor.HasValue == true && ooBject.IsSpotColor.Value == true)
                        {
                            oPdf.ColorSpace = oPdf.AddColorSpaceSpot(ooBject.SpotColorName, ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                            oPdf.Color.Gray = 255;
                        }
                    }
                    else
                    {
                        oPdf.Color.String = ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString();
                    }

                    //if (!ooBject.IsColumnNull("Tint"))
                    if (ooBject.Tint.HasValue)
                    {
                        oPdf.Color.Alpha = Convert.ToInt32((100 - ooBject.Tint) * 2.5);
                    }
                }
                else if (ooBject.ColorType.Value == 4) // For RGB Colors
                {
                    oPdf.Color.String = ooBject.RColor.ToString() + " " + ooBject.GColor.ToString() + " " + ooBject.BColor.ToString();
                }

                //for commented code see change book or commit before 3rd march
                int FontID = 0;
                var pFont = oFonts.Where(g => g.FontName == ooBject.FontName).FirstOrDefault();
                string path = "";
                if (pFont != null)
                {

                    if (pFont.FontPath == null)
                    {
                        // mpc designers fonts or system fonts 
                        path = "";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
                    }
                    else
                    {  // customer fonts 
                        path = pFont.FontPath;
                    }
                    if (System.IO.File.Exists(Font + path + pFont.FontFile + ".ttf"))
                        FontID = oPdf.EmbedFont(Font + path + pFont.FontFile + ".ttf");
                }

                oPdf.Font = FontID;
                oPdf.TextStyle.Size = ooBject.FontSize.Value;
                if (ooBject.IsUnderlinedText.HasValue)
                    oPdf.TextStyle.Underline = ooBject.IsUnderlinedText.Value;
                oPdf.TextStyle.Bold = ooBject.IsBold.Value;

                oPdf.TextStyle.Italic = ooBject.IsItalic.Value;
                double linespacing = ooBject.LineSpacing.Value - 1;
                linespacing = (linespacing * ooBject.FontSize.Value);
                oPdf.TextStyle.LineSpacing = linespacing;
                if (ooBject.Allignment == 1)
                {
                    oPdf.HPos = 0.0;
                }
                else if (ooBject.Allignment == 2)
                {
                    oPdf.HPos = 0.5;
                }
                else if (ooBject.Allignment == 3)
                {
                    oPdf.HPos = 1.0;
                }

                if (ooBject.RotationAngle != 0)
                {

                    oPdf.Transform.Reset();
                    oPdf.Transform.Rotate(360 - ooBject.RotationAngle.Value, OPosX + (OWidth / 2), oPdf.MediaBox.Height - OPosY + (OHeight / 2));
                }
                List<objTextStyles> styles = new List<objTextStyles>();
                if (ooBject.textStyles != null)
                {
                    styles = JsonConvert.DeserializeObject<List<objTextStyles>>(ooBject.textStyles);
                }
                string StyledHtml = "<p>";
                if (styles.Count != 0)
                {
                    styles = styles.OrderBy(g => g.characterIndex).ToList();
                    for (int i = 0; i < ooBject.ContentString.Length; i++)
                    {
                        objTextStyles objStyle = GetStyleByCharIndex(i, styles);
                        if (objStyle != null && ooBject.ContentString[i] != '\n')
                        {
                            if (objStyle.fontName == null && objStyle.fontSize == null && objStyle.fontStyle == null && objStyle.fontWeight == null && objStyle.textColor == null)
                            {
                                StyledHtml += ooBject.ContentString[i];
                            }
                            else
                            {
                                string toApplyStyle = ooBject.ContentString[i].ToString();
                                string fontTag = "<font";
                                string fontSize = "";
                                string pid = "";
                                if (objStyle.fontName != null)
                                {
                                    var oFont = oFonts.Where(g => g.FontName == objStyle.fontName).FirstOrDefault();
                                    if (System.IO.File.Exists(Font + path + oFont.FontFile + ".ttf"))
                                        FontID = oPdf.EmbedFont(Font + path + oFont.FontFile + ".ttf");
                                    // fontTag += " face='" + objStyle.fontName + "' embed= "+ FontID+" ";
                                    pid = "pid ='" + FontID.ToString() + "' ";
                                }
                                if (objStyle.fontSize != null)
                                {
                                    fontSize += "<StyleRun fontsize='" + Convert.ToInt32(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + pid + ">";
                                }
                                if (objStyle.fontStyle != null)
                                {
                                    fontTag += " font-style='" + objStyle.fontStyle + "'";
                                }
                                if (objStyle.fontWeight != null)
                                {
                                    fontTag += " font-weight='" + objStyle.fontWeight + "'";
                                }
                                if (objStyle.textColor != null)
                                {
                                    if (objStyle.textCMYK != null)
                                    {
                                        string[] vals = objStyle.textCMYK.Split(' ');
                                        string hexC = Convert.ToInt32(vals[0]).ToString("X");
                                        if (hexC.Length == 1)
                                            hexC = "0" + hexC;
                                        string hexM = Convert.ToInt32(vals[1]).ToString("X");
                                        if (hexM.Length == 1)
                                            hexM = "0" + hexM;
                                        string hexY = Convert.ToInt32(vals[2]).ToString("X");
                                        if (hexY.Length == 1)
                                            hexY = "0" + hexY;
                                        string hexK = Convert.ToInt32(vals[3]).ToString("X");
                                        if (hexK.Length == 1)
                                            hexK = "0" + hexK;
                                        string hex = "#" + hexC + hexM + hexY + hexK;
                                        // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), objStyle.textCMYK);
                                        //oPdf.Color.Gray = 255;
                                        // fontTag += " color='#FF' csid=" + csInlineID;
                                        fontTag += " color='" + hex + "' ";
                                    }
                                    else
                                    {
                                        fontTag += " color='" + objStyle.textColor + "'";
                                    }
                                }
                                else
                                {
                                    if (objStyle.fontName != null)
                                    {
                                        //   Utilities.CMYKtoRGBConverter objCData = new Utilities.CMYKtoRGBConverter();
                                        // string colorHex = objCData.getColorHex();
                                        // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                                        string hexC = Convert.ToInt32(ooBject.ColorC).ToString("X");
                                        if (hexC.Length == 1)
                                            hexC = "0" + hexC;
                                        string hexM = Convert.ToInt32(ooBject.ColorM).ToString("X");
                                        if (hexM.Length == 1)
                                            hexM = "0" + hexM;
                                        string hexY = Convert.ToInt32(ooBject.ColorY).ToString("X");
                                        if (hexY.Length == 1)
                                            hexY = "0" + hexY;
                                        string hexK = Convert.ToInt32(ooBject.ColorK).ToString("X");
                                        if (hexK.Length == 1)
                                            hexK = "0" + hexK;
                                        string hex = "#" + hexC + hexM + hexY + hexK;

                                        fontTag += " color='" + hex + "' ";
                                    }
                                    //fontTag += " color='" + objStyle.textColor + "'";
                                }
                                if (fontSize != "")
                                {
                                    toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
                                }
                                else
                                {
                                    if (objStyle.fontName != null)
                                    {
                                        fontSize += "<StyleRun " + pid + ">";
                                        toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
                                    }
                                    else
                                    {
                                        toApplyStyle = fontTag + " >" + toApplyStyle + "</font>";
                                    }

                                }
                                StyledHtml += toApplyStyle;
                            }
                        }
                        else
                        {
                            StyledHtml += ooBject.ContentString[i];
                        }
                    }

                }
                else
                {
                    StyledHtml += ooBject.ContentString;
                }
                StyledHtml += "</p>";
                string sNewLineNormalized = Regex.Replace(StyledHtml, @"\r(?!\n)|(?<!\r)\n", "<BR>");
                sNewLineNormalized = sNewLineNormalized.Replace("  ", "&nbsp;&nbsp;");

                if (ooBject.AutoShrinkText == true)
                {
                    oPdf.Rect.Position(OPosX, OPosY);
                    oPdf.Rect.Resize(OWidth, OHeight);
                    int theID = oPdf.AddHtml(sNewLineNormalized);
                    while (oPdf.Chainable(theID))
                    {
                        oPdf.Delete(theID);
                        oPdf.FontSize--;
                        oPdf.Rect.String = oPdf.Rect.String; // reset Doc.Pos without having to save its initial value
                        theID = oPdf.AddHtml(sNewLineNormalized);
                    }
                }
                else
                {
                    oPdf.Rect.Position(OPosX, OPosY);
                    oPdf.Rect.Resize(OWidth, OHeight);
                    oPdf.AddHtml(sNewLineNormalized);
                }
                oPdf.Transform.Reset();

            }
            catch (Exception ex)
            {
                throw new Exception("ADDSingleLineText", ex);
            }

        }

        private objTextStyles GetStyleByCharIndex(int index, List<objTextStyles> objStyles)
        {
            foreach (var obj in objStyles)
            {
                if (obj.characterIndex == index.ToString())
                {
                    return obj;
                }
            }
            return null;
        }

        private void LoadImage(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        {
            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;


                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
                {
                    logoPath = ""; //since path is already in filenm
                    string[] vals;
                    FilePath = "";
                    if (oObject.ContentString.ToLower().Contains("/mpc_content/"))
                    {
                        vals = oObject.ContentString.ToLower().Split(new string[] { "/mpc_content/" }, StringSplitOptions.None);
                        FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/" + vals[vals.Length - 1]);
                        //FilePath = logoPath + oObject.ContentString;
                        bFileExists = System.IO.File.Exists((FilePath));
                    }
                    else
                    {
                        // dont show any thing becuase path will contain dummy placeholder image
                    }


                }
                else
                {
                    if (oObject.ContentString != "")
                        FilePath = oObject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                //  else
                //     filNm = oobject.LogoName;

                if (bFileExists)
                {

                    //  oImg.SetFile(FilePath);

                    var posY = oObject.PositionY + oObject.MaxHeight;

                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

                    if (oObject.RotationAngle != null)
                    {


                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);


                        }


                    }
                    if (oObject.Opacity != null && oObject.Opacity != 1)
                    {
                        img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));

                        if (oObject.Opacity != null)
                        {
                            // float opacity =float.Parse( oObject.Tint.ToString()) /100;
                            if (oObject.Opacity != 1)
                            {
                                img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                            }
                        }
                        oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));
                        int id = oPdf.AddImageObject(oImg, true);
                    }
                    else
                    {
                        // XImage oImgx = new XImage();
                        oImg.SetFile(FilePath);
                        oPdf.AddImageObject(oImg, true);
                    }
                    //oPdf.FrameRect();


                    //if (oObject.Tint != null)
                    //{
                    //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                    //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                    //}

                    oPdf.Transform.Reset();
                }


            }
            catch (Exception ex)
            {
                throw new Exception("LoadImage", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }

        private void LoadCroppedImg(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        {


            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;
                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
                {
                    logoPath = ""; //since path is already in filenm
                    string[] vals;
                    FilePath = "";
                    if (oObject.ContentString.ToLower().Contains("/mpc_content/"))
                    {
                        vals = oObject.ContentString.ToLower().Split(new string[] { "/mpc_content/" }, StringSplitOptions.None);
                        FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/" + vals[vals.Length - 1]);
                        //   FilePath = logoPath + oObject.ContentString;
                        bFileExists = System.IO.File.Exists((FilePath));
                    }
                    else
                    {
                        // dont show any thing becuase path will contain dummy placeholder image
                    }

                }
                else
                {
                    if (oObject.ContentString != "")
                        FilePath = oObject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                if (bFileExists)
                {


                    var posY = oObject.PositionY + oObject.MaxHeight;
                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

                    if (oObject.RotationAngle != null)
                    {
                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
                        }
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(oObject.ClippedInfo);
                    string xpath = "Cropped";
                    var nodes = xmlDoc.SelectNodes(xpath);
                    double sx, sy, swidth, sheight;
                    foreach (XmlNode childrenNode in nodes)
                    {
                        sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
                        sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
                        swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
                        sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
                        oImg.Selection.Inset(sx, sy);
                        oImg.Selection.Height = sheight;
                        oImg.Selection.Width = swidth;
                    }
                    if (oObject.Opacity != null && oObject.Opacity != 1)
                    {
                        img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));
                        if (oObject.Opacity != null)
                        {
                            if (oObject.Opacity != 1)
                            {
                                img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                            }
                        }
                        oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));
                        foreach (XmlNode childrenNode in nodes)
                        {
                            sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
                            sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
                            swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
                            sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
                            oImg.Selection.Inset(sx, sy);
                            oImg.Selection.Height = sheight;
                            oImg.Selection.Width = swidth;
                        }
                        int id = oPdf.AddImageObject(oImg, true);
                    }
                    else
                    {
                        if (System.IO.Path.GetExtension(FilePath).ToLower().Contains(".tif"))
                        {
                            oPdf.AddImageFile(FilePath);
                        }
                        else
                        {
                            oImg.SetFile(FilePath);
                            foreach (XmlNode childrenNode in nodes)
                            {
                                sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
                                sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
                                swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
                                sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
                                oImg.Selection.Inset(sx, sy);
                                oImg.Selection.Height = sheight;
                                oImg.Selection.Width = swidth;
                            }
                            oPdf.AddImageObject(oImg, true);
                        }
                    }

                    oPdf.Transform.Reset();
                }


            }
            catch (Exception ex)
            {
                throw new Exception("LoadImage", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }

        private void DrawVectorLine(ref Doc oPdf, TemplateObject oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
                    //if (!ooBject.IsColumnNull("Tint"))
                    oPdf.Color.Alpha = Convert.ToInt32((oObject.Tint) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                oPdf.Width = oObject.MaxHeight.Value;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value);
                    }
                }

                // oPdf.AddImageObject(oImg,false);
                //oPdf.AddImage ((oImg);
                oPdf.AddLine(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2, oObject.PositionX.Value + oObject.MaxWidth.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorLine", ex);
            }

        }

        private void DrawVectorRectangle(ref Doc oPdf, TemplateObject oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
                    if (oObject.Opacity != null)
                        oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.5);
                    //if (!ooBject.IsColumnNull("Tint"))
                    //oPdf.Color.Alpha = 0;//Convert.ToInt32((100 - oObject.Tint) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
                    }


                }

                int id = oPdf.FillRect();
                //if (oObject.Tint != null)
                //{
                //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                //}
                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorRectangle", ex);
            }

        }

        //vector Ellipse drawing in PDF
        private void DrawVectorEllipse(ref Doc oPdf, TemplateObject oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();

                    if (oObject.Opacity != null)
                        oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }




                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
                    }


                }

                int id = oPdf.FillRect(oObject.MaxWidth.Value / 2, oObject.MaxHeight.Value / 2);
                //if (oObject.Tint != null)
                //{
                //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                //}
                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorEllipse", ex);
            }

        }

        private void GetSVGAndDraw(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        {

            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;
                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ContentString != "")
                    FilePath = oObject.ContentString;
                FilePath = logoPath + "/" + FilePath;
                bFileExists = System.IO.File.Exists(FilePath);
                if (bFileExists)
                {
                    DesignerSvgParser.MaximumSize = new Size(Convert.ToInt32(oObject.MaxWidth), Convert.ToInt32(oObject.MaxHeight));
                    img = DesignerSvgParser.GetBitmapFromSVG(FilePath, oObject.ColorHex);
                    if (oObject.Opacity != null)
                    {
                        // float opacity =float.Parse( oObject.Tint.ToString()) /100;
                        if (oObject.Opacity != 1)
                        {
                            img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                        }
                    }
                    oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));

                    var posY = oObject.PositionY + oObject.MaxHeight;

                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

                    if (oObject.RotationAngle != null)
                    {
                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
                        }
                    }
                    int id = oPdf.AddImageObject(oImg, true);
                    //if (oObject.Tint != null)
                    //{
                    //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];

                    //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                    //}
                    oPdf.Transform.Reset();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LoadSvg", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }


        private void DrawCuttingLines(ref Doc oPdf, double mrg, int PageNo, string pageName, string waterMarkTxt, bool drawCuttingMargins, bool drawWatermark, bool isWaterMarkText, double pdfTemplateHeight, double pdfTemplateWidth, double trimBoxSize, double bleedOffset)
        {
            try
            {
                oPdf.Color.String = "100 100 100 100";

                if (trimBoxSize != 0) // for digital central 
                {
                    mrg = DesignerUtils.MMToPoint(trimBoxSize);
                }
                double offset = 0;
                if (bleedOffset != 0) // for digital central 
                {
                    offset = DesignerUtils.MMToPoint(bleedOffset);
                }
                //mrg = 5.98110236159; // global change on request of digital central to make crop marks 2.11 mm
                oPdf.Layer = oPdf.LayerCount - 1;
                oPdf.PageNumber = PageNo;
                //oPdf.Width = 0.5;
                oPdf.Width = 0.25;
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
                double pgWidth = oPdf.MediaBox.Width;
                double pgHeight = oPdf.MediaBox.Height;
                for (int i = 1; i <= oPdf.PageCount; i++)
                {
                    oPdf.PageNumber = i;
                    if (drawCuttingMargins)
                    {
                        oPdf.Layer = 1;
                        oPdf.AddLine(mrg, 0, mrg, mrg - offset);
                        oPdf.AddLine(0, mrg, mrg - offset, mrg);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg, 0, oPdf.MediaBox.Width - mrg, mrg - offset);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, mrg, oPdf.MediaBox.Width, mrg);
                        oPdf.AddLine(0, oPdf.MediaBox.Height - mrg, mrg - offset, oPdf.MediaBox.Height - mrg);
                        oPdf.AddLine(mrg, oPdf.MediaBox.Height - mrg + offset, mrg, oPdf.MediaBox.Height);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, oPdf.MediaBox.Height - mrg, oPdf.MediaBox.Width, oPdf.MediaBox.Height - mrg); //----
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height - mrg + offset, oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height); //|

                        // adding date time stamp
                        oPdf.Layer = 1;
                        oPdf.TextStyle.Outline = 0;
                        oPdf.TextStyle.Strike = false;
                        // oPdf.TextStyle.Bold = true;
                        oPdf.TextStyle.Italic = false;
                        oPdf.TextStyle.CharSpacing = 0;
                        oPdf.TextStyle.Size = 6;
                        oPdf.Rect.Position(((pgWidth / 2) - 20), pgHeight + 5);
                        oPdf.Rect.Resize(200, 10);
                        oPdf.AddHtml("" + pageName + " " + DateTime.Now.ToString());
                        oPdf.Transform.Reset();
                    }
                    // water mark 
                    if (drawWatermark)
                    {
                        if (waterMarkTxt != null && waterMarkTxt != "")
                        {
                            if (isWaterMarkText)
                            {
                                oPdf.Color.String = "16 12 13 0";
                                oPdf.Color.Alpha = 220;
                                oPdf.TextStyle.Size = 30;
                                oPdf.Layer = 1;
                                oPdf.HPos = 0.5;
                                oPdf.VPos = 0.5;
                                oPdf.TextStyle.Outline = 2;
                                oPdf.Rect.Position(0, oPdf.MediaBox.Height);
                                oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
                                // oPdf.FrameRect();
                                oPdf.Transform.Reset();
                                oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
                                oPdf.AddHtml(waterMarkTxt);
                                oPdf.Transform.Reset();
                            }
                            else
                            {
                                string FilePath = string.Empty;
                                XImage oImg = new XImage();
                                System.Drawing.Image objImage = null;
                                try
                                {
                                    oPdf.PageNumber = PageNo;


                                    bool bFileExists = false;
                                    FilePath = waterMarkTxt;
                                    bFileExists = System.IO.File.Exists(FilePath);

                                    if (bFileExists)
                                    {
                                        objImage = System.Drawing.Image.FromFile(FilePath);
                                        oImg.SetFile(FilePath);
                                        double height = DesignerUtils.PixelToPoint(objImage.Height);
                                        double width = DesignerUtils.PixelToPoint(objImage.Width);
                                        if (height > pdfTemplateHeight)
                                        {
                                            height = pdfTemplateHeight;
                                        }
                                        if (width > pdfTemplateWidth)
                                        {
                                            width = pdfTemplateWidth;
                                        }

                                        double posX = (oPdf.MediaBox.Width - width) / 2;
                                        double posY = (oPdf.MediaBox.Height - height) / 2 + height;


                                        oPdf.Layer = 1;
                                        oPdf.Rect.Position(posX, posY);
                                        oPdf.Rect.Resize(width, height);


                                        oPdf.AddImageObject(oImg, true);
                                        oPdf.Transform.Reset();
                                    }


                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("LoadWaterMarkImage", ex);
                                }
                                finally
                                {
                                    oImg.Dispose();
                                    if (objImage != null)
                                        objImage.Dispose();
                                }
                                // image
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DrawCuttingLine", ex);
            }
        }

        private void DrawBackgrounText(ref Doc oPdf)
        {
            int FontID = oPdf.AddFont("Arial");
            for (int i = 1; i <= oPdf.PageCount; i++)
            {
                oPdf.PageNumber = i;
                oPdf.Color.String = "211 211 211";
                //oPdf.Color.Alpha = 60;
                oPdf.Font = FontID;
                oPdf.TextStyle.Size = 40;
                //oPdf.TextStyle.CharSpacing = 2;
                //oPdf.TextStyle.Bold = true;
                //oPdf.TextStyle.Italic = false;
                oPdf.HPos = 0.5;
                oPdf.VPos = 0.5;
                oPdf.TextStyle.Outline = 2;
                oPdf.Rect.Position(0, oPdf.MediaBox.Height);
                oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
                // oPdf.FrameRect();
                oPdf.Transform.Reset();
                oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
                oPdf.AddHtml("MPC Systems");
            }
            oPdf.HPos = 0;
            oPdf.VPos = 0;
            oPdf.TextStyle.Outline = 0;
            oPdf.TextStyle.Strike = false;
            oPdf.TextStyle.Bold = false;
            oPdf.TextStyle.Italic = false;
            oPdf.TextStyle.CharSpacing = 0;
            oPdf.Transform.Reset();
            oPdf.Transform.Rotate(0, 0, 0);
            oPdf.Transform.Reset();
        }

        public bool generatePagePreviewMultiplage(byte[] PDFDoc, string savePath, double CuttingMargin, int DPI, bool RoundCorners)
        {


            //XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {

                try
                {
                    theDoc.Read(PDFDoc);
                    for (int i = 1; i <= theDoc.PageCount; i++)
                    {


                        theDoc.PageNumber = i;
                        theDoc.Rect.String = theDoc.CropBox.String;
                        theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                        if (System.IO.Directory.Exists(savePath) == false)
                        {
                            System.IO.Directory.CreateDirectory(savePath);
                        }

                        theDoc.Rendering.DotsPerInch = DPI;
                        string fileName = "p" + i + ".png";
                        //if (RoundCorners)
                        //{
                        //    Stream str = new MemoryStream();

                        //    theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName), str);
                        //    generateRoundCorners(System.IO.Path.Combine(savePath, fileName), System.IO.Path.Combine(savePath, fileName), str);

                        //}
                        //else
                        //{
                        //    theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName));
                        //}
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName));
                    }

                    theDoc.Dispose();

                    return true;



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }
        }

        private byte[] generatePDF(Template objProduct, TemplatePage objProductPage, List<TemplateObject> listTemplateObjects, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode, long OrganisationID, double bleedareaSize, bool drawBleedArea)
        {
            Doc doc = new Doc();
            try
            {
                var FontsList = GetFontList();
                doc.TopDown = true;

                try
                {

                    if (!isoverLayMode)
                    {
                        if (objProductPage.BackGroundType == 1)  //PDF background
                        {
                            if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
                            {
                                doc.Read(ProductFolderPath + objProductPage.BackgroundFileName);

                            }
                        }
                        else if (objProductPage.BackGroundType == 2) //background color
                        {
                            //  if (objProductPage.Orientation == 1) //standard 
                            //  {
                            if (objProductPage.Height.HasValue)
                            {
                                doc.MediaBox.Height = objProductPage.Height.Value;
                            }
                            else
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                            }
                            if (objProductPage.Width.HasValue)
                            {
                                doc.MediaBox.Width = objProductPage.Width.Value;
                            }
                            else
                            {
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
                            }



                            //}
                            //else
                            //{
                            //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                            //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            //}
                            doc.AddPage();
                            LoadBackColor(ref doc, objProductPage);
                        }
                        else if (objProductPage.BackGroundType == 3) //background Image
                        {

                            //  if (objProductPage.Orientation == 1) //standard 
                            //  {
                            if (objProductPage.Height.HasValue)
                            {
                                doc.MediaBox.Height = objProductPage.Height.Value;
                            }
                            else
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                            }
                            if (objProductPage.Width.HasValue)
                            {
                                doc.MediaBox.Width = objProductPage.Width.Value;
                            }
                            else
                            {
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
                            }

                            //}
                            //else
                            //{
                            //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                            //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            //}
                            doc.AddPage();
                            LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
                        }
                    }
                    else
                    {
                        //if (objProductPage.Orientation == 1) //standard 
                        //{
                        if (objProductPage.Height.HasValue)
                        {
                            doc.MediaBox.Height = objProductPage.Height.Value;
                        }
                        else
                        {
                            doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                        }
                        if (objProductPage.Width.HasValue)
                        {
                            doc.MediaBox.Width = objProductPage.Width.Value;
                        }
                        else
                        {
                            doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
                        }

                        //}
                        //else
                        //{
                        //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                        //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                        //}
                        doc.AddPage();
                    }
                }
                catch (Exception ex)
                {
                    throw new MPCException(ex.ToString(), OrganisationID);
                }


                double YFactor = 0;
                double XFactor = 0;
                // int RowCount = 0;




                List<TemplateObject> oParentObjects = null;

                if (IsDrawHiddenObjects)
                {
                    if (isoverLayMode == true)
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                    else
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                }
                else
                {
                    if (isoverLayMode == true)
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                    else
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                }
                int count = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).Count();
                hasOverlayObject = false;
                if (count > 0)
                {
                    hasOverlayObject = true;
                }
                foreach (var objObjects in oParentObjects)
                {

                    if (XFactor != objObjects.PositionX)
                    {
                        if (objObjects.ContentString == "")
                            YFactor = objObjects.PositionY.Value - 7;
                        else
                            YFactor = 0;
                        XFactor = objObjects.PositionX.Value;
                    }



                    if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                    {


                        int VAlign = 1, HAlign = 1;

                        HAlign = objObjects.Allignment.Value;

                        VAlign = objObjects.VAllignment.Value;

                        double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


                        if (VAlign == 1 || VAlign == 2)
                            currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
                        bool isTemplateSpot = false;
                        if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
                            isTemplateSpot = true;

                        AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot);



                    }
                    // object type 13 real state property image 

                    else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
                    {
                        //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
                        // {
                        if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
                        {
                            if (objObjects.ClippedInfo == null)
                            {
                                LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            }
                            else
                            {
                                LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            }
                        }
                        //  }
                        //  else
                        // {
                        //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        // }
                    }
                    else if (objObjects.ObjectType == 5)    //line vector
                    {
                        DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
                    }
                    else if (objObjects.ObjectType == 6)    //line vector
                    {
                        DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
                    }
                    else if (objObjects.ObjectType == 7)    //line vector
                    {
                        DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
                    }
                    else if (objObjects.ObjectType == 9)    //svg Path
                    {
                        GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                    }

                }
                double TrimBoxSize = 5;
                double BleedBoxSize = 0;
                //crop marks or margins
                if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0)
                {
                    //doc.CropBox.Height = doc.MediaBox.Height;
                    //doc.CropBox.Width = doc.MediaBox.Width;


                    bool isWaterMarkText = objProduct.isWatermarkText ?? true;
                    if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null) // sytem.web.confiurationmanager
                    {
                        TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
                    }
                    doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(TrimBoxSize)).ToString());
                    if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
                    {
                        double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
                        doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(ArtBoxSize)).ToString());

                    }
                    if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
                    {
                        BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
                        doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(BleedBoxSize)).ToString());
                    }
                    if (bleedareaSize != 0)
                    {

                        doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(bleedareaSize)).ToString());
                    }
                    int FontID = 0;
                    var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();
                    if (pFont != null)
                    {
                        string path = "";
                        if (pFont.FontPath == null)
                        {
                            path = "";
                        }
                        else
                        {  // customer fonts 

                            path = pFont.FontPath;
                        }
                        if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
                            FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
                    }

                    doc.Font = FontID;
                    double trimboxSizeCuttingLines = 0;
                    if (TrimBoxSize != 5)
                        trimboxSizeCuttingLines = TrimBoxSize;
                    DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value, TrimBoxSize, BleedBoxSize);
                }

                if (IsDrawBGText == true)
                {
                    DrawBackgrounText(ref doc);
                }
                int res = 300;
                if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
                {
                    res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
                }
                doc.Rendering.DotsPerInch = res;

                //if (ShowHighResPDF == false)
                //    opage.Session["PDFFile"] = doc.GetData();
                //OpenPage(opage, "Admin/Products/ViewPdf.aspx");

                return doc.GetData();
            }
            catch (Exception ex)
            {
                throw new Exception("ShowPDF", ex);
            }
            finally
            {
                doc.Dispose();
            }

        }

        private string generatePagePreview(byte[] PDFDoc, string savePath, string PreviewFileName, double CuttingMargin, int DPI, bool RoundCorners)
        {
            using (Doc theDoc = new Doc())
            {
                Stream str = null;
                try
                {
                    theDoc.Read(PDFDoc);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;
                    theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                    if (System.IO.Directory.Exists(savePath) == false)
                    {
                        System.IO.Directory.CreateDirectory(savePath);
                    }
                    string filePath = savePath + PreviewFileName + ".png";
                    theDoc.Rendering.DotsPerInch = DPI;
                    theDoc.Rendering.Save(filePath);
                    theDoc.Dispose();
                    //if (RoundCorners)
                    //{
                    //    generateRoundCorners(filePath, filePath,str);
                    //}



                    return PreviewFileName + ".png";



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                    if (str != null)
                        str.Dispose();
                }
            }

        }
        #endregion

        
    }
}
