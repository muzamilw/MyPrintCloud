using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class AccessRight
    {
        public int RightId { get; set; }
        public int SectionId { get; set; }
        public string RightName { get; set; }
        public string Description { get; set; }
        public bool? CanEdit { get; set; }
        public bool IsSelected { get; set; }
    }
}