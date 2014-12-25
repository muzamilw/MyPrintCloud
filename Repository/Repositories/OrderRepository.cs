using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MPC.Models.Common;
using MPC.Interfaces.WebStoreServices;
namespace MPC.Repository.Repositories
{
    public class OrderRepository : BaseRepository<Estimate>, IOrderRepository
    {

        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        public OrderRepository(IUnityContainer container, IWebstoreClaimsHelperService myClaimHelper)
            : base(container)
        {
            this._myClaimHelper = myClaimHelper;
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Estimate> DbSet
        {
            get
            {
                return db.Estimates;
            }
        }

        public int GetFirstItemIDByOrderId(int orderId)
        {

            try
            {
                List<Item> itemsList = GetOrderItems(orderId);
                if (itemsList != null && itemsList.Count > 0)
                {
                    return Convert.ToInt32(itemsList[0].ItemId);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Item> GetOrderItems(int OrderId)
        {
            
            return (from r in db.Items
                    where r.EstimateId == OrderId && (r.ItemType == null || r.ItemType !=  (int)ItemTypes.Delivery)
                    select r).ToList();
        }

        public long CreateNewOrder(int customerID, int contactId,Company Company, Organisation Organisation , Prefix prefix, string orderTitle = null)
        {
            long orderId = 0;

                if (Company != null && Company.CompanyId > 0)
                    orderId = CreateOrder(Company, contactId, OrderStatus.ShoppingCart, Organisation , prefix, orderTitle);

            return orderId;
        }
        public long CreateOrder(Company tblCustomer, int ContactID, OrderStatus orderStatus,Organisation organisation,Prefix prefix, string orderTitle = null)
        {

            Estimate tblOrder = new Estimate();
            //tbl_Statuses tblOrderStatus = null;            
            long orderID = 0;
            short orderStatusID = (short)orderStatus;
            

            //CompanySiteManager companySiteManager = new CompanySiteManager();
          

                //tblOrderStatus = this.GetStatusByName(orderStatus);

               
                    tblOrder.CompanyId = (int)tblCustomer.CompanyId; // customeriD
                    tblOrder.ContactCompanyId = (int)tblCustomer.CompanyId;
                    tblOrder.OrganisationId = organisation.OrganisationId;
                    tblOrder.CompanyName = "N/A";
                    tblOrder.AddressId = (int)tblCustomer.Addresses.ToList()[0].AddressId;
                    tblOrder.ContactId = ContactID;
                    tblOrder.isEstimate = false;
                    tblOrder.StatusId = orderStatusID; //tblOrderStatus.StatusID, // E.G. SHOPPING CART.

                    tblOrder.SectionFlagId = 145;
                    tblOrder.Estimate_Name = string.IsNullOrWhiteSpace(orderTitle) ? "WebStore New Order" : orderTitle;

                    if (tblCustomer.SalesAndOrderManagerId1 != null)
                    {
                        tblOrder.SalesPersonId = (int)tblCustomer.SalesAndOrderManagerId1;
                        tblOrder.OrderManagerId = (int)tblCustomer.SalesAndOrderManagerId1;
                    }
                   

                    tblOrder.Estimate_Total = 0;
                    tblOrder.Classification1Id = 0;
                    tblOrder.OrderSourceId = 0;
                    tblOrder.isDirectSale = false;
                    //Created_by = Common.LoggedInID,
                    tblOrder.Order_CreationDateTime = DateTime.Now;
                    tblOrder.Order_Date = DateTime.Now;
                    tblOrder.StartDeliveryDate = DateTime.Now.AddDays(1);
                    tblOrder.FinishDeliveryDate = DateTime.Now.AddDays(2);
                    tblOrder.CreationDate = DateTime.Now;
                    tblOrder.CreationTime = DateTime.Now;
                   
                    // Get order prefix and update the order next number
                    

                    if (prefix != null)
                    {
                        tblOrder.Order_Code = prefix.OrderPrefix + "-001-" + prefix.OrderNext.ToString();
                        prefix.OrderNext = prefix.OrderNext + 1;
                    }

                    db.Estimates.Add(tblOrder);
                    if (db.SaveChanges() > 0)
                        orderID = tblOrder.EstimateId;

                
            
            return orderID;

        }
        public long GetOrderID(int customerID, int contactId, string orderTitle,Company company, Organisation org,Prefix prefix)
        {
            long orderID = 0;
            Estimate tblOrder = GetOrderByContactID(contactId, OrderStatus.ShoppingCart);

            if (tblOrder == null)
                orderID = CreateNewOrder(customerID, contactId,company,org,prefix,orderTitle);
            else
                orderID = tblOrder.EstimateId;
            tblOrder = null;

            return orderID;
        }

        public Estimate GetOrderByContactID(int contactID, OrderStatus orderStatus)
        {
            int orderStatusID = (int)orderStatus;
            List<Estimate> ordesList = db.Estimates.Include("tbl_items").Where(order => order.ContactId == contactID && order.StatusId == orderStatusID && order.isEstimate == false).Take(1).ToList();
            if (ordesList.Count > 0)
                return ordesList[0];
            else
                return null;
           
        }

        
    }
}
