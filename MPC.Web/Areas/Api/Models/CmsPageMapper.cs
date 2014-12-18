using System.IO;
using System.Linq;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Areas.Api.Models;
using ResponseDomainModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.Models
{
    public static class CmsPageMapper
    {
        #region
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static CmsPage CreateFrom(this DomainModels.CmsPage source)
        {
            byte[] bytes = null;
            if (source.PageBanner != null && File.Exists(source.PageBanner))
            {
                bytes = source.PageBanner != null ? File.ReadAllBytes(source.PageBanner) : null;
            }
            return new CmsPage
            {
                PageId = source.PageId,
                CategoryId = source.CategoryId,
                Meta_AuthorContent = source.Meta_AuthorContent,
                Meta_CategoryContent = source.Meta_CategoryContent,
                Meta_DescriptionContent = source.Meta_DescriptionContent,
                Meta_LanguageContent = source.Meta_LanguageContent,
                Meta_RevisitAfterContent = source.Meta_RevisitAfterContent,
                Meta_RobotsContent = source.Meta_RobotsContent,
                Meta_Title = source.Meta_Title,
                PageHTML = source.PageHTML,
                PageKeywords = source.PageKeywords,
                PageTitle = source.PageTitle,
                //Image = bytes
            };
        }

        /// <summary>
        /// Create From Api Model 
        /// </summary>
        public static DomainModels.CmsPage CreateFrom(this CmsPage source)
        {
            return new DomainModels.CmsPage
            {
                PageId = source.PageId,
                CategoryId = source.CategoryId,
                Meta_AuthorContent = source.Meta_AuthorContent,
                Meta_CategoryContent = source.Meta_CategoryContent,
                Meta_DescriptionContent = source.Meta_DescriptionContent,
                Meta_LanguageContent = source.Meta_LanguageContent,
                Meta_RevisitAfterContent = source.Meta_RevisitAfterContent,
                Meta_RobotsContent = source.Meta_RobotsContent,
                Meta_Title = source.Meta_Title,
                PageHTML = source.PageHTML,
                PageKeywords = source.PageKeywords,
                PageTitle = source.PageTitle,
                //FileName = source.FileName,
                //Image = source.Image,
            };
        }

        /// <summary>
        ///Create From For List View 
        /// </summary>
        public static CmsPageForListView CreateFromForListView(this DomainModels.CmsPage source)
        {
            return new CmsPageForListView
            {
                PageId = source.PageId,
                PageTitle = source.PageTitle,
                IsDisplay = source.isDisplay,
                IsEnabled = source.isEnabled,
                Meta_Title = source.Meta_Title,
                CategoryName = source.PageCategory != null ? source.PageCategory.CategoryName : string.Empty,
            };
        }

        /// <summary>
        /// Create From Domain Response Model
        /// </summary>
        public static SecondaryPageResponse CreateFrom(this ResponseDomainModels.SecondaryPageResponse source)
        {
            return new SecondaryPageResponse
            {
                CmsPages = source.CmsPages.Select(x => x.CreateFromForListView()),
                RowCount = source.RowCount
            };
        }

        #endregion
    }
}