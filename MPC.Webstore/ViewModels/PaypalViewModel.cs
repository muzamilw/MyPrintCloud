using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class PaypalViewModel
    {
        public string formnameAs { get; set; }
        public string cmd { get; set; }
        public string business { get; set; }
        public string return_url { get; set; }
        public string notify_url { get; set; }
        public string cancel_url { get; set; }
        public string currency_code { get; set; }
        public string no_shipping { get; set; }
        public string URL { get; set; }
        public string rm { get; set; }
        public string BusinessMail { get; set; }
        public string sPassword { get; set; }
        public string handling_cart { get; set; }
        public string upload { get; set; }
        //
        public string discount_amount_cart { get; set; }
        public string item_name1 { get; set; }
        public string amount1 { get; set; }
        public string item_name2 { get; set; }
        public string amount2 { get; set; }
        public string item_name3 { get; set; }
        public string amount3 { get; set; }
        public string item_name4 { get; set; }
        public string amount4 { get; set; }
        public string item_name5 { get; set; }
        public string amount5 { get; set; }
        //public string pageOrderID { get; set; }
        public string txtJason { get; set; }

        public string custom { get; set; }
        public string tax_cart { get; set; }
    }


   
}