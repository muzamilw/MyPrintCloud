using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Country Repository Interface
    /// </summary>
    public interface ICountryRepository : IBaseRepository<Country, long>
    {
        List<Country> PopulateBillingCountryDropDown();
        string GetCountryNameById(long CountryId);

        Country GetCountryByID(long CountryID);

        string GetCountryCodeById(long countryId);
        Country GetCountryByName(string sCountryName);
    }
}
