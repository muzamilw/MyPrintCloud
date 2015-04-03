using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class BestPress
    {
        public int MachineID { get; set; }
        public string MachineName { get; set; }
        public double Qty1Cost { get; set; }
        public double Qty1RunTime { get; set; }
        public double Qty2Cost { get; set; }
        public double Qty2RunTime { get; set; }
        public double Qty3Cost { get; set; }
        public double Qty3RunTime { get; set; }
        public bool isSelected { get; set; }
    }
}
