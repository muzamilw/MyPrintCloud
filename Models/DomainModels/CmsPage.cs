using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CmsPage
    {
        public long PageId { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public string MenuTitle { get; set; }
        public string description { get; set; }
        public string PageRelativePath { get; set; }
        public Nullable<int> SortOrder { get; set; }
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
        public Nullable<bool> isUserDefined { get; set; }
        public Nullable<long> CategoryId { get; set; }
        public Nullable<bool> isPromotionalOffer { get; set; }
        public Nullable<bool> isSepecialOffer { get; set; }
        public Nullable<bool> isMPCAdd { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string PageBanner { get; set; }
        public string PageKeywords { get; set; }
        public Nullable<bool> isEnabled { get; set; }
        public Nullable<bool> isDisplay { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual Company Company { get; set; }

        public virtual PageCategory PageCategory { get; set; }
    }
}
