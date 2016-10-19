using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    public class PTVRequestModel
    {
        public int Orientation { get; set; }
        public int ReversePtvRows { get; set; }
        public int ReversePtvCols { get; set; }
        public bool isDoubleSided { get; set; }
        public bool isWorknTrun { get; set; }
        public bool isWorknTumble { get; set; }
        public bool ApplyPressRestrict { get; set; }
        public double ItemHeight { get; set; }
        public double ItemWidth { get; set; }
        public double PrintHeight { get; set; }
        public double PrintWidth { get; set; }
        public int Grip { get; set; }
        public double GripDepth { get; set; }
        public double HeadDepth { get; set; }
        public double PrintGutter { get; set; }
        public double ItemHorizentalGutter { get; set; }
        public double ItemVerticalGutter { get; set; }
        public double BleedArea { get; set; }
    }
}
