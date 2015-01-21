using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class ShopCartAddressSelectViewModel
    {
        public ShoppingCart shopcart { get; set; }

        public Address ShippingAddress { get; set; }

        public Address BillingAddress { get; set; }

        public List<State> States { get; set; }

        public List<CostCentre> DeliveryCostCenters { get; set; }

        public List<Country> Countries { get; set; }
      

        List<Address> _ShippingAddresses = null;
        public List<Address> ShippingAddresses
        {
            get { return _ShippingAddresses; }
            set { _ShippingAddresses = value; }
        }

        public List<AddOnCostsCenter> SelectedItemsAddonsList { get; set; }
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

        public long SelectedDeliveryAddress { get; set; }

        public long SelectedBillingAddress { get; set; }

        public string LtrMessage { get; set; }

        public bool LtrMessageToDisplay { get; set; }










    }
}