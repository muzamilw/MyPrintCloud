using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class CampaignEmailParams
    {
        public Int64 AddressID { get; set; }
        public long ContactId { get; set; }
        public long CompanyId { get; set; }
        public int EstimateID { get; set; }
        public int ItemID { get; set; }
        public int BrokerID { get; set; }
        public Int64 StoreID { get; set; }
        public Int64 SalesManagerContactID { get; set; }
        public int ApprovarID { get; set; }
        public int BrokerContactID { get; set; }
        public int SystemUserID { get; set; }
        public int CorporateManagerID { get; set; }
        public int RegistrationID { get; set; }
        public int SubscriberID { get; set; }
        public int MarketingID { get; set; }
        public int CompanySiteID { get; set; }
        public int InquiryID { get; set; }
        public int orderedItemID { get; set; }
        public int ID { get; set; }// report notes id
        public int SupplierContactID { get; set; }
        public int SupplierCompanyID { get; set; }
    }
}
