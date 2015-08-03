using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ShoppingCart
    {
        List<ProductItem> _cartItemsList = null;
        List<AddOnCostsCenter> _ItemsSelectedAddonsList = null;
        List<Address> _addressesList = null;



        public ShoppingCart()
        {
            this._ItemsSelectedAddonsList = new List<AddOnCostsCenter>();
            this._addressesList = new List<Address>();
        }


        public List<ProductItem> CartItemsList
        {
            get { return _cartItemsList; }
            set { _cartItemsList = value; }
        }

        public List<AddOnCostsCenter> ItemsSelectedAddonsList
        {
            get { return _ItemsSelectedAddonsList; }
            set { _ItemsSelectedAddonsList = value; }
        }


        public List<Address> AddressesList
        {
            get { return this._addressesList; }
            set { this._addressesList = value; }
        }



        public Double VoucherDiscountRate
        {
            get;
            set;
        }

        public Double DeliveryCost
        {
            get;
            set;
        }

        public Double DeliveryTaxValue
        {
            get;
            set;
        }
        public int DeliveryCostCenterID
        {
            get;
            set;
        }
        public long DiscountVoucherID
        {
            get;
            set;
        }
        public long BillingAddressID
        {
            get;
            set;
        }
        public long ShippingAddressID
        {
            get;
            set;
        }

        public string TaxLabel
        {
            get;
            set;
        }
    }
}
