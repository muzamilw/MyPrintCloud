using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Models
{
    public class ReportDescriptor
    {
        public int Id { get; set; }
        public long ItemId { get; set; }

        public int ComboValue { get; set; }

        public string Datefrom { get; set; }
        public string DateTo { get; set; }
        public string ParamTextValue { get; set; }

        public int ComboValue2 { get; set; }
       
    }
}