using Microsoft.Practices.Unity;
using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class TemplateBackgroundImagesRepository : BaseRepository<TemplateBackgroundImage>, ITemplateBackgroundImagesRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateBackgroundImagesRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplateBackgroundImage> DbSet
        {
            get
            {
                return db.TemplateBackgroundImages;
            }
        }
        #endregion

        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public TemplateBackgroundImage Find(int id)
        {
            return DbSet.Find(id);
        }
        //delete all template background images from database
        public void DeleteTemplateBackgroundImages(long productID,out List<TemplateBackgroundImage> objTemplateImages)
        {
            objTemplateImages = db.TemplateBackgroundImages.Where(g => g.ProductId == productID).ToList();
            foreach (TemplateBackgroundImage c in objTemplateImages)
            {

                db.TemplateBackgroundImages.Remove(c);
            }
            db.SaveChanges();
        }
        // get all template images
        public List<TemplateBackgroundImage> GetProductBackgroundImages(long productId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var backgrounds = db.TemplateBackgroundImages.Where(g => g.ProductId == productId).ToList();
            return backgrounds;
        }
        public TemplateBackgroundImage DeleteBackgroundImage(long imageId)
        {
            var TemplateBackgroundImage = db.TemplateBackgroundImages.Where(g => g.Id == imageId).Single();
            if (TemplateBackgroundImage != null)
            {
                var objImgPer = db.ImagePermissions.Where(i => i.ImageId == imageId).ToList();
                foreach (var oPerm in objImgPer)
                {
                    db.ImagePermissions.Remove(oPerm);
                }
                db.TemplateBackgroundImages.Remove(TemplateBackgroundImage);
                db.SaveChanges();
            }
            return TemplateBackgroundImage;
        }
        public void UpdateCropedImage(int mode,string contentString,string newImgPath,string newBkImgPath,long objectID,long productID, int imgHeight, int imgWidth)
        {
            if (mode == 1)
            {
                foreach (TemplateObject c in db.TemplateObjects.Where(g => g.ContentString == contentString))
                {
                    c.ContentString = newImgPath;
                }
                string[] paths = contentString.Split(new string[] { "/Templates/" }, StringSplitOptions.None);
                string Comp = paths[paths.Length - 1];
                foreach (TemplateBackgroundImage c in db.TemplateBackgroundImages.Where(g => g.ImageName == Comp))
                {
                    c.ImageName = newBkImgPath;
                    c.Name = newBkImgPath;
                }
            }
            else
            {
                foreach (TemplateObject c in db.TemplateObjects.Where(g => g.ObjectId == objectID))
                {
                    c.ContentString = newImgPath;
                }
                var bgImg = new TemplateBackgroundImage();
                bgImg.Name = newBkImgPath;
                bgImg.ImageName = newBkImgPath;
                bgImg.ProductId = productID;

                bgImg.ImageWidth = imgWidth;
                bgImg.ImageHeight = imgHeight;

                bgImg.ImageType = 2;

                db.TemplateBackgroundImages.Add(bgImg);
            }
            db.SaveChanges();
        }
        //insert record of locally donwloaded image
        public void DownloadImageLocally(long productId, string ImgName, int imageType, int ImageWidth, int ImageHeight)
        {
            string[] fileName = ImgName.Split(new string[] { "/" }, StringSplitOptions.None);
            db.Configuration.LazyLoadingEnabled = false;
            string Imname = productId + "/" + fileName[fileName.Length - 1];
            var backgrounds = db.TemplateBackgroundImages.Where(g => g.ImageName == Imname && g.ImageType == imageType && g.ProductId == productId).SingleOrDefault();

            if (backgrounds == null)
            {

                    var bgImg = new TemplateBackgroundImage();
                    bgImg.Name = productId + "/" + fileName[fileName.Length - 1];
                    bgImg.ImageName = productId + "/" + fileName[fileName.Length - 1];
                    bgImg.ProductId = productId;

                    bgImg.ImageWidth = ImageWidth;
                    bgImg.ImageHeight = ImageHeight;

                    bgImg.ImageType = imageType;

                    db.TemplateBackgroundImages.Add(bgImg);

                    db.SaveChanges();

                }
            
        }

        public List<sp_GetImagesResponseModel> getImages(int isCalledFrom, int imageSetType, long productId, long contactCompanyId, long contactId, long territoryId, int pageNumber, string SearchKeyword, out int imageCount)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var imgCount = new ObjectParameter("imageCount", typeof(int));
            imgCount.Value = 0;
            var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            if(contact != null && contact.TerritoryId != null && contact.TerritoryId.HasValue)
                territoryId = contact.TerritoryId.Value;
            List<sp_GetTemplateImages_Result> result = db.sp_GetTemplateImages(isCalledFrom, imageSetType, productId, contactCompanyId, contactId, territoryId, pageNumber, 20, "", SearchKeyword, imgCount).ToList();
            imageCount =Convert.ToInt32( imgCount.Value);
            List<sp_GetImagesResponseModel> res = result.Select(g => new sp_GetImagesResponseModel
            {
                RowNum = g.RowNum,
                ID = g.ID,
                ProductID = g.ProductID,
                ImageName = g.ImageName,
                Name = g.Name,
                BackgroundImageRelativePath = g.BackgroundImageRelativePath,
                ImageType = g.ImageType,
                ImageWidth = g.ImageWidth,
                ImageHeight = g.ImageHeight,
                ImageTitle = g.ImageTitle,
                ImageDescription = g.ImageDescription,
                ImageKeywords = g.ImageKeywords
            }).ToList();
            return res;
        }
        public TemplateBackgroundImage getImage(long imgID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var img = db.TemplateBackgroundImages.Where(g => g.Id == imgID).SingleOrDefault();
            return img;
        }
        public TemplateBackgroundImage UpdateImage(long imageID, string imgTitle, string imgDescription, string imgKeywords, int imType)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var img = db.TemplateBackgroundImages.Where(g => g.Id == imageID).SingleOrDefault();
            if (img != null)
            {
                img.ImageTitle = imgTitle;
                img.ImageDescription = imgDescription;
                img.ImageKeywords = imgKeywords;
                if (imType != 0)
                {
                    img.ImageType = imType;
                }
                db.SaveChanges();
            }
            return img;
        }

        public List<CompanyTerritory> getCompanyTerritories(long companyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<CompanyTerritory> result = db.CompanyTerritories.Where(g => g.CompanyId == companyId).ToList();
            if( result.Count>0)
            {
                return result;
            }else
            {
                return null;
            }
        }
        public long insertImageRecord(List<TemplateBackgroundImage> listImages)
        {
            TemplateBackgroundImage bgImg = null;
            long result = 0;
            foreach(var images in listImages)
            {
               bgImg  = new TemplateBackgroundImage();
                bgImg.Name = images.Name;
                bgImg.ImageName = images.ImageName;
                bgImg.ProductId = images.ProductId;

                bgImg.ImageWidth = images.ImageWidth;
                bgImg.ImageHeight = images.ImageHeight;

                bgImg.ImageType = images.ImageType;
                bgImg.ImageTitle = images.ImageTitle;
                bgImg.UploadedFrom = images.UploadedFrom;
                bgImg.ContactCompanyId = images.ContactCompanyId;
                bgImg.ContactId = images.ContactId;
                db.TemplateBackgroundImages.Add(bgImg);
                db.SaveChanges();
            }
            result = bgImg.Id ;
            return result;
        }
        public List<ImagePermission> getImgTerritories(long imgID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            List<ImagePermission> listObj = db.ImagePermissions.Where(g => g.ImageId == imgID).ToList();
            return listObj;
        }
        public bool UpdateImgTerritories(long imgID, string territory)
        {
            bool result = false;
            string[] territories = territory.Split('_');


            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            List<ImagePermission> oldPermissions = db.ImagePermissions.Where(g => g.ImageId == imgID).ToList();
            foreach (var obj in oldPermissions)
            {
                db.ImagePermissions.Remove(obj);
            }
            foreach (string obj in territories)
            {
                if (obj != "")
                {
                     ImagePermission objPermission = new ImagePermission();
                     objPermission.ImageId = Convert.ToInt64(imgID);
                     objPermission.TerritoryID = Convert.ToInt64(obj);
                     db.ImagePermissions.Add(objPermission);
                }
            }
            db.SaveChanges();
            result = true;
            return result;
        }

        public List<RealEstateImage> getPropertyImages(long propertyId)
        {

            db.Configuration.LazyLoadingEnabled = true;
            var objList = from p in db.ListingImages
                          where (p.ListingId == propertyId)
                          select new
                          {
                              ImageId = p.ListingImageId,
                              ImageUrl = p.ImageURL

                          };
            List<RealEstateImage> objRes = new List<RealEstateImage>();
            
            foreach(var obj in objList)
            {
                RealEstateImage img = new RealEstateImage();
                img.ImageId = obj.ImageId;
                img.ImageUrl = obj.ImageUrl;
                objRes.Add(img);
            }

            return objRes;
            //WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            //return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objList)));   
        }
        #endregion
    }
}
