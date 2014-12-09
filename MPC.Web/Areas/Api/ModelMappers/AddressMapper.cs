using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Address Mapper
    /// </summary>
    public static class AddressMapper
    {
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Address CreateFromSupplier(this Address source)
        {
            return new DomainModels.Address
            {
                AddressId = 0,
                AddressName = source.AddressName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                Country = source.Country,
                Tel1 = source.Tel1,
                Tel2 = source.Tel2,
                Fax = source.Fax,
                URL = source.URL,
                State = source.State,
                PostCode = source.PostCode,
                Extension1 = source.Extension1,
                Extension2 = source.Extension2,
                Email = source.Email,
                Reference = source.Reference,
                FAO = source.FAO,
            };
        }
    }
}