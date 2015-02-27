using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.ViewModels
{
    public class SearchOrderViewModel
    {
        public string FromData { get; set; }
        public string ToDate { get; set; }
        public string poSearch { get; set; }
        public int StatusId { get; set; }
        public long SelectedOrder { get; set; }

        public SelectList DDOderStatus { get; set; }
        
    }
}