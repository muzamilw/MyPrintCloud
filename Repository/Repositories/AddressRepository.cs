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

        public List<Address>  GetAddressesByTerritoryID(Int64 TerritoryID)
        {
            return db.Addesses.Where(a => a.TerritoryId == TerritoryID && (a.isArchived == null || a.isArchived.Value == false) && (a.isPrivate == false || a.isPrivate == null)).ToList();
        }

        public Models.ResponseModels.AddressResponse GetAddress(Models.RequestModels.AddressRequestModel request)
        {
            int fromRow = (request.PageNo - 1)*request.PageSize;
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
            catch(Exception ex)
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

    }
}
