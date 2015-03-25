using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class LookupDeleteRequestModel
    {
        public  long GuillotinePTVId{ get; set; }
        public long LookupMethodId { get; set; }
    }
}