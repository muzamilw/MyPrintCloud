using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class MatchingSets
    {
        public long ProductID { get; set; }

        public long ProductCategoryID { get; set; }

        public string Code { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public bool IsDisabled { get; set; }

        public int PTempId { get; set; }

        public bool IsDoubleSide { get; set; }
        public bool IsUseBackGroundColor { get; set; }
        public int MultiPageCount { get; set; }
        public int Orientation { get; set; }

        public int Status { get; set; }
        public string SLThumbnail { get; set; }
        public string FullView { get; set; }
        public string SuperView { get; set; }

        public int BaseColorID { get; set; }
        public bool isEditorChoice { get; set; }
        public string CategoryName { get; set; }
        public int SubmittedBy { get; set; }
        public bool IsPrivate { get; set; }
        public int TemplateOwner { get; set; }
        public int DisplayOrder { get; set; }

    }
}
