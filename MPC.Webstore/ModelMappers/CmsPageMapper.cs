using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonModels = MPC.Models.Common;
using ApiModels = MPC.Webstore.Models;

namespace MPC.Webstore.ModelMappers
{
    public static class CmsPageMapper
    {
        #region
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static ApiModels.CmsPageModel CreateFrom(this CommonModels.CmsPageModel source)
        {
            return new ApiModels.CmsPageModel
            {
                CategoryId = source.CategoryId,
                CompanyId = source.CompanyId,
                isEnabled = source.isEnabled,
                isUserDefined = source.isUserDefined,
                Meta_AuthorContent = source.Meta_AuthorContent,
                Meta_CategoryContent = source.Meta_CategoryContent,
                Meta_DateContent = source.Meta_DateContent,
                Meta_DescriptionContent = source.Meta_DescriptionContent,
                Meta_HiddenDescriptionContent = source.Meta_HiddenDescriptionContent,
                Meta_KeywordContent = source.Meta_KeywordContent,
                Meta_LanguageContent = source.Meta_LanguageContent,
                Meta_RevisitAfterContent = source.Meta_RevisitAfterContent,
                Meta_RobotsContent = source.Meta_RobotsContent,
                Meta_Title = source.Meta_Title,
                PageId = source.PageId,
                PageName = source.PageName,
                PageTitle = source.PageTitle
            };
        }


        public static MPC.Models.Common.CmsPageModel CreateFrom(this ApiModels.CmsPageModel source)
        {
            return new MPC.Models.Common.CmsPageModel
            {
                CategoryId = source.CategoryId,
                CompanyId = source.CompanyId,
                isEnabled = source.isEnabled,
                isUserDefined = source.isUserDefined,
                
                Meta_AuthorContent = source.Meta_AuthorContent,
                Meta_CategoryContent = source.Meta_CategoryContent,
                Meta_DateContent = source.Meta_DateContent,
                Meta_DescriptionContent = source.Meta_DescriptionContent,
                Meta_HiddenDescriptionContent = source.Meta_HiddenDescriptionContent,
                Meta_KeywordContent = source.Meta_KeywordContent,
                Meta_LanguageContent = source.Meta_LanguageContent,
                Meta_RevisitAfterContent = source.Meta_RevisitAfterContent,
                Meta_RobotsContent = source.Meta_RobotsContent,
                Meta_Title = source.Meta_Title,
                PageId = source.PageId,
                PageName = source.PageName,
                PageTitle = source.PageTitle
            };
        }
        #endregion


    }
}