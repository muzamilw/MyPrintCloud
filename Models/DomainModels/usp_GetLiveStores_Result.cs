using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_GetLiveStores_Result
    {
        public long companyid { get; set; }
        public string name { get; set; }
        public short IsCustomer { get; set; }
        public string webaccesscode { get; set; }
        public string image { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string addressname { get; set; }
        public string city { get; set; }
        public string CountryName { get; set; }
        public string statename { get; set; }
        public string postcode { get; set; }
        public string geolatitude { get; set; }
        public string geolongitude { get; set; }
        public Nullable<bool> isArchived { get; set; }
        public Nullable<bool> IsDefaultAddress { get; set; }
        public Nullable<double> AllTimeTotal { get; set; }
        public Nullable<int> AllTimeCount { get; set; }
        public Nullable<double> LastWeekTotal { get; set; }
        public Nullable<int> LastWeekCount { get; set; }
        public Nullable<double> CurrentWeekTotal { get; set; }
        public Nullable<int> CurrentWeekCount { get; set; }
        public Nullable<double> CurrentMonthTotal { get; set; }
        public Nullable<int> CurrentMonthCount { get; set; }
        public Nullable<double> LastMonthTotal { get; set; }
        public Nullable<int> LastMonthCount { get; set; }
        public Nullable<double> Last3MonthTotal { get; set; }
        public Nullable<int> Last3MonthCount { get; set; }
        public string Domain { get; set; }
    }
}
