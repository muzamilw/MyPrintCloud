using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class CmsPageModel
    {
        public long PageId { get; set; }
        public string PageTitle { get; set; }
        public string PageName { get; set; }
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
        public bool? isUserDefined { get; set; }
        public long? CategoryId { get; set; }
       
        public bool? isEnabled { get; set; }
    
        public long? CompanyId { get; set; }

        public bool? isDisplay { get; set; }
    }
}
