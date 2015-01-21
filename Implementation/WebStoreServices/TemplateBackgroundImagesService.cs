﻿using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using MPC.Common;
using MPC.Models.Common;
using System.Web;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateBackgroundImagesService : ITemplateBackgroundImagesService
    {
        #region private
        private readonly ITemplateBackgroundImagesRepository _templateImagesRepository;
        private readonly ITemplateService _templateService;
        private byte[] Crop(string Img, int Width, int Height, int X, int Y, int mode, string NfileName)
        {
            try
            {
                using (Image OriginalImage = Image.FromFile(Img))
                {
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        private void GenerateThumbNail(string sourcefile, string destinationfile, int width)
        {
            System.Drawing.Image image = null;
            int ThumbnailSizeWidth = 98;
            int ThumbnailSizeHeight = 98;
            Bitmap bmp = null;
            try
            {

                using (image = System.Drawing.Image.FromFile(sourcefile))
                {
                    int srcWidth = image.Width;
                    int srcHeight = image.Height;
                    int thumbWidth = width;
                    int thumbHeight;
                    float WidthPer, HeightPer;


                    int NewWidth, NewHeight;

                    if (srcWidth > srcHeight)
                    {
                        NewWidth = ThumbnailSizeWidth;
                        WidthPer = (float)ThumbnailSizeWidth / srcWidth;
                        NewHeight = Convert.ToInt32(srcHeight * WidthPer);
                    }
                    else
                    {
                        NewHeight = ThumbnailSizeHeight;
                        HeightPer = (float)ThumbnailSizeHeight / srcHeight;
                        NewWidth = Convert.ToInt32(srcWidth * HeightPer);
                    }
                    thumbWidth = NewWidth;
                    thumbHeight = NewHeight;
                    bmp = new Bitmap(thumbWidth, thumbHeight);
                    System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    System.Drawing.Rectangle rectDestination =
                           new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                    gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                    bmp.Save(destinationfile);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }
        #endregion
        #region constructor
        public TemplateBackgroundImagesService(ITemplateBackgroundImagesRepository templateImagesRepository, IProductCategoryRepository ProductCategoryRepository)
        {
            this._templateImagesRepository = templateImagesRepository;
        }
        #endregion
        #region public
        
        //delete all template images of the given tempalte // added by saqib
        public void DeleteTemplateBackgroundImages(long productID, long OrganisationID)
        {
            List<TemplateBackgroundImage> listImages ;
            _templateImagesRepository.DeleteTemplateBackgroundImages(productID, out listImages);
            var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + productID.ToString());
                
            foreach (var obj in listImages)
            {
                string sfilePath = drURL + "/"+  obj.ImageName;
                if (File.Exists(sfilePath))
                {
                    File.Delete(sfilePath);

                }
            }
        }
        // get template images list called from designer // added by saqib
        public List<TemplateBackgroundImage> GetProductBackgroundImages(long productId,long organisationID)
        {
            try
            {
                if (productId != 0)
                {
                    var backgrounds = _templateImagesRepository.GetProductBackgroundImages(productId);
                    foreach (var objBackground in backgrounds)
                    {
                        if (objBackground.ImageName != null && objBackground.ImageName != "")
                        {
                            objBackground.BackgroundImageRelativePath = "MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" + objBackground.ImageName;
                        }
                    }
                    return backgrounds;
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organisationID);
            }

            return null;
        }

        // delete a specific image from the template based on image id // added by saqib
        public bool DeleteProductBackgroundImage(long productID, long ImageID,long organisationID)
        {
            try
            {
                TemplateBackgroundImage objImage =  _templateImagesRepository.DeleteBackgroundImage(ImageID);
                if(objImage != null)
                {
                    var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" );
                    string sfilePath = drURL + "/"+  objImage.ImageName;
                    if (File.Exists(sfilePath))
                    {
                        File.Delete(sfilePath);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organisationID);
            }
            return false;
        }
        // crop a template image
        public string CropImage(string ImgName, int ImgX1, int ImgY1, int ImWidth1, int ImHeight1, string ImProductName, int mode, long objectID,long organisationID)
        {
            try
            {

                ImgName = ImgName.Replace("___", "/");
                ImgName = ImgName.Replace("%20", " ");
                string thumbName = "";
                string NewPath = "";
                string ContentString = ImgName;
                string newImgName = Path.GetFileNameWithoutExtension(ImgName);
                string NewImgPath;
                ImgName = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" + ImgName);

                thumbName = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" + ImProductName + "/" + newImgName + "_thumb" + Path.GetExtension(ImgName));
                using (Image OriginalImage = Image.FromFile(ImgName))
                {
                    using (Bitmap bmp = new Bitmap(ImWidth1,ImHeight1))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Convert.ToInt32(ImWidth1), Convert.ToInt32(ImHeight1)), Convert.ToInt32(ImgX1), Convert.ToInt32(ImgY1), Convert.ToInt32(ImWidth1), Convert.ToInt32(ImHeight1), GraphicsUnit.Pixel);

                            string fname = Path.GetFileNameWithoutExtension(ImgName);
                            string ext = Path.GetExtension(ImgName).ToLower();
                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1, 100);
                            string bgImgName = ImProductName + "/" + newImgName + numIterations + ext;
                            NewImgPath = "~/MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" + ImProductName + "/" + newImgName + numIterations + ext;
                            string NewImgPrdoctPath = ImProductName + "/" + newImgName + numIterations + ext;
                            NewPath =System.Web.Hosting.HostingEnvironment.MapPath(NewImgPath);
                            if (ext == ".jpg")
                            {
                                bmp.Save(NewPath, ImageFormat.Jpeg);
                            }
                            else if (ext == ".png")
                            {
                                bmp.Save(NewPath, ImageFormat.Png);
                            }
                            else if (ext == ".gif")
                            {
                                bmp.Save(NewPath, ImageFormat.Gif);
                            }
                            long pID = Convert.ToInt64(ImProductName);
                            _templateImagesRepository.UpdateCropedImage(mode, ContentString, NewImgPath, bgImgName, objectID, pID, ImWidth1, ImHeight1);
                        }
                    }
                    
                    string getGName = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" + ImProductName + "/" + Path.GetFileNameWithoutExtension(NewPath) + "_thumb" + Path.GetExtension(NewPath));
                    GenerateThumbNail(NewPath, getGName, 98);
                }
                if (mode == 1)
                {
                    if (File.Exists(ImgName))
                        File.Delete(ImgName);
                    if (File.Exists(thumbName))
                        File.Delete(thumbName);
                }

                return NewImgPath;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // called from designer to download an image in a template // added by saqib
        public string DownloadImageLocally(string ImgName, long TemplateID, string imgType,long organisationID)
        {
            int imageType = Convert.ToInt32(imgType);
            System.Drawing.Image objImage = null;
            ImgName = ImgName.Replace("___", "/");
            ImgName = ImgName.Replace("%20", " ");
            ImgName = ImgName.Replace("./", "");
            ImgName = ImgName.Replace("@@", ":");
            string NewImgPath = "";
            try
            {
                string designerPath = "http://designerv2.myprintcloud.com/";
                string ImgPath = "";
                if (ImgName.Contains("-999"))
                {

                    if (System.Configuration.ConfigurationManager.AppSettings["Designerv2Path"] != null)
                    {
                        designerPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Designerv2Path"]);
                    }
                    if (!ImgName.Contains(designerPath))
                    {
                        ImgPath = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" + ImgName);
                    }
                    else
                    {
                        ImgPath = ImgName;
                    }

                }
                else
                {
                    if (!ImgName.ToLower().Contains("mpc_content"))
                    {
                        ImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ImgName);
                    }
                    else
                    {
                        ImgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + ImgName);
                    }
                }
                string[] fileName = ImgName.Split(new string[] { "/" }, StringSplitOptions.None);
                string pathToDownload =  System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" )+ TemplateID.ToString() + "/" + fileName[fileName.Length - 1];
                DesignerUtils.DownloadFile(ImgPath, pathToDownload);
                // generate thumbnail 
                string ext = Path.GetExtension(fileName[fileName.Length - 1]);
                if (!ext.Contains("svg"))
                {
                    string sourcePath = pathToDownload;
                    //string ext = Path.GetExtension(uploadPath);
                    string[] results = sourcePath.Split(new string[] { ext }, StringSplitOptions.None);
                    string destPath = results[0] + "_thumb" + ext;
                    GenerateThumbNail(sourcePath, destPath, 98);
                }
                NewImgPath = "./MPC_Content/Designer/Organisation" + organisationID.ToString() + "/Templates/" + TemplateID.ToString() + "/" + fileName[fileName.Length - 1];
                int ImageWidth = 0,ImageHeight = 0;
                if (!Path.GetExtension(fileName[fileName.Length - 1]).Contains("svg"))
                {
                   using (objImage = System.Drawing.Image.FromFile(pathToDownload))
                   {
                            ImageWidth = objImage.Width;
                            ImageHeight = objImage.Height;
                            objImage.Dispose();
                   }
                 }
                _templateImagesRepository.DownloadImageLocally(TemplateID, ImgName, imageType, ImageWidth, ImageHeight);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (objImage != null)
                    objImage.Dispose();
            }
            return NewImgPath;

        }
        // called from designer to load images from DAM // added by saqib 
        public DesignerDamImageWrapper getImages(int isCalledFrom, int imageSetType, long productId, long contactCompanyID, long contactID, long territoryId, int pageNumner, string SearchKeyword, long OrganisationID)
        {
            try
            {
                if (SearchKeyword == "___notFound")
                {
                    SearchKeyword = "";
                }
                int imgCount = 0;
                var result = _templateImagesRepository.getImages(isCalledFrom, imageSetType, productId, contactCompanyID, contactID, territoryId, pageNumner, SearchKeyword, out imgCount);
                foreach (var objBackground in result)
                {
                    if (objBackground.ImageName != null && objBackground.ImageName != "")
                    {
                        objBackground.BackgroundImageRelativePath = "MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + objBackground.ImageName;
                    }

                }
                DesignerDamImageWrapper objWrapper = new DesignerDamImageWrapper();
                objWrapper.ImageCount = imgCount;
                objWrapper.objsBackground = result;
                return objWrapper; 
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }

 

        }
        //get a single image record //added by saqib
        public TemplateBackgroundImage getImage(long imgID, long OrganisationID)
        {
            int imageID = Convert.ToInt32(imgID);
            var img = _templateImagesRepository.getImage(imgID);
            if (img != null)
            {
                if (img.ImageName != null && img.ImageName != "")
                {
                    img.BackgroundImageRelativePath = "MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + img.ImageName;
                }
                if (img.ImageTitle == null)
                {
                    string[] imgs = img.ImageName.Split('/');
                    img.ImageTitle = imgs[imgs.Length - 1];

                }
            }
            return img;
        }

        // update an image record  //added by saqib
        public TemplateBackgroundImage UpdateImage(long imageID, int imType, string imgTitle, string imgDescription, string imgKeywords)
        {
            imgTitle = imgTitle.Replace("___", "/");
            imgDescription = imgDescription.Replace("___", "/");
            imgKeywords = imgKeywords.Replace("___", "/");
            imgKeywords = imgKeywords.Replace("__", ",");
            imgTitle = imgTitle.Replace("__", ",");
            imgDescription = imgDescription.Replace("__", ",");
            return _templateImagesRepository.UpdateImage(imageID, imgTitle, imgDescription, imgKeywords, imType);
        }

        public string InsertUploadedImageRecord(string imageName, long productId, int uploadedFrom, long contactId, long organisationId, int imageType, long contactCompanyID)
        {
            var result = "false";
            System.Drawing.Image objImage = null;
            // fileName = fileID;
            try
            {

                bool isPdfBackground = false;
                // string product = idOfObject1; productId
                string ext = System.IO.Path.GetExtension(imageName);
                //fileID += ext;
                imageName = imageName.Replace("%20", " ");
                bool isUploadedPDF = false; int bkPagesCount = 0;
                List<TemplateBackgroundImage> uploadedPdfRecords = null;

                if (productId != 0)
                {
                   // int productid = Convert.ToInt32(product);
                    int ImageWidth = 0;
                    int ImageHeight = 0;

                    string imgpath =  "Organisation" + organisationId + "/Templates/";


                    if (uploadedFrom == 1 || uploadedFrom == 2)
                    {
                        imgpath = "Organisation" + organisationId + "/Templates/" + "UserImgs/" + contactId; ;
                    }
                    else if (uploadedFrom == 3|| uploadedFrom == 4)
                    {
                        imgpath = "Organisation" + organisationId + "/Templates/" + "UserImgs/Retail/" + contactId;
                    }
                    string uploadPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + imgpath);

                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }

                    string RootPath = imgpath;
                    imgpath += "/" + imageName;
                    uploadPath += "/" + imageName;
                   // string uploadPath = HttpContext.Current.Server.MapPath(imgpath);

                    if (Path.GetExtension(uploadPath).Contains("pdf"))
                    {
                        //if (Convert.ToInt32(imageType) == 3)
                        //{
                        //    bkPagesCount = obj.generatePdfAsBackgroundDesigner(uploadPath, productId);
                        //    isPdfBackground = true;
                        //    result = "uploadedPDFBK";
                        //}
                        //else
                        //{
                        //    uploadedPdfRecords = obj.CovertPdfToBackgroundDesigner(uploadPath, productId, RootPath);
                        //    isUploadedPDF = true;
                        //}
                    }

                    string UploadPathForPDF = productId + "/";
                    string Imname = productId + "/" + imageName;
                    if (uploadedFrom == 1 || uploadedFrom == 2)
                    {
                        Imname = "UserImgs/" + contactId.ToString() + "/" + imageName;
                        UploadPathForPDF = "UserImgs/" + contactId.ToString() + "/";
                    }
                    else if (uploadedFrom == 3 || uploadedFrom == 4)
                    {
                        Imname = "UserImgs/Retail/" + contactId.ToString() + "/" + imageName;
                        UploadPathForPDF = "UserImgs/Retail/" + contactId.ToString() + "/";
                    }
                    List<TemplateBackgroundImage> listImages = new List<TemplateBackgroundImage>();
                    if (isUploadedPDF)
                    {
                        foreach (TemplateBackgroundImage obj in uploadedPdfRecords)
                        {
                            var bgImg = new TemplateBackgroundImage();
                            bgImg.Name = UploadPathForPDF + obj.Name;
                            bgImg.ImageName = UploadPathForPDF + obj.Name;
                            bgImg.ProductId = productId;

                            bgImg.ImageWidth = obj.ImageWidth;
                            bgImg.ImageHeight = obj.ImageHeight;

                            bgImg.ImageType = Convert.ToInt32(imageType);
                            bgImg.ImageTitle = imageName;
                            bgImg.UploadedFrom = Convert.ToInt32(uploadedFrom);
                            bgImg.ContactCompanyId = Convert.ToInt32(contactCompanyID);
                            bgImg.ContactId = Convert.ToInt32(contactId);
                            listImages.Add(bgImg);
                            // result = bgImg.ID.ToString();
                            result = "IsUploadedPDF";
                            // generate thumbnail 
                            string imgExt = Path.GetExtension(obj.Name);
                            string sourcePath = HttpContext.Current.Server.MapPath("Designer/Products/" + UploadPathForPDF + obj.Name);
                            //string ext = Path.GetExtension(uploadPath);
                            string[] results = sourcePath.Split(new string[] { imgExt }, StringSplitOptions.None);
                            string res = results[0];
                            string destPath = res + "_thumb" + imgExt;
                            GenerateThumbNail(sourcePath, destPath, 98);

                        }
                    }
                    else
                    {
                        if (!Path.GetExtension(uploadPath).Contains("svg"))
                        {
                            using (objImage = System.Drawing.Image.FromFile(uploadPath))
                            {
                                float res = objImage.HorizontalResolution;
                                if (res < 96)
                                {
                                    result = imageName;
                                }
                                ImageWidth = objImage.Width;
                                ImageHeight = objImage.Height;
                            }
                        }
                        var bgImg = new TemplateBackgroundImage();
                        bgImg.Name = Imname;
                        bgImg.ImageName = Imname;
                        bgImg.ProductId = productId;

                        bgImg.ImageWidth = ImageWidth;
                        bgImg.ImageHeight = ImageHeight;

                        bgImg.ImageType = Convert.ToInt32(imageType);
                        bgImg.ImageTitle = imageName;
                        bgImg.UploadedFrom = Convert.ToInt32(uploadedFrom);
                        bgImg.ContactCompanyId = Convert.ToInt32(contactCompanyID);
                        bgImg.ContactId = Convert.ToInt32(contactId);


                        listImages.Add(bgImg);
                      //  result = bgImg.ID.ToString();

                        // generate thumbnail 
                        if (!ext.Contains("svg"))
                        {
                           // Services.imageSvc objSvc = new Services.imageSvc();
                            string sourcePath = uploadPath;
                            //string ext = Path.GetExtension(uploadPath);
                            string[] results = sourcePath.Split(new string[] { ext }, StringSplitOptions.None);
                            string destPath = results[0] + "_thumb" + ext;
                            GenerateThumbNail(sourcePath, destPath, 98);
                        }
                    }
                    result =  _templateImagesRepository.insertImageRecord(listImages).ToString();
                    
                }
            }
            catch (Exception ex)
            {
                //AppCommon.LogException(ex);
                throw ex;
            }
            finally
            {
                if (objImage != null)
                {
                    objImage.Dispose();
                }

            }
            return result;
        }
        #endregion
    }
   
}
