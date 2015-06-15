using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class ReportEmailResponseModel
    {

       
        public string To { get; set; }

        public string CC { get; set; }

        public string Subject { get; set; }

        public string Attachment { get; set; }

        public string AttachmentPath { get; set; }


        public string Signature { get; set; }
    }
}
