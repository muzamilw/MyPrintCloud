using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Webstore.Models; 

namespace MPC.Webstore.ModelMappers
{
    public static class CmsPageMapper
    {
        #region
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static ApiModels.CmsPage CreateFrom(this DomainModels.CmsPage source)
        {
            return new ApiModels.CmsPage
            {
                CategoryId = source.CategoryId,
                CompanyId = source.CompanyId,
                description = source.description,
                isDisplay = source.isDisplay,
                isEnabled = source.isEnabled,
                isMPCAdd = source.isMPCAdd,
                isPromotionalOffer = source.isPromotionalOffer,
                isSepecialOffer = source.isSepecialOffer,
                isUserDefined = source.isUserDefined,
                LastModifiedDate = source.LastModifiedDate,
                MenuTitle = source.MenuTitle,
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
                OrganisationId = source.OrganisationId,
                PageBanner = source.PageBanner,
                PageHTML = source.PageHTML,
                PageId = source.PageId,
                PageKeywords = source.PageKeywords,
                PageName = source.PageName,
                PageRelativePath = source.PageRelativePath,
                PageTitle = source.PageTitle,
                SortOrder = source.SortOrder

            };
        }


        public static MPC.Models.DomainModels.CmsPage CreateFrom(this ApiModels.CmsPage source)
        {
            return new MPC.Models.DomainModels.CmsPage
            {
                CategoryId = source.CategoryId,
                CompanyId = source.CompanyId,
                description = source.description,
                isDisplay = source.isDisplay,
                isEnabled = source.isEnabled,
                isMPCAdd = source.isMPCAdd,
                isPromotionalOffer = source.isPromotionalOffer,
                isSepecialOffer = source.isSepecialOffer,
                isUserDefined = source.isUserDefined,
                LastModifiedDate = source.LastModifiedDate,
                MenuTitle = source.MenuTitle,
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
                OrganisationId = source.OrganisationId,
                PageBanner = source.PageBanner,
                PageHTML = source.PageHTML,
                PageId = source.PageId,
                PageKeywords = source.PageKeywords,
                PageName = source.PageName,
                PageRelativePath = source.PageRelativePath,
                PageTitle = source.PageTitle,
                SortOrder = source.SortOrder

            };
        }
        #endregion


    }
}