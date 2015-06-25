﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Models.ResponseModels
{
    public class ReportEmailResponse
    {
        
        public string To { get; set; }

        public string CC { get; set; }

        public string Subject { get; set; }

        public string Attachment { get; set; }

        public string AttachmentPath { get; set; }

        public string Signature { get; set; }

        public long ContactId { get; set; }

    }
}