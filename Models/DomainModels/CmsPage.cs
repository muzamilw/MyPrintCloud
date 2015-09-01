using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    public class CmsPage
    {
        #region Persisted Properties
        public long PageId { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public string MenuTitle { get; set; }
        public string description { get; set; }
        public string PageRelativePath { get; set; }
        public int? SortOrder { get; set; }
        public string Meta_KeywordContent { get; set; }
        public string Meta_DescriptionContent { get; set; }
        public string Meta_HiddenDescriptionContent { get; set; }
        public string Meta_CategoryContent { get; set; }
        public string Meta_RobotsContent { get; set; }
        public string Meta_AuthorContent { get; set; }
        public string Meta_DateContent { get; set; }
        public string Meta_LanguageContent { get; set; }
        public string Meta_RevisitAfterContent { get; set; }
        public string Meta_Title { get; set; }
        public string PageHTML { get; set; }
        public bool? isUserDefined { get; set; }
        public long? CategoryId { get; set; }
        public bool? isPromotionalOffer { get; set; }
        public bool? isSepecialOffer { get; set; }
        public bool? isMPCAdd { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string PageBanner { get; set; }
        public string PageKeywords { get; set; }
        public bool? isEnabled { get; set; }
        public bool? isDisplay { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
        #endregion

        #region Reference Properties
        /// <summary>
        /// Company
        /// </summary>
       
       
        public virtual Company Company { get; set; }
        /// <summary>
        /// Page Category
        /// </summary>
        public virtual PageCategory PageCategory { get; set; }

        /// <summary>
        /// Cms Page Tags
        /// </summary>
        public virtual ICollection<CmsPageTag> CmsPageTags { get; set; }

        /// <summary>
        /// Cms Skin Page Widgets
        /// </summary>
        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }

        #endregion

        #region Additional Properties
        [NotMapped]
        public string FileName { get; set; }

        [NotMapped]
        public string Bytes { get; set; }
        #endregion

        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   
   
        public void Clone(CmsPage target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }

            target.PageName = PageName;
            target.PageTitle = PageTitle;
            target.MenuTitle = MenuTitle;
            target.description = description;
            target.PageRelativePath = PageRelativePath;
            target.SortOrder = SortOrder;
            target.Meta_KeywordContent = Meta_KeywordContent;
            target.Meta_DescriptionContent = Meta_DescriptionContent;
            target.PageName = PageName;

            target.Meta_HiddenDescriptionContent = Meta_HiddenDescriptionContent;
            target.Meta_CategoryContent = Meta_CategoryContent;
            target.Meta_RobotsContent = Meta_RobotsContent;
            target.Meta_AuthorContent = Meta_AuthorContent;
            target.Meta_DateContent = Meta_DateContent;
            target.Meta_LanguageContent = Meta_LanguageContent;
            target.Meta_RevisitAfterContent = Meta_RevisitAfterContent;
            target.Meta_Title = Meta_Title;
            target.PageHTML = PageHTML;

            target.isUserDefined = isUserDefined;
            target.CategoryId = CategoryId;
            target.isPromotionalOffer = isPromotionalOffer;
            target.isSepecialOffer = isSepecialOffer;
            target.isMPCAdd = isMPCAdd;
            target.LastModifiedDate = LastModifiedDate;
            target.PageBanner = PageBanner;
            target.PageKeywords = PageKeywords;
            target.isEnabled = isEnabled;
            target.isDisplay = isDisplay;
            target.OrganisationId = OrganisationId;
           

        }

        #endregion
    }
}
