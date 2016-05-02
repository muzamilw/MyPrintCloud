using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class WorldPayViewModel
    {
        public string formnameAs { get; set; }
    
        public string InstallationID { get; set; }
        public string return_url { get; set; }
        public string notify_url { get; set; }
        public string cancel_url { get; set; }
        public string currency_code { get; set; }
        public string no_shipping { get; set; }
        public string URL { get; set; }
        public string rm { get; set; }
       
        public string sPassword { get; set; }
        public string handling_cart { get; set; }
        public string upload { get; set; }
        public double OrderTotal { get; set; }

        public string OrderID { get; set; }
        public string tax_cart { get; set; }

        public string description { get; set; }

        public string fullName { get; set; }

        public string email { get; set; }

        public string address1 { get; set; }
        public string address2 { get; set; }

        public string city { get; set; }

        public string postcode { get; set; }

        public string country { get; set; }

        public string phone { get; set; }
    }


   
}