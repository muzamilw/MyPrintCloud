using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace MPC.Webstore.ViewModels
{
    public class ShopCartAddressSelectViewModel
    {
        public ShoppingCart shopcart { get; set; }

        public Address ShippingAddress { get; set; }

        public Address BillingAddress { get; set; }

        public SelectList DDBillingStates { get; set; }

        public SelectList DDShippingStates { get; set; }

        public List<State> BillingStates { get; set; }

        public List<State> ShippingStates { get; set; }


        public SelectList DDBillingCountries { get; set; }

        public SelectList DDShippingCountries { get; set; }

        public List<CostCentre> DeliveryCostCenters { get; set; }
        public SelectList DDDeliveryCostCenters { get; set; }

        public List<Country> BillingCountries { get; set; }

        public List<Country> ShippingCountries { get; set; }

        


        public List<Address> ShippingAddresses { get; set; }
        public SelectList DDShippingAddresses { get; set; }
   

        public List<AddOnCostsCenter> SelectedItemsAddonsList { get; set; }
        public SelectList DDBillingAddresses { get; set; }

        public List<Address> BillingAddresses { get; set; }

        
        public List<ProductItem> ProductItems { get; set; }

        public string ContactTel { get; set; }

        public string PickUpAddress { get; set; }

        public bool HasAdminMessage { get; set; }

        public string AdminName { get; set; }

        public bool IsUserRole { get; set; }

        public bool isStoreModePrivate { get; set; }
        
        public string Currency { get; set; }

        public string TaxLabel { get; set; }

        public string Warning { get; set; }


        public long selectedShippAddress { get; set; }

        public double DeliveryCost
        {
            get;
            set;
        }

        public double DeliveryCostTaxVal
        {
            get;
            set;
        }

        public string Notes { get; set; }

        public string RefNumber { get; set; }

        public long CountryID { get; set; }
        public long StateID { get; set; }

        public int SelectedDeliveryAddress { get; set; }

        public long SelectedBillingAddress { get; set; }

        public long SelectedDeliveryCountry { get; set; }

        public long SelectedBillingCountry { get; set; }

        public long SelectedDeliveryState { get; set;}

        public long SelectedBillingState { get; set; }

        public long SelectedCostCentre { get; set; }


        public string LtrMessage { get; set; }

        public bool LtrMessageToDisplay { get; set; }

        public string RefNumRetail { get; set; }

        public string GrandTotal { get; set; }

        public string chkBoxDeliverySameAsBilling { get; set; }

        public string PostBackCallFrom { get; set; }

        public long OrderId { get; set; }

        public long SelectedDeliveryCostCentreId { get; set; }
        public long? DeliveryDiscountVoucherID { get; set; }
    }
}