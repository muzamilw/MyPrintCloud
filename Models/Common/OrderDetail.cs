using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class OrderDetail
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
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public long ContactUserID { get; set; }
        public long DeliveryCostCentreID { get; set; }
        public int DeliveryAddressID { get; set; }
        public int BillingAddressID { get; set; }
        public Int16? ClientStatusID { get; set; }
        public int? ContactTerritoryID { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string PlacedBy { get; set; }
        public string YourRef { get; set; }
        public string SpecialInstNotes { get; set; }
        public double OrderTotal { get; set; }
        public Address BillingAdress { get; set; }
        public Address ShippingAddress { get; set; }
                //Order Complete Details
        public ShoppingCart ItemDetail { get; set; }

        public  List<ProductItem> ProductsList { get; set; }

        public string DeliveryMethod { get; set; }

        public string Currency { get; set; }

        public double DeliveryCost { get; set; }
        public double DeliveryCostTaxValue { get; set; }
        public long? DeliveryDiscountVoucherId { get; set; }
    }
}
