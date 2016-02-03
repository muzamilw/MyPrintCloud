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
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Addesses.Where(a => a.TerritoryId == TerritoryID && (a.isArchived == null || a.isArchived.Value == false) && (a.isPrivate == false || a.isPrivate == null)).ToList();
            }
            catch(Exception ex)
            {
                throw ex;

            }
        }

        public Models.ResponseModels.AddressResponse GetAddress(Models.RequestModels.AddressRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchFilter);
                bool isTerritoryInSearch = request.TerritoryId != 0;
                Expression<Func<Address, bool>> query =
                    s =>
                        (isSearchFilterSpecified && ((s.Email.Contains(request.SearchFilter))
                        ||
                         (s.City.Contains(request.SearchFilter)) ||
                         (s.AddressName.Contains(request.SearchFilter))) ||
                         !isSearchFilterSpecified)
                         && ((isTerritoryInSearch && (s.TerritoryId == request.TerritoryId)) || !isTerritoryInSearch) && (s.CompanyId == request.CompanyId) &&//&& (s.CompanyId == request.CompanyId)
                         (!s.isArchived.HasValue || !s.isArchived.Value);

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
            catch (Exception ex)
            {
                throw ex;

            }
           
        }

        public Address GetDefaultAddressByStoreID(Int64 StoreID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Addesses.Where(s => s.CompanyId == StoreID && s.IsDefaultAddress == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }
           
        }
        public IEnumerable<Address> GetAllDefaultAddressByStoreID(Int64 StoreID)
        {
            try
            {
                return db.Addesses.Where(s => s.CompanyId == StoreID && s.IsDefaultAddress == true);
            }
            catch (Exception ex)
            {
                throw ex;

            }
           
            
        }
        public IEnumerable<Address> GetAllAddressByStoreId(long storeId)
        {
            return db.Addesses.Where(s => s.CompanyId == storeId && s.isArchived != true);
        }
        public Address GetAddressByID(long AddressID)
        {
            db.Configuration.LazyLoadingEnabled = false;
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
            try
            {
                return db.Addesses.Where(a => a.CompanyId == customerID && a.isArchived != true ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
            try
            {
                tblAddress.AddressName = address.AddressName;
                tblAddress.Address1 = address.Address1;
                tblAddress.Address2 = address.Address2;
                tblAddress.City = address.City;
                tblAddress.State = address.State;
                tblAddress.PostCode = address.PostCode;
                tblAddress.Tel1 = address.Tel1;
                tblAddress.Country = address.Country;
                tblAddress.CountryId = address.CountryId;
                tblAddress.StateId = address.StateId;
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
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Get addresses list billing, shipping and pickup address
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<Address> GetContactCompanyAddressesList(long BillingAddressId, long ShippingAddressid, long PickUpAddressId)
        {
            try
            {
                List<Address> oAddressList = new List<Address>();

                oAddressList.Add(db.Addesses.Include("State").Include("Country").Where(estm => estm.AddressId == ShippingAddressid).FirstOrDefault());
                oAddressList.Add(db.Addesses.Include("State").Include("Country").Where(estm => estm.AddressId == BillingAddressId).FirstOrDefault());
                if (PickUpAddressId > 0)
                {
                    oAddressList.Add(db.Addesses.Where(estm => estm.AddressId == PickUpAddressId).FirstOrDefault());
                }
                else
                {
                    oAddressList.Add(null);
                }
                return oAddressList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public  List<Address> GetAddressesListByContactCompanyID(long contactCompanyId)
        {
            try
            {
                    return db.Addesses.Where(c => c.CompanyId == contactCompanyId && (c.isArchived == null || c.isArchived.Value == false) && (c.isPrivate == false || c.isPrivate == null)).OrderBy(ad => ad.AddressName).ToList();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  List<Address> GetsearchedAddress(long CompanyId, String searchtxt)
        {
            try
            {
                return db.Addesses.Where(c => c.CompanyId == CompanyId && c.AddressName.Contains(searchtxt) && (c.isArchived == null || c.isArchived.Value == false)).OrderBy(ad => ad.AddressName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Address GetAddressByAddressID(long AddressID)
        {
            try
            {
                return db.Addesses.Where(i => i.AddressId == AddressID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
          
        public bool UpdateBillingShippingAdd(Address Model)
        {
            bool Result = false;
            try
            {
                Address Address = db.Addesses.Where(i => i.AddressId == Model.AddressId).FirstOrDefault();
                Address.Address1 = Model.Address1;
                Address.Address2 = Model.Address2;
                Address.Address3 = Model.Address3;
                Address.AddressName = Model.AddressName;
                Address.City = Model.City;
                Address.FAO = Model.FAO;
                Address.Fax = Model.Fax;
                Address.PostCode = Model.PostCode;
                Address.Reference = Model.Reference;
                Address.Tel1 = Model.Tel1;
                Address.Tel2 = Model.Tel2;
                Address.Extension1 = Model.Extension1;
                Address.Extension2 = Model.Extension2;
                Address.GeoLatitude = Model.GeoLatitude;
                Address.GeoLongitude = Model.GeoLongitude;
                if (Model.CountryId == 0)
                {
                    Address.CountryId = null;
                }
                else {

                    Address.CountryId = Model.CountryId;
                }
                if (Model.StateId == 0)
                {
                    Address.StateId = null;
                }
                else
                {
                    Address.StateId = Model.StateId;
                }
                db.Addesses.Attach(Address);
                db.Entry(Address).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Result = true;
                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public bool AddressNameExist(Address address)
        {
            try
            {
                Address alreadyAddress = db.Addesses.Where(c => c.AddressId != address.AddressId && c.CompanyId == address.CompanyId && c.AddressName == address.AddressName).FirstOrDefault();
                if (alreadyAddress != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AddAddBillingShippingAdd(Address Address)
        {
            bool Result = false;
            try
            {
                Address AddAddress = new Address();
                AddAddress.Address1 = Address.Address1;
                AddAddress.Address2 = Address.Address2;
                AddAddress.Address3 = Address.Address3;
                AddAddress.AddressName = Address.AddressName;
                AddAddress.City = Address.City;
                AddAddress.FAO = Address.FAO;
                AddAddress.Fax = Address.Fax;
                AddAddress.PostCode = Address.PostCode;
                AddAddress.Reference = Address.Reference;
                AddAddress.Tel1 = Address.Tel1;
                AddAddress.Tel2 = Address.Tel2;
                AddAddress.Extension1 = Address.Extension1;
                AddAddress.Extension2 = Address.Extension2;
                AddAddress.GeoLatitude = Address.GeoLatitude;
                AddAddress.GeoLongitude = Address.GeoLongitude;
                AddAddress.TerritoryId = Address.TerritoryId;
                AddAddress.ContactId = Address.ContactId;
                if (Address.CountryId == 0)
                {
                    AddAddress.CountryId = null;
                }
                else {
                    AddAddress.CountryId = Address.CountryId;
                }
                if (Address.StateId == 0)
                {

                    AddAddress.StateId = null;
                }
                else {
                    AddAddress.StateId = Address.StateId;
                }
                AddAddress.CompanyId = Address.CompanyId;
                db.Addesses.Add(AddAddress);
                if (db.SaveChanges() > 0)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Result;
        }

        public  void ResetDefaultShippingAddress( Address address)
        {
            try
            {
                db.Addesses.Where(c => c.CompanyId == address.CompanyId && c.AddressId != address.AddressId).ToList().ForEach(add => add.IsDefaultShippingAddress = false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<State> GetCountryStates(long CountryId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<State> state = db.States.Where(i => i.CountryId == CountryId).ToList();
                return state;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Country> GetAllCountries()
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Countries.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<State> GetAllStates()
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.States.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public State GetStateByStateID(long StateID)
        {
            try
            {
                return db.States.Where(i => i.StateId == StateID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Country GetCountryByCountryID(long CountryID)
        {
            try
            {
                return db.Countries.Where(i => i.CountryId == CountryID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Address GetAddressById(long addressId)
        {
            try
            {
                return DbSet.Include("State").Include("Country").FirstOrDefault(x => x.AddressId == addressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Address GetAddressByIdforXML(int addressId)
        {
            try
            {
                return db.Addesses.Where(c => c.AddressId == addressId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     

    }
}
