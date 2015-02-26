using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.ViewModels
{
    public class ViewOrderDetails
    {
        public long SelectedOrderStatus { get; set; }

        public SelectList DDOderStatus { get; set; }
        
    }
}