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

            Estimate tblOrder = null;
            //tbl_Statuses tblOrderStatus = null;            
            long orderID = 0;
            short orderStatusID = (short)orderStatus;
            

            //CompanySiteManager companySiteManager = new CompanySiteManager();
          

                //tblOrderStatus = this.GetStatusByName(orderStatus);

                tblOrder = new Estimate()
                {
                    CompanyId = (int)tblCustomer.CompanyId, // customeriD
                    OrganisationId = organisation.OrganisationId,
                    CompanyName = "N/A",
                    AddressId = (int)tblCustomer.Addresses.ToList()[0].AddressId,
                    ContactId = ContactID,
                    isEstimate = false,
                    StatusId = orderStatusID, //tblOrderStatus.StatusID, // E.G. SHOPPING CART.

                    SectionFlagId = 145,
                    Estimate_Name = string.IsNullOrWhiteSpace(orderTitle) ? "WebStore New Order" : orderTitle,


                    SalesPersonId = (int)tblCustomer.SalesAndOrderManagerId1,
                    OrderManagerId = (int)tblCustomer.SalesAndOrderManagerId1,

                    Estimate_Total = 0,
                    Classification1Id = 0,
                    OrderSourceId = 0,
                    isDirectSale = false,
                    //Created_by = Common.LoggedInID,
                    Order_CreationDateTime = DateTime.Now,
                    Order_Date = DateTime.Now,
                    StartDeliveryDate = DateTime.Now.AddDays(1),
                    FinishDeliveryDate = DateTime.Now.AddDays(2),
                    CreationDate = DateTime.Now,
                    CreationTime = DateTime.Now,
                   
                };

               
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
