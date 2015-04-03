using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class GlobalData
    {
        public double dblFilmUnitCost { get; set; }
        public double dblFilmUnitPrice { get; set; }
        public double dblFilmCostProcessCharge { get; set; }
        public double dblFilmPriceProcessCharge { get; set; }
        public double dblUnitPrice { get; set; }
        public double dblUnitCost { get; set; }
        public double dblPackPrice { get; set; }
        public double dblPackCost { get; set; }
        public double dblPlateUnitCost { get; set; }
        public double dblPlateUnitPrice { get; set; }
        public double dblPlateCost { get; set; }
        public double dblPlatePrice { get; set; }
        public double dblPlateProcessingtCost { get; set; }
        public double dblPlateProcessingPrice { get; set; }
        public static string ServicesList { get; set; }
    }
}
