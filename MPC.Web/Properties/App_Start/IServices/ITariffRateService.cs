using System.Collections.Generic;
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Tariff Rate service interface
    /// </summary>
    public interface ITariffRateService
    {
        /// <summary>
        /// Get Base Data
        /// </summary>
        TariffRateBaseResponse GetBaseData();
       
        /// <summary>
        /// Load Tariff Rates
        /// </summary>
        TariffRateResponse LoadTariffRates(TariffRateSearchRequest tariffRateRequest);
        
        /// <summary>
        /// Get Hire Group Details For Tariff Rate
        /// </summary>
        HireGroupDetailResponse GetHireGroupDetailsForTariffRate(long standardRtMainId);
       
       /// <summary>
        /// Find Standard Rate Main By ID
       /// </summary>
       /// <param name="standardRateMain"></param>
       /// <returns></returns>
        StandardRateMain Find(long standardRateMain);
       
        /// <summary>
        /// Delete Standard Rate Main
        /// </summary>
        void DeleteTariffRate(StandardRateMain standardRateMain);
        
        /// <summary>
        /// Add Standard Rate Main
        /// </summary>
        TariffRateContent SaveTariffRate(StandardRateMain standardRateMain);
        
        /// <summary>
        /// Find Standard Rate 
        /// </summary>
        IEnumerable<StandardRate> FindStandardRate(long standardRtMainId, long hireGroupDetailId);
       
        /// <summary>
        /// Find By Tariff Type Code
        /// </summary>
        /// <param name="tariffTypeCode"></param>
        /// <returns></returns>
        IEnumerable<StandardRateMain> FindByTariffTypeCode(string tariffTypeCode);
        
        /// <summary>
        ///Find Tariff Type By Id
        /// </summary>
        /// <param name="tariffTypeId"></param>
        /// <returns></returns>
        TariffType FindTariffTypeById(long tariffTypeId);
    }
}
