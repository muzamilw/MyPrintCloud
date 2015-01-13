﻿using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf8;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateService : ITemplateService
    {
        #region privateGeneratePdf
        // helper functions to generate pdf 
        //generate pdf function
       // private byte[] generatePDF(Template objProduct, TemplatePage objProductPage, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode)
       // {

       // }
        // generate low res proof image from pdf file 
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

                    theDoc.Rendering.DotsPerInch = DPI;
                    if (RoundCorners)
                    {
                        using (str = new MemoryStream())
                        {
                            theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);
                            generateRoundCorners(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);
                        }
                    }
                    else
                    {
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png");
                    }

                    theDoc.Dispose();

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
                    if(str != null)
                       str.Dispose();
                }
            }

        }
        private void generateRoundCorners(string physicalPath, string pathToSave, Stream str)
        {
            string path = physicalPath;
            int roundedDia = 30;
            Bitmap bitmap = null;
            using (Image imgin = Image.FromStream(str))//FromFile(path)
            {
                bitmap = new Bitmap(imgin.Width, imgin.Height);
                Graphics g = Graphics.FromImage(bitmap);
                try
                {
                    g.Clear(Color.Transparent);
                    g.SmoothingMode = (System.Drawing.Drawing2D.SmoothingMode.AntiAlias);
                    Brush brush = new System.Drawing.TextureBrush(imgin);
                    FillRoundedRectangle(g, new Rectangle(0, 0, imgin.Width, imgin.Height), roundedDia, brush);
                    if (File.Exists(pathToSave))
                    {
                        File.Delete(pathToSave);
                    }
                    bitmap.Save(pathToSave, System.Drawing.Imaging.ImageFormat.Png);

                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    if(bitmap != null)
                       bitmap.Dispose();
                    if(imgin != null)
                        imgin.Dispose();
                    if(g != null)
                        g.Dispose();
                }
            }
        }
        private static void FillRoundedRectangle(Graphics g, Rectangle r, int d, Brush b)
        {
            
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                gp.AddArc(r.X, r.Y, d, d, 180, 90);
                gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);

                g.FillPath(b, gp);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(gp != null)
                  gp.Dispose();
            }
           
        }
        #endregion
        #region private
        public readonly ITemplateRepository _templateRepository;
        public readonly IProductCategoryRepository _ProductCategoryRepository;
        public readonly ITemplateBackgroundImagesService _templateBackgroundImagesService;
       
        // it will convert pdf to template pages and will preserve template objects and images 
        private bool CovertPdfToBackground(string physicalPath, long ProductID, long organizationID)
        {
            bool result = false;
            double pdfHeight, pdfWidth = 0;
            try
            {
                using (Doc theDoc = new Doc())
                {
                    try
                    {
                            List<TemplateObject> listTobjs = new List<TemplateObject>();
                            List<TemplatePage> listTpages = new List<TemplatePage>();
                            List<TemplatePage> listNewTemplatePages = new List<TemplatePage>();
                            double cuttingMargins = 0;
                            pdfWidth = theDoc.MediaBox.Width;
                            pdfHeight = theDoc.MediaBox.Height;
                            _templateRepository.DeleteTemplatePagesAndObjects(ProductID, out listTobjs,out listTpages);
                            theDoc.Read(physicalPath);
                            int srcPagesID = theDoc.GetInfoInt(theDoc.Root, "Pages");
                            int srcDocRot = theDoc.GetInfoInt(srcPagesID, "/Rotate");
                            // create template pages
                            for (int i = 1; i <= theDoc.PageCount; i++)
                            {
                                theDoc.PageNumber = i;
                                theDoc.Rect.String = theDoc.CropBox.String;
                                theDoc.Rect.Inset(cuttingMargins, cuttingMargins);
                                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/");
                                //check if folder exist
                                string tempFolder = drURL + ProductID.ToString();
                                if (!System.IO.Directory.Exists(tempFolder))
                                {
                                    System.IO.Directory.CreateDirectory(tempFolder);
                                }
                                // generate image 
                                string ThumbnailFileName = "/templatImgBk";
                                theDoc.Rendering.DotsPerInch = 150;
                                string renderingPath = tempFolder + ThumbnailFileName + i.ToString() + ".jpg";
                                theDoc.Rendering.Save(renderingPath);
                                // save template page 
                                TemplatePage objPage = new TemplatePage();
                                objPage.PageNo = i;
                                objPage.ProductId = ProductID;
                                objPage.PageName = "Front";
                                objPage.BackgroundFileName = ProductID + "/Side" + (i).ToString() + ".pdf";
                                listNewTemplatePages.Add(objPage);
                               
                                // save pdf 
                                Doc singlePagePdf = new Doc();
                                try
                                {
                                    singlePagePdf.Rect.String = singlePagePdf.MediaBox.String = theDoc.MediaBox.String;
                                    singlePagePdf.AddPage();
                                    singlePagePdf.AddImageDoc(theDoc, i, null);
                                    singlePagePdf.FrameRect();

                                    int srcPageRot = theDoc.GetInfoInt(theDoc.Page, "/Rotate");
                                    if (srcDocRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcDocRot);
                                    }
                                    if (srcPageRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcPageRot);
                                    }
                                    string targetFolder = drURL;
                                    if (File.Exists(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf"))
                                    {
                                        File.Delete(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    }
                                    singlePagePdf.Save(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    singlePagePdf.Clear();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("GenerateTemplateBackground", e);
                                }
                                finally
                                {
                                    if (singlePagePdf != null)
                                        singlePagePdf.Dispose();
                                }
                            }
                        /// save the pages
                            result = _templateRepository.updateTemplate(ProductID, pdfWidth, pdfHeight,listNewTemplatePages,listTpages,listTobjs);
                        theDoc.Dispose();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("GeneratePDfPreservingObjects", ex);
                    }
                    finally
                    {
                        if (theDoc != null)
                            theDoc.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        // it willl convert pdf to template pages and will delete all existing objects and images 
        private bool CovertPdfToBackgroundWithoutObjects(string physicalPath, long ProductID, long organizationID)
        {
            bool result = false;
            try
            {
                int CuttingMargin = 0;
                double pdfHeight, pdfWidth = 0;
                _templateRepository.DeleteTemplatePagesAndObjects(ProductID);
                _templateBackgroundImagesService.DeleteTemplateBackgroundImages(ProductID,organizationID);
                using (Doc theDoc = new Doc())
                {

                    try
                    {
                        theDoc.Read(physicalPath);
                        int tID = 0;
                        pdfWidth = theDoc.MediaBox.Width;
                        pdfHeight = theDoc.MediaBox.Height;
                        List<TemplatePage> listPages = new List<TemplatePage>();
                        int srcPagesID = theDoc.GetInfoInt(theDoc.Root, "Pages");
                        int srcDocRot = theDoc.GetInfoInt(srcPagesID, "/Rotate");
                        string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/" );
                        for (int i = 1; i <= theDoc.PageCount; i++)
                        {
                            theDoc.PageNumber = i;
                            theDoc.Rect.String = theDoc.CropBox.String;
                            theDoc.Rect.Inset(CuttingMargin, CuttingMargin);
                            //check if folder exist
                            string tempFolder = drURL + ProductID.ToString();
                            if (!System.IO.Directory.Exists(tempFolder))
                            {
                                System.IO.Directory.CreateDirectory(tempFolder);
                            }
                            // generate image 
                            string ThumbnailFileName = "/templatImgBk";
                            theDoc.Rendering.DotsPerInch = 150;
                            string renderingPath = tempFolder + ThumbnailFileName + i.ToString() + ".jpg";
                            theDoc.Rendering.Save(renderingPath);
                            // save template page 
                            TemplatePage objPage = new TemplatePage();
                            objPage.PageNo = i;
                            objPage.ProductId = ProductID;
                            objPage.PageName = "Front";
                            objPage.BackgroundFileName = ProductID + "/Side" + (i).ToString() + ".pdf";
                            listPages.Add(objPage);
                            //int templatePage = SaveTemplatePage(i, TemplateID, "Front", TemplateID + "/Side" + (i).ToString() + ".pdf");
                            // save pdf 
                            using (Doc singlePagePdf = new Doc())
                            {
                                try
                                {
                                    singlePagePdf.Rect.String = singlePagePdf.MediaBox.String = theDoc.MediaBox.String;
                                    singlePagePdf.AddPage();
                                    singlePagePdf.AddImageDoc(theDoc, i, null);
                                    singlePagePdf.FrameRect();

                                    int srcPageRot = theDoc.GetInfoInt(theDoc.Page, "/Rotate");
                                    if (srcDocRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcDocRot);
                                    }
                                    if (srcPageRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcPageRot);
                                    }
                                    string targetFolder = drURL;
                                    if (File.Exists(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf"))
                                    {
                                        File.Delete(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    }
                                    singlePagePdf.Save(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    singlePagePdf.Clear();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("GenerateTemplateBackground", e);
                                }
                                finally
                                {
                                    if (singlePagePdf != null)
                                        singlePagePdf.Dispose();
                                }
                            }
                            result = _templateRepository.updateTemplate(ProductID, pdfWidth, pdfHeight, listPages);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("GenerateTemplateThumbnail", ex);
                    }
                    finally
                    {
                        if (theDoc != null)
                            theDoc.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        // generate template pdf file called from MIS and webstore 
        private bool GenerateTemplatePdf(long productID, long organizationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners)
        {
            bool result = false;
            try
            {

                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/");
                string fontsUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/");
                List<TemplatePage> oTemplatePages = new List<TemplatePage>();
                List<TemplateObject> oTemplateObjects = new List<TemplateObject>();
                Template objProduct = _templateRepository.GetTemplate(productID, out oTemplatePages, out oTemplateObjects);
              //  string targetFolder = "";
                   // targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                    foreach (TemplatePage objPage in oTemplatePages)
                    {
                        bool hasOverlayObject = false;
                       // byte[] PDFFile = generatePDF(objProduct, objPage, drURL, fontsUrl, false, true, printCropMarks, printWaterMarks, out hasOverlayObject, false);
                        //writing the PDF to FS
                        System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + ".pdf", PDFFile);
                        //generate and write overlay image to FS 
                        generatePagePreview(PDFFile, drURL, productID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, isroundCorners);
                        if (hasOverlayObject)
                        {
                            // generate overlay PDF 
                        //    byte[] overlayPDFFile = generatePDF(objProduct, objPage, drURL, fontsUrl, false, true, printCropMarks, printWaterMarks, out hasOverlayObject, true);
                            // writing overlay pdf to FS 
                            System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
                            // generate and write overlay image to FS 
                            generatePagePreview(overlayPDFFile, drURL, productID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, isroundCorners);
                        }
                    }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
            return result;
        }
        #endregion
        #region constructor
        public TemplateService(ITemplateRepository templateRepository, IProductCategoryRepository ProductCategoryRepository,ITemplateBackgroundImagesService templateBackgroundImages)
        {
            this._templateRepository = templateRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._templateBackgroundImagesService = templateBackgroundImages;
        }
        #endregion

        #region public

        /// <summary>
        /// called from webstore usually for coping template  // added by saqib ali
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Template GetTemplate(long productID)
        {
            var product= _templateRepository.GetTemplate(productID);
            if (product.Orientation == 2) //rotating the canvas in case of vert orientation
            {
                double tmp = product.PDFTemplateHeight.Value;
                product.PDFTemplateHeight = product.PDFTemplateWidth;
                product.PDFTemplateWidth = tmp;
            }
            return product;
        }

        // called from designer, all the units are converted to pixel before sending  // added by saqib ali
        public Template GetTemplateInDesigner(long productID)
        {
            var product = _templateRepository.GetTemplate(productID);

            product.PDFTemplateHeight = DesignerUtils.PointToPixel(product.PDFTemplateHeight.Value);
            product.PDFTemplateWidth = DesignerUtils.PointToPixel(product.PDFTemplateWidth.Value);
            product.CuttingMargin = DesignerUtils.PointToPixel(product.CuttingMargin.Value);


            return product;
        }
        // delete template and all references   // added by saqib ali
        public bool DeleteTemplate(long ProductID, out long CategoryID, long organizationID)
        {
            var result = false;
            try
            {
                result=  _templateRepository.DeleteTemplate(ProductID, out CategoryID);
                var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/" + ProductID.ToString());
                if (Directory.Exists(drURL))
                {
                      foreach (string item in System.IO.Directory.GetFiles(drURL))
                        {
                            System.IO.File.Delete(item);
                        }

                    Directory.Delete(drURL);
                }
            }
            catch(Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
            return result;
        }
        public bool DeleteTemplateFiles(long ProductID, long organizationID)
        {
            try
            {

                bool result = false;

                var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/" + ProductID.ToString());
                if (Directory.Exists(drURL))
                {
                    foreach (string item in System.IO.Directory.GetFiles(drURL))
                    {
                        System.IO.File.Delete(item);
                    }

                    Directory.Delete(drURL);
                }

                result = true;

                return result;

            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
        }
        //copy template and all physical files  // added by saqib ali
        public long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName,long organizationID)
        {
            long result = 0;
            try 
            { 
                List<TemplatePage> objPages;
                List<TemplateBackgroundImage> objImages;
                 result = _templateRepository.CopyTemplate(ProductID, SubmittedBy, SubmittedByName, out objPages, organizationID,out objImages);
                 if (result != 0)
                 {
                     string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/");
                     string targetFolder = drURL + result.ToString();
                     //create template directory
                     if (!System.IO.Directory.Exists(targetFolder))
                     {
                         System.IO.Directory.CreateDirectory(targetFolder);
                     }
                     foreach (TemplatePage oTemplatePage in objPages)
                     {
                         //copy background pdfs and images
                         if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                         {
                             string filename = oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/"));
                             string destinationPath = Path.Combine(drURL + result.ToString() + "/" + filename);
                             string sourcePath = Path.Combine(drURL, ProductID.ToString() + "/" + filename);
                             if (!File.Exists(destinationPath) && File.Exists(sourcePath))
                             {
                                 //copy side 1
                                 File.Copy(sourcePath, destinationPath);
                             }
                             // copy side 1 image file if exist in case of pdf template
                             if (File.Exists(drURL + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg"))
                             {
                                 File.Copy(drURL + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", drURL + result.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", true);
                             }
                         }

                     }
                     //copy the template images

                     foreach (TemplateBackgroundImage item in objImages)
                     {
                         string ext = Path.GetExtension(item.ImageName);
                         string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                         string[] names = results[0].Split('/');
                         string filePath = drURL + "/" + ProductID.ToString() + "/" + names[names.Length - 1] + ext;
                         string filename;



                         // copy thumbnail 
                         if (!ext.Contains("svg"))
                         {

                             string imgName = names[names.Length - 1] + "_thumb" + ext;

                             string ThumbPath = drURL + "/" + ProductID.ToString() + "/" + imgName;
                             FileInfo oFileThumb = new FileInfo(ThumbPath);
                             if (oFileThumb.Exists)
                             {
                                 string oThumbName = oFileThumb.Name;
                                 oFileThumb.CopyTo((drURL + result.ToString() + "/" + oThumbName), true);
                             }
                         }
                         FileInfo oFile = new FileInfo(filePath);

                         if (oFile.Exists)
                         {
                             filename = oFile.Name;
                             oFile.CopyTo((drURL + result.ToString() + "/" + filename), true);
                         }
                     }
                 } else
                 {
                     throw new MPCException("Clone template failed due to store procedure. 'sp_cloneTemplate'", organizationID);
                 }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
            return result;
        }
        // copy list of templates called from MIS returns list of copied ids if id is null template is not copied  // added by saqib ali
        public List<long?> CopyTemplateList(List<long?> productIDList, long SubmittedBy, string SubmittedByName,long organizationID)
        {
            List<long?> newTemplateList = new List<long?>();
            try
            {
                foreach (long? ProductID in productIDList)
                {
                    if (ProductID != null && ProductID.HasValue)
                    {
                        long result = CopyTemplate(ProductID.Value, SubmittedBy, SubmittedByName, organizationID);
                        if (result != 0)
                        {
                            newTemplateList.Add(result);
                        }
                        else
                        {
                            newTemplateList.Add(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
            return newTemplateList;
        }
        // generate template from the given pdf file,called from MIS // added by saqib ali
        // filePhysicalPath =server.mappath( 'MPC_Content/Products/organization1/Templates/random__CorporateTemplateUpload.pdf')  // can be changed but it should be in mpcContent 
        //F:\\Development\\Github\\MyPrintCloud-dev\\MPC.web\\MPC_Content\\Products\\organization1\\Templates\\random__CorporateTemplateUpload.pdf
        //mode = 1 for creating template and removing all the existing objects  and images
        // mode = 2 for creating template and preserving template objects and images
        public bool generateTemplateFromPDF(string filePhysicalPath, int mode, long templateID, long organizationID)
        {
            bool result = false;
            try
            {
                if (mode == 2)
                {
                   result =  CovertPdfToBackground(filePhysicalPath, templateID, organizationID);
                }
                else
                {
                   result =  CovertPdfToBackgroundWithoutObjects(filePhysicalPath, templateID,organizationID);
                }
                if (File.Exists(filePhysicalPath))
                {
                    File.Delete(filePhysicalPath);
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
            return result;
        }

        // called from webstore and MIS to generate pdf file of the template.// added by saqib ali
        public void processTemplatePDF(long TemplateID, long organizationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners)
        {
            GenerateTemplatePdf(TemplateID, organizationID, printCropMarks, printWaterMarks, isroundCorners);
        }
        public List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID)
        {
            List<ProductCategoriesView> PCview = _ProductCategoryRepository.GetMappedCategoryNames(false, CompanyID);
            return _templateRepository.BindTemplatesList(TemplateName, pageNumber, CustomerID, CompanyID, PCview);
        }
        
        public string GetTemplateNameByTemplateID(int tempID)
        {
            return _templateRepository.GetTemplateNameByTemplateID(tempID);
        }

      
        public long CloneTemplateByTemplateID(long TempID)
        {
            return _templateRepository.CloneTemplateByTemplateID(TempID);
        }
        #endregion
    }
}
