using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Areas.Api.Models;
using ResponseDomainModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.Models
{
    public static class CmsPageMapper
    {
        #region Public
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static CmsPage CreateFrom(this DomainModels.CmsPage source)
        {
            byte[] bytes = null;

            if (!string.IsNullOrEmpty(source.PageBanner))
            {
                string filePath = HttpContext.Current.Server.MapPath("~/" + source.PageBanner);
                if (File.Exists(filePath))
                {
                    bytes = File.ReadAllBytes(filePath);
                }

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
                IsUserDefined = source.isUserDefined,
                isEnabled = source.isEnabled,
                PageKeywords = source.PageKeywords,
                PageTitle = source.PageTitle,
                Image = bytes,
                PageBanner = source.PageBanner,
                CompanyId=source.CompanyId
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
                isUserDefined = source.IsUserDefined,
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
                FileName = source.FileName,
                Bytes = source.Bytes,
                PageBanner = source.PageBanner,
                isEnabled = source.isEnabled,
                CompanyId = source.CompanyId
            };
        }

        /// <summary>
        ///Create From For List View 
        /// </summary>
        public static CmsPageForListView CreateFromForListView(this DomainModels.CmsPage source)
        {
            byte[] bytes = null;
            //if (!string.IsNullOrEmpty(source.PageBanner))
            //{
            //    string filePath = HttpContext.Current.Server.MapPath("~/" + source.PageBanner);
            //    if (File.Exists(filePath))
            //    {
            //        bytes = File.ReadAllBytes(filePath);
            //    }

            //}
            return new CmsPageForListView
            {
                PageId = source.PageId,
                PageTitle = source.PageTitle,
                IsDisplay = source.isDisplay,
                IsEnabled = source.isEnabled,
                Meta_Title = source.Meta_Title,
                IsUserDefined = source.isUserDefined,
                Image = bytes,
                CategoryName = source.PageCategory != null ? source.PageCategory.CategoryName : string.Empty,
            };
        }

        /// <summary>
        ///Create From For DropDown
        /// </summary>
        public static CmsPageDropDown CreateFromForDropDown(this DomainModels.CmsPage source)
        {
            return new CmsPageDropDown
            {
                PageId = source.PageId,
                PageTitle = source.PageTitle,
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

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CmsSkinPageWidget CreateFrom(this DomainModels.CmsSkinPageWidget source)
        {
            return new CmsSkinPageWidget
            {
                PageWidgetId = source.PageWidgetId,
                PageId = source.PageId,
                Sequence = source.Sequence,
                WidgetId = source.WidgetId,
                Html = source.Widget != null ? ReadCshtml(source.Widget) : string.Empty,
                WidgetName = source.Widget != null ? source.Widget.WidgetName : string.Empty,
                CmsSkinPageWidgetParams = source.CmsSkinPageWidgetParams != null ? source.CmsSkinPageWidgetParams.Select(pw => pw.CreateFrom()) : null
            };
        }


        #endregion

        #region Private
        /// <summary>
        /// Read Cshtml
        /// </summary>
        private static string ReadCshtml(DomainModels.Widget widget)
        {
            string html = string.Empty;
            switch (widget.WidgetControlName)
            {
                case "SavedDesignsWidget.ascx":
                    html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Stores/Views/Shared/_HomeWidget.cshtml"));
                    break;
                case "LoginBar.ascx":
                    html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Stores/Views/Shared/_AboutUs.cshtml"));
                    break;

            }
            return html;
        }

        #endregion
    }
}