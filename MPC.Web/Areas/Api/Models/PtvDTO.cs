using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class PtvDTO
    {
        public int LandScapeRows { get; set; }
        public int LandScapeCols { get; set; }

        public int PortraitRows { get; set; }
        public int PortraitCols { get; set; }
        public int LandScapeSwing { get; set; }
        public int PortraitSwing { get; set; }
        public int LandscapePTV { get; set; }
        public int PortraitPTV { get; set; }

        public byte[] Side1Image { get; set; }
        public byte[] Side2Image { get; set; }
    }
}