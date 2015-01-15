using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
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

        public List<sp_GetTemplateImages_Result> getImages(int isCalledFrom, int imageSetType, long productId, long contactCompanyId, long contactId, long territoryId, int pageNumber, string SearchKeyword, out int imageCount)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var imgCount = new ObjectParameter("imageCount", typeof(int));
            imgCount.Value = 0;
            List<sp_GetTemplateImages_Result> result = db.sp_GetTemplateImages(isCalledFrom, imageSetType, productId, contactCompanyId, contactId, territoryId, pageNumber, 20, "", SearchKeyword, imgCount).ToList();
            imageCount =Convert.ToInt32( imgCount.Value);
            return result;
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
        #endregion
    }
}
