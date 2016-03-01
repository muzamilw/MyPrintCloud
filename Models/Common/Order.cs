using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
   public class Order
    {
        public long OrderID { get; set; }
        public string OrderCode { get; set; }
        public string ProductName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Int16? StatusID { get; set; }
        public Int32? StatusTypeID { get; set; }
        public string StatusName { get; set; }
        public string ClientStatusName { get; set; }
        public string CompanyName { get; set; }
        public long? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public long? ContactUserID { get; set; }
        public int? BrokerID { get; set; }
        public int DeliveryCostCentreID { get; set; }     
        public long DeliveryAddressID { get; set; }        
        public long BillingAddressID { get; set; }
        public int DiscountVoucherID { get; set; }
        public double VoucherDiscountRate { get; set; }
        public Int16? ClientStatusID { get; set; }
        public long? ContactTerritoryID { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string PlacedBy { get; set; }
        public string YourRef { get; set; }
        public string SpecialInstNotes { get; set; }
        public double OrderTotal { get; set; }
        public Company MyCompanySite { get; set; }
        //Order Complete Details
        public ShoppingCart OrderDetails { get; set;}
        public double DeliveryCost {get; set;}
        public string SOrderDate { get; set; }
        public string SOrderDeliveryDate { get; set; }
        public long TerritoryId { get; set;}
    
    }
}
