using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineList
    {
        public int MachineId { get; set; }
        public int? MachineCatId { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public string LookupMethod { get; set; }
        public double? maximumsheetweight { get; set; }
        public double? maximumsheetheight { get; set; }
        public double? maximumsheetwidth { get; set; }
        public double? minimumsheetheight { get; set; }
        public double? minimumsheetwidth { get; set; }
        public long? LookupMethodId { get; set; }
        public string LookupMethodName { get; set; }
        public bool? IsSpotColor { get; set; }
        public byte[] Image { get; set; }

        public bool? isSheetFed { get; set; }
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