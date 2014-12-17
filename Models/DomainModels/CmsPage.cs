﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual Company Company { get; set; }
        #endregion

        #region Additional Properties
        [NotMapped]
        public string FileName { get; set; }

        [NotMapped]
        public byte[] Image { get; set; }
        #endregion
    }
}
