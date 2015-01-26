using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MPC.Repository.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Address> DbSet
        {
            get
            {
                return db.Addesses;
            }
        }

        public List<Address> GetAddressesByTerritoryID(Int64 TerritoryID)
        {
            return db.Addesses.Where(a => a.TerritoryId == TerritoryID && (a.isArchived == null || a.isArchived.Value == false) && (a.isPrivate == false || a.isPrivate == null)).ToList();
        }

        public Models.ResponseModels.AddressResponse GetAddress(Models.RequestModels.AddressRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchFilter);
            bool isTerritoryInSearch = request.TerritoryId != 0;
            Expression<Func<Address, bool>> query =
                s =>
                    (isSearchFilterSpecified && (s.Email.Contains(request.SearchFilter)) ||
                     (s.AddressName.Contains(request.SearchFilter)) ||
                     !isSearchFilterSpecified)
                     && (isTerritoryInSearch && (s.TerritoryId == request.TerritoryId) && (s.CompanyId == request.CompanyId)) || !isTerritoryInSearch && (s.CompanyId == request.CompanyId)
                     ;

            int rowCount = DbSet.Count(query);
            // ReSharper disable once ConditionalTernaryEqualBranch
            IEnumerable<Address> addresses = request.IsAsc
                ? DbSet.Where(query)
                    .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new AddressResponse
                   {
                       RowCount = rowCount,
                       Addresses = addresses
                   };
        }

        public Address GetDefaultAddressByStoreID(Int64 StoreID)
        {
            return db.Addesses.Where(s => s.CompanyId == StoreID && s.IsDefaultAddress == true).FirstOrDefault();
        }
        public IEnumerable<Address> GetAllDefaultAddressByStoreID(Int64 StoreID)
        {
            return db.Addesses.Where(s => s.CompanyId == StoreID && s.IsDefaultAddress == true);
        }
        public Address GetAddressByID(long AddressID)
        {
            try
            {
                return db.Addesses.Where(a => a.AddressId == AddressID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Address> GetAddressByCompanyID(long companyID)
        {
            try
            {

                return db.Addesses.Where(c => c.CompanyId == companyID && (c.isArchived == null || c.isArchived.Value == false) && (c.isPrivate == false || c.isPrivate == null)).OrderBy(ad => ad.AddressName).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Address> GetAdressesByContactID(long contactID)
        {

            try
            {
                return db.Addesses.Where(a => a.ContactId == contactID && a.isPrivate == true).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<Address> GetBillingAndShippingAddresses(long TerritoryID)
        {
            try
            {
                return db.Addesses.Where(a => a.TerritoryId == TerritoryID && (a.isPrivate == null || a.isPrivate == false)).OrderBy(ad => ad.AddressName).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Address> GetContactCompanyAddressesList(long customerID)
        {
            var query = from Addr in db.Addesses
                        orderby Addr.AddressName
                        where Addr.CompanyId == customerID && Addr.isArchived == false
                        select Addr;

            return query.ToList();


        }

        public void UpdateAddress(Address billingAddress, Address deliveryAddress, long contactCompanyID)
        {

            try
            {
                if (billingAddress != null)
                {
                    Address currBillAddress = db.Addesses.Where(a => a.AddressId == billingAddress.AddressId).FirstOrDefault();
                    if (currBillAddress != null)
                    {

                        PopulateAddress(currBillAddress, billingAddress);
                    }
                    else
                    { // add new billing
                        currBillAddress = new Address();

                        //reset all the shipping flags
                        //dbContext.tbl_addresses.Where(tblAdd => tblAdd.ContactCompanyID == contactCompanyID).ToList().ForEach(add => add.IsDefaultShippingAddress = false);
                        PopulateAddress(currBillAddress, billingAddress);
                        currBillAddress.CompanyId = contactCompanyID;

                        currBillAddress.IsDefaultAddress = false;
                        currBillAddress.isArchived = false;
                        db.Addesses.Add(currBillAddress);

                        db.SaveChanges();

                        billingAddress.AddressId = currBillAddress.AddressId;
                    }
                }

                if (deliveryAddress != null)
                {
                    Address currDelAddress = db.Addesses.Where(a => a.AddressId == deliveryAddress.AddressId).FirstOrDefault();
                    if (currDelAddress != null)
                    {
                        PopulateAddress(currDelAddress, deliveryAddress);
                    }
                    else
                    { // add new shiipping
                        currDelAddress = new Address();
                        //reset all the shipping flags
                        //dbContext.tbl_addresses.Where(tblAdd => tblAdd.ContactCompanyID == contactCompanyID).ToList().ForEach(add => add.IsDefaultShippingAddress = false);
                        PopulateAddress(currDelAddress, deliveryAddress);
                        currDelAddress.CompanyId = contactCompanyID;

                        currDelAddress.IsDefaultAddress = false;
                        currDelAddress.isArchived = false;
                        db.Addesses.Add(currDelAddress);

                        db.SaveChanges();

                        deliveryAddress.AddressId = currDelAddress.AddressId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal static void PopulateAddress(Address tblAddress, Address address)
        {
            tblAddress.AddressName = address.AddressName;
            tblAddress.Address1 = address.Address1;
            tblAddress.Address2 = address.Address2;
            tblAddress.City = address.City;
            tblAddress.State = address.State;
            tblAddress.PostCode = address.PostCode;
            tblAddress.Tel1 = address.Tel1;
            tblAddress.Country = address.Country;
            tblAddress.CompanyId = address.CompanyId > 0 ? address.CompanyId : tblAddress.CompanyId;
            if (tblAddress.AddressId == 0)
            {
                tblAddress.isPrivate = address.isPrivate;
                tblAddress.ContactId = address.ContactId;
                tblAddress.TerritoryId = address.TerritoryId;
            }
            tblAddress.IsDefaultAddress = address.IsDefaultAddress;
            tblAddress.IsDefaultShippingAddress = address.IsDefaultShippingAddress;
        }

        /// <summary>
        /// Get addresses list billing, shipping and pickup address
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<Address> GetContactCompanyAddressesList(long BillingAddressId, long ShippingAddressid, long PickUpAddressId)
        {

            List<Address> oAddressList = new List<Address>();

            oAddressList.Add(db.Addesses.Where(estm => estm.AddressId == ShippingAddressid).FirstOrDefault());
            oAddressList.Add(db.Addesses.Where(estm => estm.AddressId == BillingAddressId).FirstOrDefault());
            if (PickUpAddressId > 0)
            {
                oAddressList.Add(db.Addesses.Where(estm => estm.AddressId == PickUpAddressId).FirstOrDefault());
            }
            return oAddressList;
        }
    }
}
