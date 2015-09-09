using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyListViewModel
    {
        public long CompanyId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public short Status { get; set; }
        public short IsCustomer { get; set; }
        public byte[] Image { get; set; }
        public string ImageBytes { get; set; }
        public string DefaultDomain { get; set; }
        public bool? IsStoreLive { get; set; }
        /// <summary>
        /// Image Source
        /// </summary>
        public string ImageSource
        {
            get
            {
                if (Image == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(Image);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }
    }
}