using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
{
    /// <summary>
    /// Company Sites Mapper
    /// </summary>
    public static class CompanySitesMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.CompanySites CreateFrom(this DomainModels.CompanySites source)
        {
            return new ApiModels.CompanySites
            {
                CompanySiteId = source.CompanySiteId,
                CompanySiteName = source.CompanySiteName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                State = source.State,
                Country = source.Country,
                ZipCode = source.ZipCode,
                Tel = source.Tel,
                Fax = source.Fax,
                Email = source.Email,
                Mobile = source.Mobile,
                Url = source.Url,
                MisLogo = source.MisLogo,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CompanySites CreateFrom(this ApiModels.CompanySites source)
        {
            return new DomainModels.CompanySites
            {
                CompanySiteId = source.CompanySiteId,
                CompanySiteName = source.CompanySiteName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                State = source.State,
                Country = source.Country,
                ZipCode = source.ZipCode,
                Tel = source.Tel,
                Fax = source.Fax,
                Email = source.Fax,
                Mobile = source.Mobile,
                Url = source.Url,
                MisLogo = source.MisLogo,
            };
        }
        #endregion
    }
}