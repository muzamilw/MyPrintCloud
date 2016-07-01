using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.Areas.WebstoreApi.Models
{
    public class RFQContactForm
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }

        public string Quantity { get; set; }
        public string Message { get; set; }
    }
}