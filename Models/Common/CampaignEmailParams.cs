﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class CampaignEmailParams
    {
        public long AddressId { get; set; }
        public long ContactId { get; set; }
        public long CompanyId { get; set; }
        public long EstimateId { get; set; }
        public long ItemId { get; set; }
        public int BrokerID { get; set; }
        public Int64 StoreID { get; set; }
        public Int64 SalesManagerContactID { get; set; }
        public int ApprovarID { get; set; }
        public int BrokerContactID { get; set; }
        public Guid? SystemUserId { get; set; }
        public long CorporateManagerID { get; set; }
        public int RegistrationID { get; set; }
        public int SubscriberID { get; set; }
        public int MarketingID { get; set; }
        public long OrganisationId { get; set; }
        public int InquiryId { get; set; }
        public int orderedItemID { get; set; }
        public int Id { get; set; }// report notes id
        public int SupplierContactID { get; set; }
        public int SupplierCompanyID { get; set; }
    }
}
