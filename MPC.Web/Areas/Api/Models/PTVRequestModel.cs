using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class PTVRequestModel
    {
        int Orientation { get; set; }
        int ReversePtvRows { get; set; }
        int ReversePtvCols { get; set; }
        bool isDoubleSided { get; set; }
        bool isWorknTrun { get; set; }
        bool isWorknTumble { get; set; }
        bool ApplyPressRestrict { get; set; }
        double ItemHeight { get; set; }
        double ItemWidth { get; set; }
        double PrintHeight { get; set; }
        double PrintWidth { get; set; }
        int Grip { get; set; }
        double GripDepth { get; set; }
        double HeadDepth { get; set; }
        double PrintGutter { get; set; }
        double ItemHorizentalGutter { get; set; }
        double ItemVerticalGutter { get; set; }
    }
}