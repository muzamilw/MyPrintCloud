using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class USTaxInfo
    {
        public string Zip { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string CountyFIPS { get; set; }
        public string StateName { get; set; }
        public string StateAbbreviation { get; set; }
        public decimal TotalTaxRate { get; set; }
        public string TotalTaxExempt { get; set; }
        public decimal StateRate { get; set; }
        public decimal CityRate { get; set; }
        public decimal CountyRate { get; set; }
        public decimal CountyDistrictRate { get; set; }
        public decimal CityDistrictRate { get; set; }
    }
}
