using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateService : ITemplateService
    {
        #region private
        public readonly ITemplateRepository _templateRepository;
        public readonly IProductCategoryRepository _ProductCategoryRepository;
        #endregion
        #region constructor
        public TemplateService(ITemplateRepository templateRepository, IProductCategoryRepository ProductCategoryRepository)
        {
            this._templateRepository = templateRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
        }
        #endregion

        #region public
        // called from webstore usually for coping template
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

        // called from designer, all the units are converted to pixel before sending 
        public Template GetTemplateInDesigner(long productID)
        {
            var product = _templateRepository.GetTemplate(productID);

            product.PDFTemplateHeight = DesignerUtils.PointToPixel(product.PDFTemplateHeight.Value);
            product.PDFTemplateWidth = DesignerUtils.PointToPixel(product.PDFTemplateWidth.Value);
            product.CuttingMargin = DesignerUtils.PointToPixel(product.CuttingMargin.Value);


            return product;
        }
        // delete template and all references 
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
        //copy template and all physical files 
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

        public List<long?> CopyTemplateList(List<long?> productIDList, long SubmittedBy, string SubmittedByName,long organizationID)
        {
            List<long?> newTemplateList = new List<long?>();
            foreach (long? ProductID in productIDList)
            {
                if (ProductID != null && ProductID.HasValue)
                {
                    long result = CopyTemplate(ProductID.Value, SubmittedBy, SubmittedByName, organizationID);
                    if( result != 0 )
                    {
                        newTemplateList.Add(result);
                    } else
                    {
                        newTemplateList.Add(null);
                    }
                }
            }
            return newTemplateList;
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

      
        public int CloneTemplateByTemplateID(int TempID)
        {
            return _templateRepository.CloneTemplateByTemplateID(TempID);
        }
        #endregion
    }
}
