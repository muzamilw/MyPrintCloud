using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Country Repository
    /// </summary>
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CountryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Country> DbSet
        {
            get
            {
                return db.Countries;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Countries for Current Organisation
        /// </summary>
        public override IEnumerable<Country> GetAll()
        {
            try
            {
                return DbSet.OrderBy(i => i.CountryName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Country> PopulateBillingCountryDropDown()
        {
            try
            {
              return db.Countries.OrderBy(i => i.CountryName).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public Country GetCountryByID(long CountryID)
        {
            try
            {
                Country country = db.Countries.Where(i => i.CountryId == CountryID).FirstOrDefault();
                return country;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the Name of the country by its id
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public string GetCountryNameById(long CountryId)
        {
            try
            {
                return db.Countries.Where(s => s.CountryId == CountryId).Select(n => n.CountryName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetCountryCodeById(long countryId)
        {

            try
            {
                    return db.Countries.Where(a => a.CountryId == countryId).Select(c => c.CountryCode).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Country GetCountryByName(string sCountryName)
        {
            
            try
            {
                return DbSet.FirstOrDefault(c => c.CountryName == sCountryName);

            }
            catch (Exception ex)
            {
                throw new MPCException("Country list is empty.", this.OrganisationId);
            }
            
        }
        #endregion
    }
}
